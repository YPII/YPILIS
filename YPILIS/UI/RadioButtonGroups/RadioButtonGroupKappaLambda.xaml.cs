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
    /// Interaction logic for RadioButtonGroupKappaLambda.xaml
    /// </summary>

    public partial class RadioButtonGroupKappaLambda : System.Windows.Controls.UserControl
    {
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof(string),
            typeof(RadioButtonGroupKappaLambda),
            new FrameworkPropertyMetadata(
                "KappaExpresses", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                new PropertyChangedCallback(OnSelectedItemChangedCallback)));

        public string SelectedItem
        {
            get { return (string)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public RadioButtonGroupKappaLambda()
        {
            InitializeComponent();
        }

        private static void OnSelectedItemChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RadioButtonGroupKappaLambda kappaLambdaSelection = (RadioButtonGroupKappaLambda)d;
            kappaLambdaSelection.SetRadioButtons(e.NewValue.ToString());
        }        

        public void SetRadioButtons(string value)
        {
            switch (value)
            {
                case "KappaExpresses":
                    this.radioButtonKappaExpresses.IsChecked = true;
                    break;
                case "KappaEquivocal":
                    this.radioButtonKappaEquivocal.IsChecked = true;
                    break;
                case "LambdaExpresses":
                    this.radioButtonLambdaExpresses.IsChecked = true;
                    break;
                case "LambdaEquivocal":
                    this.radioButtonLambdaEquivocal.IsChecked = true;
                    break;
            }
        }       
                
        public void RadioButtonGroupKappaLambda_Click(object sender, RoutedEventArgs args)
        {
            RadioButton radioButton = (RadioButton)args.OriginalSource;
            SelectedItem = radioButton.Tag.ToString();            
        }

        public void CheckBoxKappaLambda_Unchecked(object sender, RoutedEventArgs args)
        {
            this.radioButtonKappaEquivocal.IsChecked = false;
            this.radioButtonKappaExpresses.IsChecked = false;
            this.radioButtonLambdaEquivocal.IsChecked = false;
            this.radioButtonLambdaExpresses.IsChecked = false;
            SelectedItem = string.Empty;
        }        

        public static readonly RoutedEvent SelectedItemChangedEvent = EventManager.RegisterRoutedEvent("SelectedItemChanged",
            RoutingStrategy.Bubble,
            typeof(DependencyPropertyChangedEventHandler),
            typeof(RadioButtonGroupKappaLambda));

        public event RoutedEventHandler SelectedItemChanged
        {
            add { AddHandler(SelectedItemChangedEvent, value); }
            remove { RemoveHandler(SelectedItemChangedEvent, value); }
        }

    }
}