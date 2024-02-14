using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using System.Reflection;

namespace YellowstonePathology.Document.Xps
{
	public class PlainFooter : HeaderFooterBase
	{
		private string m_FooterText;

		public PlainFooter(string footerText)
        {
			this.m_FooterText = footerText;
        }

		public override void Write(Grid grid)
		{
			ColumnDefinition col1 = new ColumnDefinition();
			col1.Width = new GridLength(ReportPage.ReportWidth * ReportPage.DisplayResolution);
			grid.ColumnDefinitions.Add(col1);
			RowDefinition row1 = new RowDefinition();
			grid.RowDefinitions.Add(row1);

			TextBlock label = new TextBlock();
			label.Text = this.m_FooterText;
			label.FontSize = 12;
			label.Margin = new Thickness(2, 0, 2, 0);
			label.FontWeight = FontWeight.FromOpenTypeWeight(700);
			label.HorizontalAlignment = HorizontalAlignment.Right;
			label.VerticalAlignment = VerticalAlignment.Center;

			Grid.SetColumn(label, 0);
			Grid.SetRow(label, 0);
			grid.Children.Add(label);
		}
	}
}
