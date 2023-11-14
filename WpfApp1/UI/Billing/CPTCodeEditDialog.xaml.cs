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
    /// Interaction logic for CPTCodeEditDialog.xaml
    /// </summary>
    public partial class CPTCodeEditDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_CptCodeString;
        private string m_HoldToCompareString;
        private YellowstonePathology.Business.Billing.Model.CptCode m_CptCode;

        public CPTCodeEditDialog(YellowstonePathology.Business.Billing.Model.CptCode cptCode)
        {
            if (cptCode == null)
            {
                this.m_CptCode = new Business.Billing.Model.CptCode();
            }
            else
            {
                this.m_CptCode = cptCode;
            }

            this.m_CptCodeString = this.m_CptCode.ToJSON();
            this.m_HoldToCompareString = this.m_CptCode.ToJSON();
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
            get { return this.m_CptCode.Code; }
            set { this.m_CptCode.Code = value; }
        }

        public string CptCodeString
        {
            get { return this.m_CptCodeString; }
            set { this.m_CptCodeString = value; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Business.Rules.MethodResult result = this.CanSave();
            if (result.Success == true)
            {
                if (this.m_HoldToCompareString != this.m_CptCodeString)
                {
                    YellowstonePathology.Business.Billing.Model.CptCode codeToSave = Business.Billing.Model.CptCodeFactory.FromJson(this.m_CptCodeString);
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
            Business.Rules.MethodResult result = Business.Helper.JSONHelper.IsValidJSONString(this.m_CptCodeString);

            if (result.Success == true)
            {
                JObject jObject = JsonConvert.DeserializeObject<JObject>(this.m_CptCodeString);
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
                    else
                    {
                        if (string.IsNullOrEmpty(this.m_CptCode.Code) == true && YellowstonePathology.Store.AppDataStore.Instance.CPTCodeCollection.Exists(codeToCompare) == true)
                        {
                            result.Success = false;
                            result.Message = "The Code being added already exists.";
                        }
                    }
                }
            }

            return result;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_HoldToCompareString != this.m_CptCodeString)
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
