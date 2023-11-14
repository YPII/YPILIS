using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for SpecimenEditDialog.xaml
    /// </summary>
    public partial class SpecimenEditDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_SpecimenString;
        private string m_HoldToCompareString;
        private YellowstonePathology.Business.Specimen.Model.Specimen m_Specimen;

        public SpecimenEditDialog(YellowstonePathology.Business.Specimen.Model.Specimen specimen)
        {
            if(specimen == null)
            {
                this.m_Specimen = new Business.Specimen.Model.Specimen();
            }
            else
            {
                this.m_Specimen = specimen;
            }

            this.m_SpecimenString = this.m_Specimen.ToJSON();
            this.m_HoldToCompareString = this.m_Specimen.ToJSON();
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

        public string SpecimenId
        {
            get { return this.m_Specimen.SpecimenId; }
        }

        public string SpecimenString
        {
            get { return this.m_SpecimenString; }
            set { this.m_SpecimenString = value; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Business.Rules.MethodResult result = this.CanSave();
            if (result.Success == true)
            {
                if (this.m_HoldToCompareString != this.m_SpecimenString)
                {
                    YellowstonePathology.Business.Specimen.Model.Specimen specimenToSave = Business.Specimen.Model.SpecimenCollection.FromJSON(this.m_SpecimenString);
                    specimenToSave.Save();
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
            Business.Rules.MethodResult result = Business.Helper.JSONHelper.IsValidJSONString(this.m_SpecimenString);

            if (result.Success == true)
            {
                JObject jObject = JsonConvert.DeserializeObject<JObject>(this.m_SpecimenString);
                if (jObject["specimenId"] == null)
                {
                    result.Success = false;
                    result.Message = "The Specimen Id must be present.";
                }
                else
                {
                    string specimenIdToCompare = jObject["specimenId"].ToString();
                    if (string.IsNullOrEmpty(specimenIdToCompare) == true)
                    {
                        result.Success = false;
                        result.Message = "The Specimen Id must have a value.";
                    }
                }
            }

            return result;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_HoldToCompareString != this.m_SpecimenString)
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
