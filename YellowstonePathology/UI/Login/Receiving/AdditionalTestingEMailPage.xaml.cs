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

namespace YellowstonePathology.UI.Login.Receiving
{
    /// <summary>
    /// Interaction logic for AdditionalTestingEMailPage.xaml
    /// </summary>
    public partial class AdditionalTestingEMailPage : UserControl, INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;
        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;        

        public AdditionalTestingEMailPage(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_PanelSetOrder = panelSetOrder;
            this.m_AccessionOrder = accessionOrder;            

            if (string.IsNullOrEmpty(this.m_PanelSetOrder.AdditionalTestingEmailAddress) == true)
            {
                YellowstonePathology.Business.Domain.Physician physician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(this.m_AccessionOrder.PhysicianId);
                this.m_PanelSetOrder.AdditionalTestingEmailAddress = physician.PublishNotificationEmailAddress;
            }

            if(string.IsNullOrEmpty(this.m_PanelSetOrder.AdditionalTestingEmailMessage) == true)
            {
                this.m_PanelSetOrder.AdditionalTestingEmailMessage = "Additional Testing is being performed.  Use YPI Connect to see details." + Environment.NewLine +
                    Environment.NewLine + Environment.NewLine + "If you don't have access to YPI Connect please call us at (406)238-6360.";
            }

            InitializeComponent();

            DataContext = this;

            Loaded += AdditionalTestingEMailPage_Loaded;
            Unloaded += AdditionalTestingEMailPage_Unloaded;
        }

        private void AdditionalTestingEMailPage_Loaded(object sender, RoutedEventArgs e)
        {
             
        }

        private void AdditionalTestingEMailPage_Unloaded(object sender, RoutedEventArgs e)
        {
             
        }

        public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }        

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Back != null) this.Back(this, new EventArgs());
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.Next != null) this.Next(this, new EventArgs());
        }

        private void HyperLinkSendEmail_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.m_PanelSetOrder.AdditionalTestingEmailAddress) == false)
            {
                if (string.IsNullOrEmpty(this.m_PanelSetOrder.AdditionalTestingEmailMessage) == false)
                {
                    YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
                    YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(this.m_PanelSetOrder.PanelSetId);
                    string subject = "Additional Testing has been ordered: " + panelSet.PanelSetName;

                    System.Net.Mail.MailAddress from = new System.Net.Mail.MailAddress("Results@YPII.com");                    
                    System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress("sid.harder@YPII.com");                    

                    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(from, to);
                    message.Subject = subject;
                    message.Body = this.m_PanelSetOrder.AdditionalTestingEmailMessage;                    

                    this.m_PanelSetOrder.AdditionalTestingEmailSent = true;
                    this.m_PanelSetOrder.TimeAdditionalTestingEmailSent = DateTime.Now;
                    this.m_PanelSetOrder.AdditionalTestingEmailSentBy = YellowstonePathology.Business.User.SystemIdentity.Instance.User.UserName;

                    this.NotifyPropertyChanged(string.Empty);

                    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.111");

                    Uri uri = new Uri("http://tempuri.org/");
                    System.Net.ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
                    System.Net.NetworkCredential credential = credentials.GetCredential(uri, "Basic");

                    client.Credentials = credential;
                    client.Send(message);
                }
                else
                {
                    MessageBox.Show("The Email may not be sent without a message.", "Missing Message", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("The Email may not be sent without an address.", "Missing Address", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
