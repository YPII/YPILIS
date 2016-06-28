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
    /// Interaction logic for RadioButtonGroupLeukemiaCellDescription.xaml
    /// </summary>

    public partial class RadioButtonGroupLeukemiaCellDescription : System.Windows.Controls.UserControl
    {
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof(string),
            typeof(RadioButtonGroupLeukemiaCellDescription),
            new FrameworkPropertyMetadata(
                "Lymphocytes", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                new PropertyChangedCallback(OnSelectedItemChangedCallback)));        

        public string SelectedItem
        {
            get { return (string)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }       
        
        public static readonly DependencyProperty BTCellSelectionVisibilityProperty = DependencyProperty.Register(
            "BTCellSelectionVisibility",
            typeof(Visibility),
            typeof(RadioButtonGroupLeukemiaCellDescription),
            new FrameworkPropertyMetadata(Visibility.Collapsed));
        
        public Visibility BTCellSelectionVisibility
        {
            get { return (Visibility)GetValue(BTCellSelectionVisibilityProperty); }
            set { SetValue(BTCellSelectionVisibilityProperty, value); }
        }

        public static readonly DependencyProperty MarkerVisibilityProperty = DependencyProperty.Register(
           "MarkerVisibility",
           typeof(Visibility),
           typeof(RadioButtonGroupLeukemiaCellDescription),
           new FrameworkPropertyMetadata(Visibility.Collapsed));

        public Visibility MarkerVisibility
        {
            get { return (Visibility)GetValue(MarkerVisibilityProperty); }
            set { SetValue(MarkerVisibilityProperty, value); }
        }        

        public RadioButtonGroupLeukemiaCellDescription()
        {
            InitializeComponent();
        }

        private static void OnSelectedItemChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RadioButtonGroupLeukemiaCellDescription cellDescription = (RadioButtonGroupLeukemiaCellDescription)d;
            string description = e.NewValue.ToString();
            cellDescription.SetRadioButtons(description);
                     
            if (description == "Lymphocytes")
            {                
                cellDescription.BTCellSelectionVisibility = Visibility.Visible;                
            }
            else
            {
                cellDescription.BTCellSelectionVisibility = Visibility.Collapsed;
                
            }

            if (description == "Myeloid Cells" || description == "Monocytes")
            {
                cellDescription.MarkerVisibility = Visibility.Collapsed;
            }
            else
            {
                cellDescription.MarkerVisibility = Visibility.Visible;
            }
        }

        public void SetRadioButtons(string value)
        {
            switch (value)
            {
                case "Lymphocytes":
                    this.radioButtonLymphocytes.IsChecked = true;
                    break;
                case "Monocytes":
                    this.radioButtonMonocytes.IsChecked = true;
                    break;
                case "Myeloid Cells":
                    this.radioButtonMyeloidCells.IsChecked = true;
                    break;
                case "DIM45":
                    this.radioButtonDIM45.IsChecked = true;
                    break;
                case "Plasma Cells":
                    this.radioButtonPlasmaCells.IsChecked = true;
                    break;
                case "Myeloid Blasts":
                    this.radioButtonMyeloidBlast.IsChecked = true;
                    break;                
            }
        }

        public void radioButtonGroupCellDescription_Click(object sender, RoutedEventArgs args)
        {
            RadioButton radioButton = (RadioButton)args.OriginalSource;
            SelectedItem = radioButton.Content.ToString();            
        }
        
        public static readonly RoutedEvent SelectedItemChangedEvent = EventManager.RegisterRoutedEvent("SelectedItemChanged",
            RoutingStrategy.Bubble,
            typeof(DependencyPropertyChangedEventHandler),
            typeof(RadioButtonGroupLeukemiaCellDescription));

        public event RoutedEventHandler SelectedItemChanged
        {
            add { AddHandler(SelectedItemChangedEvent, value); }
            remove { RemoveHandler(SelectedItemChangedEvent, value); }
        }
    }
}