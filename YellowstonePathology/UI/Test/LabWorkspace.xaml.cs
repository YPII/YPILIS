using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Windows.Controls;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Test
{

    public partial class LabWorkspace : System.Windows.Controls.UserControl
    {        
        private YellowstonePathology.UI.AmendmentControlV2 m_AmendmentControl;
        private YellowstonePathology.UI.DocumentWorkspace m_DocumentViewer;
		private YellowstonePathology.UI.Common.TreeViewWorkspace m_TreeViewWorkspace;        
		
        private LabUI m_LabUI;

        private DateTime m_WorkDate = DateTime.Today;
		private YellowstonePathology.Business.Domain.Core.Comment m_Comment;

		YellowstonePathology.Business.Logging.ScanLogger m_ScanLogger;

		YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;

        private const string m_SystemIdentityUnknowMessage = "The current operation cannot be performed because the current system user is unknown.";
        private YellowstonePathology.UI.Test.ResultDialog m_ResultDialog;
        private MainWindowCommandButtonHandler m_MainWindowCommandButtonHandler;
        private TabItem m_Writer;

        public LabWorkspace(MainWindowCommandButtonHandler mainWindowCommandButtonHandler, TabItem writer)
        {
            this.m_MainWindowCommandButtonHandler = mainWindowCommandButtonHandler;
            this.m_Writer = writer;
            this.m_SystemIdentity = YellowstonePathology.Business.User.SystemIdentity.Instance;
						
			this.m_LabUI = new LabUI(this.m_SystemIdentity, writer);

			this.m_AmendmentControl = new AmendmentControlV2(this.m_SystemIdentity, string.Empty, this.m_LabUI.AccessionOrder);
            this.m_DocumentViewer = new DocumentWorkspace();
			this.m_TreeViewWorkspace = new YellowstonePathology.UI.Common.TreeViewWorkspace(this.m_LabUI.AccessionOrder, this.m_SystemIdentity);
            
            InitializeComponent();
            
			this.TabItemDocumentWorkspace.Content = this.m_DocumentViewer;

			this.DataContext = this.m_LabUI;

			this.ComboBoxLogLocation.SelectionChanged -= this.ComboBoxLogLocation_SelectionChanged;
			this.ComboBoxLogLocation.SelectedIndex = 0;
			this.ComboBoxLogLocation.SelectionChanged += this.ComboBoxLogLocation_SelectionChanged;			

            this.Loaded +=new RoutedEventHandler(LabWorkspace_Loaded);
            this.Unloaded += new RoutedEventHandler(LabWorkspace_Unloaded);

			this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;

			this.m_ScanLogger = new YellowstonePathology.Business.Logging.ScanLogger(this.m_SystemIdentity);
            this.m_ScanLogger.Start();
			this.ListViewDocumentList.ItemsSource = this.m_LabUI.CaseDocumentCollection;
        }

        private void LabWorkspace_Loaded(object sender, RoutedEventArgs args)
        {
            this.m_BarcodeScanPort.ClientScanReceived += ClientScanReceived;
            this.m_MainWindowCommandButtonHandler.StartProviderDistributionPath += MainWindowCommandButtonHandler_StartProviderDistributionPath;
            this.m_MainWindowCommandButtonHandler.Save += MainWindowCommandButtonHandler_Save;
            this.m_MainWindowCommandButtonHandler.RemoveTab += MainWindowCommandButtonHandler_RemoveTab;
            this.m_MainWindowCommandButtonHandler.ShowCaseDocument += MainWindowCommandButtonHandler_ShowCaseDocument;
            this.m_MainWindowCommandButtonHandler.ShowOrderForm += MainWindowCommandButtonHandler_ShowOrderForm;

            UI.AppMessaging.MessagingPath.Instance.LockReleasedActionList.Add(this.Save);
            UI.AppMessaging.MessagingPath.Instance.LockAquiredActionList.Add(this.m_LabUI.RunWorkspaceEnableRules);

            this.TabItemCaseList.Focus();
        }

        private void MainWindowCommandButtonHandler_ShowOrderForm(object sender, EventArgs e)
        {
            this.ShowOrderForm();
        }

        private void MainWindowCommandButtonHandler_ShowCaseDocument(object sender, EventArgs e)
        {
            this.ShowCaseDocument();
        }

        private void MainWindowCommandButtonHandler_Save(object sender, EventArgs e)
        {
            this.Save();
        }

        private void MainWindowCommandButtonHandler_RemoveTab(object sender, EventArgs e)
        {
            Business.Persistence.DocumentGateway.Instance.Push(this.m_Writer);
        }

        private void Save()
        {
            if (this.m_LabUI.AccessionOrder != null)
            {
                MainWindow.MoveKeyboardFocusNextThenBack();
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.ReleaseLock(this.m_LabUI.AccessionOrder, this.m_Writer);
                if (this.m_LabUI.AccessionOrder.AccessionLock.IsLockAquiredByMe == false)
                {
                    this.m_LabUI.RunWorkspaceEnableRules();
                    this.m_LabUI.NotifyPropertyChanged(string.Empty);
                }

                this.m_LabUI.CaseList.SetLockIsAquiredByMe(this.m_LabUI.AccessionOrder);
            }
        }

        private void MainWindowCommandButtonHandler_StartProviderDistributionPath(object sender, EventArgs e)
        {
            if (this.m_LabUI.AccessionOrder != null)
            {
                YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath providerDistributionPath =
                    new YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath(this.m_LabUI.PanelSetOrder.ReportNo, this.m_LabUI.AccessionOrder,
                    System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed);
                providerDistributionPath.Start();
            }
        }

        private void MessageQueue_RequestReceived(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                //AppMessaging.MessagingPath messagingPath = new AppMessaging.MessagingPath();
                //messagingPath.Start(e.Message);
            }
            ));
        }

        private void MainWindowCommandButtonHandler_ShowMessagingDialog(object sender, EventArgs e)
        {
            if (this.m_LabUI.AccessionOrder != null && this.m_LabUI.AccessionOrder.AccessionLock.IsLockAquiredByMe == false && this.m_LabUI.AccessionOrder.AccessionLock.IsLockAquired == true)
            {
                UI.AppMessaging.MessagingPath.Instance.Start(this.m_LabUI.AccessionOrder);
            }
        }

        private void MessageQueue_AquireLock(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                string masterAccessionNo = (string)sender;
                if (this.m_LabUI.AccessionOrder != null && this.m_LabUI.AccessionOrder.MasterAccessionNo == masterAccessionNo)
                {
                    this.GetAccessionOrder();
                    this.m_LabUI.CaseList.SetLockIsAquiredByMe(this.m_LabUI.AccessionOrder);
                }
            }
            ));            
        }

        private void MessageQueue_ReleaseLock(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                string masterAccessionNo = (string)sender;
                if (this.m_LabUI.AccessionOrder != null && this.m_LabUI.AccessionOrder.MasterAccessionNo == masterAccessionNo)
                {
                    MainWindow.MoveKeyboardFocusNextThenBack();
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.ReleaseLock(this.m_LabUI.AccessionOrder, this.m_Writer);
                    if (this.m_LabUI.AccessionOrder.AccessionLock.IsLockAquiredByMe == false)
                    {
                        this.m_LabUI.RunWorkspaceEnableRules();
                        this.m_LabUI.NotifyPropertyChanged(string.Empty);
                    }

                    this.m_LabUI.CaseList.SetLockIsAquiredByMe(this.m_LabUI.AccessionOrder);
                }
            }
            ));            
        }

        public void LabWorkspace_Unloaded(object sender, RoutedEventArgs e)
        {
			this.m_BarcodeScanPort.ClientScanReceived -= this.ClientScanReceived;
            this.m_MainWindowCommandButtonHandler.StartProviderDistributionPath -= MainWindowCommandButtonHandler_StartProviderDistributionPath;
            this.m_MainWindowCommandButtonHandler.Save -= MainWindowCommandButtonHandler_Save;
            this.m_MainWindowCommandButtonHandler.RemoveTab -= MainWindowCommandButtonHandler_RemoveTab;
            this.m_MainWindowCommandButtonHandler.ShowCaseDocument -= MainWindowCommandButtonHandler_ShowCaseDocument;
            this.m_MainWindowCommandButtonHandler.ShowOrderForm -= MainWindowCommandButtonHandler_ShowOrderForm;

            UI.AppMessaging.MessagingPath.Instance.LockReleasedActionList.Remove(this.Save);
            UI.AppMessaging.MessagingPath.Instance.LockAquiredActionList.Remove(this.m_LabUI.RunWorkspaceEnableRules);

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
        }

        private void ShowOrderForm()
        {
            if (HaveAvailableItem() == true)
            {
                YellowstonePathology.UI.Common.OrderDialog frm = new YellowstonePathology.UI.Common.OrderDialog(this.m_LabUI.AccessionOrder, this.m_LabUI.PanelSetOrder);
                frm.ShowDialog();
                this.GetAccessionOrder();
            }			
		}

		private void ButtonOrder_Click(object sender, RoutedEventArgs e)
		{
            if (this.m_LabUI.PanelSetOrder.PanelSetId != 31)
            {
                if (this.m_SystemIdentity.IsKnown == true)
                {
					YellowstonePathology.UI.Common.OrderDialog dlg = new YellowstonePathology.UI.Common.OrderDialog(this.m_LabUI.AccessionOrder, this.m_LabUI.PanelSetOrder);
                    dlg.ShowDialog();
                    this.GetAccessionOrder();                    
                }
            }
            else
            {
                MessageBox.Show("Technical only orders cannot be placed in this window.");
            }
		}

        private void ItemIsSelected(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = false;
			if (((TabItem)this.Parent).IsSelected && this.ListViewCaseList.SelectedItem != null && this.m_LabUI.AccessionOrder.AccessionLock.IsLockAquiredByMe == true && this.m_SystemIdentity.User.UserName != null)
			{
				e.CanExecute = true;
			}
		}

        private bool HaveAvailableItem()
        {
            bool result = false;
            if (this.ListViewCaseList.SelectedItem != null && this.m_LabUI.AccessionOrder.AccessionLock.IsLockAquiredByMe == true)
            {
                result = true;
            }
            return result;
        }

        private void LinkPatient()
		{
            if (HaveAvailableItem() == true)
            {
                YellowstonePathology.Business.Patient.Model.PatientLinker patientLinker = new Business.Patient.Model.PatientLinker(this.m_LabUI.AccessionOrder.MasterAccessionNo,
                    this.m_LabUI.PanelSetOrder.ReportNo,
                    this.m_LabUI.AccessionOrder.PFirstName,
                    this.m_LabUI.AccessionOrder.PLastName,
                    this.m_LabUI.AccessionOrder.PMiddleInitial,
                    this.m_LabUI.AccessionOrder.PSSN,
                    this.m_LabUI.AccessionOrder.PatientId, this.m_LabUI.AccessionOrder.PBirthdate);
                if (patientLinker.IsOkToLink.IsValid == true)
                {
                    YellowstonePathology.UI.Common.PatientLinkingDialog patientLinkingDialog = new Common.PatientLinkingDialog(this.m_LabUI.AccessionOrder, Business.Patient.Model.PatientLinkingListModeEnum.AccessionOrder, patientLinker);
                    patientLinkingDialog.ShowDialog();
                }
                else
                {
                    MessageBox.Show(patientLinker.IsOkToLink.Message, "Missing Information");
                }
            }
        }

        public void StartProviderDistributionPath(object target, ExecutedRoutedEventArgs args)
        {
            if (this.m_LabUI.PanelSetOrder != null)
            {
                YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath providerDistributionPath = new Login.FinalizeAccession.ProviderDistributionPath(this.m_LabUI.PanelSetOrder.ReportNo, this.m_LabUI.AccessionOrder,
                    System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed);
                providerDistributionPath.Start();
            }
        }		

        public void  ShowCaseDocument()
        {
            if(this.ListViewCaseList.SelectedItem != null)
            {
                YellowstonePathology.UI.CaseDocumentViewer caseDocumentViewer = new CaseDocumentViewer();
                caseDocumentViewer.View(this.m_LabUI.AccessionOrder, this.m_LabUI.PanelSetOrder);
            }                        
        }		

		private void CheckEnabled()
		{
			if (this.m_LabUI != null)
			{
				this.m_LabUI.RunWorkspaceEnableRules();
			}
		}

		private void GetAccessionOrder()
		{
			this.m_LabUI.GetAccessionOrder(this.m_LabUI.PanelSetOrder.MasterAccessionNo, this.m_LabUI.PanelSetOrder.ReportNo);
			this.RefreshWorkspaces();
		}

		public void GetCase(string masterAccessionNo, string reportNo)
        {
			this.m_LabUI.GetAccessionOrder(masterAccessionNo, reportNo);			

            this.m_DocumentViewer.ClearContent();
			if (this.m_LabUI.CaseDocumentCollection.GetFirstRequisition() != null)
			{
				this.m_DocumentViewer.ShowDocument(this.m_LabUI.CaseDocumentCollection.GetFirstRequisition());
			}
					
			this.ListViewDocumentList.ItemsSource = this.m_LabUI.CaseDocumentCollection;
			this.RefreshWorkspaces();			
		}

		public void ListViewCaseList_SelectionChanged(object sender, RoutedEventArgs args)
        {
            if (this.ListViewCaseList.SelectedItem != null)
            {
				YellowstonePathology.Business.Search.ReportSearchItem item = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewCaseList.SelectedItem;
				this.GetCase(item.MasterAccessionNo, item.ReportNo);

                this.m_LabUI.CaseList.SetLockIsAquiredByMe(this.m_LabUI.AccessionOrder);
            }
        }

		private void RefreshWorkspaces()
		{
			this.m_TreeViewWorkspace = new Common.TreeViewWorkspace(this.m_LabUI.AccessionOrder, this.m_SystemIdentity);
            this.m_TreeViewWorkspace.IsEnabled = this.m_LabUI.AccessionOrder.AccessionLock.IsLockAquiredByMe;
			this.TabItemTreeView.Content = this.m_TreeViewWorkspace;

			this.m_AmendmentControl = new AmendmentControlV2(this.m_SystemIdentity, this.m_LabUI.PanelSetOrder.ReportNo, this.m_LabUI.AccessionOrder);
            this.m_AmendmentControl.IsEnabled = this.m_LabUI.AccessionOrder.AccessionLock.IsLockAquiredByMe;
            this.TabItemAmendment.Content = this.m_AmendmentControl;			
		}

        public void ButtonGo_Click(object sender, RoutedEventArgs args)
        {
            this.m_LabUI.FillCaseList();
            SetListViewToTop();
        }

        public void MenuItemAcceptResults_Click(object sender, RoutedEventArgs args)
        {
            Control menuItem = (Control)sender;
            YellowstonePathology.Business.Test.PanelOrder panelOrder = (YellowstonePathology.Business.Test.PanelOrder)menuItem.Tag;
            YellowstonePathology.Business.Rules.RuleExecutionStatus ruleExecutionStatus = new YellowstonePathology.Business.Rules.RuleExecutionStatus();
			panelOrder.AcceptResults(ruleExecutionStatus, this.m_LabUI.AccessionOrder, this.m_SystemIdentity.User);

            if (ruleExecutionStatus.ExecutionHalted == true)
            {
                YellowstonePathology.UI.RuleExecutionStatusDialog ruleExecutionStatusDialog = new RuleExecutionStatusDialog(ruleExecutionStatus);
                ruleExecutionStatusDialog.ShowDialog();
            }

			this.m_LabUI.GetAccessionOrder(this.m_LabUI.PanelSetOrder.MasterAccessionNo, this.m_LabUI.PanelSetOrder.ReportNo);
		}

		public void MenuItemUnacceptResults_Click(object sender, RoutedEventArgs args)
		{
			//TODO: put this in a rule.  It is here until #ifusedatasets is retired
			if (this.m_LabUI.PanelSetOrder.Final)
			{
				MessageBoxResult result = MessageBox.Show("The case is finaled, do you still want to unaccept the results?", "Case is Finaled", MessageBoxButton.YesNo, MessageBoxImage.Question);
				if (result == MessageBoxResult.No) return;
			}

			Control menuItem = (Control)sender;
			string panelOrderItemId = ((YellowstonePathology.Business.Test.PanelOrder)menuItem.Tag).PanelOrderId;
			YellowstonePathology.Business.Test.PanelOrder panelOrder = (from pso in this.m_LabUI.AccessionOrder.PanelSetOrderCollection
																				from po in pso.PanelOrderCollection
																				where po.PanelOrderId == panelOrderItemId
																				select po).Single<YellowstonePathology.Business.Test.PanelOrder>();

			if (panelOrder.Accepted)
			{
				panelOrder.UnacceptResults();
				this.GetCase(this.m_LabUI.PanelSetOrder.MasterAccessionNo, this.m_LabUI.PanelSetOrder.ReportNo);
			}
		}
				
        public void MenuItemMarkAsUnassigned_Click(object sender, RoutedEventArgs args)
        {
            Control menuItem = (Control)sender;
			YellowstonePathology.Business.Test.PanelOrder panelOrder = (from pso in this.m_LabUI.AccessionOrder.PanelSetOrderCollection
																				from po in pso.PanelOrderCollection
																				where po.PanelOrderId == ((YellowstonePathology.Business.Test.PanelOrder)menuItem.Tag).PanelOrderId
																				select po).Single<YellowstonePathology.Business.Test.PanelOrder>();
			panelOrder.MarkAsUnassigned();
			this.GetAccessionOrder();
		}        

		public void ListViewDocumentList_MouseLeftButtonUp(object sender, RoutedEventArgs args)
		{
			if (this.ListViewDocumentList.SelectedItem != null)
			{
				if (this.m_DocumentViewer.SyncDocuments == true)
				{
					this.RightTabControl.SelectedIndex = 1;
				}
				YellowstonePathology.Business.Document.CaseDocument item = (YellowstonePathology.Business.Document.CaseDocument)this.ListViewDocumentList.SelectedItem;
				this.m_DocumentViewer.ShowDocument(item);
			}
		}		

		public void MenuItemRefreshDocumentList_Click(object sender, RoutedEventArgs args)
		{
			this.m_LabUI.RefreshCaseDocumentCollection();
			this.ListViewDocumentList.ItemsSource = this.m_LabUI.CaseDocumentCollection;
		}        

        public void ButtonSearch_Click(object sender, RoutedEventArgs args)
        {            
			this.DoSearch();
		}

		private void Search_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				this.DoSearch();
			}
		}

		private void DoSearch()
		{
            if (radioButtonSearchAccessionNo.IsChecked == true)
            {
                if (this.textBoxSearchReportNo.Text.Length > 4)
                {
					this.m_LabUI.SearchEngine.SetFillByReportNo(this.textBoxSearchReportNo.Text);
					this.m_LabUI.FillCaseList();

                    this.ListViewCaseList.Focus();
					SetListViewToTop();
                }
				else
				{
					MessageBox.Show("This search option requires an Accession Number.",	"Enter an accession number", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
			}
            if (radioButtonSearchNotDistributed.IsChecked == true)
            {
				this.m_LabUI.SearchEngine.SetFillByNotDistributed(0);
				this.m_LabUI.FillCaseList();
				this.ListViewCaseList.Focus();
				SetListViewToTop();				
            }
			else if (radioButtonSearchNotFinal.IsChecked.Value)
			{				
				this.m_LabUI.SearchEngine.SetFillByNotFinal(0);
				this.m_LabUI.FillCaseList();
				this.ListViewCaseList.Focus();
				SetListViewToTop();				
			}
            else if (radioButtonSearchInHouseMolecularNotFinal.IsChecked.Value)
            {
                this.m_LabUI.SearchEngine.SetFillByInHouseMolecularPending();
                this.m_LabUI.FillCaseList();
                this.ListViewCaseList.Focus();
                SetListViewToTop();
            }
			else if (radioButtonSearchPatientName.IsChecked.Value)
			{
				if (textBoxSearchPatientName.Text.Length > 0)
				{					
					Surgical.TextSearchHandler textSearchHandler = new Surgical.TextSearchHandler(this.textBoxSearchPatientName.Text);
					object textSearchObject = textSearchHandler.GetSearchObject();
					if (textSearchObject is YellowstonePathology.Business.PatientName)
					{
						YellowstonePathology.Business.PatientName patientName = (YellowstonePathology.Business.PatientName)textSearchObject;
						this.m_LabUI.SearchEngine.SetFillByPatientName(patientName);
						this.m_LabUI.FillCaseList();
						ListViewCaseList.Focus();
						SetListViewToTop();				
					}
				}
				else
				{
					MessageBox.Show("This search option requires at least part of a patient name.",	"Enter Patient Name", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
			}			
            else if (this.radioButtonSearchThisMonth.IsChecked.Value == true)
            {
				this.m_LabUI.SearchEngine.SetFillByThisMonth(0);
				this.m_LabUI.FillCaseList();
				this.ListViewCaseList.Focus();
                this.SetListViewToTop();
            }
            else if (this.radioButtonSearchLastMonth.IsChecked.Value == true)
            {
				this.m_LabUI.SearchEngine.SetFillByLastMonth(0);
				this.m_LabUI.FillCaseList();
				this.ListViewCaseList.Focus();
                this.SetListViewToTop();
            }
			else if (this.radioButtonSearchToday.IsChecked.Value == true)
			{
				this.m_LabUI.SearchEngine.SetFillByToday(0);
				this.m_LabUI.FillCaseList();
				this.ListViewCaseList.Focus();
				this.SetListViewToTop();
			}
			else if (this.radioButtonSearchYesterday.IsChecked.Value == true)
			{
				this.m_LabUI.SearchEngine.SetFillByYesterday(0);
				this.m_LabUI.FillCaseList();
				this.ListViewCaseList.Focus();
				this.SetListViewToTop();
			}
			else if (this.radioButtonSearchNotAudited.IsChecked.Value == true)
			{
				if (this.comboBoxAuditType.SelectedItem != null)
				{
					string caseType = ((YellowstonePathology.Business.PanelSet.Model.PanelSetCaseType)this.comboBoxAuditType.SelectedItem).CaseType;
					this.m_LabUI.SearchEngine.SetFillByNotAudited(caseType);
					this.m_LabUI.FillCaseList();
					this.ListViewCaseList.Focus();
					this.SetListViewToTop();
				}
				else
				{
					MessageBox.Show("This search option requires a Case Type selection.",
						"Select a Case Type",
					MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
			}
			else if (this.radioButtonSearchMasterAccessionNo.IsChecked.Value == true)
			{
				if (textBoxSearchMasterAccessionNo.Text.Length > 0)
				{
					string masterAccessionNo = textBoxSearchMasterAccessionNo.Text;					
					this.m_LabUI.SearchEngine.SetFillByMasterAccessionNo(masterAccessionNo);
					this.m_LabUI.FillCaseList();
					ListViewCaseList.Focus();
					SetListViewToTop();
				}
				else
				{
					MessageBox.Show("This search option requires a Master Accession No.",
						"Enter Master Accession No",
					MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
			}
		}

		private void TextBoxSearch_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				if (this.TextBoxSearch.Text.Length >= 1)
				{
					Surgical.TextSearchHandler textSearchHandler = new Surgical.TextSearchHandler(this.TextBoxSearch.Text);
					object textSearchObject = textSearchHandler.GetSearchObject();
					if (textSearchObject is YellowstonePathology.Business.ReportNo)
					{
						YellowstonePathology.Business.ReportNo reportNo = (YellowstonePathology.Business.ReportNo)textSearchObject;
						this.m_LabUI.SearchEngine.SetFillByReportNo(reportNo.Value);
					}
					else if (textSearchObject is YellowstonePathology.Business.MasterAccessionNo)
					{
						YellowstonePathology.Business.MasterAccessionNo masterAccessionNo = (YellowstonePathology.Business.MasterAccessionNo)textSearchObject;
						this.m_LabUI.SearchEngine.SetFillByMasterAccessionNo(masterAccessionNo.Value);
					}
					else if (textSearchObject is YellowstonePathology.Business.PatientName)
					{
						YellowstonePathology.Business.PatientName patientName = (YellowstonePathology.Business.PatientName)textSearchObject;
						this.m_LabUI.SearchEngine.SetFillByPatientName(patientName);
					}
					this.m_LabUI.FillCaseList();
					ListViewCaseList.Focus();
					SetListViewToTop();
				}
			}
		}

		private void MenuItemSelectComment_Click(object sender, RoutedEventArgs e)
		{
			MenuItem menuItem = (MenuItem)sender;
			m_Comment = (YellowstonePathology.Business.Domain.Core.Comment)(menuItem.Tag);
		}

        public void ListViewPanelSetOrder_SelectionChanged(object sender, RoutedEventArgs args)
        {
			if(ListViewResultPanelSetOrder.SelectedIndex > -1)
			{
				this.m_LabUI.SetPanelSetOrder((YellowstonePathology.Business.Test.PanelSetOrder)ListViewResultPanelSetOrder.SelectedItem);
			}
        }

        private void ComboBoxPanelSetType_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ComboBoxPanelSetType.SelectedItem != null)
			{
                YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = (YellowstonePathology.Business.PanelSet.Model.PanelSet)this.ComboBoxPanelSetType.SelectedItem;
                this.m_LabUI.SearchEngine.SetFillByPanelSetId(panelSet.PanelSetId);
                this.m_LabUI.FillCaseList();
                this.SetListViewToTop();
            }
        }

		private void SetListViewToTop()
		{
			if (ListViewCaseList.Items.Count > 0)
			{
				ListViewCaseList.ScrollIntoView(ListViewCaseList.Items[0]);
			}
		}

        private void BatchResultPath_Finish(object sender, EventArgs e)
        {
            this.m_ResultDialog.Close();
        }        
		
		private void ButtonLinkPatient_Click(object sender, RoutedEventArgs e)
		{
            this.LinkPatient();
		}

		private void ButtonScan_Click(object sender, RoutedEventArgs e)
		{
			this.ButtonAcceptAliquotDetails.Focus();
		}		

		private void SpecimenGrid_CurrentCellChanged(object sender, EventArgs e)
		{
			if (this.SpecimenGrid.CurrentCell.Column != null)
			{
				if (this.SpecimenGrid.CurrentCell.Column.Header != null)
				{
					this.SpecimenGrid.BeginEdit();
				}
			}
		}

		private void SpecimenGrid_PreviewKeyDown(object sender, KeyEventArgs e)
		{
		}

		private void SpecimenGrid_GotFocus(object sender, RoutedEventArgs e)
		{
			if (this.SpecimenGrid.IsEnabled)
			{
				if (this.ListViewCaseList.SelectedItem != null)
				{
					if (SpecimenGrid.Items.Count == 0)
					{
						if (this.SpecimenGrid.CurrentCell.Column == null)
						{
							this.SpecimenGrid.SelectedItem = this.SpecimenGrid.Items[this.SpecimenGrid.Items.Count - 1];
							this.SpecimenGrid.CurrentCell = this.SpecimenGrid.SelectedCells[2];
						}
					}
				}
			}
		}

		private void SpecimenGrid_LostFocus(object sender, RoutedEventArgs e)
		{
			this.SpecimenGrid.SelectedIndex = -1;
		}

		private void ButtonCurrentFixationDetails_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListViewCaseList.SelectedItem != null)
			{
				this.ShowFixationDialog();
			}
		}

		private void ShowFixationDialog()
		{
			Login.FinalizeAccession.FixationPath fixationPath = new Login.FinalizeAccession.FixationPath(this.m_LabUI.AccessionOrder);
			fixationPath.Start();
		}

		private void ButtonAcceptAliquotDetails_Click(object sender, RoutedEventArgs e)
		{

		}				

		private void MainGrid_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Insert)
			{
				if (ListViewCaseList.SelectedItem != null)
				{
					this.SpecimenGrid.Focus();
					if (this.m_LabUI.AccessionOrder.SpecimenOrderCollection.Count > 0)
					{
						this.SpecimenGrid_PreviewKeyDown(sender, e);
					}
				}
			}
		}

		private void TextBoxCollectionDate_KeyUp(object sender, KeyEventArgs e)
		{
            Nullable<DateTime> targetDate = null;
            bool result = YellowstonePathology.Business.Helper.TextBoxHelper.IncrementDate(this.TextBoxCollectionDate.Text, ref targetDate, e);
            if (result == true) this.m_LabUI.AccessionOrder.CollectionDate = targetDate;            
		}

        private void ButtonBack_Click(object sender, RoutedEventArgs args)
        {
			/*int batchTypeId = 0;
            this.m_WorkDate = this.m_WorkDate.AddDays(-1);
			if (this.ComboBoxPanelSetType.SelectedItem != null)
			{
                YellowstonePathology.Business.BatchTypeListItem batchTypeListItem = (YellowstonePathology.Business.BatchTypeListItem)this.ComboBoxPanelSetType.SelectedItem;
				batchTypeId = batchTypeListItem.BatchTypeId;
			}
			this.m_LabUI.SearchEngine.SetFillByAccessionDate(this.m_WorkDate, batchTypeId, this.GetFacilityIdFromLocation(this.ComboBoxLogLocation.Text));
			this.m_LabUI.FillCaseList();*/
		}

        private void ButtonForward_Click(object sender, RoutedEventArgs args)
        {
			/*int batchTypeId = 0;
			this.m_WorkDate = this.m_WorkDate.AddDays(+1);

			if (this.ComboBoxPanelSetType.SelectedItem != null)
			{
                YellowstonePathology.Business.BatchTypeListItem batchTypeListItem = (YellowstonePathology.Business.BatchTypeListItem)this.ComboBoxPanelSetType.SelectedItem;
				batchTypeId = batchTypeListItem.BatchTypeId;
			}
            
            this.m_LabUI.SearchEngine.SetFillByAccessionDate(this.m_WorkDate, batchTypeId, this.GetFacilityIdFromLocation(this.ComboBoxLogLocation.Text));
			this.m_LabUI.FillCaseList();*/
		}

        private string GetFacilityIdFromLocation(string location)
        {
            string result = null;
            switch (this.ComboBoxLogLocation.Text)
            {
                case "All":
                    result = null;
                    break;
                case "Billings":
                    YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings ypBlgs = new Business.Facility.Model.YellowstonePathologistBillings();
                    result = ypBlgs.FacilityId;
                    break;
                case "Cody":
                    YellowstonePathology.Business.Facility.Model.YellowstonePathologistCody ypCdy = new Business.Facility.Model.YellowstonePathologistCody();
                    result = ypCdy.FacilityId;
                    break;
            }
            return result;
        }

		private void ComboBoxLogLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			
		}

        private void ButtonAssignPathologist_Click(object sender, RoutedEventArgs e)
        {
			if (this.m_LabUI.AccessionOrder.PanelSetOrderCollection.Count != 0)
            {				
				if (!this.m_LabUI.PanelSetOrder.Final)
                {
                    YellowstonePathology.UI.Common.PathologistSelectionDialog dialog = new YellowstonePathology.UI.Common.PathologistSelectionDialog(this.m_LabUI.PathologistUsers);
                    if (dialog.ShowDialog() == true)
                    {
						this.m_LabUI.PanelSetOrder.AssignedToId = dialog.SelectedPathologistUser.UserId;                        
                    }
                }
                else
                {
                    MessageBox.Show("This case is finaled.");                    
                }
            }
            else
            {
                MessageBox.Show("You must order the Surgical before assigning the Pathologist.");
            }
        }
        
		private void MenuItemPrintSelected_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListViewCaseList.SelectedItems.Count > 0)
			{
                YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = (YellowstonePathology.Business.PanelSet.Model.PanelSet)this.ComboBoxPanelSetType.SelectedItem;
				YellowstonePathology.Business.Search.ReportSearchList selectedItemList = new Business.Search.ReportSearchList();
				foreach (YellowstonePathology.Business.Search.ReportSearchItem item in ListViewCaseList.SelectedItems)
				{
					selectedItemList.Add(item);
				}

                this.m_LabUI.PrintCaseList(panelSet.PanelSetName, DateTime.Today, selectedItemList);
            }
			else
			{
				MessageBox.Show("Select all the cases to batch", "No cases selected");
			}
		}

        private void ButtonCaseHistory_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.UI.Common.CaseHistoryDialog caseHistoryDialog = new Common.CaseHistoryDialog(this.m_LabUI.AccessionOrder);
            caseHistoryDialog.ShowDialog();
        }        				

        private void SvhMedicalRecordNoReceived(string scanData)
        {
            if (this.m_LabUI.AccessionOrder.AccessionLock.IsLockAquiredByMe == true)
            {
                if (string.IsNullOrEmpty(this.m_LabUI.AccessionOrder.SvhMedicalRecord) == true)
                {
                    this.m_LabUI.AccessionOrder.SvhMedicalRecord = scanData;
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Would you like to replace the Svh Medical Record?", "Replace Data.", MessageBoxButton.OKCancel);
                    {
                        if (result == MessageBoxResult.OK)
                        {
                            this.m_LabUI.AccessionOrder.SvhMedicalRecord = scanData;
                        }
                    }
                }
            }
        }

        private void SvhAccountNoReceived(string scanData)
        {
            if (this.m_LabUI.AccessionOrder.AccessionLock.IsLockAquiredByMe == true)
            {
                if (string.IsNullOrEmpty(this.m_LabUI.AccessionOrder.SvhAccount) == true)
                {
                    this.m_LabUI.AccessionOrder.SvhAccount = scanData;
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Would you like to replace the Svh Account Number?", "Replace Data.", MessageBoxButton.OKCancel);
                    {
                        if (result == MessageBoxResult.OK)
                        {
                            this.m_LabUI.AccessionOrder.SvhMedicalRecord = scanData;
                        }
                    }
                }
            }
        }

		private void ClientScanReceived(YellowstonePathology.Business.BarcodeScanning.Barcode barcode)
		{            
            /*
			this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate()
			{
                int clientId = Convert.ToInt32(barcode.ID);
				YellowstonePathology.UI.Common.PhysicianClientSearchDialog dialog = new YellowstonePathology.UI.Common.PhysicianClientSearchDialog(this.m_LabUI.AccessionOrder, clientId);				
				bool? result = dialog.ShowDialog();
				if (result.HasValue && result.Value == true)
				{
					this.m_LabUI.AccessionOrder.SetPhysicianClient(dialog.PhysicianClientListItem);
				}
			}
			));
            */
		}

        private void MenuItemPrintMolecularLabel_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewCaseList.SelectedItems.Count != 0)
            {
                if (string.IsNullOrEmpty(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.MolecularLabelFormat) == false)
                {
					foreach (YellowstonePathology.Business.Search.ReportSearchItem item in this.ListViewCaseList.SelectedItems)
                    {
                        YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
                        YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(item.PanelSetId);

                        YellowstonePathology.Business.Label.Model.LabelFormatEnum labelFormat = (YellowstonePathology.Business.Label.Model.LabelFormatEnum)Enum.Parse(typeof(YellowstonePathology.Business.Label.Model.LabelFormatEnum), YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.MolecularLabelFormat);
                        YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ThinPrepFluid thinPrepFluid = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ThinPrepFluid();
                        YellowstonePathology.Business.Label.Model.MolecularLabelPrinter molecularLabelPrinter = new Business.Label.Model.MolecularLabelPrinter();
                        YellowstonePathology.Business.Label.Model.Label label = YellowstonePathology.Business.Label.Model.LabelFactory.GetMolecularLabel(labelFormat, item.MasterAccessionNo, item.PFirstName, item.PLastName, item.SpecimenDescription, panelSet, true);
                        molecularLabelPrinter.Queue.Enqueue(label);
                        molecularLabelPrinter.Print();
                    }
                }
                else
                {
                    MessageBox.Show("The label format must first be selected in User Preferences.");
                }
            }
        }        

        private void MenuItemOpenDocumentFolder_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewCaseList.SelectedItems.Count != 0)
            {
				YellowstonePathology.Business.Search.ReportSearchItem reportSearchItem = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewCaseList.SelectedItem;
				YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(reportSearchItem.ReportNo);
				string folderPath = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser);
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("Explorer.exe", folderPath);
                p.StartInfo = info;
                p.Start();
            }
        }		

        private void MenuItemPublish_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewCaseList.SelectedItem != null)
            {
				YellowstonePathology.Business.Search.ReportSearchItem item = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewCaseList.SelectedItem;
                YellowstonePathology.Business.Interface.ICaseDocument caseDocument = YellowstonePathology.Business.Document.DocumentFactory.GetDocument(this.m_LabUI.AccessionOrder, this.m_LabUI.PanelSetOrder, Business.Document.ReportSaveModeEnum.Normal);
				YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(item.ReportNo);
                YellowstonePathology.Business.Rules.MethodResult methodResult = caseDocument.DeleteCaseFiles(orderIdParser);
                if (methodResult.Success == true)
                {
                    caseDocument.Render();
                    caseDocument.Publish();
                    MessageBox.Show("The document has been published");
                }
                else
                {
                    MessageBox.Show(methodResult.Message);
                }
            }
        }

        private void ButtonCodes_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewCaseList.SelectedItem != null)
            {
                /*
                YellowstonePathology.Domain.ReportSearchItem item = (YellowstonePathology.Domain.ReportSearchItem)this.ListViewCaseList.SelectedItem;
				YellowstonePathology.UI.Billing.BillingCodeDialog billingcodeDialog = new Billing.BillingCodeDialog();
				billingcodeDialog.DoSearchByReportNo(item.ReportNo);
				billingcodeDialog.ShowDialog();
                */
            }
        }

        private void MenuItemSendHL7StatusMessage_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewCaseList.SelectedItem != null)
            {
				YellowstonePathology.Business.Search.ReportSearchItem item = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewCaseList.SelectedItem;                
                YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrdersByMasterAccessionNo(item.MasterAccessionNo);

                YellowstonePathology.Business.ClientOrder.Model.UniversalServiceCollection universalServiceIdCollection = YellowstonePathology.Business.ClientOrder.Model.UniversalServiceCollection.GetAll();
                YellowstonePathology.Business.ClientOrder.Model.UniversalService universalService = universalServiceIdCollection.GetByUniversalServiceId(clientOrderCollection[0].UniversalServiceId);

                YellowstonePathology.Business.HL7View.EPIC.EPICStatusMessage statusMessage = new Business.HL7View.EPIC.EPICStatusMessage(clientOrderCollection[0], YellowstonePathology.Business.HL7View.OrderStatusEnum.InProcess, universalService);
                YellowstonePathology.Business.Rules.MethodResult result = statusMessage.Send();

                if (result.Success == false)
                {
                    MessageBox.Show(result.Message);
                }
            }
        }

		private void ButtonReportDetails_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListViewCaseList.SelectedItem != null)
			{
				YellowstonePathology.Business.Search.ReportSearchItem reportSearchItem = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewCaseList.SelectedItem;
                YellowstonePathology.UI.ReportOrder.ReportOrderDetailPage reportOrderDetailPage = new YellowstonePathology.UI.ReportOrder.ReportOrderDetailPage(this.m_LabUI.AccessionOrder, reportSearchItem.ReportNo, this.m_SystemIdentity);
                reportOrderDetailPage.ShowDialog();
			}
		}        

        private void MenuItemSendMeditechResult_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewCaseList.SelectedItem != null)
            {
				//YellowstonePathology.Business.Search.ReportSearchItem item = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewCaseList.SelectedItem;
                //YellowstonePathology.Business.ReportDistribution.Model.MeditechDistribution meditechDistribution = new Business.ReportDistribution.Model.MeditechDistribution();
                //meditechDistribution.Distribute(item.ReportNo, );
                //MessageBox.Show("Meditech result has been sent");
            }
        }  

        private void MenuItemShowHl7Result_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewCaseList.SelectedItem != null)
            {
				YellowstonePathology.Business.Search.ReportSearchItem item = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewCaseList.SelectedItem;
				YellowstonePathology.Business.HL7View.IResultView resultView = YellowstonePathology.Business.HL7View.ResultViewFactory.GetResultView(item.ReportNo, this.m_LabUI.AccessionOrder, this.m_LabUI.AccessionOrder.ClientId, false);
				if (resultView != null)
				{
					XElement document = resultView.GetDocument();
					ReportDistribution.Hl7ResultDialog hl7ResultDialog = new ReportDistribution.Hl7ResultDialog(document);
					hl7ResultDialog.ShowDialog();
				}
				else
				{
					MessageBox.Show("The client does not receive an HL7 result.  Choose another report to view.");
				}
            }
        }        

        private void MenuItemSendHL7Result_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewCaseList.SelectedItem != null)
            {
				YellowstonePathology.Business.Search.ReportSearchItem item = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewCaseList.SelectedItem;                
                YellowstonePathology.Business.HL7View.IResultView resultView = YellowstonePathology.Business.HL7View.ResultViewFactory.GetResultView(item.ReportNo, this.m_LabUI.AccessionOrder, this.m_LabUI.AccessionOrder.ClientId, false);
                YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
                resultView.Send(methodResult);                               
            }
        }

        private void MenuItemSendHL7ResultTest_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewCaseList.SelectedItem != null)
            {
				YellowstonePathology.Business.Search.ReportSearchItem item = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewCaseList.SelectedItem;
                YellowstonePathology.Business.HL7View.IResultView resultView = YellowstonePathology.Business.HL7View.ResultViewFactory.GetResultView(item.ReportNo, this.m_LabUI.AccessionOrder, this.m_LabUI.AccessionOrder.ClientId, true);
                YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
                resultView.Send(methodResult);
            }
        }  

        private void MenuItemSendHL7Order_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.HL7View.Orders.ORMO01 ormo01 = new Business.HL7View.Orders.ORMO01(this.m_LabUI.AccessionOrder);
            XElement document = new XElement("HL7Message");
            ormo01.ToXml(document);
        }

		private void ButtonResults_Click(object sender, RoutedEventArgs e)
		{
            this.m_ResultDialog = new ResultDialog();
            YellowstonePathology.UI.Test.ResultPathFactory resultPathFactory = new Test.ResultPathFactory();
            resultPathFactory.Finished += new Test.ResultPathFactory.FinishedEventHandler(ResultPathFactory_Finished);

            bool started = resultPathFactory.Start(this.m_LabUI.PanelSetOrder, this.m_LabUI.AccessionOrder, this.m_ResultDialog.PageNavigator, this.m_ResultDialog, System.Windows.Visibility.Collapsed);
            if (started == true)
            {
                this.m_ResultDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("The result for this case is not available in this view.");
            }
		}

        private void ResultPathFactory_Finished(object sender, EventArgs e)
        {
            this.m_ResultDialog.Close();
        }

		private void ResultPath_Finish(object sender, EventArgs e)
		{
			this.m_ResultDialog.Close();
		}

        private void MenuItemSendPantherOrder_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewCaseList.SelectedItems.Count != 0)
            {                
                foreach(YellowstonePathology.Business.Search.ReportSearchItem reportSearchitem in this.ListViewCaseList.SelectedItems)
                {
                    YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(reportSearchitem.MasterAccessionNo, this.m_Writer);
                    if (accessionOrder.SpecimenOrderCollection.HasThinPrepFluidSpecimen() == true)
                    {
                        YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = accessionOrder.SpecimenOrderCollection.GetThinPrep();
                        if (specimenOrder.AliquotOrderCollection.HasPantherAliquot() == true)
                        {
                            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = specimenOrder.AliquotOrderCollection.GetPantherAliquot();
                            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportSearchitem.ReportNo);
                            YellowstonePathology.Business.HL7View.Panther.PantherAssay pantherAssay = null;
                            switch (panelSetOrder.PanelSetId)
                            {
                                case 14:
                                    pantherAssay = new Business.HL7View.Panther.PantherAssayHPV();
                                    break;
                                case 3:
                                    pantherAssay = new Business.HL7View.Panther.PantherAssayNGCT();
                                    break;
                                default:
                                    throw new Exception(panelSetOrder.PanelSetName + " is mot implemented yet.");
                            }

                            YellowstonePathology.Business.HL7View.Panther.PantherOrder pantherOrder = new Business.HL7View.Panther.PantherOrder(pantherAssay, specimenOrder, aliquotOrder, accessionOrder, panelSetOrder, YellowstonePathology.Business.HL7View.Panther.PantherActionCode.NewSample);
                            pantherOrder.Send();
                        }
                        else
                        {
                            MessageBox.Show("No Panther aliquot found.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No Thin Prep Fluid Specimen Found.");
                    }
                }                
            }
        }
    }
}
