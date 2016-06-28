using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace YellowstonePathology.Document
{
	public class ClientBillingDetailHeader : Xps.HeaderFooterBase
	{        
        private string m_ReportTitle1;
        private string m_ReportTitle2;
        private string m_ReportTitle3;

        public ClientBillingDetailHeader(DateTime startDate, DateTime endDate)
        {            
            this.m_ReportTitle1 = "Yellowstone Pathology Institute";
            this.m_ReportTitle2 = "Client Billing Detail Report";

            if (startDate == endDate)
            {
                this.m_ReportTitle3 = "Billing Detail Posted On: " + startDate.ToShortDateString();
            }
            else
            {
                this.m_ReportTitle3 = "Billing Detail Posted Between: " + startDate.ToShortDateString() + " and " + endDate.ToShortDateString();
            }
        }

		public override void Write(Grid grid)
		{
			ColumnDefinition col1 = new ColumnDefinition();
            col1.Width = new GridLength(Xps.ReportPage.ReportWidth * Xps.ReportPage.DisplayResolution);
			grid.ColumnDefinitions.Add(col1);

			RowDefinition row1 = new RowDefinition();
			grid.RowDefinitions.Add(row1);

            RowDefinition row2 = new RowDefinition();
            grid.RowDefinitions.Add(row2);

            RowDefinition row3 = new RowDefinition();
            grid.RowDefinitions.Add(row3);

			TextBlock title1 = new TextBlock();
            title1.Text = this.m_ReportTitle1;
            title1.FontSize = 16;
            title1.FontWeight = FontWeight.FromOpenTypeWeight(700);
            title1.HorizontalAlignment = HorizontalAlignment.Left;
            title1.VerticalAlignment = VerticalAlignment.Center;            

            Grid.SetColumn(title1, 0);
            Grid.SetRow(title1, 0);
            grid.Children.Add(title1);

            TextBlock title2 = new TextBlock();
            title2.Text = this.m_ReportTitle2;
            title2.FontSize = 12;            
            title2.HorizontalAlignment = HorizontalAlignment.Left;
            title2.VerticalAlignment = VerticalAlignment.Center;            

            Grid.SetColumn(title2, 0);
            Grid.SetRow(title2, 1);
            grid.Children.Add(title2);


            TextBlock title3 = new TextBlock();
            title3.Text = this.m_ReportTitle3;
            title3.FontSize = 10;            
            title3.HorizontalAlignment = HorizontalAlignment.Left;
            title3.VerticalAlignment = VerticalAlignment.Center;            

            Grid.SetColumn(title3, 0);
            Grid.SetRow(title3, 2);
            grid.Children.Add(title3);
		}
	}
}
