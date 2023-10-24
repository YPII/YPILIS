using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;

namespace YellowstonePathology.Business.Persistence
{
    public class XmlPropertyWriter
    {
        object m_ObjectToWriteTo;
        Type m_ObjectType;
        XElement m_Document;
        //private string m_DateFormat = "yyyy-MM-ddTHH:mm:ss.FFF";

        public XmlPropertyWriter(XElement documentToReadFrom, object objectToWriteTo)
        {
			this.m_ObjectToWriteTo = objectToWriteTo;
            this.m_Document = documentToReadFrom;
			this.m_ObjectType = objectToWriteTo.GetType();
        }

        public void Write()
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
                    this.ReadNullableInt(property);
                }
                else if (dataType == typeof(DateTime))
                {
                    this.ReadDateTime(property);
                }
                else if (dataType == typeof(bool))
                {
                    this.ReadBoolean(property);
                }
                else if (dataType == typeof(Nullable<bool>))
                {
                    this.ReadNullableBoolean(property);
                }
                else if (dataType == typeof(Nullable<DateTime>))
                {
                    this.ReadNullableDateTime(property);
                }
				else if (dataType == typeof(Double))
				{
					this.ReadDouble(property);
				}
				else if (dataType == typeof(Nullable<Double>))
				{
					this.ReadDouble(property);
				}
				else
				{
					throw new Exception("This Data Type is Not Implemented: " + dataType.Name);
				}
            }        
        }

        private void WriteString(PropertyInfo property)
        {
            XElement stringElement = this.m_Document.Element(property.Name);
            if (stringElement != null)
            {
                property.SetValue(this.m_ObjectToWriteTo, stringElement.Value, null);
            }
        }

        private void WriteInt(PropertyInfo property)
        {
            XElement intElement = this.m_Document.Element(property.Name);
            if (intElement != null)
            {
                int value = Convert.ToInt32(intElement.Value);
                property.SetValue(this.m_ObjectToWriteTo, value, null);
            }
        }

        public void ReadBoolean(PropertyInfo property)
        {
            XElement booleanElement = this.m_Document.Element(property.Name);
            if (booleanElement != null)
            {
                int value = Convert.ToInt32(booleanElement.Value);
                if (value == 1)
                {
                    property.SetValue(this.m_ObjectToWriteTo, true, null);
                }
                else
                {
                    property.SetValue(this.m_ObjectToWriteTo, false, null);
                }                
            }
        }

        public void ReadNullableBoolean(PropertyInfo property)
        {
            throw new NotImplementedException();
        }

        public void ReadNullableInt(PropertyInfo property)
        {
            XElement nullableIntElement = this.m_Document.Element(property.Name);
            Nullable<int> value = new Nullable<int>();
            if (nullableIntElement != null)
            {
                value = Convert.ToInt32(nullableIntElement.Value);                
            }
            property.SetValue(this.m_ObjectToWriteTo, value, null);
        }

        public void ReadDateTime(PropertyInfo property)
        {
            XElement dateTimeElement = this.m_Document.Element(property.Name);
            if (dateTimeElement != null)
            {
                DateTime value = DateTime.Parse(dateTimeElement.Value);
                property.SetValue(this.m_ObjectToWriteTo, value, null);
            }
        }

        public void ReadNullableDateTime(PropertyInfo property)
        {
            XElement dateTimeElement = this.m_Document.Element(property.Name);
            if (dateTimeElement != null)
            {
                Nullable<DateTime> value = DateTime.Parse(dateTimeElement.Value);
                property.SetValue(this.m_ObjectToWriteTo, value, null);
            }
        }

        public void ReadDouble(PropertyInfo property)
        {
			double value = 0;
			XElement resultElement = this.m_Document.Element(property.Name);
			if (resultElement != null)
			{
				value = Convert.ToDouble(this.m_Document.Element(property.Name).Value);
				property.SetValue(this.m_ObjectToWriteTo, value, null);
			}
		}

        public void ReadNullableDouble(PropertyInfo property)
        {
			Nullable<double> value = null;
			XElement propertyElement = this.m_Document.Element(property.Name);
			if (propertyElement != null)
			{
				value = Convert.ToDouble(this.m_Document.Element(property.Name).Value);
				property.SetValue(this.m_ObjectToWriteTo, value, null);
			}
		}        		
    }
}
