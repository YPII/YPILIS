using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace YellowstonePathology.Business.Persistence
{
    public class ObjectCloner
    {
        public ObjectCloner()
        {

        }

        public object Clone(object objectToClone)
        {            
            Type objectType = objectToClone.GetType();
            object clonedObject = this.CloneThisObject(objectToClone);
            this.HandlePersistentChildCollections(objectToClone, clonedObject);
            return clonedObject;
        }

        private void HandlePersistentChildCollections(object parentObject, object clonedParent)
        {
            if (parentObject != null)
            {
                Type parentObjectType = parentObject.GetType();

                List<PropertyInfo> childCollectionProperties = parentObjectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentCollection))).ToList();
                foreach (PropertyInfo propertyInfo in childCollectionProperties)
                {
                    IList childCollectionObject = (IList)propertyInfo.GetValue(parentObject, null);                    
                    IList clonedCollection = (IList)Activator.CreateInstance(childCollectionObject.GetType());

                    for (int i = 0; i < childCollectionObject.Count; i++)
                    {
                        object clonedCollectionItem = this.CloneThisObject(childCollectionObject[i]);
                        clonedCollection.Add(clonedCollectionItem);
                        object collectionItem = childCollectionObject[i];

                        this.HandlePersistentChildCollections(collectionItem, clonedCollectionItem);
                        this.HandlePersistentChildren(collectionItem, clonedCollectionItem);
                    }                   

                    propertyInfo.SetValue(clonedParent, clonedCollection, null);
                }
            }
        }

        private void HandlePersistentChildren(object parentObject, object parentObjectClone)
        {
            if (parentObject != null)
            {
                Type parentObjectType = parentObject.GetType();
                List<PropertyInfo> childProperties = parentObjectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentChild))).ToList();
                foreach (PropertyInfo childPropertyInfo in childProperties)
                {
                    object childObject = childPropertyInfo.GetValue(parentObject, null);
                    object clonedObject = this.CloneThisObject(childObject);

                    childPropertyInfo.SetValue(parentObjectClone, clonedObject, null);
                    this.HandlePersistentChildCollections(childObject, clonedObject);
                    this.HandlePersistentChildren(childObject, clonedObject);
                }
            }
        }

        private object CloneThisObject(object objectToClone)
        {
            if (objectToClone == null) return null;            

            object clone = Activator.CreateInstance(objectToClone.GetType());

            PropertyInfo keyPersistentProperty = objectToClone.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            object keyPersistentPropertyValue = keyPersistentProperty.GetValue(objectToClone, null);
            keyPersistentProperty.SetValue(clone, keyPersistentPropertyValue, null);                

            PropertyInfo[] persistentProperties = objectToClone.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentProperty))).ToArray();
            foreach (PropertyInfo persistentProperty in persistentProperties)
            {
                object persistentPropertyValue = persistentProperty.GetValue(objectToClone, null);
                persistentProperty.SetValue(clone, persistentPropertyValue, null);                
            }

            return clone;
        }
    }
}
