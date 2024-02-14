using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using YellowstonePathology.Document.Result.Data;
using YellowstonePathology.Document.Xps;

namespace YellowstonePathology.Document.Result.Xps
{
	/// <summary>Leukemia Lymphoma Phenotyping report class
	/// </summary>
	public class LeukemiaLymphomaReport : YpReportBase
    {

		#region Public constants
		/// <summary>report name
		/// </summary>
		public static readonly string ReportName = "Leukemia Lymphoma Phenotyping";
		/// <summary>Impression section header text
		/// </summary>
		public static readonly string ImpressionLabel = "Impression:";
		/// <summary>Interpretive Comment section header text
		/// </summary>
		public static readonly string InterpretiveCommentLabel = "Interpretive Comment:";
		/// <summary>Cell Population Of Interest section header text
		/// </summary>
		public static readonly string CellPopulationOfInterestLabel = "Cell Population Of Interest:";
		/// <summary>array of marker column's headers
		/// </summary>
		public static readonly string[] MarkerColumnHeaders = new string[] { "Marker", "Interpretation", "Intensity" };
		/// <summary>Cell Distribution section header text
		/// </summary>
		public static readonly string CellDistributionLabel = "Cell Distribution";
		/// <summary>array of сell's decriptive names
		/// </summary>
		public static readonly string[] CellTitles = new string[] { "Lymphocytes", "Monocytes", "Myeloid", "Dim CD45/Mod SS" };
		/// <summary>Cell Distribution section header text
		/// </summary>
		public static readonly string BlastHeaderLabel = "Blast Header";
		/// <summary>index of disclaimer text
		/// </summary>
		public static readonly int DisclaimerIndex;
		#endregion Public constants

		#region Private data
		/// <summary>report XML data object
		/// </summary>
		private readonly LeukemiaLymphomaReportData m_ReportData;
		#endregion Private data

		#region Constructors
		/// <summary>constructor
		/// </summary>
		/// <param name="reportData">report XML data object</param>
		public LeukemiaLymphomaReport(LeukemiaLymphomaReportData reportData)
			: base(ReportName, reportData.PageHeader)
        {
			m_ReportData = reportData;

			WriteMainBoxSections(m_ReportData.PathologistSignatureText);
			WriteAmendments(m_ReportData.Amendments, true);
			WriteSectionRow(CellPopulationOfInterestLabel, m_ReportData.CellPopulationOfInterestText, m_TopMargin);
			WriteMarkersSection();
			WriteCellDistributionBlastHeaderSection();

			WriteStandardTrailerSections(reportData.OtherReportsText, reportData.ReportDistributionList, DisclaimerIndex);
		}
		#endregion Private data

		#region Protected methods
		/// <summary>method write "Impression" and "InterpretiveComment" sections in report's main box
		/// </summary>
		/// <returns>top row index of report's main box</returns>
		protected override int WriteCustomSectionToMainBox()
		{
			const double fontSize = m_FontSize + 1;
			const double leftMargin = m_LeftMargin + 10;
			const double topMargin = 3;

			int mainBoxStartRow = WriteSimpleSectionWithTitle(ImpressionLabel, m_ReportData.ImpressionText, fontSize, leftMargin, topMargin);
			WriteSimpleSectionWithTitle(InterpretiveCommentLabel, m_ReportData.InterpretiveCommentText, fontSize, leftMargin);
			return mainBoxStartRow;
		}
		#endregion Protected methods

		#region Private methods

		#region "Markers" section
		/// <summary>method write "Markers" section to document grid
		/// </summary>
		private void WriteMarkersSection()
		{
			double[] colWidth = new double[] { 0.85, 1.0, 1.75, 0.85, 1.0, 1.75 };
			Grid grid = SetupMarkersGrid(colWidth);
			WriteMarkersHeadersRow(grid);
			WriteMarkerBodyRows(grid);
			m_ReportDocument.WriteRowContent(grid);
		}
		/// <summary>method initilize "Markers" section's main grid layout
		/// </summary>
		/// <param name="colWidth">array of column's lenght in inches</param>
		/// <returns>"Markers" section's main grid</returns>
		private Grid SetupMarkersGrid(double[] colWidth)
		{
			int markersCount = m_ReportData.MarkerRowsCount;
			int rowCount = (markersCount / 2) + (markersCount % 2) + 1;

			Grid  grid = XPSHelper.GetGrid(colWidth, rowCount);
			grid.Margin = new Thickness(m_LeftMargin, 0, 0, 0);

			return grid;
		}
		/// <summary>method write headers row of "Markers" section's main grid
		/// </summary>
		/// <param name="grid">"Markers" section's main grid</param>
		/// <param name="colHeaders">array of column's headers names</param>
		private static void WriteMarkersHeadersRow(Grid grid)
		{
			for (int i = 0; i < MarkerColumnHeaders.Length; i++)
			{
				WriteMarkersColHeader(MarkerColumnHeaders[i], grid, i);
				WriteMarkersColHeader(MarkerColumnHeaders[i], grid, i + MarkerColumnHeaders.Length);
			}
		}
		/// <summary>method write columns header
		/// </summary>
		/// <param name="colHeader">column's headers names</param>
		/// <param name="grid">"Markers" section's main grid</param>
		/// <param name="colIndex">column index</param>
		private static void WriteMarkersColHeader(string colHeader, Grid grid, int colIndex)
		{
			XPSHelper.WriteTextBlockToGrid(colHeader, grid, 0, colIndex, HorizontalAlignment.Left, VerticalAlignment.Top, null, m_FontSize, null, FontWeights.Bold, true);
		}
		/// <summary>method write all body lines of "Markers" section's main grid
		/// </summary>
		/// <param name="grid">"Markers" section's main grid</param>
		/// <param name="MarkerColumnValues">names of XML data elements with marker columns values</param>
		/// <param name="colDisp">columns displacement of second columns group of markers</param>
		private void WriteMarkerBodyRows(Grid grid)
		{
			int rowCount = grid.RowDefinitions.Count;
			for (int i = 1; i < rowCount; i++)
			{
				WriteMarkerLine(grid, i, rowCount - 1);
			}
		}
		/// <summary>method write body row of "Markers" section's main grid
		/// </summary>
		/// <param name="grid">"Markers" section's main grid</param>
		/// <param name="rowIndex">row index</param>
		/// <param name="MarkerColumnValues">names of XML data elements with marker columns values</param>
		/// <param name="markerDisp">marker index displacement for marker of second columns group</param>
		/// <param name="colDisp">columns displacement of second columns group of markers</param>
		private void WriteMarkerLine(Grid grid, int rowIndex, int markerDisp)
		{
			WriteMarker(grid, rowIndex - 1, rowIndex, 0);
			if (rowIndex + markerDisp - 1 < m_ReportData.MarkerRowsCount) WriteMarker(grid, rowIndex + markerDisp - 1, rowIndex, MarkerColumnHeaders.Length);
		}
		/// <summary>method write marker's cells
		/// </summary>
		/// <param name="grid">"Markers" section's main grid</param>
		/// <param name="markerIndex">marker index</param>
		/// <param name="colValues">names of XML data elements with marker columns values</param>
		/// <param name="rowIndex">marker's row index</param>
		/// <param name="startColIndex">start column index of marker's columns group</param>
		private void WriteMarker(Grid grid, int markerIndex, int rowIndex, int startColIndex)
		{
			for (int colIndex = 0; colIndex < LeukemiaLymphomaReportData.MarkerColumnsCount; colIndex++)
			{
				XPSHelper.WriteTextBlockToGrid(m_ReportData.GetMarkerCellValue(markerIndex, colIndex), grid, rowIndex, startColIndex++, HorizontalAlignment.Left, VerticalAlignment.Top, null, m_FontSize);
			}
		}
		#endregion "Markers" section

		#region "Cell Distribution/Blast Header" section
		/// <summary>method write "Cell Distribution/Blast Header" section to document grid
		/// </summary>
		private void WriteCellDistributionBlastHeaderSection()
		{
			double columnWidth = ReportPage.ReportWidth / 2 - (2.0 * m_LeftMargin / ReportPage.DisplayResolution);
			double[] colWidth = new double[] { columnWidth, columnWidth };

			Grid grid = SetupCellDistributionBlastHeaderGrid(colWidth);
			WriteCellDistributionSection(grid, columnWidth);
			if (m_ReportData.IsBlastHeaderVisible) WriteBlastHeaderSection(grid, columnWidth);

			m_ReportDocument.WriteRowContent(grid);
		}
		/// <summary>method initilize "Cell Distribution/Blast Header" section's main grid layout
		/// </summary>
		/// <param name="colWidth">array of column's lenght in inches</param>
		/// <returns>"Markers" section's main grid</returns>
		private static Grid SetupCellDistributionBlastHeaderGrid(double[] colWidth)
		{
			int cellDistrRowCount = LeukemiaLymphomaReportData.CellNames.Length;
			int reportBlastRowCount = LeukemiaLymphomaReportData.BlastNames.Length;
			int rowCount = Math.Max(cellDistrRowCount, reportBlastRowCount) + 1;

			Grid grid = XPSHelper.GetGrid(colWidth, rowCount);
			grid.Margin = new Thickness(m_LeftMargin, 0, 0, 0);

			return grid;
		}
		/// <summary>method write "Cell Distribution" section to document grid
		/// </summary>
		private void WriteCellDistributionSection(Grid parentGrid, double columnWidth)
		{
			const int colIndex = 0;
			double[] colWidth = new double[] { 1.75, columnWidth - 1.75 };

			WriteSectionTitle(CellDistributionLabel, parentGrid, 0, colIndex);
			for (int i = 0; i < LeukemiaLymphomaReportData.CellNames.Length; i++)
			{
				WriteSectionRow(CellTitles[i], m_ReportData.GetCellDistributionValue(i), parentGrid, colWidth, i + 1, colIndex);
				
			}
		}
		/// <summary>method write "Blast Header" section to document grid
		/// </summary>
		private void WriteBlastHeaderSection(Grid parentGrid, double columnWidth)
		{
			const int colIndex = 1;
			double[] colWidth = new double[] { 1.75, columnWidth - 1.75 };

			WriteSectionTitle(BlastHeaderLabel, parentGrid, 0, colIndex);
			for (int i = 0; i < LeukemiaLymphomaReportData.BlastNames.Length; i++)
			{
				WriteSectionRow(LeukemiaLymphomaReportData.BlastNames[i], m_ReportData.GetBlastHeaderValue(i), parentGrid, colWidth, i + 1, colIndex);
			}
		}
		#endregion "Cell Distribution" section

		#endregion Private methods

	}
}
