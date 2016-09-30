using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Reflection;

namespace YellowstonePathology.Business.Persistence
{
    public class RegisteredCollection
    {
        private object m_Collection;
        private object m_Clone;

        public RegisteredCollection(object collection, object clone)
        {
            this.m_Collection = collection;
            this.m_Clone = clone;
        }

        public object Collection
        {
            get { return this.m_Collection; }
        }

        public object Clone
        {
            get { return this.m_Clone; }
        }

        public List<object> GetDeletedObjects()
        {
            List<object> result = new List<object>();

			int collectionCount = (int)this.m_Collection.GetType().GetProperty("Count").GetValue(this.m_Collection, null);
			int cloneCount = (int)this.m_Clone.GetType().GetProperty("Count").GetValue(this.m_Clone, null);
			for (int i = 0; i < cloneCount; i++)
			{
				bool matched = false;
				object[] cloneIndex = { i };
				object cloneObject = this.m_Clone.GetType().GetProperty("Item").GetValue(this.m_Clone, cloneIndex);
				PropertyInfo keyProperty = cloneObject.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
				object cloneKeyPropertyValue = keyProperty.GetValue(cloneObject, null);

				for (int idx = 0; idx < collectionCount; idx++)
				{
					object[] collectionIndex = { idx };
					object collectionObject = this.m_Collection.GetType().GetProperty("Item").GetValue(this.m_Collection, collectionIndex);
					object keyPropertyValue = keyProperty.GetValue(collectionObject, null);

					if (keyPropertyValue == cloneKeyPropertyValue)
					{
						matched = true;
						break;
					}
				}
				if (matched == false) result.Add(cloneObject);
			}

			return result;
        }

        public List<object> GetInsertedObjects()
        {
            List<object> result = new List<object>();

			int collectionCount = (int)this.m_Collection.GetType().GetProperty("Count").GetValue(this.m_Collection, null);
			int cloneCount = (int)this.m_Clone.GetType().GetProperty("Count").GetValue(this.m_Clone, null);
			for (int idx = 0; idx < collectionCount; idx++)
			{
				bool matched = false;
				object[] collectionIndex = { idx };
				object collectionObject = this.m_Collection.GetType().GetProperty("Item").GetValue(this.m_Collection, collectionIndex);
				Type objectType = collectionObject.GetType();
				PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
				object keyPropertyValue = keyProperty.GetValue(collectionObject, null);

				for (int i = 0; i < cloneCount; i++)
				{
					object[] cloneIndex = { i };
					object cloneObject = this.m_Clone.GetType().GetProperty("Item").GetValue(this.m_Clone, cloneIndex);
					object cloneKeyPropertyValue = keyProperty.GetValue(cloneObject, null);

					if (keyPropertyValue == cloneKeyPropertyValue)
					{
						matched = true;
						break;
					}
				}
				if (matched == false) result.Add(collectionObject);
			}

			return result;
        }
    }
}
