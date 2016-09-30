using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

using YellowstonePathology.Document.Result.Data;
using YellowstonePathology.Document.Result.Xps;

namespace YellowstonePathology.Document.Result.MirthXml.ECW
{
	/// <summary>Mirth HL7 data for Leukemia Lymphoma report
	/// </summary>
	public class OruLeukemiaLymphoma : MirthXmlBase
	{

		#region Private data
		/// <summary>report XML data
		/// </summary>
		private readonly LeukemiaLymphomaReportData m_Data;
		#endregion Private data

		#region Constructors
		/// <summary>constructor
		/// </summary>
		/// <param name="data">report XML data</param>
		public OruLeukemiaLymphoma(LeukemiaLymphomaReportData data)
			: base(LeukemiaLymphomaReport.ReportName, data.PageHeader)
		{
			m_Data = data;
			AddCustomObxSegments();
			AddObxSegmentsForStandardTrailerSections(data.OtherReportsText, data.ReportDistributionList, LeukemiaLymphomaReport.DisclaimerIndex);
		}
		#endregion Constructors

		#region Private methods
		/// <summary>method add report specific OBX segments collection to document's root element
		/// </summary>
		private void AddCustomObxSegments()
		{
			AddMainBoxObxSegments();
			AddObxSegmentsForAmendments(m_Data.Amendments);
			AddObxSegmentsForSection(string.Format("{0} {1}", LeukemiaLymphomaReport.CellPopulationOfInterestLabel, m_Data.CellPopulationOfInterestText));
			AddMarkerObxSegments();
			AddCellDistributionObxSegments();
			AddBlastHeaderObxSegments();
		}
		/// <summary>method add OBX segments collection for main box section
		/// </summary>
		private void AddMainBoxObxSegments()
		{
			AddObxSegmentsForHeaderSection(LeukemiaLymphomaReport.ImpressionLabel, m_Data.ImpressionText);
			AddObxSegmentsForHeaderSection(LeukemiaLymphomaReport.InterpretiveCommentLabel, m_Data.InterpretiveCommentText);
			AddObxSegmentsForSection(m_Data.PathologistSignatureText);
		}
		/// <summary>method add OBX segments collection for markers section
		/// </summary>
		private void AddMarkerObxSegments()
		{
			AddObxSegment();
			AddMarkersTitleObxSegment();
			for (int i = 0; i < m_Data.MarkerRowsCount; i++)
			{
				AddMarkerObxSegment(i);
			}
		}
		/// <summary>method add OBX segment for markers section's title
		/// </summary>
		private void AddMarkersTitleObxSegment()
		{
			StringBuilder text = new StringBuilder();
			for (int i = 0; i < LeukemiaLymphomaReport.MarkerColumnHeaders.Length; i++)
			{
				if (i == 1) text.Append(": ");
				if (i > 1) text.Append(", ");
				text.Append(LeukemiaLymphomaReport.MarkerColumnHeaders[i]);
			}
			AddObxSegment(text.ToString());
		}
		/// <summary>method add OBX segment for single marker
		/// </summary>
		/// <param name="marker">marker object</param>
		private void AddMarkerObxSegment(int markerIndex)
		{
			StringBuilder text = new StringBuilder();
			for (int colIndex = 0; colIndex < LeukemiaLymphomaReportData.MarkerColumnsCount; colIndex++)
			{
				if (colIndex == 1) text.Append(": ");
				string cellValue = m_Data.GetMarkerCellValue(markerIndex, colIndex);
				if (!string.IsNullOrEmpty(cellValue) && colIndex > 1) text.Append(", ");
				text.Append(cellValue);
			}
			AddObxSegment(text.ToString());
		}
		/// <summary>method add OBX segments collection for cell distribution section
		/// </summary>
		private void AddCellDistributionObxSegments()
		{
			AddObxSegmentsForSection(LeukemiaLymphomaReport.CellDistributionLabel);
			for (int i = 0; i < LeukemiaLymphomaReportData.CellNames.Length; i++)
			{
				AddObxSegment(string.Format("{0}: {1}", LeukemiaLymphomaReport.CellTitles[i], m_Data.GetCellDistributionValue(i)));
			}
		}
		/// <summary>method add OBX segments collection for blast header section
		/// </summary>
		private void AddBlastHeaderObxSegments()
		{
			if (m_Data.IsBlastHeaderVisible)
			{
				AddObxSegmentsForSection(LeukemiaLymphomaReport.BlastHeaderLabel);
				for (int i = 0; i < LeukemiaLymphomaReportData.BlastNames.Length; i++)
				{
					AddObxSegment(string.Format(m_Nfi, "{0}: {1}", LeukemiaLymphomaReportData.BlastNames[i], m_Data.GetBlastHeaderValue(i)));
				}
			}
		}
		#endregion Private methods
	}
}
