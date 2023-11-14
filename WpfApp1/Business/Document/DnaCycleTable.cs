using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using System.Windows.Shapes;

namespace YellowstonePathology.Business.Document
{
	public class DnaCycleTable : Border
	{
        Grid m_Grid;
        ColumnDefinition m_Column1;
        ColumnDefinition m_Column2;
        XElement m_CycleElement;

        public void FromCycleElement(XElement cycleElement)
        {
            this.m_CycleElement = cycleElement;
            this.BorderBrush = System.Windows.Media.Brushes.Black;
            this.BorderThickness = new Thickness(1);

            this.m_Grid = new Grid();
            this.m_Grid.Width = 100;
            this.m_Grid.Margin = new Thickness(5,5,5,5);
            this.m_Column1 = new ColumnDefinition();
            this.m_Column1.Width = GridLength.Auto;

            this.m_Column2 = new ColumnDefinition();
            this.m_Column2.Width = GridLength.Auto;

            this.m_Grid.ColumnDefinitions.Add(this.m_Column1);
            this.m_Grid.ColumnDefinitions.Add(this.m_Column2);

            string cycleType = cycleElement.Element("CycleType").Value;
            switch(cycleType)
            {
                case "Diploid":
                    this.SetDiploidGrid();    
                    break;
                case "Aneuploid":
                case "Tetraploid":
                case "Hypertetraploid":
                case "Near Diploid":
                case "Hypo Diploid":
                    this.SetAneuploidGrid(cycleType);
                    break;
            }            
            this.Child = this.m_Grid;
        }

        protected virtual void SetDiploidGrid()
        {
            this.SetTitleRow("Diploid");
            this.SetTitleLine();

            this.SetDataRow("% of Cells", this.m_CycleElement.Element("PercentOfCells").Value, 2);
            this.SetDataRow("CV", this.m_CycleElement.Element("CV").Value, 3);
            this.SetDataRow("G1%", this.m_CycleElement.Element("G1").Value, 4);
            this.SetDataRow("G2%", this.m_CycleElement.Element("G2").Value, 5);
            this.SetDataRow("G2/G1", this.m_CycleElement.Element("G2G1Ratio").Value, 6);
            this.SetDataRow("S-phase%", this.m_CycleElement.Element("S").Value, 7);
            this.SetDataRow("BAD", this.m_CycleElement.Element("BAD").Value, 8);
            this.SetDataRow("RCS", this.m_CycleElement.Element("RCS").Value, 9);        
        }

        protected virtual void SetAneuploidGrid(string title)
        {            
            this.SetTitleRow(title);
            this.SetTitleLine();

            this.SetDataRow("% of Cells", this.m_CycleElement.Element("PercentOfCells").Value, 2);
            this.SetDataRow("CV", this.m_CycleElement.Element("CV").Value, 3);
            this.SetDataRow("G1%", this.m_CycleElement.Element("G1").Value, 4);
            this.SetDataRow("G2%", this.m_CycleElement.Element("G2").Value, 5);            
            this.SetDataRow("DI%", this.m_CycleElement.Element("DI").Value, 6);
            this.SetDataRow("S-phase%", this.m_CycleElement.Element("S").Value, 7);
            this.SetDataRow("BAD", this.m_CycleElement.Element("BAD").Value, 8);
            this.SetDataRow("G2/G1", this.m_CycleElement.Element("G2G1Ratio").Value, 9);
        }

        private void SetTitleRow(string title)
        {
            RowDefinition titleRow = new RowDefinition();
            this.m_Grid.RowDefinitions.Add(titleRow);
            TextBlock titleTextBlock = new TextBlock();
            titleTextBlock.Text = title;
            Grid.SetRow(titleTextBlock, 0);
            Grid.SetColumn(titleTextBlock, 0);
            Grid.SetColumnSpan(titleTextBlock, 2);
            titleTextBlock.HorizontalAlignment = HorizontalAlignment.Left;
            titleTextBlock.TextAlignment = TextAlignment.Center;
            this.m_Grid.Children.Add(titleTextBlock);            
        }

        private void SetTitleLine()
        {
            RowDefinition row = new RowDefinition();
            this.m_Grid.RowDefinitions.Add(row);
            row.Height = new GridLength(10);

            Line line = new Line();
            line.Stroke = System.Windows.Media.Brushes.Black;
            line.StrokeThickness = 1;
            line.X1 = 0;
            line.Y1 = 0;
            line.X2 = 120;
            line.Y2 = 0;            
            line.VerticalAlignment = VerticalAlignment.Center;

            Grid.SetColumn(line, 0);
            Grid.SetRow(line, 1);
            Grid.SetColumnSpan(line, 2);
            this.m_Grid.Children.Add(line);
        }

        private void SetDataRow(string columnTitle, string columnValue, int rowNumber)
        {
            RowDefinition row = new RowDefinition();
            this.m_Grid.RowDefinitions.Add(row);

            TextBlock textBlock1 = new TextBlock();
            textBlock1.Text = columnTitle;
            textBlock1.FontSize = 9;
            Grid.SetRow(textBlock1, rowNumber);
            Grid.SetColumn(textBlock1, 0);
            this.m_Grid.Children.Add(textBlock1);

            TextBlock textBlock2 = new TextBlock();
            textBlock2.Text = columnValue;
            textBlock2.FontSize = 9;
            Grid.SetRow(textBlock2, rowNumber);
            Grid.SetColumn(textBlock2, 1);
            this.m_Grid.Children.Add(textBlock2);            
        }
    }
}
