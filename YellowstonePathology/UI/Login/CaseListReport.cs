using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Login
{
    public class CaseListReport
    {
		private YellowstonePathology.Business.Search.ReportSearchList m_ReportSearchList;
        private YellowstonePathology.Document.Xps.ReportDocument m_ReportDocument;

		public CaseListReport(YellowstonePathology.Business.Search.ReportSearchList reportSearchList)
        {
            this.m_ReportSearchList = reportSearchList;

            YellowstonePathology.Document.Xps.PlainHeader header = new YellowstonePathology.Document.Xps.PlainHeader("Case List");
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
			foreach (YellowstonePathology.Business.Search.ReportSearchItem reportSearchItem in this.m_ReportSearchList)
            {
                Grid grid = new Grid();

                ColumnDefinition colReportNo = new ColumnDefinition();
                colReportNo.Width = new GridLength(96 * .75);
                grid.ColumnDefinitions.Add(colReportNo);

                ColumnDefinition colAccessionDate = new ColumnDefinition();
                colAccessionDate.Width = new GridLength(96 * 1.2);
                grid.ColumnDefinitions.Add(colAccessionDate);

                ColumnDefinition colPatientName = new ColumnDefinition();
                colPatientName.Width = new GridLength(96 * 1.5);
                grid.ColumnDefinitions.Add(colPatientName);

                ColumnDefinition colBirthdate = new ColumnDefinition();
                colBirthdate.Width = new GridLength(96 * .75);
                grid.ColumnDefinitions.Add(colBirthdate);

                ColumnDefinition colLocation = new ColumnDefinition();
                colLocation.Width = new GridLength(96 * .5);
                grid.ColumnDefinitions.Add(colLocation);

                ColumnDefinition colOrderedBy = new ColumnDefinition();
                colOrderedBy.Width = new GridLength(96 * 1.2);
                grid.ColumnDefinitions.Add(colOrderedBy);

                ColumnDefinition colCaseTpe = new ColumnDefinition();
                colCaseTpe.Width = new GridLength(96 * .75);
                grid.ColumnDefinitions.Add(colCaseTpe);

                ColumnDefinition colPanelSetName = new ColumnDefinition();
                colPanelSetName.Width = new GridLength(96 * 1.5);
                grid.ColumnDefinitions.Add(colPanelSetName);

                RowDefinition rowHeaderRow = new RowDefinition();
                grid.RowDefinitions.Add(rowHeaderRow);

                RowDefinition row = new RowDefinition();
                grid.RowDefinitions.Add(row);
                this.WriteRow(grid, row, rowIndex, reportSearchItem);
                rowIndex += 1;

                this.m_ReportDocument.WriteRowContent(grid);
            }            
        }

		private void WriteRow(Grid grid, RowDefinition row, int rowIndex, YellowstonePathology.Business.Search.ReportSearchItem reportSearchItem)
        {
            TextBlock textBlockReportNo = new TextBlock();
            textBlockReportNo.Text = reportSearchItem.ReportNo;
            textBlockReportNo.Margin = new Thickness(2, 0, 2, 0);
            textBlockReportNo.HorizontalAlignment = HorizontalAlignment.Left;
            textBlockReportNo.VerticalAlignment = VerticalAlignment.Center;            
            Grid.SetColumn(textBlockReportNo, 0);
            Grid.SetRow(textBlockReportNo, rowIndex);
            grid.Children.Add(textBlockReportNo);

            string accessionDateString = string.Empty;
            if (reportSearchItem.AccessionDate.HasValue == true) accessionDateString = reportSearchItem.AccessionDate.Value.ToString("MM/dd/yyyy HH:mm");            

            TextBlock textBlockAccessionDate = new TextBlock();
            textBlockAccessionDate.Text = accessionDateString;
            textBlockAccessionDate.Margin = new Thickness(2, 0, 2, 0);
            textBlockAccessionDate.HorizontalAlignment = HorizontalAlignment.Left;
            textBlockAccessionDate.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(textBlockAccessionDate, 1);
            Grid.SetRow(textBlockAccessionDate, rowIndex);
            grid.Children.Add(textBlockAccessionDate);


            TextBlock textBlockPatientName = new TextBlock();
            textBlockPatientName.Text = reportSearchItem.PatientName;
            textBlockPatientName.Margin = new Thickness(2, 0, 2, 0);
            textBlockPatientName.HorizontalAlignment = HorizontalAlignment.Left;
            textBlockPatientName.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(textBlockPatientName, 2);
            Grid.SetRow(textBlockPatientName, rowIndex);
            grid.Children.Add(textBlockPatientName);


            string birthdateString = string.Empty;
            if (reportSearchItem.PBirthdate.HasValue == true) birthdateString = reportSearchItem.PBirthdate.Value.ToString("MM/dd/yyyy");

            TextBlock textBlockBirthdate = new TextBlock();
            textBlockBirthdate.Text = birthdateString;
            textBlockBirthdate.Margin = new Thickness(2, 0, 2, 0);
            textBlockBirthdate.HorizontalAlignment = HorizontalAlignment.Left;
            textBlockBirthdate.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(textBlockBirthdate, 3);
            Grid.SetRow(textBlockBirthdate, rowIndex);
            grid.Children.Add(textBlockBirthdate);

            TextBlock textBlockLocation = new TextBlock();
            textBlockLocation.Text = reportSearchItem.AccessioningFacilityId;
            textBlockLocation.Margin = new Thickness(2, 0, 2, 0);
            textBlockLocation.HorizontalAlignment = HorizontalAlignment.Left;
            textBlockLocation.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(textBlockLocation, 4);
            Grid.SetRow(textBlockLocation, rowIndex);
            grid.Children.Add(textBlockLocation);

            TextBlock textBlockOrderedBy = new TextBlock();
            textBlockOrderedBy.Text = reportSearchItem.OrderedBy;
            textBlockOrderedBy.Margin = new Thickness(2, 0, 2, 0);
            textBlockOrderedBy.HorizontalAlignment = HorizontalAlignment.Left;
            textBlockOrderedBy.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(textBlockOrderedBy, 5);
            Grid.SetRow(textBlockOrderedBy, rowIndex);
            grid.Children.Add(textBlockOrderedBy);

            TextBlock textBlockPanelSetName = new TextBlock();
            textBlockPanelSetName.Text = reportSearchItem.PanelSetName;
            textBlockPanelSetName.Margin = new Thickness(2, 0, 2, 0);
            textBlockPanelSetName.HorizontalAlignment = HorizontalAlignment.Left;
            textBlockPanelSetName.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(textBlockPanelSetName, 6);
            Grid.SetRow(textBlockPanelSetName, rowIndex);
            grid.Children.Add(textBlockPanelSetName);
        }                
    }
}
