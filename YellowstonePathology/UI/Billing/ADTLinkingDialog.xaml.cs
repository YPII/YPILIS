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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace YellowstonePathology.UI.Billing
{
    /// <summary>
    /// Interaction logic for ADTLinkingDialog.xaml
    /// </summary>
    public partial class ADTLinkingDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Search.ReportSearchList m_ReportSearchList;
        private ObservableCollection<Business.HL7View.ADTMessage> m_ADTMessages;

        public ADTLinkingDialog()
        {
            InitializeComponent();
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByNoADT(DateTime.Today.AddDays(-1));
            this.DataContext = this;
        }

        public YellowstonePathology.Business.Search.ReportSearchList ReportSearchList
        {
            get { return this.m_ReportSearchList; }
        }

        public ObservableCollection<Business.HL7View.ADTMessage> ADTMessages
        {
            get { return this.m_ADTMessages; }
        }

        private void ListViewReportSearchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.ListViewReportSearchList.SelectedItem != null)
            {
                YellowstonePathology.Business.Search.ReportSearchItem reportSearchItem = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewReportSearchList.SelectedItem;
                this.m_ADTMessages = Business.HL7View.ADTMessages.GetADTByPatientNameDOB(reportSearchItem.PLastName, reportSearchItem.PFirstName, reportSearchItem.PBirthdate.Value);
                this.NotifyPropertyChanged("ADTMessages");
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
