using System;
using System.Collections.Generic;
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
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Flow
{
    /// <summary>
    /// Interaction logic for FlowWorkspace.xaml
    /// </summary>

    public partial class FlowWorkspace : System.Windows.Controls.UserControl
    {        
        private YellowstonePathology.Business.Flow.FlowUI m_FlowUI;        
        private UI.DocumentWorkspace m_DocumentViewer;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private Login.Receiving.LoginPageWindow m_LoginPageWindow;
        private MainWindowCommandButtonHandler m_MainWindowCommandButtonHandler;
        private TabItem m_Writer;

        public FlowWorkspace(MainWindowCommandButtonHandler mainWindowCommandButtonHandler, TabItem writer)
        {
            this.m_MainWindowCommandButtonHandler = mainWindowCommandButtonHandler;
            this.m_Writer = writer;
            this.m_SystemIdentity = YellowstonePathology.Business.User.SystemIdentity.Instance;

            this.m_DocumentViewer = new DocumentWorkspace();

            this.m_FlowUI = new YellowstonePathology.Business.Flow.FlowUI(this.m_Writer);

            InitializeComponent();

            this.DataContext = this.m_FlowUI;

            this.tabItemDocumentViewer.Content = this.m_DocumentViewer;            
            this.tabControlFlow.SelectionChanged += new SelectionChangedEventHandler(tabControlFlow_SelectionChanged);

            this.Loaded += new RoutedEventHandler(FlowWorkspace_Loaded);
            this.Unloaded += new RoutedEventHandler(FlowWorkspace_Unloaded);
            this.PreviewLostKeyboardFocus += FlowWorkspace_PreviewLostKeyboardFocus;            
        }

        private void FlowWorkspace_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            MainWindow.UpdateFocusedBindingSource(this);
        }

        private void FlowWorkspace_Loaded(object sender, RoutedEventArgs e)
        {
            this.m_MainWindowCommandButtonHandler.StartProviderDistributionPath += MainWindowCommandButtonHandler_StartProviderDistributionPath;
            this.m_MainWindowCommandButtonHandler.ShowAmendmentDialog += MainWindowCommandButtonHandler_ShowAmendmentDialog;
            this.m_MainWindowCommandButtonHandler.Save += MainWindowCommandButtonHandler_Save;
            this.m_MainWindowCommandButtonHandler.RemoveTab += MainWindowCommandButtonHandler_RemoveTab;
            this.m_MainWindowCommandButtonHandler.ShowMessagingDialog += MainWindowCommandButtonHandler_ShowMessagingDialog;

            UI.AppMessaging.MessagingPath.Instance.LockReleasedActionList.Add(this.Save);
            UI.AppMessaging.MessagingPath.Instance.LockAquiredActionList.Add(this.m_FlowUI.SetAccess);
        }

        private void MainWindowCommandButtonHandler_RemoveTab(object sender, EventArgs e)
        {
            Business.Persistence.DocumentGateway.Instance.Push(this.m_Writer);
        }

        private void MainWindowCommandButtonHandler_Save(object sender, EventArgs e)
        {
            this.Save();
        }

        private void Save()
        {
            if (this.m_FlowUI.AccessionOrder != null)
            {
                MainWindow.MoveKeyboardFocusNextThenBack();
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.ReleaseLock(this.m_FlowUI.AccessionOrder, this.m_Writer);
                if (this.m_FlowUI.AccessionOrder.AccessionLock.IsLockAquiredByMe == false)
                {
                    this.m_FlowUI.SetAccess();
                    this.m_FlowUI.NotifyPropertyChanged(string.Empty);
                }

                this.m_FlowUI.FlowLogSearch.FlowLogList.SetLockIsAquiredByMe(this.m_FlowUI.AccessionOrder);
            }
        }

        private void MainWindowCommandButtonHandler_ShowAmendmentDialog(object sender, EventArgs e)
        {            
            if(((TabItem)this.Parent).IsSelected && this.m_FlowUI.AccessionOrder != null)
            {
                YellowstonePathology.UI.AmendmentPageController amendmentPageController = new AmendmentPageController(this.m_FlowUI.AccessionOrder, this.m_FlowUI.PanelSetOrderLeukemiaLymphoma);
                amendmentPageController.ShowDialog();
                this.m_FlowUI.GetAccessionOrder(this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.ReportNo, this.m_FlowUI.AccessionOrder.MasterAccessionNo);
            }            
        }

        private void MainWindowCommandButtonHandler_StartProviderDistributionPath(object sender, EventArgs e)
        {
            if (this.m_FlowUI.AccessionOrder != null)
            {
                YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath providerDistributionPath =
                    new YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath(this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.ReportNo, this.m_FlowUI.AccessionOrder,
                    System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed);
                providerDistributionPath.Start();
                this.m_FlowUI.GetAccessionOrder(this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.ReportNo, this.m_FlowUI.AccessionOrder.MasterAccessionNo);
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

        private void MainWindowCommandButtonHandler_ShowMessagingDialog(object sender, EventArgs e)
        {
            if (this.m_FlowUI.AccessionOrder != null && this.m_FlowUI.AccessionOrder.AccessionLock.IsLockAquiredByMe == false && this.m_FlowUI.AccessionOrder.AccessionLock.IsLockAquired == true)
            {
                UI.AppMessaging.MessagingPath.Instance.Start(this.m_FlowUI.AccessionOrder);
            }
        }

        private void MessageQueue_AquireLock(object sender, EventArgs e)
        {
            string masterAccessionNo = (string)sender;
            if (this.m_FlowUI.AccessionOrder != null && this.m_FlowUI.AccessionOrder.MasterAccessionNo == masterAccessionNo)
            {
                this.m_FlowUI.GetAccessionOrder(this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.ReportNo, masterAccessionNo);
                this.m_FlowUI.FlowLogSearch.FlowLogList.SetLockIsAquiredByMe(this.m_FlowUI.AccessionOrder);
            }
        }

        private void MessageQueue_ReleaseLock(object sender, EventArgs e)
        {
            string masterAccessionNo = (string)sender;
            if (this.m_FlowUI.AccessionOrder != null && this.m_FlowUI.AccessionOrder.MasterAccessionNo == masterAccessionNo)
            {
                MainWindow.MoveKeyboardFocusNextThenBack();
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.ReleaseLock(this.m_FlowUI.AccessionOrder, this.m_Writer);
                if (this.m_FlowUI.AccessionOrder.AccessionLock.IsLockAquiredByMe == false)
                {
                    this.m_FlowUI.SetAccess();
                    this.m_FlowUI.NotifyPropertyChanged(string.Empty);
                }

                this.m_FlowUI.FlowLogSearch.FlowLogList.SetLockIsAquiredByMe(this.m_FlowUI.AccessionOrder);
            }
        }

        private void FlowWorkspace_Unloaded(object sender, RoutedEventArgs e)
        {
            this.m_MainWindowCommandButtonHandler.StartProviderDistributionPath -= MainWindowCommandButtonHandler_StartProviderDistributionPath;
            this.m_MainWindowCommandButtonHandler.ShowAmendmentDialog -= MainWindowCommandButtonHandler_ShowAmendmentDialog;
            this.m_MainWindowCommandButtonHandler.Save -= MainWindowCommandButtonHandler_Save;
            this.m_MainWindowCommandButtonHandler.RemoveTab -= MainWindowCommandButtonHandler_RemoveTab;
            this.m_MainWindowCommandButtonHandler.ShowMessagingDialog -= MainWindowCommandButtonHandler_ShowMessagingDialog;

            UI.AppMessaging.MessagingPath.Instance.LockReleasedActionList.Remove(this.Save);
            UI.AppMessaging.MessagingPath.Instance.LockAquiredActionList.Remove(this.m_FlowUI.SetAccess);

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
        }        

        public void GatingCount_LostFocus(object sender, RoutedEventArgs args)
        {
			((YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma)this.m_FlowUI.PanelSetOrderLeukemiaLymphoma).RefreshGatingPercent();
        }

        public void ListViewFlowMarkers_MouseDoubleClick(object sender, RoutedEventArgs args)
        {
            if (this.listViewFlowMarkers.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.Flow.MarkerItem item = (YellowstonePathology.Business.Flow.MarkerItem)this.listViewFlowMarkers.SelectedItem;
                this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.FlowMarkerCollection.Add(this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.ReportNo, item);
            }
        }

        public void MenuItemAddIcd9Code_Click(object sender, RoutedEventArgs args)
        {
            if (this.ListViewICDCodes.SelectedItem != null)
            {
                YellowstonePathology.Business.Billing.Model.ICDCode item = (YellowstonePathology.Business.Billing.Model.ICDCode)ListViewICDCodes.SelectedItem;                
                this.m_FlowUI.AddICD9Code(item.ICD9Code, item.ICD10Code);
            }
        }

        public void ButtonTechFinal_Click(object sender, RoutedEventArgs args)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_FlowUI.IsOkToMedTechFinal();
            if (methodResult.Success == true)
            {
                this.m_FlowUI.MedTechFinal();
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }        
        }

        public void ButtonTechUnfinal_Click(object sender, RoutedEventArgs args)
        {
            this.m_FlowUI.MedTechUnfinal();
        }

        public void MenuItemDeleteIcd9Code_Click(object sender, RoutedEventArgs args)
        {
            if (this.ListViewICD9BillingCodeCollection.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.Billing.Model.ICD9BillingCode item = (YellowstonePathology.Business.Billing.Model.ICD9BillingCode)this.ListViewICD9BillingCodeCollection.SelectedItem;
                MessageBoxResult result = MessageBox.Show("Delete ICD9 " + item.ICD9Code, "Delete Marker", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.m_FlowUI.RemoveICD9Code(item);
                }
            }
        }

        public void MenuItemSetCptCodes_Click(object sender, RoutedEventArgs args)
        {

        }

        public void ContextMenuFlowMarkers_Click(object sender, RoutedEventArgs args)
        {
            if (this.listViewMarkers.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.Flow.FlowMarkerItem item = (YellowstonePathology.Business.Flow.FlowMarkerItem)this.listViewMarkers.SelectedItem;
                MessageBoxResult result = MessageBox.Show("Delete Marker " + item.Name, "Delete Marker", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.FlowMarkerCollection.Remove(item);
                }
            }
        }

        private void tabControlFlow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.tabControlFlow.SelectedItem != null)
            {
                TabItem item = (TabItem)this.tabControlFlow.SelectedItem;
                switch (item.Header.ToString())
                {
                    case "Results":
                        this.tabControlRightPane.SelectedItem = this.tabItemComment;
                        break;
                    case "Client":
                        this.tabControlRightPane.SelectedItem = this.tabItemClientSearch;
                        break;
                    case "Markers":
                        this.tabControlRightPane.SelectedItem = this.tabItemMarkers;
                        break;
                }
            }
        }

        public void ComboBoxLeukemiaResult_SelectionChanged(object sender, RoutedEventArgs args)
        {
            if (this.comboBoxLeukemiaResult.SelectedValue != null)
            {
                ComboBoxItem item = (ComboBoxItem)this.comboBoxLeukemiaResult.SelectedValue;
                switch (item.Content.ToString())
                {
                    case "Normal":
                        this.stackPanelCellDescription.Visibility = Visibility.Collapsed;
                        this.stackPanelBTCellSelection.Visibility = Visibility.Collapsed;
                        this.m_FlowUI.FlowComment.CommentFilter = "Normal";
                        break;
                    case "Abnormal":
                        this.stackPanelCellDescription.Visibility = Visibility.Visible;
                        this.stackPanelBTCellSelection.Visibility = Visibility.Visible;
                        if (this.comboBoxCellDescription.SelectedValue != null)
                        {
                            ComboBoxItem abnormalItem = (ComboBoxItem)this.comboBoxCellDescription.SelectedValue;
                            this.m_FlowUI.FlowComment.CommentFilter = abnormalItem.Content.ToString();
                        }
                        break;
                }
            }
        }

        public void ComboBoxCellDescription_SelectionChanged(object sender, RoutedEventArgs args)
        {
            if (this.comboBoxCellDescription.SelectedValue != null)
            {
                ComboBoxItem item = (ComboBoxItem)this.comboBoxCellDescription.SelectedValue;
                switch (item.Content.ToString())
                {
                    case "Lymphocytes":
                        this.stackPanelBTCellSelection.Visibility = Visibility.Visible;
                        break;
                    default:
                        this.stackPanelBTCellSelection.Visibility = Visibility.Collapsed;
                        break;
                }
                this.m_FlowUI.FlowComment.CommentFilter = item.Content.ToString();
            }
        }

        private void ComboBoxTestPerformed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBoxTestPerformed.SelectedItem != null)
            {
                YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = (YellowstonePathology.Business.PanelSet.Model.PanelSet)this.ComboBoxTestPerformed.SelectedItem;
                this.m_FlowUI.ChangePanelSetIdentification(panelSet);
            }
        }

        public void SaveData(object target, ExecutedRoutedEventArgs args)
        {
            MessageBox.Show("The Flow Workspace has been saved.");
        }       

        public void ListViewFlowCaseList_SelectionChanged(object sender, RoutedEventArgs args)
        {
            if (this.ListViewFlowCaseList.SelectedIndex >= 0)
            {
                YellowstonePathology.Business.Flow.FlowLogListItem flowLogListItem = (YellowstonePathology.Business.Flow.FlowLogListItem)this.ListViewFlowCaseList.SelectedItem;
                this.GetCase(flowLogListItem.ReportNo, flowLogListItem.MasterAccessionNo);
                this.m_FlowUI.FlowLogSearch.FlowLogList.SetLockIsAquiredByMe(this.m_FlowUI.AccessionOrder);
            }
            else
            {
                this.m_FlowUI.AccessionOrder = null;
                this.m_FlowUI.PanelSetOrderLeukemiaLymphoma = null;
                this.m_FlowUI.CaseDocumentCollection = null;
                this.m_DocumentViewer.ClearContent();
                this.m_FlowUI.ICD9BillingCodeCollection = null;
            }
        }

        public void GetCase(string reportNo, string masterAccessionNo)
        {
            this.m_FlowUI.GetAccessionOrder(reportNo, masterAccessionNo);
            YellowstonePathology.Business.Document.CaseDocument requisition = this.m_FlowUI.CaseDocumentCollection.GetFirstRequisition();
            this.m_DocumentViewer.ShowDocument(requisition);            
            
            this.m_FlowUI.ICD9BillingCodeCollection = this.m_FlowUI.AccessionOrder.ICD9BillingCodeCollection.GetReportCollection(reportNo);            
        }

        public void ButtonCreateComment_Click(object sender, RoutedEventArgs args)
        {
            List<YellowstonePathology.Business.Interface.IFlowMarker> flowMarkers = this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.FlowMarkerCollection.ToList<YellowstonePathology.Business.Interface.IFlowMarker>();

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_FlowUI.AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.OrderedOn, this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.OrderedOnId);
			YellowstonePathology.Business.Helper.FlowCommentHelper comment = new YellowstonePathology.Business.Helper.FlowCommentHelper(specimenOrder.Description, this.m_FlowUI.PanelSetOrderLeukemiaLymphoma, flowMarkers);
            comment.SetInterpretiveComment();
        }

        private void textBoxSearchReportNo_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                this.Search();
            }
        }

        public void RadioButtonFlowSearch_Checked(object sender, RoutedEventArgs args)
        {
            this.textBoxSearchReportNo.Visibility = Visibility.Hidden;
            this.textBoxSearchPatientName.Visibility = Visibility.Hidden;
            this.stackPanelAccessionMonthSearch.Visibility = Visibility.Hidden;
            this.comboBoxFlowSearchTestType.Visibility = Visibility.Hidden;

            RadioButton radioButton = (RadioButton)args.OriginalSource;
            switch (radioButton.Name)
            {
                case "radioButtonSearchReportNo":
                    this.textBoxSearchReportNo.Visibility = Visibility.Visible;
                    this.textBoxSearchReportNo.Focus();
                    this.textBoxSearchReportNo.CaretIndex = this.textBoxSearchReportNo.Text.Length;
                    break;
                case "radioButtonSearchPatientName":
                    this.textBoxSearchPatientName.Visibility = Visibility.Visible;
                    break;
                case "radioButtonSearchSelectMonth":
                    this.stackPanelAccessionMonthSearch.Visibility = Visibility.Visible;
                    break;
                case "radioButtonSearchTestId":
                    this.comboBoxFlowSearchTestType.Visibility = Visibility.Visible;
                    break;
            }
        }

        public void ButtonSearch_Click(object sender, RoutedEventArgs args)
        {
            this.Search();
        }

        private void Search()
        {
            if (this.radioButtonSearchTestId.IsChecked == true)
            {
                if (this.comboBoxFlowSearchTestType.SelectedItem != null)
                {
                    YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = (YellowstonePathology.Business.PanelSet.Model.PanelSet)this.comboBoxFlowSearchTestType.SelectedItem;
                    this.m_FlowUI.FlowLogSearch.SetByTestType(panelSet.PanelSetId);
                    this.m_FlowUI.Search();
                }
            }
            if (this.radioButtonSearchCurrentMonth.IsChecked == true)
            {
                this.m_FlowUI.FlowLogSearch.SetByAccessionMonth(DateTime.Today);
                this.m_FlowUI.Search();
            }
            if (this.radioButtonSearchLeukemiaNotFinal.IsChecked == true)
            {
                this.m_FlowUI.FlowLogSearch.SetByLeukemiaNotFinal();
                this.m_FlowUI.Search();
            }
            if (this.radioButtonSearchLastMonth.IsChecked == true)
            {
                this.m_FlowUI.FlowLogSearch.SetByAccessionMonth(DateTime.Today.AddMonths(-1));
                this.m_FlowUI.Search();
            }
            if (this.radioButtonSearchReportNo.IsChecked == true)
            {
                String reportNo = this.ReportNoFromText(this.textBoxSearchReportNo.Text);
                if(string.IsNullOrEmpty(reportNo) ==false && reportNo != this.textBoxSearchReportNo.Text)
                {
                    this.textBoxSearchReportNo.Text = reportNo;
                }
                this.m_FlowUI.FlowLogSearch.SetByReportNo(reportNo);
                this.m_FlowUI.Search();
            }
            if (this.radioButtonSearchPatientName.IsChecked == true)
            {
                if (this.textBoxSearchPatientName.Text.Length > 0)
                {
                    this.m_FlowUI.FlowLogSearch.SetByPatientName(this.textBoxSearchPatientName.Text);
                    this.m_FlowUI.Search();
                }
            }
            if (this.radioButtonSearchSelectMonth.IsChecked == true)
            {
                if (this.comboBoxYear.Text == string.Empty | this.comboBoxMonth.Text == string.Empty)
                {
                    return;
                }
                else
                {
                    ComboBoxItem item = (ComboBoxItem)this.comboBoxMonth.SelectedItem;
                    int month = Convert.ToInt32(item.Tag.ToString());
                    int year = Convert.ToInt32(this.comboBoxYear.Text);
                    DateTime date = DateTime.Parse(month.ToString() + "/" + 1 + "/" + year.ToString());
                    this.m_FlowUI.FlowLogSearch.SetByAccessionMonth(date);
                    this.m_FlowUI.Search();
                }
            }

            this.tabControlBottomLeftPane.SelectedIndex = 0;
            this.ListViewFlowCaseList.SelectedIndex = -1;        
        }

        private string ReportNoFromText(string text)
        {
            string result = string.Empty;
            Surgical.TextSearchHandler textSearchHandler = new Surgical.TextSearchHandler(text);
            object textSearchObject = textSearchHandler.GetSearchObject();
            if (textSearchObject is YellowstonePathology.Business.ReportNo)
            {
                YellowstonePathology.Business.ReportNo reportNo = (YellowstonePathology.Business.ReportNo)textSearchObject;
                result = reportNo.Value;
            }
            else if (textSearchObject is YellowstonePathology.Business.MasterAccessionNo)
            {
                YellowstonePathology.Business.MasterAccessionNo masterAccessionNo = (YellowstonePathology.Business.MasterAccessionNo)textSearchObject;
                result = masterAccessionNo.Value + ".F1";
            }
            return result;
        }

        public void ListViewComments_MouseDoubleClick(object sender, RoutedEventArgs args)
        {
            List<YellowstonePathology.Business.Interface.IFlowMarker> flowMarkers = this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.FlowMarkerCollection.ToList<YellowstonePathology.Business.Interface.IFlowMarker>();
			YellowstonePathology.Business.Helper.FlowCommentHelper comment = new YellowstonePathology.Business.Helper.FlowCommentHelper(this.m_FlowUI.AccessionOrder.SpecimenOrderCollection[0].Description, this.m_FlowUI.PanelSetOrderLeukemiaLymphoma, flowMarkers);
            YellowstonePathology.Business.Flow.FlowCommentItem item = (YellowstonePathology.Business.Flow.FlowCommentItem)this.listViewComments.SelectedItem;
            comment.AddComment(item.Category, item.Impression, item.Comment);
        }

        public void ButtonSetMarkerPanel_Click(object sender, RoutedEventArgs args)
        {
            Button button = (Button)sender;
            ComboBox comboBox = null;

            switch (button.Name)
            {
                case "ButtonSetCommonMarkerPanel":
                    comboBox = this.comboBoxCommonMarkerPanel;
                    break;
                case "ButtonSetMarkerPanel":
                    comboBox = this.comboBoxMarkerPanel;
                    break;
            }

            if (comboBox.SelectedItem != null)
            {
                int panelId = (int)comboBox.SelectedValue;
                this.m_FlowUI.SetMarkerPanel(panelId);
            }
        }

        public void ButtonAddMarker_Click(object sender, RoutedEventArgs args)
        {
            if (this.listViewFlowMarkers.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.Flow.MarkerItem item = (YellowstonePathology.Business.Flow.MarkerItem)this.listViewFlowMarkers.SelectedItem;
                this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.FlowMarkerCollection.Add(this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.ReportNo, item);
                this.m_FlowUI.GetAccessionOrder(this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.ReportNo, this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.MasterAccessionNo);
            }
        }

        public void ButtonRemoveMarker_Click(object sender, RoutedEventArgs arts)
        {
            if (this.listViewMarkers.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.Flow.FlowMarkerItem item = (YellowstonePathology.Business.Flow.FlowMarkerItem)this.listViewMarkers.SelectedItem;
                this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.FlowMarkerCollection.Remove(item);
            }
        }

        public void ButtonSignReport_Click(object sender, RoutedEventArgs args)
        {
            if (this.m_FlowUI.AccessionOrder != null)
            {
                if (this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.AssignedToId == this.m_SystemIdentity.User.UserId)
                {
                    if (this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.Final == false)
                    {
                        YellowstonePathology.Business.Audit.Model.AuditResult auditResult = this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.IsOkToFinalize(this.m_FlowUI.AccessionOrder);

                        if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.OK)
                        {
                            this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.Finish(this.m_FlowUI.AccessionOrder);
                        }
                        else
                        {
                            MessageBox.Show(auditResult.Message);
                        }
                    }
                    else
                    {
                        Business.Rules.MethodResult methodResult = this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.IsOkToUnfinalize();
                        if(methodResult.Success == true)
                        {
                            this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.Unfinalize();
                        }
                        else
                        {
                            MessageBox.Show(methodResult.Message);
                        }
                    }
                    this.m_FlowUI.NotifyPropertyChanged("SignReportButtonContent");
                    this.m_FlowUI.NotifyPropertyChanged("SignReportButtonEnabled");
                }
                else
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("You cannot sign this case because it is not assigned to you. Would you like me to assign this case to you?", "Not Assigned To You", MessageBoxButton.YesNo);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.AssignedToId = this.m_SystemIdentity.User.UserId;
                        MessageBox.Show("This case has been assigned to you. You can now sign it out.");
                    }
                }
            }
        }

        public void ButtonPrintReport_Click(object sender, RoutedEventArgs args)
        {

        }

        public void ButtonPublishReport_Click(object sender, RoutedEventArgs args)
        {
            if (this.m_FlowUI.AccessionOrder != null)
            {
				YellowstonePathology.Business.Test.LLP.LeukemiaLymphomaWordDocument report = new YellowstonePathology.Business.Test.LLP.LeukemiaLymphomaWordDocument(this.m_FlowUI.AccessionOrder, this.m_FlowUI.PanelSetOrderLeukemiaLymphoma, Business.Document.ReportSaveModeEnum.Normal);
                report.Render();
            }
        }

        public void ButtonClose_Click(object sender, RoutedEventArgs args)
        {
            this.buttonViewReport.Focus();
        }

        void TextBoxPBirthdate_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            BindingExpression binding = textBox.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
            if (binding.HasError == true)
            {
                e.Handled = true;
            }
            else
            {
                binding.UpdateTarget();
            }
        }        

        public void ButtonRemoveICDCode_Click(object sender, RoutedEventArgs args)
        {
            if (this.ListViewICD9BillingCodeCollection.SelectedItem != null)
            {
                YellowstonePathology.Business.Billing.Model.ICD9BillingCode item = (YellowstonePathology.Business.Billing.Model.ICD9BillingCode)this.ListViewICD9BillingCodeCollection.SelectedItem;
                this.m_FlowUI.RemoveICD9Code(item);
            }
        }

        public void TextBoxSelect_GotFocus(object sender, RoutedEventArgs args)
        {
            TextBox textBox = (TextBox)args.Source;
            textBox.SelectAll();
        }

        public void ButtonViewDocument_Click(object sender, RoutedEventArgs args)
        {
            if (this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.PanelSetId != 19 && this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.PanelSetId != 20)
            {
                YellowstonePathology.Business.Interface.ICaseDocument caseDocument = YellowstonePathology.Business.Document.DocumentFactory.GetDocument(this.m_FlowUI.AccessionOrder, this.m_FlowUI.PanelSetOrderLeukemiaLymphoma, Business.Document.ReportSaveModeEnum.Draft);
                caseDocument.Render();

				YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.ReportNo);
				string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
                YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
            }
        }

        public void ButtonViewReport_Click(object sender, RoutedEventArgs args)
        {
            if (this.m_FlowUI.AccessionOrder != null)
            {
                if (this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.PanelSetId == 20)
                {
					YellowstonePathology.Business.Test.LLP.LeukemiaLymphomaWordDocument report = new YellowstonePathology.Business.Test.LLP.LeukemiaLymphomaWordDocument(this.m_FlowUI.AccessionOrder, this.m_FlowUI.PanelSetOrderLeukemiaLymphoma, Business.Document.ReportSaveModeEnum.Draft);
                    report.Render();
					YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.ReportNo);
					string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
					YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
                }
            }
        }        

        private void ShowAmendmentDialog(object target, ExecutedRoutedEventArgs args)
        {   
            if(this.m_FlowUI.AccessionOrder != null)
            {
                YellowstonePathology.UI.AmendmentPageController amendmentPageController = new AmendmentPageController(this.m_FlowUI.AccessionOrder, this.m_FlowUI.PanelSetOrderLeukemiaLymphoma);
                amendmentPageController.ShowDialog();
                this.m_FlowUI.GetAccessionOrder(this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.ReportNo, this.m_FlowUI.AccessionOrder.MasterAccessionNo);
            }     	            
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this.m_Writer);
            this.m_FlowUI.Search();
        }                

        public void ListViewPatientHistory_SelectionChanged(object sender, RoutedEventArgs args)
        {
            if (this.listViewPatientHistory.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.Patient.Model.PatientHistoryListItem item = (YellowstonePathology.Business.Patient.Model.PatientHistoryListItem)this.listViewPatientHistory.SelectedItem;
                this.m_FlowUI.PatientHistoryList.SetCaseDocumentCollection(item.ReportNo);
                this.m_FlowUI.NotifyPropertyChanged("PatientHistoryList");
            }
        }

        public void ListViewCaseFileList_SelectionChanged(object sender, RoutedEventArgs args)
        {

        }

        private void listViewCaseFileList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.listViewCaseFileList.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.Document.CaseDocument item = (YellowstonePathology.Business.Document.CaseDocument)this.listViewCaseFileList.SelectedItem;
                this.m_DocumentViewer.ShowDocument(item);
            }
        }

        private void ButtonAuditComplete_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.Audited == true)
            {
                this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.Audited = false;
                this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.AuditedDate = null;
                this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.AuditedById = 0;
            }
            else
            {
                this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.Audited = true;
                this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.AuditedDate = DateTime.Now;
                this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.AuditedById = this.m_SystemIdentity.User.UserId;
            }
        }

        private void ButtonPatientLinking_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Patient.Model.PatientLinker patientLinker = new Business.Patient.Model.PatientLinker(this.m_FlowUI.AccessionOrder.MasterAccessionNo,
				this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.ReportNo,
                this.m_FlowUI.AccessionOrder.PFirstName,
				this.m_FlowUI.AccessionOrder.PLastName,
				this.m_FlowUI.AccessionOrder.PMiddleInitial,
				this.m_FlowUI.AccessionOrder.PSSN,
                this.m_FlowUI.AccessionOrder.PatientId,
				this.m_FlowUI.AccessionOrder.PBirthdate);

            if (patientLinker.IsOkToLink.IsValid == true)
            {
                YellowstonePathology.UI.Common.PatientLinkingDialog patientLinkingDialog = new Common.PatientLinkingDialog(this.m_FlowUI.AccessionOrder, Business.Patient.Model.PatientLinkingListModeEnum.AccessionOrder, patientLinker);
                patientLinkingDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show(patientLinker.IsOkToLink.Message, "Missing Information");
            }
        }

        private void ButtonTechSendLinkToYpiConnect_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonSendNotificationToNeogenomics_Click(object sender, RoutedEventArgs e)
        {
            //NeogenomicsFlowSignout@ypii.com;
            if (this.m_FlowUI.AccessionOrder != null)
            {
                string from = "FlowStaff@ypii.com";
                string to = "NeogenomicsFlowSignout@ypii.com";
                string subject = "Report " + this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.ReportNo + " is ready to be signed out.";
                string body = "Report " + this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.ReportNo + " is ready to be signed out. You can view this case at: https://www.YellowstonePathology.com/YPIConnect/ClientApplication/Version/2.0.0.0/Pathologist/YellowstonePathology.YpiConnect.Client.application . If you have recently agreed to read this YPI flow case, please reply to this email.";

                System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage(from, to, subject, body);
                mailMessage.CC.Add("FlowStaff@ypii.com");
                mailMessage.CC.Add("yolanda.hutton@ypii.com");
                mailMessage.CC.Add("kevin.benge@ypii.com");

                Uri uri = new Uri("http://tempuri.org/");
                System.Net.ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
                System.Net.NetworkCredential credential = credentials.GetCredential(uri, "Basic");

                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.111");
                client.Credentials = credential;
                client.Send(mailMessage);

                MessageBox.Show("A link to YPI Connect has been sent to Neogenomics regarding report " + this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.ReportNo);
            }
        }

        private void TextBoxCollectionDate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.Down)
            {
                SplitDateTime splitDateTime = new SplitDateTime(this.TextBoxCollectionDate.Text);
                if (e.Key == Key.Up)
                {
                    splitDateTime.AddDay();
                }
                else if (e.Key == Key.Down)
                {
                    splitDateTime.SubtractDay();
                }

                this.m_FlowUI.AccessionOrder.CollectionDate = splitDateTime.Date;
                this.m_FlowUI.AccessionOrder.CollectionTime = splitDateTime.Time;
            }
        }        
       
        private void ButtonShowSpecimenDialog_Click(object sender, RoutedEventArgs e)
        {        	
            if (this.m_FlowUI.PanelSetOrderLeukemiaLymphoma != null && m_FlowUI.PanelSetOrderLeukemiaLymphoma.OrderedOnId != null)
            {
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_FlowUI.AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.OrderedOnId);
                YellowstonePathology.UI.Login.SpecimenOrderDetailsPage specimenOrderDetailsPage = new YellowstonePathology.UI.Login.SpecimenOrderDetailsPage(this.m_FlowUI.AccessionOrder, specimenOrder);
                specimenOrderDetailsPage.Next += new Login.SpecimenOrderDetailsPage.NextEventHandler(SpecimenOrderDetailsPage_Next);
                specimenOrderDetailsPage.Back += new Login.SpecimenOrderDetailsPage.BackEventHandler(SpecimenOrderDetailsPage_Next);
                this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
                this.m_LoginPageWindow.PageNavigator.Navigate(specimenOrderDetailsPage);
                this.m_LoginPageWindow.ShowDialog();                
            }
        }

        private void ButtonShowSelectSpecimenDialog_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_FlowUI.PanelSetOrderLeukemiaLymphoma != null)
            {
                YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_FlowUI.AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.OrderedOnId);
                YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
                YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.PanelSetId);
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new Business.Test.TestOrderInfo(panelSet, orderTarget, false);
                Login.Receiving.SpecimenSelectionPage specimenSelectionPage = new Login.Receiving.SpecimenSelectionPage(this.m_FlowUI.AccessionOrder, testOrderInfo);
                specimenSelectionPage.Back += new Login.Receiving.SpecimenSelectionPage.BackEventHandler(SpecimenSelectionPage_Back);
                specimenSelectionPage.TargetSelected += new Login.Receiving.SpecimenSelectionPage.TargetSelectedEventHandler(OrderTargetSelectionPage_TargetSelected);

                this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
                this.m_LoginPageWindow.PageNavigator.Navigate(specimenSelectionPage);
                this.m_LoginPageWindow.ShowDialog();
            }
        }

        private void OrderTargetSelectionPage_TargetSelected(object sender, CustomEventArgs.TestOrderInfoEventArgs e)
        {
            this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.OrderedOnId = e.TestOrderInfo.OrderTarget.GetId();
            this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.OrderedOn = e.TestOrderInfo.OrderTarget.GetOrderedOnType();
            this.m_LoginPageWindow.Close();
        }

        private void SpecimenSelectionPage_Back(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        private void SpecimenOrderDetailsPage_Next(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }        

        private void ButtonReceiveSpecimen_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.UI.Login.Receiving.ReceiveSpecimenPath clientOrderReceivingPathWithSecurity = new YellowstonePathology.UI.Login.Receiving.ReceiveSpecimenPath();
            clientOrderReceivingPathWithSecurity.Start();
        }

        private void ButtonOrderFlowOnExistingAccession_Click(object sender, RoutedEventArgs e)
        {
            this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
            this.m_LoginPageWindow.Width = 300;
            this.m_LoginPageWindow.Height = 300;

            YellowstonePathology.UI.Login.SearchReportNoPage searchReportNoPage = new Login.SearchReportNoPage();
            searchReportNoPage.Return += new Login.SearchReportNoPage.ReturnEventHandler(SearchReportNoPage_Return);
            this.m_LoginPageWindow.PageNavigator.Navigate(searchReportNoPage);
            this.m_LoginPageWindow.ShowDialog();
        }

        private void SearchReportNoPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {                
            switch (e.PageNavigationDirectionEnum)
            {
                case UI.Navigation.PageNavigationDirectionEnum.Next:
                    string reportNo = (string)e.Data;
                    this.m_LoginPageWindow.Close();
                    this.ShowReportOrderDialog(reportNo);
                    break;
                case UI.Navigation.PageNavigationDirectionEnum.Back:
                    this.m_LoginPageWindow.Close();
                    break;
            }            
        }

        private void ShowReportOrderDialog(string reportNo)
        {
            string masterAccessionNo = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromReportNo(reportNo);
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this.m_Writer);
            if (accessionOrder != null)
            {
                YellowstonePathology.Business.Gateway.ClientOrderGateway clientOrderGateway = new Business.Gateway.ClientOrderGateway();
                YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrdersByMasterAccessionNo(accessionOrder.MasterAccessionNo);
                if (clientOrderCollection.Count == 1)
                {                                        
					Login.Receiving.AccessionOrderPath accessionOrderPath = new Login.Receiving.AccessionOrderPath(accessionOrder, clientOrderCollection[0], PageNavigationModeEnum.Standalone);
					accessionOrderPath.Start();
				}
                else
                {
                    MessageBox.Show("We are not able to show the Report Order Dialog for ReportNo: " + reportNo);
                }
            }
            else
            {
                MessageBox.Show("We are not able to show the Report Order Dialog for ReportNo: " + reportNo);
            }
        }

        private void ButtonOpenDocumentFolder_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_FlowUI.AccessionOrder != null)
            {
				YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.ReportNo);
				string folderPath = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser);
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("Explorer.exe", folderPath);
                p.StartInfo = info;
                p.Start();
            }
        }

        private void ButtonOpenFC500Folder_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("Explorer.exe", @"\\10.1.1.136\PDF");
            p.StartInfo = info;
            p.Start();
        }

        private void ButtonOpenFGalliosFolder_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("Explorer.exe", @"\\10.1.1.74\PDF");
            p.StartInfo = info;
            p.Start();
        }

        private void ButtonReportOrder_Click(object sender, RoutedEventArgs e)
        {            
        	if(this.m_FlowUI.AccessionOrder != null)
            {
                YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrdersByMasterAccessionNo(this.m_FlowUI.AccessionOrder.MasterAccessionNo);
                if (clientOrderCollection.Count != 0)
                {
                    Login.Receiving.AccessionOrderPath accessionOrderPath = new Login.Receiving.AccessionOrderPath(this.m_FlowUI.AccessionOrder, clientOrderCollection[0], PageNavigationModeEnum.Standalone);
                    accessionOrderPath.Start();
                    this.m_FlowUI.GetAccessionOrder(this.m_FlowUI.PanelSetOrderLeukemiaLymphoma.ReportNo, this.m_FlowUI.AccessionOrder.MasterAccessionNo);
                }
                else
                {
                    MessageBox.Show("No Client Order was found.  Please contact IT.");
                }
            }        	
        }

        private void ListICDCode_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (this.ListViewICDCodes.SelectedItem != null)
            {
                ListBox listBox = (ListBox)sender;
                XmlElement element = (XmlElement)listBox.SelectedItem;
                string icd9Code = element.GetAttribute("ICD9");
                string icd10Code = element.GetAttribute("ICD10");
                this.m_FlowUI.AddICD9Code(icd9Code, icd10Code);
            }            
        }
    }
}