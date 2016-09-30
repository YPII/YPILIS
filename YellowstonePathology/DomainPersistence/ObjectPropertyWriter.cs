using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;

namespace YellowstonePathology.Business.Domain.Persistence
{
    public class ObjectPropertyWriter : IPropertyWriter
    {
        private Object m_Object;

        public ObjectPropertyWriter(Object o)
        {
            this.m_Object = o;
        }

        public string WriteString(string propertyName)
        {			
            PropertyInfo propertyInfo = this.m_Object.GetType().GetProperty(propertyName);
            return propertyInfo.GetValue(this.m_Object, null) as string;			
        }

        public int WriteInt(string propertyName)
        {
            PropertyInfo propertyInfo = this.m_Object.GetType().GetProperty(propertyName);
            return Convert.ToInt32(propertyInfo.GetValue(this.m_Object, null));
		}

		public Nullable<int> WriteNullableInt(string propertyName)
		{
            PropertyInfo propertyInfo = this.m_Object.GetType().GetProperty(propertyName);
			object data = propertyInfo.GetValue(this.m_Object, null);
			Nullable<int> result = null;
			if (data != null)
			{
				result = Convert.ToInt32(propertyInfo.GetValue(this.m_Object, null));
			}
			return result;
		}

        public bool WriteBoolean(string propertyName)
		{
            PropertyInfo propertyInfo = this.m_Object.GetType().GetProperty(propertyName);
            return Convert.ToBoolean(propertyInfo.GetValue(this.m_Object, null));
		}

		public Nullable<bool> WriteNullableBoolean(string propertyName)
		{
			PropertyInfo propertyInfo = this.m_Object.GetType().GetProperty(propertyName);
			object data = propertyInfo.GetValue(this.m_Object, null);
			Nullable<bool> result = null;
			if (data != null)
			{
				result = Convert.ToBoolean(propertyInfo.GetValue(this.m_Object, null));
			}
			return result;
		}

        public DateTime WriteDateTime(string propertyName)
        {
            PropertyInfo propertyInfo = this.m_Object.GetType().GetProperty(propertyName);
            return Convert.ToDateTime(propertyInfo.GetValue(this.m_Object, null));
		}

        public Nullable<DateTime> WriteNullableDateTime(string propertyName)
        {
            PropertyInfo propertyInfo = this.m_Object.GetType().GetProperty(propertyName);
			object data = propertyInfo.GetValue(this.m_Object, null);
			Nullable<DateTime> result = null;
			if (data != null)
			{
				result = Convert.ToDateTime(propertyInfo.GetValue(this.m_Object, null));
			}
			return result;
		}

        public XElement WriteXElement(string propertyName)
        {
            return null;
		}

		public double WriteFloat(string propertyName)
		{
            PropertyInfo propertyInfo = this.m_Object.GetType().GetProperty(propertyName);
            return Convert.ToDouble(propertyInfo.GetValue(this.m_Object, null));
		}

		public Nullable<double> WriteNullableFloat(string propertyName)
		{
            PropertyInfo propertyInfo = this.m_Object.GetType().GetProperty(propertyName);
			object data = propertyInfo.GetValue(this.m_Object, null);
			Nullable<double> result = null;
			if (data != null)
			{
				result = Convert.ToDouble(propertyInfo.GetValue(this.m_Object, null));
			}
			return result;
		}
    }
}
