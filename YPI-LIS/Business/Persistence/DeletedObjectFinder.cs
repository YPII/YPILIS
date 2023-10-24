using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace YellowstonePathology.Business.Persistence
{
    public class DeletedObjectFinder
    {
        private Queue<object> m_DeletedObjects;

        public DeletedObjectFinder()
        {
            this.m_DeletedObjects = new Queue<object>();
        }

        public Queue<object> DeletedObjects
        {
            get { return this.m_DeletedObjects; }
        }

        public void Run(object original, object current)
        {
            this.HandleChildCollections(original, current);         
        }        

        private void HandleChildCollections(object original, object current)
        {
            if (original != null)
            {
                Type objectType = original.GetType();

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

                    for (int i = 0; i < originalChildCollectionObject.Count; i++)
                    {
                        object originalItemObject = originalChildCollectionObject[i];
                        Type itemObjectType = originalItemObject.GetType();
                        PropertyInfo originalItemObjectKeyProperty = itemObjectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
                        object originalItemObjectKeyValue = originalItemObjectKeyProperty.GetValue(originalItemObject, null);

                        object currentItemObject = this.FindCurrentObjectInCollection(currentChildCollectionObject, originalItemObjectKeyValue);
                        if (currentItemObject == null)
                        {
                            this.m_DeletedObjects.Enqueue(originalItemObject);
                        }
                        else
                        {
                            this.HandleChildCollections(originalItemObject, currentItemObject);
                            this.HandleChildObjects(originalItemObject, currentItemObject);
                        }
                    }
                }
            }
        }

        private void HandleChildObjects(object original, object current)
        {
            Type objectType = current.GetType();
            List<PropertyInfo> childCollectionProperties = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentChild))).ToList();
            foreach (PropertyInfo propertyInfo in childCollectionProperties)
            {
                object originalChildObject = propertyInfo.GetValue(original, null);
                object currentChildObject = propertyInfo.GetValue(current, null);
                this.HandleChildCollections(originalChildObject, currentChildObject);
            }
        }

        private object FindCurrentObjectInCollection(IList currentChildCollectionObject, object originalItemObjectKeyValue)
        {            
            for (int j = 0; j < currentChildCollectionObject.Count; j++)
            {
                object currentItemObject = currentChildCollectionObject[j];
                Type currentItemObjectType = currentItemObject.GetType();
                PropertyInfo currentItemObjectKeyProperty = currentItemObjectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
                object currentItemObjectKeyValue = currentItemObjectKeyProperty.GetValue(currentItemObject, null);

                if (currentItemObjectKeyValue.Equals(originalItemObjectKeyValue) == true)
                {
                    return currentItemObject;
                }
            }
            return null;
        }
    }
}
