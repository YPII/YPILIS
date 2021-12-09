using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI.WebService
{
    /// <summary>
    /// Interaction logic for WebServiceAccountSelectionDialog.xaml
    /// </summary>
    public partial class WebServiceAccountSelectionDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<Business.WebService.WebServiceAccountView> m_WebServiceAccountViewList;
        private List<Business.WebService.WebServiceAccountView> m_LimitedWebServiceAccountViewList;

        private Business.WebService.WebServiceAccount m_CopiedWebServiceAccount;

        public WebServiceAccountSelectionDialog()
        {
            this.m_WebServiceAccountViewList = Business.Gateway.WebServiceGateway.GetWebServiceAccountViewList();
            this.m_LimitedWebServiceAccountViewList = this.m_WebServiceAccountViewList;
            DataContext = this;

            InitializeComponent();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public List<YellowstonePathology.Business.WebService.WebServiceAccountView> LimitedWebServiceAccountViewList
        {
            get { return this.m_LimitedWebServiceAccountViewList; }
        }

        private void ListViewWebServiceAccounts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(this.ListViewWebServiceAccounts.SelectedItem != null)
            {
                YellowstonePathology.Business.WebService.WebServiceAccountView webServiceAccountView = (YellowstonePathology.Business.WebService.WebServiceAccountView)this.ListViewWebServiceAccounts.SelectedItem;
                WebServiceAccountEditDialog dlg = new WebService.WebServiceAccountEditDialog(webServiceAccountView.WebServiceAccountId);
                dlg.ShowDialog();
                this.m_WebServiceAccountViewList = Business.Gateway.WebServiceGateway.GetWebServiceAccountViewList();
                this.RefreshLimitedWebServiceAccountViewList();
            }
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            WebServiceAccountEditDialog dlg = new WebService.WebServiceAccountEditDialog(0);
            dlg.ShowDialog();
            this.m_WebServiceAccountViewList = Business.Gateway.WebServiceGateway.GetWebServiceAccountViewList();
            this.RefreshLimitedWebServiceAccountViewList();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {            
            //YellowstonePathology.Business.Gateway.WebServiceGateway.UpDateSqlServerFromMySQL();
            //MessageBox.Show("MS Sql Server Updated from MySql tables WebServiceAccount and WebServiceAccountClient.");

            /*
            foreach(Business.WebService.WebServiceAccountView accountView in this.ListViewWebServiceAccounts.SelectedItems)
            {
                Business.Client.Model.ClientGroupClientCollection clientGroupClientCollection = Business.Gateway.PhysicianClientGateway.GetClientGroupClientCollectionByClientId(accountView.PrimaryClientId.ToString());
                if(clientGroupClientCollection.Count > 0)
                {                    
                    FixWebServiceAccount(clientGroupClientCollection, accountView.WebServiceAccountId);                    
                }
                else
                {
                   FixWebServiceAccount(accountView.PrimaryClientId, accountView.WebServiceAccountId);
                }                
            }
            */
        }

        private void FixWebServiceAccount(int clientId, int webServiceAccountId)
        {
            Business.WebService.WebServiceAccount account = Business.Persistence.DocumentGateway.Instance.PullWebServiceAccount(webServiceAccountId, this);            
            account.WebServiceAccountClientCollection.Clear();

            Business.WebService.WebServiceAccountClient newWebServiceAccountClient = new Business.WebService.WebServiceAccountClient();
            int id = Business.Gateway.WebServiceGateway.GetNextWebServiceAccountClientId();
            newWebServiceAccountClient.WebServiceAccountClientId = id;
            newWebServiceAccountClient.WebServiceAccountId = webServiceAccountId;
            newWebServiceAccountClient.ClientId = clientId;
            account.WebServiceAccountClientCollection.Add(newWebServiceAccountClient);            
            Business.Persistence.DocumentGateway.Instance.Push(this);
        }

        private void FixWebServiceAccount(Business.Client.Model.ClientGroupClientCollection clientGroupClientCollection, int webServiceAccountId)
        {
            Business.WebService.WebServiceAccount account = Business.Persistence.DocumentGateway.Instance.PullWebServiceAccount(webServiceAccountId, this);
            account.WebServiceAccountClientCollection.Clear();
            int id = Business.Gateway.WebServiceGateway.GetNextWebServiceAccountClientId();

            foreach (Business.Client.Model.ClientGroupClient clientGroupClient in clientGroupClientCollection)
            {
                Business.WebService.WebServiceAccountClient newWebServiceAccountClient = new Business.WebService.WebServiceAccountClient();                
                newWebServiceAccountClient.WebServiceAccountClientId = id;
                newWebServiceAccountClient.WebServiceAccountId = webServiceAccountId;
                newWebServiceAccountClient.ClientId = clientGroupClient.ClientId;
                account.WebServiceAccountClientCollection.Add(newWebServiceAccountClient);
                id += 1;
            }
            
            Business.Persistence.DocumentGateway.Instance.Push(this);
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewWebServiceAccounts.SelectedItem != null)
            {
                YellowstonePathology.Business.WebService.WebServiceAccountView webServiceAccountView = (YellowstonePathology.Business.WebService.WebServiceAccountView)this.ListViewWebServiceAccounts.SelectedItem;
                YellowstonePathology.Business.WebService.WebServiceAccount webServiceAccount = Business.Persistence.DocumentGateway.Instance.PullWebServiceAccount(webServiceAccountView.WebServiceAccountId, this);
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(webServiceAccount, this);
                this.m_WebServiceAccountViewList = Business.Gateway.WebServiceGateway.GetWebServiceAccountViewList();
                this.RefreshLimitedWebServiceAccountViewList();
            }
        }

        private void TextBoxSearchName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (this.TextBoxSearchName.Text.Length > 0)
                {
                    this.m_LimitedWebServiceAccountViewList = this.GetViewsWithMatchingText(this.TextBoxSearchName.Text);
                    this.NotifyPropertyChanged("LimitedWebServiceAccountViewList");
                }
            }
        }

        private void ButtonClearSearch_Click(object sender, RoutedEventArgs e)
        {
            this.TextBoxSearchName.Text = string.Empty;
            this.RefreshLimitedWebServiceAccountViewList();
        }

        private void RefreshLimitedWebServiceAccountViewList()
        {
            if (string.IsNullOrEmpty(this.TextBoxSearchName.Text) == true)
            {
                this.m_LimitedWebServiceAccountViewList = this.m_WebServiceAccountViewList;
            }
            else
            {
                this.m_LimitedWebServiceAccountViewList = this.GetViewsWithMatchingText(this.TextBoxSearchName.Text);
            }
            this.NotifyPropertyChanged("LimitedWebServiceAccountViewList");
        }

        public List<YellowstonePathology.Business.WebService.WebServiceAccountView> GetViewsWithMatchingText(string searchText)
        {
            List<YellowstonePathology.Business.WebService.WebServiceAccountView> result = new List<YellowstonePathology.Business.WebService.WebServiceAccountView>();

            string upper = searchText.ToUpper();
            foreach (YellowstonePathology.Business.WebService.WebServiceAccountView view in this.m_WebServiceAccountViewList)
            {
                string matchUpper = view.DisplayName.ToUpper();
                if (matchUpper.StartsWith(upper))
                {
                    result.Add(view);
                }
            }
            return result;
        }

        private void MenuItemCopyClients_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListViewWebServiceAccounts.SelectedItem != null)
            {
                Business.WebService.WebServiceAccountView webServiceAccountView = (Business.WebService.WebServiceAccountView)this.ListViewWebServiceAccounts.SelectedItem;
                this.m_CopiedWebServiceAccount = Business.Persistence.DocumentGateway.Instance.PullWebServiceAccount(webServiceAccountView.WebServiceAccountId, this);
                MessageBox.Show($"The web service account for {this.m_CopiedWebServiceAccount.DisplayName} has been copied to the clipboard.");
            }
        }

        private void MenuItemPasteClients_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_CopiedWebServiceAccount != null)
            {
                foreach(Business.WebService.WebServiceAccountView webServiceAccountView in this.ListViewWebServiceAccounts.SelectedItems)
                {                    
                    Business.WebService.WebServiceAccount thisAccount = Business.Persistence.DocumentGateway.Instance.PullWebServiceAccount(webServiceAccountView.WebServiceAccountId, this);                    
                    thisAccount.CopyClients(this.m_CopiedWebServiceAccount);
                    Business.Persistence.DocumentGateway.Instance.Push(this);                    
                }
                MessageBox.Show("All done.");
            }
            else
            {
                MessageBox.Show("You must copy a Web Service Account to the clipboard before performing this action.");
            }
        }
    }
}
