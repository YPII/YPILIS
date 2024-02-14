using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Xml.Linq;

using YellowstonePathology.Document.Result.Data;
using YellowstonePathology.Document.Xps;

namespace YellowstonePathology.Document.Result.Xps
{
	public class YpReportBase
	{

		#region Private constants
		/// <summary>Electronic signature label text for main box and amendment boxes
		/// </summary>
		private const string m_ElectronicSignatureLabelText = "*** Electronic Signature ***";
		#endregion Private constants

		#region Protected constants
		/// <summary>base font size
		/// </summary>
		protected const double m_FontSize = 10.0;
		/// <summary>base left margin
		/// </summary>
		protected const double m_LeftMargin = 7.0;
		/// <summary>base top margin
		/// </summary>
		protected const double m_TopMargin = 10.0;
		/// <summary>gap between label and text with vertical orientation
		/// </summary>
		protected const double m_VerticalGap = 2.0;
		/// <summary>Signature label left margin for main box and amendment boxes
		/// </summary>
		protected const int m_SignLeftMargin = 500;
		/// <summary>Signature label right margin for main box and amendment boxes
		/// </summary>
		protected const int m_SignRightMargin = 10;
		#endregion Protected constants

		#region Public constants
		/// <summary>main box's result label
		/// </summary>
		public static readonly string ResultLabel = "Result:";
		/// <summary>main box's сomment label
		/// </summary>
		public static readonly string CommentLabel = "Comment:";
		/// <summary>Specimen section title text
		/// </summary>
		public static readonly string SpecimenLabel = "Specimen";
		/// <summary>Indication section title text
		/// </summary>
		public static readonly string IndicationLabel = "Indication";
		/// <summary>Interpretation section title text
		/// </summary>
		public static readonly string InterpretationLabel = "Interpretation";
		/// <summary>Method section title text
		/// </summary>
		public static readonly string MethodLabel = "Method";
		/// <summary>References section title text
		/// </summary>
		public static readonly string ReferencesLabel = "References";
		/// <summary>Other Yellowstone Pathology Institute Reports section header text
		/// </summary>
		public static readonly string OtherReportsLabel = "Other Yellowstone Pathology Institute Reports";
		/// <summary>Report Distribution section header text
		/// </summary>
		public static readonly string ReportDistributionLabel = "Report Distribution";
		/// <summary>array of main box table's columns headers
		/// </summary>
		public static readonly string[] ResultColumnsHeaders = new string[] { "Test", "Result", "Reference" };
		#endregion Public constants

		#region Private data
		/// <summary>page header data
		/// </summary>
		private readonly ReportHeaderData m_PageHeaderData;
		/// <summary>"Use pathologist signature in main report box" flag
		/// </summary>
		private readonly bool m_UsePathologistSignatureInMainBox;
		#endregion Private data

		#region Protected data
		/// <summary>report document object
		/// </summary>
		protected ReportDocument m_ReportDocument;
		#endregion Protected data

		#region Constructors
		public YpReportBase(string reportName, ReportHeaderData pageHeader, bool usePathologistSignatureInMainBox = true)
		{
			m_PageHeaderData = pageHeader;
			m_UsePathologistSignatureInMainBox = usePathologistSignatureInMainBox;
			SetupReport(reportName);
		}
		#endregion Constructors

		#region Public properties
		/// <summary>report's underlying FixedDocument object
		/// </summary>
		public FixedDocument FixedDocument
		{
			get { return m_ReportDocument.FixedDocument; }
		}
		#endregion Public properties

		#region Protected methods
		/// <summary>method write custom sections in report's main box
		/// </summary>
		/// <returns>top row index of report's main box</returns>
		protected virtual int WriteCustomSectionToMainBox()
		{
			return -1;
		}
		/// <summary>method write report's main box sections (custom sections and signature section)
		/// </summary>
		/// <param name="pathologistSignature">pathologist signature string</param>
		protected void WriteMainBoxSections(string pathologistSignature)
		{
			int mainBoxStartRow, mainBoxEndRow;

			mainBoxStartRow = WriteCustomSectionToMainBox();
			if (mainBoxStartRow > -1)
			{
				mainBoxEndRow = WriteMainBoxSignature(pathologistSignature);
				WriteBoxBorder(mainBoxStartRow, mainBoxEndRow);
			}
		}
		/// <summary>method write report's amendments boxes
		/// </summary>
		/// <param name="amendments">array of XML elements with amendments parameters</param>
		/// <param name="haveMainBox">if true, then report have main box</param>
		/// <param name="useRevisedDiagnosis">if true, then RevisedDiagnosis flag is used while amendments section is writed</param>
		protected void WriteAmendments(IEnumerable<XElement> amendments, bool haveMainBox)
		{
			XElement amendmentData;
			if (amendments != null)
			{
				for (int i = 0; i < amendments.Count(); i++)
				{
					amendmentData = amendments.ElementAt(i);
					WriteAmendmentBox(amendments.ElementAt(i), (!haveMainBox && i == 0));
				}
			}
		}
		/// <summary>method write standard trailer sectiohs ("OtherReports", "ReportDistribution", "Disclaimer") and initialize page numbers in next page's headers
		/// </summary>
		/// <param name="otherReports">string for "Other report" section</param>
		/// <param name="reportDistributions">XML elements list for "ReportDistribution" section</param>
		/// <param name="disclaimerIndex">index of disclaimer text in disclaimer text array</param>
		protected void WriteStandardTrailerSections(string otherReports, IEnumerable<XElement> reportDistributions, int disclaimerIndex)
		{
			WriteSimpleSectionWithTitle(OtherReportsLabel, otherReports);
			WriteReportDistributionSection(reportDistributions);
			WriteDisclaimerSection(disclaimerIndex);
			InitPageNumbersInHeaders();
		}
		/// <summary>method write section title to document grid
		/// </summary>
		/// <param name="text">section title text</param>
		/// <param name="fontSize">font size</param>
		protected void WriteSectionTitle(string text, double fontSize = -1)
		{
			TextBlock title = new TextBlock() { Text = text, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(m_LeftMargin, m_TopMargin, 0, 0), FontSize = (fontSize == -1 ? m_FontSize : fontSize), FontWeight = FontWeights.Bold, TextDecorations = new TextDecorationCollection() };
			title.TextDecorations.Add(TextDecorations.Underline);
			m_ReportDocument.WriteRowContent(title);
		}
		/// <summary>method write section title to parent grid
		/// </summary>
		/// <param name="text">section title text</param>
		/// <param name="parentGrid">parent grid</param>
		/// <param name="rowNo">TextBlock cell row number in parent grid</param>
		/// <param name="colNo">TextBlock cell column number in parent grid</param>
		/// <param name="fontSize">font size</param>
		protected static void WriteSectionTitle(string text, Grid parentGrid, int rowIndex, int colIndex, double fontSize = -1)
		{
            XPSHelper.WriteTextBlockToGrid(text, parentGrid, rowIndex, colIndex, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(0, m_TopMargin, 0, 0), (fontSize == -1 ? m_FontSize : fontSize), null, FontWeights.Bold, true);
		}
		/// <summary>>method write section row (label/value block) to document grid
		/// </summary>
		/// <param name="labelText">label text</param>
		/// <param name="valueText">value text</param>
		/// <param name="topMargin">top margin of "Cell Population Of Interest" section</param>
		protected void WriteSectionRow(string labelText, string valueText, double topMargin = 0)
		{
			double[] colWidth = new double[] { 1.75, 5.0 };

			Grid grid = XPSHelper.GetGrid(colWidth, 1);
			grid.Margin = new Thickness(m_LeftMargin, topMargin, 0, 0);

			TextBlock label = XPSHelper.WriteTextBlockToGrid(labelText, grid, 0, 0, HorizontalAlignment.Left, VerticalAlignment.Top, null, m_FontSize);
			label.FontWeight = FontWeights.Bold;
			XPSHelper.WriteTextBlockToGrid(valueText, grid, 0, 1, HorizontalAlignment.Left, VerticalAlignment.Top, null, m_FontSize);
			m_ReportDocument.WriteRowContent(grid);
		}
		/// <summary>>method write section row (label/value block) to parent grid
		/// </summary>
		/// <param name="labelText">label text</param>
		/// <param name="valueText">value text</param>
		/// <param name="parentGrid">parent grid</param>
		/// <param name="rowNo">TextBlock cell row number in parent grid</param>
		/// <param name="colNo">TextBlock cell column number in parent grid</param>
		/// <param name="topMargin">top margin of "Cell Population Of Interest" section</param>
		protected static void WriteSectionRow(string labelText, string valueText, Grid parentGrid, double[] colWidth, int rowIndex, int colIndex, double topMargin = 0)
		{
			Grid grid = XPSHelper.GetGrid(colWidth, 1);
			grid.Margin = new Thickness(0, topMargin, 0, 0);

			TextBlock label = XPSHelper.WriteTextBlockToGrid(labelText, grid, 0, 0, HorizontalAlignment.Left, VerticalAlignment.Top, null, m_FontSize);
			label.FontWeight = FontWeights.Bold;
			XPSHelper.WriteTextBlockToGrid(valueText, grid, 0, 1, HorizontalAlignment.Left, VerticalAlignment.Top, null, m_FontSize);
			XPSHelper.WriteItemToGrid(grid, parentGrid, rowIndex, colIndex);
		}
		/// <summary>method simple section (section with width over all page and with text wrapping) to document grid
		/// </summary>
		/// <param name="bodyText">section's body text</param>
		/// <param name="fontSize">font size</param>
		/// <param name="horMargin">horizontal (left/right) margins value, by default is equals report default left margin</param>
		/// <param name="topMargin">top margin, by default is equals 0</param>
		/// <returns>document grid row's index with this section</returns>
		protected int WriteSimpleSection(string bodyText, double fontSize = -1, double horMargin = double.MinValue, double topMargin = 0)
		{
			if (horMargin == double.MinValue) horMargin = m_LeftMargin;
			TextBlock text = XPSHelper.GetTextBlock(bodyText, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(horMargin, topMargin, horMargin, 0), (fontSize == -1 ? m_FontSize : fontSize), null, null, false, true);
			text.Width = ReportPage.ReportWidth * ReportPage.DisplayResolution - 2.0 * horMargin;
			return m_ReportDocument.WriteRowContent(text);
		}
		/// <summary>method simple section (section with width over all page and with text wrapping) with standard title to document grid
		/// </summary>
		/// <param name="title">section's title</param>
		/// <param name="bodyText">section's body text</param>
		/// <param name="fontSize">font size</param>
		/// <param name="horMargin">horizontal (left/right) margins value, by default is equals report default left margin</param>
		/// <param name="topMargin">top margin, by default is equals 0</param>
		/// <returns>document grid row's index with this section</returns>
		protected int WriteSimpleSectionWithTitle(string title, string bodyText, double fontSize = -1, double horMargin = 0, double topMargin = 0)
		{
			TextBlock sectionTitle = XPSHelper.GetTextBlock(title, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(horMargin, topMargin, horMargin, 0), (fontSize == -1 ? m_FontSize : fontSize), null, FontWeights.Bold, true);
			TextBlock sectionText = XPSHelper.GetTextBlock(bodyText, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(horMargin, 0, horMargin, 0), (fontSize == -1 ? m_FontSize : fontSize), null, null, false, true);
			sectionText.Width = ReportPage.ReportWidth * ReportPage.DisplayResolution - 2.0 * horMargin;

			StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Vertical, Margin = new Thickness(m_LeftMargin, m_TopMargin, 0, 0) };
			stackPanel.Children.Add(sectionTitle);
			stackPanel.Children.Add(sectionText);
			return m_ReportDocument.WriteRowContent(stackPanel);
		}
		/// <summary>method "Result" underlined header to main box grid
		/// </summary>
		/// <param name="grid">main box grid</param>
		/// <param name="colCount">columns count in main box grid</param>
		protected void WriteResultUnderlinedHeader(Grid grid)
		{
			int colCount = grid.ColumnDefinitions.Count;
			Border root = XPSHelper.WriteUnderliningToGridCell(grid, 0, 0, null, 0.5, 0, colCount);
			TextBlock title = XPSHelper.GetTextBlock(ResultLabel, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(m_LeftMargin + 12, 4, 0, 4), m_FontSize + 2, null, FontWeights.Bold);
			root.Child = title;
		}
		/// <summary>method write "Report Distribution" section to document grid
		/// </summary>
		protected void WriteReportDistributionSection(IEnumerable<XElement> reportDistributions)
		{
			double[] colWidth = new double[] { ReportPage.ReportWidth / 2 - (2.0 * m_LeftMargin / ReportPage.DisplayResolution) };
			int rowCount = reportDistributions.Count();

			TextBlock sectionTitle = XPSHelper.GetTextBlock(ReportDistributionLabel, HorizontalAlignment.Left, VerticalAlignment.Top, null, m_FontSize, null, FontWeights.Bold, true);
			Grid grid = XPSHelper.GetGrid(colWidth, rowCount);
			for (int i = 0; i < rowCount; i++)
			{
				XPSHelper.WriteTextBlockToGrid(reportDistributions.ElementAt(i).Value, grid, i, 0, HorizontalAlignment.Left, VerticalAlignment.Top, null, m_FontSize);
			}
			StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Vertical, Margin = new Thickness(m_LeftMargin, m_TopMargin, 0, 0) };
			stackPanel.Children.Add(sectionTitle);
			stackPanel.Children.Add(grid);
			m_ReportDocument.WriteRowContent(stackPanel);
		}
		/// <summary>method write "Disclaimer" section to document grid
		/// </summary>
		/// <param name="disclaimerIndex">index of disclaimer text in disclaimer text array</param>
		protected void WriteDisclaimerSection(int disclaimerIndex)
		{
			if (disclaimerIndex > -1)
			{
				TextBlock text = XPSHelper.GetTextBlock(YpReportDataBase.DisclaimerText[disclaimerIndex], HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(m_LeftMargin, 2.0 * m_TopMargin, 0, 0), m_FontSize - 5, null, null, false, true);
				text.Width = ReportPage.ReportWidth * ReportPage.DisplayResolution - 2 * m_LeftMargin;
				text.FontStyle = FontStyles.Italic;
				m_ReportDocument.WriteRowContent(text);
			}
		}
		/// <summary>method write page numbers to header label, starting from second page
		/// </summary>
		protected void InitPageNumbersInHeaders()
		{
			int totalPagesCount = m_ReportDocument.FixedDocument.Pages.Count;
			for (int i = 1; i < totalPagesCount; i++)
			{
				SetHeaderWithPageNumber(i, totalPagesCount);
			}
		}
		#endregion Protected methods

		#region Private methods
		/// <summary>method initilize report with headers and footer
		/// </summary>
		private void SetupReport(string reportName)
		{
			ReportFirstPageHeader firstPageHeader = new ReportFirstPageHeader(reportName, m_PageHeaderData);
			ReportNextPageHeader nextPageHeader = new ReportNextPageHeader(m_PageHeaderData);
			ReportFooter footer = new ReportFooter();
			m_ReportDocument = new Document.Xps.ReportDocument(firstPageHeader, footer, nextPageHeader);
		}
		/// <summary>method write main and amendment box's border to document grid
		/// </summary>
		/// <param name="boxStartRow">document grid start row index</param>
		/// <param name="boxEndRow">document grid end row index</param>
		/// <param name="haveTop">if true, then border have top edge</param>
		private void WriteBoxBorder(int boxStartRow, int boxEndRow, bool haveTop = true)
		{
			Border border = new Border()
			{
				BorderBrush = Brushes.Black,
				BorderThickness = new Thickness(1, haveTop ? 1 : 0, 1, 1)
			};
			Grid.SetColumn(border, 0);
			Grid.SetRow(border, boxStartRow);
			if (boxEndRow > boxStartRow) Grid.SetRowSpan(border, boxEndRow - boxStartRow);
			m_ReportDocument.WriteBorder(border);
		}
		/// <summary>method write main box "Signature" section to document grid
		/// </summary>
		private int WriteMainBoxSignature(string pathologistSignature)
		{
			if (m_UsePathologistSignatureInMainBox)
			{
				string signature = m_UsePathologistSignatureInMainBox ? (string.IsNullOrEmpty(m_PageHeaderData.GetStringValue("FinalTime")) ? " " : pathologistSignature) : string.Empty;
				TextBlock sign = XPSHelper.GetTextBlock(signature, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(m_SignLeftMargin, m_TopMargin, m_SignRightMargin, m_VerticalGap), m_FontSize + 2);
				m_ReportDocument.WriteRowContent(sign);
			}
			TextBlock esign = XPSHelper.GetTextBlock(m_UsePathologistSignatureInMainBox ? m_ElectronicSignatureLabelText : string.Empty, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(m_SignLeftMargin + 20, 0, m_SignRightMargin, m_VerticalGap + m_TopMargin), m_FontSize - 2);
			return m_ReportDocument.WriteRowContent(esign) + 1;
		}
		/// <summary>method write report's amendment sections
		/// </summary>
		/// <param name="amendment">XML element with amendment parameters</param>
		/// <param name="haveTop">if true, then amendment box have top edge</param>
		private void WriteAmendmentBox(XElement amendment, bool haveTop)
		{
			Grid grid = XPSHelper.GetGrid(2, 1, ReportPage.ReportWidth - 2.0 * m_LeftMargin / ReportPage.DisplayResolution);
			int rowIndex = 0;
			string title = string.Format("{0}: {1}", amendment.Element(YpReportDataBase.AmendmentType).Value, amendment.GetDateTime(YpReportDataBase.AmendmentTime, "MM/dd/yyyy - HH:mm"));
			XPSHelper.WriteTextBlockToGrid(title, grid, rowIndex++, 0, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(m_LeftMargin + 6, 5, 0, 0), m_FontSize + 2, null, FontWeights.Bold);
			XPSHelper.WriteTextBlockToGrid(amendment.Element(YpReportDataBase.AmendmentText).Value, grid, rowIndex++, 0, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(m_LeftMargin + 6, 2, 0, 0), m_FontSize + 2, null, null, false, true);
			int startRow = m_ReportDocument.WriteRowContent(grid);

			TextBlock sign = XPSHelper.GetTextBlock(amendment.Element(YpReportDataBase.AmendmentPathologistSignature).Value, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(m_SignLeftMargin, m_TopMargin, m_SignRightMargin, m_VerticalGap), m_FontSize + 2);
			m_ReportDocument.WriteRowContent(sign);
			TextBlock esign = XPSHelper.GetTextBlock(m_ElectronicSignatureLabelText, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(m_SignLeftMargin + 20, 0, m_SignRightMargin, m_VerticalGap + m_TopMargin), m_FontSize - 2);
			int endRow = m_ReportDocument.WriteRowContent(esign) + 1;
			WriteBoxBorder(startRow, endRow, haveTop);
		}
		/// <summary>method write page numbers to header label in format "Page {PageNo} of {TotalPagesCount}"
		/// </summary>
		/// <param name="pageIndex">page index</param>
		/// <param name="totalPagesCount">total pages count of report</param>
		private void SetHeaderWithPageNumber(int pageIndex, int totalPagesCount)
		{
			const int headerGridIndex = 0;
			const int pagesTitleBorderIndex = 1;

			Grid header = (Grid)(m_ReportDocument.FixedDocument.Pages[pageIndex].Child.Children[headerGridIndex]);
			Border pagesTitleBorder = (Border)(header.Children[pagesTitleBorderIndex]);
			TextBlock pagesTitle = (TextBlock)(pagesTitleBorder.Child);
			pagesTitle.Text = string.Format("Page {0} of {1}", pageIndex + 1, totalPagesCount);
		}
		#endregion Private methods

	}
}
