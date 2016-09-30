using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using YellowstonePathology.Document.Result.Data;
using YellowstonePathology.Document.Xps;

namespace YellowstonePathology.Document.Result.Xps
{
	/// <summary>BRAF V600E Mutation Analysis report class
	/// </summary>
	public class BrafReport : YpReportBase
	{
		#region Public constants
		/// <summary>report name
		/// </summary>
		public const string ReportName = "BRAF V600E Mutation Analysis";
		/// <summary>index of disclaimer text
		/// </summary>
		public static readonly int DisclaimerIndex = 2;
		#endregion Public constants

		#region Private data
		/// <summary>report XML data object
		/// </summary>
		private readonly BrafReportData m_ReportData;
		#endregion Private data

		#region Constructors
		/// <summary>constructor with XML input data
		/// </summary>
		/// <param name="reportData">report XML data object</param>
		public BrafReport(BrafReportData reportData)
			: base(ReportName, reportData.PageHeader)
        {
			m_ReportData = reportData;

			WriteMainBoxSections(m_ReportData.PathologistSignatureText);
			WriteAmendments(m_ReportData.Amendments, true);
			WriteSimpleSectionWithTitle(SpecimenLabel, m_ReportData.SpecimenText);
			WriteSimpleSectionWithTitle(IndicationLabel, m_ReportData.ReportIndicationText);
			WriteSimpleSectionWithTitle(InterpretationLabel, m_ReportData.InterpretationText);
			WriteSimpleSectionWithTitle(MethodLabel, m_ReportData.MethodText);
			WriteSimpleSectionWithTitle(ReferencesLabel, m_ReportData.ReferencesText);
			
			WriteStandardTrailerSections(reportData.OtherReportsText, reportData.ReportDistributionList, DisclaimerIndex);
		}
		#endregion Constructors

		#region Protected methods
		/// <summary>method write report's main box custom sections to document grid
		/// </summary>
		protected override int WriteCustomSectionToMainBox()
		{
			const double fontSize = m_FontSize + 2;
			Grid grid = XPSHelper.GetGrid(XPSHelper.GetFullWidthGridColWidthArray(new double[] { 2.1 }), 3);

			WriteResultUnderlinedHeader(grid);

			int rowIndex = 1;
			XPSHelper.WriteTextBlockToGrid(m_ReportData.TestNameText, grid, rowIndex, 0, HorizontalAlignment.Right, VerticalAlignment.Top, new Thickness(0, 5, 0, 0), fontSize, null, FontWeights.Bold);
			XPSHelper.WriteTextBlockToGrid(m_ReportData.TestResultText, grid, rowIndex++, 1, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(20, 5, 0, 0), fontSize, null, FontWeights.Bold);
			XPSHelper.WriteTextBlockToGrid(CommentLabel, grid, rowIndex, 0, HorizontalAlignment.Right, VerticalAlignment.Top, new Thickness(0, 20, 0, 0), fontSize, null, FontWeights.Bold);
			XPSHelper.WriteTextBlockToGrid(m_ReportData.ResultCommentText, grid, rowIndex++, 1, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(20, 20, 5, 0), fontSize, null, FontWeights.Bold, false, true);

			return m_ReportDocument.WriteRowContent(grid);
		}
		#endregion Protected methods

	}
}
