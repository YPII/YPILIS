using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace YellowstonePathology.Business.XPSDocument.Result.Xps
{
    public class ClientSupplyOrderReportV2
    {
        private YellowstonePathology.Document.Xps.ReportDocument m_ReportDocument;
        private Business.Client.Model.ClientSupplyOrder m_ClientSupplyOrder;

        public ClientSupplyOrderReportV2(Client.Model.ClientSupplyOrder clientSupplyOrder)
        {
            this.m_ClientSupplyOrder = clientSupplyOrder;

            YellowstonePathology.Document.Xps.PlainHeader header = new YellowstonePathology.Document.Xps.PlainHeader(string.Empty);
            YellowstonePathology.Document.Xps.PlainFooter footer = new YellowstonePathology.Document.Xps.PlainFooter(string.Empty);

            this.m_ReportDocument = new YellowstonePathology.Document.Xps.ReportDocument(header, footer);

            this.WriteClientName();
            this.WriteClientAddress();
            this.WriteClientCityStateZip();
            this.WriteComment();
            this.WriteDetailInfo();
        }

        public FixedDocument FixedDocument
        {
            get { return this.m_ReportDocument.FixedDocument; }
        }

        public void WriteDetailInfo()
        {
            foreach (Business.Client.Model.ClientSupplyOrderDetail clientSupplyOrderDetail in this.m_ClientSupplyOrder.ClientSupplyOrderDetailCollection)
            {
                Grid detailGrid = this.SetupDetailGrid();
                this.WriteDetailQuantity(clientSupplyOrderDetail.quantityordered, detailGrid);
                this.WriteDetailName(clientSupplyOrderDetail.supplyname, detailGrid);
                this.WriteDetailDescription(clientSupplyOrderDetail.supplydescription, detailGrid);
                this.m_ReportDocument.WriteRowContent(detailGrid);
            }
        }

        private void WriteClientName()
        {
            Grid grid = new Grid();

            ColumnDefinition col1 = new ColumnDefinition();
            col1.Width = new GridLength(96 * 1.2);
            grid.ColumnDefinitions.Add(col1);

            ColumnDefinition col2 = new ColumnDefinition();
            col2.Width = new GridLength(96 * 3.5);
            grid.ColumnDefinitions.Add(col2);

            ColumnDefinition col3 = new ColumnDefinition();
            col3.Width = new GridLength(96 * 1.2);
            grid.ColumnDefinitions.Add(col3);

            ColumnDefinition col4 = new ColumnDefinition();
            col4.Width = new GridLength(96 * 1.1);
            grid.ColumnDefinitions.Add(col4);

            RowDefinition row1 = new RowDefinition();
            grid.RowDefinitions.Add(row1);

            TextBlock textBlockLabel = new TextBlock();
            textBlockLabel.Text = "Client Name:";
            textBlockLabel.Margin = new Thickness(2, 0, 2, 0);
            textBlockLabel.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(textBlockLabel, 0);
            Grid.SetRow(textBlockLabel, 0);
            grid.Children.Add(textBlockLabel);

            TextBlock textBlockText = new TextBlock();
            textBlockText.Text = this.m_ClientSupplyOrder.ClientName;
            textBlockText.Margin = new Thickness(2, 0, 2, 0);
            textBlockText.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(textBlockText, 1);
            Grid.SetRow(textBlockText, 0);
            grid.Children.Add(textBlockText);

            TextBlock label = new TextBlock();
            label.Text = "Order Date:";
            label.Margin = new Thickness(2, 0, 2, 0);
            label.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(label, 2);
            Grid.SetRow(label, 0);
            grid.Children.Add(label);

            TextBlock text = new TextBlock();
            text.Text = this.m_ClientSupplyOrder.OrderDate.Value.ToShortDateString();
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(text, 3);
            Grid.SetRow(text, 0);
            grid.Children.Add(text);

            this.m_ReportDocument.WriteRowContent(grid);
        }

        private void WriteClientAddress()
        {
            Grid grid = new Grid();

            ColumnDefinition col1 = new ColumnDefinition();
            col1.Width = new GridLength(96 * 1.2);
            grid.ColumnDefinitions.Add(col1);

            ColumnDefinition col2 = new ColumnDefinition();
            col2.Width = new GridLength(96 * 5.5);
            grid.ColumnDefinitions.Add(col2);

            RowDefinition row1 = new RowDefinition();
            grid.RowDefinitions.Add(row1);

            TextBlock textBlockLabel = new TextBlock();
            textBlockLabel.Text = "Address:";
            textBlockLabel.Margin = new Thickness(2, 0, 2, 0);
            textBlockLabel.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(textBlockLabel, 0);
            Grid.SetRow(textBlockLabel, 0);
            grid.Children.Add(textBlockLabel);

            TextBlock textBlockText = new TextBlock();
            textBlockText.Text = this.m_ClientSupplyOrder.ClientAddress;
            textBlockText.Margin = new Thickness(2, 0, 2, 0);
            textBlockText.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(textBlockText, 1);
            Grid.SetRow(textBlockText, 0);
            grid.Children.Add(textBlockText);

            this.m_ReportDocument.WriteRowContent(grid);
        }

        private void WriteClientCityStateZip()
        {
            Grid grid = new Grid();

            ColumnDefinition col1 = new ColumnDefinition();
            col1.Width = new GridLength(96 * 1.2);
            grid.ColumnDefinitions.Add(col1);

            ColumnDefinition col2 = new ColumnDefinition();
            col2.Width = new GridLength(96 * 2.5);
            grid.ColumnDefinitions.Add(col2);

            ColumnDefinition col3 = new ColumnDefinition();
            col3.Width = new GridLength(96 * 1.0);
            grid.ColumnDefinitions.Add(col3);

            ColumnDefinition col4 = new ColumnDefinition();
            col4.Width = new GridLength(96 * 1.1);
            grid.ColumnDefinitions.Add(col4);

            RowDefinition row1 = new RowDefinition();
            grid.RowDefinitions.Add(row1);

            TextBlock textBlockLabel = new TextBlock();
            textBlockLabel.Text = "City/State/Zip:";
            textBlockLabel.Margin = new Thickness(2, 0, 2, 0);
            textBlockLabel.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(textBlockLabel, 0);
            Grid.SetRow(textBlockLabel, 0);
            grid.Children.Add(textBlockLabel);

            TextBlock textBlockText = new TextBlock();
            textBlockText.Text = this.m_ClientSupplyOrder.ClientCity;
            textBlockText.Margin = new Thickness(2, 0, 2, 0);
            textBlockText.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(textBlockText, 1);
            Grid.SetRow(textBlockText, 0);
            grid.Children.Add(textBlockText);

            TextBlock text1 = new TextBlock();
            text1.Text = this.m_ClientSupplyOrder.ClientState;
            text1.Margin = new Thickness(2, 0, 2, 0);
            text1.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(text1, 2);
            Grid.SetRow(text1, 0);
            grid.Children.Add(text1);

            TextBlock text2 = new TextBlock();
            text2.Text = this.m_ClientSupplyOrder.ClientZip;
            text2.Margin = new Thickness(2, 0, 2, 0);
            text2.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(text2, 3);
            Grid.SetRow(text2, 0);
            grid.Children.Add(text2);

            this.m_ReportDocument.WriteRowContent(grid);
        }

        private void WriteComment()
        {
            Grid grid = new Grid();

            ColumnDefinition col1 = new ColumnDefinition();
            col1.Width = new GridLength(96 * 1.2);
            grid.ColumnDefinitions.Add(col1);

            ColumnDefinition col2 = new ColumnDefinition();
            col2.Width = new GridLength(96 * 6.3);
            grid.ColumnDefinitions.Add(col2);

            RowDefinition row1 = new RowDefinition();
            grid.RowDefinitions.Add(row1);

            TextBlock textBlockLabel = new TextBlock();
            textBlockLabel.Text = "Comment:";
            textBlockLabel.Margin = new Thickness(2, 0, 2, 0);
            textBlockLabel.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(textBlockLabel, 0);
            Grid.SetRow(textBlockLabel, 0);
            grid.Children.Add(textBlockLabel);

            TextBlock textBlockText = new TextBlock();
            textBlockText.Text = this.m_ClientSupplyOrder.Comment;
            textBlockText.Margin = new Thickness(2, 0, 2, 0);
            textBlockText.HorizontalAlignment = HorizontalAlignment.Left;
            textBlockText.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(textBlockText, 1);
            Grid.SetRow(textBlockText, 0);
            grid.Children.Add(textBlockText);

            this.m_ReportDocument.WriteRowContent(grid);

            Border border = new Border()
            {
                BorderBrush = System.Windows.Media.Brushes.Black,
                BorderThickness = new Thickness(0, 1, 0, 0)
            };

            Grid.SetColumn(border, 0);
            Grid.SetRow(border, 4);

            m_ReportDocument.WriteBorder(border);
        }

        private Grid SetupDetailGrid()
        {
            Grid detailGrid = new Grid();

            ColumnDefinition col1 = new ColumnDefinition();
            col1.Width = new GridLength(96 * 1.0);
            detailGrid.ColumnDefinitions.Add(col1);

            ColumnDefinition col2 = new ColumnDefinition();
            col2.Width = new GridLength(96 * 2.0);
            detailGrid.ColumnDefinitions.Add(col2);

            ColumnDefinition col3 = new ColumnDefinition();
            col3.Width = new GridLength(96 * 4.0);
            detailGrid.ColumnDefinitions.Add(col3);

            RowDefinition row = new RowDefinition();
            detailGrid.RowDefinitions.Add(row);

            return detailGrid;
        }

        private void WriteDetailQuantity(string quantityOrdered, Grid detailGrid)
        {
            TextBlock text = new TextBlock();
            text.Text = quantityOrdered;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            text.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(text, 0);
            Grid.SetRow(text, 0);
            detailGrid.Children.Add(text);
        }

        private void WriteDetailName(string supplyName, Grid detailGrid)
        {
            TextBlock text = new TextBlock();
            text.Text = supplyName;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            text.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(text, 1);
            Grid.SetRow(text, 0);
            detailGrid.Children.Add(text);
        }

        private void WriteDetailDescription(string supplyDescription, Grid detailGrid)
        {
            TextBlock text = new TextBlock();
            text.Text = supplyDescription;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            text.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(text, 2);
            Grid.SetRow(text, 0);
            detailGrid.Children.Add(text);
        }
    }
}
