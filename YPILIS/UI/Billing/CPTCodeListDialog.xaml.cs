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
    /// Interaction logic for CPTCodeListDialog.xaml
    /// </summary>
    public partial class CPTCodeListDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_CptCodeString;

        public CPTCodeListDialog()
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

        public YellowstonePathology.Business.Billing.Model.CptCodeCollection CptCodeCollection
        {
            get { return YellowstonePathology.Store.AppDataStore.Instance.CPTCodeCollection; }
        }

        public string CptCodeString
        {
            get { return this.m_CptCodeString; }
            set { this.m_CptCodeString = value; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ListBoxCPTCodes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ListBoxCPTCodes.SelectedItem != null)
            {
                YellowstonePathology.Business.Billing.Model.CptCode cptCode = (YellowstonePathology.Business.Billing.Model.CptCode)this.ListBoxCPTCodes.SelectedItem;
                CPTCodeEditDialog dlg = new CPTCodeEditDialog(cptCode);
                bool? dialogResult = dlg.ShowDialog();
                if (dialogResult.HasValue && dialogResult.Value == true)
                {
                    YellowstonePathology.Store.AppDataStore.Instance.CPTCodeCollection.Load();
                    this.NotifyPropertyChanged("CptCodeCollection");
                }
            }
        }

        private void ButtonNewCode_Click(object sender, RoutedEventArgs e)
        {
            CPTCodeEditDialog dlg = new CPTCodeEditDialog(null);
            bool? dialogResult = dlg.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                YellowstonePathology.Store.AppDataStore.Instance.CPTCodeCollection.Load();
                this.NotifyPropertyChanged("CptCodeCollection");
            }
        }

        private void ListBoxCPTCodes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.ListBoxCPTCodes.SelectedItem != null)
            {
                YellowstonePathology.Business.Billing.Model.CptCode cptCode = (YellowstonePathology.Business.Billing.Model.CptCode)this.ListBoxCPTCodes.SelectedItem;
                this.m_CptCodeString = cptCode.ToJSON();
                NotifyPropertyChanged("CptCodeString");
            }
        }
    }
}
