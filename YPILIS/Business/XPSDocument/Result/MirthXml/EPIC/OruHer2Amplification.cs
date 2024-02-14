using System;
using System.Collections.Generic;
using System.Linq;

using YellowstonePathology.Document.Result.Data;
using YellowstonePathology.Document.Result.Xps;

namespace YellowstonePathology.Document.Result.MirthXml.EPIC
{
	/// <summary>Mirth HL7 data for HER2 Amplification report
	/// </summary>
	public class OruHer2Amplification : MirthXmlBase
	{

		#region Private data
		/// <summary>report XML data
		/// </summary>
		private readonly Her2AmplificationReportData m_Data;
		#endregion Private data

		#region Constructors
		/// <summary>constructor
		/// </summary>
		/// <param name="data">report XML data</param>
		public OruHer2Amplification(Her2AmplificationReportData data)
			: base(Her2AmplificationReport.ReportName, data.PageHeader)
		{
			m_Data = data;
			AddCustomObxSegments();
			AddObxSegmentsForStandardTrailerSections(data.OtherReportsText, data.ReportDistributionList, Her2AmplificationReport.DisclaimerIndex);
		}
		#endregion Constructors

		#region Private methods
		/// <summary>method add report specific OBX segments collection to document's root element
		/// </summary>
		private void AddCustomObxSegments()
		{
			AddMainBoxObxSegments();
			AddObxSegmentsForAmendments(m_Data.Amendments);
			AddResultDataObxSegments();
			AddSpecimenInformationObxSegments();
			AddObxSegmentsForHeaderSection(YpReportBase.InterpretationLabel, m_Data.InterpretationText);
			AddObxSegmentsForHeaderSection(YpReportBase.MethodLabel, m_Data.MethodText);
			AddObxSegmentsForHeaderSection(YpReportBase.ReferencesLabel, m_Data.ReferencesText);
		}

		/// <summary>method add OBX segments collection for main box section
		/// </summary>
		private void AddMainBoxObxSegments()
		{
			AddObxSegmentsForHeaderSection(YpReportBase.ResultLabel, string.Format("{0} {1})", Her2AmplificationReport.TestNameText, m_Data.TestResultText));
			AddObxSegment(m_Data.TestResultValue);
			AddReferenceTableObxSegments();
			AddObxSegmentsForSection(m_Data.PathologistSignatureText);
		}
		/// <summary>method add OBX segments collection for main box section's reference table
		/// </summary>
		private void AddReferenceTableObxSegments()
		{
			AddObxSegmentsForSection(Her2AmplificationReport.ReferenceTitle);
			for (int i = 0; i < Her2AmplificationReport.ReferenceRows.GetLength(0); i++)
			{
				AddObxSegment(string.Format("{0} {1} {2}", Her2AmplificationReport.ReferenceRows[i, 0], Her2AmplificationReport.ReferenceRows[i, 1], Her2AmplificationReport.ReferenceRows[i, 2]));
			}

		}
		/// <summary>method add OBX segments collection for "Result Data" section
		/// </summary>
		private void AddResultDataObxSegments()
		{
			AddObxSegmentsForSection(Her2AmplificationReport.ResultDataTitle);
			for (int i = 0; i < Her2AmplificationReport.ResultLabels.GetLength(0); i++)
			{
				AddObxSegment(string.Format("{0} {1}", Her2AmplificationReport.ResultLabels[i], m_Data.GetResultValue(i)));
			}
		}
		/// <summary>method add OBX segments collection for "SpecimenInformation" section
		/// </summary>
		private void AddSpecimenInformationObxSegments()
		{
			AddObxSegmentsForSection(Her2AmplificationReport.SpecimenTitle);
			for (int i = 0; i < Her2AmplificationReport.SpecimenLabels.GetLength(0); i++)
			{
				AddObxSegment(string.Format("{0} {1}", Her2AmplificationReport.SpecimenLabels[i], m_Data.GetSpecimenValue(i)));
			}
		}
		#endregion Private methods

	}
}
