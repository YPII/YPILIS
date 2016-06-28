using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Reflection;

namespace YellowstonePathology.Business.Domain.Persistence
{
    public class ObjectPropertyReader : IPropertyReader
    {
        private object m_Object;

        public ObjectPropertyReader()
        {                        
            
        }

        public void Initialize(object o)
        {
            this.m_Object = o;
		}                		

        public void ReadString(string propertyName, string value)
        {
            PropertyInfo propertyInfo = this.m_Object.GetType().GetProperty(propertyName);
            propertyInfo.SetValue(this.m_Object, value, null);            
		}

        public void ReadInt(string propertyName, int value)
        {
            PropertyInfo propertyInfo = this.m_Object.GetType().GetProperty(propertyName);
            propertyInfo.SetValue(this.m_Object, value, null);            
		}

        public void ReadBoolean(string propertyName, bool value)
        {
            PropertyInfo propertyInfo = this.m_Object.GetType().GetProperty(propertyName);
            propertyInfo.SetValue(this.m_Object, value, null);            
		}

		public void ReadNullableBoolean(string propertyName, bool? value)
		{
            PropertyInfo propertyInfo = this.m_Object.GetType().GetProperty(propertyName);
            propertyInfo.SetValue(this.m_Object, value, null);            
		}

		public void ReadNullableInt(string propertyName, int? value)
		{
            PropertyInfo propertyInfo = this.m_Object.GetType().GetProperty(propertyName);
            propertyInfo.SetValue(this.m_Object, value, null);            
		}

        public void ReadDateTime(string propertyName, DateTime value)
        {
            PropertyInfo propertyInfo = this.m_Object.GetType().GetProperty(propertyName);
            propertyInfo.SetValue(this.m_Object, value, null);            
		}        

        public void ReadNullableDateTime(string propertyName, DateTime? value)
        {
            PropertyInfo propertyInfo = this.m_Object.GetType().GetProperty(propertyName);
            propertyInfo.SetValue(this.m_Object, value, null);            
		}

		public void ReadDouble(string propertyName, double value)
		{
            PropertyInfo propertyInfo = this.m_Object.GetType().GetProperty(propertyName);
            propertyInfo.SetValue(this.m_Object, value, null);            
		}

		public void ReadNullableDouble(string propertyName, double? value)
		{
            PropertyInfo propertyInfo = this.m_Object.GetType().GetProperty(propertyName);
            propertyInfo.SetValue(this.m_Object, value, null);            
		}

        public void ReadXElement(string propertyName, XElement value)
        {
            PropertyInfo propertyInfo = this.m_Object.GetType().GetProperty(propertyName);
            propertyInfo.SetValue(this.m_Object, value, null);            
		}        		
    }
}
