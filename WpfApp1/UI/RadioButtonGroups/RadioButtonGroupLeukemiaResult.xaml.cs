using System;
using System.Collections.Generic;
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

namespace YellowstonePathology.UI.RadioButtonGroups
{   
    public partial class RadioButtonGroupLeukemiaResult : System.Windows.Controls.UserControl
    {
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof(string),
            typeof(RadioButtonGroupLeukemiaResult),
            new FrameworkPropertyMetadata(
                "Normal", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                new PropertyChangedCallback(OnSelectedItemChangedCallback)));

        public string SelectedItem
        {
            get { return (string)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty AbnormalSelectionVisibilityProperty = DependencyProperty.Register(
            "AbnormalSelectionVisibility",
            typeof(Visibility),
            typeof(RadioButtonGroupLeukemiaResult),
            new FrameworkPropertyMetadata(Visibility.Collapsed));

        public Visibility AbnormalSelectionVisibility
        {
            get { return (Visibility)GetValue(AbnormalSelectionVisibilityProperty); }
            set { SetValue(AbnormalSelectionVisibilityProperty, value); }
        }

        public static readonly DependencyProperty NormalSelectionVisibilityProperty = DependencyProperty.Register(
            "NormalSelectionVisibility",
            typeof(Visibility),
            typeof(RadioButtonGroupLeukemiaResult),
            new FrameworkPropertyMetadata(Visibility.Collapsed));

        public Visibility NormalSelectionVisibility
        {
            get { return (Visibility)GetValue(NormalSelectionVisibilityProperty); }
            set { SetValue(NormalSelectionVisibilityProperty, value); }
        }
        
        public RadioButtonGroupLeukemiaResult()
        {
            InitializeComponent();
        }

        private static void OnSelectedItemChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RadioButtonGroupLeukemiaResult leukemiaResult = (RadioButtonGroupLeukemiaResult)d;
            leukemiaResult.SetRadioButtons(e.NewValue.ToString());
        }        

        public void SetRadioButtons(string value)
        {            
            switch (value)
            {
                case "Normal":
                    this.radioButtonNormal.IsChecked = true;
                    this.radioButtonAbnormal.IsChecked = false;
                    this.NormalSelectionVisibility = Visibility.Visible;
                    this.AbnormalSelectionVisibility = Visibility.Collapsed;                    
                    break;
                case "Abnormal":
                    this.radioButtonNormal.IsChecked = false;
                    this.radioButtonAbnormal.IsChecked = true;
                    this.NormalSelectionVisibility = Visibility.Visible;
                    this.AbnormalSelectionVisibility = Visibility.Visible;                    
                    break;
                default:
                    this.radioButtonNormal.IsChecked = false;
                    this.radioButtonAbnormal.IsChecked = false;
                    this.NormalSelectionVisibility = Visibility.Collapsed;
                    this.AbnormalSelectionVisibility = Visibility.Collapsed;                    
                    break;                
            }            
        }

        public void RadioButtonNormal_Click(object sender, RoutedEventArgs args)
        {
            SelectedItem = "Normal";
            NormalSelectionVisibility = Visibility.Visible;
            AbnormalSelectionVisibility = Visibility.Collapsed;
        }

        public void RadioButtonAbnormal_Click(object sender, RoutedEventArgs args)
        {
            SelectedItem = "Abnormal";
            NormalSelectionVisibility = Visibility.Collapsed;
            AbnormalSelectionVisibility = Visibility.Visible;            
        }        

        public static readonly RoutedEvent SelectedItemChangedEvent = EventManager.RegisterRoutedEvent("SelectedItemChanged",
            RoutingStrategy.Bubble,
            typeof(DependencyPropertyChangedEventHandler),
            typeof(RadioButtonGroupLeukemiaResult));

        public event RoutedEventHandler SelectedItemChanged
        {
            add { AddHandler(SelectedItemChangedEvent, value); }
            remove { RemoveHandler(SelectedItemChangedEvent, value); }
        }
    }
}