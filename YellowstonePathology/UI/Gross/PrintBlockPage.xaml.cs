using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.ComponentModel;
using System.Speech.Recognition;
using System.Collections.ObjectModel;

namespace YellowstonePathology.UI.Gross
{
	/// <summary>
	/// Interaction logic for PrintBlockPage.xaml
	/// </summary>
	public partial class PrintBlockPage : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

        public delegate void ShowBlockOptionsEventHandler(object sender, CustomEventArgs.SpecimenOrderAliquotOrderReturnEventArgs e);
        public event ShowBlockOptionsEventHandler ShowBlockOptions;

        public delegate void ShowStainOrderPageEventHandler(object sender, CustomEventArgs.SpecimenOrderAliquotOrderReturnEventArgs e);
        public event ShowStainOrderPageEventHandler ShowStainOrderPage;

        public delegate void NextEventHandler(object sender, UI.CustomEventArgs.SpecimenOrderReturnEventArgs e);
        public event NextEventHandler Next;

        public delegate void ShowProcessorSelectionPageEventHandler(object sender, UI.CustomEventArgs.SpecimenOrderReturnEventArgs e);
        public event ShowProcessorSelectionPageEventHandler ShowProcessorSelectionPage;

		System.Windows.Threading.DispatcherTimer m_ListBoxBlocksMouseDownTimer;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;        
		private YellowstonePathology.Business.View.GrossBlockManagementView m_GrossBlockManagementView;
		private XElement m_CaseNotesDocument;
		private YellowstonePathology.Business.Document.CaseDocumentCollection m_CaseDocumentCollection;
		private YellowstonePathology.UI.DocumentWorkspace m_DocumentViewer;
				
		private Nullable<int> m_Aliquots;

		private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
		private YellowstonePathology.Business.Test.Model.Test m_HandETest;
		private YellowstonePathology.Business.Test.Model.Test m_IronTest;
		private YellowstonePathology.Business.Test.Model.Test m_HPyloriTest;
		private YellowstonePathology.Business.Test.Model.Test m_FrozenTest;
        private string m_ReportNoToUse;

		private XElement m_SpecimenView;
		private YellowstonePathology.Business.Test.Model.TestCollection m_TestCollection;
		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;

		public PrintBlockPage(YellowstonePathology.Business.User.SystemIdentity systemIdentity,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,            
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
		{			
			this.m_SystemIdentity = systemIdentity;
			this.m_AccessionOrder = accessionOrder;                        
            
            this.m_SpecimenOrder = specimenOrder;

            this.SetReportNoToUse();

			this.m_CaseNotesDocument = YellowstonePathology.Business.Gateway.XmlGateway.GetOrderComments(this.m_AccessionOrder.MasterAccessionNo);
			this.m_DocumentViewer = new DocumentWorkspace();
			this.m_CaseDocumentCollection = new YellowstonePathology.Business.Document.CaseDocumentCollection(this.m_AccessionOrder, this.m_ReportNoToUse);

			this.m_GrossBlockManagementView = new Business.View.GrossBlockManagementView(this.m_AccessionOrder, this.m_CaseNotesDocument, this.m_SpecimenOrder);
			this.SetupSpecimenView();

			this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;

			this.m_TestCollection = YellowstonePathology.Business.Test.Model.TestCollection.GetAllTests();
			this.m_HandETest = this.m_TestCollection.GetTest(49);
			this.m_IronTest = this.m_TestCollection.GetTest(115);
			this.m_HPyloriTest = this.m_TestCollection.GetTest(107);
			this.m_FrozenTest = this.m_TestCollection.GetTest(45);
			this.Aliquots = 1;

			this.m_ListBoxBlocksMouseDownTimer = new System.Windows.Threading.DispatcherTimer();
			this.m_ListBoxBlocksMouseDownTimer.Interval = new TimeSpan(0, 0, 0, 0, 750);
			this.m_ListBoxBlocksMouseDownTimer.Tick += new EventHandler(ListBoxBlocksMouseDownTimer_Tick);

			InitializeComponent();
			DataContext = this;

			this.DocumentViewer.Content = this.m_DocumentViewer;

			Loaded += new RoutedEventHandler(PrintBlockPage_Loaded);
			Unloaded += new RoutedEventHandler(PrintBlockPage_Unloaded);
		}

        private void SetReportNoToUse()
        {            
            if (this.m_AccessionOrder.PanelSetOrderCollection.HasSurgical() == true)
            {
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                this.m_ReportNoToUse = panelSetOrder.ReportNo;
            }
            else
            {
                this.m_ReportNoToUse = this.m_AccessionOrder.PanelSetOrderCollection[0].ReportNo;
            }         
        }

		private void PrintBlockPage_Loaded(object sender, RoutedEventArgs e)
		{
			this.m_BarcodeScanPort.HistologyBlockScanReceived += this.HistologyBlockScanReceived;
			if (this.m_CaseDocumentCollection.GetFirstRequisition() != null)
			{
				this.m_DocumentViewer.ShowDocument(this.m_CaseDocumentCollection.GetFirstRequisition());
			}
			if (this.m_AccessionOrder.PrintMateColumnNumber != 0)
			{
				this.PrintBlocks();
			}
		}

		private void PrintBlockPage_Unloaded(object sender, RoutedEventArgs e)
		{
			this.m_BarcodeScanPort.HistologyBlockScanReceived -= this.HistologyBlockScanReceived;
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

		public YellowstonePathology.Business.View.GrossBlockManagementView GrossBlockManagementView
		{
			get { return this.m_GrossBlockManagementView; }
			set
			{
				this.m_GrossBlockManagementView = value;
				NotifyPropertyChanged("GrossBlockManagementView");
			}
		}

		public XElement SpecimenView
		{
			get { return this.m_SpecimenView; }
			set
			{
				this.m_SpecimenView = value;
				NotifyPropertyChanged("SpecimenView");
			}
		}

		public Nullable<int> Aliquots
		{
			get { return this.m_Aliquots; }
			set
			{
				this.m_Aliquots = value;
				this.NotifyPropertyChanged("Aliquots");
			}
		}

		private void HistologyBlockScanReceived(YellowstonePathology.Business.BarcodeScanning.Barcode barcode)
		{
			this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate()
			{
                if (this.m_SpecimenOrder.AliquotOrderCollection.Exists(barcode.ID))
				{
                    YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();
                    YellowstonePathology.Business.Facility.Model.LocationCollection locationCollection = YellowstonePathology.Business.Facility.Model.LocationCollection.GetAllLocations();
					YellowstonePathology.Business.Facility.Model.Facility thisFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
					YellowstonePathology.Business.Facility.Model.Location thisLocation = locationCollection.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);                    

                    YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_SpecimenOrder.AliquotOrderCollection.GetByAliquotOrderId(barcode.ID);
					string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
					YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog = new Business.MaterialTracking.Model.MaterialTrackingLog(objectId, barcode.ID, null, thisFacility.FacilityId, thisFacility.FacilityName,
                        thisLocation.LocationId, thisLocation.Description, "Block Scanned", "Block Scanned At Gross", "Aliquot", this.m_AccessionOrder.MasterAccessionNo, aliquotOrder.Label, aliquotOrder.ClientAccessioned);
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingLog, Window.GetWindow(this));					

                    aliquotOrder.GrossVerify(this.m_SystemIdentity.User);
					this.GrossBlockManagementView = new Business.View.GrossBlockManagementView(this.m_AccessionOrder, this.m_CaseNotesDocument, this.m_SpecimenOrder);
					this.SetupSpecimenView();

					if (this.m_SpecimenOrder.AliquotOrderCollection.HasUnverifiedBlocks() == false)
					{
                        CustomEventArgs.SpecimenOrderReturnEventArgs specimenOrderReturnEventArgs = new CustomEventArgs.SpecimenOrderReturnEventArgs(this.m_SpecimenOrder);
                        this.Next(this, specimenOrderReturnEventArgs);
					}					
				}
				else
				{
					MessageBox.Show("The block scanned is not from this specimen.", "Scanned Block Mismatch", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
			}
			));
		}

		private bool CheckForUnorderedTestRequest()
		{
			//Checking for Iron, OrderCommentId = 6030, TestId = 115 and for H Pylori OrderCommentId = 6040, TestId = 107
			bool result = this.HasTestRequest(6030);

			if (result == true)
			{
				if (this.HasTestOrder(115) == true) result = false;
			}

			if (result == false)
			{
				result = this.HasTestRequest(6040);
				if (result == true)
				{
					if (this.HasTestOrder(107) == true) result = false;
				}
			}
			return result;
		}

		private bool HasTestRequest(int orderCommentId)
		{
			bool result = false;
			foreach (XElement commentElement in this.m_GrossBlockManagementView.Element("CaseNotesCollection").Elements("OrderComment"))
			{
				if (commentElement.Element("OrderCommentId").Value == orderCommentId.ToString())
				{
					result = true;
				}
			}
			return result;
		}

		private bool HasTestOrder(int testId)
		{
			bool result = false;
			foreach (XElement aliquotElement in this.m_GrossBlockManagementView.Element("SpecimenOrder").Element("AliquotOrderCollection").Elements("AliquotOrder"))
			{
				foreach (XElement testElement in aliquotElement.Element("TestOrderCollection").Elements("TestOrder"))
				{
					if (testElement.Element("TestId").Value == testId.ToString())
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		private void ListBoxBlocks_MouseUp(object sender, MouseButtonEventArgs e)
		{
			this.m_ListBoxBlocksMouseDownTimer.Stop();
		}

		private void ListBoxBlocks_MouseDown(object sender, MouseButtonEventArgs e)
		{
			this.m_ListBoxBlocksMouseDownTimer.Start();
		}        

		private void ListBoxBlocksMouseDownTimer_Tick(object sender, EventArgs e)
		{
			this.m_ListBoxBlocksMouseDownTimer.Stop();

			if (this.ListBoxBlocks.SelectedItem != null)
			{				
				string aliquotOrderId = ((XElement)this.ListBoxBlocks.SelectedItem).Element("AliquotOrderId").Value;
                YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_SpecimenOrder.AliquotOrderCollection.GetByAliquotOrderId(aliquotOrderId);

                YellowstonePathology.UI.CustomEventArgs.SpecimenOrderAliquotOrderReturnEventArgs specimenOrderAliquotOrderReturnEventArgs = new CustomEventArgs.SpecimenOrderAliquotOrderReturnEventArgs(this.m_SpecimenOrder, aliquotOrder);
                this.ShowBlockOptions(this, specimenOrderAliquotOrderReturnEventArgs);
			}
		}

		private void AddBlocksToSpecimen(YellowstonePathology.Business.Test.Model.Test test, int quantity, string blockType, string prefix)
		{
			string patientInitials = YellowstonePathology.Business.Helper.PatientHelper.GetPatientInitials(this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PLastName);            

			for (int i = 0; i < quantity; i++)
			{
                YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_SpecimenOrder.AliquotOrderCollection.AddBlock(this.m_SpecimenOrder, YellowstonePathology.Business.Specimen.Model.AliquotLabelType.DirectPrint, this.m_AccessionOrder.AccessionDate.Value);
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                if (panelSetOrder == null) panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNoToUse);

                YellowstonePathology.Business.Visitor.OrderTestVisitor orderTestVisitor = new Business.Visitor.OrderTestVisitor(panelSetOrder.ReportNo, test, test.OrderComment, null, false, aliquotOrder, false, false, this.m_AccessionOrder.TaskOrderCollection);
                this.m_AccessionOrder.TakeATrip(orderTestVisitor);

                YellowstonePathology.Business.Visitor.AddSlideOrderVisitor addSlideOrderVisitor = new Business.Visitor.AddSlideOrderVisitor(aliquotOrder, orderTestVisitor.TestOrder);
                this.m_AccessionOrder.TakeATrip(addSlideOrderVisitor);
            }
		}

		private void PrintBlocks()
		{            
            YellowstonePathology.Business.Test.AliquotOrderCollection blocksToPrintCollection = this.m_SpecimenOrder.AliquotOrderCollection.GetUnPrintedBlocks();
            YellowstonePathology.Business.Label.Model.CassettePrinter cassettePrinter = new Business.Label.Model.CassettePrinter(blocksToPrintCollection, this.m_AccessionOrder);
            if (cassettePrinter.HasItemsToPrint() == true)
            {
                cassettePrinter.Print();
            }
            
			this.GrossBlockManagementView = new Business.View.GrossBlockManagementView(this.m_AccessionOrder, this.m_CaseNotesDocument, this.m_SpecimenOrder);
		}

		private void OnCommandAddBlock(int quantity)
		{
			if (this.m_AccessionOrder.PrintMateColumnNumber != 0)
			{
				this.AddBlocksToSpecimen(this.m_HandETest, quantity, "Block", string.Empty);
				this.GrossBlockManagementView = new Business.View.GrossBlockManagementView(this.m_AccessionOrder, this.m_CaseNotesDocument, this.m_SpecimenOrder);
				this.SetupSpecimenView();
				this.PrintBlocks();				
			}			
		}

		private void OnCommandAddFrozenBlock(int quantity)
		{
			if (this.m_AccessionOrder.PrintMateColumnNumber != 0)
			{
				this.AddBlocksToSpecimen(this.m_FrozenTest, quantity, "FrozenBlock", "FS");
				this.GrossBlockManagementView = new Business.View.GrossBlockManagementView(this.m_AccessionOrder, this.m_CaseNotesDocument, this.m_SpecimenOrder);
				this.SetupSpecimenView();
				this.PrintBlocks();
				//this.Save(false);
			}			
		}

		private void OnCommandAddCellBlock(int quantity)
		{
			if (this.m_AccessionOrder.PrintMateColumnNumber != 0)
			{
				this.AddBlocksToSpecimen(this.m_HandETest, quantity, "CellBlock", "CB");
				this.GrossBlockManagementView = new Business.View.GrossBlockManagementView(this.m_AccessionOrder, this.m_CaseNotesDocument, this.m_SpecimenOrder);
				this.SetupSpecimenView();
				this.PrintBlocks();
				//this.Save(false);
			}			
		}

		private void OnCommandOrderIron()
		{
			if (this.ListBoxBlocks.SelectedItem != null)
			{
				this.OrderSpecial(this.m_IronTest);
			}
		}

		private void OnCommandOrderHPylori()
		{
			if (this.ListBoxBlocks.SelectedItem != null)
			{
				this.OrderSpecial(this.m_HPyloriTest);
			}
		}

		private void OrderSpecial(YellowstonePathology.Business.Test.Model.Test test)
		{
			string aliquotOrderId = ((XElement)this.ListBoxBlocks.SelectedItem).Element("AliquotOrderId").Value;            
			foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in this.m_SpecimenOrder.AliquotOrderCollection)
			{
				if (aliquotOrder.AliquotOrderId == aliquotOrderId)
				{
					YellowstonePathology.Business.Visitor.OrderTestVisitor orderTestVisitor = new Business.Visitor.OrderTestVisitor(this.m_ReportNoToUse, test, test.OrderComment, null, false, aliquotOrder, false, false, this.m_AccessionOrder.TaskOrderCollection);
                    this.m_AccessionOrder.TakeATrip(orderTestVisitor);
					break;
				}
			}

			this.GrossBlockManagementView = new Business.View.GrossBlockManagementView(this.m_AccessionOrder, this.m_CaseNotesDocument, this.m_SpecimenOrder);
			this.SetupSpecimenView();
		}

		private void OnCommandNext()
		{
            YellowstonePathology.UI.CustomEventArgs.SpecimenOrderReturnEventArgs specimenOrderReturnEventArgs = new CustomEventArgs.SpecimenOrderReturnEventArgs(this.m_SpecimenOrder);
            this.Next(this, specimenOrderReturnEventArgs);
		}		

		private void SetupSpecimenView()
		{
			XElement view = new XElement("SpecimenOrders");            

			XElement specimenElement = new XElement("SpecimenOrder");
			XElement specimenIsSelectedElement = new XElement("IsSelected", false);
			XElement specimenOrderIdElement = new XElement("SpecimenOrderId", this.m_SpecimenOrder.SpecimenOrderId);
            XElement specimenDescriptionElement = new XElement("Description", this.m_SpecimenOrder.Description);
            XElement specimenProcessorRunElement = new XElement("ProcessorRun", this.m_SpecimenOrder.ProcessorRun);
            XElement specimenProcessorRunIdElement = new XElement("ProcessorRunId", this.m_SpecimenOrder.ProcessorRunId);
            XElement specimenFixationDurationStringElement = new XElement("FixationDurationString", this.m_SpecimenOrder.FixationDurationString);
            XElement specimenTimeToFixationStringElement = new XElement("TimeToFixationString", this.m_SpecimenOrder.TimeToFixationString);
            XElement specimenFixationCommentElement = new XElement("FixationComment", this.m_SpecimenOrder.FixationComment);

			specimenElement.Add(specimenIsSelectedElement);
			specimenElement.Add(specimenOrderIdElement);
			specimenElement.Add(specimenDescriptionElement);
            specimenElement.Add(specimenProcessorRunElement);
            specimenElement.Add(specimenProcessorRunIdElement);
            specimenElement.Add(specimenFixationDurationStringElement);
            specimenElement.Add(specimenTimeToFixationStringElement);
            specimenElement.Add(specimenFixationCommentElement);
			view.Add(specimenElement);

			foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in this.m_SpecimenOrder.AliquotOrderCollection)
			{
				XElement aliquotElement = new XElement("AliquotOrder");
				XElement aliquotIdElement = new XElement("AliquotOrderId", aliquotOrder.AliquotOrderId);
				XElement aliquotLabelElement = new XElement("Label", aliquotOrder.Display);
				XElement aliquotTypeElement = new XElement("Type", aliquotOrder.AliquotType);
				XElement aliquotIsSelectedElement = new XElement("IsSelected", false);

				aliquotElement.Add(aliquotLabelElement);
				aliquotElement.Add(aliquotIdElement);
				aliquotElement.Add(aliquotTypeElement);
				aliquotElement.Add(aliquotIsSelectedElement);
				specimenElement.Add(aliquotElement);
				
				foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
				{
					foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
					{
						foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in panelOrder.TestOrderCollection)
						{
							if (aliquotOrder.TestOrderCollection.Exists(testOrder.TestOrderId) == true)
							{
								XElement testElement = new XElement("TestOrder");
								XElement testOrderIdElement = new XElement("TestOrderId", testOrder.TestOrderId.ToString());
								XElement testNameElement = new XElement("TestName", testOrder.TestName);
								XElement testIsSelectedElement = new XElement("IsSelected", false);
								testElement.Add(testOrderIdElement);
								testElement.Add(testIsSelectedElement);
								testElement.Add(testNameElement);
								aliquotElement.Add(testElement);
							}
						}
					}
				}				
			}
			this.SpecimenView = view;
		}

		private void ButtonAddBlock_Click(object sender, RoutedEventArgs e)
		{
			OnCommandAddBlock(1);
		}

		private void ButtonNumberPad_Click(object sender, RoutedEventArgs e)
		{
			NumberPadDlg numberPadDlg = new NumberPadDlg();
			bool? result = numberPadDlg.ShowDialog();
			if (numberPadDlg.Number.HasValue)
			{
				if (numberPadDlg.Number <= 50)
				{
					OnCommandAddBlock(numberPadDlg.Number.Value);
				}
				else
				{
					MessageBox.Show("The number is to large.");
				}
			}
		}

		private void ButtonDelete_Click(object sender, RoutedEventArgs e)
		{			
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_SpecimenOrder.AliquotOrderCollection.GetLastBlock();

			if (aliquotOrder != null)
			{                
                YellowstonePathology.Business.Visitor.RemoveAliquotOrderVisitor removeAliquotOrderVisitor = new Business.Visitor.RemoveAliquotOrderVisitor(aliquotOrder);
                this.m_AccessionOrder.TakeATrip(removeAliquotOrderVisitor);

                this.m_SpecimenOrder.SetAliquotRequestCount();
				this.GrossBlockManagementView = new Business.View.GrossBlockManagementView(this.m_AccessionOrder, this.m_CaseNotesDocument, this.m_SpecimenOrder);
				this.SetupSpecimenView();
			}
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			OnCommandNext();			
		}

		private void ButtonHistory_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.UI.Common.CaseHistoryDialog caseHistoryDialog = new Common.CaseHistoryDialog(this.m_AccessionOrder);
			caseHistoryDialog.ShowDialog();
		}		

        private void ButtonStains_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListBoxBlocks.SelectedItem != null)
			{
                if (this.m_AccessionOrder.PanelSetOrderCollection.HasSurgical() == true)
                {
                    string aliquotOrderId = ((XElement)this.ListBoxBlocks.SelectedItem).Element("AliquotOrderId").Value;
                    YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_SpecimenOrder.AliquotOrderCollection.GetByAliquotOrderId(aliquotOrderId);

                    YellowstonePathology.UI.CustomEventArgs.SpecimenOrderAliquotOrderReturnEventArgs specimenOrderAliquotOrderReturnEventArgs = new CustomEventArgs.SpecimenOrderAliquotOrderReturnEventArgs(this.m_SpecimenOrder, aliquotOrder);
                    this.ShowStainOrderPage(this, specimenOrderAliquotOrderReturnEventArgs);
                }
                else
                {
                    MessageBox.Show("This feature only works if there is a Surgical");
                }
			}            
        }

        private void ButtonProcessor_Click(object sender, RoutedEventArgs e)
        {
            if (this.ShowProcessorSelectionPage != null) this.ShowProcessorSelectionPage(this, new CustomEventArgs.SpecimenOrderReturnEventArgs(this.m_SpecimenOrder));
        }        	
	}
}
