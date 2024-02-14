using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using System.Reflection;

namespace YellowstonePathology.Document.Xps
{
	public class PageNumberFooter : HeaderFooterBase
	{
        private int m_PageNumber;

        public PageNumberFooter(int pageNumber)
        {
            this.m_PageNumber = pageNumber;	
        }

		public override void Write(Grid grid)
		{
			ColumnDefinition col1 = new ColumnDefinition();
			col1.Width = new GridLength(ReportPage.ReportWidth * ReportPage.DisplayResolution);
			grid.ColumnDefinitions.Add(col1);
			RowDefinition row1 = new RowDefinition();
			grid.RowDefinitions.Add(row1);

			TextBlock textBlockPageNumber = new TextBlock();
            textBlockPageNumber.Text = "Page: " + m_PageNumber.ToString();
            textBlockPageNumber.FontSize = 10;
            textBlockPageNumber.Margin = new Thickness(2, 0, 2, 0);            
            textBlockPageNumber.HorizontalAlignment = HorizontalAlignment.Right;
            textBlockPageNumber.VerticalAlignment = VerticalAlignment.Center;

            Grid.SetColumn(textBlockPageNumber, 0);
            Grid.SetRow(textBlockPageNumber, 0);
            grid.Children.Add(textBlockPageNumber);

            TextBlock textBlockGenerationDate = new TextBlock();
            textBlockGenerationDate.Text = "Generated On: " + System.DateTime.Now.ToString("MM/dd/yyyy HH:mm");       
            textBlockGenerationDate.FontSize = 10;
            textBlockGenerationDate.Margin = new Thickness(2, 0, 2, 0);            
            textBlockGenerationDate.HorizontalAlignment = HorizontalAlignment.Left;
            textBlockGenerationDate.VerticalAlignment = VerticalAlignment.Center;

            Grid.SetColumn(textBlockGenerationDate, 0);
            Grid.SetRow(textBlockGenerationDate, 0);
            grid.Children.Add(textBlockGenerationDate);            
		}
	}
}
