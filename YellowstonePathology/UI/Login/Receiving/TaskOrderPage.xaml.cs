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
using Newtonsoft.Json;

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
        private List<string> m_FaxDocumentNameList;
        private YellowstonePathology.Business.Facility.Model.FacilityCollection m_FacilityCollection;

        private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
        private List<string> m_PaymentTypeList;

        public TaskOrderPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Task.Model.TaskOrder taskOrder,
            PageNavigationModeEnum pageNavigationMode)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_TaskOrder = taskOrder;
            this.m_PageNavigationMode = pageNavigationMode;

            this.m_FacilityCollection = Business.Facility.Model.FacilityCollection.Instance;
            this.m_TaskAssignmentList = YellowstonePathology.Business.Task.Model.TaskAssignment.GetTaskAssignmentList();
            this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;

            this.m_PaymentTypeList = new List<string>();
            this.m_PaymentTypeList.Add("SENDER");
            this.m_PaymentTypeList.Add("THIRD_PARTY");
            this.m_PaymentTypeList.Add("RECIPIENT");

            this.m_FaxDocumentNameList = new List<string>();
            this.m_FaxDocumentNameList.Add("AdditionalTestingNotification");
            this.m_FaxDocumentNameList.Add("PreauthorizationNotification");

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

        public Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public List<string> PaymentTypeList
        {
            get { return this.m_PaymentTypeList; }
        }

        public List<string> FaxDocumentNameList
        {
            get { return this.m_FaxDocumentNameList; }
        }

        private void BarcodeScanPort_FedexOvernightScanReceived(string scanData)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                if (this.m_TaskOrder.TaskOrderDetailCollection.FedexShipmentExists() == true)
                {
                    Business.Task.Model.TaskOrderDetailFedexShipment taskOrderDetailFedexShipment = this.m_TaskOrder.TaskOrderDetailCollection.GetFedexShipment();
                    if (string.IsNullOrEmpty(taskOrderDetailFedexShipment.TrackingNumber) == true)
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

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.Next != null)
            {
                if (this.ValidateFedXTaskOrderDetail() == true)
                {
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                    this.Next(this, new EventArgs());
                }
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Back != null) this.Back(this, new EventArgs());
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            if (this.Close != null)
            {
                if (this.ValidateFedXTaskOrderDetail() == true) this.Close(this, new EventArgs());
            }
        }

        private bool ValidateFedXTaskOrderDetail()
        {
            bool result = true;
            if (this.m_TaskOrder.TaskOrderDetailCollection.FedexShipmentExists() == true)
            {
                result = false;
                YellowstonePathology.Business.Task.Model.TaskOrderDetailFedexShipment taskOrderDetailFedexShipment = this.m_TaskOrder.TaskOrderDetailCollection.GetFedexShipment();
                taskOrderDetailFedexShipment.ValidateObject();
                if (taskOrderDetailFedexShipment.ValidationErrors.Count > 0)
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show(taskOrderDetailFedexShipment.Errors + Environment.NewLine +
                        "One or more FedX issues need to handled.  Are you sure you want to continue?", "FedX Issues", MessageBoxButton.YesNo,
                        MessageBoxImage.Exclamation, MessageBoxResult.No);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        result = true;
                    }
                }
                else
                {
                    result = true;
                }
            }
            return result;
        }

        private void HyperLinkSendToNeogenomics_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Facility.Model.Facility neo = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            YellowstonePathology.Business.Facility.Model.Facility ypi = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_TaskOrder.TaskOrderDetailCollection.Clear();
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_TaskOrder.ReportNo);
            panelSetOrder.TechnicalComponentBillingFacilityId = ypi.FacilityId;
            panelSetOrder.TechnicalComponentFacilityId = neo.FacilityId;

            panelSetOrder.ProfessionalComponentBillingFacilityId = neo.FacilityId;
            panelSetOrder.ProfessionalComponentFacilityId = neo.FacilityId;

            YellowstonePathology.Business.Task.Model.TaskSendBlockToNeogenomics task = new Business.Task.Model.TaskSendBlockToNeogenomics();
            string taskOrderDetailId = YellowstonePathology.Business.OrderIdParser.GetNextTaskOrderDetailId(this.m_TaskOrder.TaskOrderDetailCollection, this.m_TaskOrder.TaskOrderId);
            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail = new Business.Task.Model.TaskOrderDetail(taskOrderDetailId, this.m_TaskOrder.TaskOrderId, objectId, task, this.m_AccessionOrder.ClientId);
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
            taskOrderDetail.ValidateObject();
            if (taskOrderDetail.ValidationErrors.Count == 0)
            {
                if (this.IsOKToGetTrackingNumber(taskOrderDetail) == true)
                {
                    string masterAccessionNo = taskOrderDetail.TaskOrderDetailId.Split(new char[] { '.' })[0];
                    Business.Facility.Model.Facility facility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(taskOrderDetail.ShipToFacilityId);
                    Business.MaterialTracking.Model.FedexAccountProduction fedExAccount = new Business.MaterialTracking.Model.FedexAccountProduction();
                    Business.MaterialTracking.Model.FedexShipmentRequest shipmentRequest = new Business.MaterialTracking.Model.FedexShipmentRequest(fedExAccount,
                        masterAccessionNo, taskOrderDetail.PaymentType, taskOrderDetail.ServiceType, taskOrderDetail.TrackingNumber, taskOrderDetail.ShipToName, 
                        taskOrderDetail.ShipToPhone, taskOrderDetail.ShipToAddress1, taskOrderDetail.ShipToAddress2, taskOrderDetail.ShipToCity, 
                        taskOrderDetail.ShipToState, taskOrderDetail.ShipToZip, taskOrderDetail.AccountNo);
                    Business.MaterialTracking.Model.FedexProcessShipmentReply result = shipmentRequest.RequestShipment();

                    if (result.RequestWasSuccessful == true)
                    {
                        taskOrderDetail.TrackingNumber = result.TrackingNumber;
                        taskOrderDetail.ZPLII = Business.Label.Model.ZPLPrinterTCP.DecodeZPLFromBase64(result.ZPLII);
                    }
                    else
                    {
                        MessageBox.Show(result.Message);
                    }
                }
                else
                {
                    MessageBox.Show("We are unable to get the tracking number at this point because there are problems with the data.");
                }
            }
            else
            {
                string message = "We are unable to get the tracking number at this point because" + Environment.NewLine + taskOrderDetail.Errors;
                MessageBox.Show(message);
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
            if (string.IsNullOrEmpty(taskOrderDetail.ZPLII) == false)
            {
                if (string.IsNullOrEmpty(Business.User.UserPreferenceInstance.Instance.UserPreference.FedExLabelPrinter) == false)
                {
                    Business.Label.Model.ZPLPrinterTCP zplPrinter = new Business.Label.Model.ZPLPrinterTCP(Business.User.UserPreferenceInstance.Instance.UserPreference.FedExLabelPrinter);
                    zplPrinter.Print(taskOrderDetail.ZPLII);
                    taskOrderDetail.LabelHasBeenPrinted = true;
                }
                else
                {
                    MessageBox.Show("You need to go into User Preferences and choose your FedEx label printer before your label can be printed.");
                }
            }
            else
            {
                MessageBox.Show("The label cannot be printed until a tracking number exists.");
            }
        }

        private void HyperLinkCancelShipment_Click(object sender, RoutedEventArgs e)
        {
            Business.Task.Model.TaskOrderDetailFedexShipment taskOrderDetail = this.m_TaskOrder.TaskOrderDetailCollection.GetFedexShipment();
            if (string.IsNullOrEmpty(taskOrderDetail.TrackingNumber) == false)
            {
                Business.MaterialTracking.Model.FedexAccountProduction fedExAccount = new Business.MaterialTracking.Model.FedexAccountProduction();
                Business.MaterialTracking.Model.FedexDeleteShipmentRequest deleteShipmentRequest = new Business.MaterialTracking.Model.FedexDeleteShipmentRequest(fedExAccount, taskOrderDetail.TrackingNumber);
                Business.MaterialTracking.Model.FedexDeleteShipmentReply result = deleteShipmentRequest.Post();

                if (result.RequestWasSuccessful == true)
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
            YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail = new Business.Task.Model.TaskOrderDetail(taskOrderDetailId, this.m_TaskOrder.TaskOrderId, objectId, task, this.m_AccessionOrder.ClientId);
            this.m_TaskOrder.TaskOrderDetailCollection.Add(taskOrderDetail);
        }

        private void ComboboxShipToFacility_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded == true)
            {
                ComboBox comboBox = (ComboBox)sender;
                if (comboBox.SelectionBoxItem != null)
                {
                    Business.Facility.Model.Facility facility = (Business.Facility.Model.Facility)comboBox.SelectedItem;
                    if (facility != null)
                    {
                        Business.Task.Model.TaskOrderDetailFedexShipment taskOrderDetail = this.m_TaskOrder.TaskOrderDetailCollection.GetFedexShipment();
                        taskOrderDetail.SetShipTo(facility);
                    }
                }
                this.NotifyPropertyChanged(string.Empty);
            }
        }

        private void HyperLinkSendFax_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperlink = (Hyperlink)sender;
            YellowstonePathology.Business.Task.Model.TaskOrderDetailFax taskOrderDetailFax = (YellowstonePathology.Business.Task.Model.TaskOrderDetailFax)hyperlink.Tag;
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_TaskOrder.ReportNo);
            Business.OrderIdParser orderIdParser = new Business.OrderIdParser(panelSetOrder.ReportNo);

            if(string.IsNullOrEmpty(taskOrderDetailFax.FaxNumber) == true || taskOrderDetailFax.FaxNumber.Length != 10)
            {
                MessageBox.Show("The Fax Number must be 10 digits.");
                return;
            }

            if (taskOrderDetailFax.DocumentName == "AdditionalTestingNotification")
            {
                YellowstonePathology.Business.Test.AdditionalTestingNotification.AdditionalTestingNotificationWordDocument reportNotify =
                new YellowstonePathology.Business.Test.AdditionalTestingNotification.AdditionalTestingNotificationWordDocument(this.m_AccessionOrder, panelSetOrder, Business.Document.ReportSaveModeEnum.Normal, taskOrderDetailFax.SendToName);
                reportNotify.Render();
                reportNotify.Publish();

                System.Threading.Thread.Sleep(2000);

                string notifyFileName = Business.Document.CaseDocument.GetCaseFileNameTifNotify(orderIdParser);
                Business.ReportDistribution.Model.FaxSubmission.Submit(taskOrderDetailFax.FaxNumber, panelSetOrder.ReportNo + " - Additional Testing Notification", notifyFileName);
                MessageBox.Show("The fax was successfully submitted.");
            }
            else if(taskOrderDetailFax.DocumentName == "PreauthorizationNotification")
            {
                YellowstonePathology.Business.Test.ExtractAndHoldForPreauthorization.ExtractAndHoldForPreauthorizationWordDocument reportPreauth =
                new YellowstonePathology.Business.Test.ExtractAndHoldForPreauthorization.ExtractAndHoldForPreauthorizationWordDocument(this.m_AccessionOrder, panelSetOrder, Business.Document.ReportSaveModeEnum.Normal);
                reportPreauth.Render();
                reportPreauth.Publish();

                System.Threading.Thread.Sleep(2000);

                string preauthFileName = Business.Document.CaseDocument.GetCaseFileNameTifPreAuth(orderIdParser);
                Business.ReportDistribution.Model.FaxSubmission.Submit(taskOrderDetailFax.FaxNumber, panelSetOrder.ReportNo + "Preauthorization Notification", preauthFileName);
                MessageBox.Show("The fax was successfully submitted.");
            }                       
        }        

        private void HyperLinkAddSendAdditionalTestingFaxTask_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Task.Model.TaskFax task = new Business.Task.Model.TaskFax(string.Empty, string.Empty, "AdditionalTesetingNotification");
            string taskOrderDetailId = YellowstonePathology.Business.OrderIdParser.GetNextTaskOrderDetailId(this.m_TaskOrder.TaskOrderDetailCollection, this.m_TaskOrder.TaskOrderId);
            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

            Business.Client.Model.Client client = Business.Gateway.PhysicianClientGateway.GetClientByClientId(this.m_AccessionOrder.ClientId);
            YellowstonePathology.Business.Task.Model.TaskOrderDetailFax taskOrderDetail = new Business.Task.Model.TaskOrderDetailFax(taskOrderDetailId, this.m_TaskOrder.TaskOrderId, objectId, task, this.m_AccessionOrder.ClientId);
            taskOrderDetail.FaxNumber = client.Fax;
            this.m_TaskOrder.TaskOrderDetailCollection.Add(taskOrderDetail);
        }

        private void HyperLinkAddSendAuthorizationFaxTask_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Task.Model.TaskFax task = new Business.Task.Model.TaskFax(string.Empty, string.Empty, "PreauthorizationNotification");
            string taskOrderDetailId = YellowstonePathology.Business.OrderIdParser.GetNextTaskOrderDetailId(this.m_TaskOrder.TaskOrderDetailCollection, this.m_TaskOrder.TaskOrderId);
            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

            Business.Client.Model.Client client = Business.Gateway.PhysicianClientGateway.GetClientByClientId(this.m_AccessionOrder.ClientId);
            YellowstonePathology.Business.Task.Model.TaskOrderDetailFax taskOrderDetail = new Business.Task.Model.TaskOrderDetailFax(taskOrderDetailId, this.m_TaskOrder.TaskOrderId, objectId, task, this.m_AccessionOrder.ClientId);
            taskOrderDetail.FaxNumber = client.Fax;
            this.m_TaskOrder.TaskOrderDetailCollection.Add(taskOrderDetail);
        }
    }
}
