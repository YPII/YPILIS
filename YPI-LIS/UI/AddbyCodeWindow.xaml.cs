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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for AddbyCodeWindow.xaml
    /// </summary>
    public partial class AddbyCodeWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_FieldDefinitions;
        private Business.Test.PanelSetOrder m_PanelSetOrder;

        public AddbyCodeWindow(string fieldDefinitions, Business.Test.PanelSetOrder panelSetOrder)
        {
            this.m_FieldDefinitions = fieldDefinitions;
            this.m_PanelSetOrder = panelSetOrder;
            InitializeComponent();
            DataContext = this.PanelSetOrder;

            Loaded += AddbyCodeWindow_Loaded;
        }

        private void AddbyCodeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SetupGrid();
            JArray fields = JArray.Parse(m_FieldDefinitions);

            for(int idx = 0; idx < fields.Count; idx ++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(0, GridUnitType.Auto);
                this.MainGrid.RowDefinitions.Add(row);

                string caption = (string)fields[idx]["label"];
                string value = (string)fields[idx]["property"];
                string fieldType = (string)fields[idx]["dataType"];

                this.AddData(idx, caption, value, fieldType);
            }

            RowDefinition row1 = new RowDefinition();
            row1.Height = new GridLength(150, GridUnitType.Star);
            this.MainGrid.RowDefinitions.Add(row1);

            Button c = new Button();
            c.Width = 80;
            c.Height = 30;
            c.Margin = new Thickness(5);
            c.VerticalAlignment = VerticalAlignment.Bottom;
            c.Click += Button1_Click;
            c.HorizontalAlignment = HorizontalAlignment.Right;
            c.Content = "Close";
            Grid.SetRow(c, fields.Count);
            Grid.SetColumn(c, 1);
            this.MainGrid.Children.Add(c);
        }

        private void SetupGrid()
        {
            ColumnDefinition coldef1 = new ColumnDefinition();
            GridLength gl1 = new GridLength(0, GridUnitType.Auto);
            coldef1.Width = gl1;

            ColumnDefinition colDef2 = new ColumnDefinition();
            GridLength gl2 = new GridLength(100, GridUnitType.Star);
            colDef2.Width = gl2;

            this.MainGrid.ColumnDefinitions.Add(coldef1);
            this.MainGrid.ColumnDefinitions.Add(colDef2);
        }

        private void AddData(int row, string caption, string value, string fieldType)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.FontWeight = FontWeights.Bold;
            textBlock.HorizontalAlignment = HorizontalAlignment.Right;
            textBlock.Margin = new Thickness(2);
            textBlock.Text = caption + ": ";
            Grid.SetRow(textBlock, row);
            Grid.SetColumn(textBlock, 0);
            this.MainGrid.Children.Add(textBlock);

            switch (fieldType)
            {
                case "string":
                    this.CreateTextBox(row, value, null);
                    break;
                case "DateTime":
                    this.CreateTextBox(row, value, new Converter.MilitaryDateTimeConverterV2());
                    break;
                case "bool":
                    this.CreateCheckBox(row, value);
                    break;
            }
        }

        private void CreateTextBox(int row, string value, IValueConverter converter)
        {
            TextBox textBox = new TextBox();
            textBox.HorizontalAlignment = HorizontalAlignment.Stretch;
            textBox.Margin = new Thickness(2);
            Binding binding = new Binding(value);
            if(converter != null)
            {
                binding.Converter = converter;
            }
            textBox.SetBinding(TextBox.TextProperty, binding);
            Grid.SetRow(textBox, row);
            Grid.SetColumn(textBox, 1);
            this.MainGrid.Children.Add(textBox);
        }

        private void CreateCheckBox(int row, string value)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.HorizontalAlignment = HorizontalAlignment.Left;
            checkBox.Margin = new Thickness(2);
            Binding binding = new Binding(value);
            checkBox.SetBinding(CheckBox.IsCheckedProperty,binding);
            Grid.SetRow(checkBox, row);
            Grid.SetColumn(checkBox, 1);
            this.MainGrid.Children.Add(checkBox);
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }
    }
}
