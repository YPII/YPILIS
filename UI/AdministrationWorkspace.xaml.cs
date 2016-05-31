using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Printing;
using System.IO;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Xml.XPath;
using System.Xml.Linq;
using System.ServiceModel;
using YellowstonePathology.Business.Helper;
using System.Collections.ObjectModel;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;

namespace YellowstonePathology.UI
{    
    public partial class AdministrationWorkspace : System.Windows.Controls.UserControl
    {                
        static AdministrationWorkspace m_Instance;

        private Nullable<DateTime> m_WorkDate;

        private AdministrationWorkspace()
        {
            this.m_WorkDate = DateTime.Now;			

            InitializeComponent();

            this.DataContext = this;            
        }

        public Nullable<DateTime> WorkDate
        {
            get { return this.m_WorkDate; }
        }

		private void m_BarcodeScanPort_HistologySlideScanReceived(Business.BarcodeScanning.HistologySlide histologySlide)
        {
            
        }                
        
        public static AdministrationWorkspace Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new AdministrationWorkspace();
                }
                return m_Instance;
            }
        }

        private void ButtonBuildJson_Click(object sender, RoutedEventArgs e)
        {
            /*YellowstonePathology.Business.Billing.Model.CptCodeCollection cptCodes = YellowstonePathology.Business.Billing.Model.CptCodeCollection.GetAll();
            StringBuilder result = new StringBuilder();
            result.Append("[");

            foreach (YellowstonePathology.Business.Billing.Model.CptCode cptCode in cptCodes)
            {
                YellowstonePathology.Business.Persistence.JSONObjectWriter.WriteIndented(result, cptCode, 1);
                result.Append(",");
            }

            if (result.Length > 1)
            {
                result.Remove(result.Length - 1, 1);
            }
            result.AppendLine();
            result.AppendLine("]");

            using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(@"C:\TEMP\CPTCodeDefinitions.txt", false))
            {
                streamWriter.Write(result);
            }*/
            YellowstonePathology.Business.Billing.Model.CptCodeCollection result = YellowstonePathology.Business.Billing.Model.JSONCPTCodeFactory.DeserializeJSONFile();
            MessageBox.Show("Done");
        }

        private void ButtonPOCRetension_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Reports.POCRetensionReport report = new Business.Reports.POCRetensionReport(DateTime.Parse("3/26/2011"), DateTime.Parse("5/04/2011"));
            System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();
            printDialog.ShowDialog();
            printDialog.PrintDocument(report.DocumentPaginator, "POC Retention Report for: ");
        }

        private void ButtonCytologyUnsatLetters_Click(object sender, RoutedEventArgs e)
        {
            //YellowstonePathology.Reports.Cytology.CytologyAbnormalUnsatLetter unsatLetters = new YellowstonePathology.Reports.Cytology.CytologyAbnormalUnsatLetter(0, DateTime.Parse("3/1/2011"), DateTime.Parse("3/31/11"), "DATE");
            //unsatLetters.CreateReports();
            //unsatLetters.FaxReports();  
        }

        private void ButtonConvertXPSToPDF_Click(object sender, RoutedEventArgs e)
        {
            //YellowstonePathology.Business.DataContext.YpiData ypiData = new Business.DataContext.YpiData();
            //List<YellowstonePathology.Business.ReportNo> reportNos = ypiData.GetReportNumbers().ToList<YellowstonePathology.Business.ReportNo>();

			YellowstonePathology.Business.ReportNoCollection reportNos = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetReportNumbers();
            foreach (YellowstonePathology.Business.ReportNo reportNo in reportNos)
            {
                bool result = YellowstonePathology.Business.Helper.FileConversionHelper.ConvertXpsDocumentToPdf(reportNo.Value);
                YellowstonePathology.Business.DataContext.YpiData dataContext = new Business.DataContext.YpiData();                
            }
        }        

        private void ButtonSqlXmlTest_Click(object sender, RoutedEventArgs e)
        {
            //YellowstonePathology.Business.Gateway.SerializeToSql.BillingAccessionSerialization gw = new Business.Gateway.SerializeToSql.BillingAccessionSerialization();
            //gw.Testing123();
		}        

        private void ButtonShowCuttingStationWindow_Click(object sender, RoutedEventArgs e)
        {
			//YellowstonePathology.Business.User.SystemIdentity identity = Business.User.SystemIdentity.Instance;
		}        

        private void ButtonPublishCase_Click(object sender, RoutedEventArgs e)
        {
            //YellowstonePathology.Business.Document.PlateletAssociatedAntibodiesReport report = new Business.Document.PlateletAssociatedAntibodiesReport();
            //report.Render(2013003414, "F13-99", Business.Document.ReportSaveModeEnum.Normal);
            //report.Publish();


            //YellowstonePathology.Business.Document.ProthrombinReport report = new Business.Document.ProthrombinReport();
            //report.Publish(2012014504, "M12-1145", Business.Document.ReportSaveModeEnum.Normal);

            //YellowstonePathology.Business.Document.CytologyReport cytology = new Business.Document.CytologyReport();
            //cytology.Publish(2009055679, "P09-18568", Business.Document.ReportSaveModeEnum.Normal);

            //YellowstonePathology.Business.Document.HER2ByFishReport her2 = new Business.Document.HER2ByFishReport();
            //her2.Publish(2008018916, "M10-2365", Business.Document.ReportSaveModeEnum.Normal);

            //YellowstonePathology.Business.Document.SurgicalReport surgical = new Business.Document.SurgicalReport();
            //surgical.Publish(2011009851, "S11-5008", Business.Document.ReportSaveModeEnum.Normal);
        }        

        private void ButtonDataMatrixBarcodeTest_Click(object sender, RoutedEventArgs e)
        {
            
        }        

        private void ButtonMolecularLabel_Click(object sender, RoutedEventArgs e)
        {
        }                

        private void ButtonCreateXPSDocs_Click(object sender, RoutedEventArgs e)
        {
            //YellowstonePathology.Business.DataContext.YpiData dataContext = new Business.DataContext.YpiData();
            //List<YellowstonePathology.Business.ReportNo> reportNumbers = dataContext.GetReportNumbers().ToList<YellowstonePathology.Business.ReportNo>();

			YellowstonePathology.Business.ReportNoCollection reportNumbers = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetReportNumbers();
			foreach (YellowstonePathology.Business.ReportNo reportNo in reportNumbers)
            {
				YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(reportNo.Value);
				string xpsDoc = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNameXPS(orderIdParser);
                if (System.IO.File.Exists(xpsDoc) == false)
                {
					YellowstonePathology.Business.Document.CaseDocument.SaveDocAsXPS(orderIdParser);
                }
            }
        }

        private void ButtonFix2001_Click(object sender, RoutedEventArgs e)
        {
            /*
            string[] files = System.IO.Directory.GetFiles(@"\\cfileserver\Documents\Surgical\2001\08000-08999");
            foreach (string file in files)
            {
                string[] slashSplit = file.Split('\\');
                string[] dotSplit = slashSplit[slashSplit.Length - 1].Split('.');
                string reportNo = dotSplit[0];
                string path = YellowstonePathology.Business.Document.CaseDocument.GetCasePath(reportNo) + slashSplit[slashSplit.Length - 1];
                try
                {
                    System.IO.File.Move(file, path);
                }
                catch (Exception ex)
                {

                }
                //int number = YellowstonePathology.Business.ReportNo.GetNumber(reportNo);

            }
            */
        }

        private void ButtonXpsToTiff_Click(object sender, RoutedEventArgs e)
        {
            /*YellowstonePathology.Business.DataContext.YpiData dataContext = new YellowstonePathology.Business.DataContext.YpiData();
            YellowstonePathology.Business.Repository.CytologyRepository repository = new YellowstonePathology.Business.Repository.CytologyRepository(dataContext);

            List<YellowstonePathology.Business.ReportNo> reportNoList = repository.GetReportNumbers();

            foreach (YellowstonePathology.Business.ReportNo reportNo in reportNoList)
            {
                //YellowstonePathology.Business.Helper.FileConversionHelper.ConvertXpsDocumentToTiff(reportNo.Number);
				YellowstonePathology.Business.Helper.FileConversionHelper.SaveXpsReportToTiff(reportNo.Number, false);
			}*/

			YellowstonePathology.Business.ReportNoCollection reportNoCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetReportNumbers();
			foreach (YellowstonePathology.Business.ReportNo reportNo in reportNoCollection)
			{
                YellowstonePathology.Business.Helper.FileConversionHelper.SaveXpsReportToTiff(reportNo.Value);
			}
       }

        private void ComprehensiveCareReports_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Stuff_Click(object sender, RoutedEventArgs e)
        {
            
		}

		private void GrossWorkspace_Click(object sender, RoutedEventArgs e)
		{
		}

        private void PrintRequisition_Click(object sender, RoutedEventArgs e)
        {
            System.Printing.PrintServer printServer = new System.Printing.LocalPrintServer();
            System.Printing.PrintQueue printQueue = printServer.GetPrintQueue(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.RequisitionPrinter);            

            Client.StandardRequisition requisitionHeader = new Client.StandardRequisition(983);
            requisitionHeader.Print(2, printQueue);
        }

        private void XpsToTiff_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Helper.FileConversionHelper.SaveXpsReportToTiff("S11-11826");
        }       		

        private void TestBillingData_Click(object sender, RoutedEventArgs e)
        {         
            /*
            YellowstonePathology.UI.Login.Icd9CodeDialog dialog = new Login.Icd9CodeDialog();
            dialog.SetSearchToMasterAccessionNo(2011023315);
            dialog.GetBillingAccession();
            dialog.ShowDialog();
            */
        }

        private void SerializeAccessionOrder_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ButtonShowTestWindow_Click(object sender, RoutedEventArgs e)
        {            
            TestWindow window = new TestWindow();
            window.ShowDialog();            
        }

		private void ButtonDeleteTest_Click(object sender, RoutedEventArgs e)
		{
            //YellowstonePathology.Business.SpecialStain.StainResultItemCollection item = new Business.SpecialStain.StainResultItemCollection();
            //item.Remove(

			/*YellowstonePathology.Business.Test.PanelOrderCollection panelOrderItemCollection = new Business.Test.PanelOrderCollection();
			YellowstonePathology.Business.Test.PanelOrder panelOrder = new Business.Test.PanelOrder(19, "ReportNo", 5091);
			panelOrder.PanelOrderId = 1;
			YellowstonePathology.Business.Test.Model.TestOrder testOrder = new YellowstonePathology.Business.Test.Model.TestOrder();
			testOrder.PanelOrderId = 1;
			testOrder.TestOrderId = 2;
			object obj = testOrder;

			YellowstonePathology.Business.Test.Model.TestOrder testOrder1 = new YellowstonePathology.Business.Test.Model.TestOrder();
			testOrder1.PanelOrderId = 1;
			testOrder1.TestOrderId = 3;

			panelOrder.TestOrderCollection.Add(testOrder);
			panelOrder.TestOrderCollection.Add(testOrderI1);

			panelOrderItemCollection.Add(panelOrder);

			panelOrderItemCollection.Remove(obj);
			foreach (YellowstonePathology.Business.Test.PanelOrder item in panelOrderItemCollection)
			{
				item.TestOrderCollection.Remove(testOrder);
			}*/
		}

		private void ButtonOpenCytologyCase_Click(object sender, RoutedEventArgs e)
		{
			
		}

		private void ButtonHL7Response_Click(object sender, RoutedEventArgs e)
		{
		}

        private void ButtonTestYpiConnect_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ButtonOpenVerificationWindow_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonProcessBillingFile_Click(object sender, RoutedEventArgs e)
        {
        }

		private void ButtonIncomingBillingData_Click(object sender, RoutedEventArgs e)
		{
			
		}

        private void ButtonSerumLabels_Click(object sender, RoutedEventArgs e)
        {
            System.Printing.PrintServer printServer = new System.Printing.LocalPrintServer();
            string printer = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.ContainerLabelPrinter;
            System.Printing.PrintQueue printQueue = printServer.GetPrintQueue(printer);

            YellowstonePathology.UI.Login.SerumLabel label = new Login.SerumLabel("Serum", "84165-26");                        

            System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();
            printDialog.PrintTicket.CopyCount = 50;
            printDialog.PrintTicket.PageMediaSize = new PageMediaSize(384, 96);
            printDialog.PrintQueue = printQueue;

            printDialog.PrintDocument(label.DocumentPaginator, "Labels");            
        }

        private void ButtonFormalinAddedLabels_Click(object sender, RoutedEventArgs e)
        {
            System.Printing.PrintServer printServer = new System.Printing.LocalPrintServer();
            string printer = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.ContainerLabelPrinter;
            System.Printing.PrintQueue printQueue = printServer.GetPrintQueue(printer);
            
            YellowstonePathology.UI.Login.FormalinAddedLabel label = new Login.FormalinAddedLabel();
            System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();
            printDialog.PrintTicket.CopyCount = 50;
            printDialog.PrintTicket.PageMediaSize = new PageMediaSize(384, 96);
            printDialog.PrintQueue = printQueue;

            printDialog.PrintDocument(label.DocumentPaginator, "Labels");            
        }

        private void ButtonIFLabels_Click(object sender, RoutedEventArgs e)
        {
            System.Printing.PrintServer printServer = new System.Printing.LocalPrintServer();
            string printer = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.ContainerLabelPrinter;
            System.Printing.PrintQueue printQueue = printServer.GetPrintQueue(printer);

            YellowstonePathology.UI.Login.IFELabel label = new Login.IFELabel();
            System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();
            printDialog.PrintTicket.CopyCount = 50;
            printDialog.PrintTicket.PageMediaSize = new PageMediaSize(384, 96);
            printDialog.PrintQueue = printQueue;

            printDialog.PrintDocument(label.DocumentPaginator, "Labels");            
        }

        private void ButtonUrineLabels_Click(object sender, RoutedEventArgs e)
        {
            System.Printing.PrintServer printServer = new System.Printing.LocalPrintServer();
            System.Printing.PrintQueue printQueue = printServer.GetPrintQueue(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.ContainerLabelPrinter);
            
            YellowstonePathology.UI.Login.SerumLabel serumLabel = new Login.SerumLabel("Urine", "84166-26");
            System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();

            printDialog.PrintTicket.CopyCount = 50;
            printDialog.PrintTicket.PageMediaSize = new PageMediaSize(384, 96);
            printDialog.PrintQueue = printQueue;
            printDialog.PrintDocument(serumLabel.DocumentPaginator, "Urine Labels");           
        }

		private void ButtonAccessionSlideOrderTracking_Click(object sender, RoutedEventArgs e)
		{
			MaterialTrackingDialog dialog = new MaterialTrackingDialog();
			dialog.ShowDialog();
		}

        private void ButtonSpellCheckTesting_Click(object sender, RoutedEventArgs e)
        {
            SpellCheckingTest test = new SpellCheckingTest();
            test.ShowDialog();
        }

        private void ButtonPhysicianEntry_Click(object sender, RoutedEventArgs e)
        {
			Client.ProviderLookupDialog providerLookupDialog = new Client.ProviderLookupDialog();
			providerLookupDialog.ShowDialog();
        }

		private void ButtonListInvalidShortcut_Click(object sender, RoutedEventArgs e)
		{
			
		}

		private void ButtonProcessSvhBillingFile_Click(object sender, RoutedEventArgs e)
		{
			//YellowstonePathology.Business.Billing.IncomingBillingDataProcessor incomingBillingDataProcessor = new Business.Billing.IncomingBillingDataProcessor();
			//incomingBillingDataProcessor.Process();
			//MessageBox.Show("Finished");
		}        

        private void ButtonBarcodeTesting_Click(object sender, RoutedEventArgs e)
        {
            System.Printing.PrintServer printServer = new System.Printing.LocalPrintServer();
            System.Printing.PrintQueue printQueue = printServer.GetPrintQueue(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HistologySlideLabelPrinter);            

            YellowstonePathology.Business.BarcodeScanning.HistologySlide slide = new YellowstonePathology.Business.BarcodeScanning.HistologySlide("12345678", "S11-17715", "1A4", "Pickles", "Mashed Potatoes", "Billings");
            HistologySlideLabelDocument histologySlideLabelDocument = new HistologySlideLabelDocument(slide, 4);
            histologySlideLabelDocument.Print(printQueue);            
        }

        private void ButtonPageScanningTest_Click(object sender, RoutedEventArgs e)
        {
            //PageScanningTestDialog dialog = new PageScanningTestDialog();
            //dialog.ShowDialog();
        }

        private void ButtonGetPatientHistoryTest_Click(object sender, RoutedEventArgs e)
        {
			//YellowstonePathology.Business.MessageQueues.GetPatientHistoryCommand cmd = new Business.MessageQueues.GetPatientHistoryCommand();
			//cmd.SetCommandData(2012025485);
			//cmd.Execute();
		}

		private void ButtonSetTestOrderCptCodes_Click(object sender, RoutedEventArgs e)
		{
			//YellowstonePathology.Business.MessageQueues.SetTestOrderCptCodesCommand cmd = new Business.MessageQueues.SetTestOrderCptCodesCommand();
			//cmd.SetCommandData("2013014815", "1001286386", "1001286386.TO1", "Helicobacter pylori");
			//cmd.Execute();

			//YellowstonePathology.Business.MessageQueues.AcknowledgePanelOrderCommand cmd = new Business.MessageQueues.AcknowledgePanelOrderCommand();
			//cmd.SetCommandData(2013004097, 1000987473, "S13-1905", 5091, DateTime.Today, DateTime.Now);

			//YellowstonePathology.Business.MessageQueues.AmmendmentFromResultMessageCommand cmd = new Business.MessageQueues.AmmendmentFromResultMessageCommand();
			//cmd.SetCommandData(2013003584, "M13-407", 25, "S13-1671");
			//cmd.Execute();

			//cmd.SetCommandData(2013003746, "M13-416", 46, "S13-1738");
			//cmd.Execute();

			//cmd.SetCommandData(2013003324, "M13-374", 46, "S13-1533");
			//cmd.Execute();

			//MessageBox.Show("OK");
		}

        private void ButtonHL7StatusTest_Click(object sender, RoutedEventArgs e)
        {
            //YellowstonePathology.Business.ClientOrder.Model.UniversalServiceIdCollection universalServiceIdCollection = YellowstonePathology.Business.ClientOrder.Model.UniversalServiceIdCollection.GetAll();
            //YellowstonePathology.Business.ClientOrder.Model.UniversalServiceId universalServiceId = universalServiceIdCollection.GetByTestCode(this.m_ClientOrderReceivingHandler.ClientOrder.OrderType);

            //YellowstonePathology.Business.HL7View.EPIC.EpicStatusMessage statusMessage = new Business.HL7View.EPIC.EpicStatusMessage("d13a97e1-c228-404a-9cae-97bd7556f180", Business.HL7View.OrderStatusEnum.InProcess, universalServiceId);
            //statusMessage.Send();
        }

		private void ButtonTestInMemoryXml_Click(object sender, RoutedEventArgs e)
		{
			//ReportViewer reportViewer = new ReportViewer();
			//reportViewer.ShowDocument("S11-19011");
			//reportViewer.ShowDialog();
		}        

        private void ButtonPatientInfo_Click(object sender, RoutedEventArgs e)
        {
            //YellowstonePathology.Business.Billing.IncomingBillingDataProcessor incomingBillingDataProcessor = new Business.Billing.IncomingBillingDataProcessor();
            //incomingBillingDataProcessor.Process();
        }

        private void ButtonPDFToXPSConversion_Click(object sender, RoutedEventArgs e)
        {            
            Process process = new Process();
            process.StartInfo.FileName = @"C:\Program Files\gs\gs9.02\bin\GSWIN32C.exe";
            process.StartInfo.Arguments = "-dNOPAUSE -q -g300x300 -sDEVICE=tiffg4 -dBATCH -sOutputFile=test.tif test.pdf";
            process.StartInfo.RedirectStandardOutput = true; 
            process.StartInfo.CreateNoWindow = true; 
            process.StartInfo.UseShellExecute = false; 
            process.Start(); 
            process.StandardOutput.ReadToEnd(); 
            process.WaitForExit();            
        }

        private void ButtonParsePsa_Click(object sender, RoutedEventArgs e)
        {
            ParsePsaAccessionsWindow window = new ParsePsaAccessionsWindow();
            window.ShowDialog();
        }

		private void ButtonReportSlides_Click(object sender, RoutedEventArgs e)
        {
			MaterialTrackingReportNoDialog dialog = new MaterialTrackingReportNoDialog();
			dialog.ShowDialog();
        }        

        private void ButtonXPSTest_Click(object sender, RoutedEventArgs e)
        {            
            //Type workingType = typeof(YellowstonePathology.Business.Test.PanelSetOrderArupBraf);
            //while (workingType.Name != "Object")
            //{                
            //    Type baseType = workingType.BaseType;
            //    if (baseType.Name == "PanelSetOrder")
            //    {
                    
            //    }
            //}          
        }

        private void ButtonPqrs_Click(object sender, RoutedEventArgs e)
        {
            //string path = System.IO.Path.GetTempPath();
            //Client.WebBrowser browser = new Client.WebBrowser();
            //browser.ShowDialog();

			//YellowstonePathology.Business.Test.AccessionOrderExplorer accessionOrderExplorer = new Business.Test.AccessionOrderExplorer(true);
			//accessionOrderExplorer.SetSearchByReportNo("S12-9461"); 
			//accessionOrderExplorer.Execute();

			//YellowstonePathology.UI.Surgical.PQRSMeasureDialog dlg = new Surgical.PQRSMeasureDialog();
			//dlg.HandlePqrs(accessionOrderExplorer.AccessionOrder);

			//YellowstonePathology.Business.Test.AccessionOrderExplorer accessionOrderExplorer = new Business.Test.AccessionOrderExplorer(true);

			//accessionOrderExplorer.SetSearchByReportNo("14-7028.S"); //eso
			//accessionOrderExplorer.Execute();

			//YellowstonePathology.UI.Surgical.PQRSMeasureDialog dlg = new Surgical.PQRSMeasureDialog();
			//dlg.HandlePqrs(accessionOrderExplorer.AccessionOrder, "14-7028.S");
/*
			accessionOrderExplorer.SetSearchByReportNo("S12-9337"); //pros
			accessionOrderExplorer.Execute();

			dlg = new Surgical.PQRSMeasureDialog();
			dlg.HandlePqrs(accessionOrderExplorer.AccessionOrder);

			accessionOrderExplorer.SetSearchByReportNo("S12-9319"); //colo
			accessionOrderExplorer.Execute();

			dlg = new Surgical.PQRSMeasureDialog();
			dlg.HandlePqrs(accessionOrderExplorer.AccessionOrder);

			accessionOrderExplorer.SetSearchByReportNo("S12-9303"); //breast
			accessionOrderExplorer.Execute();

			dlg = new Surgical.PQRSMeasureDialog();
			dlg.HandlePqrs(accessionOrderExplorer.AccessionOrder);*/
		}        

        private void ButtonFlowResultTest_Click(object sender, RoutedEventArgs e)
        {
			//XElement accessionOrderDocument = YellowstonePathology.Business.Gateway.XmlGateway.GetAccessionOrder(2012015454); // (2012012026);
			//XElement specimenOrderDocument = YellowstonePathology.Business.Gateway.XmlGateway.GetSpecimenOrder(2012015454); // (2012012026);
			//XElement clientOrderDocument = YellowstonePathology.Business.Gateway.XmlGateway.GetClientOrders(2012015454); // (2012012026); S12-6205
			//XElement caseNotesDocument = YellowstonePathology.Business.Gateway.XmlGateway.GetOrderComments(2012015454);
			//YellowstonePathology.UI.Login.AccessionOrderDataSheetData data = new UI.Login.AccessionOrderDataSheetData("S12-7998", accessionOrderDocument, specimenOrderDocument, clientOrderDocument, caseNotesDocument);
        }

        private void ButtonMtDohTest_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonClientOrderSubmitTest_Click(object sender, RoutedEventArgs e)
        {                        
            //YellowstonePathology.YpiConnect.Service.FlowAccessionGateway gateway = new YpiConnect.Service.FlowAccessionGateway(); 
            //XElement accessionDocument = gateway.GetAccessionDocument(2012015317);
            //accessionDocument.Save(@"c:\testing\FlowLeukemiaReportData.xml");

            //YellowstonePathology.Document.Result.Data.LeukemiaLymphomaReportData reportData = new YellowstonePathology.Document.Result.Data.LeukemiaLymphomaReportData(accessionDocument);
            //YellowstonePathology.Document.Result.Xps.LeukemiaLymphomaReport report = new YellowstonePathology.Document.Result.Xps.LeukemiaLymphomaReport(reportData);

            //XpsDocumentViewer xpsDocumentViewer = new XpsDocumentViewer();
            //xpsDocumentViewer.LoadDocument(report.FixedDocument);
            //xpsDocumentViewer.ShowDialog();

            /*
            YellowstonePathology.Business.User.SystemIdentity systemIdentity = Business.User.SystemIdentity.Instance;
            TestPage testPage = new TestPage(systemIdentity);
            testPage.ShowDialog();
			*/

            /*
            YellowstonePathology.Business.Document.SurgicalReport report1 = new Business.Document.SurgicalReport();
            report1.Publish(2012014459, "S12-7439", Business.Document.ReportSaveModeEnum.Normal);

            YellowstonePathology.Business.Document.SurgicalReport report2 = new Business.Document.SurgicalReport();
            report2.Publish(2012014464, "S12-7444", Business.Document.ReportSaveModeEnum.Normal);

            YellowstonePathology.Business.Document.SurgicalReport report3 = new Business.Document.SurgicalReport();
            report3.Publish(2012014525, "S12-7478", Business.Document.ReportSaveModeEnum.Normal);
            */

            /*
            YellowstonePathology.Business.Document.SurgicalReport report4 = new Business.Document.SurgicalReport();
            report4.Publish(2012014016, "S12-7220", Business.Document.ReportSaveModeEnum.Normal);

            YellowstonePathology.Business.Document.SurgicalReport report5 = new Business.Document.SurgicalReport();
            report5.Publish(2012014007, "S12-7213", Business.Document.ReportSaveModeEnum.Normal);
             */            
        }

        private void ButtonShowGrossWorkspace_Click(object sender, RoutedEventArgs e)
        {
           // YellowstonePathology.UI.Gross.HistologyGrossPath histologyGrossPath = new Gross.HistologyGrossPath();
           // histologyGrossPath.Start();
        }        

		private void ButtonBillingTypeProcessorTests_Click(object sender, RoutedEventArgs e)
		{
			
		}		

		private void ButtonVoiceSpecimenDescription_Click(object sender, RoutedEventArgs e)
		{			
			using (System.Speech.Synthesis.SpeechSynthesizer synth = new System.Speech.Synthesis.SpeechSynthesizer())
			{
				synth.SetOutputToDefaultAudioDevice();

				System.Speech.Synthesis.PromptBuilder builder = new System.Speech.Synthesis.PromptBuilder();
				builder.AppendTextWithHint("S12", System.Speech.Synthesis.SayAs.NumberCardinal);
                builder.AppendTextWithHint("10456", System.Speech.Synthesis.SayAs.SpellOut);

				synth.Speak(builder);
			}			
		}
		
        private void ButtonDoStuff_Click(object sender, RoutedEventArgs e)
        {            
            string folderRoot = @"\\cfileserver\Documents\TechnicalOnly\2013\00001-00999\";                        
            string [] directories = System.IO.Directory.GetDirectories(folderRoot);
            foreach (string directory in directories)
            {
                string[] files = System.IO.Directory.GetFiles(directory);
                foreach (string file in files)
                {
                    if (System.IO.Path.GetExtension(file) == ".jpg")
                    {                        
                        /*
                        bool result = false;
                        using (BinaryReader br = new BinaryReader(File.Open(file, FileMode.Open)))
                        {
                            UInt16 soi = br.ReadUInt16();  // Start of Image (SOI) marker (FFD8)
                            UInt16 jfif = br.ReadUInt16(); // JFIF marker (FFE0)                            
                            result = (soi == 0xd8ff && jfif == 0xe0ff);
                        }
                        */

                        /*
                        if (file == @"\\cfileserver\Documents\TechnicalOnly\2013\00001-00999\B13-300\B13-300.jpg")
                        {
                            System.Drawing.Imaging.ImageCodecInfo myImageCodecInfo;
                            System.Drawing.Imaging.Encoder myEncoder;
                            System.Drawing.Imaging.EncoderParameter myEncoderParameter;
                            System.Drawing.Imaging.EncoderParameters myEncoderParameters;

                            myImageCodecInfo = GetEncoderInfo("image/tiff");
                            myEncoder = System.Drawing.Imaging.Encoder.Compression;
                            myEncoderParameters = new System.Drawing.Imaging.EncoderParameters(1);

                            myEncoderParameter = new System.Drawing.Imaging.EncoderParameter(myEncoder, (long)System.Drawing.Imaging.EncoderValue.CompressionCCITT4);
                            myEncoderParameters.Param[0] = myEncoderParameter;

                            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(file);
                            string newFile = System.IO.Path.ChangeExtension(file, "tif");
                            bitmap.Save(newFile, myImageCodecInfo, myEncoderParameters);                                                   
                        } 
                         */
                        
                        System.IO.File.Delete(file);
                    }
                }               
            }           
        }

        private static System.Drawing.Imaging.ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            System.Drawing.Imaging.ImageCodecInfo[] encoders;
            encoders = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

		private void ButtonFLowSpecimenOrder_Click(object sender, RoutedEventArgs e)
		{
		}

        private void ButtonAccessionMickyMouseCreate_Click(object sender, RoutedEventArgs e)
        {
            AOBuilder aoBuilder = new AOBuilder();
            Business.Test.AccessionOrder accessionOrder = aoBuilder.Build();
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(aoBuilder);
        }

        private void ButtonAccessionMickyMouseRemove_Click(object sender, RoutedEventArgs e)
        {
        }

		private void ButtonTestEGFRAccession_Click(object sender, RoutedEventArgs e)
		{
			

		}

        private void ButtonCDCPrep_Click(object sender, RoutedEventArgs e)
        {
			                                            
        }

        private void ButtonSVHTesting_Click(object sender, RoutedEventArgs e)
        {
            /*
            YellowstonePathology.Business.Slide.ModelLabelCollection slideLabelCollection = new YellowstonePathology.Business.Slide.ModelLabelCollection();
            

            YellowstonePathology.UI.Login.CytologySlideLabelDocument labels = new Login.CytologySlideLabelDocument(slideLabelCollection, false);

            XpsDocumentViewer viewer = new XpsDocumentViewer();
            viewer.LoadDocument(labels);
            viewer.ShowDialog();
            */            
            

            /*
            string [] files = System.IO.Directory.GetFiles(@"C:\Testing");
            foreach (string file in files)
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    String line;                    
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }
            */            
        }

        private void ButtonInsertTesting_Click(object sender, RoutedEventArgs e)
        {
            
        }                		

        private void ButtonStartMessageHost_Click(object sender, RoutedEventArgs e)
        {
            //YellowstonePathology.MessageNotification.NotificationServiceHostInstance.Start();
            //YellowstonePathology.MessageNotification.NotificationServiceHostInstance.Instance.Service.MessageNotificationReceived += new YellowstonePathology.MessageNotification.NotificationService.MessageNotificationReceivedEventHandler(Service_MessageReceived);
        }

        private void ButtonSendMessageToEric_Click(object sender, RoutedEventArgs e)
        {
            
		}

        private void CreateIHCTestSelectStatement()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select pso.ReportNo, t.TestName " +
                "from tblAccessionOrder ao " +
                "join tblPanelSetOrder pso on ao.MasterAccessionno = pso.masterAccessionno " +
                "join tblPanelOrder po on pso.ReportNo = po.ReportNo " +
                "join tblTestOrder t on po.panelOrderId = t.panelOrderId " +
                "where ao.AccessionDate between '1/1/2014' and '12/31/2014' and t.TestId in (");

            YellowstonePathology.Business.Test.Model.TestCollection testCollection = YellowstonePathology.Business.Test.Model.TestCollection.GetIHCTests();
            foreach (YellowstonePathology.Business.Test.Model.Test test in testCollection)
            {
                sql.Append(test.TestId.ToString() + ", ");
            }

            sql.Remove(sql.Length - 2, 2);
            sql.Append(")");
            Console.WriteLine(sql.ToString());
        }

        private void CreateCPTCodeTypeListForSQL()
        {
            YellowstonePathology.Business.Billing.Model.CptCodeCollection cptCodeCollection = YellowstonePathology.Business.Billing.Model.CptCodeCollection.GetAll();
            StringBuilder technicalOnly = new StringBuilder();
            StringBuilder professionalOnly = new StringBuilder();
            StringBuilder global = new StringBuilder();
            StringBuilder pqrs = new StringBuilder();

            foreach (YellowstonePathology.Business.Billing.Model.CptCode cptCode in cptCodeCollection)
            {                
                switch (cptCode.CodeType)
                {
                    case Business.Billing.Model.CPTCodeTypeEnum.TechnicalOnly:
                        technicalOnly.Append("'" + cptCode.Code + "', ");
                        break;
                    case Business.Billing.Model.CPTCodeTypeEnum.ProfessionalOnly:
                        professionalOnly.Append("'" + cptCode.Code + "', ");
                        break;
                    case Business.Billing.Model.CPTCodeTypeEnum.Global:
                        global.Append("'" + cptCode.Code + "', ");
                        break;
                    case Business.Billing.Model.CPTCodeTypeEnum.PQRS:
                        pqrs.Append("'" + cptCode.Code + "', ");
                        break;
                }
            }

            technicalOnly = technicalOnly.Remove(technicalOnly.Length - 2, 2);
            professionalOnly = professionalOnly.Remove(professionalOnly.Length - 2, 2);
            global = global.Remove(global.Length - 2, 2);
            pqrs = pqrs.Remove(pqrs.Length - 2, 2);

            Console.WriteLine("Update tblPanelSetOrderCPTCodeBill Set CodeType = 'TechnicalOnly' where Cptcode in (" + technicalOnly.ToString() + ") and CodeType <> 'TechnicalOnly'");
            Console.WriteLine("Update tblPanelSetOrderCPTCodeBill Set CodeType = 'ProfessionalOnly' where Cptcode in (" + professionalOnly.ToString() + ") and CodeType <> 'ProfessionalOnly'");
            Console.WriteLine("Update tblPanelSetOrderCPTCodeBill Set CodeType = 'Global' where Cptcode in (" + global.ToString() + ") and CodeType <> 'Global'");
            Console.WriteLine("Update tblPanelSetOrderCPTCodeBill Set CodeType = 'PQRS' where Cptcode in (" + pqrs.ToString() + ") and CodeType <> 'PQRS'");
        }

        private void CreateCaseTypeListForSQL()
        {
            YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();            
            foreach (YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet in panelSetCollection)
            {
                string updatePanelSetOrder = "Update tblPanelSetOrder set CaseType = '" + panelSet.CaseType + "' Where panelSetId = " + panelSet.PanelSetId;
                Console.WriteLine(updatePanelSetOrder);            
            }            
        }

        private void ButtonSendMessageToSid_Click(object sender, RoutedEventArgs e)
        {
            this.SendTestFax();

            //this.DoMongoMove();

            this.CreateCPTCodeTypeListForSQL();
            //this.CRC();
            //this.WriteAssemblyQualifiedTypeSQL();
            //this.BuildObjectsTesting();

            //this.BuildObjectsTesting();
            //double xx = (DateTime.Now - DateTime.Parse("10/10/2014 9:40")).TotalHours;
            //MessageBox.Show(xx.ToString());

            //YellowstonePathology.Business.Test.AccessionOrder ao = YellowstonePathology.Business.Gateway.AccessionOrderGatewayV2.GetAccessionOrderByMasterAccessionNo("14-19341");

            //Type collectionType = Type.GetType("YellowstonePathology.Business.Test.HPV.PanelSetOrderHPV, BusinessObjects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            //System.Collections.IList childObjectCollection = (System.Collections.IList)Activator.CreateInstance(collectionType);

            //YellowstonePathology.Business.Persistence.SqlCommandBuilder sqlCommandBuilder = new Persistence.SqlCommandBuilder(typeof(YellowstonePathology.Business.Test.AccessionOrder), "14-19341");
            //System.Data.SqlClient.SqlCommand cmd = sqlCommandBuilder.Build();

            /*
            YellowstonePathology.Business.PanelSet.Model.PanelSetCollection psc = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
            foreach (YellowstonePathology.Business.PanelSet.Model.PanelSet ps in psc)
            {
                YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine facility = new Domain.Facility.Model.NeogenomicsIrvine();
                if (ps.TechnicalComponentFacility != null)
                {
                    if (ps.TechnicalComponentFacility.FacilityId == facility.FacilityId)
                    {
                        //Console.WriteLine("Update tblPanelSetOrder set UniversalServiceId = '" + ps.UniversalServiceIdCollection[0].UniversalServiceId + "' where PanelSetId = " + ps.PanelSetId + " and UniversalServiceId is null ");
                        Console.WriteLine(ps.PanelSetName);
                    }
                }
            }
            */
        }           

        private void FindMissingReportNumbers()
        {
            
        }        

        private void WriteAssemblyQualifiedTypeSQL()
        {

			YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder psos = new YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder();
            Console.WriteLine(psos.GetType().AssemblyQualifiedName);
            
			YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection psocptc = new Business.Test.PanelSetOrderCPTCodeCollection();
			Console.WriteLine(psocptc.GetType().AssemblyQualifiedName);

			YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill psocptb = new Business.Test.PanelSetOrderCPTCodeBill();
			Console.WriteLine(psocptb.GetType().AssemblyQualifiedName);

			YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBillCollection psocptbc = new Business.Test.PanelSetOrderCPTCodeBillCollection();
			Console.WriteLine(psocptbc.GetType().AssemblyQualifiedName);


            YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen ssr = new YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen();
			Console.WriteLine(ssr.GetType().AssemblyQualifiedName);

            YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenCollection ssrc = new YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenCollection();
			Console.WriteLine(ssrc.GetType().AssemblyQualifiedName);

			YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult ic = new YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult();
			Console.WriteLine(ic.GetType().AssemblyQualifiedName);

			YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResultCollection icc = new YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResultCollection();
			Console.WriteLine(icc.GetType().AssemblyQualifiedName);

            YellowstonePathology.Business.Test.Surgical.SurgicalAudit sra = new YellowstonePathology.Business.Test.Surgical.SurgicalAudit();
			Console.WriteLine(sra.GetType().AssemblyQualifiedName);

            YellowstonePathology.Business.Test.Surgical.SurgicalAuditCollection srac = new YellowstonePathology.Business.Test.Surgical.SurgicalAuditCollection();
			Console.WriteLine(srac.GetType().AssemblyQualifiedName);

			YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenAudit ssra = new YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenAudit();
			Console.WriteLine(ssra.GetType().AssemblyQualifiedName);

			YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenAuditCollection ssrac = new YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenAuditCollection();
			Console.WriteLine(ssrac.GetType().AssemblyQualifiedName);

			YellowstonePathology.Business.SpecialStain.StainResultItem sri = new Business.SpecialStain.StainResultItem();
			Console.WriteLine(sri.GetType().AssemblyQualifiedName);

			YellowstonePathology.Business.SpecialStain.StainResultItemCollection sric = new Business.SpecialStain.StainResultItemCollection();
			Console.WriteLine(sric.GetType().AssemblyQualifiedName);

			YellowstonePathology.Business.Billing.ICD9BillingCode icd = new Business.Billing.ICD9BillingCode();
			Console.WriteLine(icd.GetType().AssemblyQualifiedName);

			YellowstonePathology.Business.Billing.ICD9BillingCodeCollection icdc = new Business.Billing.ICD9BillingCodeCollection();
			Console.WriteLine(icdc.GetType().AssemblyQualifiedName);

			YellowstonePathology.Business.Billing.CptBillingCodeItem cpt = new Business.Billing.CptBillingCodeItem();
			Console.WriteLine(cpt.GetType().AssemblyQualifiedName);

			YellowstonePathology.Business.Billing.CptBillingCodeItemCollection cptc = new Business.Billing.CptBillingCodeItemCollection();
			Console.WriteLine(cptc.GetType().AssemblyQualifiedName);

            //foreach (YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet in panelSetCollection)
            //{
            //    Console.WriteLine("Update tblPanelSet set AssemblyQualifiedTypeName = '" + panelSet.GetType().AssemblyQualifiedName + "' where panelsetId = " + panelSet.PanelSetId);
            //}
        }

        private void BuildObjectsTesting()
        {
            XElement xElement = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderXMLDocument("14-27868");
			YellowstonePathology.Business.Persistence.ObjectXMLBuilder objectXMLBuilder = new YellowstonePathology.Business.Persistence.ObjectXMLBuilder();
            object result = objectXMLBuilder.Build(xElement);
        }

        private void WriteStVincentAllInSql()
        {
            
        }

        private void FindTextInFiles()
        {
            // Read the file and display it line by line.
            string [] files = System.IO.Directory.GetFiles(@"\\dc1\FTPData\SVBBilling\Processed\");

            foreach (string filePath in files)
            {
                Console.WriteLine("Search file: " + filePath);

                int counter = 0;
                string line;

                System.IO.StreamReader file = new System.IO.StreamReader(filePath);
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains("149450262"))
                    {                        
                        System.Windows.MessageBox.Show("Found on line: " + counter.ToString());
                    }
                    counter++;
                }
                file.Close();
            }		

        }

        private void WriteHPVStandingOrderRules()
        {
            YellowstonePathology.Business.Client.Model.StandingOrderCollection standingOrderCollection = YellowstonePathology.Business.Client.Model.StandingOrderCollection.GetHPVStandingOrders();
            foreach (YellowstonePathology.Business.Client.Model.StandingOrder standingOrder in standingOrderCollection)
            {
                Console.WriteLine(standingOrder.ToString());
            }
        }

        private void CRC()
        {
            //string crc = YellowstonePathology.Business.BarcodeScanning.CRC32V.CRC32("15-1234.1.1");
            //Console.WriteLine("CRC: " + crc);            
        }

        Business.Test.AccessionOrder m_AccessionOrder = null;
        string masterAccessionNo = "16-9590";
        string actNo = "1";
        string writer = "W1";

        private void ButtonRunMethod_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.UI.MakeDictionary make = new MakeDictionary();
            make.DoIt();
        }        

        private string CallBackOne(string x)
        {
            return "Purple";
        }

        private void FindY()
        {
            YellowstonePathology.Business.ReportNoCollection reportNoCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetReportNumbers();
            YellowstonePathology.Business.ReportNoCollection fix = new Business.ReportNoCollection();

            foreach (YellowstonePathology.Business.ReportNo reportNo in reportNoCollection)
            {
				YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(reportNo.Value);
				string path = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser);
                string[] files = System.IO.Directory.GetFiles(path);
                foreach (string file in files)
                {
                    if(file.Contains(".Y.") == true)
                    {
                        //System.IO.File.Delete(file);

                        if (fix.Exists(reportNo.Value) == false)
                        {
                            fix.Add(reportNo);
                        }                    
                    }
                }
            }

            /*
            foreach (YellowstonePathology.Business.ReportNo reportNo in fix)
            {
                YellowstonePathology.Business.Interface.ICaseDocument caseDocument = YellowstonePathology.Business.Document.DocumentFactory.GetDocument(116);
                YellowstonePathology.Domain.OrderIdParser orderIdParser = new Domain.OrderIdParser(reportNo.Value);
                YellowstonePathology.Business.Rules.MethodResult methodResult = caseDocument.DeleteCaseFiles(orderIdParser);

                if (methodResult.Success == true)
                {
                    caseDocument.Render(orderIdParser.MasterAccessionNo, reportNo.Value, YellowstonePathology.Business.Document.ReportSaveModeEnum.Normal);
                    caseDocument.Publish();                    
                }
                else
                {
                    Console.WriteLine(methodResult.Message);
                }
            }
            */
        }

        private void FixHPV()
        {
            
        }

        private void FindNonASCICharacters()
        {
			List<YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder> list = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSurgicalTestOrder();
            foreach (YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder sto in list)
            {
                for (int i = 0; i < sto.CancerSummary.Length; ++i)
                {
                    char c = sto.CancerSummary[i];
                    if (((int)c) > 127)
                    {
                        Console.WriteLine(sto.ReportNo + "'" + c + "' at index " + i + " is not ASCII");
                    }
                }  
            }
        }

        private void TestBsonDateTime()
        {
            DateTime xx = DateTime.Now;
            BsonDateTime dd = BsonDateTime.Create(xx.ToUniversalTime());
        }        

        private void SendTestFax()
        {
            YellowstonePathology.Business.ReportDistribution.Model.FaxSubmission.Submit("99999", false, "Hello World", @"c:\Testing\Test.tif");            
        }

        private void TestReflectionDelagate()
        {
            YellowstonePathology.Business.Mongo.LocalServer localServer = new Business.Mongo.LocalServer("LocalLIS");
            YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionCollection tt = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionCollection();            
            YellowstonePathology.Business.Mongo.DocumentCollectionTracker dct = new Business.Mongo.DocumentCollectionTracker(tt, localServer);

            YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution t1 = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution();
            tt.Add(t1);
        }

        private void DoMongoMove()
        {            
            //YellowstonePathology.UI.Mongo.ProcessRunner runner = new Mongo.ProcessRunner();
            //runner.Run();            

            //MessageBox.Show("Done.");
        }

        private void MongoPersistenceTest()
        {
            YellowstonePathology.Business.ReportDistribution.Model.ReportDistributionLogEntryCollection col = YellowstonePathology.Business.Mongo.Gateway.GetReportDistributionLogEntryCollectionGTETime(DateTime.Now);
            YellowstonePathology.Business.Mongo.LocalServer localServer = new Business.Mongo.LocalServer(YellowstonePathology.Business.Mongo.LocalServer.LocalLISDatabaseName);

            foreach (YellowstonePathology.Business.ReportDistribution.Model.ReportDistributionLogEntry item in col)
            {
                YellowstonePathology.Business.Mongo.DocumentTracker documentTracker = new Business.Mongo.DocumentTracker(localServer);
                documentTracker.Register(item);
                item.Message = "What's up";
                documentTracker.SubmitChanges();
            }
        }

        private void WriteTestOrderReportDistributionIds()
        {
            System.ComponentModel.BackgroundWorker backgroundWorker = new System.ComponentModel.BackgroundWorker();
            backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(BackgroundWorker_DoWork);
            backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
            backgroundWorker.RunWorkerAsync();                      
            
        }        

        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("All Done");
        }

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
			
        }

        private void WriteNonDatabaseTests()
        {
            StringBuilder result = new StringBuilder();
            YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
            foreach (YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet in panelSetCollection)
            {
                if (panelSet.ResultDocumentSource != Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase)
                {
                    result.Append(panelSet.PanelSetId + ", ");
                }
            }
            Console.WriteLine(result.ToString());
        }

        private void WriteFacilitySql()
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection fc = YellowstonePathology.Business.Facility.Model.FacilityCollection.GetAllFacilities();
            StringBuilder sql = new StringBuilder();
            foreach (YellowstonePathology.Business.Facility.Model.Facility f in fc)
            {
                //sql.Append(f.
            }
        }

        private void BuildTest()
        {
            XElement xElement = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderXMLDocument("14-123");
			YellowstonePathology.Business.Persistence.ObjectXMLBuilder b = new YellowstonePathology.Business.Persistence.ObjectXMLBuilder();
            b.Build(xElement);
        }

        private void ButtonRunShowTemplatePage_Click(object sender, RoutedEventArgs e)
        {
            //YellowstonePathology.UI.Gross.SecondaryWindow window = new Gross.SecondaryWindow();
            //YellowstonePathology.UI.Gross.DictationTemplatePage page = new Gross.DictationTemplatePage("SPCMNPRSTTRDCLRSCTN");
            //window.Show();
            //window.PageNavigator.Navigate(page);
        }

        private void GetTableNames()
        {
            
        }
    }
}
