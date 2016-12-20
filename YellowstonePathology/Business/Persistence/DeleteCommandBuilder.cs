using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class DeleteCommandBuilder
    {                
        public DeleteCommandBuilder()
        {            
            
        }

        public void Build(object parentObject, Stack<MySqlCommand> deleteFirstCommands, Stack<MySqlCommand> deleteCommands)
        {
            Type objectType = parentObject.GetType();
            this.ProcessObjectForDelete(parentObject, objectType, deleteFirstCommands, deleteCommands);
            this.HandlePersistentChildCollections(parentObject, objectType, deleteFirstCommands, deleteCommands);
            this.HandlePersistentChildren(parentObject, objectType, deleteFirstCommands, deleteCommands);
        }

        private void ProcessObjectForDelete(object objectToDelete, Type objectType, Stack<MySqlCommand> deleteFirstCommands, Stack<MySqlCommand> deleteCommands)
        {
            PersistentClass persistentClassAttribute = (PersistentClass)objectType.GetCustomAttributes(typeof(PersistentClass), false).Single();
            PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            PropertyBridge keyPropertyBridge = PropertyBridgeFactory.GetPropertyBridge(keyProperty, objectToDelete);

			if (persistentClassAttribute.IsPersisted == true)
			{
                if (persistentClassAttribute.IsManyToManyRelationship == true)
                {
                    deleteFirstCommands.Push(this.CreateSqlCommand(keyPropertyBridge, persistentClassAttribute.StorageName));
                }
                else
                {
                    deleteCommands.Push(this.CreateSqlCommand(keyPropertyBridge, persistentClassAttribute.StorageName));
                }
			}

            if (persistentClassAttribute.HasPersistentBaseClass == true)
            {
                if (persistentClassAttribute.IsManyToManyRelationship == true)
                {
                    deleteFirstCommands.Push(this.CreateSqlCommand(keyPropertyBridge, persistentClassAttribute.BaseStorageName));
                }
                else
                {
                    deleteCommands.Push(this.CreateSqlCommand(keyPropertyBridge, persistentClassAttribute.BaseStorageName));
                }
            }
        }

        private void HandlePersistentChildCollections(object parentObject, Type objectType, Stack<MySqlCommand> deleteFirstCommands, Stack<MySqlCommand> deleteCommands)
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
                IEnumerable<object> childCollectionObject = (IEnumerable<object>)propertyInfo.GetValue(parentObject, null);
                foreach (object listObject in childCollectionObject)
                {
                    this.ProcessObjectForDelete(listObject, listObject.GetType(), deleteFirstCommands, deleteCommands);
                    this.HandlePersistentChildCollections(listObject, listObject.GetType(), deleteFirstCommands, deleteCommands);
                    this.HandlePersistentChildren(listObject, listObject.GetType(), deleteFirstCommands, deleteCommands);
                }
            }
        }

        private void HandlePersistentChildren(object objectToInsert, Type objectType, Stack<MySqlCommand> deleteFirstCommands, Stack<MySqlCommand> deleteCommands)
        {
            List<PropertyInfo> childProperties = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentChild))).ToList();
            foreach (PropertyInfo propertyInfo in childProperties)
            {
                object childObject = propertyInfo.GetValue(objectToInsert, null);

				if (childObject != null)
				{
                    this.ProcessObjectForDelete(childObject, childObject.GetType(), deleteFirstCommands, deleteCommands);
                    this.HandlePersistentChildCollections(childObject, childObject.GetType(), deleteFirstCommands, deleteCommands);
                    this.HandlePersistentChildren(childObject, childObject.GetType(), deleteFirstCommands, deleteCommands);
				}
            }
        }

        private MySqlCommand CreateSqlCommand(PropertyBridge keyPropertyBridge, string storageName)
        {
            MySqlCommand cmd = new MySqlCommand();            
                        
            StringBuilder sqlStatement = new StringBuilder("Delete " + storageName);
            sqlStatement.Append(" Where " + keyPropertyBridge.Name + " = " + keyPropertyBridge.AtName);
            
            cmd.CommandText = sqlStatement.ToString();
            keyPropertyBridge.SetSqlParameter(cmd);

            return cmd;                                
        }       
    }
}
