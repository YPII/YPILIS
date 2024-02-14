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
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Test
{
    public partial class BarcodeLabelDialog : Window
    {
        private StringBuilder m_KeyboardInput;
        private string m_ContainerId;

        public BarcodeLabelDialog()
        {
            this.m_KeyboardInput = new StringBuilder();
            InitializeComponent();
            this.ComboboxDocumentType.SelectedIndex = 0;
        }

        public string ContainerId
        {
            get { return this.m_ContainerId; }
            set { this.m_ContainerId = value; }
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ComboboxDocumentType.Text))
            {
                if (this.TextBlockRowCount.Text.Length > 0)
                {
                    Business.Label.Model.ZPLPrinterTCP printer = new Business.Label.Model.ZPLPrinterTCP("10.1.1.21");
                    int pageCount = this.GetPageCount();
                    for (int x=0; x<pageCount; x++)
                    {                        
                        string commands = Business.Label.Model.ContainerZPLLabel.GetCommands();
                        printer.Print(commands);                        
                    }
                }
                else
                {
                    MessageBox.Show("The row count is not set correctly.");
                }
            }
        }

        private int GetPageCount()
        {
            int result = 0;
            int pageCount = 0;
            if (Int32.TryParse(this.TextBlockRowCount.Text, out pageCount) == true)
            {
                result = pageCount;
            }
            return result;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
