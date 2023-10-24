using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace YellowstonePathology.Business.Persistence
{
    public class InsertedObjectFinder
    {
        private Queue<object> m_InsertedObjects;

        public InsertedObjectFinder()
        {
            this.m_InsertedObjects = new Queue<object>();
        }        

        public void Run(object original, object current)
        {
            this.HandleChildCollections(original, current);            
        }

        public Queue<object> InsertedObjects
        {
            get { return this.m_InsertedObjects; }
        }

        private void HandleChildObjects(object original, object current)
        {
            Type objectType = current.GetType();            

            List<PropertyInfo> childCollectionProperties = objectType.GetProperties()
                .Where(
                    prop => prop.GetCustomAttributes(typeof(PersistentCollection), true)
                        .Where(pc => ((PersistentCollection)pc).IsBuildOnly == false)
                        .Any()
                        )
                .ToList();

            foreach (PropertyInfo propertyInfo in childCollectionProperties)
            {
                object originalChildObject = propertyInfo.GetValue(original, null);
                object currentChildObject = propertyInfo.GetValue(current, null);
                this.HandleChildCollections(originalChildObject, currentChildObject);
            }
        }

        private void HandleChildCollections(object original, object current)
        {
            if (original == null && current == null) return;

            Type objectType = current.GetType();

            List<PropertyInfo> childCollectionProperties = objectType.GetProperties()
                .Where(
                    prop => prop.GetCustomAttributes(typeof(PersistentCollection), true)
                        .Where(pc => ((PersistentCollection)pc).IsBuildOnly == false)
                        .Any()
                        )
                .ToList();

            foreach (PropertyInfo propertyInfo in childCollectionProperties)
            {
                IList originalChildCollectionObject = (IList)propertyInfo.GetValue(original, null);
                IList currentChildCollectionObject = (IList)propertyInfo.GetValue(current, null);

                for (int i = 0; i < currentChildCollectionObject.Count; i++)
                {
                    object currentItemObject = currentChildCollectionObject[i];
                    Type itemObjectType = currentItemObject.GetType();
                    PropertyInfo currentItemObjectKeyProperty = itemObjectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
                    object currentItemObjectKeyValue = currentItemObjectKeyProperty.GetValue(currentItemObject, null);

                    object originalItemObject = this.FindOriginalObjectInCollection(originalChildCollectionObject, currentItemObjectKeyValue);
                    if (originalItemObject == null)
                    {
                        this.m_InsertedObjects.Enqueue(currentItemObject);
                    }
                    else
                    {
                        this.HandleChildCollections(originalItemObject, currentItemObject);
                        this.HandleChildObjects(originalItemObject, currentItemObject);
                    }
                }
            }
        }

        private object FindOriginalObjectInCollection(IList originalChildCollectionObject, object currentItemObjectKeyValue)
        {            
            for (int j = 0; j < originalChildCollectionObject.Count; j++)
            {
                object originalItemObject = originalChildCollectionObject[j];
                Type originalItemObjectType = originalItemObject.GetType();
                PropertyInfo originalItemObjectKeyProperty = originalItemObjectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
                object originalItemObjectKeyValue = originalItemObjectKeyProperty.GetValue(originalItemObject, null);

                if (originalItemObjectKeyValue.Equals(currentItemObjectKeyValue) == true)
                {
                    return originalItemObject;
                }
            }
            return null;
        }
    }
}
