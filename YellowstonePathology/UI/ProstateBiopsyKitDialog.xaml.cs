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
    /// Interaction logic for ProstateBiopsyKitDialog.xaml
    /// </summary>
    public partial class ProstateBiopsyKitDialog : Window
    {
        public ProstateBiopsyKitDialog()
        {
            InitializeComponent();
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            Business.Label.Model.ProstateBiopsyKitCollection prostateBiopsyKitCollection = new Business.Label.Model.ProstateBiopsyKitCollection();
            Business.Label.Model.CassettePrinterGrossHobbit printer = new Business.Label.Model.CassettePrinterGrossHobbit();
            printer.Print(prostateBiopsyKitCollection);
            MessageBox.Show("Prostate biopsy kit blocks have been sent to the printer.");
        }
    }
}
