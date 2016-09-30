using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class UpdateCommandBuilder
    {                
        public UpdateCommandBuilder()
        {                        
        }

		public void Build(object parentObject, object originalValues, Queue<SqlCommand> commandQueue)
        {			
            Type objectType = parentObject.GetType();
            this.ProcessObjectForUpdate(parentObject, objectType, originalValues, commandQueue);
            this.HandlePersistentChildCollections(parentObject, objectType, originalValues, commandQueue);         
        }

        private void HandlePersistentChildCollections(object parentObject, Type objectType, object originalValues, Queue<SqlCommand> commandQueue)
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
                IList childCollectionObjectOriginalValues = (IList)propertyInfo.GetValue(originalValues, null);
                
                for (int i = 0; i < childCollectionObject.Count; i++)
                {
                    object itemOriginalValueObject = this.GetOriginalValueObject(childCollectionObject[i], childCollectionObjectOriginalValues);
                    if (itemOriginalValueObject != null) //If it's null then it's an insert not an update
                    {
                        this.ProcessObjectForUpdate(childCollectionObject[i], childCollectionObject[i].GetType(), itemOriginalValueObject, commandQueue);
                        this.HandlePersistentChildCollections(childCollectionObject[i], childCollectionObject[i].GetType(), itemOriginalValueObject, commandQueue);
                        this.HandlePersistentChildren(childCollectionObject[i], childCollectionObject[i].GetType(), itemOriginalValueObject, commandQueue);
                    }
                }               
            }
        }

        private object GetOriginalValueObject(object childObject, IList originalValueCollection)
        {
            object result = null;

            Type objectType = childObject.GetType();
            PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            object keyPropertyValue = keyProperty.GetValue(childObject, null);
            
            for (int i = 0; i < originalValueCollection.Count; i++)
            {
                object collectionItem = originalValueCollection[i];
                Type collectionItemType = collectionItem.GetType();
                PropertyInfo collectionItemKeyProperty = collectionItemType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
                object collectionItemKeyValue = collectionItemKeyProperty.GetValue(collectionItem, null);
                
                if (collectionItemKeyValue.Equals(keyPropertyValue) == true)
                {
                    result = collectionItem;
                    break;
                }
            }

            return result;
        }

        private void HandlePersistentChildren(object parentObject, Type objectType, object parentObjectOriginalValues, Queue<SqlCommand> commandQueue)
        {
            List<PropertyInfo> childProperties = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentChild))).ToList();
            foreach (PropertyInfo propertyInfo in childProperties)
            {
                object childObject = propertyInfo.GetValue(parentObject, null);
                object childObjectOriginalValues = propertyInfo.GetValue(parentObjectOriginalValues, null);

                if (childObject != null)
                {
                    this.ProcessObjectForUpdate(childObject, childObject.GetType(), childObjectOriginalValues, commandQueue);
                    this.HandlePersistentChildCollections(childObject, childObject.GetType(), childObjectOriginalValues, commandQueue);
                    this.HandlePersistentChildren(childObject, childObject.GetType(), childObjectOriginalValues, commandQueue);
                }
            }
        }

        private void ProcessObjectForUpdate(object objectToUpdate, Type objectType, object originalValues, Queue<SqlCommand> commandQueue)
        {
            if (PersistenceHelper.ArePersistentPropertiesEqual(objectToUpdate, originalValues) == false)
            {
                PersistentClass persistentClassAttribute = (PersistentClass)objectType.GetCustomAttributes(typeof(PersistentClass), false).Single();
                
                PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
                PropertyBridge keyPropertyBridge = PropertyBridgeFactory.GetPropertyBridge(keyProperty, objectToUpdate);

                if (persistentClassAttribute.HasPersistentBaseClass == true)
                {
                    Type rootType = PersistenceHelper.GetRootType(objectToUpdate);
                    List<PropertyBridge> basePropertyBridgeList = this.GetPropertyBridgeList(rootType, objectToUpdate, originalValues);
                    if (basePropertyBridgeList.Count != 0)
                    {
                        commandQueue.Enqueue(this.CreateSqlCommand(keyPropertyBridge, basePropertyBridgeList, persistentClassAttribute.BaseStorageName));
                    }
                }

                if (persistentClassAttribute.IsPersisted == true)
                {
                    List<PropertyBridge> propertyBridgeList = this.GetPropertyBridgeList(objectType, objectToUpdate, originalValues);
                    if (propertyBridgeList.Count != 0)
                    {
                        commandQueue.Enqueue(this.CreateSqlCommand(keyPropertyBridge, propertyBridgeList, persistentClassAttribute.StorageName));
                    }
                }
            }
        }

        private SqlCommand CreateSqlCommand(PropertyBridge keyPropertyBridge, List<PropertyBridge> propertyBridgeList, string storageName)
        {
            SqlCommand cmd = new SqlCommand();

            keyPropertyBridge.SetSqlParameter(cmd);
            StringBuilder sqlStatement = new StringBuilder("Update " + storageName + " Set ");

            for (int i = 0; i < propertyBridgeList.Count; i++)
            {
                sqlStatement.Append("[" + propertyBridgeList[i].Name + "] = " + propertyBridgeList[i].AtName);
                if (i != propertyBridgeList.Count - 1)
                {
                    sqlStatement.Append(", ");
                }
                propertyBridgeList[i].SetSqlParameter(cmd);
            }
            
            sqlStatement.Append(" Where " + keyPropertyBridge.Name + " = " + keyPropertyBridge.AtName);
            cmd.CommandText = sqlStatement.ToString();            

            return cmd;            
        }        

        private List<PropertyBridge> GetPropertyBridgeList(Type type, object objectToUpdate, object originalValues)
        {
            List<PropertyBridge> propertyBridgeList = new List<PropertyBridge>();
            List<PropertyInfo> properties = PersistenceHelper.GetPropertiesToHandle(type);            

            foreach (PropertyInfo property in properties)
            {                
                var originalValue = property.GetValue(originalValues, null);
                var currentValue = property.GetValue(objectToUpdate, null);

                if (currentValue == null && originalValue != null)
                {
                    PropertyBridge propertyBridge = PropertyBridgeFactory.GetPropertyBridge(property, objectToUpdate);
                    propertyBridgeList.Add(propertyBridge);
                }
                if (currentValue != null && originalValue == null)
                {
                    PropertyBridge propertyBridge = PropertyBridgeFactory.GetPropertyBridge(property, objectToUpdate);
                    propertyBridgeList.Add(propertyBridge);
                }
                else if (currentValue != null && originalValue != null)
                {
                    if (currentValue.Equals(originalValue) == false)
                    {
                        PropertyBridge propertyBridge = PropertyBridgeFactory.GetPropertyBridge(property, objectToUpdate);
                        propertyBridgeList.Add(propertyBridge);
                    }
                }                
            }

            return propertyBridgeList;
        }        
    }
}
