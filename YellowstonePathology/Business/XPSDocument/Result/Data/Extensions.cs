using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Xml.Linq;

namespace YellowstonePathology.Document.Result.Data
{
	/// <summary>class with XElement extension methods
	/// </summary>
	static class XElementExtensions
	{

		#region Private constants
		/// <summary>date/time format (used for normal read string representation of dates in non-US cultures)
		/// </summary>
		private readonly static DateTimeFormatInfo m_Dfi = new CultureInfo("en-US").DateTimeFormat;
		/// <summary>numeric format (used for normal read string representation of numerics in non-US cultures)
		/// </summary>
		private readonly static NumberFormatInfo m_Nfi = new CultureInfo("en-US").NumberFormat;
		#endregion Private constants

		#region Public methods
		/// <summary>method add child element to this element 
		/// </summary>
		/// <param name="e">XElement instance</param>
		/// <param name="сhildElementName">child element name</param>
		/// <param name="сhildElementValue">child element value</param>
		public static XElement AddChildElement(this XElement e, string сhildElementName, string сhildElementValue = "")
		{
			XElement childElement = new XElement(сhildElementName, сhildElementValue);
			e.Add(childElement);
			return childElement;
		}
		/// <summary>method add HL7 child element to this element if child element value is not empty
		/// </summary>
		/// <param name="e">XElement instance</param>
		/// <param name="сhildElementName">child element name</param>
		/// <param name="сhildElementValue">child element value</param>
		/// <returns>added child element if child element value is not empty, otherwise null</returns>
		public static XElement AddChildHl7Element(this XElement e, string сhildElementName, string сhildElementValue)
		{
			return (сhildElementValue == string.Empty ? null : e.AddChildElement(сhildElementName, сhildElementValue));
		}
		/// <summary>method add child element to this element from source element or empty child element, if source child element is not found 
		/// </summary>
		/// <param name="e">XElement instance</param>
		/// <param name="srcParentElement">source element with child element, which value is used for new element</param>
		/// <param name="srcChildElementName">child element name, which value is used for new element</param>
		/// <param name="destChildElementName">name of new element, default is source child element name</param>
		/// <returns>added child element</returns>
		public static XElement AddChildElement(this XElement e, XElement srcParentElement, string srcChildElementName, string destChildElementName = "")
		{
			string name = (destChildElementName == "" ? srcChildElementName : destChildElementName);
			XElement srcElement = srcParentElement.Element(srcChildElementName);
			if(srcElement != null)
				return e.AddChildElement(name, srcElement.Value);
			else
				return e.AddChildElement(name);
		}
		/// <summary>method add child element to XML data class
		/// </summary>
		/// <param name="e">XElement instance</param>
		/// <param name="xmlDataDocument">source XML element</param>
		/// <param name="rootName">name of source XML parent collection element</param>
		/// <param name="itemName">name of source XML collection item element</param>
		/// <param name="srcItemChildName">name of source XML collection item's child element</param>
		/// <param name="destItemChildName">name of added XML element</param>
		/// <param name="whereItemChildName">optional name of source XML collection item's child element used for filtration</param>
		/// <param name="whereItemChildValue">optional value of source XML collection item's child element used for filtration</param>
		public static XElement AddChildElement(this XElement e, XElement xmlDataDocument, string rootName, string itemName, string srcItemChildName, string destItemChildName, string[] whereItemChildNames = null, string[] whereItemChildValues = null)
		{
			XElement childElement;
			XElement root = xmlDataDocument.Descendants(rootName).FirstOrDefault();
			if (root != null)
			{
				XElement xElement = null;
				if (whereItemChildNames != null)
				{
					int whereCount = whereItemChildNames.GetLength(0);
					if (whereCount > 0)
					{
						List<XElement> query = root.Elements(itemName).Where((x) => x.Element(whereItemChildNames[0]).Value == whereItemChildValues[0]).ToList();
						if (query.Count() > 0 && whereCount > 1)
						{
							for (int i = 1; i < whereCount; i++)
							{
								query = query.Where((x) => x.Element(whereItemChildNames[i]).Value == whereItemChildValues[i]).ToList();
								if (query.Count() == 0) break;
							}
						}
						if (query.Count > 0)
							xElement = query[0];
						else
							xElement = null;

					}
					else
						xElement = root.Elements(itemName).FirstOrDefault();
				}
				else
					xElement = root.Elements(itemName).FirstOrDefault();
				if (xElement != null)
					childElement = e.AddChildElement(destItemChildName, xElement.GetStringValue(srcItemChildName));
				else
					childElement = e.AddChildElement(destItemChildName);
			}
			else
				childElement = e.AddChildElement(destItemChildName);
			return childElement;
		}
		/// <summary>method add child element to this element from source element descendands or empty child element, if source child element is not found 
		/// </summary>
		/// <param name="e">XElement instance</param>
		/// <param name="srcParentElement">source element with descendant elements, which value is used for new element</param>
		/// <param name="srcChildElementName">descendant element name, which value is used for new element</param>
		/// <param name="destChildElementName">name of added element, default is source child element name</param>
		/// <returns></returns>
		public static XElement AddFirstDescendantElement(this XElement e, XElement srcParentElement, string srcChildElementName, string destChildElementName = "")
		{
			string name = (destChildElementName == "" ? srcChildElementName : destChildElementName);
			XElement srcElement = srcParentElement.Descendants(srcChildElementName).FirstOrDefault() ;
			if (srcElement != null)
				return e.AddChildElement(name, srcElement.Value);
			else
				return e.AddChildElement(name);
		}
		/// <summary>method return child element's value as string or empty string, if child element is not found
		/// </summary>
		/// <param name="e">XElement instance</param>
		/// <param name="elementName">child element name</param>
		/// <returns>child element's value as string or empty string, if child element is not found</returns>
		public static string GetStringValue(this XElement e, string elementName, string parentElementName = "")
		{
			XElement srcElement = (parentElementName == "" ? e.Element(elementName) : (e.Element(parentElementName) == null ? null : e.Element(parentElementName).Element(elementName)));
			if (srcElement != null)
				return srcElement.Value;
			else
				return string.Empty;
		}
		/// <summary>method return child element's value as nullable DateTime
		/// </summary>
		/// <param name="e">XElement instance</param>
		/// <param name="elementName">child element name</param>
		/// <returns>child element's value as DateTime or null, if child's element value isn't valid DateTime</returns>
		public static DateTime? GetNullableDateValue(this XElement e, string elementName)
		{
			string dateString = e.Element(elementName).Value;

			if (string.IsNullOrEmpty(dateString)) return null;
			DateTime date;
			if (DateTime.TryParse(dateString, m_Dfi, DateTimeStyles.None, out date))
				return new DateTime?(date);
			else
				return null;
		}
		/// <summary>method return date/time value as string with null value check
		/// </summary>
		/// <param name="e">XElement instance</param>
		/// <param name="elementName">name of element with DateTime value</param>
		/// <returns>date/time value as string or empty string, if child's element value isn't valid DateTime</returns>
		public static string GetDateTime(this XElement e, string elementName, string format = "MM/dd/yyyy hh:mm tt")
		{
			DateTime? date = e.GetNullableDateValue(elementName);
			return date.HasValue ? date.Value.ToString(format, m_Dfi) : string.Empty;
		}
		/// <summary>method return date with optional time value as string with null value check
		/// </summary>
		/// <param name="e">XElement instance</param>
		/// <param name="elementName">name of element with DateTime value</param>
		/// <returns>date with optional time as string or empty string, if child's element value isn't valid DateTime</returns>
		public static string GetDateWithOptionalTime(this XElement e, string elementName)
		{
			DateTime? date = e.GetNullableDateValue(elementName);
			if (date.HasValue)
			{
				DateTime dateValue = (DateTime)date;
				string format = (dateValue.Hour == 0 && dateValue.Minute == 0 ? "MM/dd/yyyy" : "MM/dd/yyyy hh:mm");
				return dateValue.ToString(format, m_Dfi);
			}
			else
				return string.Empty;
		}
		/// <summary>method return child element's value as nullable int
		/// </summary>
		/// <param name="e">XElement instance</param>
		/// <param name="elementName">child element name</param>
		/// <returns>child element's value as int or null, if child's element value isn't valid int</returns>
		public static int? GetNullableIntValue(this XElement e, string elementName, string parentElementName = "")
		{
			string strValue = (parentElementName == "" ? e.Element(elementName).Value : e.Element(parentElementName).Element(elementName).Value);
			if (string.IsNullOrEmpty(strValue)) return null;
			int intValue;
			if (int.TryParse(strValue, out intValue))
				return new int?(intValue);
			else
				return null;
		}
		/// <summary>method return XML int value as int with null value check
		/// </summary>
		/// <param name="e">XElement instance</param>
		/// <param name="elementName">name of element with int value</param>
		/// <returns>value as int or 0, if child's element value isn't valid int</returns>
		public static int GetIntValue(this XElement e, string elementName, string parentElementName = "")
		{
			return e.GetNullableIntValue(elementName, parentElementName) ?? 0;
		}
		/// <summary>method return child element's value as nullable decimal
		/// </summary>
		/// <param name="e">XElement instance</param>
		/// <param name="elementName">child element name</param>
		/// <returns>child element's value as decimal or null, if child's element value isn't valid decimal</returns>
		public static decimal? GetNullableDecimalValue(this XElement e, string elementName, string parentElementName = "")
		{
			string strValue = (parentElementName == "" ? e.Element(elementName).Value : e.Element(parentElementName).Element(elementName).Value);
			if (string.IsNullOrEmpty(strValue)) return null;
			decimal decValue;
			if (decimal.TryParse(strValue, NumberStyles.Number, m_Nfi, out decValue))
				return new decimal?(decValue);
			else
				return null;
		}
		/// <summary>method return XML int value as decimal with null value check
		/// </summary>
		/// <param name="e">XElement instance</param>
		/// <param name="elementName">name of element with decimal value</param>
		/// <returns>value as decimal or 0, if child's element value isn't valid decimal</returns>
		public static decimal GetDecimalValue(this XElement e, string elementName, string parentElementName = "")
		{
			return e.GetNullableDecimalValue(elementName, parentElementName) ?? 0;
		}
		/// <summary>method return child element's value as nullable boolean
		/// </summary>
		/// <param name="e">XElement instance</param>
		/// <param name="elementName">child element name</param>
		/// <returns>child element's value as boolean or null, if child's element value isn't valid boolean</returns>
		public static bool? GetNullableBoolValue(this XElement e, string elementName, string parentElementName = "")
		{
			string strValue = (parentElementName == "" ? e.Element(elementName).Value : e.Element(parentElementName).Element(elementName).Value);
			if (string.IsNullOrEmpty(strValue)) return null;
			bool bValue;
			if (bool.TryParse(strValue, out bValue))
				return new bool?(bValue);
			else
				return null;
		}
		/// <summary>method return XML int value as boolean with null value check
		/// </summary>
		/// <param name="e">XElement instance</param>
		/// <param name="elementName">name of element with boolean value</param>
		/// <returns>value as boolean or false, if child's element value isn't valid boolean</returns>
		public static bool GetBoolValue(this XElement e, string elementName, string parentElementName = "")
		{
			return e.GetNullableBoolValue(elementName, parentElementName) ?? false;
		}
		#endregion Public methods
	}
}
