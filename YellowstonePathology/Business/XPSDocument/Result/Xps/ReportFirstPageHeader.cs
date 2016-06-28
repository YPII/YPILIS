using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Reflection;

using YellowstonePathology.Document.Result.Data;
using YellowstonePathology.Document.Xps;

namespace YellowstonePathology.Document.Result.Xps
{
	/// <summary>Report first page header class
	/// </summary>
	public class ReportFirstPageHeader : HeaderFooterBase
    {

		#region Private data
		/// <summary>block of report header data
		/// </summary>
		private readonly ReportHeaderData m_Data;
		/// <summary>report name
		/// </summary>
		private readonly string m_ReportName;
		#endregion Private data

		#region Constructors
		/// <summary>default constructor
		/// </summary>
		/// <param name="reportName">report name</param>
		/// <param name="data">block of report first page header data</param>
		public ReportFirstPageHeader(string reportName, ReportHeaderData data)
        {
			m_ReportName = reportName;
			m_Data = data;
		}
		#endregion Constructors

		#region Public methods
		/// <summary>method write content of report header grid
		/// </summary>
		/// <param name="grid">root report header grid</param>
		public override void Write(Grid grid)
		{
			XPSHelper.SetupGrid(grid, 2, 1);
			WriteLogoGrid(grid);
			WriteInformGrid(grid);
		}
		#endregion Public methods

		#region Private methods
		/// <summary>method write content to logo grid
		/// </summary>
		/// <param name="grid">root report header grid</param>
		private void WriteLogoGrid(Grid grid)
		{
			const int rowsCount = 2;
			double[] сolWidth = new double[] { 2.5, 4.0, 1.0 };

			Grid logoGrid = XPSHelper.GetGrid(сolWidth, rowsCount);
			logoGrid.RowDefinitions[0].Height = new GridLength(1, GridUnitType.Auto);		//0.2 * ReportPage.DisplayResolution
			logoGrid.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);		//0.1 * ReportPage.DisplayResolution
			//logoGrid.RowDefinitions[2].Height = new GridLength(7, GridUnitType.Star);		//0.1 * ReportPage.DisplayResolution
			XPSHelper.WriteItemToGrid(logoGrid, grid, 0, 0);

			WriteLogo(logoGrid);
			WriteReportName(logoGrid);
			//if (m_Data.HasAmendments) WriteAmendmentLabel(logoGrid);
			WriteReportNo(logoGrid);
		}
		/// <summary>method write content to informational grid
		/// </summary>
		/// <param name="grid">root report header grid</param>
		private void WriteInformGrid(Grid grid)
		{
			int rowsCount = 6;
			double[] colWidth = new double[] { 0.7, 3.8, 1.5, 1.5 };
			const int topMargin = 2;

			Grid childGrid = XPSHelper.GetGrid(colWidth, rowsCount);
			childGrid.Margin = new Thickness(m_HorMargin, topMargin, 0, 0);
			XPSHelper.WriteItemToGrid(childGrid, grid, 1, 0);

			WritePatientCode(childGrid);
			WriteLabelAndTextBlock1(childGrid, 2, "Provider:", m_Data.GetStringValue(ReportHeaderData.Provider));
			WriteLabelAndTextBlock1(childGrid, 3, string.Empty, m_Data.GetStringValue("ClientName"));
			WriteLabelAndTextBlock1(childGrid, 5, string.Empty, "Full distribution list is provided at the end of the report.", m_FontSize, true);
			WriteLabelAndTextBlock2(childGrid, 1, "Master Accession #:", m_Data.GetStringValue(ReportHeaderData.MasterAccessionNo));
			WriteLabelAndTextBlock2(childGrid, 2, "Date of report:", m_Data.FinalTimeString);
			WriteLabelAndTextBlock2(childGrid, 3, "Accessioned:", m_Data.GetDateTime("AccessionTime"));
			WriteLabelAndTextBlock2(childGrid, 4, "Collected:", m_Data.GetDateTime("CollectionTime", "MM/dd/yyyy"));
			WriteLabelAndTextBlock2(childGrid, 5, "Client Ref #:", m_Data.GetStringValue(ReportHeaderData.ClientRefNumber));
		}
		/// <summary>method write logo image
		/// </summary>
		/// <param name="grid">logo grid</param>
		private static void WriteLogo(Grid grid)
		{
			const int rowIndex = 0;
			const int colIndex = 0;
			const int rowSpan = 3;
			const int colSpan = 3;
			const string imagePath = "YellowstonePathology.Document.Xps.ReportLogo.jpg";

			Assembly assembly = Assembly.GetExecutingAssembly();
			Image logo = new Image() { Source = ((BitmapDecoder)(JpegBitmapDecoder.Create(assembly.GetManifestResourceStream(imagePath), BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default))).Frames[0], };
			XPSHelper.WriteItemToGrid(logo, grid, rowIndex, colIndex, rowSpan, colSpan);
		}
		/// <summary>method write report name
		/// </summary>
		/// <param name="grid">logo grid</param>
		private void WriteReportName(Grid grid)
		{
			const int rowIndex = 0;
			const int colIndex = 1;
			Thickness reportNameMargin = new Thickness(0, 3, m_HorMargin, 0);
			Thickness amendmentLabelMargin = new Thickness(0, 5, m_HorMargin, 0);

			StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Vertical };
			stackPanel.Children.Add(XPSHelper.GetTextBlock(m_ReportName, HorizontalAlignment.Right, VerticalAlignment.Top, reportNameMargin, m_FontSize + 11, m_RedishBrush, FontWeights.Bold, false, false));
			stackPanel.Children.Add(XPSHelper.GetTextBlock((m_Data.HasAmendments ? "Amendment" : string.Empty), HorizontalAlignment.Right, VerticalAlignment.Top, amendmentLabelMargin, m_FontSize + 7, m_RedishBrush, FontWeights.Bold, false, false));
			XPSHelper.WriteItemToGrid(stackPanel, grid, rowIndex, colIndex, 0, 2);
		}
		/// <summary>method write report number
		/// </summary>
		/// <param name="grid">logo grid</param>
		private void WriteReportNo(Grid grid)
		{
			const int rowIndex = 1;
			const int labelColIndex = 1;
			const int valueColIndex = 2;
			double fontSize = m_FontSize + 7;

			Thickness margin = new Thickness(0, 2, m_HorMargin, 0);
			XPSHelper.WriteTextBlockToGrid("Report #:", grid, rowIndex, labelColIndex, HorizontalAlignment.Right, VerticalAlignment.Top, margin, fontSize);
			XPSHelper.WriteTextBlockToGrid(m_Data.GetStringValue(ReportHeaderData.ReportNo), grid, rowIndex, valueColIndex, HorizontalAlignment.Right, VerticalAlignment.Top, margin, fontSize);
		}
		/// <summary>method write patient full display name and code
		/// </summary>
		/// <param name="grid">informational grid</param>
		private void WritePatientCode(Grid grid)
		{
			const int rowsCount = 1;
			GridLength[] colWidth = new GridLength[] { GridLength.Auto, new GridLength(1, GridUnitType.Star) };

			XPSHelper.WriteTextBlockToGrid("Patient:", grid, 0, 0, HorizontalAlignment.Left, VerticalAlignment.Bottom, null, m_FontSize + 3);

			Grid childGrid = XPSHelper.GetGrid(colWidth, rowsCount);
			XPSHelper.WriteTextBlockToGrid(m_Data.GetPatientDisplayName(), childGrid, 0, 0, HorizontalAlignment.Left, VerticalAlignment.Bottom, new Thickness(0, 0, 10, 0), m_FontSize + 6, null, FontWeights.Bold);
			XPSHelper.WriteTextBlockToGrid(m_Data.GetPatientCode(), childGrid, 0, 1, HorizontalAlignment.Left, VerticalAlignment.Bottom, new Thickness(0, 0, 0, 1), m_FontSize + 1);
			XPSHelper.WriteItemToGrid(childGrid, grid, 0, 1);
		}
		/// <summary>method write label/value block to first column of informational grid
		/// </summary>
		/// <param name="rowNo">row index</param>
		/// <param name="label">label text</param>
		/// <param name="value">value text</param>
		/// <param name="grid">informational grid</param>
		private void WriteLabelAndTextBlock1(Grid grid, int rowNo, string label, string value, double fontSize = -1.0, bool isItalic = false)
		{
			if (label != string.Empty) XPSHelper.WriteTextBlockToGrid(label, grid, rowNo, 0, HorizontalAlignment.Left, VerticalAlignment.Bottom, null, (fontSize == -1 ? m_FontSize + 3 : fontSize + 2));
			if (value != string.Empty)
			{
				TextBlock textBlock = XPSHelper.WriteTextBlockToGrid(value, grid, rowNo, 1, HorizontalAlignment.Left, VerticalAlignment.Bottom, new Thickness(0, 0, 0, 1), (fontSize == -1 ? m_FontSize + 1 : fontSize));
				if (isItalic) textBlock.FontStyle = FontStyles.Italic;
			}
		}
		/// <summary>method write label/value block to second column of informational grid
		/// </summary>
		/// <param name="rowNo">row index</param>
		/// <param name="label">label text</param>
		/// <param name="value">value text</param>
		/// <param name="grid">informational grid</param>
		private void WriteLabelAndTextBlock2(Grid grid, int rowNo, string label, string value)
        {
			if (label != string.Empty) XPSHelper.WriteTextBlockToGrid(label, grid, rowNo, 2, HorizontalAlignment.Right, VerticalAlignment.Bottom, new Thickness(0, 0, 7, 0), m_FontSize + 3);
			if (value != string.Empty) XPSHelper.WriteTextBlockToGrid(value, grid, rowNo, 3, HorizontalAlignment.Left, VerticalAlignment.Bottom, new Thickness(0, 0, 0, 1), m_FontSize + 1);
		}
		#endregion Private methods

	}
}
