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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for SearchWorkspace.xaml
    /// </summary>

    public partial class SearchWorkspace : System.Windows.Controls.UserControl
    {        
        private YellowstonePathology.Business.SearchUI m_Search;
        private UI.DocumentWorkspace m_DocumentViewer;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private MainWindowCommandButtonHandler m_MainWindowCommandButtonHandler;
        private System.Windows.Controls.TabItem m_Writer;

        public SearchWorkspace(MainWindowCommandButtonHandler mainWindowCommandButtonHandler, System.Windows.Controls.TabItem writer)
        {
            this.m_MainWindowCommandButtonHandler = mainWindowCommandButtonHandler;
            this.m_Writer = writer;

            this.m_SystemIdentity = YellowstonePathology.Business.User.SystemIdentity.Instance;
            this.m_Search = new Business.SearchUI();            
            this.m_DocumentViewer = new DocumentWorkspace();            

            InitializeComponent();

            this.DataContext = this.m_Search;
            this.TabItemDocumentViewer.Content = this.m_DocumentViewer;

            this.Loaded += new RoutedEventHandler(SearchWorkspace_Loaded);
            this.Unloaded += new RoutedEventHandler(SearchWorkspace_Unloaded);
        }

        private void SearchWorkspace_Loaded(object sender, RoutedEventArgs e)
        {
            this.m_MainWindowCommandButtonHandler.StartProviderDistributionPath += new MainWindowCommandButtonHandler.StartProviderDistributionPathEventHandler(MainWindowCommandButtonHandler_StartProviderDistributionPath);
        }

        private void SearchWorkspace_Unloaded(object sender, RoutedEventArgs e)
        {
            this.m_MainWindowCommandButtonHandler.StartProviderDistributionPath -= MainWindowCommandButtonHandler_StartProviderDistributionPath;
        }

        private void MainWindowCommandButtonHandler_StartProviderDistributionPath(object sender, EventArgs e)
        {            
            if (this.listViewCaseList.SelectedItem != null)
            {
                YellowstonePathology.Business.SearchListItem item = (YellowstonePathology.Business.SearchListItem)this.listViewCaseList.SelectedItem;
				YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(item.MasterAccessionNo, this.m_Writer);

                YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath providerDistributionPath =
                    new Login.FinalizeAccession.ProviderDistributionPath(item.ReportNo, accessionOrder,
                    System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed);
                providerDistributionPath.Start();
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(accessionOrder, this.m_Writer);
            }         
        }

        public void SetFocus()
        {
            this.textBoxSearchCriteria.Focus();
        }

        public void TextBoxSearchCriteria_KeyUp(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.Return)
            {
                this.DoSearch();
            }
        }

        public void ListViewCaseList_MouseLeftButtonUp(object sender, RoutedEventArgs args)
        {
            if (this.listViewCaseList.SelectedItem != null)
            {
                YellowstonePathology.Business.SearchListItem item = (YellowstonePathology.Business.SearchListItem)this.listViewCaseList.SelectedItem;
				this.m_Search.ResultList.SetFillByAccessionNo(item.ReportNo);
                this.m_Search.ResultList.Fill();

				this.listViewDocumentList.SelectionChanged -= ListViewDocumentList_SelectionChanged;
				this.m_Search.RefreshCaseDocumentCollection(item.ReportNo);
				this.listViewDocumentList.SelectedIndex = -1;
				this.listViewDocumentList.SelectionChanged += new SelectionChangedEventHandler(ListViewDocumentList_SelectionChanged);
                
                this.m_DocumentViewer.ClearContent();                
            }            
        }

		public void ListViewDocumentList_SelectionChanged(object sender, RoutedEventArgs args)
        {
            if (this.listViewDocumentList.SelectedItem != null)
            {
                YellowstonePathology.Business.Document.CaseDocument item = (YellowstonePathology.Business.Document.CaseDocument)this.listViewDocumentList.SelectedItem;
				this.m_DocumentViewer.ShowDocument(item);
                this.TabItemDocumentViewer.Focus();
            }
        }                

        public void ButtonSearch_Click(object sender, RoutedEventArgs args)
        {
            this.DoSearch();            
        }                          
        
        public void ComboBoxSort_SelectionChanged(object sender, RoutedEventArgs args)
        {            
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)comboBox.SelectedItem;
            if (item.Content != null)
            {                
                this.m_Search.Sort(item.Content.ToString());            
            }
        }

        public void DoSearch()
        {
            string patientName = this.textBoxSearchCriteria.Text;
            
            this.m_Search.ResultList.Clear();			
            this.m_DocumentViewer.ClearContent();			
            this.m_Search.PatientHistoryList.Clear();

            if (this.textBoxSearchCriteria.Text.Length >= 4)
            {                
                this.m_Search.SearchList.SearchType = this.textBoxSearchCriteria.Text.Substring(0, 2);
                this.m_Search.SearchList.SearchString = this.textBoxSearchCriteria.Text.Substring(3, this.textBoxSearchCriteria.Text.Length - 3);
                YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_Search.SearchList.SetFill();
                if(methodResult.Success == false)
                {
                    MessageBox.Show(methodResult.Message);
                }
            }            
        }

        public void TextBoxPhysicianLastName_TextChanged(object sender, RoutedEventArgs args)
        {
            if (this.TextBoxPhysicianLastName.Text.Length > 0)
            {
                this.m_Search.GetPhysicianClientCollection(this.TextBoxPhysicianLastName.Text);                  
            }			
		}        

        public void ListViewPhysicianClient_MouseLeftButtonUp(object sender, RoutedEventArgs args)
        {
            if (this.listViewPhysicianClient.SelectedItem != null)
            {
                YellowstonePathology.Business.Client.Model.PhysicianClient physicianClient = (YellowstonePathology.Business.Client.Model.PhysicianClient)this.listViewPhysicianClient.SelectedItem;
                this.textBoxSearchCriteria.Text = "PH " + physicianClient.PhysicianClientId;
                this.DoSearch();
            }
        }
        
        public void ContextMenuGetPatientHistory_Click(object sender, RoutedEventArgs args)
        {
            if (this.listViewCaseList.SelectedItem != null)
            {
                YellowstonePathology.Business.SearchListItem item = (YellowstonePathology.Business.SearchListItem)this.listViewCaseList.SelectedItem;
				this.m_Search.PatientHistoryList.SetFillCommandByAccessionNo(item.ReportNo);
                this.m_Search.PatientHistoryList.Fill();
            }
        }

        public void ListViewPatientHistoryViewDocument_MouseLeftButtonUp(object sender, RoutedEventArgs args)
        {
            if (this.listViewPatientHistoryCaseFileList.SelectedItem != null)
            {
                this.TabItemDocumentViewer.Focus();
                YellowstonePathology.Business.Document.CaseDocument item = (YellowstonePathology.Business.Document.CaseDocument)this.listViewPatientHistoryCaseFileList.SelectedItem;
				this.m_DocumentViewer.ShowDocument(item);
            }
        }

        public void ListViewPatientHistoryCaseList_MouseLeftButtonUp(object sender, RoutedEventArgs args)
        {
            if (this.listViewPatientHistory.SelectedItem != null)
            {                
                YellowstonePathology.Business.Patient.Model.PatientHistoryListItem item = (YellowstonePathology.Business.Patient.Model.PatientHistoryListItem)this.listViewPatientHistory.SelectedItem;				
				this.m_Search.RefreshPatientHistoryCaseDocumentCollection(item.ReportNo);
			}
        }        

        private void ContextMenuShowPatientHistoryDialog_Click(object sender, RoutedEventArgs e)
        {
            if (this.listViewCaseList.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.SearchListItem item = (YellowstonePathology.Business.SearchListItem)this.listViewCaseList.SelectedItem;
				YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(item.MasterAccessionNo, this.m_Writer);

                YellowstonePathology.UI.Common.CaseHistoryDialog caseHistoryDialog = new Common.CaseHistoryDialog(accessionOrder);
                caseHistoryDialog.ShowDialog();
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(accessionOrder, this.m_Writer);
            }
        }
	}
}