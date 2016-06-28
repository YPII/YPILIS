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
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI.Test
{
    /// <summary>
    /// Interaction logic for AcidWashOrdersDialog.xaml
    /// </summary>
    public partial class AcidWashOrdersDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Test.ThinPrepPap.AcidWashList m_AcidWashList;

        public AcidWashOrdersDialog()
        {
            this.m_AcidWashList = Business.Gateway.ReportSearchGateway.GetAcidWashList(DateTime.Today.AddMonths(-3));
            InitializeComponent();
            DataContext = this;            
        }        

        public Business.Test.ThinPrepPap.AcidWashList AcidWashList
        {
            get { return this.m_AcidWashList; }
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

        private void ListViewAcidWashList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewAcidWashList.SelectedItem != null)
            {
                Business.Test.ThinPrepPap.AcidWashListItem acidWashListItem = (Business.Test.ThinPrepPap.AcidWashListItem)this.ListViewAcidWashList.SelectedItem;
                AcidWashResultDialog acidWashResultDialog = new AcidWashResultDialog(acidWashListItem.MasterAccessionNo, acidWashListItem.ReportNo);
                acidWashResultDialog.ShowDialog();
                this.m_AcidWashList = Business.Gateway.ReportSearchGateway.GetAcidWashList(DateTime.Today.AddMonths(-3));
                this.NotifyPropertyChanged("AcidWashList");
            }
        }        
    }
}
