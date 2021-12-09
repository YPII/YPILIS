using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.UI.Billing
{
    /// <summary>
    /// Interaction logic for ICDCodeEditDialog.xaml
    /// </summary>
    public partial class ICDCodeEditDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_ICDCodeString;
        private string m_HoldToCompareString;
        private YellowstonePathology.Business.Billing.Model.ICDCode m_ICDCode;

        public ICDCodeEditDialog(YellowstonePathology.Business.Billing.Model.ICDCode icdCode)
        {
            if(icdCode == null)
            {
                this.m_ICDCode = new Business.Billing.Model.ICDCode();
            }
            else
            {
                this.m_ICDCode = icdCode;
            }

            this.m_ICDCodeString = this.m_ICDCode.ToJSON();
            this.m_HoldToCompareString = this.m_ICDCode.ToJSON();
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

        public string Code
        {
            get { return this.m_ICDCode.Code; }
        }

        public string ICDCodeString
        {
            get { return this.m_ICDCodeString; }
            set { this.m_ICDCodeString = value; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Business.Rules.MethodResult result = this.CanSave();
            if (result.Success == true)
            {
                if (this.m_HoldToCompareString != this.m_ICDCodeString)
                {
                    YellowstonePathology.Business.Billing.Model.ICDCode codeToSave = Business.Billing.Model.ICDCodeFactory.FromJson(this.m_ICDCodeString);
                    codeToSave.Save();
                    this.DialogResult = true;
                }
                else
                {
                    this.DialogResult = false;
                }

                this.Close();
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private Business.Rules.MethodResult CanSave()
        {
            Business.Rules.MethodResult result = Business.Helper.JSONHelper.IsValidJSONString(this.m_ICDCodeString);

            if (result.Success == true)
            {
                JObject jObject = JsonConvert.DeserializeObject<JObject>(this.m_ICDCodeString);
                if (jObject["code"] == null)
                {
                    result.Success = false;
                    result.Message = "The Code must be present.";
                }
                else
                {
                    string codeToCompare = jObject["code"].ToString();
                    if (string.IsNullOrEmpty(codeToCompare) == true)
                    {
                        result.Success = false;
                        result.Message = "The Code must be present.";
                    }
                }
            }

            return result;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_HoldToCompareString != this.m_ICDCodeString)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Do you  want to save the changes?", "Save Changes", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.No)
                {
                    this.DialogResult = false;
                    this.Close();
                }
            }
            else
            {
                this.DialogResult = false;
                this.Close();
            }
        }
    }
}
