using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
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
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for DailyDOHDistributionDialog.xaml
    /// </summary>
    public partial class DailyDOHDistributionDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.View.StVClientDOHReportViewCollection m_StVClientDOHReportViewCollection;
        private DateTime m_DateAdded;
        private string m_Client;
        private List<string> m_Clients;

        public DailyDOHDistributionDialog()
        {
            this.m_Clients = new List<string>();
            this.m_Clients.Add("SCL");
            this.m_Clients.Add("WYDOH");

            this.m_StVClientDOHReportViewCollection = new YellowstonePathology.Business.View.StVClientDOHReportViewCollection();
            this.DateAdded = DateTime.Today;
            this.Client = "SCL";

            InitializeComponent();
            this.Loaded += DailyDOHDistributionDialog_Loaded;

            this.DataContext = this;
        }

        private void DailyDOHDistributionDialog_Loaded(object sender, RoutedEventArgs e)
        {            
            this.GetDistributions(this.m_Client);
        }

        public DateTime DateAdded
        {
            get { return this.m_DateAdded; }
            set
            {
                if (this.m_DateAdded != value)
                {
                    this.m_DateAdded = value;                    
                    this.NotifyPropertyChanged("DateAdded");
                }
            }
        }

        public string Client
        {
            get { return this.m_Client; }
            set
            {
                if (this.m_Client != value)
                {
                    this.m_Client = value;
                    this.NotifyPropertyChanged("Client");
                }
            }
        }

        public List<String> Clients
        {
            get { return this.m_Clients; }
        }

        public YellowstonePathology.Business.View.StVClientDOHReportViewCollection StVClientDOHReportViewCollection
        {
            get { return this.m_StVClientDOHReportViewCollection; }
        }

        private void GetDistributions(string selectedClient)
        {            
            if(selectedClient == "SCL")
            {
                this.m_StVClientDOHReportViewCollection = Business.Gateway.ReportDistributionGateway.GetReportDistributionCollectionByDateTumorRegistryStVClients(this.m_DateAdded, this.m_DateAdded.AddHours(24));
                this.NotifyPropertyChanged("StVClientDOHReportViewCollection");
            }
            else if (selectedClient == "WYDOH")
            {
                this.m_StVClientDOHReportViewCollection = Business.Gateway.ReportDistributionGateway.GetReportDistributionCollectionByDateTumorRegistryWyoming(this.m_DateAdded, this.m_DateAdded.AddHours(24));
                this.NotifyPropertyChanged("StVClientDOHReportViewCollection");
            }            
        }

        private void ButtonDateBack_Click(object sender, RoutedEventArgs args)
        {
            this.DateAdded = this.m_DateAdded.AddDays(-1);
            this.GetDistributions(this.m_Client);
        }

        private void ButtonDateForward_Click(object sender, RoutedEventArgs args)
        {
            this.DateAdded = this.m_DateAdded.AddDays(1);
            this.GetDistributions(this.m_Client);
        }

        private void ButtonSendFax_Click(object sender, RoutedEventArgs args)
        {
            string message = string.Empty;
            if (this.StVClientDOHReportViewCollection.Count > 0)
            {
                foreach (YellowstonePathology.Business.View.StVClientDOHReportView view in this.StVClientDOHReportViewCollection)
                {
                    YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(view.ReportNo);
                    string tifCaseFileName = Business.Document.CaseDocument.GetCaseFileNameTif(orderIdParser);
                    if (File.Exists(tifCaseFileName) == true)
                    {
                        string faxNumber = "4062373672";
                        if (this.m_Client == "WYDOH")
                        {
                            faxNumber = "3077773419";
                        }

                        YellowstonePathology.Business.ReportDistribution.Model.FaxSubmission.Submit(faxNumber, view.ReportNo, tifCaseFileName, $"SCL DOH: {view.ReportNo}");
                        //YellowstonePathology.Business.ReportDistribution.Model.FaxSubmission.Submit("4062386361", view.ReportNo, tifCaseFileName);
                    }
                    else
                    {
                        message = message + view.ReportNo + ", ";
                    }
                }

                if (message.Length > 0)
                {
                    message = message.Substring(0, message.Length - 2);
                    MessageBox.Show("The report/s listed were not faxed as the corresponding file was not found." + Environment.NewLine + message);
                }
                else
                {
                    MessageBox.Show("Faxes sent.");
                }
            }
            else
            {
                MessageBox.Show("No Faxes to send.");
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ComboboxClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.IsLoaded)
            {
                string client = (string)this.ComboboxClient.SelectedItem;
                this.GetDistributions(client);
            }
        }
    }
}
