using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace YellowstonePathology.Document.Result.MirthXml
{
	/// <summary>converter from MirthXml document to string with all OBX segments content
	/// </summary>
	public class MirthXmlObxTextConv
	{

		#region Private constants
		/// <summary>name of root xml element for OBX segment
		/// </summary>
		private const string m_ObxRootName = "OBX.5";
		/// <summary>name of xml element with text for OBX segment
		/// </summary>
		private const string m_ObxBodyName = "OBX.5.1";
		#endregion Private constants

		#region Private data
		/// <summary>MirthXml document
		/// </summary>
		private readonly XDocument _MirthXmlDoc;
		#endregion Private data

		#region Constructors
		/// <summary>constructor with input MirthXml document
		/// </summary>
		/// <param name="mirthXmlDoc">MirthXml document</param>
		public MirthXmlObxTextConv(XDocument mirthXmlDoc)
		{
			_MirthXmlDoc = mirthXmlDoc;
		}
		#endregion Constructors

		#region Public methods
		/// <summary>method return string with with all OBX segments content
		/// </summary>
		/// <returns>string with with all OBX segments content</returns>
		public override string ToString()
		{
			const string delimeter = "\r\n";
			string[] obxLines = _MirthXmlDoc.Descendants(m_ObxRootName).SelectMany(s => GetBodyObxSegments(s)).Select(s => s.Value).ToArray();
			string obxText = string.Join(delimeter, obxLines);
			return obxText.Substring(2, obxText.Length - delimeter.Length);
		}
        #endregion Public methods

		#region Private methods
		/// <summary>method return list of child OBX 5.1 segments or empty OBX 5.1 segments, if parent OBX segment 
		/// not contains any OBX 5.1 segments
		/// </summary>
		/// <param name="rootObxSegment">parent OBX segment</param>
		/// <returns>list of child OBX 5.1 segments</returns>
		private static IEnumerable<XElement> GetBodyObxSegments(XElement rootObxSegment)
		{
			IEnumerable<XElement> obxBodySegments = rootObxSegment.Descendants(m_ObxBodyName);
			if (obxBodySegments.Count() == 0)
				return new XElement[] { new XElement(m_ObxBodyName) };
			else
				return obxBodySegments;
		}
		#endregion Private methods
	}
}
