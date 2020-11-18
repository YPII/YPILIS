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

namespace YellowstonePathology.UI.WebService
{
    /// <summary>
    /// Interaction logic for WebServiceAccountClientEditDialog.xaml
    /// </summary>
    public partial class WebServiceAccountClientEditDialog : Window
    {
        private List<YellowstonePathology.Business.WebService.WebServiceClientView> m_WebServiceClientViews;
        private YellowstonePathology.Business.WebService.WebServiceAccount  m_WebServiceAccount;

        public WebServiceAccountClientEditDialog(YellowstonePathology.Business.WebService.WebServiceAccount webServiceAccount)
        {
            this.m_WebServiceAccount = webServiceAccount;
            this.m_WebServiceClientViews = YellowstonePathology.Business.Gateway.WebServiceGateway.GetWebServiceClientViews();

            InitializeComponent();

            this.DataContext = this;
            Loaded += WebServiceAccountClientEditDialog_Loaded;
        }

        private void WebServiceAccountClientEditDialog_Loaded(object sender, RoutedEventArgs e)
        {
            this.ListViewWebServiceClientViews.SelectedIndex = -1;
        }

        public List<YellowstonePathology.Business.WebService.WebServiceClientView> WebServiceClientViews
        {
            get { return this.m_WebServiceClientViews; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewWebServiceClientViews.SelectedItems.Count > 0)
            {
                int id = YellowstonePathology.Business.Gateway.WebServiceGateway.GetNextWebServiceAccountClientId();
                if(this.m_WebServiceAccount.WebServiceAccountClientCollection.Count > 0)
                {
                    int existingId = this.m_WebServiceAccount.WebServiceAccountClientCollection[this.m_WebServiceAccount.WebServiceAccountClientCollection.Count - 1].WebServiceAccountClientId;
                    if (existingId >= id) id = existingId + 1;
                }
                foreach (YellowstonePathology.Business.WebService.WebServiceClientView webServiceClientView in this.ListViewWebServiceClientViews.SelectedItems)
                {
                    if (this.m_WebServiceAccount.WebServiceAccountClientCollection.Exists(webServiceClientView.ClientId) == false)
                    {
                        YellowstonePathology.Business.WebService.WebServiceAccountClient webServiceAccountClient = new Business.WebService.WebServiceAccountClient();
                        webServiceAccountClient.WebServiceAccountClientId = id;
                        webServiceAccountClient.WebServiceAccountId = this.m_WebServiceAccount.WebServiceAccountId;
                        webServiceAccountClient.ClientId = webServiceClientView.ClientId;
                        webServiceAccountClient.ClientName = webServiceClientView.ClientName;
                        this.m_WebServiceAccount.WebServiceAccountClientCollection.Add(webServiceAccountClient);
                        id++;
                    }
                }
                this.DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("A client must be selected.");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }
    }
}
