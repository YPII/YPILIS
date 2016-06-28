using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;
using System.Globalization;
using YellowstonePathology.Document.Xps;
using System.Text;

namespace YellowstonePathology.Document
{
	public class BillingReport
	{
        private const int FontSize = 6;
        private const int MarginSize = 3;
        private const double GridWidth = 680; // 96 * 8.5 - m_MarginSize * 2;

		private ReportDocument m_ReportDocument;
		private XElement m_ReportData;						

		public BillingReport(XElement reportData)
		{
            this.m_ReportData = reportData;

			YellowstonePathology.Document.Xps.PlainHeader header = new YellowstonePathology.Document.Xps.PlainHeader("Billing Report");
			NoFooter footer = new NoFooter();
			this.m_ReportDocument = new ReportDocument(header, footer);            
            
			WriteDocument();
		}

		public FixedDocument FixedDocument
		{
			get { return m_ReportDocument.FixedDocument; }
		}

		private void WriteDocument()
		{
			foreach(XElement accessionElement in this.m_ReportData.Elements("AccessionOrder"))
			{
                List<XElement> panelSetOrderElements = XMLHelper.GetElementList(accessionElement, "PanelSetOrderCollection", "PanelSetOrder");
                foreach (XElement panelSetOrderElement in panelSetOrderElements)
                {
                    Grid accessionGrid = this.GetFirstLineGrid(accessionElement, panelSetOrderElement);
                    this.m_ReportDocument.WriteRowContent(accessionGrid);

                    Grid panelSetGrid = this.GetSecondLineGrid(accessionElement, panelSetOrderElement);
                    this.m_ReportDocument.WriteRowContent(panelSetGrid);

                    List<XElement> panelSetOrderCPTCodeElements = XMLHelper.GetElementList(panelSetOrderElement, "PanelSetOrderCPTCodeCollection", "PanelSetOrderCPTCode");
                    if (panelSetOrderCPTCodeElements.Count > 0)
                    {
                        Grid cptHeaderGrid = this.GetCodeSectionHeader("CPT Codes");
                        this.m_ReportDocument.WriteRowContent(cptHeaderGrid);
                    }
                    foreach (XElement panelSetOrderCPTCodeElement in panelSetOrderCPTCodeElements)
                    {
                        Grid panelSetOrderCPTCodeGrid = this.GetPanelSetOrderCPTCodeGrid(panelSetOrderCPTCodeElement);
                        this.m_ReportDocument.WriteRowContent(panelSetOrderCPTCodeGrid);
                    }

                    List<XElement> PanelSetOrderCPTCodeBillNonCLNTElements = XMLHelper.GetElementList(panelSetOrderElement, "PanelSetOrderCPTCodeBillNonCLNTCollection", "PanelSetOrderCPTCodeBill");
                    if (PanelSetOrderCPTCodeBillNonCLNTElements.Count > 0)
                    {
                        Grid nonClientHeaderGrid = this.GetCodeSectionHeader("CPT Summary");
                        this.m_ReportDocument.WriteRowContent(nonClientHeaderGrid);
                    }
                    foreach (XElement panelSetOrderCPTCodeBillElement in PanelSetOrderCPTCodeBillNonCLNTElements)
                    {
                        Grid nonClientPanelSetOrderCPTCodeBillGrid = this.GetPanelSetOrderCPTCodeBillGrid(panelSetOrderCPTCodeBillElement);
                        this.m_ReportDocument.WriteRowContent(nonClientPanelSetOrderCPTCodeBillGrid);
                    }

                    List<XElement> PanelSetOrderCPTCodeBillCLNTElements = XMLHelper.GetElementList(panelSetOrderElement, "PanelSetOrderCPTCodeBillCLNTCollection", "PanelSetOrderCPTCodeBill");
                    if (PanelSetOrderCPTCodeBillCLNTElements.Count > 0)
                    {
                        Grid medicareHeaderGrid = this.GetCodeSectionHeader("Medicare Codes");
                        this.m_ReportDocument.WriteRowContent(medicareHeaderGrid);
                    }
                    foreach (XElement panelSetOrderCPTCodeBillElement in PanelSetOrderCPTCodeBillCLNTElements)
                    {
                        Grid medicarePanelSetOrderCPTCodeBillGrid = this.GetPanelSetOrderCPTCodeBillGrid(panelSetOrderCPTCodeBillElement);
                        this.m_ReportDocument.WriteRowContent(medicarePanelSetOrderCPTCodeBillGrid);
                    }

                    Grid blankGrid = new Grid();
                    blankGrid.Height = 30;
                    this.m_ReportDocument.WriteRowContent(blankGrid);
                }
			}
		}

		private Grid GetFirstLineGrid(XElement accessionOrderElement, XElement panelSetOrderElement)
		{            
			Grid result = new Grid();
            result.Width = GridWidth;

			ColumnDefinition colDateOfSerice = new ColumnDefinition();
            colDateOfSerice.Width = GridLength.Auto;
            result.ColumnDefinitions.Add(colDateOfSerice);

            Nullable<DateTime> dateOfService = XMLHelper.GetNullableDateTime(accessionOrderElement, "DateOfService");
            string dateOfServiceString = string.Empty;
            if (dateOfService.HasValue == true)
            {
                dateOfServiceString = dateOfService.Value.ToString("MM/dd/yyyy");
            }

            TextBlock textBlockDateOfService = XPSHelper.GetTextBlock(dateOfServiceString, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
            Grid.SetColumn(textBlockDateOfService, 0);
            Grid.SetRow(textBlockDateOfService, 0);
            result.Children.Add(textBlockDateOfService);

            ColumnDefinition colPatientDisplayString = new ColumnDefinition();
            colPatientDisplayString.Width = new GridLength(20, GridUnitType.Star);
            result.ColumnDefinitions.Add(colPatientDisplayString);

            string firstName = XMLHelper.GetString(accessionOrderElement, "PFirstName");
            string lastName = XMLHelper.GetString(accessionOrderElement, "PLastName");
            string accountNumber = XMLHelper.GetString(accessionOrderElement, "SvhAccount");
            string medicalRecordNumber = XMLHelper.GetString(accessionOrderElement, "SvhMedicalRecord");

            StringBuilder patientDisplayString = new StringBuilder();
            patientDisplayString.Append(YellowstonePathology.Business.Patient.Model.Patient.GetLastFirstDisplayName(firstName, lastName));

            if (string.IsNullOrEmpty(accountNumber) == false)
            {
                patientDisplayString.Append("(" + accountNumber);
                if (string.IsNullOrEmpty(medicalRecordNumber) == false)
                {
                    patientDisplayString.Append("/");
                }
                else
                {
                    patientDisplayString.Append(")");
                }
            }

            if (string.IsNullOrEmpty(medicalRecordNumber) == false)
            {
                patientDisplayString.Append(medicalRecordNumber + ")");
            }

            TextBlock textBlockPaientDisplayString = XPSHelper.GetTextBlock(patientDisplayString.ToString(), HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
            Grid.SetColumn(textBlockPaientDisplayString, 1);
            Grid.SetRow(textBlockPaientDisplayString, 0);
            result.Children.Add(textBlockPaientDisplayString);

            ColumnDefinition colReportNo = new ColumnDefinition();
            colReportNo.Width = GridLength.Auto;
            result.ColumnDefinitions.Add(colReportNo);

            string reportNo = XMLHelper.GetString(panelSetOrderElement, "ReportNo");
            TextBlock textBlockReportNo = XPSHelper.GetTextBlock(reportNo, HorizontalAlignment.Right, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
            Grid.SetColumn(textBlockReportNo, 2);
            Grid.SetRow(textBlockReportNo, 0);
            result.Children.Add(textBlockReportNo);            

            return result;
		}

        private Grid GetSecondLineGrid(XElement accessionOrderElement, XElement panelSetElement)
        {
            Grid result = new Grid();
            result.Width = GridWidth;

            ColumnDefinition colPhysicianClientDisplay = new ColumnDefinition();
            colPhysicianClientDisplay.Width = GridLength.Auto;
            result.ColumnDefinitions.Add(colPhysicianClientDisplay);

            string physicianName = XMLHelper.GetString(accessionOrderElement, "PhysicianName");
            string clientName = XMLHelper.GetString(accessionOrderElement, "ClientName");
            string physicianClientDisplay = physicianName + " - " + clientName;

            TextBlock textBlockPhysicianClient = XPSHelper.GetTextBlock(physicianClientDisplay, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
            Grid.SetColumn(textBlockPhysicianClient, 0);
            Grid.SetRow(textBlockPhysicianClient, 0);
            result.Children.Add(textBlockPhysicianClient);

            string patientType = XMLHelper.GetString(accessionOrderElement, "PatientType");
            string primaryInsurance = XMLHelper.GetString(accessionOrderElement, "PrimaryInsurance");

            StringBuilder patientTypeString = new StringBuilder();
            if(patientType != "Not Selected") patientTypeString.Append(patientType);
            if (primaryInsurance != "Not Selected")
            {
                if (patientTypeString.Length != 0) patientTypeString.Append(" ");
                patientTypeString.Append(primaryInsurance);
            }

            ColumnDefinition colPatientType = new ColumnDefinition();
            colPhysicianClientDisplay.Width = GridLength.Auto;
            result.ColumnDefinitions.Add(colPatientType);

            TextBlock textBlockPatientType = XPSHelper.GetTextBlock(patientTypeString.ToString(), HorizontalAlignment.Right, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
            Grid.SetColumn(textBlockPatientType, 2);
            Grid.SetRow(textBlockPatientType, 0);
            result.Children.Add(textBlockPatientType);            
            
            return result;
        }

		private Grid GetPanelSetOrderCPTCodeGrid(XElement panelSetOrderCPTCode)
		{
			Grid result = new Grid();
			result.Width = GridWidth;

			ColumnDefinition colQuantity = new ColumnDefinition();
			colQuantity.Width = new GridLength(30);
			result.ColumnDefinitions.Add(colQuantity);

			string quantity = XMLHelper.GetString(panelSetOrderCPTCode, "Quantity");
			TextBlock textBlockQuantity = XPSHelper.GetTextBlock(quantity, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
			Grid.SetColumn(textBlockQuantity, 0);
			Grid.SetRow(textBlockQuantity, 0);
			result.Children.Add(textBlockQuantity);

			ColumnDefinition colCPTCode = new ColumnDefinition();
			colCPTCode.Width = new GridLength(75);
			result.ColumnDefinitions.Add(colCPTCode);

			string cptCode = XMLHelper.GetString(panelSetOrderCPTCode, "CPTCode");
			TextBlock textBlockCPTCode = XPSHelper.GetTextBlock(cptCode, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
			Grid.SetColumn(textBlockCPTCode, 1);
			Grid.SetRow(textBlockCPTCode, 0);
			result.Children.Add(textBlockCPTCode);

			ColumnDefinition colCPTModifier = new ColumnDefinition();
			colCPTModifier.Width = new GridLength(75);
			result.ColumnDefinitions.Add(colCPTModifier);

			string modifier = XMLHelper.GetString(panelSetOrderCPTCode, "Modifier");
			TextBlock textBlockModifier = XPSHelper.GetTextBlock(modifier, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
			Grid.SetColumn(textBlockModifier, 2);
			Grid.SetRow(textBlockModifier, 0);
			result.Children.Add(textBlockModifier);

			ColumnDefinition colBillableDescription = new ColumnDefinition();
			colBillableDescription.Width = new GridLength(240);
			result.ColumnDefinitions.Add(colBillableDescription);

			string description = XMLHelper.GetString(panelSetOrderCPTCode, "CodeableDescription");
			TextBlock textBlockDescription = XPSHelper.GetTextBlock(description, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
			Grid.SetColumn(textBlockDescription, 3);
			Grid.SetRow(textBlockDescription, 0);
			result.Children.Add(textBlockDescription);

			ColumnDefinition colBillableType = new ColumnDefinition();
			colBillableType.Width = new GridLength(240);
			result.ColumnDefinitions.Add(colBillableType);

			string type = XMLHelper.GetString(panelSetOrderCPTCode, "CodeableType");
			TextBlock textBlockType = XPSHelper.GetTextBlock(type, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
			Grid.SetColumn(textBlockType, 4);
			Grid.SetRow(textBlockType, 0);
			result.Children.Add(textBlockType);

			return result;
		}

        private Grid GetPanelSetOrderCPTCodeBillGrid(XElement panelSetOrderCPTCode)
        {
            Grid result = new Grid();
            result.Width = GridWidth;

            ColumnDefinition colCPTQuantity = new ColumnDefinition();
            colCPTQuantity.Width = new GridLength(30);
            result.ColumnDefinitions.Add(colCPTQuantity);                        

            string cptCodeQuantity = XMLHelper.GetString(panelSetOrderCPTCode, "Quantity");
            TextBlock textBlockQuantity = XPSHelper.GetTextBlock(cptCodeQuantity, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
            Grid.SetColumn(textBlockQuantity, 0);
            Grid.SetRow(textBlockQuantity, 0);
            result.Children.Add(textBlockQuantity);

            ColumnDefinition colCPTCode = new ColumnDefinition();
            colCPTCode.Width = new GridLength(75);
            result.ColumnDefinitions.Add(colCPTCode);

            string cptCode = XMLHelper.GetString(panelSetOrderCPTCode, "CPTCode");
            TextBlock textBlockCPTCode = XPSHelper.GetTextBlock(cptCode, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
            Grid.SetColumn(textBlockCPTCode, 1);
            Grid.SetRow(textBlockCPTCode, 0);
            result.Children.Add(textBlockCPTCode);

            ColumnDefinition colCPTModifier = new ColumnDefinition();
            colCPTModifier.Width = new GridLength(100);
            result.ColumnDefinitions.Add(colCPTModifier);    

            string modifier = XMLHelper.GetString(panelSetOrderCPTCode, "Modifier");
            TextBlock textBlockModifier = XPSHelper.GetTextBlock(modifier, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
            Grid.SetColumn(textBlockModifier, 2);
            Grid.SetRow(textBlockModifier, 0);
            result.Children.Add(textBlockModifier);

            return result;
        }

		private Grid GetCodeSectionHeader(string headerText)
		{
			Grid result = new Grid();
			result.Width = GridWidth;

			ColumnDefinition colCPTQuantity = new ColumnDefinition();
			colCPTQuantity.Width = new GridLength(GridWidth - (MarginSize * 3 * 2) - 10);
			result.ColumnDefinitions.Add(colCPTQuantity);

			TextBlock textBlockQuantity = XPSHelper.GetTextBlock(headerText, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
			Grid.SetColumn(textBlockQuantity, 0);
			Grid.SetRow(textBlockQuantity, 0);
			result.Children.Add(textBlockQuantity);

			return result;
		}
	}
}
