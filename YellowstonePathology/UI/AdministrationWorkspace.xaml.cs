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
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using LibGit2Sharp;

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
            string resultString = Business.Specimen.Model.SpecimenCollection.GetAll().ToJSON();
            using (StreamWriter sw = new StreamWriter(@"C:\ProgramData\ypi\lisdata\YellowstonePathology.Business.Specimen.Model.SpecimenCollection.json", false))
            {
                sw.Write(resultString);
            }

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

			YellowstonePathology.Business.Billing.Model.ICD9BillingCode icd = new Business.Billing.Model.ICD9BillingCode();
			Console.WriteLine(icd.GetType().AssemblyQualifiedName);

			YellowstonePathology.Business.Billing.Model.ICD9BillingCodeCollection icdc = new Business.Billing.Model.ICD9BillingCodeCollection();
			Console.WriteLine(icdc.GetType().AssemblyQualifiedName);

			YellowstonePathology.Business.Billing.Model.CptBillingCodeItem cpt = new Business.Billing.Model.CptBillingCodeItem();
			Console.WriteLine(cpt.GetType().AssemblyQualifiedName);

			YellowstonePathology.Business.Billing.Model.CptBillingCodeItemCollection cptc = new Business.Billing.Model.CptBillingCodeItemCollection();
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

        

        private void ButtonRunMethod_Click(object sender, RoutedEventArgs e)
        {
            //if (accessionOrder.AccessionLock.IsLockAquiredByMe == false ||
            //    (this.m_TestOrder.Final == true &&
            //    (this.m_TestOrder.Distribute == false ||
            //    this.m_TestOrder.TestOrderReportDistributionCollection.HasDistributedItems())))

            bool haveLock = false;
            bool isFinal = false;
            bool willDistribute = false;
            bool hasDistributed = false;

            bool result = haveLock || (isFinal && (willDistribute || hasDistributed));
            Console.WriteLine("Result: " + result);
        }        

        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //byte[] bytes = Convert.FromBase64String("JVBERi0xLjQKMSAwIG9iago8PAovVHlwZSAvQ2F0YWxvZwovUGFnZXMgMyAwIFIKPj4KZW5kb2JqCjIgMCBvYmoKPDwKL1R5cGUgL091dGxpbmVzCi9Db3VudCAwCj4+CmVuZG9iagozIDAgb2JqCjw8Ci9UeXBlIC9QYWdlcwovQ291bnQgMQovS2lkcyBbMTggMCBSXQo+PgplbmRvYmoKNCAwIG9iagpbL1BERiAvVGV4dCAvSW1hZ2VCIC9JbWFnZUMgL0ltYWdlSV0KZW5kb2JqCjUgMCBvYmoKPDwKL1R5cGUgL0ZvbnQKL1N1YnR5cGUgL1R5cGUxCi9CYXNlRm9udCAvSGVsdmV0aWNhCi9FbmNvZGluZyAvTWFjUm9tYW5FbmNvZGluZwo+PgplbmRvYmoKNiAwIG9iago8PAovVHlwZSAvRm9udAovU3VidHlwZSAvVHlwZTEKL0Jhc2VGb250IC9IZWx2ZXRpY2EtQm9sZAovRW5jb2RpbmcgL01hY1JvbWFuRW5jb2RpbmcKPj4KZW5kb2JqCjcgMCBvYmoKPDwKL1R5cGUgL0ZvbnQKL1N1YnR5cGUgL1R5cGUxCi9CYXNlRm9udCAvSGVsdmV0aWNhLU9ibGlxdWUKL0VuY29kaW5nIC9NYWNSb21hbkVuY29kaW5nCj4+CmVuZG9iago4IDAgb2JqCjw8Ci9UeXBlIC9Gb250Ci9TdWJ0eXBlIC9UeXBlMQovQmFzZUZvbnQgL0hlbHZldGljYS1Cb2xkT2JsaXF1ZQovRW5jb2RpbmcgL01hY1JvbWFuRW5jb2RpbmcKPj4KZW5kb2JqCjkgMCBvYmoKPDwKL1R5cGUgL0ZvbnQKL1N1YnR5cGUgL1R5cGUxCi9CYXNlRm9udCAvQ291cmllcgovRW5jb2RpbmcgL01hY1JvbWFuRW5jb2RpbmcKPj4KZW5kb2JqCjEwIDAgb2JqCjw8Ci9UeXBlIC9Gb250Ci9TdWJ0eXBlIC9UeXBlMQovQmFzZUZvbnQgL0NvdXJpZXItQm9sZAovRW5jb2RpbmcgL01hY1JvbWFuRW5jb2RpbmcKPj4KZW5kb2JqCjExIDAgb2JqCjw8Ci9UeXBlIC9Gb250Ci9TdWJ0eXBlIC9UeXBlMQovQmFzZUZvbnQgL0NvdXJpZXItT2JsaXF1ZQovRW5jb2RpbmcgL01hY1JvbWFuRW5jb2RpbmcKPj4KZW5kb2JqCjEyIDAgb2JqCjw8Ci9UeXBlIC9Gb250Ci9TdWJ0eXBlIC9UeXBlMQovQmFzZUZvbnQgL0NvdXJpZXItQm9sZE9ibGlxdWUKL0VuY29kaW5nIC9NYWNSb21hbkVuY29kaW5nCj4+CmVuZG9iagoxMyAwIG9iago8PAovVHlwZSAvRm9udAovU3VidHlwZSAvVHlwZTEKL0Jhc2VGb250IC9UaW1lcy1Sb21hbgovRW5jb2RpbmcgL01hY1JvbWFuRW5jb2RpbmcKPj4KZW5kb2JqCjE0IDAgb2JqCjw8Ci9UeXBlIC9Gb250Ci9TdWJ0eXBlIC9UeXBlMQovQmFzZUZvbnQgL1RpbWVzLUJvbGQKL0VuY29kaW5nIC9NYWNSb21hbkVuY29kaW5nCj4+CmVuZG9iagoxNSAwIG9iago8PAovVHlwZSAvRm9udAovU3VidHlwZSAvVHlwZTEKL0Jhc2VGb250IC9UaW1lcy1JdGFsaWMKL0VuY29kaW5nIC9NYWNSb21hbkVuY29kaW5nCj4+CmVuZG9iagoxNiAwIG9iago8PAovVHlwZSAvRm9udAovU3VidHlwZSAvVHlwZTEKL0Jhc2VGb250IC9UaW1lcy1Cb2xkSXRhbGljCi9FbmNvZGluZyAvTWFjUm9tYW5FbmNvZGluZwo+PgplbmRvYmoKMTcgMCBvYmogCjw8Ci9DcmVhdGlvbkRhdGUgKEQ6MjAwMykKL1Byb2R1Y2VyIChGZWRFeCBTZXJ2aWNlcykKL1RpdGxlIChGZWRFeCBTaGlwcGluZyBMYWJlbCkNL0NyZWF0b3IgKEZlZEV4IEN1c3RvbWVyIEF1dG9tYXRpb24pDS9BdXRob3IgKENMUyBWZXJzaW9uIDUxMjAxMzUpCj4+CmVuZG9iagoxOCAwIG9iago8PAovVHlwZSAvUGFnZQ0vUGFyZW50IDMgMCBSCi9SZXNvdXJjZXMgPDwgL1Byb2NTZXQgNCAwIFIgCiAvRm9udCA8PCAvRjEgNSAwIFIgCi9GMiA2IDAgUiAKL0YzIDcgMCBSIAovRjQgOCAwIFIgCi9GNSA5IDAgUiAKL0Y2IDEwIDAgUiAKL0Y3IDExIDAgUiAKL0Y4IDEyIDAgUiAKL0Y5IDEzIDAgUiAKL0YxMCAxNCAwIFIgCi9GMTEgMTUgMCBSIAovRjEyIDE2IDAgUiAKID4+Ci9YT2JqZWN0IDw8IC9GZWRFeEV4cHJlc3MgMjAgMCBSCi9FeHByZXNzRSAyMSAwIFIKL2JhcmNvZGUwIDIyIDAgUgo+Pgo+PgovTWVkaWFCb3ggWzAgMCA2MTIgNzkyXQovVHJpbUJveFswIDAgNjEyIDc5Ml0KL0NvbnRlbnRzIDE5IDAgUgovUm90YXRlIDA+PgplbmRvYmoKMTkgMCBvYmoKPDwgL0xlbmd0aCAzNzA0Ci9GaWx0ZXIgWy9BU0NJSTg1RGVjb2RlIC9GbGF0ZURlY29kZV0gCj4+CnN0cmVhbQpHYXQ9L2hlc0xEJlVscWVycltoZDMoQ1VEbjJMO2hbUCQiPWIrNVM7ZmgoLmgySzlkcSMmK2FYJUpuXU4sSXB1TGI4amYuI0slMkpeN0M8QApTL0tWNlBdX1U5KUJYaE4hbmpCRCVZSWUvZFwlNnBsUE0nc0VfPkFrcVhyPiJvdTsxRCJKWWBmKls4PEBJKFlLIl9MbjEnYSREZiInUzs8Lgo+MyFEX245JztsMF1RSyNTIVcjbzhvIjk8Jz41PWFbbyZyT19aLFcjP1s4MycxIlslO29FSHQvNUIjJlhqM0BSRElvLSQjPTBNQU0+UDdbYQpwV1tVUlMyO0VRS0pGXDZpW2RKXVBrT1g4ZSlCPz1iMVE9ZjlfdXJsPFguYWJTW2QvSjY9IkdIL3A5MGQkKnNBJWQuYjA8bGYzY0xHV2I/QgpdQnNpY11LVFlOWVAqYiNcMCNZMGgwSUZdWFxJQl5SLjNkY2hzVWJsKHNcOldfUTxJdUliWkI5ckouXV5kQiM3J29RbCJzNFQvLSo0Ij9qbwpTKz9kLlJiZik+WjN0ZG5cKVpuUERHT1VUQj0rVz1UZ1ErPzNMQlpdSCRRYTBHUWouI2Q3KzhXYjdDTj5wKGw7OUBaO1NELDFrOVRhc19FJwphWy51Skwra11MPSlXOzxfc1JeUl4iTWVtMVpgbjEtaCRrUGtXPmcxbUhgVDlIRClzT2w+UmBubCJeN2RxPT9Dciw5Jz1ucEdfUjhuQjNhcApQdGRxRD1QaTxXbCI4QSwoVkknWTpJO15YMlciQXIldVZVcVZvYG5hIiRZXUJSImVqZlU/b1A5NHQlNEs/W29YZ0dXWWRlbSZEXHJAXXUjOQpnWTVALms9TjxeLGhrOTxMV146aFRYQGhQWUt1anEzPVtcOGhrJmM5azRlXV9NPChxV1w8XSdaQyclXF4jLTErMS5ib2dXYzBoJyZgRTxlVAo/UF9iaSZGLyVaaWQ+by0/bTFxKkM0Q0wuOHRAPj48cSJsNy4zXjFOYVtCWnU6Uk44XVxDZUo2UVU0YCRmYVBsIzU+NTI7Q2ZYKGdHOz9QUQoxZUIpRVVPSzsqXzhJJkhubXV1IVRyRyZTOUZfcFheY3UoQ3FqMlx0UUhoVWZIdTotQylEXVRKS2pCPU5fRUwmNWNSPF0wOyJgQ29UUTVpWApLbVh1XGEidCVvVCFRUkpHSildT05vMDleNDxbQC0oN0hZNGZvMCgvIkY4cHErLm9CKzxjcENoJF9NUShGKTRGT04ocF9IR0AhTnU/S29zUgpHSVQ6QHFKKzQ4bj4wbWM6Km1kZF5zdSUuSC1CRjphRV5naWtJY1QvO1ZsaCFLTk43XiUjKkF0MDltMzBtLGlpZ2NGcCdAQT49VEZEUldeTwphS0ghWDNQVVJaUjNiPHFmdG50SEs+ZCwuMTFWQz5TdGpAOFslKkVdSV5TXUVAUjpLbD5YakdKY3JAXEEmRFdoKDBPRl5qJEZELTVKY0dMWwpGOEJXPTk7ajo9NW5DNGFMP1hcUFkrRD1qIU8+W0phcms0bUY5LU11WEFqWUEoOWBIKlFMX3NLRlo+XCNoMF5CVFB1KzcwY2NJPU0obnFLZgoxZ1tPVlhHUzREK0JLayxSWDFLZyNrTTprUlZZTUlRZUdpPFc1QXA5UVk4cDRVY0RrPDlmPERXTy06MzFwKSJzRit1c091M2djLVkmISZpOApqbHBfTlUyNFRUNmcxO0YyXFsuJCVPUyJZO2lxPCtVXyJkNkVCVD0uPiVIX3NBIUoxZWUyOTVeTjs0ZEZqJlVbc2heN2BbZG8rYTdsW0BjPwpEP1ZINDVjIiFaYjdLVi1BQCg8YVE4bWhbJkpnWm4hK1NqcztWPDomUiFQRlRJIlxLJSZvPTpBbDMqLF1NJV5RYVFCciYsUnBaW0E4aixwYQo0Q2FsUFJMalQ1QV04MklIRCRXNCczdW4wRj9BdSVlcDlKVUMtSEQ8V2sxK0FHXSwkTy46TkgvVkZsSlopYV4zSmBbb1JPLVQuTmtrLlQ+LApGOipTM2xARUNvMm41UzQiOWBgbEVSaHI2OmVbLi9DL2wybThmaFZJVnMmXDAuXltpLVo3ZloiamUzM2RYN25sWTtrUShsRmcvOCZiJ1sxXQo1VWRGX2BUazk3UWE0YjxmJSMsPzFvJGdKWD1paWo2QmotXkhDUyhiMW80YWZkSEE9RUUlW0hVIiVvKmNqXlImbEZoc2ZPTVQhKVAlaHFycgplZ25BPFltQCwxOU06bXRTa3EsbCciSTolNzpaK1tsQElCTzZIcldVIkhPXi4nWi1ySHBIWV9SO1FCWz8uVVpJTWI3WkM3O09WRlU6NT9rLAoqPldSOmwwWCUyV21KbVNYPzlUYDFNYVQ0LVI/UkBjJCtUYy1yNSFIUzl0SmdWUiJIbElAOSswZkUtKnQuJktVKC9aLyZEX1hiSnUvVEZaNAoxQDdtXDpzO0xEY19bQk9UVTxrXG1nVSU+R2dBZnJPLTozRUJhNVdkZio2TWJGYnEuZlZLMFNsTD45KCxvZnQ2M0QjUEFNK3A+ZiRqbihNXQpPZWwoZiNQO0YvYjVwb0E0XSJmZT4xNDliSm5La1lTPkdNJEBdSjVdMl0/VTJOM0o2ISQ7YUBWQ3RtSHElX20+UVxTPzlrLiZLVygzTF0pXAotOVpEZkkwRCRxbyRmY103VDFlcmJkcioyJ049aDldK0lzT2M8Lk5uPlFJSHI6Y1spb0dmMzUmTTY0ZERTPSVBVSVuPkBxbjMiIU9kMCU/ZQpFSG9WUT9dUGo5QGpbWVxLaDxGOCZAJjk4TFFjSitaRVhsXjc3KEdHMS8jIW0kIztfJzosN1xvViVla1hARS90JjMyb0JoW1smU2FWWUlCOApnWW5yTFBdRWdXMUsmYGhlXThAVEVEazhaQiRCQF5bUDtONj1KPzlRMi1vcUNKMjxyVHFUT3QvTyg0JTM9XE1YbzxROlhoViJOY2M6bk5OXgouNnM9NFspXTRPb2cxc2twQF9DSElWJjNHaVZBTCFBazcsJGVbcT1MblJiIjdZRSYncTJpJUBuZWJDRFhhWWQtK2pHQG5vYmpTP2EoRD1UUgoiU2MlOzxDRlQ5QztYUU03b19iLzIyM2ZVVzMob1ZtWUYlW0AxYipaNWs+O1pIRiYlaXBrZkM9YClTZzs7Q1kzNz4pSVpxN3MkZS4rLm9CNApwVSc1dDFuVDhIR0dQZSFHcm9BbnFVXDMlcks2cScnW1JtVyldJXUsNlB0Mz0zTlVtUEQuUVsyciFgOXA1T08hWHJWayxNNVExO0ZdKGZrLgprOUErRzhRUGUkOjpEKW08UEBxIV1MZ1dzbklsNlJRRSUjWGJcaV9WMW1ZXU1pMDExajpwLTI9Mzs9dVIoKEkjYEJdK1pDWEUxc3Q8dShTQApeXSspSTAwYXA2OiQ/K0FeT0RRTExOW0UtKmk0PlopITY1QHBtK09kNmA2ZnBWTFNWWVo1XjQzYjBqTGZWS1AuaTM1cnUoZzBGPjQmVEtMMQpkLTVRSlBxX1Q2NHRnPXI5KGNbOmlrKlc0amRMcWlPUytIPT1lXUZQTmkibFFQWWFSSi8xZGhRNGNILj06TW8zYjpBckJxJl86WVcoZTpubgowZG0yQXBWPj9fM2dgU1lQXyQ1TzY+TVlRJHE3ZDhrPzVXWGUzViRdI1JFVHBEQVdvSkA3IytSUFhoSEQnTTNAJipER1UwSV8ncl0mZEdudAowSS4kZmFCaENWbVNRU15WPENgMk5iTGtDWnEiSmpcOVI3SDE3JDlMUFVQNys/Jl1cZSVPUlsuT0lMbDMmW1VRM2lGazk+OD0/RFwwMVtMaQpPQDE6Jlo1dD82aihSM0MnNEgjUydLZW43Xy41SF5oNzwmOjlhOz9iJHMhKV5hRWEmSUxXUGNQZHIkU1E8L3RyTVVKKTEzZF5cUFE+MU5tPAphb21Icks/NjR1OiJSWj4tV0hadTFNT0NUTTVEVy9QJ2p0R2BybypJUE0kQWg3KlNXTDpkZXM0Nzg0O1ZmZHNjIkpDblZkWkszV0xTWWglXQpDa2Y8J0Y+UltnPmctSSJvSXVdWDpdZSZmYGc8Zk9EamUuPV8yNllCQXBMSyduOHBYaDZJVXJMMTlXRGVBN2pNIl8kTGpGRE1tO1cxSFpbUQpXIjtcWlA8by1mNmQhVXNdbjJJREZoJ0Qub2RvdTY1JWgjNj5mIk41PSJdS15AcVI5Tk82NlJYIlhpQlFYbyxLJ01wU0Y4UD0mRGoxKi81ZwpJOVRucS5XWCh0JDtTXCZnMl8rOy9kSzM0YjVydTMhWWE8dERnYTYuPFwyRSwtP051WW9qREdaSmpbOThCVTE/P2BOM2MuRE5qZUBlb1NCcApgc0psJjJTOEQrZzA/Nl8jMFpZSi5sYTZZQ21yWW5bN2FWI2ZcI0cuMTxWXTZcbFhbTjgxOWI2JDBsTHNLcSohR21ZbydUYlFeaygnMkZbWgpSUEFHUC9cQjZXTEZZP2A3XUdLWGs3IylQTl87R3M6LFlPaWxINzdWayRZYVxBdUw9Z2VhXyojS0Y3dWlcXj9wLjRcZVUrPy1AQCZXQEpwMgpTYTE4Xl0xcG10biw9I1RAOUs3VmIrTjxgcV4ncEEnM0hAPz9udGA2Q1FILj1qZyYxTW5scS5nIyNDVjQxTHMiT2lTW2g0JU9cViY+MiRQRApqJicmWGBrJ0otb2YrQnRHSDZFQGw+WCRYTVM7TT5VPGdcKUQjIVdeaT1zNmpoJDxOKFJbZ3FlX0EvUzdVIWMzWHFeZ2hFP2IoTCRuRlNdNgo2Pj0kTW0kJCRVUVRvaytMY1YnUSdpSl89JVlLRkIkbF9WdToyJTMicj5eUSgkMCwuZSVwaks4XCx+PgplbmRzdHJlYW0KZW5kb2JqCjIwIDAgb2JqCjw8IC9UeXBlIC9YT2JqZWN0Ci9TdWJ0eXBlIC9JbWFnZQovV2lkdGggMTE4Ci9IZWlnaHQgNDkKL0NvbG9yU3BhY2UgL0RldmljZVJHQgovQml0c1BlckNvbXBvbmVudCA4Ci9MZW5ndGggNzE1Ci9GaWx0ZXIgWy9BU0NJSTg1RGVjb2RlIC9GbGF0ZURlY29kZV0KPj5zdHJlYW0KR2IiLyZiRHNMYCUtLkE7XHBJT25gPCViaCo4NDc7NHUibU4rJmdScVswV3FJJjVyODhDdSlzYGJCbUwmRFcsU1xZck8wR1AxNWNkM0Uza24KOyRpbDdSaEddP2ZvMnEkYDY+MDU6RXR0amJtTHFZS01OZz1AO3VRZEh1WyInaFkmV2IuKjF0MmEqUigjJzM8JjBKXSwzK24zM0h0QEhDMXEKOSNCT1Y0bDU8YyQ+L0JCJER1JD9tPCdlciZvMWo3RW5UNkZadDBRSmxYaF8zVEktNGZbKWxoXi10O1BTZnJeaj5OMUFoJkByKm9pXlxkcD8KUCQzVylnc21JQFpnLkBaUXFDOEFbcERJW11FJCxVNVtUN107Q2pMU0BSdVc6N3B1YiRKPTNURig2S2RPTzRvPk00Ki1kXj44RT5GJiErUk4KaTJhLGZJQ2oxZ0FpY0c5ITlkbSZvLz1LJVhTU0ZOJmVjVmIjRnJkYko8Z2stWSxpSTxCKEliZiU/YyNRUj9iIlFKJCZnX0QsaT1dSjdjR00KOnFKQFkoJWRdcD9VZXU3QFFRJGImb0BOUkh1OCRUUWhqLypoK2lwZUUlSjg7PzYya11SX1tHMkcmJ2pzSnNfRChoKC1nL2BHMiU+IUtsS0IKP1NxLWttWVNmMzBPNmUwJDNDUnRPUEJmTVAzYzFoTmpYNjY2bGRgRVtLQDZ1SnNrSjtAPi4lQTplMWllUzE5MWhPZSwjZ0hsQzFdUVomZiMKKVNFbHIyMyVDRkhoWmItNHFRbEwmLV1EU1ZHRk82cUZrKidJZ3MyV21PczYlQjUhbmo9VT9iNzw8RjxSbHVBSERFYkJaViUwLCN0KllJIy8KS15tOWlhcjB1czNbSyUtPSwpK2NQRStGRyk0InA0RmcoS2whJnR0YWQ3SjUpL0xsRyZXLlt0LGZuPWhUKVNyXT1+PgplbmRzdHJlYW0KZW5kb2JqCjIxIDAgb2JqCjw8IC9UeXBlIC9YT2JqZWN0Ci9TdWJ0eXBlIC9JbWFnZQovV2lkdGggNTQKL0hlaWdodCA1NAovQ29sb3JTcGFjZSAvRGV2aWNlUkdCCi9CaXRzUGVyQ29tcG9uZW50IDgKL0xlbmd0aCAxMDgKL0ZpbHRlciBbL0FTQ0lJODVEZWNvZGUgL0ZsYXRlRGVjb2RlXQo+PnN0cmVhbQpHYiIwS0pJTVNqJHEzSlowP2pWRilBS2BrXnFbN1E4NWBWS1dVR1E0WUYqJCgrWCg5W1lkWUAqT0g+Um4/IiJqQjZHYSthSz1PNyNLUzUjMwpxaDNvNiNtbXFrIzlsOFpQYmRDdTgjbHJofj4KZW5kc3RyZWFtCmVuZG9iagoyMiAwIG9iago8PCAvVHlwZSAvWE9iamVjdAovU3VidHlwZSAvSW1hZ2UKL1dpZHRoIDI5NAovSGVpZ2h0IDYxCi9Db2xvclNwYWNlIC9EZXZpY2VSR0IKL0JpdHNQZXJDb21wb25lbnQgOAovTGVuZ3RoIDQ3MDQKL0ZpbHRlciBbL0FTQ0lJODVEZWNvZGUgL0ZsYXRlRGVjb2RlXQo+PnN0cmVhbQpHYiIvYTQtSm8jJGo6a1U5NjYpOC10SEA5LFleQm9EK21iTzpuUipibikqcjNodGBpI3E0NkY9U19zNzZeSCNAVHMqa3I6VC5Caz1Fb0Q9SgpVZVZkRUslKmw8VVpNaTBvXEUiWC9sKl9aWkRmTjE3P3BmVTUyVTpOaTx0VlM8TldJYDQuT1BbYlFoRXVQJ0RsMzc1aUAnIk0vUDgvVWRuIwpJalciLGs0KnBnTkFhWGgkU1BdUFgmZy4pVTw0b2VUQzk4OG46ZWw6V24sZWsqUjxkaGsoPjlLOE5hdVUrcmZZTiFhVz0tKC1fbyI1RWsjJgpvRCQobzdbcD9vS2JwbGNlTFFTTzs8OExuY2hIMSxwZC9JWDxVNEdxJVZQQURvMEYxYSxiQUllT2kwP2tPM0ZGPU08UmZeLShXQG9mVjc6QgozTmlFPSZfZGpmWGRRcDdVZ0EuQkVVRFZkS0JQb0RyVjkuODA4bzdHWTg+LTFhaDg2WU1vaGRxL0RcTGxCVXQ1K2lUTydgLShXQG9mVjc6QgozTmlFPSZfZGpmWGRRcDdVZ0EuQkVVRFZkS0JQb0RyVjkuODA4bzdHWTg+LTFhaDg2WU1vaGRxL0RcTGxCVXQ1K2lUTydgLShXQG9mVjc6QgozTmlFPSZfZGpmWGRRcDdVZ0EuQkVVRFZkS0JQb0RyVjkuODA4bzdHWTg+LTFhaDg2WU1vaGRxL0RYPGZjcGc/NTtOZzdpJ15sK3BqQWVWPwokSVg9NC1FXzlnZWIjPWAzaHBuc2dzUkQ4QV4jJzgxU004Q24yOCo9N09fWnBIU0EvXGFCXysnVzhPVkwrVlMnU28hXFRKPT87SG9TXydgUQpJOktDVmhmUjZgXWZZPkEoM3FRQ0lLUHVFWzlcUWspNCEhTG5acSMoQmJFYyMkQ1hIZzdpYCYsZlA2U2EiXD9KWEVHWG1QMkdOS1ZDR2wnZgpNcENQQi5DWlwqSFB1RG9hQHJRIjNkZjJWTik+SiFvYz9laiY5OmshNE0hampOUzYxYT90Z2EzLmUhOGJFMklFMi9MaSRkakk5RDdYaFQmdApQZG8wMVY2ZlA+ZTUsTGhZUWRrOURCLGc8Wm1oXVsvKElWNkxvPV48M2tmSmVVZ1JtWkk0Rl1SSW0hPSNNV1dRYzY/NWM2YWxWMU9IXkAkVAovLGBkOGdWLSNRaklRV21pNy4pNGlRPV1cJnVyUUE6JWFGIyRDN1hObFIidCtRM1BkOG50LFp0MUc+UzxOXClfK2E1aV5wTmlnJz8pM2hZWgpuIm1BWC8oQEFfTTZJMVpdbCJnbU4zayUtXGd1PDFeUEpoXTJCIkUsTV9cKWwmaT9gSm1qazR0XHVUI0ZWQDAlMGpKQyVzTHE0UicoUWpxVgpdLiE5MVlLaXBQLjJYKE4oUSs0UlpwRytzIUFydEdARGteMGZTXyM1VzxRMStjRF0saFR1XGtCPEszVzhhWixwJ0NsNTE3JlRuW1dmc0hZbQpodFpKMWIpaDUmT2EhTV8oZEtVST9sRDpPbU0vM00iSCdsYFpDI0EscCNWLE40VmBrQGRUdER1OFhZUC9SRU1JYFxmPVFgT2QmU1AuLmBHJQpjRDFqdChgZGw8TU4uX0U2NDBSajIhODFSYCg4KWNLVD5KVlgpTTsiYVNnRzdSUT1EWFImJFNfUSFsRWQzWCQ9bDtyQ1ZSWGNNKFVKUToqcQo5LnJyO2NcTyQwOiw1ZmdPY20tRypEJmtePCdhN1UvYXRBakVLKW9dJHRbUnI8N1JkX1o9JyZxVj9ONW4xKWAxVyhTPVhhPCldb1tmNXVSRApkQUtWajk8JDZuLj8uKFg8PyUnYFtJdFY0OHJRXzg6Jj9PMy9sSFxLMm08XiViJWFvK1VpM1BIUiYkU19RIWxFZDNYJD1sO3JDVlJYY00oVQpKUToqcTkucnI7Y1xPJDA6LDVmZ09jbS1HKkQma148LlVvX1FcJE0lM2o1ZSdlPGolXm9WWGtdRUBOQl9qSzxvZENhIm1sLHFsOXA+dCldZgowc0lYZk0zMklickkqVCMnZDJiZzJtImQzXSEpQGlIZDNuMkslYyw9LmIzXT4yVXJFTDxuIV07RS5LZXMnU3M+OWhdWjIwVWdWPCZDMiRgXQpFSF9oLUMyUksrKUhxWEpwTzM5OkhMc29eYmMqNDJfPWFCVU01WypAaWYkTSZIJllQKzciYSVeVmBuQUQ5RkQjdW1iMm0rKCc1cGVudEs9QQo8Q0dVXyIuUGFLKGw9IXVodE9mayFLbkxQZGdDL3IkWDdTYGs9ZCdMUi1bZWJYR2YpTCtGUWpmYjNeSlFTcjBCPlA2ITpHYUA6KFtlKmMkdAo2WyRQKEZwdDYoaCwwOl08YSYqYVcmPzIpOj5UMCpYTGhfNUA/UUVScDhFSWA5Y0BxdDhtTV1wSmE/PT8nUnFtUkFJUjhVL1JtR0ldNSxJdApSRiR1MUZKXyxMZF9uYU5XPGglLzdFV19WLyErbj9sUj5MPzs4Rj1IWUhDSVdOREhNXTVxLm5ELltLJ2kjLlRVOCluZVs2XTZBSC5mY18kXwowcHA3LjduJmtLQmRqZGpnOT0rLkgjIVNobVcoSUU4S28qPig4bUJgbFJQYmlQVGw0S1Y7K3VERj1DSGQncE1mdGRzRHNYO1NjNEZiVVpKMwowQVJNKT8yNER1O2AlTDBCaUB1Mi9taFY5TWBSZ3VgR2xHXklHK0hGcGEiNGVGVGxuQFExKj0nXUp1PTYpNyI/VSdwTWZ0ZHNEc1g7U2M0RgpiVVpKMzBBUk0pPzI0RHU7YCVMMEJpQHUyL21oVjlNYFJndWBHbEdeSUcrSEZwYSI0ZUZUbG5AUTEqPSddSnU9Nik3Ij9VJ3BNZnRkc0RzWAo7U2M0RmJVWkozMEFSTSk/MjREdTtgJUwwQmlAdTIvbWhWOU1gUmd1YEdsR15JRykxPmxAImY6bm5ZaCpqJDlOPFExX107KS5PbV9DLkZVYwo6Sks8LVZwcylGUmAtU1QwOV4xW25YS1pWM3VXJjRTY1AoUlAjUCZxOy0yMzZBYlZAVDtyZj9vKUtCMFZQdGAtIyM4M1ApPWVBP1gwZkcyagpHJyohQGUyNGphOGUkM049XjlETzE6XyE5Y1JELGpVW2EqLSkqaUhzOFlxWGtGWD9FXUZ0XFw0Nz8ydSEmSTE0JGZwJmFhK01mTGxvc3JRaQpAaj1RWzghY3RIOiIsVDBlQmxnOWA/aFc+UEFAVnNTLSNLSilLNURCIUwwTEY8KGI4MD00Nk1KcFFDbkJBMF0vYnBaaXRmMlBTQSNjbjNscgo9ZDRLc1UnYzc2NSFTVVZuRVBAOEM2OVo7K3NkX2YtWEZ1OktqLEtzLkhIMlgoJE1xWFUzUHNkSj5nPTVIUEJHTiRCVSclPUdWUURtI2lwQgo1XUtZVF1oPXQ+cSk8UWs0WjBYZyYxTl9TI0wvSThrLiY6MSVCQzM2X1UvXjRmYmRSS0ZcK2h0QD9gbl4uU2c0PTZsUltGNXJFZ1coMWovSwpXNUBEZmNdQHVDXV9cNGc+JUVHUjg7VnVdOFFUdDxpUldgYCRAMVREImhKTi8uJVwhcStLQC08Ry85PTQtcTApajxyYGs6P1hbKSROMCVzPAolTiFFT2tML1JNUVMwNThKamkzKiRzQkVLYlhOOyNGLyMhPj5yXXMmKF1TTlBtJDxRK0tnRFJEbDppIm1jRmdxN29pJiloNEAxZktiJFEtRAokZzVRT0JscTs6UU9IQCJVOCoqalhBVilrLERzPnVkN11IT1NFLytWSiU4SSNiPS9BSCtPXk1lVk1cPmM/SnBuQC45S0ZnK2RrSVMwOTNyKApvQWRBPDNvXTZvIyw+U1I7Kkk/Si4zNWAsN0gnZCNWKyk3ckVwOlFYPlVkO0gsI2khNDhSXFI7QCoyKjxgbjAxNGU/ayxILiN1Sm8sWT1ROQpuaW9QSz9adW1iUGJIdS1kTVpDLFZTOVQrSWYqMiQ3Pis1SEFkT2RraXFbZlVMbzdVVD8mRkZGV21TJUQ9SFhAaS5dI2lVLHU+U0ptNzxDJApEcmxKKyZBSVFKQklRPGBRX1pGTy5TUDgzSmMsO0RBO1VZVi4qTjtUaDhUIVNeKEYuVFNbb0xJJ2lecjpRYHFfYVAjUCZDL2tWY3IwJilMNgpGbCg6QCdIcWtbL15SQ01JUDQ5KkhyWlIxam9ZQUs9MzItZy07Zy1WPUxzRlJoZ0hgQGRRMk0iK3RBdD1rWl8qLz1YZUtPJltMbyVRUyhIUgpQI2pFZVI5aSwqLWxnYCdLLjIyYjhcTC8uMl5sQmA8V3IoRk1jIW5ITEBzaGk5QTMqUzxJbmw3PDhbRz9YQTxfKyw/XDhwMDhdQ15fbzhOQgpuaylBNGUqQUEiS25FUUIwU046XldSbTouIy9yTz5NUCs5ZTBlMURmWVsyYHQ4XDZGUi5QUnVubE1JMDpvdVRwQixgQms2LS4wR0hcRzNGJQo/SEdgTkhNcDcsRSk2b09WWE9tPm9mPWtjVF9PN0QmXypqNDdZVVhUZXVMR1ZCZkRrZyJmQmZNVz1GSzxRaDNuVTc/SE0jUT0lJDlGKHUsMwpBaktVOWMhdFIkWCgsa1FebmVAPlxTcmVqLCRcWl9tWmxeaCEwZXUwOVYtdFBPTVdVPi4xMmlYJEVDQ2RdS25WZlVoZ1g9R2BxL2ltRFlTNgo2X0BNQ2VscVlePk04OkRnKz4hbVhbIk1vYU0lb3IpNkEwMjMkU0YiTUVbNiVpPDVycTk3Mlc2QldabGRYPEROLzopR0VhIj1dcFFxLE1icgpWb0kocig4U0BoNE46RjEzZ09wczlOaXMuJFRHQEVfRjRbcD80Im1fNWNMTChqQ3BSUy5HUFEuVStmZEZqOzBGOFgvMDNvOyZbcSVsWE9LPwouPCVjJFojXGxoP2pyLXMwbzpkZzdIMUQ7UlstTVgoPXFHYy5gMiRQU2dlTmU4ZWoiODIjZ1wqaWAlbktRUyhdcCJcVmhHOVsjQU4/OjV1ZQpeUmJvbkFKZDtGYilLPk8wZ01iWSxWI3I0TTZncjM9c2NnQz1HUDUlI2pXUlhWUjVHNzxQMEkmcipyMFI7clFxSFxlR19rLzVdUCsxIlJbcwomVy1HYUg5MF1DQEZROEkyYmpcISEzJG0jVzQ2IlZjRG1qWFIzWjNMKkJwQUZYRks8IWRbJHE1blVhPSdOdGo2QUtxaiJaO0ZGS2dmSTkxJgpQKGolbmJidC06OVNHZmY+cjdMYCs3QS87LyVVZjlWKj1LYU9cSyM1VUVcdUdYTnM/YnAtJlxdJmJVNDltbCtzSy9WdTk/LlBPWGpWPG5LUApuWHA0VlJJNFdNWGBuPisnSmYkTEBDNW9OKyVLSl1JQ3E2K1I3RTBPZE9INE87Nk9KWDJHMTdLMGonRjZUQmdtPjhddHBBUzRRXDJHdT8sLwoyYyxQPzw+YGwjMENybmsub1cuJ0AyQkJKaDF0a2ViKTVXSUlBdE9UPiVFZ3RpTV9RWSpVQyUvTVM1KGVDLi8jKmtQSXRXQWtWLFdVJ2YsPgpXQ2k/NWNCLCJKUEI8aDM6KCJScWA2MWNgI18iQDhAalpiIVM7MyIoOkZjYCZWM21HZE0iKiZWYVtMND4kSVljSCRuNGwtV1dVNVIhJV09Tgo3WlVhLikwMmdTYV4xL2w3NnQiPEdaZFZrJ01dTWIxQzclPzRCclNGPiJOKk5gTFEhU04sUU8oL3JHUWdPPWhoajg3PkNxKkBVPVo/a0dzZQpISVVzIk9DS1MycDZzSlskRylEPWI9XXBTRTY/aCM4WT9eWmlNKy06OGNoWDdqczExQEhqYFxpOy1eYmNGPUFZKyRCcTRMNV1KRldNYidSPgpFOVZNTE9VQ1knUUInXHFwZXNLR10ranVcVktlQW9JSDpNcFhqXXB1O2hqcCVLbUVbYVhtXzRGYl0mYSlqOEJOXCIkR3BALjk5MWYuXUQ7VApiRmJsL2ZTQD5gOy1QLGwuXUBjXmluaFFlWTszR2Y+aTtOJkhLZkNrT003U0djXyVvRFNbIkNzOi9aSlArIVBFTExFLmhfYiI0dSkyKj5JRwpQOCtmLzp0UmFCbGYxYkVbSTcjS0heTmVtQihoSnRyM1QzUCpEUkwjZitkQkNaYVtZKVE4Nj9MTU5ATCEsUnM3SSxHZmtQR0NKXXNURG5tdAo7QjNVfj4KZW5kc3RyZWFtCmVuZG9iagp4cmVmCjAgMjMKMDAwMDAwMDAwMCA2NTUzNSBmIAowMDAwMDAwMDA5IDAwMDAwIG4gCjAwMDAwMDAwNTggMDAwMDAgbiAKMDAwMDAwMDEwNCAwMDAwMCBuIAowMDAwMDAwMTYyIDAwMDAwIG4gCjAwMDAwMDAyMTQgMDAwMDAgbiAKMDAwMDAwMDMxMiAwMDAwMCBuIAowMDAwMDAwNDE1IDAwMDAwIG4gCjAwMDAwMDA1MjEgMDAwMDAgbiAKMDAwMDAwMDYzMSAwMDAwMCBuIAowMDAwMDAwNzI3IDAwMDAwIG4gCjAwMDAwMDA4MjkgMDAwMDAgbiAKMDAwMDAwMDkzNCAwMDAwMCBuIAowMDAwMDAxMDQzIDAwMDAwIG4gCjAwMDAwMDExNDQgMDAwMDAgbiAKMDAwMDAwMTI0NCAwMDAwMCBuIAowMDAwMDAxMzQ2IDAwMDAwIG4gCjAwMDAwMDE0NTIgMDAwMDAgbiAKMDAwMDAwMTYyMiAwMDAwMCBuIAowMDAwMDAyMDAxIDAwMDAwIG4gCjAwMDAwMDU3OTcgMDAwMDAgbiAKMDAwMDAwNjY5NyAwMDAwMCBuIAowMDAwMDA2OTg5IDAwMDAwIG4gCnRyYWlsZXIKPDwKL0luZm8gMTcgMCBSCi9TaXplIDIzCi9Sb290IDEgMCBSCj4+CnN0YXJ0eHJlZgoxMTg3OQolJUVPRgo=");
            //var bw = new BinaryWriter(File.Open(@"D:\node\fedex\label.pdf", FileMode.OpenOrCreate));
            //bw.Write(bytes);
            //bw.Close();            

            /*
            System.Drawing.Image image;
            byte[] bytes = Convert.FromBase64String("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");
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = System.Drawing.Image.FromStream(ms);
            }
            image.Save(@"d:\node\fedex\label.png");
            */

            System.Drawing.Image i = System.Drawing.Image.FromFile(@"d:\node\fedex\label.png");            
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(0, 0, i.Width, i.Height);
            e.Graphics.DrawImage(i, rectangle);

            /*
            System.Drawing.Image resizedImage = (System.Drawing.Image)(new System.Drawing.Bitmap(image, new System.Drawing.Size(400, 600)));

            System.Drawing.Imaging.ImageAttributes imageAttr = new System.Drawing.Imaging.ImageAttributes();
            imageAttr.SetThreshold(0.55f);

            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, 410, 610);            
            e.Graphics.DrawImage(resizedImage, rect, 0, 0, 400, 600, System.Drawing.GraphicsUnit.Pixel, imageAttr);
            */


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
            
        }

        private void GetTableNames()
        {
            
        }

        private void ButtonWilliamTesting_Click(object sender, RoutedEventArgs e) // Compares AccessionOrderBuilder ao to AccessionOrderBuilderV2 ao
        {
            string resultString = string.Empty;
            DateTime startDate = DateTime.Parse("2/10/2016");
            for (int didx = 0; didx < 4; didx++)
            {
                //DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            DateTime endDate = startDate.AddDays(1);
            List<string> masterAccessionNos = new List<string>();
            //masterAccessionNos.Add("16-3667");
                SqlCommand cmd = new SqlCommand("select MasteraccessionNo from tblAccessionOrder where AccessionDate between '" + startDate.ToString() + "' and '" + endDate.ToString() + "'"); // order by 1 asc");
                cmd.CommandType = CommandType.Text;
                using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
                {
                    cn.Open();
                    cmd.Connection = cn;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string s = dr[0].ToString();
                            masterAccessionNos.Add(s);
                        }
                    }
                }

                foreach (string masterAccessionNo in masterAccessionNos)
                {
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.CommandText = "gwGetAccessionByMasterAccessionNo_A9";
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = masterAccessionNo;

                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandText = "prcGetAccessionOrder";
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = masterAccessionNo;

                    Business.Test.AccessionOrder ao1 = new Business.Test.AccessionOrder();
                    Business.Test.AccessionOrder ao2 = new Business.Test.AccessionOrder();

                    Business.Gateway.AccessionOrderBuilder accessionOrderBuilder = new Business.Gateway.AccessionOrderBuilder();
                    accessionOrderBuilder.Build(cmd1, ao1);

                    Business.Gateway.AccessionOrderBuilderV2 accessionOrderBuilderV2 = new Business.Gateway.AccessionOrderBuilderV2();
                    accessionOrderBuilderV2.Build(cmd2, ao2);

                    Business.Persistence.DocumentTestBuilders document = new Business.Persistence.DocumentTestBuilders(ao1, ao2);
                    if (document.Compare() == false)
                    {
                        resultString = ao1.MasterAccessionNo + ": ";
                        resultString += "results are not the same.";
                        MessageBox.Show(resultString);
                    }
                }
                //Console.WriteLine("Finished " + startDate.ToString("MM/dd/yyyy") + " : at " + DateTime.Now.ToString());
                //startDate = startDate.AddMonths(1);
                startDate = startDate.AddDays(2);
            }
            resultString = "done";
            MessageBox.Show(resultString);
        }

        /*private void ButtonWilliamTesting_Click(object sender, RoutedEventArgs e) //Compares MySql accession order (accession table only) to Sql Server
        {
            List<string> masterAccessionNos = new List<string>();
            masterAccessionNos.Add("16-123");
            masterAccessionNos.Add("16-17110");
            masterAccessionNos.Add("16-11839");
            masterAccessionNos.Add("16-18075");
            masterAccessionNos.Add("16-21523");
            masterAccessionNos.Add("16-19328");
            masterAccessionNos.Add("16-19220");
            masterAccessionNos.Add("16-9960");
            masterAccessionNos.Add("16-12139");
            masterAccessionNos.Add("13-30079");
            foreach (string masterAccessionNo in masterAccessionNos)
            {
                SqlCommand cmd1 = new SqlCommand();
                cmd1.CommandText = "Select 'tblAccessionOrder' tablename, ao.* from tblAccessionOrder ao where MasterAccessionNo = '13-30079'"; //gwGetAccessionByMasterAccessionNo_A8";
                cmd1.CommandType = CommandType.Text; //.StoredProcedure;
                //cmd1.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = masterAccessionNo;

                MySqlCommand cmd2 = new MySqlCommand();
                cmd2.CommandText = "Select 'tblAccessionOrder' tablename, ao.* from tblAccessionOrder ao where MasterAccessionNo = '13-30079'"; //"prcGetAccessionOrder";
                cmd2.CommandType = CommandType.Text; //.StoredProcedure;
                //cmd2.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = masterAccessionNo;

                Business.Test.AccessionOrder ao1 = new Business.Test.AccessionOrder();
                Business.Test.AccessionOrder ao2 = new Business.Test.AccessionOrder();

                Business.Gateway.AccessionOrderBuilderV2 accessionOrderBuilder = new Business.Gateway.AccessionOrderBuilderV2();
                accessionOrderBuilder.Build(cmd1, ao1);

                Business.Gateway.AccessionOrderBuilderV2 accessionOrderBuilderV2 = new Business.Gateway.AccessionOrderBuilderV2();
                accessionOrderBuilderV2.BuildMySql(cmd2, ao2);

                string resultString = ao1.MasterAccessionNo + ": ";
                Business.Persistence.ObjectComparer objectComparer = new Business.Persistence.ObjectComparer(ao1, ao2);

                StringBuilder result1 = new StringBuilder();
                Business.Persistence.JSONObjectWriter.WriteV2(result1, ao1);

                StringBuilder result2 = new StringBuilder();
                Business.Persistence.JSONObjectWriter.WriteV2(result2, ao2);

                string s1 = result1.ToString();
                string s2 = result2.ToString();

                for (int idx = 0; idx < s1.Length; idx++)
                {
                    if (s1[idx] != s2[idx])
                    {
                        resultString += s1.Substring(idx - 50, 51);
                        break;
                    }
                }
            }
            resultString = "done";
            MessageBox.Show(resultString);
        }*/

        /*private void ButtonWilliamTesting_Click(object sender, RoutedEventArgs e) //Set initial string lengths in .cs files
        {
            string basePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            basePath = basePath.Remove(0, 6);
            basePath = basePath.Replace(@"YellowstonePathology\bin\Debug", "");
            MySQLMigration.MySQLDatabaseBuilder builder = new MySQLMigration.MySQLDatabaseBuilder();
            MySQLMigration.MigrationStatusCollection migrationStatusCollection = MySQLMigration.MigrationStatusCollection.GetAll();
            foreach (MySQLMigration.MigrationStatus migrationStatus in migrationStatusCollection)
            {
                string filePath = System.IO.Path.Combine(basePath, migrationStatus.FileName);
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    if (builder.SetPersistentAttributeDefaultValue(migrationStatus, lines))
                    {
                        using (StreamWriter writer = new StreamWriter(filePath, false))
                        {
                            foreach (string line in lines)
                            {
                                writer.WriteLine(line);
                            }
                        }
                    }
                }
                else
                {
                    string s = filePath;
                    using (StreamWriter writer = new StreamWriter(@"C:\TEMP\FilesNotFound.txt", true))
                    {
                        writer.WriteLine(migrationStatus.Name);
                    }
                }
            }
            string resultString = "done";
            MessageBox.Show(resultString);
        }*/

        /*private void ButtonWilliamTesting_Click(object sender, RoutedEventArgs e) // Recalculates and Sets the length of the string in the .cs files
        {
            MySQLMigration.MySQLDatabaseBuilder builder = new MySQLMigration.MySQLDatabaseBuilder();
            List<string> dbTypes = new List<string>();
            MySQLMigration.MigrationStatusCollection migrationStatusCollection = MySQLMigration.MigrationStatusCollection.GetAll();
            foreach (MySQLMigration.MigrationStatus migrationStatus in migrationStatusCollection)
            {
                builder.GetDBDataTypes(migrationStatus.TableName, dbTypes);
            }
            using (StreamWriter writer = new StreamWriter(@"C:\TEMP\DBTypesToUse.txt", true))
            {
                foreach (string ctype in dbTypes)
                {
                    writer.WriteLine(ctype);
                }
            }
            string resultString = "done";
            MessageBox.Show(resultString);
        }*/
    }
}
