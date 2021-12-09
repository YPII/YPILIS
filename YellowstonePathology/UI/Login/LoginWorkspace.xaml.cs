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
using System.Diagnostics;

namespace YellowstonePathology.UI.Login
{
    /// <summary>
    /// Interaction logic for LoginWorkspace.xaml
    /// </summary>
    public partial class LoginWorkspace : UserControl
    {
        public delegate void BringPageToForeEventHandler(object sender, EventArgs e);
        public event BringPageToForeEventHandler BringPageToFore;

        private YellowstonePathology.UI.DocumentWorkspace m_DocumentViewer;

        private LoginUIV2 m_LoginUI;
        private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
        private bool m_LoadedHasRun;
        private MainWindowCommandButtonHandler m_MainWindowCommandButtonHandler;
        private TabItem m_Writer;

        private Billing.BillingPath m_BillingPath;

        private Login.Receiving.LoginPageWindow m_LoginPageWindow;
        private Business.GrossCameraPubSubHandler m_GrossCameraPubSubHandler;      

        public LoginWorkspace(MainWindowCommandButtonHandler mainWindowCommandButtonHandler, TabItem writer)
        {
            this.m_MainWindowCommandButtonHandler = mainWindowCommandButtonHandler;
            this.m_LoadedHasRun = false;
            this.m_Writer = writer;

            this.m_GrossCameraPubSubHandler = new Business.GrossCameraPubSubHandler();
            this.m_LoginUI = new LoginUIV2(this.m_Writer);
            this.m_DocumentViewer = new DocumentWorkspace();

            this.m_BarcodeScanPort = Business.BarcodeScanning.BarcodeScanPort.Instance;

            InitializeComponent();

            this.TabItemDocumentWorkspace.Content = this.m_DocumentViewer;
            this.DataContext = this.m_LoginUI;

            this.Loaded += new RoutedEventHandler(LoginWorkspace_Loaded);
            this.Unloaded += new RoutedEventHandler(LoginWorkspace_Unloaded);            
        }        

        private void LoginWorkspace_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.m_LoadedHasRun == false)
            {
                this.ComboBoxCaseType.SelectedValue = Business.CaseType.ALLCaseTypes;
                this.m_BarcodeScanPort.ContainerScanReceived += ContainerScanReceived;
                this.m_BarcodeScanPort.HistologySlideScanReceived += new Business.BarcodeScanning.BarcodeScanPort.HistologySlideScanReceivedHandler(BarcodeScanPort_HistologySlideScanReceived);
                this.m_BarcodeScanPort.AliquotOrderIdReceived += BarcodeScanPort_AliquotOrderIdReceived;
                this.m_BarcodeScanPort.VantageSlideScanReceived += BarcodeScanPort_VantageSlideScanReceived;
                this.m_BarcodeScanPort.HistologyBlockScanReceived += BarcodeScanPort_HistologyBlockScanReceived;

                this.m_MainWindowCommandButtonHandler.StartProviderDistributionPath += MainWindowCommandButtonHandler_StartProviderDistributionPath;
                this.m_MainWindowCommandButtonHandler.Save += MainWindowCommandButtonHandler_Save;
                this.m_MainWindowCommandButtonHandler.ShowAmendmentDialog += MainWindowCommandButtonHandler_ShowAmendmentDialog;
                this.m_MainWindowCommandButtonHandler.RemoveTab += MainWindowCommandButtonHandler_RemoveTab;
                this.m_MainWindowCommandButtonHandler.ShowMessagingDialog += MainWindowCommandButtonHandler_ShowMessagingDialog;
                this.m_MainWindowCommandButtonHandler.ShowCaseDocument += MainWindowCommandButtonHandler_ShowCaseDocument;

                UI.AppMessaging.MessagingPath.Instance.LockReleasedActionList.Add(this.Save);
                UI.AppMessaging.MessagingPath.Instance.LockAquiredActionList.Add(this.HandleAccessionOrderListChange);
            }

            this.m_LoadedHasRun = true;
        }

        private void BarcodeScanPort_HistologyBlockScanReceived(Business.BarcodeScanning.Barcode barcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {                
                this.m_BarcodeScanPort.HistologyBlockScanReceived -= BarcodeScanPort_HistologyBlockScanReceived;
                this.m_LoginUI.GetReportSearchListByAliquotOrderId(barcode.ID);                
                this.m_BarcodeScanPort.HistologyBlockScanReceived += BarcodeScanPort_HistologyBlockScanReceived;
            }));
        }

        private void MainWindowCommandButtonHandler_ShowCaseDocument(object sender, EventArgs e)
        {
            if (this.m_LoginUI.AccessionOrder != null)
            {
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_LoginUI.AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_LoginUI.ReportNo);
                YellowstonePathology.Business.Interface.ICaseDocument caseDocument = Business.Document.DocumentFactory.GetDocument(this.m_LoginUI.AccessionOrder, panelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
                caseDocument.Render();
                YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWord(caseDocument.SaveFileName);
            }
        }

        private void MainWindowCommandButtonHandler_RemoveTab(object sender, EventArgs e)
        {
            Business.Persistence.DocumentGateway.Instance.Push(this.m_Writer);            
        }

        private void MainWindowCommandButtonHandler_ShowMessagingDialog(object sender, EventArgs e)
        {
            if(this.ListViewAccessionOrders.SelectedItem != null)
            {
               AppMessaging.MessagingPath.Instance.Start(this.m_LoginUI.AccessionOrder);                
            }            
        }

        private void MainWindowCommandButtonHandler_Save(object sender, EventArgs e)
        {
            this.Save();
        }

        private void Save()
        {
            if (this.m_LoginUI.AccessionOrder != null)
            {
                Business.Persistence.DocumentGateway.Instance.ReleaseLock(this.m_LoginUI.AccessionOrder, this.m_Writer);

                if (this.m_LoginUI.AccessionOrder.AccessionLock.IsLockAquiredByMe == true)
                {
                    this.TabControlRightSide.SelectedIndex = 1;
                    this.TabItemTasks.IsEnabled = true;
                }
                else
                {
                    this.TabControlRightSide.SelectedIndex = 0;
                    this.TabItemTasks.IsEnabled = false;
                }

                this.m_LoginUI.ReportSearchList.SetLockIsAquiredByMe(this.m_LoginUI.AccessionOrder);
            }
        }

        private void BarcodeScanPort_AliquotOrderIdReceived(string scanData)
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                this.m_BarcodeScanPort.AliquotOrderIdReceived -= BarcodeScanPort_AliquotOrderIdReceived;
                this.m_LoginUI.GetReportSearchListByAliquotOrderId(scanData);
                this.m_BarcodeScanPort.ContainerScanReceived += ContainerScanReceived;
            }
            ));
        }

        private void BarcodeScanPort_VantageSlideScanReceived(string scanData)
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                this.HandleVantageSlideScan(scanData);
            }
            ));
        }

        private void MainWindowCommandButtonHandler_StartProviderDistributionPath(object sender, EventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {                
                YellowstonePathology.Business.Search.ReportSearchItem reportSearchItem = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewAccessionOrders.SelectedItem;

                FinalizeAccession.ProviderDistributionPath providerDistributionPath = new FinalizeAccession.ProviderDistributionPath(reportSearchItem.ReportNo, this.m_LoginUI.AccessionOrder,
                    System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed);
                providerDistributionPath.Start();
            }
        }

        private void MainWindowCommandButtonHandler_ShowAmendmentDialog(object sender, EventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.Search.ReportSearchItem reportSearchItem = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewAccessionOrders.SelectedItem;
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_LoginUI.AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportSearchItem.ReportNo);
                YellowstonePathology.UI.AmendmentPageController amendmentPageController = new AmendmentPageController(this.m_LoginUI.AccessionOrder, panelSetOrder);
                amendmentPageController.ShowDialog();
            }
        }

        private void LoginWorkspace_Unloaded(object sender, RoutedEventArgs e)
        {
            this.m_LoadedHasRun = false;

            this.m_BarcodeScanPort.ContainerScanReceived -= ContainerScanReceived;
            this.m_BarcodeScanPort.HistologySlideScanReceived -= BarcodeScanPort_HistologySlideScanReceived;
            this.m_BarcodeScanPort.AliquotOrderIdReceived -= BarcodeScanPort_AliquotOrderIdReceived;
            this.m_BarcodeScanPort.VantageSlideScanReceived -= BarcodeScanPort_VantageSlideScanReceived;

            this.m_MainWindowCommandButtonHandler.StartProviderDistributionPath -= MainWindowCommandButtonHandler_StartProviderDistributionPath;
            this.m_MainWindowCommandButtonHandler.Save -= MainWindowCommandButtonHandler_Save;
            this.m_MainWindowCommandButtonHandler.ShowAmendmentDialog -= MainWindowCommandButtonHandler_ShowAmendmentDialog;
            this.m_MainWindowCommandButtonHandler.RemoveTab -= MainWindowCommandButtonHandler_RemoveTab;
            this.m_MainWindowCommandButtonHandler.ShowMessagingDialog -= MainWindowCommandButtonHandler_ShowMessagingDialog;
            this.m_MainWindowCommandButtonHandler.ShowCaseDocument -= MainWindowCommandButtonHandler_ShowCaseDocument;

            UI.AppMessaging.MessagingPath.Instance.LockReleasedActionList.Remove(this.Save);
            UI.AppMessaging.MessagingPath.Instance.LockAquiredActionList.Remove(this.HandleAccessionOrderListChange);

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_LoginUI.AccessionOrder != null)
            {
                Business.Persistence.DocumentGateway.Instance.ReleaseLock(this.m_LoginUI.AccessionOrder, this.m_Writer);
            }

            SearchPath searchPath = new SearchPath(this.m_LoginUI);
            searchPath.Start();
        }

        private void SearchPath_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {
            switch (e.PageNavigationDirectionEnum)
            {
                case UI.Navigation.PageNavigationDirectionEnum.Next:
                    this.m_LoginUI.NotifyPropertyChanged("ReportSearchList");
                    break;
                case UI.Navigation.PageNavigationDirectionEnum.Back:
                    break;
            }
        }

        private void ButtonAccessionOrderBack_Click(object sender, RoutedEventArgs args)
        {
            if (this.m_LoginUI.AccessionOrder != null)
            {
                Business.Persistence.DocumentGateway.Instance.ReleaseLock(this.m_LoginUI.AccessionOrder, this.m_Writer);
            }

            this.m_LoginUI.AccessionOrderDate = this.m_LoginUI.AccessionOrderDate.AddDays(-1);
            this.m_LoginUI.GetReportSearchList();
        }

        private void ButtonAccessionOrderForward_Click(object sender, RoutedEventArgs args)
        {
            if (this.m_LoginUI.AccessionOrder != null)
            {
                Business.Persistence.DocumentGateway.Instance.ReleaseLock(this.m_LoginUI.AccessionOrder, this.m_Writer);
            }

            this.m_LoginUI.AccessionOrderDate = this.m_LoginUI.AccessionOrderDate.AddDays(1);
            this.m_LoginUI.GetReportSearchList();
        }

        private void ButtonAccessionOrderRefresh_Click(object sender, RoutedEventArgs args)
        {
            if(this.m_LoginUI.AccessionOrder != null)
            {
                Business.Persistence.DocumentGateway.Instance.ReleaseLock(this.m_LoginUI.AccessionOrder, this.m_Writer);
            }

            this.m_LoginUI.GetReportSearchList();
        }

        private void ListViewAccessionOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.Search.ReportSearchItem item = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewAccessionOrders.SelectedItem;
                this.GetCase(item.MasterAccessionNo, item.ReportNo);
                this.HandleAccessionOrderListChange();
            }
        }

        private void HandleAccessionOrderListChange()
        {
            App.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                this.m_LoginUI.SelectedItemCount = "Selected Items: " + this.ListViewAccessionOrders.SelectedItems.Count.ToString();

                if (this.m_LoginUI.AccessionOrder.AccessionLock.IsLockAquiredByMe == true)
                {
                    this.TabControlRightSide.SelectedIndex = 1;
                    this.TabItemTasks.IsEnabled = true;
                }
                else
                {
                    this.TabControlRightSide.SelectedIndex = 0;
                    this.TabItemTasks.IsEnabled = false;
                }

                this.m_LoginUI.ReportSearchList.SetLockIsAquiredByMe(this.m_LoginUI.AccessionOrder);
            }));            
        }

        public void GetCase(string masterAccessionNo, string reportNo)
        {
            this.m_LoginUI.GetAccessionOrder(masterAccessionNo, reportNo);
            this.m_DocumentViewer.ClearContent();

            if (string.IsNullOrEmpty(this.m_LoginUI.AccessionOrder.SvhAccount) == false)
            {
                YellowstonePathology.Business.Document.AccessionOrderCaseDocument accessionOrderCaseDocument = new Business.Document.AccessionOrderCaseDocument(reportNo, masterAccessionNo);
                this.m_DocumentViewer.ShowDocument(accessionOrderCaseDocument);
            }
            else
            {
                Business.Document.CaseDocument caseDocument = this.m_LoginUI.CaseDocumentCollection.GetFirstRequisition();
                this.m_DocumentViewer.ShowDocument(caseDocument);
            }
        }

        private void BarcodeScanPort_HistologySlideScanReceived(Business.BarcodeScanning.Barcode barcode)
        {
            /*
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                this.m_BarcodeScanPort.HistologySlideScanReceived -= BarcodeScanPort_HistologySlideScanReceived;
                this.m_LoginUI.GetAccessionOrderBySlideOrderId(barcode.ID);
                if (this.ListViewAccessionOrders.Items.Count > 0)
                {
                    this.ListViewAccessionOrders.SelectedItem = this.ListViewAccessionOrders.Items[0];
                }
                this.m_BarcodeScanPort.ContainerScanReceived += ContainerScanReceived;
            }
            ));
            */
        }

        private void ContainerScanReceived(YellowstonePathology.Business.BarcodeScanning.ContainerBarcode containerBarcode)
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                this.m_BarcodeScanPort.ContainerScanReceived -= ContainerScanReceived;
                if (this.ContainerExistsInAccessionOrder(containerBarcode.ToString()) == true)
                {
                    YellowstonePathology.Business.User.SystemIdentity systemIdentity = Business.User.SystemIdentity.Instance;
                    this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();

                    SpecimenOrderDetailsPath specimenOrderDetailsPath = new SpecimenOrderDetailsPath(this.m_LoginUI.AccessionOrder, containerBarcode.ToString(), this.m_LoginPageWindow.PageNavigator);
                    specimenOrderDetailsPath.Finish += new SpecimenOrderDetailsPath.FinishEventHandler(SpecimenOrderDetailsPath_Finish);
                    specimenOrderDetailsPath.Start();
                    this.m_LoginPageWindow.ShowDialog();
                }
                else
                {
                    if (this.m_LoginUI.GetAccessionOrderByContainerId(containerBarcode.ToString()) == true)
                    {
                        if (this.ListViewAccessionOrders.Items.Count > 0)
                        {
                            this.ListViewAccessionOrders.SelectedItem = this.ListViewAccessionOrders.Items[0];
                        }
                    }
                }
                this.m_BarcodeScanPort.ContainerScanReceived += ContainerScanReceived;
            }
            ));
        }

        private void SpecimenOrderDetailsPath_Finish(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        private bool ContainerExistsInAccessionOrder(string containerId)
        {
            bool result = false;
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                if (this.m_LoginUI.AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByContainerId(containerId) != null)
                {
                    result = true;
                }
            }
            return result;
        }

        private void TileFinalize_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.Search.ReportSearchItem reportSearchItem = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewAccessionOrders.SelectedItem;

                if (this.m_LoginUI.AccessionOrder.PanelSetOrderCollection.HasThinPrepPapOrder() == true)
                {
                    if (string.IsNullOrEmpty(this.m_LoginUI.AccessionOrder.ClientOrderId) == false)
                    {
                        YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = Business.Persistence.DocumentGateway.Instance.PullClientOrder(this.m_LoginUI.AccessionOrder.ClientOrderId, this.m_Writer);

                        YellowstonePathology.Business.User.SystemIdentity systemIdentity = Business.User.SystemIdentity.Instance;
                        this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
                        this.m_LoginPageWindow.Show();

                        YellowstonePathology.UI.Login.FinalizeAccession.FinalizeCytologyPath finalizeCytologyPath = new YellowstonePathology.UI.Login.FinalizeAccession.FinalizeCytologyPath(clientOrder, this.m_LoginUI.AccessionOrder, this.m_LoginUI.ReportNo, this.m_LoginPageWindow.PageNavigator);
                        finalizeCytologyPath.Start();
                        finalizeCytologyPath.Finish += new YellowstonePathology.UI.Login.FinalizeAccession.FinalizeCytologyPath.FinishEventHandler(CytologyFinalizationPath_Finish);
                        finalizeCytologyPath.Return += new FinalizeAccession.FinalizeCytologyPath.ReturnEventHandler(FinalizeCytologyPath_Return);
                    }
                }
                else
                {
                    YellowstonePathology.UI.Login.FinalizeAccession.FinalizeAccessionPathWithSecurity finalizeAccessionPathWithSecurity = new FinalizeAccession.FinalizeAccessionPathWithSecurity(this.m_LoginUI.ReportNo, this.m_LoginUI.AccessionOrder);
                    finalizeAccessionPathWithSecurity.Start();
                }
            }
        }

        private void FinalizeCytologyPath_Return(object sender, Navigation.PageNavigationReturnEventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        private void CytologyFinalizationPath_Finish(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        private void TilePatient_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                YellowstonePathology.UI.Login.FinalizeAccession.PatientDemographicsPath patientDemographicsPath = new FinalizeAccession.PatientDemographicsPath(this.m_LoginUI.AccessionOrder);
                patientDemographicsPath.Start();
            }
        }

        private void TileLinking_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                FinalizeAccession.PatientLinkingPath patientLinkingPath = new FinalizeAccession.PatientLinkingPath(this.m_LoginUI.AccessionOrder, this.m_LoginUI.ReportNo);
                patientLinkingPath.Start();
            }
        }

        private void TileProviderDistribution_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.Search.ReportSearchItem reportSearchItem = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewAccessionOrders.SelectedItem;

                FinalizeAccession.ProviderDistributionPath providerDistributionPath = new FinalizeAccession.ProviderDistributionPath(reportSearchItem.ReportNo, this.m_LoginUI.AccessionOrder,
                    System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed);
                providerDistributionPath.Start();
            }
        }

        private void TileAssignment_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                FinalizeAccession.AssignmentPath assignmentPath = new FinalizeAccession.AssignmentPath(this.m_LoginUI.AccessionOrder);
                assignmentPath.Start();
            }
        }

        private void TileMaterialTracking_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {                
                YellowstonePathology.UI.MaterialTracking.MaterialTrackingPath materialTrackingPath = new MaterialTracking.MaterialTrackingPath(this.m_LoginUI.AccessionOrder.MasterAccessionNo);
                materialTrackingPath.Start();
            }
        }

        private void TileScanning_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                FinalizeAccession.DocumentScanningPath documentScanningPath = new FinalizeAccession.DocumentScanningPath(this.m_LoginUI.AccessionOrder);
                documentScanningPath.Start();
            }
        }

        private void TileAliquotsAndStains_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();             
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_LoginUI.AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_LoginUI.ReportNo);
                YellowstonePathology.UI.Login.FinalizeAccession.AliquotAndStainOrderPath aliquotAndStainOrderPath = new FinalizeAccession.AliquotAndStainOrderPath(this.m_LoginUI.AccessionOrder, panelSetOrder, this.m_LoginPageWindow);
                aliquotAndStainOrderPath.Start();                
            }
        }

        private void TileDocuments_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                DocumentListDialog documentListDialog = new DocumentListDialog(this.m_LoginUI.AccessionOrder, this.m_LoginUI.ReportNo);
                documentListDialog.ShowDialog();
            }
        }

        private void TileContainerLabels_MouseUp(object sender, MouseButtonEventArgs e)
        {
            YellowstonePathology.UI.Test.BarcodeLabelDialog containerLabelDialog = new YellowstonePathology.UI.Test.BarcodeLabelDialog();
            containerLabelDialog.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            containerLabelDialog.ShowDialog();
        }

        private void TileAccessionLabels_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.Search.ReportSearchItem reportSearchItem = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewAccessionOrders.SelectedItem;
                YellowstonePathology.UI.Test.AccessionLabelDialog accessionLabelDialog = new Test.AccessionLabelDialog(reportSearchItem.MasterAccessionNo, reportSearchItem.PFirstName, reportSearchItem.PLastName);
                accessionLabelDialog.ShowDialog();
            }
            else
            {
                YellowstonePathology.UI.Test.AccessionLabelDialog accessionLabelDialog = new Test.AccessionLabelDialog();
                accessionLabelDialog.ShowDialog();
            }
        }

        private void TileFixation_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                FinalizeAccession.FixationPath fixationPath = new FinalizeAccession.FixationPath(this.m_LoginUI.AccessionOrder);
                fixationPath.Start();
            }
        }

        private void TileCaseNotes_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                FinalizeAccession.LoginCaseNotesPath loginCaseNotesPath = new FinalizeAccession.LoginCaseNotesPath(this.m_LoginUI.AccessionOrder);
                loginCaseNotesPath.Start();

            }
        }

        private void TileReportOrders_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = Business.Gateway.ClientOrderGateway.GetClientOrdersByMasterAccessionNo(this.m_LoginUI.AccessionOrder.MasterAccessionNo);

                if (clientOrderCollection.Count != 0)
                {
                    Login.Receiving.AccessionOrderPath accessionOrderPath = new Receiving.AccessionOrderPath(this.m_LoginUI.AccessionOrder, clientOrderCollection[0], PageNavigationModeEnum.Standalone);
                    accessionOrderPath.Start();
                }
                else
                {
                    MessageBox.Show("No Client Order was found.  Please contact IT.");
                }
            }
        }

        private void TileBarcodeReassignment_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.m_BarcodeScanPort.ContainerScanReceived -= ContainerScanReceived;
            Receiving.BarcodeReassignmentPath assignBarcodePath = new Receiving.BarcodeReassignmentPath();
            assignBarcodePath.Start();
            this.m_BarcodeScanPort.ContainerScanReceived += ContainerScanReceived;
        }

        private void ComboBoxCaseType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string caseType = (string)this.ComboBoxCaseType.SelectedItem;
            this.m_LoginUI.CurrentCaseType = caseType;
            this.m_LoginUI.GetReportSearchList();
        }

        private void TileReport_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.User.SystemIdentity systemIdentity = Business.User.SystemIdentity.Instance;
                YellowstonePathology.Business.Search.ReportSearchItem reportSearchItem = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewAccessionOrders.SelectedItem;
                YellowstonePathology.UI.ReportOrder.ReportOrderDetailPage reportOrderDetailPage = new YellowstonePathology.UI.ReportOrder.ReportOrderDetailPage(this.m_LoginUI.AccessionOrder, reportSearchItem.ReportNo, systemIdentity);
                reportOrderDetailPage.ShowDialog();
            }
        }

        private void TileResult_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.ShowResultsPage();
        }

        private void ShowResultsPage()
        {

            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_LoginUI.AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_LoginUI.ReportNo);
                YellowstonePathology.Business.User.SystemIdentity systemIdentity = Business.User.SystemIdentity.Instance;

                YellowstonePathology.UI.Test.ResultPathFactory resultPathFactory = new Test.ResultPathFactory();
                resultPathFactory.Finished += new Test.ResultPathFactory.FinishedEventHandler(ResultPathFactory_Finished);

                this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
                bool started = resultPathFactory.Start(panelSetOrder, this.m_LoginUI.AccessionOrder, this.m_LoginPageWindow.PageNavigator, this.m_LoginPageWindow, System.Windows.Visibility.Collapsed);
                if (started == true)
                {
                    this.m_LoginPageWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("The result for this case is not available in this view.");
                }
            }
        }

        private void ResultPathFactory_Finished(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        private void TileCaseHistory_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                YellowstonePathology.UI.Common.CaseHistoryDialog caseHistoryDialog = new Common.CaseHistoryDialog(this.m_LoginUI.AccessionOrder);
                caseHistoryDialog.ShowDialog();
            }
        }

        private void ButtonReceiveSpecimen_Click(object sender, RoutedEventArgs e)
        {
            this.m_BarcodeScanPort.ContainerScanReceived -= ContainerScanReceived;
            Receiving.ReceiveSpecimenPath clientOrderReceivingPathWithSecurity = new Receiving.ReceiveSpecimenPath();
            clientOrderReceivingPathWithSecurity.Start();
            this.m_BarcodeScanPort.ContainerScanReceived += ContainerScanReceived;
        }

        private void TileGrossEntry_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                if (this.m_LoginUI.AccessionOrder.PanelSetOrderCollection.HasSurgical() == true)
                {
                    YellowstonePathology.UI.Login.FinalizeAccession.GrossEntryPage grossEntryPage = new FinalizeAccession.GrossEntryPage(this.m_LoginUI.AccessionOrder);
                    grossEntryPage.Next += new FinalizeAccession.GrossEntryPage.NextEventHandler(GrossEntryPage_Next);
                    grossEntryPage.Back += new FinalizeAccession.GrossEntryPage.BackEventHandler(GrossEntryPage_Back);

                    this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
                    this.m_LoginPageWindow.PageNavigator.Navigate(grossEntryPage);
                    this.m_LoginPageWindow.ShowDialog();
                }
            }
        }

        private void GrossEntryPage_Back(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        private void GrossEntryPage_Next(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }        

        private void TaskOrderPath_Close(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        private void TileTasks_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                Business.Search.ReportSearchItem reportSearchItem = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewAccessionOrders.SelectedItem;

                Business.Task.Model.TaskOrder taskOrder = this.m_LoginUI.AccessionOrder.TaskOrderCollection.GetTaskOrderByReportNo(reportSearchItem.ReportNo);

                if (taskOrder != null)
                {                    
                    this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();

                    UI.Login.Receiving.TaskOrderPath taskOrderPath = new Receiving.TaskOrderPath(this.m_LoginUI.AccessionOrder, taskOrder, this.m_LoginPageWindow.PageNavigator, PageNavigationModeEnum.Standalone);
                    taskOrderPath.Close += new Receiving.TaskOrderPath.CloseEventHandler(TaskOrderPath_Close);
                    taskOrderPath.Start();
                    this.m_LoginPageWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("There are no tasks for this case.");
                }
            }
        }

        private void TileSpecimenMapping_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.Search.ReportSearchItem reportSearchItem = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewAccessionOrders.SelectedItem;
                YellowstonePathology.UI.Login.FinalizeAccession.SpecimenMappingPage specimenMappingPage = new FinalizeAccession.SpecimenMappingPage(this.m_LoginUI.AccessionOrder);
                specimenMappingPage.Next += new FinalizeAccession.SpecimenMappingPage.NextEventHandler(SpecimenMappingPage_Next);
                specimenMappingPage.Back += new FinalizeAccession.SpecimenMappingPage.BackEventHandler(SpecimenMappingPage_Back);
                
                this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
                this.m_LoginPageWindow.Show();
                this.m_LoginPageWindow.PageNavigator.Navigate(specimenMappingPage);
            }
        }

        private void SpecimenMappingPage_Next(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        private void SpecimenMappingPage_Back(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        private void TileBilling_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                if (this.BringPageToFore != null)
                {
                    this.BringPageToFore(this, EventArgs.Empty);
                }
                else
                {
                    YellowstonePathology.Business.Search.ReportSearchItem reportSearchItem = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewAccessionOrders.SelectedItem;
                    this.m_LoginUI.ReportSearchList.SetCurrentReportSearchItem(reportSearchItem.ReportNo);
                    this.m_BillingPath = new Billing.BillingPath(this.m_LoginUI.ReportSearchList);
                    this.m_BillingPath.Finish += BillingPath_Finish;
                    this.BringPageToFore += this.m_BillingPath.BringPageToFore;
                    this.m_BillingPath.Start(this.m_LoginUI.AccessionOrder);                    
                }
            }
        }

        private void BillingPath_Finish(object sender, EventArgs e)
        {
            this.BringPageToFore -= this.m_BillingPath.BringPageToFore;            
        }

        private void TileICDCodes_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                
                ICDEntryPage icdEntryPage = new ICDEntryPage(this.m_LoginUI.AccessionOrder, this.m_LoginUI.ReportNo);
                icdEntryPage.Next += new ICDEntryPage.NextEventHandler(IcdEntryPage_Next);

                this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
                this.m_LoginPageWindow.PageNavigator.Navigate(icdEntryPage);
                this.m_LoginPageWindow.ShowDialog();
            }
        }

        private void MenuItemCancelTest_Click(object sender, RoutedEventArgs e)
        {                        
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                if(this.m_LoginUI.AccessionOrder.AccessionLock.IsLockAquiredByMe == true)
                {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to cancel this test?", "Cancel Test", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {

                        YellowstonePathology.Business.Search.ReportSearchItem reportSearchItem = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewAccessionOrders.SelectedItem;
                        YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_LoginUI.AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportSearchItem.ReportNo);

                        if (panelSetOrder.Final == false)
                        {
                            string reportNo = panelSetOrder.ReportNo;
                            string testName = panelSetOrder.PanelSetName;
                            int panelSetId = panelSetOrder.PanelSetId;

                            YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_LoginUI.AccessionOrder.SpecimenOrderCollection.GetOrderTarget(panelSetOrder.OrderedOnId);
                            Business.Test.TestCancelled.TestCancelledTest cancelledTest = new Business.Test.TestCancelled.TestCancelledTest();
                            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new Business.Test.TestOrderInfo(cancelledTest, orderTarget, true);
                            YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Business.Visitor.OrderTestOrderVisitor(testOrderInfo);

                            this.m_LoginUI.AccessionOrder.PanelSetOrderCollection.Remove(panelSetOrder);
                            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();

                            this.m_LoginUI.AccessionOrder.TakeATrip(orderTestOrderVisitor);
                            Business.Test.TestCancelled.TestCancelledTestOrder testCancelledTestOrder = (Business.Test.TestCancelled.TestCancelledTestOrder)orderTestOrderVisitor.PanelSetOrder;
                            testCancelledTestOrder.Comment = testName + " has been cancelled.";
                            testCancelledTestOrder.CancelledTestId = panelSetId;
                            testCancelledTestOrder.CancelledTestName = testName;

                            this.m_LoginUI.AccessionOrder.TaskOrderCollection.SetNewReportNoForCancelledTest(reportNo, testCancelledTestOrder.ReportNo);

                            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();

                            this.m_LoginUI.GetReportSearchListByReportNo(panelSetOrder.ReportNo);
                        }
                        else
                        {
                            MessageBox.Show("Cannot cancel a test that has been finalized.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Cannot cancel this test because the case is locked by someone else.");
                }
            }            
        }

        private void IcdEntryPage_Next(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        private void ButtonPrintList_Click(object sender, RoutedEventArgs e)
        {
            CaseListReport caseListReport = new CaseListReport(this.m_LoginUI.ReportSearchList);
            YellowstonePathology.UI.XpsDocumentViewer xpsDocumentViewer = new XpsDocumentViewer();
            xpsDocumentViewer.LoadDocument(caseListReport.FixedDocument);
            xpsDocumentViewer.ShowDialog();
        }

        private void MenuItemShowMasterLog_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Reports.Surgical.SurgicalMasterLog report = new YellowstonePathology.Business.Reports.Surgical.SurgicalMasterLog();
            report.CreateReport(this.m_LoginUI.AccessionOrderDate);
            report.OpenReport();
        }

        private void MenuItemShowNorthernMtMasterLog_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Reports.Surgical.SurgicalMasterLog report = new YellowstonePathology.Business.Reports.Surgical.SurgicalMasterLog();
            report.CreateNorthernMtTechOnlyReport(this.m_LoginUI.AccessionOrderDate);
            report.OpenReport();
        }

        private void TextBoxMasterAccessionNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (this.TextBoxMasterAccessionNo.Text.Length >= 1)
                {
                    Surgical.TextSearchHandler textSearchHandler = new Surgical.TextSearchHandler(this.TextBoxMasterAccessionNo.Text);
                    object textSearchObject = textSearchHandler.GetSearchObject();
                    if (textSearchObject is YellowstonePathology.Business.ReportNo)
                    {
                        YellowstonePathology.Business.ReportNo reportNo = (YellowstonePathology.Business.ReportNo)textSearchObject;
                        this.m_LoginUI.GetReportSearchListByReportNo(reportNo.Value);
                    }
                    else if (textSearchObject is YellowstonePathology.Business.MasterAccessionNo)
                    {
                        YellowstonePathology.Business.MasterAccessionNo masterAccessionNo = (YellowstonePathology.Business.MasterAccessionNo)textSearchObject;
                        this.m_LoginUI.GetReportSearchListByMasterAccessionNo(masterAccessionNo.Value);
                    }
                    else if (textSearchObject is YellowstonePathology.Business.PatientName)
                    {
                        YellowstonePathology.Business.PatientName patientName = (YellowstonePathology.Business.PatientName)textSearchObject;
                        this.m_LoginUI.GetReportSearchListByPatientName(patientName);
                    }

                    if (this.m_LoginUI.AccessionOrder != null)
                    {
                        YellowstonePathology.Business.Persistence.DocumentGateway.Instance.ReleaseLock(this.m_LoginUI.AccessionOrder, this.m_Writer);
                    }
                }
            }
        }

        private void TileSpecimenSelection_MouseUp(object sender, MouseButtonEventArgs e)
        {
            YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = Business.PanelSet.Model.PanelSetCollection.GetAll();
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_LoginUI.AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_LoginUI.ReportNo);
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(panelSetOrder.PanelSetId);
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new Business.Test.TestOrderInfo(panelSet, null, true);
            YellowstonePathology.UI.Login.Receiving.SpecimenSelectionPage specimenSelectionPage = new Receiving.SpecimenSelectionPage(this.m_LoginUI.AccessionOrder, testOrderInfo);
            
            this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
            this.m_LoginPageWindow.Show();
            specimenSelectionPage.TargetSelected += new Receiving.SpecimenSelectionPage.TargetSelectedEventHandler(SpecimenSelectionPage_TargetSelected);
            this.m_LoginPageWindow.PageNavigator.Navigate(specimenSelectionPage);
        }

        private void SpecimenSelectionPage_TargetSelected(object sender, CustomEventArgs.TestOrderInfoEventArgs e)
        {
            YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = Business.PanelSet.Model.PanelSetCollection.GetAll();
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_LoginUI.AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_LoginUI.ReportNo);
            
            panelSetOrder.OrderedOn = e.TestOrderInfo.OrderTarget.GetOrderedOnType();
            panelSetOrder.OrderedOnId = e.TestOrderInfo.OrderTarget.GetId();            
            this.m_LoginPageWindow.Close();
        }

        private void SlidePrintingPath_Done(object sender, EventArgs e)
        {
            this.m_BarcodeScanPort.ContainerScanReceived += ContainerScanReceived;
        }

        private void TileTracking_MouseUp(object sender, MouseButtonEventArgs e)
        {
            YellowstonePathology.UI.MaterialTracking.MaterialTrackingPath materialTrackingPath = new MaterialTracking.MaterialTrackingPath(this.m_LoginUI.AccessionOrder);
            materialTrackingPath.Start();
        }

        private void MenuItemShowITAudits_Click(object sender, RoutedEventArgs e)
        {
            this.m_LoginUI.GetReportSearchListByITAudit(Business.Test.ITAuditPriorityEnum.Medium);
        }

        private void MenuItemSendPantherOrder_Click(object sender, RoutedEventArgs e)
        {
            foreach(Business.Search.ReportSearchItem reportSearchItem in this.ListViewAccessionOrders.SelectedItems)
            {                
                Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(reportSearchItem.MasterAccessionNo, this);
                Business.Test.PanelSetOrder panelSetOrder = ao.PanelSetOrderCollection.GetPanelSetOrder(reportSearchItem.ReportNo);
                Business.HL7View.Panther.PantherAssay pantherAssay = Business.HL7View.Panther.PantherAssayFactory.GetPantherAssay(panelSetOrder);

                Business.Rules.MethodResult methodResult = pantherAssay.CanSendOrder(this.m_LoginUI.AccessionOrder, panelSetOrder);
                if (methodResult.Success == true)
                {
                    Business.Specimen.Model.SpecimenOrder specimenOrder = ao.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(panelSetOrder.OrderedOnId);
                    Business.HL7View.Panther.PantherOrder pantherOrder = new Business.HL7View.Panther.PantherOrder(pantherAssay, specimenOrder, ao, panelSetOrder, YellowstonePathology.Business.HL7View.Panther.PantherActionCode.NewSample);
                    pantherOrder.Send();
                }
                else
                {
                    MessageBox.Show(methodResult.Message);
                }
            }
        }

        private void MenuItemCancelPantherOrder_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                if (this.m_LoginUI.AccessionOrder.SpecimenOrderCollection.HasThinPrepFluidSpecimen() == true)
                {
                    throw new Exception("Cancelling a Panther order needs work.");
                    /*
                    YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_LoginUI.AccessionOrder.SpecimenOrderCollection.GetThinPrep();                    
                    YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_LoginUI.AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(14);
                    YellowstonePathology.Business.HL7View.Panther.PantherAssayHPV pantherAssayHPV = new Business.HL7View.Panther.PantherAssayHPV();
                    YellowstonePathology.Business.HL7View.Panther.PantherOrder pantherOrder = new Business.HL7View.Panther.PantherOrder(pantherAssayHPV, specimenOrder, aliquotOrder.AliquotOrderId, this.m_LoginUI.AccessionOrder, panelSetOrder, YellowstonePathology.Business.HL7View.Panther.PantherActionCode.CancelRequest);
                    pantherOrder.Send();
                    */
                }   
            }
        }

        private void HandleVantageSlideScan(string scanData)
        {
            this.m_BarcodeScanPort.VantageSlideScanReceived -= BarcodeScanPort_VantageSlideScanReceived;
            string masterAccessionNo = null;

            List<string> results = Business.Slide.Model.VantageSlide.GetBySlideId(scanData);
            foreach (string result in results)
            {
                Business.Slide.Model.VantageSlide vantageSlide = Business.Slide.Model.VantageSlide.FromJson(result);
                masterAccessionNo = vantageSlide.MasterAccessionNo;
                break;
            }

            this.m_LoginUI.GetReportSearchListByMasterAccessionNo(masterAccessionNo);
            this.ListViewAccessionOrders.SelectedIndex = 0;
            this.ShowResultsPage();
            this.m_BarcodeScanPort.VantageSlideScanReceived += BarcodeScanPort_VantageSlideScanReceived;
        }

        private void CytologyLabels_MouseUp(object sender, MouseButtonEventArgs e)
        {
            FinalizeAccession.PrintCytologyLabelsPage printCytologyLabelsPage = new FinalizeAccession.PrintCytologyLabelsPage(this.m_LoginUI.ReportNo, this.m_LoginUI.AccessionOrder);
            printCytologyLabelsPage.Back += new FinalizeAccession.PrintCytologyLabelsPage.BackEventHandler(PrintCytologyLabelsPage_Back);
            printCytologyLabelsPage.Finish += new FinalizeAccession.PrintCytologyLabelsPage.FinishEventHandler(PrintCytologyLabelsPage_Finish);
            this.m_LoginPageWindow = new Receiving.LoginPageWindow();
            this.m_LoginPageWindow.PageNavigator.Navigate(printCytologyLabelsPage);
            this.m_LoginPageWindow.Show();
        }

        private void PrintCytologyLabelsPage_Back(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
            this.m_LoginPageWindow = null;
        }

        private void PrintCytologyLabelsPage_Finish(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
            this.m_LoginPageWindow = null;
        }

        private void WriteGrossCameraArgsFile()
        {
            Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_LoginUI.AccessionOrder.MasterAccessionNo);
            string casePath = Business.Document.CaseDocumentPath.GetPath(orderIdParser);

            string[] lines = { this.m_LoginUI.AccessionOrder.MasterAccessionNo, casePath };
            System.IO.StreamWriter file = new System.IO.StreamWriter($@"\\ypiidc\YPIILIS\gross_camera_{Environment.MachineName}_args.txt");

            foreach (string line in lines)
            {
                file.WriteLine(line);
            }

            file.Close();
        }

        private void ButtonShowCamera_Click(object sender, RoutedEventArgs e)
        {
            if(this.m_LoginUI.AccessionOrder != null)
            {
                string fileName = @"c:\Program Files\Yellowstone Pathology Institute\Gross Camera\Video.exe";                
                if (System.IO.File.Exists(fileName) == true)
                {
                    this.WriteGrossCameraArgsFile();
                    Process process = new Process();
                    process.StartInfo.FileName = fileName;                    
                    process.Start();
                }
                else
                {
                    MessageBox.Show("The Gross Camera Software has not been isntalled.");
                }
            }    
            else
            {
                MessageBox.Show("Please select an Accession first.");
            }
        }

        private void ButtonShowCOVIDCases_Click(object sender, RoutedEventArgs e)
        {
            this.m_LoginUI.GetReportSearchListByCOVIDCases();
        }

        private void ButtonShowYPICOVIDCases_Click(object sender, RoutedEventArgs e)
        {
            this.m_LoginUI.GetReportSearchListByYPICOVIDCases();
        }

        private void ButtonShowSCLCOVIDCases_Click(object sender, RoutedEventArgs e)
        {
            this.m_LoginUI.GetReportSearchListBySCLCOVIDCases();
        }

        private void ButtonShowCOVIDPatientDistribution_Click(object sender, RoutedEventArgs e)
        {
            this.m_LoginUI.GetReportSearchListByCOVIDPatientDistributionCases();
        }

        private void ButtonShowDurdenCOVIDCases_Click(object sender, RoutedEventArgs e)
        {            
            this.m_LoginUI.GetReportSearchListByDurdenCOVIDCases();
        }        

        private void MenuItemITAudited_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListViewAccessionOrders.SelectedItem != null)
            {
                Business.Search.ReportSearchItem reportSearchItem = (Business.Search.ReportSearchItem)this.ListViewAccessionOrders.SelectedItem;
                foreach(Business.Search.ReportSearchItem rsi in this.m_LoginUI.ReportSearchList)
                {
                    if(rsi.ReportNo == reportSearchItem.ReportNo)
                    {
                        reportSearchItem.ITAudited = true;
                    }                    
                }                
                this.m_LoginUI.AccessionOrder.ITAudited = true;
            }            
        }

        private void ButtonSendProvationResult_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                Business.Search.ReportSearchItem reportSearchItem = (Business.Search.ReportSearchItem)this.ListViewAccessionOrders.SelectedItem;
                foreach (Business.Search.ReportSearchItem rsi in this.ListViewAccessionOrders.SelectedItems)
                {
                    Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(rsi.MasterAccessionNo, this);
                    Business.HL7View.EPIC.EPICBeakerResultView resultView = new Business.HL7View.EPIC.EPICBeakerResultView(rsi.ReportNo, ao, false, false);
                    Business.Rules.MethodResult result = new Business.Rules.MethodResult();
                    resultView.HandleSendToProvation(result);
                }
                MessageBox.Show("All done.");
            }
        }

        private void ButtonSendNMHResult_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewAccessionOrders.SelectedItem != null)
            {
                Business.Search.ReportSearchItem reportSearchItem = (Business.Search.ReportSearchItem)this.ListViewAccessionOrders.SelectedItem;
                foreach (Business.Search.ReportSearchItem rsi in this.ListViewAccessionOrders.SelectedItems)
                {
                    Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(rsi.MasterAccessionNo, this);
                    Business.HL7View.NMH.NMHResultView resultView = new Business.HL7View.NMH.NMHResultView(rsi.ReportNo, ao, false);
                    Business.Rules.MethodResult result = new Business.Rules.MethodResult();
                    resultView.Send(result);
                }
                MessageBox.Show("All done.");
            }
        }
    }
}
