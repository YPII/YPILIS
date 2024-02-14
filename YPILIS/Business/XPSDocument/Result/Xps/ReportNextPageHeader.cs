using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using YellowstonePathology.Document.Result.Data;
using YellowstonePathology.Document.Xps;

namespace YellowstonePathology.Document.Result.Xps
{
	/// <summary>Report second and next page header class
	/// </summary>
	public class ReportNextPageHeader : HeaderFooterBase
	{

		#region Private constants
		/// <summary>row count of header root grid
		/// </summary>
		private const int m_RowCount = 1;
		/// <summary>column count of header root grid
		/// </summary>
		private const int m_ColCount = 2;
		/// <summary>bottom margin between header labels and bottom line
		/// </summary>
		private const double m_BottomMargin = 3;
		#endregion Private constants

		#region Private data
		/// <summary>block of report first page header data
		/// </summary>
		private readonly ReportHeaderData m_Data;
		/// <summary>font size of header labels text
		/// </summary>
		private new readonly double m_FontSize;
		#endregion Private data

		#region Constructors
		/// <summary>default constructor
		/// </summary>
		/// <param name="reportName">report name</param>
		/// <param name="data">block of report first page header data</param>
		public ReportNextPageHeader(ReportHeaderData data)
        {
			m_Data = data;
			m_FontSize = base.m_FontSize + 3;
		}
		#endregion Constructors

		#region Public methods
		/// <summary>method write content of report header grid
		/// </summary>
		/// <param name="grid">root report header grid</param>
		public override void Write(Grid grid)
		{
			SetupGrid(grid);
			WriteLeftColumn(grid);
			WriteRightColumn(grid);
		}
		#endregion Public methods

		#region Private methods
		/// <summary>method initialize report header layout
		/// </summary>
		/// <param name="grid">root report header grid</param>
		private static void SetupGrid(Grid grid)
		{
			XPSHelper.SetupGrid(grid, m_RowCount, m_ColCount, ReportPage.ReportWidth);
		}
		/// <summary>method write content to left column of page header
		/// </summary>
		private void WriteLeftColumn(Grid grid)
		{
			const int rowIndex = 0;
			const int colIndex = 0;

			Border root = XPSHelper.WriteUnderliningToGridCell(grid, rowIndex, colIndex, m_RedishBrush);
			Thickness margin = new Thickness(m_HorMargin, 0, 0, m_BottomMargin);
			string text = string.Format("{0}, {1}", m_Data.GetPatientDisplayName(), m_Data.GetStringValue(ReportHeaderData.ReportNo));
			TextBlock title = XPSHelper.GetTextBlock(text, HorizontalAlignment.Left, VerticalAlignment.Top, margin, m_FontSize, m_RedishBrush, FontWeights.Bold);
			root.Child = title;
		}
		/// <summary>method write content to right column of page header
		/// </summary>
		private void WriteRightColumn(Grid grid )
		{
			const int rowIndex = 0;
			const int colIndex = 1;

			Border root = XPSHelper.WriteUnderliningToGridCell(grid, rowIndex, colIndex, m_RedishBrush);
			Thickness margin = new Thickness(0, 0, m_HorMargin, m_BottomMargin);
			TextBlock title = XPSHelper.GetTextBlock(string.Empty, HorizontalAlignment.Right, VerticalAlignment.Top, margin, m_FontSize, m_RedishBrush, FontWeights.Bold);
			root.Child = title;
		}
		#endregion Private methods

	}
}
