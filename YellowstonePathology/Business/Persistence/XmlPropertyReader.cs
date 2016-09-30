using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;

namespace YellowstonePathology.Business.Persistence
{
    public class XmlPropertyReader
    {
        object m_ObjectToReadFrom;
        Type m_ObjectType;
        XElement m_Document;
        private string m_DateFormat;

		public XmlPropertyReader(object objectToReadFrom, XElement document)
        {
			this.m_ObjectToReadFrom = objectToReadFrom;
            this.m_Document = document;
			this.m_ObjectType = objectToReadFrom.GetType();
            this.m_DateFormat = "yyyy-MM-ddTHH:mm:ss.FFF";    
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
                    this.ReadString(property);
                }
                else if (dataType == typeof(int))
                {
                    this.ReadInt(property);
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
                else
                {
                    throw new Exception("This Data Type is Not Implemented: " + dataType.Name);
                }
            }        
        }

        public void ReadString(PropertyInfo property)
        {
            XElement resultElement;
			string value = Convert.ToString(property.GetValue(this.m_ObjectToReadFrom, null));
            if (value != null)
            {
                resultElement = new XElement(property.Name, value);
                this.m_Document.Add(resultElement);
            }
            else
            {
                resultElement = new XElement(property.Name);
                this.m_Document.Add(resultElement);
            }
        }

        public void ReadInt(PropertyInfo property)
        {
            XElement resultElement;
			string value = Convert.ToString(property.GetValue(this.m_ObjectToReadFrom, null));
            if (value != null)
            {
                resultElement = new XElement(property.Name, value);
                this.m_Document.Add(resultElement);
            }
            else
            {
                resultElement = new XElement(property.Name);
                this.m_Document.Add(resultElement);
            }
        }

        public void ReadBoolean(PropertyInfo property)
        {
            XElement resultElement;
			string value = Convert.ToString(property.GetValue(this.m_ObjectToReadFrom, null));
            if (value != null)
            {
                resultElement = new XElement(property.Name, value);
                this.m_Document.Add(resultElement);
            }
            else
            {
                resultElement = new XElement(property.Name);
                this.m_Document.Add(resultElement);
            }
        }

        public void ReadNullableBoolean(PropertyInfo property)
        {            
            XElement resultElement;
			object value = property.GetValue(this.m_ObjectToReadFrom, null);                        
            if (value != null)
            {
                resultElement = new XElement(property.Name, value.ToString().ToLower());
            }
            else
            {
                resultElement = new XElement(property.Name);
            }
            this.m_Document.Add(resultElement);         
        }

        public void ReadNullableInt(PropertyInfo property)
        {
            XElement resultElement;
			object value = property.GetValue(this.m_ObjectToReadFrom, null);
            if (value != null)
            {
                resultElement = new XElement(property.Name, value.ToString());
            }
            else
            {
                resultElement = new XElement(property.Name);
            }
            this.m_Document.Add(resultElement);  
        }

        public void ReadDateTime(PropertyInfo property)
        {
            XElement resultElement;
			object value = property.GetValue(this.m_ObjectToReadFrom, null);
            if (value != null)
            {
                resultElement = new XElement(property.Name, DateTime.Parse(value.ToString()).ToString(this.m_DateFormat));
            }
            else
            {
                resultElement = new XElement(property.Name);
            }
            this.m_Document.Add(resultElement);              
        }

        public void ReadNullableDateTime(PropertyInfo property)
        {
            XElement resultElement;
			object value = property.GetValue(this.m_ObjectToReadFrom, null);
            if (value != null)
            {
                resultElement = new XElement(property.Name, DateTime.Parse(value.ToString()).ToString(this.m_DateFormat));
            }
            else
            {
                resultElement = new XElement(property.Name);
            }
            this.m_Document.Add(resultElement);              
        }

        public void ReadDouble(PropertyInfo property)
        {
            XElement resultElement;
			string value = Convert.ToString(property.GetValue(this.m_ObjectToReadFrom, null));
            if (value != null)
            {
                resultElement = new XElement(property.Name, value);
                this.m_Document.Add(resultElement);
            }
            else
            {
                resultElement = new XElement(property.Name);
                this.m_Document.Add(resultElement);
            }
        }

        public void ReadNullableDouble(PropertyInfo property)
        {
            XElement resultElement;
			string value = Convert.ToString(property.GetValue(this.m_ObjectToReadFrom, null));
            if (value != null)
            {
                resultElement = new XElement(property.Name, value);
                this.m_Document.Add(resultElement);
            }
            else
            {
                resultElement = new XElement(property.Name);
                this.m_Document.Add(resultElement);
            }
        }        		
    }
}
