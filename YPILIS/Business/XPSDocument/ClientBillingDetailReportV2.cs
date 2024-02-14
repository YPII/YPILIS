using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml.Linq;
using YellowstonePathology.Document.Xps;
using System.Text;

namespace YellowstonePathology.Document
{
    class ClientBillingDetailReportV2
    {
        private const int FontSize = 6;
        private const int MarginSize = 3;
        private const double GridWidth = 680; // 96 * 8.5 - m_MarginSize * 2;

        private MultiPageDocument m_MultiPageDocument;
        private Business.XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportData m_ClientBillingDetailReportData;
        private DateTime m_PostDateStart;
        private DateTime m_PostDateEnd;

        public ClientBillingDetailReportV2(Business.XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportData clientBillingDetailReportData, DateTime postDateStart, DateTime postDateEnd)
        {
            this.m_ClientBillingDetailReportData = clientBillingDetailReportData;
            this.m_PostDateStart = postDateStart;
            this.m_PostDateEnd = postDateEnd;

            YellowstonePathology.Document.ClientBillingDetailHeader header = new ClientBillingDetailHeader(postDateStart, postDateEnd);
            this.m_MultiPageDocument = new MultiPageDocument(header);

            WriteDocument();
        }

        public FixedDocument FixedDocument
        {
            get { return m_MultiPageDocument.FixedDocument; }
        }

        private void WriteDocument()
        {
            foreach (Business.XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataAccessionOrder clientBillingDetailReportDataAccessionOrder in this.m_ClientBillingDetailReportData)
            {
                foreach (Business.XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataReport clientBillingDetailReportDataReport in clientBillingDetailReportDataAccessionOrder.ClientBillingDetailReportDataReports)
                {
                    if (this.OKToIncludePanelSetOnReport(clientBillingDetailReportDataReport) == true)
                    {
                        Grid accessionGrid = this.GetFirstLineGrid(clientBillingDetailReportDataAccessionOrder, clientBillingDetailReportDataReport);
                        this.m_MultiPageDocument.WriteRowContent(accessionGrid);

                        Grid panelSetGrid = this.GetSecondLineGrid(clientBillingDetailReportDataAccessionOrder, clientBillingDetailReportDataReport);
                        this.m_MultiPageDocument.WriteRowContent(panelSetGrid);

                        foreach (Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode in clientBillingDetailReportDataReport.PanelSetOrderCPTCodes)
                        {
                            Grid panelSetOrderCPTCodeGrid = this.GetPanelSetOrderCPTCodeGrid(panelSetOrderCPTCode);
                            this.m_MultiPageDocument.WriteRowContent(panelSetOrderCPTCodeGrid);
                        }

                        if (clientBillingDetailReportDataReport.PanelSetOrderCPTCodeBills.Count > 0)
                        {
                            Grid cptSummaryHeaderGrid = this.GetCodeSectionHeader("CPT Summary");
                            this.m_MultiPageDocument.WriteRowContent(cptSummaryHeaderGrid);
                        }

                        foreach (Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in clientBillingDetailReportDataReport.PanelSetOrderCPTCodeBills)
                        {
                            if (panelSetOrderCPTCodeBill.BillTo == "Client")
                            {
                                if (panelSetOrderCPTCodeBill.PostDate.HasValue)
                                {
                                    if (YellowstonePathology.Document.XMLHelper.IsDateElementInRange(panelSetOrderCPTCodeBill.PostDate.Value, this.m_PostDateStart, this.m_PostDateEnd) == true)
                                    {
                                        Grid nonClientPanelSetOrderCPTCodeBillGrid = this.GetPanelSetOrderCPTCodeBillGrid(panelSetOrderCPTCodeBill);
                                        this.m_MultiPageDocument.WriteRowContent(nonClientPanelSetOrderCPTCodeBillGrid);
                                    }
                                }
                            }
                        }

                        Grid blankGrid = new Grid();
                        blankGrid.Height = 30;
                        this.m_MultiPageDocument.WriteRowContent(blankGrid);
                    }
                }
            }
        }

        private bool OKToIncludePanelSetOnReport(Business.XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataReport clientBillingDetailReportDataReport)
        {
            bool result = false;
            if (this.HasChargeableClientPostDateInRange(clientBillingDetailReportDataReport) == true)
            {
                if (this.HasNonZeroQuantity(clientBillingDetailReportDataReport.PanelSetOrderCPTCodeBills) == true)
                {
                    result = true;
                }
            }
            return result;
        }

        private bool HasNonZeroQuantity(List<Business.Test.PanelSetOrderCPTCodeBill> panelSetOrderCPTCodeBills)
        {
            bool result = false;
            int sumOfQuantity = (from nd in panelSetOrderCPTCodeBills
                                 select nd.Quantity).Sum();
            if (sumOfQuantity != 0) result = true;
            return result;
        }

        private bool HasChargeableClientPostDateInRange(Business.XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataReport clientBillingDetailReportDataReport)
        {
            bool result = false;
            if (clientBillingDetailReportDataReport.NoCharge == false)
            {
                foreach (Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in clientBillingDetailReportDataReport.PanelSetOrderCPTCodeBills)
                {
                    if (panelSetOrderCPTCodeBill.PostDate.HasValue)
                    {
                        if (YellowstonePathology.Document.XMLHelper.IsDateElementInRange(panelSetOrderCPTCodeBill.PostDate.Value, this.m_PostDateStart, this.m_PostDateEnd) == true)
                        {
                            if (panelSetOrderCPTCodeBill.BillTo == "Client")
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }

        private bool HasCLNTMedicareCodes(List<XElement> medicareCodeElements)
        {
            bool result = false;
            YellowstonePathology.Business.Billing.Model.MedicareCodeCollection medicareCodeCollection = Business.Billing.Model.MedicareCodeCollection.GetAll();
            foreach (XElement medicareCodeElement in medicareCodeElements)
            {
                string cptCode = medicareCodeElement.Element("CPTCode").Value;
                if (medicareCodeCollection.IsMedicareCode(cptCode) == true)
                {
                    if (medicareCodeElement.Element("BillBy").Value == "CLNT")
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        private Grid GetFirstLineGrid(Business.XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataAccessionOrder clientBillingDetailReportDataAccessionOrder,
            Business.XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataReport clientBillingDetailReportDataReport)
        {
            Grid result = new Grid();
            result.Width = GridWidth;

            ColumnDefinition colDateOfSerice = new ColumnDefinition();
            colDateOfSerice.Width = GridLength.Auto;
            result.ColumnDefinitions.Add(colDateOfSerice);

            string dateOfServiceString = string.Empty;
            if (clientBillingDetailReportDataAccessionOrder.DateOfService.HasValue == true)
            {
                dateOfServiceString = clientBillingDetailReportDataAccessionOrder.DateOfService.Value.ToString("MM/dd/yyyy");
            }

            TextBlock textBlockDateOfService = XPSHelper.GetTextBlock(dateOfServiceString, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
            textBlockDateOfService.FontWeight = FontWeight.FromOpenTypeWeight(700);
            Grid.SetColumn(textBlockDateOfService, 0);
            Grid.SetRow(textBlockDateOfService, 0);
            result.Children.Add(textBlockDateOfService);

            ColumnDefinition colPatientDisplayString = new ColumnDefinition();
            colPatientDisplayString.Width = new GridLength(20, GridUnitType.Star);
            result.ColumnDefinitions.Add(colPatientDisplayString);

            StringBuilder patientDisplayString = new StringBuilder();
            patientDisplayString.Append(YellowstonePathology.Business.Patient.Model.Patient.GetLastFirstDOBDisplayName(clientBillingDetailReportDataAccessionOrder.PFirstName, clientBillingDetailReportDataAccessionOrder.PLastName, clientBillingDetailReportDataAccessionOrder.PBirthdate));

            if (string.IsNullOrEmpty(clientBillingDetailReportDataAccessionOrder.SvhAccount) == false)
            {
                patientDisplayString.Append("  (" + clientBillingDetailReportDataAccessionOrder.SvhAccount);
                if (string.IsNullOrEmpty(clientBillingDetailReportDataAccessionOrder.SvhMedicalRecord) == false)
                {
                    patientDisplayString.Append("/");
                }
                else
                {
                    patientDisplayString.Append(")");
                }
            }

            if (string.IsNullOrEmpty(clientBillingDetailReportDataAccessionOrder.SvhMedicalRecord) == false)
            {
                patientDisplayString.Append(clientBillingDetailReportDataAccessionOrder.SvhMedicalRecord + ")");
            }

            TextBlock textBlockPatientDisplayString = XPSHelper.GetTextBlock(patientDisplayString.ToString(), HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
            textBlockPatientDisplayString.FontWeight = FontWeight.FromOpenTypeWeight(700);
            Grid.SetColumn(textBlockPatientDisplayString, 1);
            Grid.SetRow(textBlockPatientDisplayString, 0);
            result.Children.Add(textBlockPatientDisplayString);

            ColumnDefinition colReportNo = new ColumnDefinition();
            colReportNo.Width = GridLength.Auto;
            result.ColumnDefinitions.Add(colReportNo);

            TextBlock textBlockReportNo = XPSHelper.GetTextBlock(clientBillingDetailReportDataReport.ReportNo, HorizontalAlignment.Right, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
            Grid.SetColumn(textBlockReportNo, 2);
            Grid.SetRow(textBlockReportNo, 0);
            result.Children.Add(textBlockReportNo);

            return result;
        }

        private Grid GetSecondLineGrid(Business.XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataAccessionOrder clientBillingDetailReportDataAccessionOrder,
            Business.XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataReport clientBillingDetailReportDataReport)
        {
            Grid result = new Grid();
            result.Width = GridWidth;

            ColumnDefinition colPhysicianClientDisplay = new ColumnDefinition();
            colPhysicianClientDisplay.Width = GridLength.Auto;
            result.ColumnDefinitions.Add(colPhysicianClientDisplay);

            string physicianClientDisplay = clientBillingDetailReportDataAccessionOrder.PhysicianName + " - " + clientBillingDetailReportDataAccessionOrder.ClientName;

            TextBlock textBlockPhysicianClient = XPSHelper.GetTextBlock(physicianClientDisplay, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
            Grid.SetColumn(textBlockPhysicianClient, 0);
            Grid.SetRow(textBlockPhysicianClient, 0);
            result.Children.Add(textBlockPhysicianClient);

            StringBuilder patientTypeString = new StringBuilder();
            if (clientBillingDetailReportDataAccessionOrder.PatientType != "Not Selected") patientTypeString.Append(clientBillingDetailReportDataAccessionOrder.PatientType);
            if (clientBillingDetailReportDataAccessionOrder.PrimaryInsurance != "Not Selected")
            {
                if (patientTypeString.Length != 0) patientTypeString.Append(" ");
                patientTypeString.Append(clientBillingDetailReportDataAccessionOrder.PrimaryInsurance);
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

        private Grid GetPanelSetOrderCPTCodeGrid(Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode)
        {
            Grid result = new Grid();
            result.Width = GridWidth;

            ColumnDefinition colQuantity = new ColumnDefinition();
            colQuantity.Width = new GridLength(30);
            result.ColumnDefinitions.Add(colQuantity);

            string quantity = panelSetOrderCPTCode.Quantity.ToString();
            TextBlock textBlockQuantity = XPSHelper.GetTextBlock(quantity, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
            Grid.SetColumn(textBlockQuantity, 0);
            Grid.SetRow(textBlockQuantity, 0);
            result.Children.Add(textBlockQuantity);

            ColumnDefinition colCPTCode = new ColumnDefinition();
            colCPTCode.Width = new GridLength(75);
            result.ColumnDefinitions.Add(colCPTCode);

            TextBlock textBlockCPTCode = XPSHelper.GetTextBlock(panelSetOrderCPTCode.CPTCode, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
            Grid.SetColumn(textBlockCPTCode, 1);
            Grid.SetRow(textBlockCPTCode, 0);
            result.Children.Add(textBlockCPTCode);

            ColumnDefinition colCPTModifier = new ColumnDefinition();
            colCPTModifier.Width = new GridLength(75);
            result.ColumnDefinitions.Add(colCPTModifier);

            TextBlock textBlockModifier = XPSHelper.GetTextBlock(panelSetOrderCPTCode.Modifier, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
            Grid.SetColumn(textBlockModifier, 2);
            Grid.SetRow(textBlockModifier, 0);
            result.Children.Add(textBlockModifier);

            ColumnDefinition colBillableDescription = new ColumnDefinition();
            colBillableDescription.Width = new GridLength(240);
            result.ColumnDefinitions.Add(colBillableDescription);

            TextBlock textBlockDescription = XPSHelper.GetTextBlock(panelSetOrderCPTCode.CodeableDescription, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(MarginSize * 10, MarginSize, MarginSize * 3, MarginSize));
            Grid.SetColumn(textBlockDescription, 3);
            Grid.SetRow(textBlockDescription, 0);
            result.Children.Add(textBlockDescription);

            ColumnDefinition colBillableType = new ColumnDefinition();
            colBillableType.Width = new GridLength(240);
            result.ColumnDefinitions.Add(colBillableType);

            TextBlock textBlockType = XPSHelper.GetTextBlock(panelSetOrderCPTCode.CodeableType, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
            Grid.SetColumn(textBlockType, 4);
            Grid.SetRow(textBlockType, 0);
            result.Children.Add(textBlockType);

            return result;
        }

        private Grid GetPanelSetOrderCPTCodeBillGrid(Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill)
        {
            Grid result = new Grid();
            result.Width = GridWidth;

            ColumnDefinition colCPTQuantity = new ColumnDefinition();
            colCPTQuantity.Width = new GridLength(30);
            result.ColumnDefinitions.Add(colCPTQuantity);

            string cptCodeQuantity = panelSetOrderCPTCodeBill.Quantity.ToString();
            TextBlock textBlockQuantity = XPSHelper.GetTextBlock(cptCodeQuantity, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
            Grid.SetColumn(textBlockQuantity, 0);
            Grid.SetRow(textBlockQuantity, 0);
            result.Children.Add(textBlockQuantity);

            ColumnDefinition colCPTCode = new ColumnDefinition();
            colCPTCode.Width = new GridLength(75);
            result.ColumnDefinitions.Add(colCPTCode);

            TextBlock textBlockCPTCode = XPSHelper.GetTextBlock(panelSetOrderCPTCodeBill.CPTCode, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
            Grid.SetColumn(textBlockCPTCode, 1);
            Grid.SetRow(textBlockCPTCode, 0);
            result.Children.Add(textBlockCPTCode);

            ColumnDefinition colCPTModifier = new ColumnDefinition();
            colCPTModifier.Width = new GridLength(100);
            result.ColumnDefinitions.Add(colCPTModifier);

            TextBlock textBlockModifier = XPSHelper.GetTextBlock(panelSetOrderCPTCodeBill.Modifier, HorizontalAlignment.Left, VerticalAlignment.Top, new Thickness(MarginSize * 3, MarginSize, MarginSize * 3, MarginSize));
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

