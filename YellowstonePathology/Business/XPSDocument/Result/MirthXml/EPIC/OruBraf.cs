using System;
using System.Collections.Generic;
using System.Linq;

using YellowstonePathology.Document.Result.Data;
using YellowstonePathology.Document.Result.Xps;

namespace YellowstonePathology.Document.Result.MirthXml.EPIC
{
	/// <summary>Mirth HL7 data for BRAF V600E Mutation Analysis report
	/// </summary>
	public class OruBraf : MirthXmlBase
	{

		#region Private data
		/// <summary>report XML data
		/// </summary>
		private readonly BrafReportData m_Data;
		#endregion Private data

		#region Constructors
		/// <summary>constructor
		/// </summary>
		/// <param name="data">report XML data</param>
		public OruBraf(BrafReportData data)
			: base(BrafReport.ReportName, data.PageHeader)
		{
			m_Data = data;
			AddCustomObxSegments();
			AddObxSegmentsForStandardTrailerSections(data.OtherReportsText, data.ReportDistributionList, BrafReport.DisclaimerIndex);
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
			AddObxSegmentsForHeaderSection(YpReportBase.IndicationLabel, m_Data.ReportIndicationText);
			AddObxSegmentsForHeaderSection(YpReportBase.InterpretationLabel, m_Data.InterpretationText);
			AddObxSegmentsForHeaderSection(YpReportBase.MethodLabel, m_Data.MethodText);
			AddObxSegmentsForHeaderSection(YpReportBase.ReferencesLabel, m_Data.ReferencesText);
		}
		/// <summary>method add OBX segments collection for main box section
		/// </summary>
		private void AddMainBoxObxSegments()
		{
			AddObxSegmentsForSection(YpReportBase.ResultLabel);
			AddObxSegment(string.Format("{0} {1}", m_Data.TestNameText, m_Data.TestResultText));
			AddObxSegment(string.Format("{0} {1}", YpReportBase.CommentLabel, m_Data.ResultCommentText));
			AddObxSegmentsForSection(m_Data.PathologistSignatureText);
		}
		#endregion Private methods

	}
}
