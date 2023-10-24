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
    /// <summary>
    /// Interaction logic for RadioButtonGroupAgreeDisagree.xaml
    /// </summary>

    public partial class RadioButtonGroupReviewResult : System.Windows.Controls.UserControl
    {
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof(string),
            typeof(RadioButtonGroupReviewResult),
            new FrameworkPropertyMetadata(
                "Agree", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                new PropertyChangedCallback(OnSelectedItemChangedCallback)));

        public string SelectedItem
        {
            get { return (string)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public RadioButtonGroupReviewResult()
        {
            InitializeComponent();
        }

        private static void OnSelectedItemChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RadioButtonGroupReviewResult cellSelection = (RadioButtonGroupReviewResult)d;
            cellSelection.SetRadioButtons(e.NewValue.ToString());
        }

        public void SetRadioButtons(string value)
        {            
            switch (value)
            {
                case "Not Reviewed":
                    this.radioButtonNotReviewed.IsChecked = true;                    
                    break;
                case "Agree":                    
                    this.radioButtonAgree.IsChecked = true;                    
                    break;
                case "Disagree":                    
                    this.radioButtonDisagree.IsChecked = true;
                    break;                
            }         
        }

        public void RadioButtonReviewResultSelection_Click(object sender, RoutedEventArgs args)
        {
            //RadioButton radioButton = (RadioButton)args.OriginalSource;
            //SelectedItem = radioButton.Content.ToString();
        }

        public static readonly RoutedEvent SelectedItemChangedEvent = EventManager.RegisterRoutedEvent("SelectedItemChanged",
            RoutingStrategy.Direct,
            typeof(DependencyPropertyChangedEventHandler),
            typeof(RadioButtonGroupReviewResult));

        public event RoutedEventHandler SelectedItemChanged
        {
            add { AddHandler(SelectedItemChangedEvent, value); }
            remove { RemoveHandler(SelectedItemChangedEvent, value); }
        }
    }
}