using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Domain.Persistence
{
    public class SqlXmlPropertyWriter : IPropertyWriter
    {
        XElement m_Document;

        public SqlXmlPropertyWriter(XElement document)
        {
            this.m_Document = document;
        }

        public string WriteString(string propertyName)
        {
			string result = null;
			XElement propertyElement = this.m_Document.Element(propertyName);
			if (propertyElement != null)
			{
				result = propertyElement.Value;
			}
			return result;
        }

        public int WriteInt(string propertyName)
        {
			int result = 0;
			XElement resultElement = this.m_Document.Element(propertyName);
			if (resultElement != null)
			{
				result = Convert.ToInt32(this.m_Document.Element(propertyName).Value);
			}
			return result;
		}

		public Nullable<int> WriteNullableInt(string propertyName)
		{
			Nullable<int> result = null;
			XElement propertyElement = this.m_Document.Element(propertyName);
			if (propertyElement != null)
			{
				result = Convert.ToInt32(this.m_Document.Element(propertyName).Value);
			}
			return result;
		}

        public bool WriteBoolean(string propertyName)
		{
			bool result = false;
			XElement resultElement = this.m_Document.Element(propertyName);
			if (resultElement != null)
			{
				result = Convert.ToBoolean(Convert.ToInt32(this.m_Document.Element(propertyName).Value));
			}
			return result;
		}

        public DateTime WriteDateTime(string propertyName)
        {
			DateTime result = DateTime.Parse(this.m_Document.Element(propertyName).Value);
			return result;
		}

        public Nullable<DateTime> WriteNullableDateTime(string propertyName)
        {
			Nullable<DateTime> result = null;
			XElement propertyElement = this.m_Document.Element(propertyName);
			if (propertyElement != null)
			{
				result = DateTime.Parse(propertyElement.Value);
			}
			return result;
		}

        public XElement WriteXElement(string propertyName)
        {
			XElement result = null;
			XElement propertyElement = this.m_Document.Element(propertyName);
			if (propertyElement != null)
			{
				result = propertyElement.Element("Document");
			}
			return result;
		}

		public double WriteFloat(string propertyName)
		{
			double result = 0;
			XElement resultElement = this.m_Document.Element(propertyName);
			if (resultElement != null)
			{
				result = Convert.ToDouble(this.m_Document.Element(propertyName).Value);
			}
			return result;
		}

		public Nullable<double> WriteNullableFloat(string propertyName)
		{
			Nullable<double> result = null;
			XElement propertyElement = this.m_Document.Element(propertyName);
			if (propertyElement != null)
			{
				result = Convert.ToDouble(this.m_Document.Element(propertyName).Value);
			}
			return result;
		}
    }
}
