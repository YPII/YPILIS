using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using YellowstonePathology.Document.Xps;

namespace YellowstonePathology.Document.Result.Xps
{
	/// <summary>Report footer class
	/// </summary>
	public class ReportFooter : HeaderFooterBase
	{

		#region Private constants
		/// <summary> footer title
		/// </summary>
		private const string m_Title =  "Yellowstone Pathology Institute Inc.";
		/// <summary>array of column's headers text
		/// </summary>
		private readonly string[] m_ColHeaders = new string[] { "Billings", "Cody", "Y e l l o w s t o n e P a t h o l o g y . c o m" };
		/// <summary>array of column's headers horizontal alingments
		/// </summary>
		private readonly HorizontalAlignment[] m_ColAlign = new HorizontalAlignment[] { HorizontalAlignment.Left, HorizontalAlignment.Left, HorizontalAlignment.Right };
		/// <summary>array of rows texts for first column 
		/// </summary>
		private readonly string[][] m_BillingsRows = new string[][] 
			{ 
				new string[] { "2900 12th Avenue North, Suite 295W" }, 
				new string[] { "Billings, MT 59101" }, 
				new string[] { "phone", "406.238.6360", "fax", "406.238.6361" }, 
				new string[] { "toll-free", "1.888.400.6640" }
			};
		/// <summary>array of rows texts for second column 
		/// </summary>
		private readonly string[][] m_CodyRows = new string[][] 
			{ 
				new string[] { "1008 Cody Avenue" }, 
				new string[] { "Cody, WY 82414" }, 
				new string[] { "phone", "307.578.1850", "fax", "406.238.6361" }, 
				new string[] { "toll-free", "1.888.400.6640" }
			};
		/// <summary>array of rows texts for third column 
		/// </summary>
		private readonly string[] m_SiteRows = new string[] 
			{ 
				"Michael S. Brown, MD, FCAP", 
				"Pamela P. Clegg, MD, FCAP", 
				"Angela F. Durden, MD, FCAP", 
				"Kerrie R. Emerick, MD, FCAP", 
				"Christofer J. Nero, MD, FCAP", 
				"Duane A. Schultz, MD, FCAP", 
			};
		#endregion Private constants

		#region Public methods
		/// <summary>method write report footer to parent grid
		/// </summary>
		/// <param name="grid">document grid</param>
		public override void Write(Grid grid)
		{
			const double gridWidth = 7.5;
			const int rowCount = 3;

			XPSHelper.SetupGrid(grid, rowCount, m_ColHeaders.GetLength(0), gridWidth);
			XPSHelper.WriteTextBlockToGrid(m_Title, grid, 0, 0, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(m_HorMargin, 0, 0, 0), m_FontSize + 1, m_RedishBrush);
			WriteColumnHeaders(grid);
			WriteAddressColumn(m_BillingsRows, grid, 0);
			WriteAddressColumn(m_CodyRows, grid, 1);
			WriteSiteColumn(grid);
		}
		#endregion Public methods

		#region Private methods
		/// <summary>method write column headers to document grid
		/// </summary>
		/// <param name="grid">document grid</param>
		private void WriteColumnHeaders(Grid grid)
		{
			int colCount = m_ColHeaders.GetLength(0);

			for (int i = 0; i < colCount; i++)
			{
				WriteColumnHeader(grid, m_ColHeaders[i], i, (i == 0 ? m_FontSize + 1 : m_FontSize), m_ColAlign[i]);
			}
		}
		/// <summary>method write column header to document grid
		/// </summary>
		/// <param name="grid">document grid</param>
		/// <param name="text">column header's text</param>
		/// <param name="colIndex">column index</param>
		/// <param name="fontSize">header's text font size</param>
		/// <param name="colAlign">header's horizontal alignment</param>
		private void WriteColumnHeader(Grid grid, string text, int colIndex, double fontSize, HorizontalAlignment horAlign)
		{
			const int topMargin = 0;
			const int rowIndex = 1;

			Border border = XPSHelper.WriteUnderliningToGridCell(grid, rowIndex, colIndex, m_RedishBrush);
			Thickness margin = new Thickness(horAlign == HorizontalAlignment.Left ? m_HorMargin : 0, topMargin, horAlign == HorizontalAlignment.Right ? m_HorMargin : 0, 0);
			FontWeight fontWeight = (colIndex == 2 ? FontWeights.Normal : FontWeights.Bold);
			TextBlock colHeader = XPSHelper.GetTextBlock(text, horAlign, VerticalAlignment.Top, margin, fontSize, m_RedishBrush, fontWeight);
			border.Child = colHeader;
		}
		/// <summary>method write address column to document grid
		/// </summary>
		/// <param name="rows">array of rows strings</param>
		/// <param name="grid">document grid</param>
		/// <param name="colIndex">column index</param>
		private void WriteAddressColumn(string[][] rows, Grid grid, int colIndex)
		{
			const int rowIndex = 2;

			int colCount;
			HorizontalAlignment horAlign = m_ColAlign[colIndex];
			int rowCount = rows.GetLength(0);
			
			Grid childGrid = XPSHelper.GetGrid(rowCount, 1);

			for (int i = 0; i < rowCount; i++)
			{
				colCount = rows[i].GetLength(0);
				if (colCount == 1)
					XPSHelper.WriteTextBlockToGrid(rows[i][0], childGrid, i, 0, horAlign, VerticalAlignment.Top, new Thickness(m_HorMargin, (i == 1 ? -5.0 : 0), 0, 0), m_FontSize + 1, m_GreenishBrush);
				else
					WritePhoneRow(childGrid, rows[i], i);
			}
			XPSHelper.WriteItemToGrid(childGrid, grid, rowIndex, colIndex);
		}
		/// <summary>method write phone row of address column to address column grid
		/// </summary>
		/// <param name="grid">address column grid</param>
		/// <param name="rows">array of row's strings</param>
		/// <param name="rowIndex">row index</param>
		private void WritePhoneRow(Grid grid, string[] row, int rowIndex)
		{
			int i;
			TextBlock text;

			int colCount = row.GetLength(0);
			GridLength[] colWidth = new GridLength[colCount];
			for (i = 0; i < colCount; i++)
			{
				if (i % 2 == 0)
					colWidth[i] = GridLength.Auto;
				else
					colWidth[i] = new GridLength(0.8 * ReportPage.DisplayResolution);
			}
			Grid childGrid = XPSHelper.GetGrid(colWidth, 1);
			childGrid.Margin = new Thickness(m_HorMargin, 0, 0, 0);
			for (i = 0; i < colCount; i++)
			{
				text = XPSHelper.WriteTextBlockToGrid(row[i], childGrid, 0, i, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness((i % 2 == 0 ? 0 : 3), (rowIndex == 2 ? 0 : -5.0), 0, 0), m_FontSize);
				text.FontWeight = (i % 2 == 0 ? FontWeights.Normal : FontWeights.Bold);
			}
			XPSHelper.WriteItemToGrid(childGrid, grid, rowIndex, 0);
		}
		/// <summary>method write site column to document grid
		/// </summary>
		/// <param name="grid">document grid</param>
		private void WriteSiteColumn(Grid grid)
		{
			const int rowIndex = 2;
			const int colIndex = 2;
			const double topMargin = -0.7;

			HorizontalAlignment horAlign = m_ColAlign[colIndex];
			int rowCount = m_SiteRows.GetLength(0);
			Grid childGrid = XPSHelper.GetGrid(rowCount, 1);
			for (int i = 0; i < rowCount; i++)
			{
				XPSHelper.WriteTextBlockToGrid(m_SiteRows[i], childGrid, i, 0, horAlign, VerticalAlignment.Top, new Thickness(0, topMargin, m_HorMargin, 0), m_FontSize, m_RedishBrush);
			}
			XPSHelper.WriteItemToGrid(childGrid, grid, rowIndex, colIndex);
		}
		#endregion Private methods

	}
}
