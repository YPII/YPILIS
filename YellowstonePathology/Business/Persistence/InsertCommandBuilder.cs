using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class InsertCommandBuilder
    {                        
        private bool m_InsertSubclassOnly;

        public InsertCommandBuilder()
        {
            
        }

        public void Build(object parentObject, Queue<SqlCommand> insertCommands, Queue<SqlCommand> insertLastCommands)
        {            
            this.m_InsertSubclassOnly = false;
            Type objectType = parentObject.GetType();
            this.ProcessObjectForInsert(parentObject, objectType, insertCommands, insertLastCommands);
            this.HandlePersistentChildCollections(parentObject, objectType, insertCommands, insertLastCommands);            
        }

        public void BuildSubclassOnly(object parentObject, Queue<SqlCommand> insertCommands, Queue<SqlCommand> insertLastCommands)
        {            
            this.m_InsertSubclassOnly = true;
            Type objectType = parentObject.GetType();
            this.ProcessObjectForInsert(parentObject, objectType, insertCommands, insertLastCommands);                        
        }

        private void HandlePersistentChildCollections(object parentObject, Type objectType, Queue<SqlCommand> insertCommands, Queue<SqlCommand> insertLastCommands)
        {
            List<PropertyInfo> childCollectionProperties = objectType.GetProperties()
                .Where(
                    prop => prop.GetCustomAttributes(typeof(PersistentCollection), true)
                        .Where(pc => ((PersistentCollection)pc).IsBuildOnly == false)
                        .Any()
                        )
                .ToList();

            foreach (PropertyInfo propertyInfo in childCollectionProperties)
            {                
                IList childCollectionObject = (IList)propertyInfo.GetValue(parentObject, null);
                foreach (object listObject in childCollectionObject)
                {
                    this.ProcessObjectForInsert(listObject, listObject.GetType(), insertCommands, insertLastCommands);
                    this.HandlePersistentChildCollections(listObject, listObject.GetType(), insertCommands, insertLastCommands);
                    this.HandlePersistentChildren(listObject, listObject.GetType(), insertCommands, insertLastCommands);
                }                
            }
        }

        private void HandlePersistentChildren(object objectToInsert, Type objectType, Queue<SqlCommand> insertCommands, Queue<SqlCommand> insertLastCommands)
        {
            List<PropertyInfo> childProperties = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentChild))).ToList();
            foreach (PropertyInfo propertyInfo in childProperties)
            {
                object childObject = propertyInfo.GetValue(objectToInsert, null);
                if (childObject != null)
                {
                    this.ProcessObjectForInsert(childObject, childObject.GetType(), insertCommands, insertLastCommands);
                    this.HandlePersistentChildCollections(childObject, childObject.GetType(), insertCommands, insertLastCommands);
                    this.HandlePersistentChildren(childObject, childObject.GetType(), insertCommands, insertLastCommands);
                }
            }
        }        

        private void ProcessObjectForInsert(object objectToInsert, Type objectType, Queue<SqlCommand> insertCommands, Queue<SqlCommand> insertLastCommands)
        {                        
            PersistentClass persistentClassAttribute = (PersistentClass)objectType.GetCustomAttributes(typeof(PersistentClass), false).Single();
            PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
			PersistentPrimaryKeyProperty keyAttribute = (PersistentPrimaryKeyProperty)keyProperty.GetCustomAttributes(typeof(PersistentPrimaryKeyProperty), false).Single();
            PropertyBridge keyPropertyBridge = PropertyBridgeFactory.GetPropertyBridge(keyProperty, objectToInsert);
            string keyPropertyValue = (string)keyPropertyBridge.GetPropertyValue();
            
            if (this.m_InsertSubclassOnly == false)
            {
                if (persistentClassAttribute.HasPersistentBaseClass == true)
                {
                    Type rootType = PersistenceHelper.GetRootType(objectToInsert);
                    List<PropertyBridge> basePropertyBridgeList = this.GetPropertyBridgeList(rootType, objectToInsert);                    
                    if (persistentClassAttribute.IsManyToManyRelationship == true)
                    {
                        SqlCommand sqlCommand = this.CreateSqlCommand(keyPropertyBridge, basePropertyBridgeList, persistentClassAttribute.BaseStorageName, keyAttribute.IsAutoGenerated);
                        insertLastCommands.Enqueue(sqlCommand);
                    }
                    else
                    {
                        SqlCommand sqlCommand = this.CreateSqlCommand(keyPropertyBridge, basePropertyBridgeList, persistentClassAttribute.BaseStorageName, keyAttribute.IsAutoGenerated); 
                        insertCommands.Enqueue(sqlCommand);
                    }                    
                }
            }
            
            if (persistentClassAttribute.IsPersisted == true)
            {
                List<PropertyBridge> propertyBridgeList = this.GetPropertyBridgeList(objectType, objectToInsert);
                if (persistentClassAttribute.IsManyToManyRelationship == true)
                {
                    SqlCommand sqlCommandToAdd = this.CreateSqlCommand(keyPropertyBridge, propertyBridgeList, persistentClassAttribute.StorageName, keyAttribute.IsAutoGenerated);
                    insertLastCommands.Enqueue(sqlCommandToAdd);
                }
                else
                {
                    SqlCommand sqlCommand = this.CreateSqlCommand(keyPropertyBridge, propertyBridgeList, persistentClassAttribute.StorageName, keyAttribute.IsAutoGenerated);
                    insertCommands.Enqueue(sqlCommand);
                }
            }            
        }        

        private SqlCommand CreateSqlCommand(PropertyBridge keyPropertyBridge, List<PropertyBridge> propertyBridgeList, string storageName, bool isAutoGenerated)
        {
            SqlCommand cmd = new SqlCommand();   

            StringBuilder sqlUpdateStatement = new StringBuilder("Insert " + storageName);
            StringBuilder fieldList = new StringBuilder();
            StringBuilder parameterList = new StringBuilder();

            for (int i = 0; i < propertyBridgeList.Count; i++)
            {
                fieldList.Append("[" + propertyBridgeList[i].Name + "]");
                fieldList.Append(", ");

                parameterList.Append(propertyBridgeList[i].AtName);
                parameterList.Append(", ");
            }

			if (isAutoGenerated == true)
			{
				fieldList.Remove(fieldList.Length - 2, 2);
				parameterList.Remove(parameterList.Length - 2, 2);
			}
			else
			{
				fieldList.Append(keyPropertyBridge.Name);
				parameterList.Append(keyPropertyBridge.AtName);
			}

            sqlUpdateStatement.Append("(" + fieldList + ")");
            sqlUpdateStatement.Append(" Values ");
            sqlUpdateStatement.Append("(" + parameterList + ")");

            cmd.CommandText = sqlUpdateStatement.ToString();
            keyPropertyBridge.SetSqlParameter(cmd);
            foreach (PropertyBridge propertyBridge in propertyBridgeList)
            {
                propertyBridge.SetSqlParameter(cmd);
            }

            return cmd;            
        }        

        private List<PropertyBridge> GetPropertyBridgeList(Type type, object o)
        {            
            List<PropertyBridge> propertyBridgeList = new List<PropertyBridge>();
            List<PropertyInfo> properties = PersistenceHelper.GetPropertiesToHandle(type);

            foreach (PropertyInfo property in properties)
            {                
                var currentValue = property.GetValue(o, null);
                if (currentValue != null)
                {
                    PropertyBridge propertyBridge = PropertyBridgeFactory.GetPropertyBridge(property, o);
                    propertyBridgeList.Add(propertyBridge);
                }                
            }

            return propertyBridgeList;
        }
        
    }
}
