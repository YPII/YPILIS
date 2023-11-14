using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class RadioButtonGroupMarkerExpression : System.Windows.Controls.UserControl
    {        
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof(int),
            typeof(RadioButtonGroupMarkerExpression),
            new FrameworkPropertyMetadata(
                0,FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                new PropertyChangedCallback(OnSelectedItemChangedCallback)));

        public int SelectedItem
        {
            get { return (int)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }        

        public RadioButtonGroupMarkerExpression()
        {
            InitializeComponent();
        }

        private static void OnSelectedItemChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RadioButtonGroupMarkerExpression markersExpress = (RadioButtonGroupMarkerExpression)d;
            markersExpress.SetRadioButtons(Convert.ToInt32(e.NewValue.ToString()));            
        }
        
        public void SetRadioButtons(int value)
        {            
            switch (value)
            {
                case 0:
                    this.checkBoxMarkerSelected.IsChecked = false;
                    this.radioButtonExpresses.IsChecked = false;
                    this.radioButtonDoesNotExpress.IsChecked = false;                    
                    this.radioButtonEquivocal.IsChecked = false;
                    break;
                case 1:
                    this.checkBoxMarkerSelected.IsChecked = true;
                    this.radioButtonExpresses.IsChecked = true;
                    this.radioButtonDoesNotExpress.IsChecked = false;                    
                    this.radioButtonEquivocal.IsChecked = false;
                    break;
                case 2:
                    this.checkBoxMarkerSelected.IsChecked = true;
                    this.radioButtonExpresses.IsChecked = false;
                    this.radioButtonDoesNotExpress.IsChecked = true;                    
                    this.radioButtonEquivocal.IsChecked = false;
                    break;
                case 3:
                    this.checkBoxMarkerSelected.IsChecked = true;
                    this.radioButtonExpresses.IsChecked = false;
                    this.radioButtonDoesNotExpress.IsChecked = false;                    
                    this.radioButtonEquivocal.IsChecked = true;
                    break;                
            }
        }

        public void RadioButtonExpresses_Click(object sender, RoutedEventArgs args)
        {            
            SelectedItem = 1;
        }

        public void RadioButtonDoesNotExpress_Click(object sender, RoutedEventArgs args)
        {
            SelectedItem = 2;
        }

        public void RadioButtonEquivocal_Click(object sender, RoutedEventArgs args)
        {
            SelectedItem = 3;            
        }

        public void CheckBoxMarkerSelected_Unchecked(object sender, RoutedEventArgs args)
        {
            this.SelectedItem = 0;
        }
        
        public static readonly RoutedEvent SelectedItemChangedEvent = EventManager.RegisterRoutedEvent("SelectedItemChanged", 
            RoutingStrategy.Bubble, 
            typeof(DependencyPropertyChangedEventHandler), 
            typeof(RadioButtonGroupMarkerExpression));

        public event RoutedEventHandler SelectedItemChanged
        {
            add { AddHandler(SelectedItemChangedEvent, value); }
            remove { RemoveHandler(SelectedItemChangedEvent, value); }
        }
    }
}