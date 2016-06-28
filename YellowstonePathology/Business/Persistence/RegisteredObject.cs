using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace YellowstonePathology.Business.Persistence
{
    public class RegisteredObject
    {
        private object m_Key;
        private object m_Value;
        private List<object> m_RegisteredBy;

        public RegisteredObject(object value, object registeredBy)
        {
        	this.m_RegisteredBy = new List<object>();
            this.m_Value = value;
            this.m_RegisteredBy.Add(registeredBy);
            Type objectType = this.m_Value.GetType();
            PropertyInfo keyProperty = objectType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            this.m_Key = keyProperty.GetValue(this.m_Value, null);
        }        

        public object Key
        {
            get { return this.m_Key; }
        }

        public object Value
        {
            get { return this.m_Value; }
            set { this.m_Value = value; }
        }

        public List<object> RegisteredBy
        {
            get { return this.m_RegisteredBy; }
        }
        
        public Type ValueType
        {
        	get { return this.m_Value.GetType(); }
        }
    }
}
