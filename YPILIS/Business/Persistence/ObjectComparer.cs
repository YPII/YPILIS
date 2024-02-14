using System;
using System.Collections.Generic;

namespace YellowstonePathology.Business.Persistence
{
	public class ObjectComparer
	{
		private bool m_Result;
		private List<object> m_ChangedObjects;
		private List<object> m_DeletedObjects;
		private List<object> m_InsertedObjects;
		
		private object m_FirstObject;
		private object m_SecondObject;
		
		public ObjectComparer(object firstObject, object secondObject)
		{
			this.m_FirstObject = firstObject;
			this.m_SecondObject = secondObject;
				
			this.m_Result = false;
			
			this.m_ChangedObjects = new List<object>();
			this.m_DeletedObjects = new List<object>();
			this.m_InsertedObjects = new List<object>();
		}
		
		public void Compare()
		{
			this.recursiveCompare(this.m_FirstObject, this.m_SecondObject);
		}
		
		private void recursiveCompare(object firstObject, object secondObject)
		{
			this.m_Result = Persistence.PersistenceHelper.ArePersistentPropertiesEqual(firstObject, secondObject);
		}
		
		public bool Result
		{
			get { return this.m_Result;}
		}
	}
}
