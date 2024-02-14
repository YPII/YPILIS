using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;
using YellowstonePathology.Business.Search;

namespace YellowstonePathology.UI.Login
{
    public class AcidWashListReport
    {
        private Business.Test.ThinPrepPap.AcidWashList m_AcidWashList;
        private Document.Xps.ReportDocument m_ReportDocument;

		public AcidWashListReport(Business.Test.ThinPrepPap.AcidWashList acidWashList)
        {
            this.m_AcidWashList = acidWashList;

            Document.Xps.PlainHeader header = new YellowstonePathology.Document.Xps.PlainHeader("Acid Wash List");
            Document.Xps.PlainFooter footer = new YellowstonePathology.Document.Xps.PlainFooter(string.Empty);

            this.m_ReportDocument = new YellowstonePathology.Document.Xps.ReportDocument(header, footer);

            this.SetGrid();
        }

        public FixedDocument FixedDocument
        {
            get { return this.m_ReportDocument.FixedDocument; }
        }

        private void SetGrid()
        {            
            int rowIndex = 0;
			
            Grid grid = new Grid();

            ColumnDefinition colOne = new ColumnDefinition();
            colOne.Width = new GridLength(96 * .5);
            grid.ColumnDefinitions.Add(colOne);

            ColumnDefinition colTwo = new ColumnDefinition();
            colTwo.Width = new GridLength(96 * 1);
            grid.ColumnDefinitions.Add(colTwo);

            ColumnDefinition colThree = new ColumnDefinition();
            colThree.Width = new GridLength(96 * 1);
            grid.ColumnDefinitions.Add(colThree);

            ColumnDefinition colFour = new ColumnDefinition();
            colFour.Width = new GridLength(96 * .5);
            grid.ColumnDefinitions.Add(colFour);

            ColumnDefinition colFive = new ColumnDefinition();
            colFive.Width = new GridLength(96 * .5);
            grid.ColumnDefinitions.Add(colFive);

            ColumnDefinition colSix = new ColumnDefinition();
            colSix.Width = new GridLength(96 * 3);
            grid.ColumnDefinitions.Add(colSix);

            foreach (Business.Test.ThinPrepPap.AcidWashListItem item in this.m_AcidWashList)
            {
                RowDefinition rowHeaderRow = new RowDefinition();
                grid.RowDefinitions.Add(rowHeaderRow);

                RowDefinition row = new RowDefinition();
                grid.RowDefinitions.Add(row);
                this.WriteRow(grid, row, rowIndex, item);
                rowIndex += 1;                
            }

            this.m_ReportDocument.WriteRowContent(grid);
        }

		private void WriteRow(Grid grid, RowDefinition row, int rowIndex, Business.Test.ThinPrepPap.AcidWashListItem item)
        {
            TextBlock textBlockReportNo = new TextBlock();
            textBlockReportNo.FontSize = 6;
            textBlockReportNo.Text = item.ReportNo;
            textBlockReportNo.Margin = new Thickness(2, 0, 2, 0);
            textBlockReportNo.HorizontalAlignment = HorizontalAlignment.Left;
            textBlockReportNo.VerticalAlignment = VerticalAlignment.Center;            
            Grid.SetColumn(textBlockReportNo, 0);
            Grid.SetRow(textBlockReportNo, rowIndex);
            grid.Children.Add(textBlockReportNo);

            TextBlock textBlockOrderedBy = new TextBlock();
            textBlockOrderedBy.FontSize = 6;
            textBlockOrderedBy.Text = item.UserName;
            textBlockOrderedBy.Margin = new Thickness(2, 0, 2, 0);
            textBlockOrderedBy.HorizontalAlignment = HorizontalAlignment.Left;
            textBlockOrderedBy.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(textBlockOrderedBy, 1);
            Grid.SetRow(textBlockOrderedBy, rowIndex);
            grid.Children.Add(textBlockOrderedBy);

            string orderTime = item.OrderTime.ToString("MM/dd/yyyy HH:mm");
            TextBlock textBlockOrderTime = new TextBlock();
            textBlockOrderTime.FontSize = 6;
            textBlockOrderTime.Text = orderTime;
            textBlockOrderTime.Margin = new Thickness(2, 0, 2, 0);
            textBlockOrderTime.HorizontalAlignment = HorizontalAlignment.Left;
            textBlockOrderTime.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(textBlockOrderTime, 2);
            Grid.SetRow(textBlockOrderTime, rowIndex);
            grid.Children.Add(textBlockOrderTime);

            TextBlock textBlockPatientName = new TextBlock();
            textBlockPatientName.FontSize = 6;
            textBlockPatientName.Text = item.PatientName;
            textBlockPatientName.Margin = new Thickness(2, 0, 2, 0);
            textBlockPatientName.HorizontalAlignment = HorizontalAlignment.Left;
            textBlockPatientName.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(textBlockPatientName, 3);
            Grid.SetRow(textBlockPatientName, rowIndex);
            grid.Children.Add(textBlockPatientName);

            TextBlock textBlockAccepted = new TextBlock();
            textBlockAccepted.FontSize = 6;
            textBlockAccepted.Text = item.Accepted.ToString();
            textBlockAccepted.Margin = new Thickness(2, 0, 2, 0);
            textBlockAccepted.HorizontalAlignment = HorizontalAlignment.Left;
            textBlockAccepted.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(textBlockAccepted, 4);
            Grid.SetRow(textBlockAccepted, rowIndex);
            grid.Children.Add(textBlockAccepted);

            TextBlock textBlockComment = new TextBlock();
            textBlockComment.FontSize = 6;
            textBlockComment.Text = item.Comment;
            textBlockComment.Margin = new Thickness(2, 0, 2, 0);
            textBlockComment.HorizontalAlignment = HorizontalAlignment.Left;
            textBlockComment.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(textBlockComment, 5);
            Grid.SetRow(textBlockComment, rowIndex);
            grid.Children.Add(textBlockComment);            
        }                
    }
}
