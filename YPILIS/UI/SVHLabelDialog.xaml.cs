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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for LabelPrintDialog.xaml
    /// </summary>
    public partial class SVHLabelDialog : Window
    {
        private string m_Year;
        private int m_Rows;

        public SVHLabelDialog()
        {
            this.m_Year = DateTime.Today.ToString("yyyy");
            this.m_Rows = 50;
            InitializeComponent();

            DataContext = this;
        }

        public int Rows
        {
            get { return this.m_Rows; }
            set { this.m_Rows = value; }
        }

        public string Year
        {
            get { return this.m_Year; }
            set { this.m_Year = value; }
        }

        private void HyperLinkPrintIFLabels_Click(object sender, RoutedEventArgs e)
        {
            if(this.IsValidYear() == true && this.RowCountIsAcceptable() == true)
            {
                Business.Label.Model.ZPLPrinterTCP printer = new Business.Label.Model.ZPLPrinterTCP("10.1.1.21");
                for (int x = 0; x < this.m_Rows; x++)
                {
                    string commands = Business.Label.Model.IFEZPLLabel.GetCommands(this.m_Year);
                    printer.Print(commands);
                }
            }
        }

        private void HyperLinkPrintFormalinAddedLabels_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsValidYear() == true && this.RowCountIsAcceptable() == true)
            {
                Business.Label.Model.ZPLPrinterTCP printer = new Business.Label.Model.ZPLPrinterTCP("10.1.1.21");
                for (int x = 0; x < this.m_Rows; x++)
                {
                    string commands = Business.Label.Model.FormalinAddedZPLLabel.GetCommands();
                    printer.Print(commands);
                }
            }
        }

        private void HyperLinkPrintSerumLabels_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsValidYear() == true && this.RowCountIsAcceptable() == true)
            {
                Business.Label.Model.ZPLPrinterTCP printer = new Business.Label.Model.ZPLPrinterTCP("10.1.1.21");
                for (int x = 0; x < this.m_Rows; x++)
                {
                    string commands = Business.Label.Model.SerumZPLLabel.GetCommands(this.m_Year);
                    printer.Print(commands);
                }
            }
        }

        private void HyperLinkPrintUrineLabels_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsValidYear() == true && this.RowCountIsAcceptable() == true)
            {
                Business.Label.Model.ZPLPrinterTCP printer = new Business.Label.Model.ZPLPrinterTCP("10.1.1.21");
                for (int x = 0; x < this.m_Rows; x++)
                {
                    string commands = Business.Label.Model.UrineZPLLabel.GetCommands(this.m_Year);
                    printer.Print(commands);
                }
            }
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool IsValidYear()
        {
            bool result = true;
            if (this.m_Year.Length != 4)
            {
                result = false;
                MessageBox.Show("Enter a valid 4 digit year.");
            }
            else
            {
                DateTime date;
                string testDate = "1/1/" + this.m_Year;
                result = DateTime.TryParse(testDate, out date);
                if (result == false)
                {
                    MessageBox.Show("Enter a valid 4 digit year.");
                }
            }
            return result;
        }

        private bool RowCountIsAcceptable()
        {
            bool result = true;
            if(this.m_Rows < 1 || this.m_Rows > 100)
            {
                result = false;
                MessageBox.Show("Enter a row count between 1 and 100.");
            }
            return result;
        }
    }
}
