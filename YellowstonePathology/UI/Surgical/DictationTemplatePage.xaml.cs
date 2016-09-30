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
    public partial class DictationTemplatePage : UserControl, INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder m_SurgicalTestOrder;
        private YellowstonePathology.UI.Gross.DictationTemplateCollection m_DictationTemplateCollection;
        private YellowstonePathology.UI.Gross.DictationTemplate m_DictationTemplate;        
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.User.SystemUserCollection m_PathologistUsers;
        private YellowstonePathology.Business.User.UserPreference m_UserPreference;
        private string m_GrossDescription;

        public DictationTemplatePage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = systemIdentity;

            this.m_SurgicalTestOrder = (YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            this.m_DictationTemplateCollection = YellowstonePathology.UI.Gross.DictationTemplateCollection.GetAll();            
			this.m_PathologistUsers = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Pathologist, true);
			this.m_UserPreference = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference;

			InitializeComponent();
            this.Loaded += DictationTemplatePage_Loaded;

			DataContext = this;
		}
        
        public YellowstonePathology.Business.User.SystemUserCollection PathologistUsers
		{
			get { return this.m_PathologistUsers; }
		}
        
        public YellowstonePathology.Business.User.UserPreference UserPreference
		{
			get { return this.m_UserPreference; }
		}

        private void DictationTemplatePage_Loaded(object sender, RoutedEventArgs e)
        {
            if(this.m_AccessionOrder.SpecimenOrderCollection.Count != 0)
            {
                this.ListBoxSpecimenOrders.SelectedIndex = 0;
            }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public YellowstonePathology.UI.Gross.DictationTemplate DictationTemplate
        {
            get { return this.m_DictationTemplate; }            
        }  
        
        public string GrossDescription
        {
            get { return this.m_GrossDescription; }
            set
            {
                if(this.m_GrossDescription != value)
                {
                    this.m_GrossDescription = value;
                    this.NotifyPropertyChanged("GrossDescription");
                }
            }
        }      	                        

        private void ListBoxSpecimen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.m_GrossDescription = null;
            if(this.ListBoxSpecimenOrders.SelectedItem != null)
            {                                                              
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = (YellowstonePathology.Business.Specimen.Model.SpecimenOrder)this.ListBoxSpecimenOrders.SelectedItem;                    
                if(string.IsNullOrEmpty(specimenOrder.SpecimenId) == false)
                {
                    this.m_DictationTemplate = this.m_DictationTemplateCollection.GetTemplate(specimenOrder.SpecimenId);
                    
                    if(this.m_DictationTemplate is YellowstonePathology.UI.Gross.TemplateNotFound == false)
                    {
	                    this.m_GrossDescription = this.m_DictationTemplate.BuildResultText(specimenOrder, this.m_AccessionOrder, this.m_SystemIdentity);	                    	                    
	                    this.NotifyPropertyChanged(string.Empty);
	                    this.TextBoxGrossDescription.Focus();
	                    this.SelectNextInput(0);
                    }
                }                
            }

            this.NotifyPropertyChanged(string.Empty);
        }   
        
        private bool SelectNextInput(int startingPosition)
        {
            bool result = false;   
            if(startingPosition != this.TextBoxGrossDescription.Text.Length)
            {
                int positionOfNextLeftBracket = this.TextBoxGrossDescription.Text.IndexOf("[", startingPosition + 1);
                if (positionOfNextLeftBracket != -1)
                {
                    int positionOfNextRightBracket = this.TextBoxGrossDescription.Text.IndexOf("]", positionOfNextLeftBracket);
                    this.TextBoxGrossDescription.SelectionStart = positionOfNextLeftBracket;
                    this.TextBoxGrossDescription.SelectionLength = positionOfNextRightBracket - positionOfNextLeftBracket + 1;
                    result = true;
                }
            }                           
            return result;
        }    
        
        private void HyperLinkAddDicationToGross_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_SurgicalTestOrder.GrossX == "???") this.m_SurgicalTestOrder.GrossX = null;
            if(string.IsNullOrEmpty(this.m_SurgicalTestOrder.GrossX) == true)
            {
                this.m_SurgicalTestOrder.GrossX = this.m_GrossDescription;
            }
            else
            {
                this.m_SurgicalTestOrder.GrossX = this.m_SurgicalTestOrder.GrossX + Environment.NewLine + Environment.NewLine + this.m_GrossDescription;
            }

            this.m_GrossDescription = null;
            this.NotifyPropertyChanged("GrossDescription");

            if(this.ListBoxSpecimenOrders.SelectedIndex != this.ListBoxSpecimenOrders.Items.Count - 1)
            {
                this.ListBoxSpecimenOrders.SelectedIndex = this.ListBoxSpecimenOrders.SelectedIndex + 1;
            }
        }

        private void TextBoxGrossDescription_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox tbx = sender as TextBox;
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                this.HandleTextBoxGrossDescriptionTab();
            }
            else if(e.Key == Key.Enter)
            {
                if(this.TextBoxGrossDescription.SelectedText.Length > 0)
                {
                    if (this.TextBoxGrossDescription.SelectedText.Substring(0, 2) == "[?")
                    {
                        e.Handled = true;

                        int selectedTextStart = this.TextBoxGrossDescription.SelectionStart;
                        int selectedTextLength = this.TextBoxGrossDescription.SelectionLength;
                        string selectedText = this.TextBoxGrossDescription.Text.Substring(selectedTextStart, selectedTextLength);
                        this.TextBoxGrossDescription.Text = this.TextBoxGrossDescription.Text.Remove(selectedTextStart, selectedTextLength);

                        selectedText = selectedText.Replace("[?", "");
                        selectedText = selectedText.Replace("?]", "");

                        this.TextBoxGrossDescription.Text = this.TextBoxGrossDescription.Text.Insert(selectedTextStart, selectedText);

                        this.TextBoxGrossDescription.SelectionStart = selectedTextStart;
                        this.TextBoxGrossDescription.SelectionLength = selectedTextLength - 4;
                    }
                }                
            }
        }        

        private void HandleTextBoxGrossDescriptionTab()
        {
            int startingPosition = this.TextBoxGrossDescription.SelectionStart;
            if (string.IsNullOrEmpty(this.TextBoxGrossDescription.SelectedText) == false)
            {
                startingPosition = this.TextBoxGrossDescription.SelectionStart;
            }

            if (startingPosition == 0)
            {
                SelectNextInput(startingPosition);
            }
            else
            {
                if (SelectNextInput(startingPosition) == false)
                {
                    SelectNextInput(0);
                }
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
