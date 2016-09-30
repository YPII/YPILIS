using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace YellowstonePathology.Business.Domain.Persistence
{
    public class SqlXmlPropertyReader : IPropertyReader
    {
        private string m_DateFormat;
        private XElement m_Document;
        private YellowstonePathology.Business.Domain.Persistence.PropertyReaderFilter m_Filter;
        private bool m_UseFilter;
        private Type m_Type;

        public SqlXmlPropertyReader()
        {            
            this.m_UseFilter = false;
            this.m_DateFormat = "yyyy-MM-ddTHH:mm:ss.FFF";            
        }

        public void Initialize(Type type)
        {
            this.m_Type = type;            
            this.m_Document = new XElement(this.m_Type.Name);
		}

        public void SetFilter(YellowstonePathology.Business.Domain.Persistence.PropertyReaderFilter filter)
        {
            this.m_Filter = filter;
            this.m_UseFilter = true;
        }

        public string DateFormat
        {
            get { return this.m_DateFormat; }
            set { this.m_DateFormat = value; }
        }

		public XElement Document
		{
			get { return this.m_Document; }
		}


        public void ReadString(string propertyName, string value)
        {
            if (this.IsPropertyFiltered(propertyName) == false)
            {
                if (value != null)
                {
                    XElement resultElement = new XElement(propertyName, value);
					this.m_Document.Add(resultElement);
                }
				else
				{
                    if (SqlXmlPropertyReader.IsElementNullable(propertyName, this.m_Type))
					{
						this.SetNullElement(propertyName);
					}
				}
			}
		} 

        public void ReadInt(string propertyName, int value)
        {
            if (this.IsPropertyFiltered(propertyName) == false)
            {
                this.ReadString(propertyName, value.ToString());
            }
		}

        public void ReadBoolean(string propertyName, bool value)
        {
            if (this.IsPropertyFiltered(propertyName) == false)
            {
                this.ReadString(propertyName, value.ToString().ToLower());
            }
		}

		public void ReadNullableBoolean(string propertyName, bool? value)
		{
			if (this.IsPropertyFiltered(propertyName) == false)
			{
				if (value.HasValue)
				{
					this.ReadBoolean(propertyName, value.Value);
				}
				else
				{
					this.SetNullElement(propertyName);
				}
			}
		}

		public void ReadNullableInt(string propertyName, int? value)
		{
            if (this.IsPropertyFiltered(propertyName) == false)
            {
                if (value.HasValue)
                {
                    this.ReadInt(propertyName, value.Value);
                }
                else
                {
                    this.SetNullElement(propertyName);
                }
            }
		}

        public void ReadDateTime(string propertyName, DateTime value)
        {
            if (this.IsPropertyFiltered(propertyName) == false)
            {
                this.ReadString(propertyName, value.ToString(this.m_DateFormat));
            }
		}        

        public void ReadNullableDateTime(string propertyName, DateTime? value)
        {
            if (this.IsPropertyFiltered(propertyName) == false)
            {
                if (value.HasValue)
                {
                    this.ReadDateTime(propertyName, value.Value);
                }
                else
                {
                    this.SetNullElement(propertyName);
                }
            }
		}

		public void ReadDouble(string propertyName, double value)
		{
            if (this.IsPropertyFiltered(propertyName) == false)
            {
                this.ReadString(propertyName, value.ToString());
            }
		}

		public void ReadNullableDouble(string propertyName, double? value)
		{
            if (this.IsPropertyFiltered(propertyName) == false)
            {
                if (value.HasValue)
                {
                    this.ReadDouble(propertyName, value.Value);
                }
                else
                {
                    this.SetNullElement(propertyName);
                }
            }
		}

        public void ReadXElement(string propertyName, XElement value)
        {
            if (this.IsPropertyFiltered(propertyName) == false)
            {
                if (value != null)
                {
                    XElement resultElement = new XElement(propertyName);
                    resultElement.Add(value);
                    this.m_Document.Add(resultElement);
                }
                else
                {
                    this.SetNullElement(propertyName);
                }
            }
		}

        private bool IsPropertyFiltered(string propertyName)
        {
            bool result = false;
            if (this.m_UseFilter == true)
            {
                if (this.m_Filter.OKToRead(propertyName) == false)
                {
                    result = true;
                }
            }
            return result;
        }

		private void SetNullElement(string propertyName)
		{		
			XElement resultElement = new XElement(propertyName);
			XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
			XAttribute nil = new XAttribute(xsi + "nil", true);
			resultElement.ReplaceAll(nil);
			this.m_Document.Add(resultElement);		
		}

		public static bool IsElementNullable(string elementName, Type type)
		{
			bool result = false;
			XDocument schema = YellowstonePathology.Business.Domain.Persistence.Schema.SchemaDocumentFactory.GetSchemaDocument(type);
			List<XElement> elements = schema.Root.Elements("{http://www.w3.org/2001/XMLSchema}complexType").ToList<XElement>();
			foreach (XElement itemElement in elements)
			{
				XAttribute itemAttribute = itemElement.Attribute("name");
				if (itemAttribute.Value.ToString() == type.Name + "Type")
				{
					List<XElement> fieldElements = itemElement.Element("{http://www.w3.org/2001/XMLSchema}sequence").Elements("{http://www.w3.org/2001/XMLSchema}element").ToList<XElement>();
					foreach (XElement element in fieldElements)
					{
						XAttribute nameAttribute = element.Attribute("name");
						if (nameAttribute.Value.ToString() == elementName)
						{
							XAttribute nilAttribute = element.Attribute("nillable");
							if (nilAttribute != null)
							{
								result = true;
							}
							break;
						}
					}
				}
			}
			return result;
		}
    }
}
