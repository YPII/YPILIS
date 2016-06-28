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
                    int pageCount = this.GetPageCount();
                    if (pageCount > 0)
                    {
                        YellowstonePathology.Business.Label.Model.ContainerPaperLabelPrinter labelPrinter = new Business.Label.Model.ContainerPaperLabelPrinter();
                        for(int i=0; i<pageCount; i++)
                        {
							YellowstonePathology.Business.BarcodeScanning.ContainerBarcode containerBarcode = Business.BarcodeScanning.ContainerBarcode.Parse();
                            YellowstonePathology.Business.Label.Model.ContainerPaperLabel containerPaperLabel = new Business.Label.Model.ContainerPaperLabel(containerBarcode);
                            labelPrinter.Queue.Enqueue(containerPaperLabel);
                        }
                        labelPrinter.Print();
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
                result = pageCount * 4;
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
