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
using System.ComponentModel;

namespace YellowstonePathology.UI.Billing
{
    /// <summary>
    /// Interaction logic for ICDCodeListDialog.xaml
    /// </summary>
    public partial class ICDCodeListDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICDCodeListDialog()
        {
            InitializeComponent();

            DataContext = this;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public YellowstonePathology.Business.Billing.Model.ICDCodeCollection ICDCodeCollection
        {
            get { return YellowstonePathology.Business.Billing.Model.ICDCodeCollection.Instance; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ListBoxICDCodes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ListBoxICDCodes.SelectedItem != null)
            {
                YellowstonePathology.Business.Billing.Model.ICDCode icdCode = (YellowstonePathology.Business.Billing.Model.ICDCode)this.ListBoxICDCodes.SelectedItem;
                ICDCodeEditDialog dlg = new ICDCodeEditDialog(icdCode);
                bool? dialogResult = dlg.ShowDialog();
                if (dialogResult.HasValue && dialogResult.Value == true)
                {
                    YellowstonePathology.Business.Billing.Model.ICDCodeCollection.Refresh();
                    this.NotifyPropertyChanged("ICDCodeCollection");
                }
            }
        }

        private void ButtonNewCode_Click(object sender, RoutedEventArgs e)
        {
            ICDCodeEditDialog dlg = new ICDCodeEditDialog(null);
            bool? dialogResult = dlg.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                YellowstonePathology.Business.Billing.Model.ICDCodeCollection.Refresh();
                this.NotifyPropertyChanged("ICDCodeCollection");
            }
        }
    }
}
