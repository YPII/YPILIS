using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Reflection;

namespace YellowstonePathology.Business.Domain.Persistence
{
    public class SerializationHelper
    {
        public static Collection<XElement> SerializeToElements<T>(Collection<T> objectList)
        {
            Collection<XElement> elements = new Collection<XElement>();
            foreach (T o in objectList)
            {
                XElement element = XElement.Parse(SerializationHelper.SerializeObject<T>(o));
                elements.Add(element);
            }
            return elements;
        }

        public static Collection<XElement> SerializeToElements<T>(List<T> objectList)
        {
            Collection<XElement> elements = new Collection<XElement>();
            foreach (T o in objectList)
            {
                XElement element = XElement.Parse(SerializationHelper.SerializeObject<T>(o));
                elements.Add(element);
            }
            return elements;
        }

        public static String SerializeObject(Object pObject, Type type)
        {            
            String XmlizedString = null;
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(type);
            System.Xml.XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter(memoryStream, Encoding.UTF8);            
            xs.Serialize(xmlTextWriter, pObject);
            memoryStream = (System.IO.MemoryStream)xmlTextWriter.BaseStream;
            XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
            return XmlizedString.Substring(1, XmlizedString.Length - 1);            
        }

        public static String SerializeObject<T>(T o)
        {
            String XmlizedString = null;
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
            System.Xml.XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter(memoryStream, Encoding.UTF8);
            xs.Serialize(xmlTextWriter, o);
            memoryStream = (System.IO.MemoryStream)xmlTextWriter.BaseStream;
            XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
            return XmlizedString.Substring(1, XmlizedString.Length - 1);
        }

        public static XElement SerializeToElement<T>(T o)
        {
            String XmlizedString = null;
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(o.GetType());
            System.Xml.XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter(memoryStream, Encoding.UTF8);
            xs.Serialize(xmlTextWriter, o);
            memoryStream = (System.IO.MemoryStream)xmlTextWriter.BaseStream;
            XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
            return XElement.Parse(XmlizedString.Substring(1, XmlizedString.Length - 1));
        }

        public static T DeserializeCollection<T>(String xmlString) 
        {            
			if (!string.IsNullOrEmpty(xmlString))
			{
				System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
				System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(StringToUTF8ByteArray(xmlString));
				System.Xml.XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter(memoryStream, Encoding.UTF8);
				T result = (T)xs.Deserialize(memoryStream);				
				return result;
			}
			return default(T);
        }

        public static T DeserializeItem<T>(String xmlString) 
        {            
			System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
			System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(StringToUTF8ByteArray(xmlString));
			System.Xml.XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter(memoryStream, Encoding.UTF8);

			T result = (T)xs.Deserialize(memoryStream);			
			return result;
		}

        public static T DeserializeItem<T>(XmlReader xmlReader)
        {
            StringBuilder xmlString = new StringBuilder();  
            while (xmlReader.Read()) xmlString.AppendLine(xmlReader.ReadOuterXml());

            T result = default(T);
            if (xmlString.Length != 0)
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
                System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(StringToUTF8ByteArray(xmlString.ToString()));
                System.Xml.XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter(memoryStream, Encoding.UTF8);

                result = (T)xs.Deserialize(memoryStream);                
            }
            return result;
		}

		public static object DeserializeItem(String xmlString, Type oType)
		{
			System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(oType);
			System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(StringToUTF8ByteArray(xmlString));
			System.Xml.XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter(memoryStream, Encoding.UTF8);
			return xs.Deserialize(memoryStream);
		}

        public static String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        public static Byte[] StringToUTF8ByteArray(String pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }

		public static void Serialize(XElement root, string name, string value)
		{
			if (value != null)
			{				
				XElement resultElement = new XElement(name, value);
				root.Add(resultElement);
			}
		}

		public static void Serialize(XElement root, string name, Nullable<DateTime> date)
		{
			if (date.HasValue)
			{
				Serialize(root, name, date.Value.ToString("yyyy-MM-ddTHH:mm:ss.FFF"));
			}
			else
			{
				XElement resultElement = new XElement(name);
				XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
				XAttribute nil = new XAttribute(xsi + "nil", true);
				resultElement.ReplaceAll(nil);
				root.Add(resultElement);

			}
		}

		public static void Serialize(XElement root, string name, DateTime date)
		{
			Serialize(root, name, date.ToString("yyyy-MM-ddTHH:mm:ss.FFF"));
		}

		public static void Serialize(XElement root, string name, int value)
		{
			Serialize(root, name, value.ToString());
		}

		public static void Serialize(XElement root, string name, bool value)
		{
			Serialize(root, name, value.ToString().ToLower());
		}

		public static void Serialize(XElement root, string name, XElement value)
		{
			XElement resultElement = new XElement(name);
            if (value != null)
            {
				resultElement.Add(value);
			}
			else
			{
				XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
				XAttribute nil = new XAttribute(xsi + "nil", true);
				resultElement.ReplaceAll(nil);
			}
			root.Add(resultElement);
		}        		
	}
}
