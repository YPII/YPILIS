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
    /// Interaction logic for WebServiceAccountDialog.xaml
    /// </summary>
    public partial class WebServiceAccountEditDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.WebService.WebServiceAccount m_WebServiceAccount;
        private List<string> m_InitialPages;
        private List<string> m_DownloadFileTypes;

        public WebServiceAccountEditDialog(int webServiceAccountId)
        {
            if (webServiceAccountId == 0)
            {
                this.m_WebServiceAccount = new Business.WebService.WebServiceAccount();
                this.m_WebServiceAccount.SetDefaults();
            }
            else
            {
                this.m_WebServiceAccount = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullWebServiceAccount(webServiceAccountId, this);
            }

            this.m_InitialPages = new List<string>();
            this.m_InitialPages.Add("OrderBrowser");
            this.m_InitialPages.Add("ReportBrowser");
            this.m_InitialPages.Add("BillingBrowser");
            this.m_InitialPages.Add("FileUpload");
            this.m_InitialPages.Add("PathologyDashboard");

            this.m_DownloadFileTypes = new List<string>();
            this.m_DownloadFileTypes.Add("XPS");
            this.m_DownloadFileTypes.Add("TIF");

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

        public YellowstonePathology.Business.WebService.WebServiceAccount WebServiceAccount
        {
            get { return this.m_WebServiceAccount; }
        }

        public List<string> InitialPages
        {
            get { return this.m_InitialPages; }
        }

        public List<string> DownloadFileTypes
        {
            get { return this.m_DownloadFileTypes; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.CanSave();
            if (methodResult.Success == true)
            {
                if (this.m_WebServiceAccount.WebServiceAccountId == 0)
                {
                    if (this.IsNameInUse() == false)
                    {
                        int id = YellowstonePathology.Business.Gateway.WebServiceGateway.GetNextWebServiceAccountId();
                        this.m_WebServiceAccount.WebServiceAccountId = id;
                        foreach (YellowstonePathology.Business.WebService.WebServiceAccountClient webServiceAccountClient in this.m_WebServiceAccount.WebServiceAccountClientCollection)
                        {
                            if (webServiceAccountClient.WebServiceAccountId == 0) webServiceAccountClient.WebServiceAccountId = id;
                        }
                        YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(this.m_WebServiceAccount, this);
                    }
                    else
                    {
                        MessageBox.Show("The User Name already exists.");
                        return;
                    }
                }

                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);                
                Close();
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private YellowstonePathology.Business.Rules.MethodResult CanSave()
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            if(string.IsNullOrEmpty(this.m_WebServiceAccount.UserName) == true)
            {
                methodResult.Success = false;
                methodResult.Message = "A UserName is required" + Environment.NewLine;
            }

            if(string.IsNullOrEmpty(this.m_WebServiceAccount.Password) == true)
            {
                methodResult.Success = false;
                methodResult.Message += "A Password is required";
            }
            return methodResult;
        }

        private bool IsNameInUse()
        {
            Business.WebService.WebServiceAccountCollection webServiceAccountCollection = Business.Gateway.WebServiceGateway.GetWebServiceAccounts();
            bool result = webServiceAccountCollection.ExistsByUserName(this.m_WebServiceAccount.UserName);
            return result;
        }

        private void ButtonAddClient_Click(object sender, RoutedEventArgs e)
        {
            WebServiceAccountClientEditDialog dlg = new WebServiceAccountClientEditDialog(this.m_WebServiceAccount);
            bool? result = dlg.ShowDialog();
            if(result.HasValue && result.Value == true)
            {
                this.NotifyPropertyChanged("WebServiceAccountClientCollection");
            }
        }

        private void ButtonDeleteClient_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewWebServiceAccountClientViews.SelectedItems.Count > 0)
            {
                for (int idx = ListViewWebServiceAccountClientViews.SelectedItems.Count -1; idx > -1; idx--)
                {
                    YellowstonePathology.Business.WebService.WebServiceAccountClient webServiceAccountClient = (Business.WebService.WebServiceAccountClient)this.ListViewWebServiceAccountClientViews.SelectedItems[idx];
                    this.m_WebServiceAccount.WebServiceAccountClientCollection.Remove(webServiceAccountClient);
                    this.NotifyPropertyChanged("WebServiceAccountClientCollection");
                }
            }
        }

        private void PwGenerator_Click(object sender, RoutedEventArgs e)
        {
            string pw = YellowstonePathology.Business.WebService.PasswordGenerator.GeneratePassword(true, true, true, false, false, 8);
            while (!YellowstonePathology.Business.WebService.PasswordGenerator.PasswordIsValid(true, true, true, false, false, pw))
            {
                pw = YellowstonePathology.Business.WebService.PasswordGenerator.GeneratePassword(true, true, true, false, false, 8);
            }
            this.m_WebServiceAccount.Password = pw;
            this.NotifyPropertyChanged(string.Empty);
        }        

        private void HyperLinkImpersonateSid_Click(object sender, RoutedEventArgs e)
        {
            if(this.m_WebServiceAccount.WebServiceAccountId == 1091)
            {
                this.m_WebServiceAccount.SetAsYPI();
                
            }
            else
            {
                Business.WebService.WebServiceAccount sidHarder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullWebServiceAccount(1091, this);
                sidHarder.Impersonate(this.m_WebServiceAccount);                
            }

            Business.Persistence.DocumentGateway.Instance.Push(this);
            this.Close();
        }

        private void HyperLinkImpersonateKevin_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_WebServiceAccount.WebServiceAccountId == 1090)
            {
                this.m_WebServiceAccount.SetAsYPI();
            }
            else
            {
                Business.WebService.WebServiceAccount sidHarder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullWebServiceAccount(1090, this);
                sidHarder.Impersonate(this.m_WebServiceAccount);                
            }

            Business.Persistence.DocumentGateway.Instance.Push(this);
            this.Close();            
        }
    }
}
