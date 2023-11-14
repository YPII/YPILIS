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
    /// Interaction logic for SlideBlockStorage.xaml
    /// </summary>
    public partial class MaterialStorageLabels : Window
    {
        private int m_SlideStartNumber;
        private int m_Quantity;
        private string m_CaseType;

        public MaterialStorageLabels()
        {
            this.m_SlideStartNumber = 1000;
            this.m_Quantity = 10;
            this.m_CaseType = "S";

            InitializeComponent();
            this.DataContext = this;
        }      

        public int SlideStartNumber
        {
            get { return this.m_SlideStartNumber; }
            set { this.m_SlideStartNumber = value; }
        }

        public int SlideQuantity
        {
            get { return this.m_Quantity; }
            set { this.m_Quantity = value; }
        }

        public string CaseType
        {
            get { return this.m_CaseType; }
            set { this.m_CaseType = value; }
        }

        private void ButtonMaterialStorageLabels_Click(object sender, RoutedEventArgs e)
        {                        
            string zplCommands = Business.Label.Model.MaterialStorageLabel.GetCommands(this.m_SlideStartNumber, this.m_Quantity, this.CaseType);
            Business.Label.Model.ZPLPrinterTCP zplPrinter = new Business.Label.Model.ZPLPrinterTCP("10.1.1.19");
            zplPrinter.Print(zplCommands);

            MessageBox.Show("Your labels have been sent to the printer.");
        }
    }
}
