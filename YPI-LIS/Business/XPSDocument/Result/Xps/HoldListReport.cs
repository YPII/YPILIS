using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;

namespace YellowstonePathology.Business.XPSDocument.Result.Xps
{
    public class HoldListReport
    {
        private YellowstonePathology.Document.Xps.ReportDocument m_ReportDocument;
        private Business.Specimen.Model.AliquotOrderHoldCollection m_HoldList;

        public HoldListReport(Business.Specimen.Model.AliquotOrderHoldCollection holdList)
        {
            this.m_HoldList = holdList;

            YellowstonePathology.Document.Xps.PlainHeader header = new YellowstonePathology.Document.Xps.PlainHeader("Embedding Hold List");
            YellowstonePathology.Document.Xps.PlainFooter footer = new YellowstonePathology.Document.Xps.PlainFooter(string.Empty);            

            this.m_ReportDocument = new YellowstonePathology.Document.Xps.ReportDocument(header, footer);            

            this.WriteHeaderGrid();
            this.WriteDetailInfo();
        }

        public FixedDocument FixedDocument
        {
            get { return this.m_ReportDocument.FixedDocument; }
        }

        private void WriteHeaderGrid()
        {
            Grid grid = SetupDetailGrid();
            this.WriteHeaderText("Aliquot Order Id", grid, 0, 0);
            this.WriteHeaderText("Type", grid, 0, 1);
            this.WriteHeaderText("Description", grid, 0, 2);
            this.WriteHeaderText("Instructions", grid, 0, 3);            

            this.m_ReportDocument.WriteRowContent(grid);

            Border border = new Border()
            {
                BorderBrush = System.Windows.Media.Brushes.Black,
                BorderThickness = new Thickness(0, 0, 0, 1)
            };

            Grid.SetColumn(border, 0);
            Grid.SetRow(border, 0);

            m_ReportDocument.WriteBorder(border);
        }

        private void WriteHeaderText(string text, Grid grid, int row, int column)
        {
            TextBlock textBlockText = new TextBlock();
            textBlockText.Text = text;
            textBlockText.Margin = new Thickness(2, 0, 2, 0);
            textBlockText.HorizontalAlignment = HorizontalAlignment.Left;
            textBlockText.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(textBlockText, column);
            Grid.SetRow(textBlockText, row);
            grid.Children.Add(textBlockText);
        }

        public void WriteDetailInfo()
        {
            foreach (Business.Specimen.Model.AliquotOrderHold hold in this.m_HoldList)
            {
                Grid detailGrid = this.SetupDetailGrid();
                this.WriteDetailAliquotOrderId(hold.AliquotOrderId, detailGrid);
                this.WriteDetailAliquotType(hold.AliquotType, detailGrid);
                this.WriteDetailDescription(hold.Description, detailGrid);
                this.WriteDetailInstructions(hold.EmbeddingInstructions, detailGrid);                
                this.m_ReportDocument.WriteRowContent(detailGrid);
            }
        }

        private Grid SetupDetailGrid()
        {
            Grid detailGrid = new Grid();

            ColumnDefinition col1 = new ColumnDefinition();
            col1.Width = new GridLength(96 * 1.00);
            detailGrid.ColumnDefinitions.Add(col1);

            ColumnDefinition col2 = new ColumnDefinition();
            col2.Width = new GridLength(96 * 1.00);
            detailGrid.ColumnDefinitions.Add(col2);

            ColumnDefinition col3 = new ColumnDefinition();
            col3.Width = new GridLength(96 * 2.50);
            detailGrid.ColumnDefinitions.Add(col3);

            ColumnDefinition col4 = new ColumnDefinition();
            col4.Width = new GridLength(96 * 2.50);
            detailGrid.ColumnDefinitions.Add(col4);            

            RowDefinition row = new RowDefinition();
            detailGrid.RowDefinitions.Add(row);

            return detailGrid;
        }

        private void WriteDetailAliquotOrderId(string aliquotOrderId,  Grid detailGrid)
        {
            TextBlock text = new TextBlock();
            text.Text = aliquotOrderId;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            text.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(text, 0);
            Grid.SetRow(text, 0);
            detailGrid.Children.Add(text);
        }

        private void WriteDetailAliquotType(string aliquotType, Grid detailGrid)
        {
            TextBlock text = new TextBlock();
            text.Text = aliquotType;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            text.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(text, 1);
            Grid.SetRow(text, 0);
            detailGrid.Children.Add(text);
        }

        private void WriteDetailDescription(string description, Grid detailGrid)
        {
            TextBlock text = new TextBlock();
            text.Text = description;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            text.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(text, 2);
            Grid.SetRow(text, 0);
            detailGrid.Children.Add(text);
        }

        private void WriteDetailInstructions(string instructions, Grid detailGrid)
        {
            TextBlock text = new TextBlock();
            text.Text = instructions;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            text.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(text, 3);
            Grid.SetRow(text, 0);
            detailGrid.Children.Add(text);
        }
        
    }
}
