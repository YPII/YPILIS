using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using YellowstonePathology.Document.Result.Data;
using YellowstonePathology.Document.Xps;

namespace YellowstonePathology.Document.Result.Xps
{
	/// <summary>HER2 Amplification report class
	/// </summary>
	public class Her2AmplificationReport : YpReportBase
	{

		#region Public constants
		/// <summary>report name
		/// </summary>
		public const string ReportName = "HER2 Amplification";
		/// <summary>index of disclaimer text
		/// </summary>
		public static readonly int DisclaimerIndex = 1;
		/// <summary>"Result" field text
		/// </summary>
		public new static readonly string ResultLabel = "Result";
		/// <summary>test name label text
		/// </summary>
		public static readonly string TestNameText = "HER2:";
		/// <summary>reference table title text
		/// </summary>
		public static readonly string ReferenceTitle = "Reference:";
		/// <summary>reference table rows text
		/// </summary>
		public static readonly string[,] ReferenceRows = 
		{
			{ "Negative", "<", "1.8" },
			{ "Equivocal", "=", "1.8 – 2.2" },
			{ "Positive", ">", "2.2" }
		};
		/// <summary>"Result Data" section title text
		/// </summary>
		public static readonly string ResultDataTitle = "Result Data";
		/// <summary>"Result Data" sections's labels array
		/// </summary>
		public static readonly string[] ResultLabels = new string[] { "Number of invasive tumor cells counted:", "Number of observers:", "HER2 average copy number per nucleus:", "Chr17 average copy number per nucleus:", "Ratio of average HER2/Chr17 signals:" };
		/// <summary>"Specimen Information" section's title text
		/// </summary>
		public static readonly string SpecimenTitle = "Specimen Information";
		/// <summary>"Specimen Information" sections's labels array
		/// </summary>
		public static readonly string[] SpecimenLabels = new string[] { "Specimen site and type:", "Specimen fixation type:", "Time to fixation:", "Duration of fixation:", "Sample adequacy:" };

		#endregion Public constants

		#region Private data
		/// <summary>report XML data object
		/// </summary>
		private readonly Her2AmplificationReportData m_ReportData;
		#endregion Private data

		#region Constructors
		/// <summary>constructor with XML input data
		/// </summary>
		/// <param name="reportData">report XML data object</param>
		public Her2AmplificationReport(Her2AmplificationReportData reportData)
			: base(ReportName, reportData.PageHeader)
        {
			m_ReportData = reportData;

			WriteMainBoxSections(m_ReportData.PathologistSignatureText);
			WriteAmendments(m_ReportData.Amendments, true);
			WriteResultDataAndSpecimenInformation();
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

			Thickness rowMargin = new Thickness(0, -3, 0, 0);
			Grid grid = XPSHelper.GetGrid(XPSHelper.GetFullWidthGridColWidthArray(new double[] { 0.7, 0.55, 4.0, 0.8, 0.3 }), 5);

			WriteResultUnderlinedHeader(grid);

			int rowIndex = 2;
			XPSHelper.WriteTextBlockToGrid(TestNameText, grid, rowIndex, 1, HorizontalAlignment.Left, VerticalAlignment.Top, null, fontSize, null, FontWeights.Bold);
			XPSHelper.WriteTextBlockToGrid(m_ReportData.TestResultText, grid, rowIndex++, 2, HorizontalAlignment.Left, VerticalAlignment.Top, null, fontSize, null, FontWeights.Bold);
			XPSHelper.WriteTextBlockToGrid(m_ReportData.TestResultValue, grid, rowIndex++, 2, HorizontalAlignment.Left, VerticalAlignment.Top, rowMargin, fontSize, null, FontWeights.Bold);

			WriteReferenceTableSectionToMainBox(grid, fontSize, rowMargin);

			return m_ReportDocument.WriteRowContent(grid);
		}
		#endregion Protected methods

		#region Private methods
		/// <summary>method write report's main box reference table to document grid
		/// </summary>
		private void WriteReferenceTableSectionToMainBox(Grid grid, double fontSize, Thickness rowMargin)
		{
			XPSHelper.WriteTextBlockToGrid(ReferenceTitle, grid, 1, 3, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(10, 15, 0, 0), fontSize, null, FontWeights.Bold, false, false, 0, 3);
			for (int i = 0; i < ReferenceRows.GetLength(0); i++)
			{
				for (int j = 0; j < ReferenceRows.GetLength(1); j++)
				{
					XPSHelper.WriteTextBlockToGrid(ReferenceRows[i, j], grid, i + 2, j + 3, HorizontalAlignment.Left, VerticalAlignment.Top, GetReferenceTableRowMargin(i, rowMargin), fontSize);
				}
				
			}
		}
		/// <summary>method return margin object for report's main box reference table row
		/// 
		/// </summary>
		/// <param name="rowIndex">row index</param>
		/// <param name="rowMargin">row margin for middle rows</param>
		/// <returns></returns>
		private static Thickness? GetReferenceTableRowMargin(int rowIndex, Thickness rowMargin)
		{
			switch (rowIndex)
			{
				case 1:
					return rowMargin;
				case 2:
					return new Thickness(0, -3, 0, 10);
				default:
					return null;
			}
		}
		/// <summary>method write "Result Data" and "Specimen Information" sections to document grid
		/// </summary>
		private void WriteResultDataAndSpecimenInformation()
		{
			const double leftMargin = m_LeftMargin;
			const double topMargin = m_TopMargin;

			double[] colWidth = XPSHelper.GetFullWidthGridColWidthArray(new double[] { 3.4 }, leftMargin);
			Grid grid = XPSHelper.GetGrid(colWidth, 1);
			grid.Margin = new Thickness(leftMargin, topMargin, 0, 0);

			XPSHelper.WriteItemToGrid(WriteResultData(), grid, 0, 0);
			XPSHelper.WriteItemToGrid(WriteSpecimenInformation(), grid, 0, 1);
			m_ReportDocument.WriteRowContent(grid);
		}
		/// <summary>method return "Result Data" section grid
		/// </summary>
		private Grid WriteResultData()
		{
			double[] colWidth = new double[] { 2.1, 1.3 };
			Thickness margin = new Thickness(10, 0, 0, 0);
			int rowCount = ResultLabels.GetLength(0);

			Grid grid = XPSHelper.GetGrid(colWidth, rowCount + 1);

			int rowIndex = 0;
			XPSHelper.WriteTextBlockToGrid(ResultDataTitle, grid, rowIndex++, 0, HorizontalAlignment.Center, VerticalAlignment.Top, null, m_FontSize, null, FontWeights.Bold, true, false, 0, 2);
			for (int i = 0; i < rowCount; i++)
			{
				XPSHelper.WriteTextBlockToGrid(ResultLabels[i], grid, rowIndex, 0, HorizontalAlignment.Right, VerticalAlignment.Top, null, m_FontSize);
				XPSHelper.WriteTextBlockToGrid(m_ReportData.GetResultValue(i), grid, rowIndex++, 1, HorizontalAlignment.Left, VerticalAlignment.Top, margin, m_FontSize);
			}
			return grid;
		}
		/// <summary>method return "Specimen Information" section grid
		/// </summary>
		private Grid WriteSpecimenInformation()
		{
			double[] colWidth = new double[] { 1.3, 2.5 };
			Thickness margin = new Thickness(10, 0, 0, 0);
			int rowCount = SpecimenLabels.GetLength(0);

			Grid grid = XPSHelper.GetGrid(colWidth, rowCount + 1);

			int rowIndex = 0;
			XPSHelper.WriteTextBlockToGrid(SpecimenTitle, grid, rowIndex++, 0, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(80, 0, 0, 0), m_FontSize, null, FontWeights.Bold, true, false, 0, 2);
			for (int i = 0; i < rowCount; i++)
			{
				XPSHelper.WriteTextBlockToGrid(SpecimenLabels[i], grid, rowIndex, 0, HorizontalAlignment.Right, VerticalAlignment.Top, null, m_FontSize);
				XPSHelper.WriteTextBlockToGrid(m_ReportData.GetSpecimenValue(i), grid, rowIndex++, 1, HorizontalAlignment.Left, VerticalAlignment.Top, margin, m_FontSize);
			}
			return grid;
		}
		#endregion Private methods

	}
}
