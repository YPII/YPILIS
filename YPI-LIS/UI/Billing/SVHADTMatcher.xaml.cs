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

namespace YellowstonePathology.UI.Billing
{
    /// <summary>
    /// Interaction logic for SVHADTMatcher.xaml
    /// </summary>
    public partial class SVHADTMatcher : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<Business.Billing.Model.AccessionListItem> m_AccessionList;
        private List<Business.Billing.Model.ADTListItem> m_ADTList;
        private DateTime m_PostDate;

        public SVHADTMatcher()
        {
            this.m_PostDate = DateTime.Today;
            this.m_AccessionList = Business.Gateway.AccessionOrderGateway.GetSVHNotPosted();
            InitializeComponent();
            this.DataContext = this;
        }

        public DateTime PostDate
        {
            get { return this.m_PostDate; }
            set
            {
                if(this.m_PostDate != value)
                {
                    this.m_PostDate = value;
                    this.NotifyPropertyChanged("PostDate");
                }
            }
        }

        public List<Business.Billing.Model.AccessionListItem> AccessionList
        {
            get { return this.m_AccessionList; }
        }

        public List<Business.Billing.Model.ADTListItem> ADTList
        {
            get { return this.m_ADTList; }
        }        

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ListViewAccession_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.ListViewAccession.SelectedItem != null)
            {
                Business.Billing.Model.AccessionListItem item = (Business.Billing.Model.AccessionListItem)this.ListViewAccession.SelectedItem;
                this.m_ADTList = Business.Gateway.AccessionOrderGateway.GetADTList(item.PFirstName, item.PLastName, item.PBirthdate.Value);
                this.NotifyPropertyChanged("ADTList");
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonMatch_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListViewAccession.SelectedItem != null)
            {
                if (this.ListViewADT.SelectedItem != null)
                {
                    Business.Billing.Model.AccessionListItem aoItem = (Business.Billing.Model.AccessionListItem)this.ListViewAccession.SelectedItem;
                    Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(aoItem.MasterAccessionNo, this);
                    Business.Test.PanelSetOrder pso = ao.PanelSetOrderCollection.GetPanelSetOrder(aoItem.ReportNo);

                    Business.Billing.Model.ADTListItem adtItem = (Business.Billing.Model.ADTListItem)this.ListViewADT.SelectedItem;
                    if (adtItem.MedicalRecord.StartsWith("V") == true)
                    {
                        foreach (Business.Test.PanelSetOrderCPTCodeBill psocb in pso.PanelSetOrderCPTCodeBillCollection)
                        {
                            if (psocb.BillTo == "Client")
                            {
                                psocb.MedicalRecord = adtItem.MedicalRecord;
                                psocb.Account = adtItem.Account;
                            }

                            if (psocb.PostDate.HasValue == false)
                                psocb.PostDate = this.m_PostDate;
                        }

                        Business.Persistence.DocumentGateway.Instance.Push(ao, this);
                        this.m_AccessionList = Business.Gateway.AccessionOrderGateway.GetSVHNotPosted();
                        this.m_ADTList = new List<Business.Billing.Model.ADTListItem>();
                        this.NotifyPropertyChanged(string.Empty);
                    }                
                    else
                    {
                        MessageBox.Show("The MRN does not start with a V.");
                    }
                    
                }
                else
                {
                    MessageBox.Show("You must first select an ADT item to Match.");
                }
            }
            else
            {
                MessageBox.Show("You must first select and Accession Order to Match.");
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.PostDate = this.m_PostDate.AddDays(-1);
            this.NotifyPropertyChanged("PostDate");
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            this.PostDate = this.m_PostDate.AddDays(1);
            this.NotifyPropertyChanged("PostDate");
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.m_ADTList = new List<Business.Billing.Model.ADTListItem>();
            this.m_AccessionList = Business.Gateway.AccessionOrderGateway.GetSVHNotPosted();
            this.NotifyPropertyChanged("AccessionList");
            this.NotifyPropertyChanged("ADTList");
        }

        private void ButtonMatchList_Click(object sender, RoutedEventArgs e)
        {
            this.MatchAccessionOrdersToADT(this.m_PostDate);
        }

        private void MatchAccessionOrdersToADT(DateTime postDate)
        {
            List<Business.Billing.Model.AccessionListItem> accessionList = Business.Gateway.AccessionOrderGateway.GetSVHNotPosted();
            foreach (Business.Billing.Model.AccessionListItem accessionListItem in accessionList)
            {
                List<Business.Billing.Model.ADTListItem> adtList = Business.Gateway.AccessionOrderGateway.GetADTList(accessionListItem.PFirstName, accessionListItem.PLastName, accessionListItem.PBirthdate.Value);

                foreach (Business.Billing.Model.ADTListItem adtItem in adtList)
                {
                    DateTime received = DateTime.Parse(adtItem.DateReceived.ToShortDateString());                    
                    int daysDiff = (int)(postDate - received).TotalDays;
                    if (daysDiff <= 3)
                    {
                        if (adtItem.MedicalRecord.StartsWith("V") == true)
                        {
                            Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(accessionListItem.MasterAccessionNo, this);
                            Business.Test.PanelSetOrder pso = ao.PanelSetOrderCollection.GetPanelSetOrder(accessionListItem.ReportNo);

                            foreach (Business.Test.PanelSetOrderCPTCodeBill psocb in pso.PanelSetOrderCPTCodeBillCollection)
                            {
                                if (psocb.BillTo == "Client")
                                {
                                    psocb.MedicalRecord = adtItem.MedicalRecord;
                                    psocb.Account = adtItem.Account;
                                }

                                if (psocb.PostDate.HasValue == false)
                                    psocb.PostDate = this.m_PostDate;
                            }

                            Business.Persistence.DocumentGateway.Instance.Push(ao, this);
                            break;
                        }
                    }                                            
                }
            }
        }
    }
}
