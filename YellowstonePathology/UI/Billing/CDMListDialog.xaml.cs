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
    /// Interaction logic for CDMListDialog.xaml
    /// </summary>
    public partial class CDMListDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Billing.Model.CDM m_SelectedCDM;
        private List<string> m_CDMClients;

        public CDMListDialog()
        {
            this.m_CDMClients = new List<string>();
            this.m_CDMClients.Add("SVH");
            InitializeComponent();

            DataContext = this;
        }

        public YellowstonePathology.Business.Billing.Model.CDMCollection CDMCollection
        {
            get { return YellowstonePathology.Business.Billing.Model.CDMCollection.Instance; }
        }

        public YellowstonePathology.Business.Billing.Model.CDM SelectedCDM
        {
            get { return this.m_SelectedCDM; }
        }

        public List<string> CDMClients
        {
            get { return this.m_CDMClients; }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ListBoxCDM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.ListBoxCDM.SelectedItem != null)
            {
                this.m_SelectedCDM = (YellowstonePathology.Business.Billing.Model.CDM)this.ListBoxCDM.SelectedItem;
                NotifyPropertyChanged("SelectedCDM");
            }
        }

        private void ButtonNewCDM_Click(object sender, RoutedEventArgs e)
        {
            this.m_SelectedCDM = new Business.Billing.Model.CDM();
            NotifyPropertyChanged("SelectedCDM");
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {                     
            YellowstonePathology.Business.Rules.MethodResult result = this.CanSave();
            if(result.Success == true)
            {
                this.m_SelectedCDM.Save();
                YellowstonePathology.Business.Billing.Model.CDMCollection.Refresh();
                this.NotifyPropertyChanged("CDMCollection");
            }
            else
            {
                MessageBox.Show(result.Message);
            }            
        }

        private YellowstonePathology.Business.Rules.MethodResult CanSave()
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            result.Success = true;

            if (this.m_SelectedCDM != null)
            {
                if (string.IsNullOrEmpty(this.m_SelectedCDM.CDMCode) == true)
                {
                    result.Success = false;
                    result.Message = "Unable to save as there are no values.";
                }

                if(result.Success == true)
                {
                    if(string.IsNullOrEmpty(this.m_SelectedCDM.CPTCode) == true)
                    {
                        result.Success = false;
                        result.Message = "Unable to save as the CPT Code does not have a value.";
                    }
                }

                if (result.Success == true)
                {
                    if (string.IsNullOrEmpty(this.m_SelectedCDM.CDMClient) == true)
                    {
                        result.Success = false;
                        result.Message = "Unable to save as the Client does not have a value.";
                    }
                }
            }
            else
            {
                result.Success = false;
                result.Message = "Unable to save as there are no values.";
            }

            return result;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
