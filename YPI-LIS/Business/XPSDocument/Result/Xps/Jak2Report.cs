using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using YellowstonePathology.Document.Result.Data;
using YellowstonePathology.Document.Xps;

namespace YellowstonePathology.Document.Result.Xps
{
	/// <summary>JAK2 Mutation V617F report class
	/// </summary>
	public class Jak2Report : YpReportBase
	{

		#region Public constants
		/// <summary>report name
		/// </summary>
		public static readonly string ReportName = "JAK2 Mutation V617F";
		/// <summary>index of disclaimer text
		/// </summary>
		public static readonly int DisclaimerIndex = 2;
		#endregion Public constants

		#region Private data
		/// <summary>report XML data object
		/// </summary>
		private readonly Jak2ReportData m_ReportData;
		#endregion Private data

		#region Constructors
		/// <summary>constructor with XML input data
		/// </summary>
		/// <param name="reportData">report XML data object</param>
		public Jak2Report(Jak2ReportData reportData)
			: base(ReportName, reportData.PageHeader)
        {
			m_ReportData = reportData;

			WriteMainBoxSections(m_ReportData.PathologistSignatureText);
			WriteAmendments(m_ReportData.Amendments, true);
			WriteSimpleSectionWithTitle(SpecimenLabel, m_ReportData.SpecimenText);
			WriteSimpleSectionWithTitle(InterpretationLabel, m_ReportData.InterpretationText);
			WriteSimpleSectionWithTitle(MethodLabel, m_ReportData.MethodText);
			WriteSimpleSectionWithTitle(ReferencesLabel, m_ReportData.ReferencesText);

			WriteStandardTrailerSections(m_ReportData.OtherReportsText, m_ReportData.ReportDistributionList, DisclaimerIndex);
		}
		#endregion Constructors

		#region Protected methods
		/// <summary>method write report's main box custom sections to document grid
		/// </summary>
		protected override int WriteCustomSectionToMainBox()
		{
			const double fontSize = m_FontSize + 2;
			const double leftMargin = m_LeftMargin + 18;
			const double topMargin = m_TopMargin + 6;
			const int rowCount = 3;

			Grid grid = XPSHelper.GetGrid(XPSHelper.GetFullWidthGridColWidthArray(new double[] { 1.0, 1.1 }, m_LeftMargin), rowCount);
			grid.Margin = new Thickness(leftMargin, topMargin, 0, 0);

			int rowIndex = 0;

			XPSHelper.WriteTextBlockToGrid(YpReportBase.ResultLabel, grid, rowIndex++, 0, HorizontalAlignment.Left, VerticalAlignment.Top, null, fontSize, null, FontWeights.Bold, false, false, 0, 3);
			XPSHelper.WriteTextBlockToGrid(m_ReportData.TestNameText, grid, rowIndex, 1, HorizontalAlignment.Left, VerticalAlignment.Top, null, fontSize, null, FontWeights.Bold);
			XPSHelper.WriteTextBlockToGrid(m_ReportData.TestResultText, grid, rowIndex++, 2, HorizontalAlignment.Left, VerticalAlignment.Top, null, fontSize, null, FontWeights.Bold);
			XPSHelper.WriteTextBlockToGrid(m_ReportData.ResultCommentText, grid, rowIndex++, 2, HorizontalAlignment.Left, VerticalAlignment.Top, null, fontSize, null, FontWeights.Bold);

			return m_ReportDocument.WriteRowContent(grid);
		}
		#endregion Protected methods

	}
}
