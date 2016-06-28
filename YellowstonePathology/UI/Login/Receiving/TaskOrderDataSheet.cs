using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Login.Receiving
{
    public class TaskOrderDataSheet
    {
        private YellowstonePathology.Document.Xps.ReportDocument m_ReportDocument;

		private YellowstonePathology.Business.Task.Model.TaskOrder m_TaskOrder;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

		public TaskOrderDataSheet(YellowstonePathology.Business.Task.Model.TaskOrder taskOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_TaskOrder = taskOrder;
			this.m_AccessionOrder = accessionOrder;
            YellowstonePathology.Document.Xps.PlainHeader header = new YellowstonePathology.Document.Xps.PlainHeader("Task Order Data Sheet");
            YellowstonePathology.Document.Xps.PlainFooter footer = new YellowstonePathology.Document.Xps.PlainFooter(this.m_TaskOrder.ReportNo);

            this.m_ReportDocument = new YellowstonePathology.Document.Xps.ReportDocument(header, footer);

			this.SetTaskGrid();
			this.SetDetailGrid();

		}

		public FixedDocument FixedDocument
		{
			get { return this.m_ReportDocument.FixedDocument; }
		}

		private void SetTaskGrid()
		{
			Grid grid = new Grid();

			ColumnDefinition col1 = new ColumnDefinition();
			col1.Width = new GridLength(96 * 1.6);
			grid.ColumnDefinitions.Add(col1);

			ColumnDefinition col2 = new ColumnDefinition();
			col2.Width = new GridLength(96 * 5.6);
			grid.ColumnDefinitions.Add(col2);

			RowDefinition row1 = new RowDefinition();
			grid.RowDefinitions.Add(row1);

			RowDefinition row2 = new RowDefinition();
			grid.RowDefinitions.Add(row2);

			RowDefinition row3 = new RowDefinition();
			grid.RowDefinitions.Add(row3);

			RowDefinition row4 = new RowDefinition();
			grid.RowDefinitions.Add(row4);

			RowDefinition row5 = new RowDefinition();
			grid.RowDefinitions.Add(row5);

			RowDefinition row6 = new RowDefinition();
			grid.RowDefinitions.Add(row6);

			RowDefinition row7 = new RowDefinition();
			grid.RowDefinitions.Add(row7);

			RowDefinition row8 = new RowDefinition();
			grid.RowDefinitions.Add(row8);
			

			this.WriteData(grid, 0, 0, "ReportNo:", this.m_TaskOrder.ReportNo);
            this.WriteData(grid, 0, 1, "Test:", this.m_TaskOrder.PanelSetName);			

			string dataText = Business.Helper.DateTimeExtensions.DateAndTimeStringFromNullable(this.m_TaskOrder.OrderDate);
			this.WriteData(grid, 0, 3, "Order Date:", dataText);
            
			Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_TaskOrder.TargetId);
            if (orderTarget != null)
            {
                dataText = orderTarget.GetDescription();
                this.WriteData(grid, 0, 4, "Ordered On:", dataText);
            }

			this.WriteData(grid, 0, 5, "Ordered By:", this.m_TaskOrder.OrderedByInitials);			           

			this.m_ReportDocument.WriteRowContent(grid);
		}

		private void SetDetailGrid()
		{
			foreach (YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail in this.m_TaskOrder.TaskOrderDetailCollection)
			{
				Grid grid = new Grid();

				ColumnDefinition col1 = new ColumnDefinition();
				col1.Width = new GridLength(96 * 1.6);
				grid.ColumnDefinitions.Add(col1);

				ColumnDefinition col2 = new ColumnDefinition();
				col2.Width = new GridLength(96 * 5.6);
				grid.ColumnDefinitions.Add(col2);
                
				RowDefinition row1 = new RowDefinition();
				grid.RowDefinitions.Add(row1);

				RowDefinition row2 = new RowDefinition();
				grid.RowDefinitions.Add(row2);

                RowDefinition row3 = new RowDefinition();
                grid.RowDefinitions.Add(row3);

                this.WriteData(grid, 0, 0, "Assigned To:", taskOrderDetail.AssignedTo, 5);
				this.WriteData(grid, 0, 1, "Description:", taskOrderDetail.Description, 5);
				this.WriteData(grid, 0, 2, "Comment:", taskOrderDetail.Comment);

				this.m_ReportDocument.WriteRowContent(grid);
			}
		}

		private void WriteData(Grid grid, int column, int row, string labelText, string dataText, int topMargin = 0, int bottomMargin = 0)
		{
			TextBlock textBlockLabel = new TextBlock();
			textBlockLabel.Text = labelText;
            textBlockLabel.FontWeight = FontWeights.Bold;
			textBlockLabel.Margin = new Thickness(2, topMargin, 2, bottomMargin);
			textBlockLabel.HorizontalAlignment = HorizontalAlignment.Right;
			textBlockLabel.VerticalAlignment = VerticalAlignment.Top;
			Grid.SetColumn(textBlockLabel, column);
			Grid.SetRow(textBlockLabel, row);
			grid.Children.Add(textBlockLabel);

			TextBlock textBlockText = new TextBlock();
			textBlockText.Text = dataText;			
			textBlockText.Margin = new Thickness(2, topMargin, 2, bottomMargin);
			textBlockText.HorizontalAlignment = HorizontalAlignment.Left;
			textBlockText.VerticalAlignment = VerticalAlignment.Top;			
			textBlockText.TextWrapping = TextWrapping.Wrap;
			Grid.SetColumn(textBlockText, column + 1);
			Grid.SetRow(textBlockText, row);
			grid.Children.Add(textBlockText);
		}
	}
}
