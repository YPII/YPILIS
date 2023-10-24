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
    /// Interaction logic for RadioButtonGroupBTCellSelection.xaml
    /// </summary>

    public partial class RadioButtonGroupBTCellSelection : System.Windows.Controls.UserControl
    {
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem",
            typeof(string),
            typeof(RadioButtonGroupBTCellSelection),
            new FrameworkPropertyMetadata(
                "B-Cell", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                new PropertyChangedCallback(OnSelectedItemChangedCallback)));

        public string SelectedItem
        {
            get { return (string)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public RadioButtonGroupBTCellSelection()
        {
            InitializeComponent();
        }

        private static void OnSelectedItemChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RadioButtonGroupBTCellSelection cellSelection = (RadioButtonGroupBTCellSelection)d;
            cellSelection.SetRadioButtons(e.NewValue.ToString());
        }

        public void SetRadioButtons(string value)
        {
            switch (value)
            {
                case "B-Cells":
                    this.radioButtonBCells.IsChecked = true;
                    break;
                case "T-Cells":
                    this.radioButtonTCells.IsChecked = true;
                    break;                
            }
        }

        public void RadioButtonBTCellSelection_Click(object sender, RoutedEventArgs args)
        {
            RadioButton radioButton = (RadioButton)args.OriginalSource;
            SelectedItem = radioButton.Content.ToString();
        }

        public static readonly RoutedEvent SelectedItemChangedEvent = EventManager.RegisterRoutedEvent("SelectedItemChanged",
            RoutingStrategy.Bubble,
            typeof(DependencyPropertyChangedEventHandler),
            typeof(RadioButtonGroupBTCellSelection));

        public event RoutedEventHandler SelectedItemChanged
        {
            add { AddHandler(SelectedItemChangedEvent, value); }
            remove { RemoveHandler(SelectedItemChangedEvent, value); }
        }
    }
}