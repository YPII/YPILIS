using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;

namespace YellowstonePathology.Document.Result.Xps
{
	public class ClientOrderDataSheet
	{
		private YellowstonePathology.Document.Xps.ReportDocument m_ReportDocument;
        private Grid m_PatientInfoGrid;

		private Data.ClientOrderDataSheetData m_ClientOrderDataSheetData;

		public ClientOrderDataSheet(Data.ClientOrderDataSheetData clientOrderDataSheetData)
        {
            this.m_ClientOrderDataSheetData = clientOrderDataSheetData; 

			YellowstonePathology.Document.Xps.PlainHeader header = new YellowstonePathology.Document.Xps.PlainHeader("Client Order Data Sheet");
			YellowstonePathology.Document.Xps.PlainFooter footer = new YellowstonePathology.Document.Xps.PlainFooter(this.m_ClientOrderDataSheetData.Element("PatientDisplayName").Value);

			this.m_ReportDocument = new YellowstonePathology.Document.Xps.ReportDocument(header, footer);

            this.SetupPatientInfoGrid();
			this.WriteSpecimenInfo();
		}

		public FixedDocument FixedDocument
		{
			get { return this.m_ReportDocument.FixedDocument; }
		}

        private void SetupPatientInfoGrid()
        {
            this.m_PatientInfoGrid = new Grid();

            ColumnDefinition col1 = new ColumnDefinition();
            col1.Width = new GridLength(96 * 1.2);
            this.m_PatientInfoGrid.ColumnDefinitions.Add(col1);

            ColumnDefinition col2 = new ColumnDefinition();
            col2.Width = new GridLength(96 * 3.5);
            this.m_PatientInfoGrid.ColumnDefinitions.Add(col2);

			ColumnDefinition col3 = new ColumnDefinition();
			col3.Width = new GridLength(96 * 1.2);
			this.m_PatientInfoGrid.ColumnDefinitions.Add(col3);

			ColumnDefinition col4 = new ColumnDefinition();
			col4.Width = new GridLength(96 * 1.1);
			this.m_PatientInfoGrid.ColumnDefinitions.Add(col4);            

            RowDefinition row1 = new RowDefinition();
            this.m_PatientInfoGrid.RowDefinitions.Add(row1);

            RowDefinition row2 = new RowDefinition();
            this.m_PatientInfoGrid.RowDefinitions.Add(row2);

			RowDefinition row3 = new RowDefinition();
			this.m_PatientInfoGrid.RowDefinitions.Add(row3);

			RowDefinition row4 = new RowDefinition();
			this.m_PatientInfoGrid.RowDefinitions.Add(row4);

			RowDefinition row5 = new RowDefinition();
			this.m_PatientInfoGrid.RowDefinitions.Add(row5);

			RowDefinition row6 = new RowDefinition();
			this.m_PatientInfoGrid.RowDefinitions.Add(row6);

			RowDefinition row7 = new RowDefinition();
			this.m_PatientInfoGrid.RowDefinitions.Add(row7);

            this.WritePatient();
            this.WriteSSN();
            this.WriteClientName();
            this.WriteProviderName();
			this.WriteClinicalHistory();
			this.WritePreOpDiagnosis();
			this.WriteOrderDate();
			this.WriteMedicalRecord();
			this.WriteAccountNo();
			this.WriteSpecialInstructions();

			this.m_ReportDocument.WriteRowContent(this.m_PatientInfoGrid);
        }

		public void WriteSpecimenInfo()
		{
			TextBlock label = new TextBlock();
			label.Text = "Specimen Description";
			label.Margin = new Thickness(2, 10, 2, 0);
			label.HorizontalAlignment = HorizontalAlignment.Center;
			label.FontWeight = FontWeight.FromOpenTypeWeight(700);
			Grid.SetColumn(label, 0);
			Grid.SetRow(label, 0);
			Grid.SetColumnSpan(label, 4);
			this.m_ReportDocument.WriteRowContent(label);

			foreach (XElement specimenElement in this.m_ClientOrderDataSheetData.Elements("ClientOrderDetail"))
			{
				Grid specimenGrid = this.SetupSpecimenGrid();
				this.WriteSpecimenData(specimenElement, specimenGrid);
				this.m_ReportDocument.WriteRowContent(specimenGrid);
			}
		}

        private void WritePatient()
        {
            TextBlock textBlockLabel = new TextBlock();
            textBlockLabel.Text = "Patient:";
			textBlockLabel.Margin = new Thickness(2, 0, 2, 0);
			textBlockLabel.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(textBlockLabel, 0);
            Grid.SetRow(textBlockLabel, 0);
            this.m_PatientInfoGrid.Children.Add(textBlockLabel);
            
            TextBlock textBlockText = new TextBlock();
            textBlockText.Text = this.m_ClientOrderDataSheetData.Element("PatientDisplayName").Value;
			textBlockText.FontSize = 14;
			textBlockText.Margin = new Thickness(2, 0, 2, 0);
			textBlockText.HorizontalAlignment = HorizontalAlignment.Left;
			textBlockText.FontWeight = FontWeight.FromOpenTypeWeight(700);
            Grid.SetColumn(textBlockText, 1);
            Grid.SetRow(textBlockText, 0);
            this.m_PatientInfoGrid.Children.Add(textBlockText);            
        }

        private void WriteSSN()
        {
			TextBlock label = new TextBlock();
			label.Text = "Patient SSN:";
			label.Margin = new Thickness(2, 0, 2, 0);
			label.HorizontalAlignment = HorizontalAlignment.Right;
			Grid.SetColumn(label, 0);
			Grid.SetRow(label, 1);
			this.m_PatientInfoGrid.Children.Add(label);

			TextBlock text = new TextBlock();
			text.Text = this.m_ClientOrderDataSheetData.Element("SSN").Value;
			text.Margin = new Thickness(2, 0, 2, 0);
			text.HorizontalAlignment = HorizontalAlignment.Left;
			text.FontWeight = FontWeight.FromOpenTypeWeight(700);
			Grid.SetColumn(text, 1);
			Grid.SetRow(text, 1);
			this.m_PatientInfoGrid.Children.Add(text);
		}

        private void WriteClientName()
        {
			TextBlock label = new TextBlock();
			label.Text = "Client Name:";
			label.Margin = new Thickness(2, 0, 2, 0);
			label.HorizontalAlignment = HorizontalAlignment.Right;
			Grid.SetColumn(label, 0);
			Grid.SetRow(label, 2);
			this.m_PatientInfoGrid.Children.Add(label);

			TextBlock text = new TextBlock();
			text.Text = this.m_ClientOrderDataSheetData.Element("ClientName").Value;
			text.Margin = new Thickness(2, 0, 2, 0);
			text.HorizontalAlignment = HorizontalAlignment.Left;
			text.FontWeight = FontWeight.FromOpenTypeWeight(700);
			Grid.SetColumn(text, 1);
			Grid.SetRow(text, 2);
			this.m_PatientInfoGrid.Children.Add(text);
		}

        private void WriteProviderName()
        {
			TextBlock label = new TextBlock();
			label.Text = "Provider Name:";
			label.Margin = new Thickness(2, 0, 2, 0);
			label.HorizontalAlignment = HorizontalAlignment.Right;
			Grid.SetColumn(label, 0);
			Grid.SetRow(label, 3);
			this.m_PatientInfoGrid.Children.Add(label);

			TextBlock text = new TextBlock();
			text.Text = this.m_ClientOrderDataSheetData.Element("PhysicianName").Value;
			text.Margin = new Thickness(2, 0, 2, 0);
			text.HorizontalAlignment = HorizontalAlignment.Left;
			text.FontWeight = FontWeight.FromOpenTypeWeight(700);
			Grid.SetColumn(text, 1);
			Grid.SetRow(text, 3);
			this.m_PatientInfoGrid.Children.Add(text);
		}

		private void WriteClinicalHistory()
		{
			TextBlock label = new TextBlock();
			label.Text = "Clinical History:";
			label.Margin = new Thickness(2, 0, 2, 0);
			label.HorizontalAlignment = HorizontalAlignment.Right;
			Grid.SetColumn(label, 0);
			Grid.SetRow(label, 4);
			this.m_PatientInfoGrid.Children.Add(label);

			TextBlock text = new TextBlock();
			text.Text = this.m_ClientOrderDataSheetData.Element("ClinicalHistory").Value;
			text.Margin = new Thickness(2, 0, 2, 0);
			text.HorizontalAlignment = HorizontalAlignment.Left;
			text.FontWeight = FontWeight.FromOpenTypeWeight(700);
			Grid.SetColumn(text, 1);
			Grid.SetRow(text, 4);
			this.m_PatientInfoGrid.Children.Add(text);
		}

		private void WritePreOpDiagnosis()
		{
			TextBlock label = new TextBlock();
			label.Text = "Pre-Op Diagnosis:";
			label.Margin = new Thickness(2, 0, 2, 0);
			label.HorizontalAlignment = HorizontalAlignment.Right;
			Grid.SetColumn(label, 0);
			Grid.SetRow(label, 5);
			this.m_PatientInfoGrid.Children.Add(label);

			TextBlock text = new TextBlock();
			text.Text = this.m_ClientOrderDataSheetData.Element("PreOpDiagnosis").Value;
			text.Margin = new Thickness(2, 0, 2, 0);
			text.HorizontalAlignment = HorizontalAlignment.Left;
			text.FontWeight = FontWeight.FromOpenTypeWeight(700);
			text.TextWrapping = TextWrapping.Wrap;
			Grid.SetColumn(text, 1);
			Grid.SetRow(text, 5);
			this.m_PatientInfoGrid.Children.Add(text);
		}

		private void WriteOrderDate()
		{
			TextBlock label = new TextBlock();
			label.Text = "Order Date:";
			label.Margin = new Thickness(2, 0, 2, 0);
			label.HorizontalAlignment = HorizontalAlignment.Right;
			Grid.SetColumn(label, 2);
			Grid.SetRow(label, 2);
			this.m_PatientInfoGrid.Children.Add(label);

			TextBlock text = new TextBlock();
			text.Text = this.m_ClientOrderDataSheetData.Element("OrderDate").Value;
			text.Margin = new Thickness(2, 0, 2, 0);
			text.HorizontalAlignment = HorizontalAlignment.Left;
			text.FontWeight = FontWeight.FromOpenTypeWeight(700);
			Grid.SetColumn(text, 3);
			Grid.SetRow(text, 2);
			this.m_PatientInfoGrid.Children.Add(text);
		}

		private void WriteMedicalRecord()
		{
			TextBlock label = new TextBlock();
			label.Text = "SVH Medical Record:";
			label.Margin = new Thickness(2, 0, 2, 0);
			label.HorizontalAlignment = HorizontalAlignment.Right;
			Grid.SetColumn(label, 2);
			Grid.SetRow(label, 3);
			this.m_PatientInfoGrid.Children.Add(label);

			TextBlock text = new TextBlock();
			text.Text = this.m_ClientOrderDataSheetData.Element("SvhMedicalRecord").Value;
			text.Margin = new Thickness(2, 0, 2, 0);
			text.HorizontalAlignment = HorizontalAlignment.Left;
			text.FontWeight = FontWeight.FromOpenTypeWeight(700);
			Grid.SetColumn(text, 3);
			Grid.SetRow(text, 3);
			this.m_PatientInfoGrid.Children.Add(text);
		}

		private void WriteAccountNo()
		{
			TextBlock label = new TextBlock();
			label.Text = "SVH Account No:";
			label.Margin = new Thickness(2, 0, 2, 0);
			label.HorizontalAlignment = HorizontalAlignment.Right;
			Grid.SetColumn(label, 2);
			Grid.SetRow(label, 4);
			this.m_PatientInfoGrid.Children.Add(label);

			TextBlock text = new TextBlock();
			text.Text = this.m_ClientOrderDataSheetData.Element("SvhAccount").Value;
			text.Margin = new Thickness(2, 0, 2, 0);
			text.HorizontalAlignment = HorizontalAlignment.Left;
			text.FontWeight = FontWeight.FromOpenTypeWeight(700);
			Grid.SetColumn(text, 3);
			Grid.SetRow(text, 4);
			this.m_PatientInfoGrid.Children.Add(text);
		}

		private void WriteSpecialInstructions()
		{
			TextBlock label = new TextBlock();
			label.Text = "Special Instructions:";
			label.Margin = new Thickness(2, 0, 2, 0);
			label.HorizontalAlignment = HorizontalAlignment.Right;
			Grid.SetColumn(label, 0);
			Grid.SetRow(label, 6);
			this.m_PatientInfoGrid.Children.Add(label);

			TextBlock text = new TextBlock();
			text.Text = this.m_ClientOrderDataSheetData.Element("SpecialInstructions").Value;
			text.Margin = new Thickness(2, 0, 2, 0);
			text.HorizontalAlignment = HorizontalAlignment.Left;
            text.TextWrapping = TextWrapping.Wrap;
			text.FontWeight = FontWeight.FromOpenTypeWeight(700);
			Grid.SetColumn(text, 1);
			Grid.SetRow(text, 6);
			this.m_PatientInfoGrid.Children.Add(text);
		}

		private Grid SetupSpecimenGrid()
		{
			Grid specimenGrid = new Grid();

			ColumnDefinition col1 = new ColumnDefinition();
			col1.Width = new GridLength(96 * 3.75);
			specimenGrid.ColumnDefinitions.Add(col1);

			ColumnDefinition col3 = new ColumnDefinition();
			col3.Width = new GridLength(96 * 3.75);
			specimenGrid.ColumnDefinitions.Add(col3);

			return specimenGrid;
		}

		private void WriteSpecimenData(XElement specimenElement, Grid specimenGrid)
		{
			RowDefinition row = new RowDefinition();
			specimenGrid.RowDefinitions.Add(row);

			Grid orderGrid = this.WriteSpecimenOrderInfo(specimenElement);
			Grid.SetColumn(orderGrid, 0);
			Grid.SetRow(orderGrid, specimenGrid.RowDefinitions.Count - 1);
			specimenGrid.Children.Add(orderGrid);
		}

		private Grid WriteSpecimenOrderInfo(XElement specimenElement)
		{
			Grid orderGrid = new Grid();
			orderGrid.VerticalAlignment = VerticalAlignment.Top;

			RowDefinition row = new RowDefinition();
			orderGrid.RowDefinitions.Add(row);

			RowDefinition row1 = new RowDefinition();
			orderGrid.RowDefinitions.Add(row1);

			RowDefinition row2 = new RowDefinition();
			orderGrid.RowDefinitions.Add(row2);

			RowDefinition row3 = new RowDefinition();
			orderGrid.RowDefinitions.Add(row3);

			RowDefinition row4 = new RowDefinition();
			orderGrid.RowDefinitions.Add(row4);

			RowDefinition row5 = new RowDefinition();
			orderGrid.RowDefinitions.Add(row5);

			RowDefinition row6 = new RowDefinition();
			orderGrid.RowDefinitions.Add(row6);

			ColumnDefinition col1 = new ColumnDefinition();
			col1.Width = new GridLength(96 * 1.2);
			orderGrid.ColumnDefinitions.Add(col1);

			ColumnDefinition col2 = new ColumnDefinition();
			col2.Width = new GridLength(96 * 2.55);
			orderGrid.ColumnDefinitions.Add(col2);

			TextBlock label = new TextBlock();
			label.Text = "Ordered";
			label.Margin = new Thickness(2, 10, 2, 0);
			label.HorizontalAlignment = HorizontalAlignment.Left;
			label.TextDecorations = TextDecorations.Underline;
			label.FontWeight = FontWeight.FromOpenTypeWeight(700);
			Grid.SetColumn(label, 0);
			Grid.SetRow(label, 0);
			Grid.SetColumnSpan(label, 2);
			orderGrid.Children.Add(label);

			this.WriteOrderDescription(specimenElement, orderGrid);
			this.WriteAccessionAsDescription(specimenElement, orderGrid);
			this.WriteSpecimenOrderSpecialInstructions(specimenElement, orderGrid);
			this.WriteOrderCallbackNumber(specimenElement, orderGrid);
			this.WriteOrderCollectionTime(specimenElement, orderGrid);
			this.WriteOrderOrderedBy(specimenElement, orderGrid);

			return orderGrid;
		}

		private void WriteOrderDescription(XElement clientElement, Grid orderGrid)
		{
			TextBlock text = new TextBlock();
			text.Text = clientElement.Element("Description").Value;
			text.Margin = new Thickness(2, 0, 2, 0);
			text.HorizontalAlignment = HorizontalAlignment.Left;
			text.TextWrapping = TextWrapping.Wrap;
			Grid.SetColumn(text, 0);
			Grid.SetRow(text, 1);
			Grid.SetColumnSpan(text, 2);
			orderGrid.Children.Add(text);
		}

		private void WriteAccessionAsDescription(XElement clientElement, Grid orderGrid)
		{
			TextBlock label = new TextBlock();
			label.Text = "Accession As:";
			label.Margin = new Thickness(2, 0, 2, 0);
			label.HorizontalAlignment = HorizontalAlignment.Right;
			Grid.SetColumn(label, 0);
			Grid.SetRow(label, 2);
			orderGrid.Children.Add(label);

			TextBlock text = new TextBlock();
			text.Text = clientElement.Element("AccessionedAsDescription").Value;
			text.Margin = new Thickness(2, 0, 2, 0);
			text.HorizontalAlignment = HorizontalAlignment.Left;
			text.TextWrapping = TextWrapping.Wrap;
			Grid.SetColumn(text, 1);
			Grid.SetRow(text, 2);
			orderGrid.Children.Add(text);
		}

		private void WriteSpecimenOrderSpecialInstructions(XElement clientElement, Grid orderGrid)
		{
			TextBlock label = new TextBlock();
			label.Text = "Special Instructions:";
			label.Margin = new Thickness(2, 0, 2, 0);
			label.HorizontalAlignment = HorizontalAlignment.Right;
			Grid.SetColumn(label, 0);
			Grid.SetRow(label, 3);
			orderGrid.Children.Add(label);

			TextBlock text = new TextBlock();
			text.Text = clientElement.Element("SpecialInstructions").Value;
			text.Margin = new Thickness(2, 0, 2, 0);
			text.HorizontalAlignment = HorizontalAlignment.Left;
			text.TextWrapping = TextWrapping.Wrap;
			Grid.SetColumn(text, 1);
			Grid.SetRow(text, 3);
			orderGrid.Children.Add(text);
		}

		private void WriteOrderCallbackNumber(XElement clientElement, Grid orderGrid)
		{
			TextBlock label = new TextBlock();
			label.Text = "Callback No:";
			label.Margin = new Thickness(2, 0, 2, 0);
			label.HorizontalAlignment = HorizontalAlignment.Right;
			Grid.SetColumn(label, 0);
			Grid.SetRow(label, 4);
			orderGrid.Children.Add(label);

			TextBlock text = new TextBlock();
			text.Text = clientElement.Element("CallbackNumber").Value;
			text.Margin = new Thickness(2, 0, 2, 0);
			text.HorizontalAlignment = HorizontalAlignment.Left;
			Grid.SetColumn(text, 1);
			Grid.SetRow(text, 4);
			orderGrid.Children.Add(text);
		}

		private void WriteOrderCollectionTime(XElement clientElement, Grid orderGrid)
		{
			TextBlock label = new TextBlock();
			label.Text = "CollectionTime:";
			label.Margin = new Thickness(2, 0, 2, 0);
			label.HorizontalAlignment = HorizontalAlignment.Right;
			Grid.SetColumn(label, 0);
			Grid.SetRow(label, 5);
			orderGrid.Children.Add(label);

			TextBlock text = new TextBlock();
			text.Text = clientElement.Element("CollectionDate").Value;
			text.Margin = new Thickness(2, 0, 2, 0);
			text.HorizontalAlignment = HorizontalAlignment.Left;
			Grid.SetColumn(text, 1);
			Grid.SetRow(text, 5);
			orderGrid.Children.Add(text);
		}

		private void WriteOrderOrderedBy(XElement clientElement, Grid orderGrid)
		{
			TextBlock label = new TextBlock();
			label.Text = "Ordered By:";
			label.Margin = new Thickness(2, 0, 2, 0);
			label.HorizontalAlignment = HorizontalAlignment.Right;
			Grid.SetColumn(label, 0);
			Grid.SetRow(label, 6);
			orderGrid.Children.Add(label);

			TextBlock text = new TextBlock();
			text.Text = clientElement.Element("OrderedBy").Value;
			text.Margin = new Thickness(2, 0, 2, 0);
			text.HorizontalAlignment = HorizontalAlignment.Left;
			Grid.SetColumn(text, 1);
			Grid.SetRow(text, 6);
			orderGrid.Children.Add(text);
		}

		private void WriteSectionHeader(string text, int column, int row, Grid headerGrid)
		{
			TextBlock label = new TextBlock();
			label.Text = text;
			label.Margin = new Thickness(2, 10, 2, 0);
			label.HorizontalAlignment = HorizontalAlignment.Left;
			label.FontWeight = FontWeight.FromOpenTypeWeight(700);
			Grid.SetColumn(label, column);
			Grid.SetRow(label, row);
			headerGrid.Children.Add(label);
		}

		private Grid SetupClientInfoGrid()
		{
			Grid grid = new Grid();

			RowDefinition row = new RowDefinition();
			grid.RowDefinitions.Add(row);

			ColumnDefinition col1 = new ColumnDefinition();
			col1.Width = new GridLength(96 * 2);
			grid.ColumnDefinitions.Add(col1);

			ColumnDefinition col2 = new ColumnDefinition();
			col2.Width = new GridLength(96 * 1.3);
			grid.ColumnDefinitions.Add(col2);

			ColumnDefinition col3 = new ColumnDefinition();
			col3.Width = new GridLength(96 * .9);
			grid.ColumnDefinitions.Add(col3);

			ColumnDefinition col4 = new ColumnDefinition();
			col4.Width = new GridLength(96 * .7);
			grid.ColumnDefinitions.Add(col4);

			ColumnDefinition col5 = new ColumnDefinition();
			col5.Width = new GridLength(96 * .9);
			grid.ColumnDefinitions.Add(col5);

			ColumnDefinition col6 = new ColumnDefinition();
			col6.Width = new GridLength(96 * 1.3);
			grid.ColumnDefinitions.Add(col6);

			return grid;
		}
	}
}
