using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using YellowstonePathology.Document.Result.Data;
using YellowstonePathology.Document.Result.Xps;

namespace YellowstonePathology.Document.Result.MirthXml.EPIC
{
	/// <summary>Mirth HL7 data for HPV-16 Testing report
	/// </summary>
	public class OruHpv16 : MirthXmlBase
	{

		#region Private data
		/// <summary>report XML data
		/// </summary>
		private readonly Hpv16ReportData m_Data;
		#endregion Private data

		#region Constructors
		/// <summary>constructor
		/// </summary>
		/// <param name="data">report XML data</param>
		public OruHpv16(Hpv16ReportData data)
			: base(Hpv16Report.ReportName, data.PageHeader)
		{
			m_Data = data;
			AddCustomObxSegments();
			AddObxSegmentsForStandardTrailerSections(data.OtherReportsText, data.ReportDistributionList, Hpv16Report.DisclaimerIndex);
		}
		#endregion Constructors

		#region Private methods
		/// <summary>method add report specific OBX segments collection to document's root element
		/// </summary>
		private void AddCustomObxSegments()
		{
			AddResultsObxSegments();
			AddObxSegmentsForAmendments(m_Data.Amendments);
			AddObxSegmentsForHeaderSection(YpReportBase.SpecimenLabel, m_Data.SpecimenText);
			AddObxSegmentsForHeaderSection(YpReportBase.InterpretationLabel, m_Data.InterpretationText);
			AddObxSegmentsForHeaderSection(YpReportBase.MethodLabel, m_Data.MethodText);
		}
		/// <summary>method add OBX segments collection for results section
		/// </summary>
		private void AddResultsObxSegments()
		{
			AddObxSegment();
			AddResultsTitleObxSegment();
			for (int i = 0; i < m_Data.ResultRowsCount; i++)
			{
				AddResultObxSegment(i);
			}
			AddObxSegmentsForSection(m_Data.PathologistSignatureText);
		}
		/// <summary>method add OBX segment for result section's title
		/// </summary>
		private void AddResultsTitleObxSegment()
		{
			StringBuilder text = new StringBuilder();
			for (int i = 0; i < YpReportBase.ResultColumnsHeaders.Length; i++)
			{
				if (i == 1) text.Append(": ");
				if (i > 1) text.Append(", ");
				text.Append(YpReportBase.ResultColumnsHeaders[i]);
			}
			AddObxSegment(text.ToString());
		}
		/// <summary>method add OBX segment for result row
		/// </summary>
		/// <param name="rowIndex">row index</param>
		private void AddResultObxSegment(int rowIndex)
		{
			StringBuilder text = new StringBuilder();
			for (int colIndex = 0; colIndex < YpReportBase.ResultColumnsHeaders.Length; colIndex++)
			{
				if (colIndex == 1) text.Append(": ");
				string cellValue = m_Data.GetResultCellValue(rowIndex, colIndex);
				if (!string.IsNullOrEmpty(cellValue) && colIndex > 1) text.Append(", ");
				text.Append(cellValue);
			}
			AddObxSegment(text.ToString());
		}
		#endregion Private methods

	}
}
