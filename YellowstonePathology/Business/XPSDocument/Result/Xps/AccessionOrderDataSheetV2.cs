﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;

namespace YellowstonePathology.Document.Result.Xps
{
    public class AccessionOrderDataSheetV2
    {
        private YellowstonePathology.Document.Xps.ReportDocument m_ReportDocument;
        private Grid m_PatientInfoGrid;

        private Business.XPSDocument.Result.Data.AccessionOrderDataSheetDataV2 m_AccessionOrderDataSheetData;

        public AccessionOrderDataSheetV2(Business.XPSDocument.Result.Data.AccessionOrderDataSheetDataV2 accessionOrderDataSheetData)
        {
            this.m_AccessionOrderDataSheetData = accessionOrderDataSheetData;

            YellowstonePathology.Document.Xps.PlainHeader header = new YellowstonePathology.Document.Xps.PlainHeader("Accession Order Data Sheet");
            YellowstonePathology.Document.Xps.PlainFooter footer = new YellowstonePathology.Document.Xps.PlainFooter(this.m_AccessionOrderDataSheetData.AccessionOrderDataSheetDataAccessionOrder.MasterAccessionNo);

            this.m_ReportDocument = new YellowstonePathology.Document.Xps.ReportDocument(header, footer);

            this.SetupPatientInfoGrid();
            this.WriteSpecimenInfo();
            this.WriteClientOrderInfo();
            this.WriteContainerMatchingStatusInfo();
            this.WriteCaseNotesInfo();
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
            col3.Width = new GridLength(96 * 1.3);
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
            this.WriteMasterAccessionNo();
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

            foreach (Business.XPSDocument.Result.Data.AccessionOrderDataSheetDataSpecimenOrder specimenOrderData in this.m_AccessionOrderDataSheetData.AccessionOrderDataSheetDataSpecimenOrders)
            {
                Grid specimenGrid = this.SetupSpecimenGrid();
                this.WriteSpecimenData(specimenOrderData, specimenGrid);
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
            textBlockText.Text = this.m_AccessionOrderDataSheetData.AccessionOrderDataSheetDataAccessionOrder.PatientDisplayName;
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
            text.Text = this.m_AccessionOrderDataSheetData.AccessionOrderDataSheetDataAccessionOrder.SSN;
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
            text.Text = this.m_AccessionOrderDataSheetData.AccessionOrderDataSheetDataAccessionOrder.ClientName;
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
            text.Text = this.m_AccessionOrderDataSheetData.AccessionOrderDataSheetDataAccessionOrder.PhysicianName;
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
            text.Text = this.m_AccessionOrderDataSheetData.AccessionOrderDataSheetDataAccessionOrder.ClinicalHistory;
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
            text.Text = this.m_AccessionOrderDataSheetData.AccessionOrderDataSheetDataAccessionOrder.PreOpDiagnosis;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            text.FontWeight = FontWeight.FromOpenTypeWeight(700);
            text.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(text, 1);
            Grid.SetRow(text, 5);
            this.m_PatientInfoGrid.Children.Add(text);
        }

        private void WriteSpecialInstructions()
        {
            TextBlock label = new TextBlock();
            label.Text = "Special Instructions:";
            label.Margin = new Thickness(2, 0, 2, 0);
            label.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, 7);
            this.m_PatientInfoGrid.Children.Add(label);

            TextBlock text = new TextBlock();
            text.Text = this.m_AccessionOrderDataSheetData.AccessionOrderDataSheetDataAccessionOrder.SpecialInstructions;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            text.TextWrapping = TextWrapping.Wrap;
            text.FontWeight = FontWeight.FromOpenTypeWeight(700);
            Grid.SetColumn(text, 1);
            Grid.SetRow(text, 7);
            this.m_PatientInfoGrid.Children.Add(text);
        }

        private void WriteMasterAccessionNo()
        {
            TextBlock label = new TextBlock();
            label.Text = "Master Accession No:";
            label.Margin = new Thickness(2, 0, 2, 0);
            label.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(label, 2);
            Grid.SetRow(label, 1);
            this.m_PatientInfoGrid.Children.Add(label);

            TextBlock text = new TextBlock();
            text.Text = this.m_AccessionOrderDataSheetData.AccessionOrderDataSheetDataAccessionOrder.MasterAccessionNo;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            text.FontWeight = FontWeight.FromOpenTypeWeight(700);
            Grid.SetColumn(text, 3);
            Grid.SetRow(text, 1);
            this.m_PatientInfoGrid.Children.Add(text);
        }

        private void WriteOrderDate()
        {
            TextBlock label = new TextBlock();
            label.Text = "Accession Time:";
            label.Margin = new Thickness(2, 0, 2, 0);
            label.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(label, 2);
            Grid.SetRow(label, 2);
            this.m_PatientInfoGrid.Children.Add(label);

            TextBlock text = new TextBlock();
            text.Text = this.m_AccessionOrderDataSheetData.AccessionOrderDataSheetDataAccessionOrder.AccessionTime;
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
            text.Text = this.m_AccessionOrderDataSheetData.AccessionOrderDataSheetDataAccessionOrder.SvhMedicalRecord;
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
            text.Text = this.m_AccessionOrderDataSheetData.AccessionOrderDataSheetDataAccessionOrder.SvhAccount;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            text.FontWeight = FontWeight.FromOpenTypeWeight(700);
            Grid.SetColumn(text, 3);
            Grid.SetRow(text, 4);
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

        private void WriteSpecimenData(Business.XPSDocument.Result.Data.AccessionOrderDataSheetDataSpecimenOrder specimenOrderData, Grid specimenGrid)
        {
            RowDefinition row = new RowDefinition();
            specimenGrid.RowDefinitions.Add(row);

            Grid orderGrid = this.WriteSpecimenOrderInfo(specimenOrderData);
            Grid.SetColumn(orderGrid, 0);
            Grid.SetRow(orderGrid, specimenGrid.RowDefinitions.Count - 1);
            specimenGrid.Children.Add(orderGrid);

            Grid accessionedGrid = this.WriteSpecimenAccessionedInfo(specimenOrderData);
            Grid.SetColumn(accessionedGrid, 1);
            Grid.SetRow(accessionedGrid, specimenGrid.RowDefinitions.Count - 1);
            specimenGrid.Children.Add(accessionedGrid);
        }

        private Grid WriteSpecimenOrderInfo(Business.XPSDocument.Result.Data.AccessionOrderDataSheetDataSpecimenOrder specimenOrderData)
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

            this.WriteOrderDescription(specimenOrderData, orderGrid);
            this.WriteAccessionAsDescription(specimenOrderData, orderGrid);
            this.WriteOrderSpecialInstructions(specimenOrderData, orderGrid);
            this.WriteOrderCallbackNumber(specimenOrderData, orderGrid);
            this.WriteOrderCollectionTime(specimenOrderData, orderGrid);
            this.WriteOrderOrderedBy(specimenOrderData, orderGrid);

            return orderGrid;
        }

        private void WriteOrderDescription(Business.XPSDocument.Result.Data.AccessionOrderDataSheetDataSpecimenOrder specimenOrderData, Grid orderGrid)
        {
            TextBlock text = new TextBlock();
            text.Text = specimenOrderData.ClientDescription;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            text.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(text, 0);
            Grid.SetRow(text, 1);
            Grid.SetColumnSpan(text, 2);
            orderGrid.Children.Add(text);
        }

        private void WriteAccessionAsDescription(Business.XPSDocument.Result.Data.AccessionOrderDataSheetDataSpecimenOrder specimenOrderData, Grid orderGrid)
        {
            TextBlock label = new TextBlock();
            label.Text = "Accession As:";
            label.Margin = new Thickness(2, 0, 2, 0);
            label.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, 2);
            orderGrid.Children.Add(label);

            TextBlock text = new TextBlock();
            text.Text = specimenOrderData.AccessionedAsDescription;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            text.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(text, 1);
            Grid.SetRow(text, 2);
            orderGrid.Children.Add(text);
        }

        private void WriteOrderSpecialInstructions(Business.XPSDocument.Result.Data.AccessionOrderDataSheetDataSpecimenOrder specimenOrderData, Grid orderGrid)
        {
            TextBlock label = new TextBlock();
            label.Text = "Special Instructions:";
            label.Margin = new Thickness(2, 0, 2, 0);
            label.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, 3);
            orderGrid.Children.Add(label);

            TextBlock text = new TextBlock();
            text.Text = specimenOrderData.SpecialInstructions;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            text.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(text, 1);
            Grid.SetRow(text, 3);
            orderGrid.Children.Add(text);
        }

        private void WriteOrderCallbackNumber(Business.XPSDocument.Result.Data.AccessionOrderDataSheetDataSpecimenOrder specimenOrderData, Grid orderGrid)
        {
            TextBlock label = new TextBlock();
            label.Text = "Callback No:";
            label.Margin = new Thickness(2, 0, 2, 0);
            label.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, 4);
            orderGrid.Children.Add(label);

            TextBlock text = new TextBlock();
            text.Text = specimenOrderData.CallbackNumber;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(text, 1);
            Grid.SetRow(text, 4);
            orderGrid.Children.Add(text);
        }

        private void WriteOrderCollectionTime(Business.XPSDocument.Result.Data.AccessionOrderDataSheetDataSpecimenOrder specimenOrderData, Grid orderGrid)
        {
            TextBlock label = new TextBlock();
            label.Text = "CollectionTime:";
            label.Margin = new Thickness(2, 0, 2, 0);
            label.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, 5);
            orderGrid.Children.Add(label);

            TextBlock text = new TextBlock();
            text.Text = specimenOrderData.CollectionDate;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(text, 1);
            Grid.SetRow(text, 5);
            orderGrid.Children.Add(text);
        }

        private void WriteOrderOrderedBy(Business.XPSDocument.Result.Data.AccessionOrderDataSheetDataSpecimenOrder specimenOrderData, Grid orderGrid)
        {
            TextBlock label = new TextBlock();
            label.Text = "Ordered By:";
            label.Margin = new Thickness(2, 0, 2, 0);
            label.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, 6);
            orderGrid.Children.Add(label);

            TextBlock text = new TextBlock();
            text.Text = specimenOrderData.OrderedBy;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(text, 1);
            Grid.SetRow(text, 6);
            orderGrid.Children.Add(text);
        }

        private Grid WriteSpecimenAccessionedInfo(Business.XPSDocument.Result.Data.AccessionOrderDataSheetDataSpecimenOrder specimenOrderData)
        {
            Grid accessionedGrid = new Grid();
            accessionedGrid.VerticalAlignment = VerticalAlignment.Top;

            RowDefinition row = new RowDefinition();
            accessionedGrid.RowDefinitions.Add(row);

            RowDefinition row1 = new RowDefinition();
            accessionedGrid.RowDefinitions.Add(row1);

            RowDefinition row2 = new RowDefinition();
            accessionedGrid.RowDefinitions.Add(row2);

            RowDefinition row3 = new RowDefinition();
            accessionedGrid.RowDefinitions.Add(row3);

            RowDefinition row4 = new RowDefinition();
            accessionedGrid.RowDefinitions.Add(row4);

            RowDefinition row5 = new RowDefinition();
            accessionedGrid.RowDefinitions.Add(row5);

            RowDefinition row6 = new RowDefinition();
            accessionedGrid.RowDefinitions.Add(row6);

            RowDefinition row7 = new RowDefinition();
            accessionedGrid.RowDefinitions.Add(row7);

            ColumnDefinition col1 = new ColumnDefinition();
            col1.Width = new GridLength(96 * 1.2);
            accessionedGrid.ColumnDefinitions.Add(col1);

            ColumnDefinition col2 = new ColumnDefinition();
            col2.Width = new GridLength(96 * 2.55);
            accessionedGrid.ColumnDefinitions.Add(col2);

            TextBlock label = new TextBlock();
            label.Text = "Accessioned";
            label.Margin = new Thickness(2, 10, 2, 0);
            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.TextDecorations = TextDecorations.Underline;
            label.FontWeight = FontWeight.FromOpenTypeWeight(700);
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, 0);
            Grid.SetColumnSpan(label, 2);
            accessionedGrid.Children.Add(label);

            this.WriteAccessionedDescription(specimenOrderData, accessionedGrid);
            this.WriteAccessionedCollectionDate(specimenOrderData, accessionedGrid);
            this.WriteDateReceived(specimenOrderData, accessionedGrid);
            this.WriteAccessionedFixationType(specimenOrderData, accessionedGrid);

            return accessionedGrid;
        }

        private void WriteAccessionedDescription(Business.XPSDocument.Result.Data.AccessionOrderDataSheetDataSpecimenOrder specimenOrderData, Grid accessionedGrid)
        {
            TextBlock text = new TextBlock();
            text.Text = specimenOrderData.Description;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            text.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(text, 0);
            Grid.SetRow(text, 1);
            Grid.SetColumnSpan(text, 2);
            accessionedGrid.Children.Add(text);
        }

        private void WriteAccessionedCollectionDate(Business.XPSDocument.Result.Data.AccessionOrderDataSheetDataSpecimenOrder specimenOrderData, Grid accessionedGrid)
        {
            TextBlock label = new TextBlock();
            label.Text = "Collection Date:";
            label.Margin = new Thickness(2, 0, 2, 0);
            label.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, 2);
            accessionedGrid.Children.Add(label);

            TextBlock text = new TextBlock();
            text.Text = specimenOrderData.CollectionTime;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(text, 1);
            Grid.SetRow(text, 2);
            accessionedGrid.Children.Add(text);
        }

        private void WriteDateReceived(Business.XPSDocument.Result.Data.AccessionOrderDataSheetDataSpecimenOrder specimenOrderData, Grid accessionedGrid)
        {
            TextBlock label = new TextBlock();
            label.Text = "Received:";
            label.Margin = new Thickness(2, 0, 2, 0);
            label.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, 3);
            accessionedGrid.Children.Add(label);

            TextBlock text = new TextBlock();
            text.Text = specimenOrderData.DateReceived;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(text, 1);
            Grid.SetRow(text, 3);
            accessionedGrid.Children.Add(text);
        }

        private void WriteAccessionedFixationType(Business.XPSDocument.Result.Data.AccessionOrderDataSheetDataSpecimenOrder specimenOrderData, Grid accessionedGrid)
        {
            TextBlock receivedLabel = new TextBlock();
            receivedLabel.Text = "Received In:";
            receivedLabel.Margin = new Thickness(2, 0, 2, 0);
            receivedLabel.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(receivedLabel, 0);
            Grid.SetRow(receivedLabel, 4);
            accessionedGrid.Children.Add(receivedLabel);

            TextBlock receivedText = new TextBlock();
            receivedText.Text = specimenOrderData.ReceivedIn;
            receivedText.Margin = new Thickness(2, 0, 2, 0);
            receivedText.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(receivedText, 1);
            Grid.SetRow(receivedText, 4);
            accessionedGrid.Children.Add(receivedText);

            TextBlock label = new TextBlock();
            label.Text = "Processed In:";
            label.Margin = new Thickness(2, 0, 2, 0);
            label.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(label, 0);
            Grid.SetRow(label, 5);
            accessionedGrid.Children.Add(label);

            TextBlock text = new TextBlock();
            text.Text = specimenOrderData.ProcessedIn;
            text.Margin = new Thickness(2, 0, 2, 0);
            text.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(text, 1);
            Grid.SetRow(text, 5);
            accessionedGrid.Children.Add(text);
        }

        private void WriteClientOrderInfo()
        {
            this.WriteClientHeading();

            foreach (Business.XPSDocument.Result.Data.AccessionOrderDataSheetDataClientOrder clientOrderData in this.m_AccessionOrderDataSheetData.AccessionOrderDataSheetDataClientOrders)
            {
                Grid clientGrid = this.SetupClientInfoGrid();
                this.WriteClientCell(clientOrderData.ClientName, 0, 0, clientGrid);
                this.WriteClientCell(clientOrderData.OrderedBy, 1, 0, clientGrid);
                this.WriteClientCell(clientOrderData.OrderDate, 2, 0, clientGrid);
                this.WriteClientCell(clientOrderData.Submitted, 3, 0, clientGrid);
                this.WriteClientCell(clientOrderData.Accessioned, 4, 0, clientGrid);
                this.WriteClientCell(clientOrderData.SystemInitiatingOrder, 5, 0, clientGrid);

                this.m_ReportDocument.WriteRowContent(clientGrid);
            }
        }

        private void WriteClientHeading()
        {
            Grid grid = new Grid();

            RowDefinition row = new RowDefinition();
            grid.RowDefinitions.Add(row);

            RowDefinition row1 = new RowDefinition();
            grid.RowDefinitions.Add(row1);

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

            this.WriteSectionHeader("Client Orders", 0, 0, grid);
            this.WriteClientHeader("Client Name", 0, 1, grid);
            this.WriteClientHeader("Ordered By", 1, 1, grid);
            this.WriteClientHeader("Order Date", 2, 1, grid);
            this.WriteClientHeader("Submitted", 3, 1, grid);
            this.WriteClientHeader("Accessioned", 4, 1, grid);
            this.WriteClientHeader("Initiating System", 5, 1, grid);

            this.m_ReportDocument.WriteRowContent(grid);
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

        private void WriteClientHeader(string text, int column, int row, Grid headerGrid)
        {
            TextBlock label = new TextBlock();
            label.Text = text;
            label.Margin = new Thickness(2, 0, 2, 0);
            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.FontWeight = FontWeight.FromOpenTypeWeight(700);
            label.TextDecorations = TextDecorations.Underline;
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

        private void WriteClientCell(string text, int column, int row, Grid clientGrid)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Margin = new Thickness(2, 0, 2, 0);
            textBlock.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(textBlock, column);
            Grid.SetRow(textBlock, row);
            clientGrid.Children.Add(textBlock);
        }

        private void WriteContainerMatchingStatusInfo()
        {
            this.WriteContainerMatchingHeader();

            foreach (Business.XPSDocument.Result.Data.AccessionOrderDataSheetDataSpecimenOrder specimenOrderData in this.m_AccessionOrderDataSheetData.AccessionOrderDataSheetDataSpecimenOrders)
            {
                Grid matchingGrid = this.SetupContainerMatchingGrid();
                this.WriteContainerMatchingGridCell(specimenOrderData.AccessionedAsNumberedDescription, 0, 0, matchingGrid);
                this.WriteContainerMatchingGridCell(specimenOrderData.SpecimenDescriptionMatchStatus, 1, 0, matchingGrid);
                this.WriteContainerMatchingGridCell(specimenOrderData.SpecimenNumberMatchStatus, 2, 0, matchingGrid);
                this.m_ReportDocument.WriteRowContent(matchingGrid);
            }
        }

        private void WriteContainerMatchingHeader()
        {
            Grid grid = new Grid();

            RowDefinition row = new RowDefinition();
            grid.RowDefinitions.Add(row);

            RowDefinition row1 = new RowDefinition();
            grid.RowDefinitions.Add(row1);

            ColumnDefinition col1 = new ColumnDefinition();
            col1.Width = new GridLength(96 * 3.75);
            grid.ColumnDefinitions.Add(col1);

            ColumnDefinition col2 = new ColumnDefinition();
            col2.Width = new GridLength(96 * 1.27);
            grid.ColumnDefinitions.Add(col2);

            ColumnDefinition col3 = new ColumnDefinition();
            col3.Width = new GridLength(96 * 1.23);
            grid.ColumnDefinitions.Add(col3);

            this.WriteSectionHeader("Container Matching Status", 0, 0, grid);
            this.WriteContainerMatchingHeaderCell("Accession As Description", 0, 1, grid);
            this.WriteContainerMatchingHeaderCell("Description", 1, 1, grid);
            this.WriteContainerMatchingHeaderCell("Number", 2, 1, grid);

            this.m_ReportDocument.WriteRowContent(grid);
        }

        private void WriteContainerMatchingHeaderCell(string text, int column, int row, Grid headerGrid)
        {
            TextBlock label = new TextBlock();
            label.Text = text;
            label.Margin = new Thickness(2, 0, 2, 0);
            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.FontWeight = FontWeight.FromOpenTypeWeight(700);
            label.TextDecorations = TextDecorations.Underline;
            Grid.SetColumn(label, column);
            Grid.SetRow(label, row);
            headerGrid.Children.Add(label);
        }

        private void WriteContainerMatchingGridCell(string text, int column, int row, Grid containerMatchingGrid)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Margin = new Thickness(2, 0, 2, 0);
            textBlock.HorizontalAlignment = HorizontalAlignment.Left;
            textBlock.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(textBlock, column);
            Grid.SetRow(textBlock, row);
            containerMatchingGrid.Children.Add(textBlock);
        }

        private Grid SetupContainerMatchingGrid()
        {
            Grid grid = new Grid();

            RowDefinition row = new RowDefinition();
            grid.RowDefinitions.Add(row);

            ColumnDefinition col1 = new ColumnDefinition();
            col1.Width = new GridLength(96 * 3.75);
            grid.ColumnDefinitions.Add(col1);

            ColumnDefinition col2 = new ColumnDefinition();
            col2.Width = new GridLength(96 * 1.27);
            grid.ColumnDefinitions.Add(col2);

            ColumnDefinition col3 = new ColumnDefinition();
            col3.Width = new GridLength(96 * 1.23);
            grid.ColumnDefinitions.Add(col3);

            return grid;
        }

        private void WriteCaseNotesInfo()
        {
            this.WriteCaseNotesHeader();
            foreach (Business.XPSDocument.Result.Data.AccessionOrderDataSheetDataCommentLog commentLogData in this.m_AccessionOrderDataSheetData.AccessionOrderDataSheetDataCommentLogs)
            {
                Grid caseNoteGrid = this.SetupCaseNotesGrid();
                this.WriteCaseNoteCell(commentLogData.LoggedBy, 0, 0, caseNoteGrid);
                this.WriteCaseNoteCell(commentLogData.Description, 1, 0, caseNoteGrid);
                this.WriteCaseNoteCell(commentLogData.Comment, 2, 0, caseNoteGrid);
                this.WriteCaseNoteCell(commentLogData.LogDate, 3, 0, caseNoteGrid);

                this.m_ReportDocument.WriteRowContent(caseNoteGrid);
            }
        }

        private void WriteCaseNotesHeader()
        {
            Grid grid = new Grid();

            RowDefinition row = new RowDefinition();
            grid.RowDefinitions.Add(row);

            RowDefinition row1 = new RowDefinition();
            grid.RowDefinitions.Add(row1);

            ColumnDefinition col1 = new ColumnDefinition();
            col1.Width = new GridLength(96 * 1.15);
            grid.ColumnDefinitions.Add(col1);

            ColumnDefinition col2 = new ColumnDefinition();
            col2.Width = new GridLength(96 * 2.6);
            grid.ColumnDefinitions.Add(col2);

            ColumnDefinition col3 = new ColumnDefinition();
            col3.Width = new GridLength(96 * 2.6);
            grid.ColumnDefinitions.Add(col3);

            ColumnDefinition col4 = new ColumnDefinition();
            col4.Width = new GridLength(96 * 1.15);
            grid.ColumnDefinitions.Add(col4);

            this.WriteSectionHeader("Case Notes", 0, 0, grid);
            this.WriteCaseNotesHeaderCell("Logged By", 0, 1, grid);
            this.WriteCaseNotesHeaderCell("Description", 1, 1, grid);
            this.WriteCaseNotesHeaderCell("Comment", 2, 1, grid);
            this.WriteCaseNotesHeaderCell("Log Time", 3, 1, grid);

            this.m_ReportDocument.WriteRowContent(grid);
        }

        private Grid SetupCaseNotesGrid()
        {
            Grid grid = new Grid();

            RowDefinition row = new RowDefinition();
            grid.RowDefinitions.Add(row);

            ColumnDefinition col1 = new ColumnDefinition();
            col1.Width = new GridLength(96 * 1.15);
            grid.ColumnDefinitions.Add(col1);

            ColumnDefinition col2 = new ColumnDefinition();
            col2.Width = new GridLength(96 * 2.6);
            grid.ColumnDefinitions.Add(col2);

            ColumnDefinition col3 = new ColumnDefinition();
            col3.Width = new GridLength(96 * 2.6);
            grid.ColumnDefinitions.Add(col3);

            ColumnDefinition col4 = new ColumnDefinition();
            col4.Width = new GridLength(96 * 1.15);
            grid.ColumnDefinitions.Add(col4);

            return grid;
        }

        private void WriteCaseNotesHeaderCell(string text, int column, int row, Grid headerGrid)
        {
            TextBlock label = new TextBlock();
            label.Text = text;
            label.Margin = new Thickness(2, 0, 2, 0);
            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.FontWeight = FontWeight.FromOpenTypeWeight(700);
            label.TextDecorations = TextDecorations.Underline;
            Grid.SetColumn(label, column);
            Grid.SetRow(label, row);
            headerGrid.Children.Add(label);
        }

        private void WriteCaseNoteCell(string text, int column, int row, Grid caseNotesGrid)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Margin = new Thickness(2, 0, 2, 0);
            textBlock.HorizontalAlignment = HorizontalAlignment.Left;
            textBlock.TextWrapping = TextWrapping.Wrap;
            Grid.SetColumn(textBlock, column);
            Grid.SetRow(textBlock, row);
            caseNotesGrid.Children.Add(textBlock);
        }
    }
}