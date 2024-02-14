using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Document
{
    public class XMLHelper
    {
        public static bool IsDateElementInRange(DateTime inputDate, DateTime startDate, DateTime endDate)
        {
            bool result = false;
            if (inputDate >= startDate && inputDate <= endDate)
            {
                result = true;
            }
            return result;
        }

        public static bool ElementExists(XElement parentElement, string elementName)
        {
            bool result = false;
            XElement element = parentElement.Element(elementName);
            if (element != null)
            {
                result = true;
            }
            return result;
        }

        public static List<XElement> GetElementList(XElement parentElement, string collectionName, string elementName)
        {            
            List<XElement> result = new List<XElement>();
            if (ElementExists(parentElement, collectionName) == true)
            {
                result = parentElement.Element(collectionName).Elements(elementName).ToList<XElement>();
            }
            return result;
        }

        public static Nullable<DateTime> GetNullableDateTime(XElement parentElement, string elementName)
        {
            Nullable<DateTime> result = null;
            XElement propertyElement = parentElement.Element(elementName);
            if (propertyElement != null)
            {
                result = DateTime.Parse(propertyElement.Value);
            }
            return result;
        }

        public static string GetString(XElement parentElement, string elementName)
        {
            string result = null;
            XElement propertyElement = parentElement.Element(elementName);
            if (propertyElement != null)
            {
                result = propertyElement.Value;
            }
            return result;
        }
    }
}
