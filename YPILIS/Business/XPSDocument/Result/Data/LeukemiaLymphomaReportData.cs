using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace YellowstonePathology.Document.Result.Data
{
	/// <summary>Leukemia Lymphoma Phenotyping report data class
	/// </summary>
	public class LeukemiaLymphomaReportData : YpReportDataBase
    {

		#region Public constants
		/// <summary>array of сell's names
		/// </summary>
		public static readonly string[] CellNames = new string[] { "Lymphocyte", "Monocyte", "Myeloid", "DimCD45ModSS" };
		/// <summary>array of Blast Header сell's names
		/// </summary>
		public static readonly string[] BlastNames = new string[] { "CD34", "CD117" };
		#endregion Public constants

		#region Private constants
		/// <summary>Impression XML element name
		/// </summary>
		private static readonly string Impression = "Impression";
		/// <summary>Interpretive Сomment XML element name
		/// </summary>
		private static readonly string InterpretiveComment = "InterpretiveComment";
		/// <summary>Cell Population Of Interest XML element name
		/// </summary>
		private static readonly string CellPopulationOfInterest = "CellPopulationOfInterest";
		/// <summary>markers array xml element name
		/// </summary>
		private static readonly string MarkersName = "FlowMarkerCollection";
		/// <summary>markers xml element name
		/// </summary>
		private static readonly string MarkerName = "FlowMarker";
		/// <summary>array of marker column's XML element name
		/// </summary>
		private static readonly string[] MarkerColumnValues = new string[] { "Name", "Interpretation", "Intensity" };
		/// <summary>suffix for XML elements with cells count
		/// </summary>
		private static readonly string CellNamesSuffix = "Count";
		/// <summary>suffix for XML elements with cells percent
		/// </summary>
		private static readonly string CellPercentSuffix = "Percent";
		/// <summary>Blast Header XML element name
		/// </summary>
		private static readonly string BlastHeaderSection = "EGateBlastPercentSection";
		/// <summary>prefix for XML elements with Blast Header cells percent
		/// </summary>
		private static readonly string BlastNamesPrefix = "EGate";
		/// <summary>suffix for XML elements with Blast Header cells percent
		/// </summary>
		private static readonly string BlastNamesSuffix = "Percent";
		/// <summary>Blast Header section IsVisible XML element name
		/// </summary>
		private static readonly string BlastHeaderSectionIsVisible = "IsVisible";
		#endregion Private constants

		#region Constructors
		/// <summary>constructor with report number and XML data block
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="inDocument">XML element with input data</param>
		public LeukemiaLymphomaReportData(string reportNo, XElement xmlDataDocument)
			: base(reportNo, xmlDataDocument, "LeukemiaLyphomaReport")
        {
		}
		#endregion Constructors

		#region Public properties
		/// <summary>property return body text for "Impression" section
		/// </summary>
		public string ImpressionText
		{
			get { return (this.GetStringValue(LeukemiaLymphomaReportData.Impression)); }
		}
		/// <summary>property return body text for "InterpretiveComment" section
		/// </summary>
		public string InterpretiveCommentText
		{
			get { return (this.GetStringValue(LeukemiaLymphomaReportData.InterpretiveComment)); }
		}
		/// <summary>property return rows count for table in "Markers" section
		/// </summary>
		public int MarkerRowsCount
		{
			get { return Element(MarkersName).Elements(MarkerName).Count(); }
		}
		/// <summary>property return columns count for table in "Markers" section
		/// </summary>
		public static int MarkerColumnsCount
		{
			get { return MarkerColumnValues.GetLength(0); }
		}
		/// <summary>property return body text for "Cell Population Of Interest" section
		/// </summary>
		public string CellPopulationOfInterestText
		{
			get { return (this.GetStringValue(LeukemiaLymphomaReportData.CellPopulationOfInterest)); }
		}
		/// <summary>property return true, if "Blast Header" section must be visible
		/// </summary>
		public bool IsBlastHeaderVisible
		{
			get { return this.GetBoolValue(LeukemiaLymphomaReportData.BlastHeaderSectionIsVisible, LeukemiaLymphomaReportData.BlastHeaderSection); }
		}
		#endregion Public properties

		#region Public methods
		/// <summary>method return cell value of markers table
		/// </summary>
		/// <param name="markerIndex">marker index</param>
		/// <param name="columnIndex">column index</param>
		/// <returns>cell value of markers table</returns>
		public string GetMarkerCellValue(int markerIndex, int columnIndex)
		{
			IEnumerable<XElement> markers = Element(MarkersName).Elements(MarkerName);
			XElement marker = markers.ElementAt(markerIndex);
			return marker.GetStringValue(MarkerColumnValues[columnIndex]);
		}
		/// <summary>method return cell distribution value
		/// </summary>
		/// <param name="cellIndex">cell distribution index</param>
		/// <returns>cell distribution value</returns>
		public string GetCellDistributionValue(int cellIndex)
		{
			return this.GetStringValue(string.Format("{0}{1}", LeukemiaLymphomaReportData.CellNames[cellIndex], LeukemiaLymphomaReportData.CellPercentSuffix));
		}
		/// <summary>method return blast header value
		/// </summary>
		/// <param name="blastIndex">blast index</param>
		/// <returns>blast header value</returns>
		public string GetBlastHeaderValue(int blastIndex)
		{
			string dataItemName = string.Format("{0}{1}{2}", LeukemiaLymphomaReportData.BlastNamesPrefix, LeukemiaLymphomaReportData.BlastNames[blastIndex], LeukemiaLymphomaReportData.BlastNamesSuffix);
			return string.Format("{0}%", this.GetStringValue(dataItemName, LeukemiaLymphomaReportData.BlastHeaderSection));
		}
		#endregion Public methods

		#region Protected methods
		/// <summary>method add custom report data
		/// </summary>
		/// <param name="xmlDataDocument">XML document with report data</param>
		protected override void AddCustomData(string reportNo, XElement xmlDataDocument)
		{
			XElement flowLeukemia = xmlDataDocument.Element("FlowLeukemia");
			AddRootData(xmlDataDocument);
			AddFlowLeukemiaData(flowLeukemia);
			Add(new XElement(MarkersName, xmlDataDocument.Element(MarkersName).Elements(MarkerName)));
			AddCellDistributionData(flowLeukemia);
			AddReportBlastData(flowLeukemia);
		}
		#endregion Protected methods

		#region Private methods
		/// <summary>method add root data
		/// </summary>
		/// <param name="flowAccessionDocument">XML document with report data</param>
		private void AddRootData(XElement flowAccessionDocument)
		{
			this.AddChildElement(flowAccessionDocument, "Final");
			this.AddChildElement(flowAccessionDocument, "FinalDate");
			this.AddChildElement(flowAccessionDocument, PathologistSignature);
			this.AddChildElement(flowAccessionDocument, "SpecimenType");
			this.AddChildElement(flowAccessionDocument, "AccessionDate");
		}
		/// <summary>method add flowLeukemia data
		/// </summary>
		/// <param name="flowLeukemia">flowLeukemia section of XML document with report data</param>
		private void AddFlowLeukemiaData(XElement flowLeukemia)
		{
			this.AddChildElement(flowLeukemia, "GatingPopulationV2");
			this.AddChildElement(flowLeukemia, "GatingPopulationV2");
			this.AddChildElement(flowLeukemia, "OtherCount");
			this.AddChildElement(flowLeukemia, "OtherName");
			this.AddChildElement(flowLeukemia, InterpretiveComment);
			this.AddChildElement(flowLeukemia, Impression);
			this.AddChildElement(flowLeukemia, CellPopulationOfInterest);
			this.AddChildElement(flowLeukemia, "TestResult");
			this.AddChildElement(flowLeukemia, "CellDescription");
			this.AddChildElement(flowLeukemia, "BTCellSelection");
		}
		/// <summary>method add cell distribution data block
		/// </summary>
		/// <param name="flowLeukemia">flowLeukemia section of XML document with report data</param>
		private void AddCellDistributionData(XElement flowLeukemia)
        {
			int i;
			string countName;
			decimal totalCount = 0;
			int itemsCount = CellNames.GetLength(0);
			decimal[] count = new decimal[itemsCount];
			decimal[] percent = new decimal[itemsCount];

			for (i = 0; i < itemsCount; i++)
			{
				countName = string.Format("{0}{1}", CellNames[i], CellNamesSuffix);
				this.AddChildElement(flowLeukemia, countName);
				count[i] = this.GetIntValue(countName);
				totalCount+=count[i];
			}
			for (i = 0; i < itemsCount; i++)
			{
				countName = string.Format("{0}{1}", CellNames[i], CellPercentSuffix);
				if (totalCount > 0)
				{
					percent[i] = count[i] * 100 / totalCount;
				}
				else
				{
					percent[i] = 0;
				}
				Add(new XElement(countName, GetCellDistributionValueString(percent[i])));
			}
		}
		/// <summary>method add "BlastHeader" data block
		/// </summary>
		/// <param name="flowLeukemia">flowLeukemia section of XML document with report data</param>
		private void AddReportBlastData(XElement flowLeukemia)
		{
			int blastCount = BlastNames.GetLength(0);
			string[] blastNames = new string[blastCount];
			decimal[] blastValues = new decimal[blastCount];
			bool isVisible = false;
			for (int i = 0; i < blastCount; i++)
			{
				blastNames[i] = string.Format("{0}{1}{2}", BlastNamesPrefix, BlastNames[i], BlastNamesSuffix);
				blastValues[i] = flowLeukemia.GetDecimalValue(blastNames[i]);
				if (blastValues[i] != 0) isVisible = true;
			}
			XElement reportBlast = this.AddChildElement(BlastHeaderSection);
			reportBlast.AddChildElement(BlastHeaderSectionIsVisible, isVisible.ToString());
			for (int i = 0; i < blastCount; i++)
			{
				reportBlast.AddChildElement(blastNames[i], string.Format(m_Nfi, "{0}", blastValues[i]));
			}
		}
		/// <summary>method return string representation of cell distribution value
		/// </summary>
		/// <param name="value">cell distribution value</param>
		/// <returns>cell distribution value or "less 1%" if value < 1</returns>
		private static string GetCellDistributionValueString(decimal value)
		{
			if (value < 1)
				return "< 1%";
			else
				return string.Format("{0:##0.00}%", value);
		}
		#endregion Private methods
	}
}
