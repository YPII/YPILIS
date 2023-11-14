using System;
using System.Collections.Generic;
using System.Linq;

using YellowstonePathology.Document.Result.Data;
using YellowstonePathology.Document.Result.Xps;

namespace YellowstonePathology.Document.Result.MirthXml.EPIC
{
	/// <summary>Mirth HL7 data for BRAF V600E Mutation Analysis report
	/// </summary>
	public class OruSurgicalPathology : MirthXmlBase
	{
		#region Private data
		/// <summary>report XML data
		/// </summary>
		private readonly SurgicalPathologyReportData m_Data;
		#endregion Private data

		#region Constructors
		/// <summary>constructor
		/// </summary>
		/// <param name="data">report XML data</param>
		public OruSurgicalPathology(SurgicalPathologyReportData data)
			: base(SurgicalPathologyReport.ReportName, data.PageHeader)
		{
			m_Data = data;
			AddCustomObxSegments();
			//AddObxSegmentsForStandardTrailerSections(data.OtherReportsText, data.ReportDistributionList, BrafReport.DisclaimerIndex);
		}
		#endregion Constructors

		#region Private methods
		/// <summary>method add report specific OBX segments collection to document's root element
		/// </summary>
		private void AddCustomObxSegments()
		{
			AddMainBoxObxSegments();
			AddObxSegmentsForAmendments(m_Data.Amendments);
			AddObxSegmentsForHeaderSection(SurgicalPathologyReport.MicroscopicDescriptionTitle, m_Data.MicroscopicDescriptionText);
			AddObxSegmentsForHeaderSection(SurgicalPathologyReport.ClinicalInfoTitle, m_Data.ClinicalInfoText);
			AddObxSegmentsForHeaderSection(SurgicalPathologyReport.GrossDescriptionTitle, m_Data.GrossDescriptionText);
			AddObxSegmentsForHeaderSection(YpReportBase.OtherReportsLabel, m_Data.OtherReportsText);
			if (m_Data.IsRevised) AddPrevDiagnosisObxSegments();
			AddReportDistributionObxSegments(m_Data.ReportDistributionList);
		}
		/// <summary>method add OBX segments collection for main box section
		/// </summary>
		private void AddMainBoxObxSegments()
		{
			AddObxSegmentsForSection(m_Data.DiagnosisLabel);
			for (int i = 0; i < m_Data.DiagnosisRowsCount; i++)
			{
				AddObxSegmentsForHeaderSection(m_Data.GetDiagnosisRowTitle(i), m_Data.GetDiagnosisRowBody(i));
			}
			AddObxSegmentsForHeaderSection(YpReportBase.CommentLabel, m_Data.DiagnosisCommentText);
			AddObxSegmentsForSection(m_Data.PathologistSignatureText);
		}
		/// <summary>method add OBX segments collection for "Previous Diagnosis" section
		/// </summary>
		private void AddPrevDiagnosisObxSegments()
		{
			AddObxSegmentsForSection(m_Data.PrevDiagnosisLabel);
			for (int i = 0; i < m_Data.PrevDiagnosisRowsCount; i++)
			{
				AddObxSegmentsForHeaderSection(m_Data.GetPrevDiagnosisRowTitle(i), m_Data.GetPrevDiagnosisRowBody(i));
			}
			AddObxSegmentsForHeaderSection(YpReportBase.CommentLabel, m_Data.PrevDiagnosisCommentText);
			AddObxSegmentsForSection(m_Data.PathologistSignatureText);
		}
		#endregion Private methods


	}
}
