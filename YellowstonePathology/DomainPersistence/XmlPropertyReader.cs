using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace YellowstonePathology.Business.Domain.Persistence
{
    public class XmlPropertyReader : IPropertyReader
    {
        private string m_DateFormat;
        private XElement m_Document;                        

        public XmlPropertyReader()
        {                        
            this.m_DateFormat = "yyyy-MM-ddTHH:mm:ss.FFF";            
        }

        public void Initialize(string documentname)
        {            
            this.m_Document = new XElement(documentname);
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
            XElement resultElement;
            if (value != null)
            {
                resultElement = new XElement(propertyName, value);
                this.m_Document.Add(resultElement);
            }
			else
			{
                resultElement = new XElement(propertyName);
                this.m_Document.Add(resultElement);
			}			
		}

        public void ReadInt(string propertyName, int value)
        {            
            this.ReadString(propertyName, value.ToString());         
		}

        public void ReadBoolean(string propertyName, bool value)
        {            
            this.ReadString(propertyName, value.ToString().ToLower());            
		}

		public void ReadNullableBoolean(string propertyName, bool? value)
		{
			XElement resultElement;
			if (value.HasValue)
			{
				resultElement = new XElement(propertyName, value.ToString().ToLower());
			}
			else
			{
				resultElement = new XElement(propertyName);
			}
			this.Document.Add(resultElement);
		}

		public void ReadNullableInt(string propertyName, int? value)
		{
            XElement resultElement;
            if (value.HasValue)
            {
                resultElement = new XElement(propertyName, value.ToString());
            }
            else
            {
                resultElement = new XElement(propertyName);
            }
            this.Document.Add(resultElement);
		}

        public void ReadDateTime(string propertyName, DateTime value)
        {            
            this.ReadString(propertyName, value.ToString(this.m_DateFormat));            
		}        

        public void ReadNullableDateTime(string propertyName, DateTime? value)
        {
            XElement resultElement;
            if (value.HasValue)
            {
                resultElement = new XElement(propertyName, value.ToString());
            }
            else
            {
                resultElement = new XElement(propertyName);
            }
            this.Document.Add(resultElement);        
		}

		public void ReadDouble(string propertyName, double value)
		{            
            this.ReadString(propertyName, value.ToString());            
		}

		public void ReadNullableDouble(string propertyName, double? value)
		{
            XElement resultElement;
            if (value.HasValue)
            {
                resultElement = new XElement(propertyName, value.ToString());
            }
            else
            {
                resultElement = new XElement(propertyName);
            }
            this.Document.Add(resultElement);           
		}

        public void ReadXElement(string propertyName, XElement value)
        {            
            if (value != null)
            {
                XElement resultElement = new XElement(propertyName);
                resultElement.Add(value);
                this.m_Document.Add(resultElement);
            }
            else
            {
                this.Document.Add(propertyName);
            }            
		}        		
    }
}
