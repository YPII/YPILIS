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

namespace YellowstonePathology.UI.Cytology
{    
    public partial class SpecimenAdequacySelection : UserControl
    {                
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text",
            typeof(string), typeof(SpecimenAdequacySelection), new UIPropertyMetadata(string.Empty, TextChangedCallback));

		public static readonly DependencyProperty AccessionOrderProperty = DependencyProperty.Register("AccessionOrder",
			typeof(YellowstonePathology.Business.Test.AccessionOrder), typeof(SpecimenAdequacySelection), new UIPropertyMetadata(null, AccessionOrderChangedCallback));

		public static readonly DependencyProperty CytologyPanelOrderProperty = DependencyProperty.Register("CytologyPanelOrder",
            typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology), typeof(SpecimenAdequacySelection), new UIPropertyMetadata(null, CytologyPanelOrderChangedCallback));

        public static readonly DependencyProperty SpecimenAdequacyVisibilityProperty = DependencyProperty.Register("SpecimenAdequacyVisibility",
            typeof(Visibility), typeof(SpecimenAdequacySelection), new UIPropertyMetadata(System.Windows.Visibility.Collapsed, VisibilityChangedCallback));

		YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyCollection m_SpecimenAdequacyCollection;
		YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyCommentCollection m_SpecimenAdequacyCommentCollection;                

        public SpecimenAdequacySelection()
        {
			this.m_SpecimenAdequacyCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSpecimenAdequacy();
			this.m_SpecimenAdequacyCommentCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSpecimenAdequacyComments();

            InitializeComponent();

            this.ListBoxSpecimenAdequacy.DataContext = this.m_SpecimenAdequacyCollection;
            this.ListViewComments.DataContext = this.m_SpecimenAdequacyCommentCollection;
        }                

        public Visibility SpecimenAdequacyVisibility
        {
            get { return (Visibility)GetValue(SpecimenAdequacyVisibilityProperty); }
            set 
            { 
                this.SetValue(SpecimenAdequacyVisibilityProperty, value); 
            }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                this.SetValue(TextProperty, value);
            }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
			get { return (YellowstonePathology.Business.Test.AccessionOrder)GetValue(AccessionOrderProperty); }
            set
            {
                this.SetValue(AccessionOrderProperty, value);
            }
        }

        public YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology CytologyPanelOrder
        {
            get { return (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)GetValue(CytologyPanelOrderProperty); }
            set
            {
                this.SetValue(CytologyPanelOrderProperty, value);
            }
        }

        private void TextBoxText_TextChanged(object sender, TextChangedEventArgs e)
        {            
            this.SetValue(TextProperty, this.TextBoxSpecimenAdequacy.Text);            
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.Cytology.SetSpecimenAdequacy setSpecimenAdequacy = new YellowstonePathology.Business.Rules.Cytology.SetSpecimenAdequacy();
            YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();

			YellowstonePathology.Business.Cytology.Model.SpecimenAdequacy specimenAdequacy = (YellowstonePathology.Business.Cytology.Model.SpecimenAdequacy)this.ListBoxSpecimenAdequacy.SelectedItem;
			List<YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyComment> specimenAdequacyComments = new List<YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyComment>();
			foreach (YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyComment specimenAdequacyComment in this.ListViewComments.SelectedItems)
            {
                specimenAdequacyComments.Add(specimenAdequacyComment);  
            }

			setSpecimenAdequacy.Execute(specimenAdequacy, specimenAdequacyComments, this.CytologyPanelOrder, this.AccessionOrder, executionStatus);
            if (executionStatus.Halted == true)
            {
                System.Windows.MessageBox.Show(executionStatus.ExecutionMessagesString);
            }
            else
            {
                this.ListViewComments.SelectedItems.Clear();
                this.StackPanelSpecimenAdequacy.Visibility = System.Windows.Visibility.Collapsed;
                this.SpecimenAdequacyVisibility = System.Windows.Visibility.Collapsed;                
            }            
        }        

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.ListViewComments.SelectedItems.Clear();
            this.StackPanelSpecimenAdequacy.Visibility = System.Windows.Visibility.Collapsed;
            this.SpecimenAdequacyVisibility = System.Windows.Visibility.Collapsed;
            
        }        

        private static void TextChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SpecimenAdequacySelection specimenAdequacySelection = (SpecimenAdequacySelection)d;
            specimenAdequacySelection.TextBoxSpecimenAdequacy.Text = e.NewValue as string;
        }

        private static void AccessionOrderChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SpecimenAdequacySelection specimenAdequacySelection = (SpecimenAdequacySelection)d;
			specimenAdequacySelection.AccessionOrder = (YellowstonePathology.Business.Test.AccessionOrder)e.NewValue;
		}

        private static void VisibilityChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SpecimenAdequacySelection specimenAdequacySelection = (SpecimenAdequacySelection)d;
            specimenAdequacySelection.StackPanelSpecimenAdequacy.Visibility = (Visibility)e.NewValue;
        }

        private static void CytologyPanelOrderChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SpecimenAdequacySelection specimenAdequacySelection = (SpecimenAdequacySelection)d;
            specimenAdequacySelection.CytologyPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)e.NewValue;
        }        
    }
}