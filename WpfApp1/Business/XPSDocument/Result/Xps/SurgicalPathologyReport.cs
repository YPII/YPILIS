using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using YellowstonePathology.Document.Result.Data;
using YellowstonePathology.Document.Xps;

namespace YellowstonePathology.Document.Result.Xps
{
	/// <summary>Surgical Pathology report class
	/// </summary>
	public class SurgicalPathologyReport : YpReportBase
    {
		#region Public constants
		/// <summary>report name
		/// </summary>
		public const string ReportName = "Surgical Pathology Report";
		/// <summary>index of disclaimer text
		/// </summary>
		public static readonly int DisclaimerIndex = -1;
        /// <summary>"Microscopic Description" section title text
        /// </summary>
        public static readonly string MicroscopicDescriptionTitle = "Microscopic Description";
        /// <summary>"Ancillary Studies" section title text
        /// </summary>
        public static readonly string AncillaryStudiesTitle = "Ancillary Studies";
		/// <summary>"Ancillary Studies" table's columns title text
		/// </summary>
		public static readonly string[] AncillaryStudiesColTitles = new string[] {"Test", "Block", "Result"};
		/// <summary>"Clinical Information" section title text
		/// </summary>
		public static readonly string ClinicalInfoTitle = "Clinical Information";
		/// <summary>"Gross Description" section title text
		/// </summary>
		public static readonly string GrossDescriptionTitle = "Gross Description";
		#endregion Public constants

		#region Private data
		/// <summary>report XML data object
		/// </summary>
		private readonly SurgicalPathologyReportData m_ReportData;
		#endregion Private data

		#region Constructors
		/// <summary>constructor with XML input data
		/// </summary>
		/// <param name="reportData">report XML data object</param>
		public SurgicalPathologyReport(SurgicalPathologyReportData reportData)
			: base(ReportName, reportData.PageHeader)
        {
			m_ReportData = reportData;

			WriteMainBoxSections(m_ReportData.PathologistSignatureText);
			WriteAmendments(m_ReportData.Amendments, true);
            WriteSimpleSectionWithTitle(MicroscopicDescriptionTitle, m_ReportData.MicroscopicDescriptionText);
            WriteAncillaryStudiesSection();
			WriteSimpleSectionWithTitle(ClinicalInfoTitle, m_ReportData.ClinicalInfoText);
			WriteSimpleSectionWithTitle(GrossDescriptionTitle, m_ReportData.GrossDescriptionText);
			WriteSimpleSectionWithTitle(OtherReportsLabel, reportData.OtherReportsText);
			if (m_ReportData.IsRevised) WritePrevDiagnosisSection();
			WriteReportDistributionSection(reportData.ReportDistributionList);
			InitPageNumbersInHeaders();
		}
		#endregion Constructors

		#region Protected methods
		/// <summary>method write report's main box custom sections to document grid
		/// </summary>
		protected override int WriteCustomSectionToMainBox()
        {
            const double fontSize = m_FontSize + 2;
            const double titleLeftMargin = 4.0 * m_LeftMargin;
            const double bodyLeftMargin = titleLeftMargin + 4.0 * m_LeftMargin;
            const double topMargin = -1.35;
			const double commentLeftMargin = 9.0;
			const double commentTopMargin = 14.0;
			int startRow = WriteDiagnosisRowTitle(m_ReportData.DiagnosisLabel, fontSize + 1, m_LeftMargin, 5, 4);
            for (int i = 0; i < m_ReportData.DiagnosisRowsCount; i++)
            {
                WriteDiagnosisRowTitle(m_ReportData.GetDiagnosisRowTitle(i), fontSize, titleLeftMargin, topMargin);
				WriteDiagnosisRowBody(m_ReportData.GetDiagnosisRowBody(i), fontSize, bodyLeftMargin, topMargin);
            }
			WriteDiagnosisComment(m_ReportData.DiagnosisCommentText, fontSize, commentLeftMargin, commentTopMargin);
            return startRow;
        }
		#endregion Protected methods

		#region Private methods
		/// <summary>method write diagnosis row title
		/// </summary>
		/// <param name="title">diagnosis row title text</param>
		/// <param name="fontSize">diagnosis row title font size</param>
		/// <param name="leftMargin">diagnosis row title left margin</param>
		/// <param name="topMargin">diagnosis row title top margin</param>
		/// <param name="bottomMargin">diagnosis row title bottom margin</param>
		/// <param name="isUnderlined">if true, then title is underlined</param>
		/// <returns>main box first row index</returns>
		private int WriteDiagnosisRowTitle(string title, double fontSize, double leftMargin, double topMargin, double bottomMargin = 0, bool isUnderlined = false)
        {
			TextBlock text = XPSHelper.GetTextBlock(title, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(leftMargin, topMargin, m_LeftMargin, bottomMargin), fontSize, null, FontWeights.Bold, isUnderlined);
            text.Width = ReportPage.ReportWidth * ReportPage.DisplayResolution - (leftMargin + m_LeftMargin);
            return m_ReportDocument.WriteRowContent(text);
        }
		/// <summary>method write diagnosis row body
		/// </summary>
		/// <param name="body">diagnosis row body text</param>
		/// <param name="fontSize">diagnosis row body font size</param>
		/// <param name="leftMargin">diagnosis row body left margin</param>
		/// <param name="topMargin">diagnosis row body top margin</param>
		private void WriteDiagnosisRowBody(string body, double fontSize, double leftMargin, double topMargin)
        {
            TextBlock text = XPSHelper.GetTextBlock(body, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(leftMargin, topMargin, m_LeftMargin, 0), fontSize, null, null, false, true);
            text.Width = ReportPage.ReportWidth * ReportPage.DisplayResolution - (leftMargin + m_LeftMargin);
            m_ReportDocument.WriteRowContent(text);
        }
		/// <summary>method write diagnosis comment
		/// </summary>
		/// <param name="commentBody">diagnosis comment body text</param>
		/// <param name="fontSize">diagnosis comment font size</param>
		/// <param name="leftMargin">diagnosis comment left margin</param>
		/// <param name="topMargin">diagnosis comment top margin</param>
		private void WriteDiagnosisComment(string commentBody, double fontSize, double leftMargin, double topMargin)
		{
			Grid grid = XPSHelper.GetGrid(XPSHelper.GetFullWidthGridColWidthArray(new double[] { 0.8 }), 1, 0, new Thickness(leftMargin, topMargin, 0, 0));
			XPSHelper.WriteTextBlockToGrid(CommentLabel, grid, 0, 0, HorizontalAlignment.Left, VerticalAlignment.Top, null, fontSize, null, FontWeights.Bold);
			XPSHelper.WriteTextBlockToGrid(commentBody, grid, 0, 1, HorizontalAlignment.Left, VerticalAlignment.Top, null, fontSize, null, null, false, true);
			m_ReportDocument.WriteRowContent(grid);
		}
		/// <summary>method write "Ancillary Studies" section
		/// </summary>
        private void WriteAncillaryStudiesSection()
        {
			WriteSimpleSectionWithTitle(AncillaryStudiesTitle, m_ReportData.AncillaryStudiesHeaderText);

            int rowsCount = m_ReportData.AncillaryStudiesCount;
            Grid grid = XPSHelper.GetGrid(XPSHelper.GetFullWidthGridColWidthArray(new double[] { 0.4, 2.1, 1.2 }), rowsCount * 3);
            for (int i = 0; i < rowsCount; i++)
            {
                WriteAncillaryStudyRow(grid, i);
            }
            m_ReportDocument.WriteRowContent(grid);
        }
		/// <summary>method write "Ancillary Studies" section's table row
		/// </summary>
		/// <param name="grid">"Ancillary Studies" section's table grid</param>
		/// <param name="rowIndex">"Ancillary Studies" section's table row index</param>
        private void WriteAncillaryStudyRow(Grid grid, int rowIndex)
        {
			int colCount = AncillaryStudiesColTitles.GetLength(0);
			int baseRowIndex = rowIndex * 3;
			XPSHelper.WriteTextBlockToGrid(m_ReportData.GetAncillaryStudyTitle(rowIndex), grid, baseRowIndex, 0, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(17, 7, 0, 0), m_FontSize + 1, null, FontWeights.Bold, false, false, 0, grid.ColumnDefinitions.Count);
			for (int i = 0; i < colCount; i++)
			{
				XPSHelper.WriteTextBlockToGrid(AncillaryStudiesColTitles[i], grid, baseRowIndex + 1, i + 1, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(0, -2, 0, 0), m_FontSize + 1, null, FontWeights.Bold, true);
				XPSHelper.WriteTextBlockToGrid(m_ReportData.GetAncillaryStudyCellValue(rowIndex, i), grid, baseRowIndex + 2, i + 1, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(0, -2, 0, 2), m_FontSize);
			}
		}
		/// <summary>method write "Previous Diagnosis" sections to document grid
		/// </summary>
		private int WritePrevDiagnosisSection()
		{
			const double fontSize = m_FontSize;
			const double titleLeftMargin = m_LeftMargin;
			const double bodyLeftMargin = titleLeftMargin + 4.0 * m_LeftMargin;
			const double topMargin = -2;
			const double commentLeftMargin = 9.0;
			const double commentTopMargin = 0;
			int startRow = WriteDiagnosisRowTitle(m_ReportData.PrevDiagnosisLabel, fontSize + 1, m_LeftMargin, 5, 0, true);
			for (int i = 0; i < m_ReportData.PrevDiagnosisRowsCount; i++)
			{
				WriteDiagnosisRowTitle(m_ReportData.GetPrevDiagnosisRowTitle(i), fontSize, titleLeftMargin, topMargin);
				WriteDiagnosisRowBody(m_ReportData.GetPrevDiagnosisRowBody(i), fontSize, bodyLeftMargin, topMargin);
			}
			WriteDiagnosisComment(m_ReportData.PrevDiagnosisCommentText, fontSize, commentLeftMargin, commentTopMargin);
			m_ReportDocument.WriteRowContent(XPSHelper.GetTextBlock(m_ReportData.PathologistSignatureText, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(m_SignLeftMargin, 0, 0, 0), m_FontSize));
			return startRow;
		}
		#endregion Private methods
	}
}
