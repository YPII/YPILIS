using System;
using System.Reflection;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class PropertyBridge
    {
        protected PropertyInfo m_Property;
        protected object m_Object;        
        protected string m_Name;
        protected string m_AtName;
        protected MySqlParameter m_SqlParameter;
        protected object m_SqlValue;
        protected object m_PropertyValue;

        public PropertyBridge(PropertyInfo property, object parentObject)
        {
            this.m_Property = property;
            this.m_Object = parentObject;
            this.m_Name = this.m_Property.Name;
            this.m_AtName = "@" + this.m_Property.Name;            
        }        

        public virtual void SetSqlParameter(MySqlCommand cmd)
        {
            this.m_SqlParameter = new MySqlParameter();
            this.m_SqlParameter.ParameterName = this.m_AtName;
            this.m_SqlParameter.Direction = ParameterDirection.Input;

            var propertyValue = this.m_Property.GetValue(this.m_Object, null);
            if (propertyValue != null)
            {
                this.m_SqlParameter.Value = propertyValue;                
            }
            else
            {
                this.m_SqlParameter.Value = DBNull.Value;                
            }
            cmd.Parameters.Add(this.m_SqlParameter);
        }

        public virtual void SetSqlValue(object sqlValue)
        {
            this.m_SqlValue = sqlValue;
        }

        public virtual object GetPropertyValue()
        {
            return this.m_PropertyValue;
        }

        public string Name
        {
            get { return this.m_Name; }
        }

        public string AtName
        {
            get { return this.m_AtName; }
        }        
    }
}
