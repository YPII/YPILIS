using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using YellowstonePathology.Document.Result.Data;
using YellowstonePathology.Document.Xps;

namespace YellowstonePathology.Document.Result.Xps
{
	/// <summary>Chlamydia Gonorrhea Screening report class
	/// </summary>
	public class NgctReport : YpReportBase
	{

		#region Public constants
		/// <summary>report name
		/// </summary>
		public const string ReportName = "Chlamydia Gonorrhea Screening";
		/// <summary>index of disclaimer text
		/// </summary>
		public static readonly int DisclaimerIndex = 2;
		#endregion Public constants

		#region Private data
		/// <summary>report XML data object
		/// </summary>
		private readonly NgctReportData m_ReportData;
		#endregion Private data

		#region Constructors
		/// <summary>constructor with XML input data
		/// </summary>
		/// <param name="reportData">report XML data object</param>
		public NgctReport(NgctReportData reportData)
			: base(ReportName, reportData.PageHeader, false)
        {
			m_ReportData = reportData;

			WriteMainBoxSections(string.Empty);
			WriteAmendments(m_ReportData.Amendments, true);
			WriteSimpleSectionWithTitle(SpecimenLabel, m_ReportData.SpecimenText);
			WriteSimpleSectionWithTitle(MethodLabel, m_ReportData.MethodText);

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

			int rowsCount = m_ReportData.ResultRowsCount + 1;
			Grid grid = XPSHelper.GetGrid(XPSHelper.GetFullWidthGridColWidthArray(new double[] { 0.4, 1.8, 2.5 }, m_LeftMargin), rowsCount);
			grid.Margin = new Thickness(leftMargin, topMargin, 0, 0);

			for (int i = 0; i < rowsCount; i++)
			{
				for (int j = 0; j < ResultColumnsHeaders.GetLength(0); j++)
				{
					if(i == 0)
						XPSHelper.WriteTextBlockToGrid(ResultColumnsHeaders[j], grid, i, j + 1, HorizontalAlignment.Left, VerticalAlignment.Top, null, fontSize, null, FontWeights.Bold, true);
					else
						XPSHelper.WriteTextBlockToGrid(m_ReportData.GetResultCellValue(i - 1, j), grid, i, j + 1, HorizontalAlignment.Left, VerticalAlignment.Top, null, fontSize);
				}
			}
			return m_ReportDocument.WriteRowContent(grid);
		}
		#endregion Protected methods

	}
}
