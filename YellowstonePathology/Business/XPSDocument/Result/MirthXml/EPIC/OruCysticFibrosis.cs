using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

using YellowstonePathology.Document.Result.Data;
using YellowstonePathology.Document.Result.Xps;

namespace YellowstonePathology.Document.Result.MirthXml.EPIC
{
	/// <summary>Mirth HL7 data for Cystic Fibrosis Carrier Screnning report
	/// </summary>
	public class OruCysticFibrosis : MirthXmlBase
	{

		#region Private data
		/// <summary>report XML data
		/// </summary>
		private readonly CysticFibrosisReportData m_Data;
		#endregion Private data

		#region Constructors
		/// <summary>constructor
		/// </summary>
		/// <param name="data">report XML data</param>
		public OruCysticFibrosis(CysticFibrosisReportData data)
			: base(CysticFibrosisReport.ReportName, data.PageHeader)
		{
			m_Data = data;
			AddCustomObxSegments();
			AddObxSegmentsForStandardTrailerSections(data.OtherReportsText, data.ReportDistributionList, CysticFibrosisReport.DisclaimerIndex);
		}
		#endregion Constructors

		#region Private methods
		/// <summary>method add report specific OBX segments collection to document's root element
		/// </summary>
		private void AddCustomObxSegments()
		{
			AddMainBoxObxSegments();
			AddObxSegmentsForAmendments(m_Data.Amendments);
			AddObxSegmentsForHeaderSection(YpReportBase.SpecimenLabel, m_Data.SpecimenText);
			AddObxSegmentsForHeaderSection(YpReportBase.InterpretationLabel, m_Data.InterpretationText);
			AddInterpretationRefTableObxSegments();
			AddObxSegmentsForHeaderSection(YpReportBase.MethodLabel, m_Data.MethodText);
			AddObxSegmentsForHeaderSection(CysticFibrosisReport.MutationsTestedLabel, m_Data.MutationsTestedXml);
			AddObxSegmentsForHeaderSection(YpReportBase.ReferencesLabel, m_Data.ReferencesText);
		}
		/// <summary>method add OBX segments collection for main box section
		/// </summary>
		private void AddMainBoxObxSegments()
		{
			AddObxSegmentsForHeaderSection(YpReportBase.ResultLabel, string.Format("{0}: {1})", m_Data.TestNameText, m_Data.TestResultText));
			AddObxSegment(m_Data.ResultCommentText);
			AddObxSegmentsForSection(m_Data.PathologistSignatureText);
		}
		/// <summary>method add OBX segments collection for interpretation reference table section
		/// </summary>
		private void AddInterpretationRefTableObxSegments()
		{
			AddObxSegmentsForSection(m_Data.InterpretationRefTableHeaderText);
			InterpretationRefTableTitleObxSegment();
			for (int i = 0; i < m_Data.InterpretationRefTableRowsCount; i++)
			{
				StringBuilder text = new StringBuilder();
				for (int j = 0; j < m_Data.InterpretationRefTableColsCount; j++)
				{
					if (j == 1) text.Append(": ");
					if (j > 1) text.Append(", ");
					text.Append(m_Data.GetInterpretationRefTableCellText(i, j));
				}
				AddObxSegment(text.ToString());
			}
		}
		/// <summary>method add OBX segment for interpretation reference table's title
		/// </summary>
		private void InterpretationRefTableTitleObxSegment()
		{
			StringBuilder text = new StringBuilder();
			IEnumerable<XElement> colHeaders = m_Data.InterpretationRefTableColHeaders;
			for (int i = 0; i < colHeaders.Count(); i++)
			{
				if (i == 1) text.Append(": ");
				if (i > 1) text.Append(", ");
				text.Append(colHeaders.ElementAt(i).Value);
			}
			AddObxSegment(text.ToString());
		}
		#endregion Private methods

	}
}
