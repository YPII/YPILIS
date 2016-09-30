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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for PantherStorageDialog.xaml
    /// </summary>
    public partial class PantherStorageDialog : Window
    {
        private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;

        public PantherStorageDialog()
        {
            this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;
            this.m_BarcodeScanPort.CytologySlideScanReceived += BarcodeScanPort_CytologySlideScanReceived;
            this.m_BarcodeScanPort.HistologySlideScanReceived += BarcodeScanPort_HistologySlideScanReceived;
            this.m_BarcodeScanPort.ThinPrepSlideScanReceived += BarcodeScanPort_ThinPrepSlideScanReceived;

            InitializeComponent();
        }

        private void BarcodeScanPort_ThinPrepSlideScanReceived(Business.BarcodeScanning.Barcode barcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate ()
                    {
                        if (barcode.IsValidated == true)
                        {
                            MessageBox.Show("Got a Thin Prep Slide Scan");
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("The scan did not result in a valid case, please try again.", "Invalid Scan", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    }));
        }

        private void BarcodeScanPort_HistologySlideScanReceived(Business.BarcodeScanning.Barcode barcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate ()
                    {
                        if (barcode.IsValidated == true)
                        {
                            MessageBox.Show("Got a Histology Slide Scan");
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("The scan did not result in a valid case, please try again.", "Invalid Scan", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    }));
        }

        private void BarcodeScanPort_CytologySlideScanReceived(Business.BarcodeScanning.CytycBarcode barcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate ()
                    {
                        if (barcode.IsValidated == true)
                        {
                            MessageBox.Show("Got a Cytology Slide Scan");
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("The scan did not result in a valid case, please try again.", "Invalid Scan", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    }));
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
