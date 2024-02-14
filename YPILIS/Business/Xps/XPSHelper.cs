using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace YellowstonePathology.Document.Xps
{
	/// <summary>class contain some common methods with WPF controls
	/// </summary>
	public class XPSHelper
	{
		/// <summary>method return array of columns widthes for grid with width equal full report width
		/// </summary>
		/// <param name="startColWidth">array of several start columns widthes (in inches)</param>
		/// <param name="leftMargin">left margin in pixels</param>
		/// <param name="rightMargin">right margin in pixels, is equal to left margin by default</param>
		public static double[] GetFullWidthGridColWidthArray(double[] startColWidth, double leftMargin = 0, double rightMargin = double.MinValue)
		{
			if (rightMargin == double.MinValue) rightMargin = leftMargin;
			int colCount = startColWidth.GetLength(0) + 1;
			double[] colWidth = new double[colCount];
			Array.Copy(startColWidth, colWidth, colCount - 1);
			colWidth[colCount - 1] = ReportPage.ReportWidth - ((leftMargin + rightMargin) / ReportPage.DisplayResolution) - startColWidth.Sum();
			return colWidth;
		}
		/// <summary>method initialize grid
		/// </summary>
		/// <param name="grid">grid being initialized</param>
		/// <param name="colWidth">array of column's width in inches</param>
		/// <param name="rowCount">rows count</param>
		/// <param name="gridWidth">optional grid width in inches</param>
		/// <param name="margin">optional grid margin</param>
		public static void SetupGrid(Grid grid, double[] colWidth, int rowCount, double gridWidth = 0.0, Thickness? margin = null)
		{
			int i;
			int colCount = colWidth.GetLength(0);
			GridLength[] colWidthArray = new GridLength[colCount];
			for (i = 0; i < colCount; i++)
			{
				colWidthArray[i] = new GridLength(colWidth[i] * ReportPage.DisplayResolution);
			}
			SetupGrid(grid, colWidthArray, rowCount, gridWidth, margin);
		}
		/// <summary>method initialize grid
		/// </summary>
		/// <param name="grid">grid being initialized</param>
		/// <param name="colWidth">array of column's width in GridLength units</param>
		/// <param name="rowCount">rows count</param>
		/// <param name="gridWidth">optional grid width in inches</param>
		/// <param name="margin">optional grid margin</param>
		public static void SetupGrid(Grid grid, GridLength[] colWidth, int rowCount, double gridWidth = 0.0, Thickness? margin = null)
		{
			int i;
			int colCount = colWidth.GetLength(0);
			for (i = 0; i < colCount; i++)
			{
				grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = colWidth[i] });
			}
			for (i = 0; i < rowCount; i++)
			{
				grid.RowDefinitions.Add(new RowDefinition());
			}
			if (gridWidth > 0.0) grid.Width = gridWidth * ReportPage.DisplayResolution;
			if(margin.HasValue) grid.Margin = (Thickness)margin;
		}
		/// <summary>method initialize grid
		/// </summary>
		/// <param name="grid">grid being initialized</param>
		/// <param name="rowCount">rows count</param>
		/// <param name="colCount">columns count</param>
		/// <param name="gridWidth">optional grid width in inches</param>
		/// <param name="margin">optional grid margin</param>
		public static void SetupGrid(Grid grid, int rowCount = 0, int colCount = 0, double gridWidth = 0.0, Thickness? margin = null)
		{
			int i;
			if (colCount > 0)
			{
				for (i = 0; i < colCount; i++)
				{
					grid.ColumnDefinitions.Add(new ColumnDefinition());
				}
			}
			if (rowCount > 0)
			{
				for (i = 0; i < rowCount; i++)
				{
					grid.RowDefinitions.Add(new RowDefinition());
				}
			}
			if (gridWidth > 0.0) grid.Width = gridWidth * ReportPage.DisplayResolution;
			if (margin.HasValue) grid.Margin = (Thickness)margin;
		}
		/// <summary>method return new grid
		/// </summary>
		/// <param name="grid">grid being initialized</param>
		/// <param name="colWidth">array of column's width in inches</param>
		/// <param name="rowCount">rows count</param>
		/// <param name="gridWidth">optional grid width in inches</param>
		/// <param name="margin">optional grid margin</param>
		/// <returns>new grid</returns>
		public static Grid GetGrid(double[] colWidth, int rowCount, double gridWidth = 0.0, Thickness? margin = null)
		{
			Grid grid = new Grid();
			SetupGrid(grid, colWidth, rowCount, gridWidth, margin);
			//grid.ShowGridLines = true;
			return grid;
		}
		/// <summary>method return new grid
		/// </summary>
		/// <param name="colWidth">array of column's width in GridLength units</param>
		/// <param name="rowCount">rows count</param>
		/// <param name="gridWidth">optional grid width in inches</param>
		/// <param name="margin">optional grid margin</param>
		/// <returns>new grid</returns>
		public static Grid GetGrid(GridLength[] colWidth, int rowCount, double gridWidth = 0.0, Thickness? margin = null)
		{
			Grid grid = new Grid();
			SetupGrid(grid, colWidth, rowCount, gridWidth, margin);
			return grid;
		}
		/// <summary>method return new grid
		/// </summary>
		/// <param name="rowCount">rows count</param>
		/// <param name="colCount">columns count</param>
		/// <param name="gridWidth">optional grid width in inches</param>
		/// <param name="margin">optional grid margin</param>
		/// <returns>new grid</returns>
		public static Grid GetGrid(int rowCount = 0, int colCount = 0, double gridWidth = 0.0, Thickness? margin = null)
		{
			Grid grid = new Grid();
			SetupGrid(grid, rowCount, colCount, gridWidth, margin);
			return grid;
		}
		/// <summary>method return new grid spreaded to full report width with columns of same width
		/// </summary>
		/// <param name="rowCount">rows count</param>
		/// <param name="colCount">columns count</param>
		/// <param name="leftMargin">optional horizontal (left and right) margin</param>
		/// <returns>new grid</returns>
		public static Grid GetGridWithEqualColumns(int rowCount, int colCount, double leftMargin = 0, double topMargin = 0)
		{
			double colWidth = (ReportPage.ReportWidth * ReportPage.DisplayResolution - 2 * leftMargin) / (colCount * ReportPage.DisplayResolution);
			List<double> colWidths = new List<double>();
			for (int i = 0; i < colCount; i++)
			{
				colWidths.Add(colWidth);
			}
			Grid grid = XPSHelper.GetGrid(colWidths.ToArray(), rowCount);
			grid.Margin = new Thickness(leftMargin, topMargin, 0, 0);
			return grid;
		}
		/// <summary>method initialize TextBlock control
		/// </summary>
		/// <param name="text">TextBlock text</param>
		/// <param name="horAlign">TextBlock horizontal alignment, default is left</param>
		/// <param name="vertAlign">TextBlock vertical alignment, default is top</param>
		/// <param name="margin">TextBlock margin, default is zero margin</param>
		/// <param name="fontSize">TextBlock text font size, default is 10</param>
		/// <param name="foreground">TextBlock text foreground brush, default is black solid brush</param>
		/// <param name="fontWeight">TextBlock text font weight, default is normal</param>
		/// <param name="isUnderlined">if true, then text is underlined (default is false)</param>
		/// <param name="textWrapping">if true, then text is wrapped (default is false)</param>
		/// <returns>initialized TextBlock control</returns>
		public static TextBlock GetTextBlock(string text, HorizontalAlignment horAlign = HorizontalAlignment.Left, VerticalAlignment vertAlign = VerticalAlignment.Top, Thickness? margin = null, double fontSize = 10.0, Brush foreground = null, FontWeight? fontWeight = null, bool isUnderlined = false, bool textWrapping = false)
		{
			TextBlock textBlock = new TextBlock()
			{
				//Background = Brushes.LightGray,
				Text = text,
				HorizontalAlignment = horAlign,
				VerticalAlignment = vertAlign,
				Margin = (margin.HasValue ? (Thickness)margin : new Thickness(0)),
				FontSize = fontSize,
				Foreground = (foreground ?? Brushes.Black),
				FontWeight = (fontWeight ?? FontWeights.Normal),
				TextWrapping = textWrapping ? TextWrapping.Wrap : TextWrapping.NoWrap
			};
			if (isUnderlined)
			{
				textBlock.TextDecorations = new TextDecorationCollection();
				textBlock.TextDecorations.Add(TextDecorations.Underline);
			}
			return textBlock;
		}
		/// <summary>method initialize TextBlock control
		/// </summary>
		/// <param name="text">CheckBox label text</param>
		/// <param name="IsChecked">true, if CheckBox must be checked</param>
		/// <param name="horAlign">CheckBox horizontal alignment, default is left</param>
		/// <param name="vertAlign">CheckBox vertical alignment, default is top</param>
		/// <param name="margin">CheckBox margin, default is zero margin</param>
		/// <param name="fontSize">CheckBox text font size, default is 10</param>
		/// <param name="foreground">CheckBox text foreground brush, default is black solid brush</param>
		/// <param name="fontWeight">CheckBox text font weight, default is normal</param>
		/// <returns>initialized CheckBox control</returns>
		public static CheckBox GetCheckBox(string text, bool IsChecked, HorizontalAlignment horAlign = HorizontalAlignment.Left, VerticalAlignment vertAlign = VerticalAlignment.Top, Thickness? margin = null, double fontSize = 10.0, Brush foreground = null, FontWeight? fontWeight = null)
		{
			CheckBox checkBox = new CheckBox()
			{
				Content = text,
				IsChecked = IsChecked,
				HorizontalAlignment = horAlign,
				VerticalAlignment = vertAlign,
				Margin = (margin.HasValue ? (Thickness)margin : new Thickness(0)),
				FontSize = fontSize,
				Foreground = (foreground ?? Brushes.Black),
				FontWeight = (fontWeight ?? FontWeights.Normal),
			};
			return checkBox;
		}
		/// <summary>method initialize TextBlock control
		/// </summary>
		/// <param name="horAlign">Border horizontal alignment, default is stretch</param>
		/// <param name="vertAlign">Border vertical alignment, default is stretch</param>
		/// <param name="margin">Border margin, default is zero margin</param>
		/// <param name="borderBrush">Border brushб default is black </param>
		/// <param name="borderThickness">border thickness, default is 1</param>
		/// <returns>initialized Border control</returns>
		public static Border GetBorder(HorizontalAlignment horAlign = HorizontalAlignment.Stretch, VerticalAlignment vertAlign = VerticalAlignment.Stretch, Thickness? margin = null, Brush borderBrush = null, Thickness? borderThickness = null)
		{
			Border border = new Border()
			{
				HorizontalAlignment = horAlign,
				VerticalAlignment = vertAlign,
				Margin = (margin.HasValue ? (Thickness)margin : new Thickness(0)),
				BorderBrush = (borderBrush ?? Brushes.Black),
				BorderThickness = (borderThickness.HasValue ? (Thickness)borderThickness : new Thickness(1)),
			};
			return border;
		}
		/// <summary>method add item to parent grid
		/// </summary>
		/// <param name="item">adding item</param>
		/// <param name="parentGrid">parent grid</param>
		/// <param name="rowNo">item cell row number in parent grid</param>
		/// <param name="colNo">item cell column number in parent grid</param>
		/// <param name="rowSpan">item cell row span value, default is no row span</param>
		/// <param name="colSpan">item cell column span value, default is no column span</param>
		public static void WriteItemToGrid(UIElement item, Grid parentGrid, int rowNo, int colNo, int rowSpan = 0, int colSpan = 0)
		{
			Grid.SetRow(item, rowNo);
			if (rowSpan > 0) Grid.SetRowSpan(item, rowSpan);
			Grid.SetColumn(item, colNo);
			if (colSpan > 0) Grid.SetColumnSpan(item, colSpan);
			parentGrid.Children.Add(item);
		}
		/// <summary>method initialize TextBlock control and add it to parent grid
		/// </summary>
		/// <param name="text">TextBlock text</param>
		/// <param name="parentGrid">parent grid</param>
		/// <param name="rowNo">TextBlock cell row number in parent grid</param>
		/// <param name="colNo">TextBlock cell column number in parent grid</param>
		/// <param name="horAlign">TextBlockhorizontal alignment, default is left</param>
		/// <param name="vertAlign">TextBlock vertical alignment, default is top</param>
		/// <param name="margin">TextBlock margin, default is zero margin</param>
		/// <param name="fontSize">TextBlock text font size, default is 10</param>
		/// <param name="foreground">TextBlock text foreground brush, default is black solid brush</param>
		/// <param name="fontWeight">TextBlock text font weight, default is normal</param>
		/// <param name="isUnderlined">if true, then text is underlined (default is false)</param>
		/// <param name="textWrapping">if true, then text is wrapped (default is false)</param>
		/// <param name="rowSpan">TextBlock cell row span value, default is no row span</param>
		/// <param name="colSpan">TextBlock cell column span value, default is no column span</param>
		/// <returns>initialized TextBlock control</returns>
		public static TextBlock WriteTextBlockToGrid(string text, Grid parentGrid, int rowNo, int colNo, HorizontalAlignment horAlign = HorizontalAlignment.Left, VerticalAlignment vertAlign = VerticalAlignment.Top, Thickness? margin = null, double fontSize = 10.0, Brush foreground = null, FontWeight? fontWeight = null, bool isUnderlined = false, bool textWrapping = false, int rowSpan = 0, int colSpan = 0)
		{
			TextBlock textBlock = GetTextBlock(text, horAlign, vertAlign, margin, fontSize, foreground, fontWeight, isUnderlined, textWrapping);
			WriteItemToGrid(textBlock, parentGrid, rowNo, colNo, rowSpan, colSpan);
			return textBlock;
		}
		/// <summary>method write check box to document grid
		/// </summary>
		/// <param name="labelText">CheckBox label text</param>
		/// <param name="isChecked">true, if CheckBox must be checked</param>
		/// <param name="parentGrid">parent grid</param>
		/// <param name="rowNo">CheckBox cell row number in parent grid</param>
		/// <param name="colNo">CheckBox cell column number in parent grid</param>
		/// <param name="horAlign">CheckBox horizontal alignment, default is left</param>
		/// <param name="vertAlign">CheckBox vertical alignment, default is top</param>
		/// <param name="margin">CheckBox margin, default is zero margin</param>
		/// <param name="fontSize">CheckBox text font size, default is 10</param>
		/// <param name="foreground">CheckBox text foreground brush, default is black solid brush</param>
		/// <param name="fontWeight">CheckBox text font weight, default is normal</param>
		/// <param name="rowSpan">TextBlock cell row span value, default is no row span</param>
		/// <param name="colSpan">TextBlock cell column span value, default is no column span</param>
		public static CheckBox WriteCheckBox(string labelText, bool isChecked, Grid parentGrid, int rowNo, int colNo, HorizontalAlignment horAlign = HorizontalAlignment.Left, VerticalAlignment vertAlign = VerticalAlignment.Top, Thickness? margin = null, double fontSize = 10.0, Brush foreground = null, FontWeight? fontWeight = null, int rowSpan = 0, int colSpan = 0)
		{
			CheckBox checkBox = GetCheckBox(labelText, isChecked,  horAlign, vertAlign, margin, fontSize, foreground, fontWeight);
			WriteItemToGrid(checkBox, parentGrid, rowNo, colNo, rowSpan, colSpan);
			return checkBox;
		}
		/// <summary>method add underlining emulation to parent grid cell
		/// </summary>
		/// <param name="grid">parent grid</param>
		/// <param name="rowIndex">cell's row index</param>
		/// <param name="colIndex">cell's column index</param>
		/// <param name="color">underlining brush, default is black solid brush</param>
		/// <param name="thickness">underlining thickness, default is 1</param>
		/// <param name="rowSpan">cell row span value, default is no row span</param>
		/// <param name="colSpan">cell column span value, default is no column span</param>
		/// <returns>border object, that become root container for cell's content</returns>
		public static Border WriteUnderliningToGridCell(Grid grid, int rowIndex, int colIndex, Brush color = null, double thickness = 1, int rowSpan = 0, int colSpan = 0)
		{
			if(color == null) color = Brushes.Black;
			Border border = new Border()
			{
				HorizontalAlignment = HorizontalAlignment.Stretch,
				BorderBrush = color,
				BorderThickness = new Thickness(0, 0, 0, thickness),
				Margin = new Thickness(0, 0, -6, 0)
			};
			WriteItemToGrid(border, grid, rowIndex, colIndex, rowSpan, colSpan);
			return border;
		}
	}
}
