using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace YellowstonePathology.Business.Persistence
{
    public class ObjectPropertyWriter
    {
        object m_ObjectToWriteTo;
        object m_ObjectToReadFrom;
        Type m_ObjectType;

        public ObjectPropertyWriter(object objectToWriteTo, object objectToReadFrom)
        {
            this.m_ObjectToWriteTo = objectToWriteTo;
            this.m_ObjectToReadFrom = objectToReadFrom;
            this.m_ObjectType = this.m_ObjectToReadFrom.GetType();
        }

        public void WriteProperties()
        {
            PropertyInfo[] properties = this.m_ObjectType.GetProperties().
                Where(prop => Attribute.IsDefined(prop, typeof(PersistentProperty)) || Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).ToArray();
            foreach (PropertyInfo property in properties)
            {
                Type dataType = property.PropertyType;
                if (dataType == typeof(string))
                {
                    this.WriteString(property);
                }
                else if (dataType == typeof(int))
                {
                    this.WriteInt(property);
                }
                else if (dataType == typeof(Nullable<int>))
                {
                    this.WriteNullableInt(property);
                }
                else if (dataType == typeof(DateTime))
                {
                    this.WriteDateTime(property);
                }
                else if (dataType == typeof(bool))
                {
                    this.WriteBoolean(property);
                }
                else if (dataType == typeof(Nullable<bool>))
                {
                    this.WriteNullableBoolean(property);
                }
                else if (dataType == typeof(Nullable<DateTime>))
                {
                    this.WriteNullableDateTime(property);
                }
                else
                {
                    throw new Exception("This Data Type is Not Implemented: " + dataType.Name);
                }
            }
        }

        private void WriteString(PropertyInfo property)
        {
            string objValue = (string)property.GetValue(this.m_ObjectToReadFrom, null);
            property.SetValue(this.m_ObjectToWriteTo, objValue, null);            
        }

        private void WriteInt(PropertyInfo property)
        {
            int objValue = (int)property.GetValue(this.m_ObjectToReadFrom, null);
            property.SetValue(this.m_ObjectToWriteTo, objValue, null);            
        }

        private void WriteNullableInt(PropertyInfo property)
        {            
            object objValue = property.GetValue(this.m_ObjectToReadFrom, null);
            Nullable<int> result = null;
            if (objValue != null)
            {
                result = Convert.ToInt32(property.GetValue(this.m_ObjectToReadFrom, null));
                property.SetValue(this.m_ObjectToWriteTo, result, null);
            }            
        }

        private void WriteDateTime(PropertyInfo property)
        {
            DateTime objValue = (DateTime)property.GetValue(this.m_ObjectToReadFrom, null);
            property.SetValue(this.m_ObjectToWriteTo, objValue, null);  
        }

        private void WriteBoolean(PropertyInfo property)
        {
            bool objValue = (bool)property.GetValue(this.m_ObjectToReadFrom, null);
            property.SetValue(this.m_ObjectToWriteTo, objValue, null);  
        }

        private void WriteNullableBoolean(PropertyInfo property)
        {
            object objValue = property.GetValue(this.m_ObjectToReadFrom, null);
            Nullable<bool> result = null;
            if (objValue != null)
            {
                result = Convert.ToBoolean(property.GetValue(this.m_ObjectToReadFrom, null));
                property.SetValue(this.m_ObjectToWriteTo, result, null);
            }   
        }

        private void WriteNullableDateTime(PropertyInfo property)
        {
            object objValue = property.GetValue(this.m_ObjectToReadFrom, null);
            Nullable<DateTime> result = null;
            if (objValue != null)
            {
                result = Convert.ToDateTime(property.GetValue(this.m_ObjectToReadFrom, null));
                property.SetValue(this.m_ObjectToWriteTo, result, null);
            }   
        }
    }
}
