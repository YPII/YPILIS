using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

using YellowstonePathology.Document.Result.Data;
using YellowstonePathology.Document.Xps;

namespace YellowstonePathology.Document.Result.Xps
{
	/// <summary>Cystic Fibrosis Carrier Screening report class
	/// </summary>
	public class CysticFibrosisReport : YpReportBase
	{

		#region Public constants
		/// <summary>report name
		/// </summary>
		public const string ReportName = "Cystic Fibrosis Carrier Screening";
		/// <summary>Mutations Tested section title text
		/// </summary>
		public const string MutationsTestedLabel = "Mutations Tested";
		/// <summary>index of disclaimer text
		/// </summary>
		public static readonly int DisclaimerIndex = 0;
		#endregion Public constants

		#region Private data
		/// <summary>report XML data object
		/// </summary>
		private readonly CysticFibrosisReportData m_ReportData;
		#endregion Private data

		#region Constructors
		/// <summary>constructor with XML input data
		/// </summary>
		/// <param name="reportData">report XML data object</param>
		public CysticFibrosisReport(CysticFibrosisReportData reportData)
			: base(ReportName, reportData.PageHeader)
        {
			m_ReportData = reportData;

			WriteMainBoxSections(m_ReportData.PathologistSignatureText);
			WriteAmendments(m_ReportData.Amendments, true);
			WriteSimpleSectionWithTitle(SpecimenLabel, m_ReportData.SpecimenText);
			WriteSimpleSectionWithTitle(InterpretationLabel, m_ReportData.InterpretationText);
			WriteInterpretationRefTable(reportData);
			WriteSimpleSectionWithTitle(MethodLabel, m_ReportData.MethodText);
			WriteSimpleSectionWithTitle(MutationsTestedLabel, m_ReportData.MutationsTestedText);
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
			const double leftMargin = m_LeftMargin + 18;
			const double topMargin = m_TopMargin + 6;
			const int rowCount = 3;

			Grid grid = XPSHelper.GetGrid(XPSHelper.GetFullWidthGridColWidthArray(new double[] { 0.5, 1.5 }, m_LeftMargin), rowCount);
			grid.Margin = new Thickness(leftMargin, topMargin, 0, 0);

			int rowIndex = 0;
			XPSHelper.WriteTextBlockToGrid(YpReportBase.ResultLabel, grid, rowIndex++, 0, HorizontalAlignment.Left, VerticalAlignment.Top, null, fontSize, null, FontWeights.Bold, false, false, 0, 3);
			XPSHelper.WriteTextBlockToGrid(m_ReportData.TestNameText, grid, rowIndex, 1, HorizontalAlignment.Left, VerticalAlignment.Top, null, fontSize, null, FontWeights.Bold);
			XPSHelper.WriteTextBlockToGrid(m_ReportData.TestResultText, grid, rowIndex++, 2, HorizontalAlignment.Left, VerticalAlignment.Top, null, fontSize, null, FontWeights.Bold);
			XPSHelper.WriteTextBlockToGrid(m_ReportData.ResultCommentText, grid, rowIndex++, 2, HorizontalAlignment.Left, VerticalAlignment.Top, null, fontSize, null, FontWeights.Bold);

			return m_ReportDocument.WriteRowContent(grid);
		}
		#endregion Protected methods

		#region Private methods
		/// <summary>method write interpretation reference table to document grid, if it is needed
		/// </summary>
		/// <param name="reportData">report XML data object</param>
		private void WriteInterpretationRefTable(CysticFibrosisReportData reportData)
		{
			if (reportData.IsInterpretationRefTableVisible)
			{
				int i, j;
				string cellText;
				double fontSize = m_FontSize - 0.5;

				IEnumerable<XElement> colHeaders = reportData.InterpretationRefTableColHeaders;

				int colCount = colHeaders.Count();
				int rowCount = reportData.InterpretationRefTableRowsCount + 1;
				Grid grid = XPSHelper.GetGridWithEqualColumns(rowCount, colCount, 2.0 * m_LeftMargin, m_TopMargin);

				WriteSimpleSection(reportData.InterpretationRefTableHeaderText, fontSize);
				for (j = 0; j < colCount; j++)
				{
					XPSHelper.WriteTextBlockToGrid(colHeaders.ElementAt(j).Value, grid, 0, j, HorizontalAlignment.Left, VerticalAlignment.Top, null, fontSize, null, FontWeights.Bold);
				}
				for (i = 1; i < rowCount; i++)
				{
					for (j = 0; j < colCount; j++)
					{
						cellText = reportData.GetInterpretationRefTableCellText(i - 1, j);
						XPSHelper.WriteTextBlockToGrid(cellText, grid, i, j, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(0, -2, 0, 0), fontSize);
					}
				}
				m_ReportDocument.WriteRowContent(grid);
			}
		}
		#endregion Private methods

	}
}
