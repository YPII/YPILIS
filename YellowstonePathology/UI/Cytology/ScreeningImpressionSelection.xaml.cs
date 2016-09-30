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
    /// <summary>
    /// Interaction logic for CytologyOtherConditions.xaml
    /// </summary>
    public partial class ScreeningImpressionSelection : UserControl
    {                
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text",
            typeof(string), typeof(ScreeningImpressionSelection), new UIPropertyMetadata(string.Empty, TextChangedCallback));

		public static readonly DependencyProperty AccessionOrderProperty = DependencyProperty.Register("AccessionOrder",
			typeof(YellowstonePathology.Business.Test.AccessionOrder), typeof(ScreeningImpressionSelection), new UIPropertyMetadata(null, AccessionOrderChangedCallback));

		public static readonly DependencyProperty CytologyPanelOrderProperty = DependencyProperty.Register("CytologyPanelOrder",
            typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology), typeof(ScreeningImpressionSelection), new UIPropertyMetadata(null, CytologyPanelOrderChangedCallback));

        public static readonly DependencyProperty ScreeningImpressionVisibilityProperty = DependencyProperty.Register("ScreeningImpressionVisibility",
            typeof(Visibility), typeof(ScreeningImpressionSelection), new UIPropertyMetadata(System.Windows.Visibility.Collapsed, VisibilityChangedCallback));

		YellowstonePathology.Business.Cytology.Model.ScreeningImpressionCollection m_ScreeningImpressionCollection;

        public ScreeningImpressionSelection()
        {
			this.m_ScreeningImpressionCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetScreeningImpressions();

            InitializeComponent();

            this.ListBoxScreeningImpression.DataContext = this.m_ScreeningImpressionCollection;
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
			get { return (YellowstonePathology.Business.Test.AccessionOrder)GetValue(AccessionOrderProperty); }
            set
            {
                this.SetValue(AccessionOrderProperty, value);
            }
        }

        public Visibility ScreeningImpressionVisibility
        {
            get { return (Visibility)GetValue(ScreeningImpressionVisibilityProperty); }
            set 
            {
                this.SetValue(ScreeningImpressionVisibilityProperty, value); 
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
            this.SetValue(TextProperty, this.TextBoxScreeningImpression.Text);            
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.Cytology.SetScreeningImpression setScreeningImpression = new YellowstonePathology.Business.Rules.Cytology.SetScreeningImpression();
            YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();

			YellowstonePathology.Business.Cytology.Model.ScreeningImpression screeningImpression = (YellowstonePathology.Business.Cytology.Model.ScreeningImpression)this.ListBoxScreeningImpression.SelectedItem;
			setScreeningImpression.Execute(screeningImpression, this.CytologyPanelOrder, this.AccessionOrder, executionStatus);
           
            if (executionStatus.Halted == true)
            {
                System.Windows.MessageBox.Show(executionStatus.ExecutionMessagesString);
            }
            else
            {                
                this.StackPanelScreeningImpression.Visibility = System.Windows.Visibility.Collapsed;
                this.ScreeningImpressionVisibility = System.Windows.Visibility.Collapsed;
            }              
        }        

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {                        
            this.StackPanelScreeningImpression.Visibility = System.Windows.Visibility.Collapsed;
            this.ScreeningImpressionVisibility = System.Windows.Visibility.Collapsed;         
        }        

        private static void TextChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {            
            ScreeningImpressionSelection screeningImpressionSelection = (ScreeningImpressionSelection)d;
            screeningImpressionSelection.TextBoxScreeningImpression.Text = e.NewValue as string;         
        }

        private static void AccessionOrderChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ScreeningImpressionSelection screeningImpressionSelection = (ScreeningImpressionSelection)d;
			screeningImpressionSelection.AccessionOrder = (YellowstonePathology.Business.Test.AccessionOrder)e.NewValue;
		}

        private static void VisibilityChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {            
            ScreeningImpressionSelection screeningImpressionSelection = (ScreeningImpressionSelection)d;
            screeningImpressionSelection.StackPanelScreeningImpression.Visibility = (Visibility)e.NewValue;         
        }

        private static void CytologyPanelOrderChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ScreeningImpressionSelection ScreeningImpressionSelection = (ScreeningImpressionSelection)d;
            ScreeningImpressionSelection.CytologyPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)e.NewValue;
        }        
    }
}
