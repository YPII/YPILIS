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

namespace YellowstonePathology.UI.Surgical
{
    /// <summary>
    /// Interaction logic for PapCorrelationPage.xaml
    /// </summary>
    public partial class PapCorrelationPage : UserControl, INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;
        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;
        public delegate void CloseEventHandler(object sender, EventArgs e);
        public event CloseEventHandler Close;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder m_SurgicalTestOrder;
        private YellowstonePathology.Business.Patient.Model.PatientHistoryList m_PatientHistoryList;
        private string m_PageHeaderText;
        private System.Windows.Visibility m_BackButtonVisibility;
        private System.Windows.Visibility m_NextButtonVisibility;

        public PapCorrelationPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder,
            System.Windows.Visibility backButtonVisibility,
            System.Windows.Visibility nextButtonVisibility)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_SurgicalTestOrder = surgicalTestOrder;
            this.m_BackButtonVisibility = backButtonVisibility;
            this.m_NextButtonVisibility = nextButtonVisibility;
            this.m_PageHeaderText = "Pap Correlation Page";
            this.m_PatientHistoryList = new YellowstonePathology.Business.Patient.Model.PatientHistoryList();
            this.m_PatientHistoryList.SetFillCommandByAccessionNo(m_SurgicalTestOrder.ReportNo);
            this.m_PatientHistoryList.Fill();

            InitializeComponent();

            this.DataContext = this;

            Loaded += PapCorrelationPage_Loaded;
            Unloaded += PapCorrelationPage_Unloaded;
        }

        private void PapCorrelationPage_Loaded(object sender, RoutedEventArgs e)
        {
             
        }

        private void PapCorrelationPage_Unloaded(object sender, RoutedEventArgs e)
        {
             
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public bool OkToSaveOnNavigation(Type pageNavigatingTo)
        {
            return true;
        }

        public bool OkToSaveOnClose()
        {
            return true;
        }

        public void Save(bool releaseLock)
        {
            //YellowstonePathology.Business.Persistence.DocumentGateway.Instance.SubmitChanges(this.m_AccessionOrder, false);
        }

        public void UpdateBindingSources()
        {

        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            this.Next(this, new EventArgs());
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Back != null) this.Back(this, new EventArgs());
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close(this, new EventArgs());
        }

        public string PageHeaderText
        {
            get { return this.m_PageHeaderText; }
        }

        public YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder SurgicalTestOrder
        {
            get { return this.m_SurgicalTestOrder; }
        }

        public YellowstonePathology.Business.Patient.Model.PatientHistoryList PatientHistoryList
        {
            get { return this.m_PatientHistoryList; }
        }

        public System.Windows.Visibility BackButtonVisibility
        {
            get { return this.m_BackButtonVisibility; }
        }

        public System.Windows.Visibility NextButtonVisibility
        {
            get { return this.m_NextButtonVisibility; }
        }

        public System.Windows.Visibility CloseButtonVisibility
        {
            get
            {
                if (this.m_NextButtonVisibility == System.Windows.Visibility.Hidden)
                {
                    return System.Windows.Visibility.Visible;
                }
                return System.Windows.Visibility.Hidden;
            }
        }

        private void ListViewAccessions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(this.ListViewAccessions.SelectedItem != null)
            {
                this.m_SurgicalTestOrder.PapCorrelationAccessionNo = ((YellowstonePathology.Business.Patient.Model.PatientHistoryListItem)this.ListViewAccessions.SelectedItem).ReportNo;
            }
        }
    }
}
