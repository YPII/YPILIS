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
using System.ComponentModel;
using System.Xml;
using System.ServiceModel;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Cytology
{    
    public partial class CytologyWorkspace : UserControl
    {                
		public delegate void CytologyReportNoChanged();
        
		private UI.Cytology.CytologyResultsWorkspace m_CytologyResultsWorkspace;

        private CytologyUI m_CytologyUI;
        private YellowstonePathology.UI.DocumentWorkspace m_DocumentViewer;

		private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

        private YellowstonePathology.UI.LabEventsControlTab m_LabEventsControlTab;
        private MainWindowCommandButtonHandler m_MainWindowCommandButtonHandler;
        private TabItem m_Writer;

        public CytologyWorkspace(MainWindowCommandButtonHandler mainWindowCommandButtonHandler, TabItem writer)
        {
            this.m_MainWindowCommandButtonHandler = mainWindowCommandButtonHandler;
            this.m_SystemIdentity = YellowstonePathology.Business.User.SystemIdentity.Instance;
            this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;

            this.m_Writer = writer;
            this.m_CytologyUI = new CytologyUI(this.m_Writer);
			this.m_CytologyUI.AccessionChanged += new CytologyUI.AccessionChangedEventHandler(CytologyUI_AccessionChanged);
            
			this.m_CytologyResultsWorkspace = new CytologyResultsWorkspace(this.m_CytologyUI);
            this.m_CytologyUI.WHPOpened += CytologyUI_WHPOpened;
            this.m_CytologyUI.WHPClosed += CytologyUI_WHPClosed;

            this.m_DocumentViewer = new DocumentWorkspace();
            
            InitializeComponent();

            this.DataContext = this.m_CytologyUI;
            this.ContentControlDocumentViewer.Content = this.m_DocumentViewer;
			this.m_BarcodeScanPort.CytologySlideScanReceived += new YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.CytologySlideScanReceivedHandler(CytologySlideScanReceived);
			this.m_BarcodeScanPort.ThinPrepSlideScanReceived += new Business.BarcodeScanning.BarcodeScanPort.ThinPrepSlideScanReceivedHandler(BarcodeScanPort_ThinPrepSlideScanReceived);

            this.Loaded += new RoutedEventHandler(CytologyWorkspace_Loaded);
            this.Unloaded += new RoutedEventHandler(CytologyWorkspace_Unloaded);

            this.ResultsGrid.Children.Add(this.m_CytologyResultsWorkspace);
            this.m_LabEventsControlTab = new LabEventsControlTab(this.m_SystemIdentity);
            this.TabControlLeft.Items.Add(this.m_LabEventsControlTab);
        }

        private void CytologyWorkspace_Loaded(object sender, RoutedEventArgs e)
        {
            this.ComboBoxSearchType.SelectedIndex = 0;
            this.ComboBoxScreenerSelection.SelectedIndex = this.m_CytologyUI.GetScreenerIndex();

            this.m_MainWindowCommandButtonHandler.StartProviderDistributionPath += MainWindowCommandButtonHandler_StartProviderDistributionPath;
            this.m_MainWindowCommandButtonHandler.Save += MainWindowCommandButtonHandler_Save;
            this.m_MainWindowCommandButtonHandler.ShowAmendmentDialog += this.m_CytologyResultsWorkspace.CytologyUI.ShowAmendmentDialog;
            this.m_MainWindowCommandButtonHandler.RemoveTab += MainWindowCommandButtonHandler_RemoveTab;
            this.m_MainWindowCommandButtonHandler.ShowMessagingDialog += MainWindowCommandButtonHandler_ShowMessagingDialog;
            this.m_MainWindowCommandButtonHandler.ShowCaseDocument += MainWindowCommandButtonHandler_ShowCaseDocument;

            this.ListViewSearchResults.SelectedIndex = -1;

            Keyboard.Focus(this.m_CytologyResultsWorkspace.TextBoxReportNoSearch);
        }

        private void CytologyWorkspace_Unloaded(object sender, RoutedEventArgs e)
        {
            this.m_MainWindowCommandButtonHandler.StartProviderDistributionPath -= MainWindowCommandButtonHandler_StartProviderDistributionPath;
            this.m_MainWindowCommandButtonHandler.Save -= MainWindowCommandButtonHandler_Save;
            this.m_MainWindowCommandButtonHandler.ShowAmendmentDialog -= this.m_CytologyResultsWorkspace.CytologyUI.ShowAmendmentDialog;
            this.m_MainWindowCommandButtonHandler.RemoveTab -= MainWindowCommandButtonHandler_RemoveTab;
            this.m_MainWindowCommandButtonHandler.ShowMessagingDialog -= MainWindowCommandButtonHandler_ShowMessagingDialog;
            this.m_MainWindowCommandButtonHandler.ShowCaseDocument -= MainWindowCommandButtonHandler_ShowCaseDocument;

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
        }

        private void BarcodeScanPort_ThinPrepSlideScanReceived(Business.BarcodeScanning.Barcode barcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate()
                    {
                        YellowstonePathology.UI.MainWindow mainWindow = Application.Current.MainWindow as YellowstonePathology.UI.MainWindow;
                        if (((TabItem)mainWindow.TabControlLeftWorkspace.SelectedItem).Tag.ToString() == "Cytology")
                        {
                            if (barcode.IsValidated == true)
                            {                                
                                if (this.m_CytologyUI.SetAccessionOrderByAliquotOrderId(barcode.ID))
                                {
									this.m_CytologyResultsWorkspace.ReportNo = this.m_CytologyUI.PanelSetOrderCytology.ReportNo;
                                }
                            }
                            else
                            {
                                System.Windows.MessageBox.Show("The scanner did not read the label correctly.", "Scan not successful.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            }
                        }
                    }));     
        }        

        private void MainWindowCommandButtonHandler_ShowCaseDocument(object sender, EventArgs e)
        {
            this.m_CytologyUI.ShowCaseDocument();
        }

        private void MainWindowCommandButtonHandler_Save(object sender, EventArgs e)
        {
            this.ReleaseLock();
        }

        private void MainWindowCommandButtonHandler_StartProviderDistributionPath(object sender, EventArgs e)
        {
            if (this.m_CytologyUI.AccessionOrder != null)
            {
                YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath providerDistributionPath =
					new YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath(this.m_CytologyUI.PanelSetOrderCytology.ReportNo, this.m_CytologyUI.AccessionOrder,
                    System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed);
                providerDistributionPath.Start();
            }
        }

        private void MainWindowCommandButtonHandler_ShowMessagingDialog(object sender, EventArgs e)
        {
            if (this.m_CytologyUI.AccessionOrder != null)
            {                
                UI.AppMessaging.MessagingPath.Instance.Start(this.m_CytologyUI.AccessionOrder);
            }
        }

        private void MainWindowCommandButtonHandler_RemoveTab(object sender, EventArgs e)
        {
            Business.Persistence.DocumentGateway.Instance.Push(this.m_Writer);
        }

        private void MessageQueue_AquireLock(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate ()
                    {
                        string masterAccessionNo = (string)sender;
                        if (this.m_CytologyUI.AccessionOrder != null && this.m_CytologyUI.AccessionOrder.MasterAccessionNo == masterAccessionNo)
                        {
                            this.m_CytologyUI.LoadDataByReportNo(this.m_CytologyUI.PanelSetOrderCytology.ReportNo);
                        }
                    }));            
        }

        private void MessageQueue_ReleaseLock(object sender, EventArgs e)
        {
            string masterAccessionNo = (string)sender;
            if (this.m_CytologyUI.AccessionOrder != null && this.m_CytologyUI.AccessionOrder.MasterAccessionNo == masterAccessionNo)
            {
                this.ReleaseLock();
            }
        }

        private void MessageQueue_RequestReceived(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                //AppMessaging.MessagingPath.Instance.StartRequestReceived(e.Message);
            }
            ));
        }

        private void CytologySlideScanReceived(YellowstonePathology.Business.BarcodeScanning.CytycBarcode cytycBarcode)
        {            
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate()
                    {
                        YellowstonePathology.UI.MainWindow mainWindow = Application.Current.MainWindow as YellowstonePathology.UI.MainWindow;
                        if (((TabItem)mainWindow.TabControlLeftWorkspace.SelectedItem).Tag.ToString() == "Cytology")
                        {

                            if (cytycBarcode.IsValidated == true)
							{
                                if (this.m_CytologyUI.SetAccessionOrderByReportNo(cytycBarcode.ReportNo))
								{
                                    this.m_CytologyResultsWorkspace.ReportNo = cytycBarcode.ReportNo;									
								}
							}
							else
							{
								System.Windows.MessageBox.Show("The scanner did not read the label correctly.", "Scan not successful.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
							}
                        }
                    }));            
        }
        
        private void ReleaseLock()
        {
            if (this.m_CytologyUI.AccessionOrder != null)
            {
                MainWindow.MoveKeyboardFocusNextThenBack();
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.ReleaseLock(this.m_CytologyUI.AccessionOrder, this.m_Writer);

                if (this.m_CytologyUI.AccessionOrder.AccessionLock.IsLockAquiredByMe == false)
                {
                    this.m_CytologyUI.NotifyPropertyChanged(string.Empty);
                }
            }
        }

        private void CytologyUI_AccessionChanged(object sender, EventArgs e)
		{
			this.m_CytologyResultsWorkspace.TextBoxReportNoSearch.Text = this.m_CytologyUI.PanelSetOrderCytology.ReportNo;
			YellowstonePathology.Business.Document.CaseDocumentCollection caseDocumentCollection = new Business.Document.CaseDocumentCollection(this.m_CytologyUI.AccessionOrder, this.m_CytologyUI.PanelSetOrderCytology.ReportNo);
			this.m_DocumentViewer.ShowDocument(caseDocumentCollection.GetFirstRequisition());			
			this.m_LabEventsControlTab.SetCurrentOrder(this.m_CytologyUI.AccessionOrder);
            this.m_CytologyResultsWorkspace.SelectAppropriatePanel();
		}

		public void MoveFocus(object target, ExecutedRoutedEventArgs args)
        {
            IInputElement focusedElement = Keyboard.FocusedElement;
            FrameworkElement element = (FrameworkElement)focusedElement;
            element.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));                  
        }       

        public void StartProviderDistributionPath(object target, ExecutedRoutedEventArgs args)
        {
            string reportNo = this.m_CytologyUI.PanelSetOrderCytology.ReportNo;
            YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath providerDistributionPath = new Login.FinalizeAccession.ProviderDistributionPath(reportNo, this.m_CytologyUI.AccessionOrder,
                System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed);
            providerDistributionPath.Start();
        }

        public UI.Cytology.CytologyResultsWorkspace CytologyResultsWorkspace
		{
			get { return this.m_CytologyResultsWorkspace; }
		}

        private void ListViewSearchResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			DateTime hit = DateTime.Now;
            if (this.ListViewSearchResults.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.Search.CytologyScreeningSearchResult selectedSearchResult = (YellowstonePathology.Business.Search.CytologyScreeningSearchResult)this.ListViewSearchResults.SelectedItem;                
                this.m_CytologyUI.SetAccessionOrder(selectedSearchResult);				
			}			
        }
                
		private void ButtonGetList_Click(object sender, RoutedEventArgs e)
		{
            if (this.ComboBoxSearchType.SelectedItem != null)
            {
                string selectedSearch = (string)this.ComboBoxSearchType.SelectedItem;
				int screenedById = 0;
				if (this.ComboBoxScreenerSelection.SelectedItem != null)
				{
					screenedById = (int)this.ComboBoxScreenerSelection.SelectedValue;
				}
                this.m_CytologyUI.DoListSearch(selectedSearch, screenedById);

                this.TextBlockCount.Text = "Count: " + this.m_CytologyUI.Search.Results.Count.ToString();                
            }
        }		

        private void ButtonAssignTo_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.Business.User.SystemUser systemUser = (YellowstonePathology.Business.User.SystemUser)this.ComboBoxAssignedToSelection.SelectedItem;
			System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show("Assign selected cases to " + systemUser.DisplayName + "?", "Assign selected cases?", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                List<YellowstonePathology.Business.Search.CytologyScreeningSearchResult> cytologyScreeningSearchResult = new List<YellowstonePathology.Business.Search.CytologyScreeningSearchResult>();
                foreach (YellowstonePathology.Business.Search.CytologyScreeningSearchResult listItem in this.ListViewSearchResults.SelectedItems)
                {
                    cytologyScreeningSearchResult.Add(listItem);
                }
                if (cytologyScreeningSearchResult.Count > 0)
                {
                    this.m_CytologyUI.AssignScreenings(cytologyScreeningSearchResult, systemUser);
                }
                else
                {
                    System.Windows.MessageBox.Show("No items selected to assign.");
                }
            }
        }

        private void ButtonPrintList_Click(object sender, RoutedEventArgs e)
        {
            XElement listElement = null;

            if (this.ListViewSearchResults.SelectedItems.Count != 0)
            {
                listElement = new XElement("CytologyScreeningList");
                foreach (YellowstonePathology.Business.Search.CytologyScreeningSearchResult item in this.ListViewSearchResults.SelectedItems)
                {
                    item.ToXml(listElement);
                }
            }
            else if(this.m_CytologyUI.Search.Results.Count != 0)
            {
                listElement = new XElement("CytologyScreeningList");
                foreach (YellowstonePathology.Business.Search.CytologyScreeningSearchResult item in this.m_CytologyUI.Search.Results)
                {
                    item.ToXml(listElement);
                }
            }

            if (listElement != null)
            {
                YellowstonePathology.Business.XPSDocument.Result.Data.CytologyScreeningListReportData cytologyScreeningListReportData = new Business.XPSDocument.Result.Data.CytologyScreeningListReportData(listElement);
                YellowstonePathology.Business.XPSDocument.Result.Xps.CytologyScreeningListReport clientSupplyOrderReport = new Business.XPSDocument.Result.Xps.CytologyScreeningListReport(cytologyScreeningListReportData);
                XpsDocumentViewer xpsDocumentViewer = new XpsDocumentViewer();
                xpsDocumentViewer.LoadDocument(clientSupplyOrderReport.FixedDocument);
                xpsDocumentViewer.ShowDialog();
            }
            else
            {
                MessageBox.Show("Fill the list or select list entries", "Nothing to report.");
            }
        }       

        private void CytologyUI_WHPOpened(object sender, EventArgs e)
        {
            this.m_BarcodeScanPort.CytologySlideScanReceived -= CytologySlideScanReceived;
            this.m_BarcodeScanPort.ThinPrepSlideScanReceived -= BarcodeScanPort_ThinPrepSlideScanReceived;
        }

        private void CytologyUI_WHPClosed(object sender, EventArgs e)
        {
            this.m_BarcodeScanPort.CytologySlideScanReceived += new YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.CytologySlideScanReceivedHandler(CytologySlideScanReceived);
            this.m_BarcodeScanPort.ThinPrepSlideScanReceived += new Business.BarcodeScanning.BarcodeScanPort.ThinPrepSlideScanReceivedHandler(BarcodeScanPort_ThinPrepSlideScanReceived);
        }
    }
}
