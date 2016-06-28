using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace YellowstonePathology.Document.Xps
{
	public class PlainHeader : HeaderFooterBase
	{
		private string m_ReportName;

		public PlainHeader(string reportName)
        {
			this.m_ReportName = reportName;
        }

		public override void Write(Grid grid)
		{
			ColumnDefinition col1 = new ColumnDefinition();
			col1.Width = new GridLength(ReportPage.ReportWidth * ReportPage.DisplayResolution);
			grid.ColumnDefinitions.Add(col1);
			RowDefinition row1 = new RowDefinition();
			grid.RowDefinitions.Add(row1);

			TextBlock label = new TextBlock();
			label.Text = m_ReportName;
			label.FontSize = 14;
			label.FontWeight = FontWeight.FromOpenTypeWeight(700);
			label.HorizontalAlignment = HorizontalAlignment.Center;
			label.VerticalAlignment = VerticalAlignment.Center;
			label.TextDecorations = TextDecorations.Underline;

			Grid.SetColumn(label, 0);
			Grid.SetRow(label, 0);
			grid.Children.Add(label);
		}
	}
}
