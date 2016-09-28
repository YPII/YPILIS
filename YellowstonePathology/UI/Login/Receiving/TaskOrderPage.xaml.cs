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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace YellowstonePathology.UI.Login.Receiving
{
	/// <summary>
	/// Interaction logic for TaskOrderPage.xaml
	/// </summary>
	public partial class TaskOrderPage : UserControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;

		public delegate void CloseEventHandler(object sender, EventArgs e);
		public event CloseEventHandler Close;		
				
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;		
		private YellowstonePathology.Business.Task.Model.TaskOrder m_TaskOrder;
		private PageNavigationModeEnum m_PageNavigationMode;
        private List<string> m_TaskAssignmentList;
        private YellowstonePathology.Business.Facility.Model.FacilityCollection m_FacilityCollection;

        private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;

        public TaskOrderPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Task.Model.TaskOrder taskOrder,
			PageNavigationModeEnum pageNavigationMode)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_TaskOrder = taskOrder;
			this.m_PageNavigationMode = pageNavigationMode;

            this.m_FacilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();
            this.m_TaskAssignmentList = YellowstonePathology.Business.Task.Model.TaskAssignment.GetTaskAssignmentList();
            this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;

            InitializeComponent();

			this.SetButtonVisibility();
			DataContext = this;

            Loaded += TaskOrderPage_Loaded;
            Unloaded += TaskOrderPage_Unloaded;
		}

        private void TaskOrderPage_Loaded(object sender, RoutedEventArgs e)
        {            
            this.m_BarcodeScanPort.FedexOvernightScanReceived += BarcodeScanPort_FedexOvernightScanReceived;
        }        

        private void TaskOrderPage_Unloaded(object sender, RoutedEventArgs e)
        {
             
        }

        private void BarcodeScanPort_FedexOvernightScanReceived(string scanData)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {                                    
                if(this.m_TaskOrder.TaskOrderDetailCollection.FedexShipmentExists() == true)
                {
                    Business.Task.Model.TaskOrderDetailFedexShipment taskOrderDetailFedexShipment = this.m_TaskOrder.TaskOrderDetailCollection.GetFedexShipment();
                    if(string.IsNullOrEmpty(taskOrderDetailFedexShipment.TrackingNumber) == true)
                    {
                        taskOrderDetailFedexShipment.TrackingNumber = scanData.Substring(22, 12);
                    }                    
                }
            }));            
        }

        public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		private void SetButtonVisibility()
		{
			switch (this.m_PageNavigationMode)
			{
				case PageNavigationModeEnum.Inline:
					this.ButtonBack.Visibility = System.Windows.Visibility.Visible;
					this.ButtonClose.Visibility = System.Windows.Visibility.Collapsed;
					this.ButtonNext.Visibility = System.Windows.Visibility.Visible;
					break;
				case PageNavigationModeEnum.Standalone:
					this.ButtonBack.Visibility = System.Windows.Visibility.Collapsed;
					this.ButtonClose.Visibility = System.Windows.Visibility.Visible;
					this.ButtonNext.Visibility = System.Windows.Visibility.Collapsed;
					break;
			}
		}

        public YellowstonePathology.Business.Facility.Model.FacilityCollection FacilityCollection
        {
            get { return this.m_FacilityCollection; }
        }

        public List<string> TaskAssignmentList
        {
            get { return this.m_TaskAssignmentList; }
        }

		public YellowstonePathology.Business.Task.Model.TaskOrder TaskOrder
		{
			get { return this.m_TaskOrder; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.Next != null)
            {
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                this.Next(this, new EventArgs());
            }
        }

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			if (this.Back != null) this.Back(this, new EventArgs());
		}

		private void ButtonClose_Click(object sender, RoutedEventArgs e)
		{
			if (this.Close != null) this.Close(this, new EventArgs());
		}        				

        private void HyperLinkSendToNeogenomics_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine neo = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();
            YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings ypi = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_TaskOrder.TaskOrderDetailCollection.Clear();
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_TaskOrder.ReportNo);
            panelSetOrder.TechnicalComponentBillingFacilityId = ypi.FacilityId;
            panelSetOrder.TechnicalComponentFacilityId = neo.FacilityId;

            panelSetOrder.ProfessionalComponentBillingFacilityId = neo.FacilityId;
            panelSetOrder.ProfessionalComponentFacilityId = neo.FacilityId;

            YellowstonePathology.Business.Task.Model.TaskSendBlockToNeogenomics task = new Business.Task.Model.TaskSendBlockToNeogenomics();
            string taskOrderDetailId = YellowstonePathology.Business.OrderIdParser.GetNextTaskOrderDetailId(this.m_TaskOrder.TaskOrderDetailCollection, this.m_TaskOrder.TaskOrderId);
            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail = new Business.Task.Model.TaskOrderDetail(taskOrderDetailId, this.m_TaskOrder.TaskOrderId, objectId, task);
            this.m_TaskOrder.TaskOrderDetailCollection.Add(taskOrderDetail);
        }		      

        private void HyperlingPrintTaskOrder_Click(object sender, RoutedEventArgs e)
        {
            this.PrintTaskOrder(1);
        }

        private void HyperlingPrint2CopiesTaskOrder_Click(object sender, RoutedEventArgs e)
        {
            this.PrintTaskOrder(2);
        }

        private void PrintTaskOrder(int copyCount)
        {
            Receiving.TaskOrderDataSheet taskOrderDataSheet = new Receiving.TaskOrderDataSheet(this.m_TaskOrder, this.m_AccessionOrder);
            System.Printing.PrintQueue printQueue = new System.Printing.LocalPrintServer().DefaultPrintQueue;
            System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();
            printDialog.PrintTicket.PageOrientation = System.Printing.PageOrientation.Portrait;
            printDialog.PrintTicket.CopyCount = copyCount;
            printDialog.PrintQueue = printQueue;
            printDialog.PrintDocument(taskOrderDataSheet.FixedDocument.DocumentPaginator, "Task Order Data Sheet");
            MessageBox.Show("This task has been submitted to the printer.");
        }

        private void HyperLinkAcknowledge_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperLink = (Hyperlink)e.Source;
			YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail = (YellowstonePathology.Business.Task.Model.TaskOrderDetail)hyperLink.Tag;
            taskOrderDetail.Acknowledged = true;
            taskOrderDetail.AcknowledgedById = Business.User.SystemIdentity.Instance.User.UserId;
            taskOrderDetail.AcknowledgedDate = DateTime.Now;
            taskOrderDetail.AcknowledgedByInitials = Business.User.SystemIdentity.Instance.User.Initials;

            if (this.m_TaskOrder.TaskOrderDetailCollection.HasUnacknowledgeItems() == false)
            {
                this.m_TaskOrder.Acknowledged = true;
                this.m_TaskOrder.AcknowledgedDate = DateTime.Now;
            }
        }        

        private void HyperLinkDelete_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink control = (Hyperlink)sender;
			YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail = (YellowstonePathology.Business.Task.Model.TaskOrderDetail)control.Tag;
            MessageBoxResult result = MessageBox.Show("Delete the selected Task", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (result == MessageBoxResult.Yes)
            {
                this.m_TaskOrder.TaskOrderDetailCollection.Remove(taskOrderDetail);
            }
        }

        private void HyperLinkUnacknowledge_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperLink = (Hyperlink)e.Source;
			YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail = (YellowstonePathology.Business.Task.Model.TaskOrderDetail)hyperLink.Tag;
            taskOrderDetail.Acknowledged = false;
            taskOrderDetail.AcknowledgedById = null;
            taskOrderDetail.AcknowledgedDate = null;
            taskOrderDetail.AcknowledgedByInitials = null;

            if (this.m_TaskOrder.TaskOrderDetailCollection.HasAcknowledgeItems() == false)
            {
                this.m_TaskOrder.Acknowledged = false;
                this.m_TaskOrder.AcknowledgedDate = null;
            }
        }

        private void HyperLinkGetTrackingNumber_Click(object sender, RoutedEventArgs e)
        {
            Business.Task.Model.TaskOrderDetailFedexShipment taskOrderDetail = this.m_TaskOrder.TaskOrderDetailCollection.GetFedexShipment();
            if(this.IsOKToGetTrackingNumber(taskOrderDetail) == true)
            {
                Business.Facility.Model.FacilityCollection allFacilities = Business.Facility.Model.FacilityCollection.GetAllFacilities();
                Business.Facility.Model.Facility facility = allFacilities.GetByFacilityId(taskOrderDetail.ShipToFacilityId);
                Business.MaterialTracking.Model.FedexAccountProduction fedExAccount = new Business.MaterialTracking.Model.FedexAccountProduction();
                Business.MaterialTracking.Model.FedexShipmentRequest shipmentRequest = new Business.MaterialTracking.Model.FedexShipmentRequest(facility, fedExAccount, taskOrderDetail.PaymentType, taskOrderDetail);
                Business.MaterialTracking.Model.FedexProcessShipmentReply result = shipmentRequest.RequestShipment();

                if (result.RequestWasSuccessful == true)
                {
                    taskOrderDetail.TrackingNumber = result.TrackingNumber;
                    taskOrderDetail.SetZPLFromBase64(result.ZPLII);
                }
                else
                {
                    MessageBox.Show("There was a problem with this shipping request.");
                }
            }
            else
            {
                MessageBox.Show("We are unable to get the tracking number at this point because there are problems with the data.");
            }        
        }

        private bool IsOKToGetTrackingNumber(Business.Task.Model.TaskOrderDetailFedexShipment taskOrderDetail)
        {
            bool result = true;
            if (string.IsNullOrEmpty(taskOrderDetail.TrackingNumber) == false) result = false;
            if (string.IsNullOrEmpty(taskOrderDetail.ShipToAddress1) == true) result = false;
            if (string.IsNullOrEmpty(taskOrderDetail.ShipToCity) == true) result = false;
            if (string.IsNullOrEmpty(taskOrderDetail.ShipToState) == true) result = false;
            if (string.IsNullOrEmpty(taskOrderDetail.ShipToZip) == true) result = false;
            if (string.IsNullOrEmpty(taskOrderDetail.PaymentType) == true) result = false;
            return result;
        }

        private void HyperLinkPrintLabel_Click(object sender, RoutedEventArgs e)
        {
            Business.Task.Model.TaskOrderDetailFedexShipment taskOrderDetail = this.m_TaskOrder.TaskOrderDetailCollection.GetFedexShipment();      
            if(string.IsNullOrEmpty(taskOrderDetail.ZPLII) == false)
            {
                Business.Label.Model.ZPLPrinter zplPrinter = new Business.Label.Model.ZPLPrinter("10.1.1.20");
                zplPrinter.Print(taskOrderDetail.ZPLII);
                taskOrderDetail.LabelHasBeenPrinted = true;
            }
            else
            {
                MessageBox.Show("The label cannot be printed until a tracking number exists.");
            }
        }

        private void HyperLinkCancelShipment_Click(object sender, RoutedEventArgs e)
        {
            Business.Task.Model.TaskOrderDetailFedexShipment taskOrderDetail = this.m_TaskOrder.TaskOrderDetailCollection.GetFedexShipment();
            if(string.IsNullOrEmpty(taskOrderDetail.TrackingNumber) == false)
            {
                Business.MaterialTracking.Model.FedexAccountProduction fedExAccount = new Business.MaterialTracking.Model.FedexAccountProduction();
                Business.MaterialTracking.Model.FedexDeleteShipmentRequest deleteShipmentRequest = new Business.MaterialTracking.Model.FedexDeleteShipmentRequest(fedExAccount, taskOrderDetail.TrackingNumber);
                Business.MaterialTracking.Model.FedexDeleteShipmentReply result = deleteShipmentRequest.Post();

                if(result.RequestWasSuccessful == true)
                {
                    taskOrderDetail.ZPLII = null;
                    taskOrderDetail.TrackingNumber = null;
                    taskOrderDetail.LabelHasBeenPrinted = false;
                }
                else
                {
                    MessageBox.Show("There was a problem with this Request.");
                }
            }            
        }

        private void HyperLinkSendAddGenericTask_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Task.Model.Task task = new Business.Task.Model.Task(string.Empty, string.Empty);
            string taskOrderDetailId = YellowstonePathology.Business.OrderIdParser.GetNextTaskOrderDetailId(this.m_TaskOrder.TaskOrderDetailCollection, this.m_TaskOrder.TaskOrderId);
            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail = new Business.Task.Model.TaskOrderDetail(taskOrderDetailId, this.m_TaskOrder.TaskOrderId, objectId, task);
            this.m_TaskOrder.TaskOrderDetailCollection.Add(taskOrderDetail);
        }

        private void ComboboxShipToFacility_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.IsLoaded == true)
            {
                ComboBox comboBox = (ComboBox)sender;
                Business.Facility.Model.Facility facility = (Business.Facility.Model.Facility)comboBox.SelectedItem;
                Business.Task.Model.TaskOrderDetailFedexShipment taskOrderDetail = this.m_TaskOrder.TaskOrderDetailCollection.GetFedexShipment();
                taskOrderDetail.SetShipTo(facility);
                this.NotifyPropertyChanged(string.Empty);
            }            
        }
    }
}
