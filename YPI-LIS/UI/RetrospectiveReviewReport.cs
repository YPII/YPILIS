using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;

namespace YellowstonePathology.UI
{
    public class RetrospectiveReviewReport
    {
		private YellowstonePathology.UI.RetrospectiveReviewList m_RetrospectiveReviewList;
        private YellowstonePathology.Document.Xps.ReportDocument m_ReportDocument;

		public RetrospectiveReviewReport(YellowstonePathology.UI.RetrospectiveReviewList retrospectiveReviewList)
        {
            this.m_RetrospectiveReviewList = retrospectiveReviewList;

            YellowstonePathology.Document.Xps.PlainHeader header = new YellowstonePathology.Document.Xps.PlainHeader("Retrospective Reviews");
            YellowstonePathology.Document.Xps.PlainFooter footer = new YellowstonePathology.Document.Xps.PlainFooter(string.Empty);

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
			foreach (YellowstonePathology.UI.RetrospectiveReviewListItem item in this.m_RetrospectiveReviewList)
            {
                Grid grid = new Grid();

                ColumnDefinition colReportNo = new ColumnDefinition();
                colReportNo.Width = new GridLength(96 * .75);
                grid.ColumnDefinitions.Add(colReportNo);                               

                ColumnDefinition colSurgicalFinal = new ColumnDefinition();
                colSurgicalFinal.Width = new GridLength(96 * 1.5);
                grid.ColumnDefinitions.Add(colSurgicalFinal);

                ColumnDefinition colSurgicalFinalDate = new ColumnDefinition();
                colSurgicalFinalDate.Width = new GridLength(96 * 1.5);
                grid.ColumnDefinitions.Add(colSurgicalFinalDate);

                RowDefinition rowHeaderRow = new RowDefinition();
                grid.RowDefinitions.Add(rowHeaderRow);

                RowDefinition row = new RowDefinition();
                grid.RowDefinitions.Add(row);
                this.WriteRow(grid, row, rowIndex, item);
                rowIndex += 1;

                this.m_ReportDocument.WriteRowContent(grid);
            }            
        }

		private void WriteRow(Grid grid, RowDefinition row, int rowIndex, YellowstonePathology.UI.RetrospectiveReviewListItem item)
        {
            TextBlock textBlockReportNo = new TextBlock();
            textBlockReportNo.Text = item.SurgicalReportNo;
            textBlockReportNo.Margin = new Thickness(2, 0, 2, 0);
            textBlockReportNo.HorizontalAlignment = HorizontalAlignment.Left;
            textBlockReportNo.VerticalAlignment = VerticalAlignment.Center;            
            Grid.SetColumn(textBlockReportNo, 0);
            Grid.SetRow(textBlockReportNo, rowIndex);
            grid.Children.Add(textBlockReportNo);                                                     

            TextBlock textBlockSurgicalFinal = new TextBlock();
            textBlockSurgicalFinal.Text = item.SurgicalFinaledBy;
            textBlockSurgicalFinal.Margin = new Thickness(2, 0, 2, 0);
            textBlockSurgicalFinal.HorizontalAlignment = HorizontalAlignment.Left;
            textBlockSurgicalFinal.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(textBlockSurgicalFinal, 1);
            Grid.SetRow(textBlockSurgicalFinal, rowIndex);
            grid.Children.Add(textBlockSurgicalFinal);

            TextBlock textBlockSurgicalFinalDate = new TextBlock();
            textBlockSurgicalFinalDate.Text = item.SurgicalFinalDate.ToString("MM/dd/yyyy");
            textBlockSurgicalFinalDate.Margin = new Thickness(2, 0, 2, 0);
            textBlockSurgicalFinalDate.HorizontalAlignment = HorizontalAlignment.Left;
            textBlockSurgicalFinalDate.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(textBlockSurgicalFinalDate, 2);
            Grid.SetRow(textBlockSurgicalFinalDate, rowIndex);
            grid.Children.Add(textBlockSurgicalFinalDate);
        }                
    }
}
