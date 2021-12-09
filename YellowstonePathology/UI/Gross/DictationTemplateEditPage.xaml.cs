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
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace YellowstonePathology.UI.Gross
{
    /// <summary>
    /// Interaction logic for DictationTemplateEditPage.xaml
    /// </summary>
    public partial class DictationTemplateEditPage : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DictationTemplate m_DictationTemplate;
        private string m_HoldToCompareString;

        public DictationTemplateEditPage(DictationTemplate dictationTemplate)
        {
            if (dictationTemplate == null)
            {
                this.m_DictationTemplate = new Gross.DictationTemplate();
            }
            else
            {
                this.m_DictationTemplate = dictationTemplate;
            }

            this.m_HoldToCompareString = this.m_DictationTemplate.ToJSON();
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

        public DictationTemplate DictationTemplate
        {
            get { return this.m_DictationTemplate; }
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.CanSave();
            if (methodResult.Success == true)
            {
                this.m_DictationTemplate.Save();
                this.DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private YellowstonePathology.Business.Rules.MethodResult CanSave()
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            if (string.IsNullOrEmpty(this.m_DictationTemplate.TemplateName) == true)
            {
                result.Success = false;
                result.Message = "A Dictation Template Name is required.";
            }
            else if(string.IsNullOrEmpty(this.m_DictationTemplate.Text) == true)
            {
                result.Success = false;
                result.Message = "The Dictation Template Text is required.";
            }
            else
            {
                if (string.IsNullOrEmpty(this.m_DictationTemplate.TemplateId) == true)
                {
                    string id = this.DetermineId();
                    if (string.IsNullOrEmpty(id) == false)
                    {
                        if (Gross.DictationTemplateCollection.Instance.Exists(id) == true)
                        {
                            result.Success = false;
                            result.Message = "The Dictation Template Id is not unique. Add a number or a consonent to the Name.";
                        }
                        else
                        {
                            this.m_DictationTemplate.TemplateId = id;
                        }
                    }
                }
            }

            if(result.Success == true)
            {
                result = Business.Helper.JSONHelper.IsValidJSONString(this.m_DictationTemplate.ToJSON());
            }
            return result;
        }

        private string DetermineId()
        {
            string lower = this.m_DictationTemplate.TemplateName.ToLower();
            char first = lower[0];
            char last = lower[lower.Length - 1];
            string pattern = @"[aeiou]";
            Regex regex = new Regex(pattern);
            string result = regex.Replace(lower, "");
            if (first != result[0]) result = first + result;
            if (last != result[result.Length - 1]) result = result + last;
            pattern = @"[)(/\\,\.\s -]";
            regex = new Regex(pattern);
            result = regex.Replace(result, "");
            return result;
        }

        private void ButtonAddSpecimen_Click(object sender, RoutedEventArgs e)
        {
            SpecimenSelectionPage dlg = new Gross.SpecimenSelectionPage(this.m_DictationTemplate.SpecimenCollection);
            dlg.ShowDialog();
            this.NotifyPropertyChanged(string.Empty);
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            string currentString = this.m_DictationTemplate.ToJSON();
            if (this.m_HoldToCompareString != currentString)
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

        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListViewSpecimen.SelectedItem != null)
            {

            }
        }
    }
}
