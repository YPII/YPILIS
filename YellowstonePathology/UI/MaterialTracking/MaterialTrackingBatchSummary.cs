using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;

namespace YellowstonePathology.UI.MaterialTracking
{
	public class MaterialTrackingBatchSummary
	{
		private YellowstonePathology.Document.Xps.ReportDocument m_ReportDocument;
		private YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch m_MaterialTrackingBatch;
		private YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogViewCollection m_MaterialTrackingLogViewCollection;

		public MaterialTrackingBatchSummary(YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch,
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogViewCollection materialTrackingLogViewCollection)
		{
            this.m_MaterialTrackingBatch = materialTrackingBatch;
            this.m_MaterialTrackingLogViewCollection = materialTrackingLogViewCollection;

			YellowstonePathology.Document.Xps.PlainHeader header = new YellowstonePathology.Document.Xps.PlainHeader("Yellowstone Pathology Institute - Material Distribution Report");
			YellowstonePathology.Document.Xps.NoFooter footer = new YellowstonePathology.Document.Xps.NoFooter();

			this.m_ReportDocument = new YellowstonePathology.Document.Xps.ReportDocument(header, footer);			

            int totalMaterialCount = 0;

            MaterialTrackingSummaryCollection materialTrackingSummaryCollection = new MaterialTrackingSummaryCollection();

            List<string> materialTypeList = this.m_MaterialTrackingLogViewCollection.GetDistinctMaterialTypes();
            List<string> masterAccessionNoList = this.m_MaterialTrackingLogViewCollection.GetMasterAccessionNoList();

            foreach (string masterAccessionNo in masterAccessionNoList)
            {
                MaterialTrackingSummary materialTrackingSummary = new MaterialTrackingSummary(masterAccessionNo, false);
                
                foreach (string materialType in materialTypeList)
                {
                    int materialCount = this.m_MaterialTrackingLogViewCollection.GetMaterialCount(masterAccessionNo, materialType);
                    MaterialTrackingSummaryColumn materialTrackingSummaryColumn = new MaterialTrackingSummaryColumn(materialType, materialCount);
                    materialTrackingSummary.ColumnList.Add(materialTrackingSummaryColumn);
                    totalMaterialCount += materialCount;
                }
                materialTrackingSummaryCollection.Add(materialTrackingSummary);
            }

            this.WriteHeadingsGrid(materialTypeList);
            foreach (MaterialTrackingSummary materialTrackingSummary in materialTrackingSummaryCollection)
            {
                this.WriteReportLine(materialTrackingSummary);
            }
            
            if (materialTrackingSummaryCollection.Count > 0)
            {
                materialTrackingSummaryCollection.SetTotals();
                this.WriteTotalLine(materialTrackingSummaryCollection[materialTrackingSummaryCollection.Count - 1]);
            }
		}

		public FixedDocument FixedDocument
		{
			get { return this.m_ReportDocument.FixedDocument; }
		}

        private void WriteHeadingsGrid(List<string> materialTypeList)
		{
			Grid grid = new Grid();

			RowDefinition row = new RowDefinition();
			grid.RowDefinitions.Add(row);

			RowDefinition row1 = new RowDefinition();
			grid.RowDefinitions.Add(row1);

            RowDefinition row2 = new RowDefinition();
            row2.Height = new GridLength(20, GridUnitType.Pixel);
            grid.RowDefinitions.Add(row2);

            RowDefinition row3 = new RowDefinition();
            row3.Height = new GridLength(20, GridUnitType.Pixel);
            grid.RowDefinitions.Add(row3);

            ColumnDefinition masterAccessionColumn = new ColumnDefinition();
            masterAccessionColumn.Width = new GridLength(96 * 2, GridUnitType.Pixel);
            grid.ColumnDefinitions.Add(masterAccessionColumn);

            for (int i = 0; i < materialTypeList.Count; i++)
            {
                ColumnDefinition materialTypeColumn = new ColumnDefinition();
                materialTypeColumn.Width = new GridLength(96 * 1, GridUnitType.Pixel);
                grid.ColumnDefinitions.Add(materialTypeColumn);
            }					

			TextBlock label1 = new TextBlock();
			label1.Text = "Batch Date: " + this.m_MaterialTrackingBatch.OpenDate.ToString();
			label1.Margin = new Thickness(0, 2, 2, 2);
			label1.HorizontalAlignment = HorizontalAlignment.Left;			
            Grid.SetColumnSpan(label1, 4);
			Grid.SetColumn(label1, 0);            
			Grid.SetRow(label1, 0);
			grid.Children.Add(label1);

			TextBlock text1 = new TextBlock();
            text1.Text = "Description: " + this.m_MaterialTrackingBatch.Description;
			text1.Margin = new Thickness(2, 2, 2, 2);
			text1.HorizontalAlignment = HorizontalAlignment.Left;			
            Grid.SetColumnSpan(text1, 4);
			Grid.SetColumn(text1, 0);
			Grid.SetRow(text1, 1);
			grid.Children.Add(text1);			
            
			TextBlock label4 = new TextBlock();
			label4.Text = "Master Accession";
			label4.Margin = new Thickness(0, 2, 2, 2);
			label4.HorizontalAlignment = HorizontalAlignment.Left;
			label4.TextDecorations = TextDecorations.Underline;
			label4.FontWeight = FontWeight.FromOpenTypeWeight(700);
			Grid.SetColumn(label4, 0);
			Grid.SetRow(label4, 3);
			grid.Children.Add(label4);

            int column = 1;
            foreach (string materialType in materialTypeList)
            {
                TextBlock materialTypeTextBlock = new TextBlock();
                materialTypeTextBlock.Text = materialType;
                materialTypeTextBlock.Margin = new Thickness(0, 2, 2, 2);
                materialTypeTextBlock.HorizontalAlignment = HorizontalAlignment.Left;
                materialTypeTextBlock.TextDecorations = TextDecorations.Underline;
                materialTypeTextBlock.FontWeight = FontWeight.FromOpenTypeWeight(700);
                Grid.SetColumn(materialTypeTextBlock, column);
                Grid.SetRow(materialTypeTextBlock, 3);
                grid.Children.Add(materialTypeTextBlock);
                column += 1;
            }

			this.m_ReportDocument.WriteRowContent(grid);
		}

        private void WriteReportLine(MaterialTrackingSummary materialTrackingSummary)
		{
			Grid grid = new Grid();
			RowDefinition row = new RowDefinition();
			grid.RowDefinitions.Add(row);

            ColumnDefinition columnDefinitionMA = new ColumnDefinition();
            columnDefinitionMA.Width = new GridLength(96 * 2, GridUnitType.Pixel);
            grid.ColumnDefinitions.Add(columnDefinitionMA);

            TextBlock textBlockMA = new TextBlock();
            textBlockMA.Text = materialTrackingSummary.MasterAccessionNo;
            textBlockMA.Margin = new Thickness(2, 10, 2, 0);
            textBlockMA.HorizontalAlignment = HorizontalAlignment.Left;
            textBlockMA.TextAlignment = TextAlignment.Right;
            Grid.SetColumn(textBlockMA, 0);
            Grid.SetRow(textBlockMA, 0);
            grid.Children.Add(textBlockMA);

            int column = 1;
            foreach (MaterialTrackingSummaryColumn materialTrackingSummaryColumn in materialTrackingSummary.ColumnList)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(96 * 1, GridUnitType.Pixel);
                grid.ColumnDefinitions.Add(columnDefinition);

                TextBlock textBlock = new TextBlock();
                textBlock.Text = materialTrackingSummaryColumn.Value.ToString();
                textBlock.Margin = new Thickness(2, 10, 2, 0);
                textBlock.HorizontalAlignment = HorizontalAlignment.Left;
                textBlock.TextAlignment = TextAlignment.Right;
                Grid.SetColumn(textBlock, column);
                Grid.SetRow(textBlock, 0);
                grid.Children.Add(textBlock);
                column += 1;
            }

			this.m_ReportDocument.WriteRowContent(grid);
		}

        private void WriteTotalLine(MaterialTrackingSummary materialTrackingSummaryTotal)
        {
            Grid grid = new Grid();
            RowDefinition row = new RowDefinition();
            grid.RowDefinitions.Add(row);

            ColumnDefinition column1 = new ColumnDefinition();
            column1.Width = new GridLength(96 * 2, GridUnitType.Pixel);
            grid.ColumnDefinitions.Add(column1);            

            TextBlock text1 = new TextBlock();
            text1.Text = "Total";
            text1.Margin = new Thickness(2, 10, 2, 0);
            text1.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(text1, 0);
            Grid.SetRow(text1, 0);
            grid.Children.Add(text1);

            int column = 1;
            foreach (MaterialTrackingSummaryColumn materialTrackingSummaryColumn in materialTrackingSummaryTotal.ColumnList)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(96 * 1, GridUnitType.Pixel);
                grid.ColumnDefinitions.Add(columnDefinition);

                TextBlock textBlock = new TextBlock();
                textBlock.Text = materialTrackingSummaryColumn.Value.ToString();
                textBlock.Margin = new Thickness(2, 10, 2, 0);
                textBlock.HorizontalAlignment = HorizontalAlignment.Left;
                textBlock.TextAlignment = TextAlignment.Right;
                Grid.SetColumn(textBlock, column);
                Grid.SetRow(textBlock, 0);
                grid.Children.Add(textBlock);
                column += 1;
            }

            this.m_ReportDocument.WriteRowContent(grid);
        }
	}
}
