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

namespace YellowstonePathology.UI.Login
{
    /// <summary>
    /// Interaction logic for DeleteAccessionLookupPage.xaml
    /// </summary>
    public partial class DeleteAccessionLookupPage : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void NextEventHandler(object sender, CustomEventArgs.MasterAccessionNoReturnEventArgs e);
        public event NextEventHandler Next;
        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

        private YellowstonePathology.Business.Search.ReportSearchList m_ReportSearchList;
        private string m_PageHeaderText = "Lookup Accession to Delete";

        public DeleteAccessionLookupPage()
        {
            InitializeComponent();

            DataContext = this;
            Loaded += DeleteAccessionLookupPage_Loaded;
        }

        private void DeleteAccessionLookupPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.TextBoxMasterAccessionNo.Focus();
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
            this.Back(this, new EventArgs());
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListViewAccessionOrders.SelectedItem != null)
            {
                Business.Search.ReportSearchItem reportSearchItem = (Business.Search.ReportSearchItem)this.ListViewAccessionOrders.SelectedItem;
                this.Next(this, new CustomEventArgs.MasterAccessionNoReturnEventArgs(reportSearchItem.MasterAccessionNo));
            }
            else
            {
                MessageBox.Show("Select an item.");
            }
        }

        public string PageHeaderText
        {
            get { return this.m_PageHeaderText; }
        }

        public YellowstonePathology.Business.Search.ReportSearchList ReportSearchList
        {
            get { return this.m_ReportSearchList; }
        }

        private void TextBoxMasterAccessionNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.DoSearch();
            }
        }

        public void DoSearch()
        {
            if (this.TextBoxMasterAccessionNo.Text.Length >= 1)
            {
                Surgical.TextSearchHandler textSearchHandler = new Surgical.TextSearchHandler(this.TextBoxMasterAccessionNo.Text);
                object textSearchObject = textSearchHandler.GetSearchObject();
                if (textSearchObject is YellowstonePathology.Business.ReportNo)
                {
                    YellowstonePathology.Business.ReportNo reportNo = (YellowstonePathology.Business.ReportNo)textSearchObject;
                    this.GetReportSearchListByReportNo(reportNo.Value);
                }
                else if (textSearchObject is YellowstonePathology.Business.MasterAccessionNo)
                {
                    YellowstonePathology.Business.MasterAccessionNo masterAccessionNo = (YellowstonePathology.Business.MasterAccessionNo)textSearchObject;
                    this.GetReportSearchListByMasterAccessionNo(masterAccessionNo.Value);
                }
                else if (textSearchObject is YellowstonePathology.Business.PatientName)
                {
                    YellowstonePathology.Business.PatientName patientName = (YellowstonePathology.Business.PatientName)textSearchObject;
                    this.GetReportSearchListByPatientName(patientName);
                }
            }
        }

        public void GetReportSearchListByReportNo(string reportNo)
        {
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByReportNo(reportNo);
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByMasterAccessionNo(string masterAccessionNo)
        {
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByMasterAccessionNo(masterAccessionNo);
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByPatientName(YellowstonePathology.Business.PatientName patientName)
        {
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByPatientName(new List<object>() { patientName.LastName, patientName.FirstName });
            this.NotifyPropertyChanged("ReportSearchList");
        }
    }
}
