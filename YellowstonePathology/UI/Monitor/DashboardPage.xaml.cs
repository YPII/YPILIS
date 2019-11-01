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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.WebServices.Data;

namespace YellowstonePathology.UI.Monitor
{
    /// <summary>
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : UserControl, INotifyPropertyChanged, IMonitorPage
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Monitor.Model.BlockCountCollection m_BlockCountColletion;
        private int m_BlocksToTransfer;

        private Login.Receiving.LoginPageWindow m_LoginPageWindow;

        public DashboardPage()
        {                        
            InitializeComponent();
            this.DataContext = this;
        }                

        public void Refresh()
        {
            YellowstonePathology.Business.Gateway.AccessionOrderGateway.SetTodaysBlockCountRow();
            YellowstonePathology.Business.Gateway.AccessionOrderGateway.SetBillingsBlockCount();
            this.HandleBlockCountEmails();
            this.m_BlockCountColletion = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetMonitorBlockCount();
            this.m_BlocksToTransfer = this.m_BlockCountColletion.SetBlocksToTransfer();
            this.NotifyPropertyChanged("");
        }        

        public YellowstonePathology.Business.Monitor.Model.BlockCountCollection BlockCountCollection
        {
            get { return this.m_BlockCountColletion; }
        }

        public DateTime DashboardDate
        {
            get { return DateTime.Now; }
        }

        public int BlocksToTransfer
        {
            get { return this.m_BlocksToTransfer; }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ResultPathFactory_Finished(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }        

        public void HandleBlockCountEmails()
        {            
            ServicePointManager.ServerCertificateValidationCallback = CertificateValidationCallBack;
            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
            service.Credentials = new WebCredentials("ypiilab\\blockcount", "blockorama");

            service.TraceEnabled = true;
            service.TraceFlags = TraceFlags.All;

            service.AutodiscoverUrl("blockcount@ypii.com", RedirectionUrlValidationCallback);            
            ItemView view = new ItemView(50);
            view.PropertySet = new PropertySet(BasePropertySet.FirstClassProperties, ItemSchema.Subject, ItemSchema.DateTimeReceived);
            view.OrderBy.Add(ItemSchema.DateTimeReceived, SortDirection.Descending);
            view.Traversal = ItemTraversal.Shallow;
            FindItemsResults<Item> findResults = service.FindItems(WellKnownFolderName.Inbox, view);
                        
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"(^|\s*)(\d{1,3})");

            bool blockCountFound = false;
            foreach (Item mailItem in findResults.Items)
            {
                if (mailItem is EmailMessage)
                {
                    if(blockCountFound == false)
                    {
                        System.Text.RegularExpressions.Match match = regex.Match(mailItem.Subject);
                        if (match.Captures.Count != 0)
                        {
                            int blockCount = Convert.ToInt32(match.Value);                            
                            YellowstonePathology.Business.Gateway.AccessionOrderGateway.SetBozemanBlockCount(blockCount, DateTime.Parse(mailItem.DateTimeSent.ToShortDateString()));
                            blockCountFound = true;
                        }
                    }
                    mailItem.Delete(DeleteMode.MoveToDeletedItems);
                }
            }            
        }

        private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            // The default for the validation callback is to reject the URL.
            bool result = false;

            Uri redirectionUri = new Uri(redirectionUrl);

            // Validate the contents of the redirection URL. In this simple validation
            // callback, the redirection URL is considered valid if it is using HTTPS
            // to encrypt the authentication credentials. 
            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }
            return result;
        }

        private static bool CertificateValidationCallBack(
            object sender,
            System.Security.Cryptography.X509Certificates.X509Certificate certificate,
            System.Security.Cryptography.X509Certificates.X509Chain chain,
            System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            // If the certificate is a valid, signed certificate, return true.
            if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
            {
                return true;
            }

            // If there are errors in the certificate chain, look at each error to determine the cause.
            if ((sslPolicyErrors & System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors) != 0)
            {
                if (chain != null && chain.ChainStatus != null)
                {
                    foreach (System.Security.Cryptography.X509Certificates.X509ChainStatus status in chain.ChainStatus)
                    {
                        if ((certificate.Subject == certificate.Issuer) &&
                           (status.Status == System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.UntrustedRoot))
                        {
                            // Self-signed certificates with an untrusted root are valid. 
                            continue;
                        }
                        else
                        {
                            if (status.Status != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
                            {
                                // If there are any other errors in the certificate chain, the certificate is invalid,
                                // so the method returns false.
                                return false;
                            }
                        }
                    }
                }

                // When processing reaches this line, the only errors in the certificate chain are 
                // untrusted root errors for self-signed certificates. These certificates are valid
                // for default Exchange server installations, so return true.
                return true;
            }
            else
            {
                // In all other cases, return false.
                return false;
            }
        }
    }
}
