using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Helper
{
    public class SerializationHelper
    {
        public static String SerializeObject(Object pObject, Type type)
        {            
            String XmlizedString = null;
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(type);
            System.Xml.XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter(memoryStream, Encoding.UTF8);            
            xs.Serialize(xmlTextWriter, pObject);
            memoryStream = (System.IO.MemoryStream)xmlTextWriter.BaseStream;
			XmlizedString = YellowstonePathology.Business.Helper.SerializationHelper.UTF8ByteArrayToString(memoryStream.ToArray());
            return XmlizedString.Substring(1, XmlizedString.Length - 1);            
        }

        public static Object DeserializeObject(String pXmlizedString, Type type)
        {
            object result = null;
            if (string.IsNullOrEmpty(pXmlizedString) == false)
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(type);
				System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(YellowstonePathology.Business.Helper.SerializationHelper.StringToUTF8ByteArray(pXmlizedString));
                System.Xml.XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter(memoryStream, Encoding.UTF8);
                result = xs.Deserialize(memoryStream);
            }
            return result;
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
    }
}
