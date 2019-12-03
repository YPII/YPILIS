using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;

namespace YellowstonePathology.UI.Surgical
{
    /// <summary>
    /// Interaction logic for PathologistsScanDialog.xaml
    /// </summary>
    public partial class PathologistsScanDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private System.ComponentModel.BackgroundWorker m_BackgroundWorker;

        private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
        private YellowstonePathology.Business.Surgical.AssignmentScanCollection m_AssignmentScanCollection;

        private string m_PersistanceContext = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        private string m_StatusMessage;

        public PathologistsScanDialog()
        {
            this.m_AssignmentScanCollection = new Business.Surgical.AssignmentScanCollection();

            InitializeComponent();
            DataContext = this;

            this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;
            this.m_BarcodeScanPort.HistologySlideScanReceived += new Business.BarcodeScanning.BarcodeScanPort.HistologySlideScanReceivedHandler(HistologySlideScanReceived);
            this.m_BarcodeScanPort.ThinPrepSlideScanReceived += new Business.BarcodeScanning.BarcodeScanPort.ThinPrepSlideScanReceivedHandler(BarcodeScanPort_ThinPrepSlideScanReceived);
            this.m_BarcodeScanPort.CytologySlideScanReceived += new YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.CytologySlideScanReceivedHandler(CytologySlideScanReceived);
            this.m_BarcodeScanPort.HistologyBlockScanReceived += new Business.BarcodeScanning.BarcodeScanPort.HistologyBlockScanReceivedHandler(HistologyBlockScanReceived);

            //this.AddTestSlides();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public string StatusMessage
        {
            get { return this.m_StatusMessage; }
            set
            {
                if (this.m_StatusMessage != value)
                {
                    this.m_StatusMessage = value;
                    this.NotifyPropertyChanged("StatusMessage");
                }
            }
        }

        public YellowstonePathology.Business.Surgical.AssignmentScanCollection AssignmentScanCollection
        {
            get { return this.m_AssignmentScanCollection; }
        }

        private void HistologyBlockScanReceived(YellowstonePathology.Business.BarcodeScanning.Barcode barcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate ()
                    {

                        if (barcode.IsValidated == true)
                        {
                            this.ChangeListing(barcode.ID);
                        }
                        else
                        {
                            MessageBox.Show("The scanner did not read the barcode correctly.  Please try again.", "Invalid Scan", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    }));
        }

        private void HistologySlideScanReceived(YellowstonePathology.Business.BarcodeScanning.Barcode barcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate ()
                    {

                        if (barcode.IsValidated == true)
                        {
                            this.ChangeListing(barcode.ID);                            
                        }
                        else
                        {
                            MessageBox.Show("The scanner did not read the barcode correctly.  Please try again.", "Invalid Scan", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    }));
        }

        private void BarcodeScanPort_ThinPrepSlideScanReceived(Business.BarcodeScanning.Barcode barcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate ()
                    {
                        if (barcode.IsValidated == true)
                        {
                            this.ChangeListing(barcode.ID);
                        }
                        else
                        {
                            MessageBox.Show("The scanner did not read the barcode correctly.  Please try again.", "Invalid Scan", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    }));
        }

        private void CytologySlideScanReceived(YellowstonePathology.Business.BarcodeScanning.CytycBarcode barcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate ()
                    {
                        if (barcode.IsValidated == true)
                        {
                            this.ChangeListing(barcode.ReportNo);
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("The scan did not result in a valid case, please try again.", "Invalid Scan", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    }));
        }

        public void ChangeListing(string slideOrderId)
        {
            if (this.m_AssignmentScanCollection.Exists(slideOrderId) == false)
            {
                YellowstonePathology.Business.Surgical.AssignmentScan assignmentScan = new Business.Surgical.AssignmentScan();
                assignmentScan.ScanId = slideOrderId;
                this.m_AssignmentScanCollection.Add(assignmentScan);
            }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonAssign_Click(object sender, RoutedEventArgs e)
        {
            this.ButtonOK.IsEnabled = false;
            this.m_BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.m_BackgroundWorker.WorkerSupportsCancellation = false;
            this.m_BackgroundWorker.WorkerReportsProgress = true;
            this.m_BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(BackgroundWorker_ProgressChanged);
            this.m_BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(Assign);
            this.m_BackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
            this.m_BackgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.ButtonOK.IsEnabled = true;
            this.m_StatusMessage = "All done.";
            this.NotifyPropertyChanged("StatusMessage");
        }

        private void BackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {                
                this.m_StatusMessage = (string)e.UserState;                
                this.NotifyPropertyChanged("StatusMessage");
            }));
        }

        private void Assign(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            foreach (YellowstonePathology.Business.Surgical.AssignmentScan assignmentScan in this.m_AssignmentScanCollection)
            {                
                YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(assignmentScan.ScanId);
                string masterAccessionNo = orderIdParser.MasterAccessionNo;
                this.m_BackgroundWorker.ReportProgress(100, "Assigning Case: " + masterAccessionNo);
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this.m_PersistanceContext);
                accessionOrder.SetCaseOwnerId();
                assignmentScan.MasterAccessionNo = masterAccessionNo;
                foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in accessionOrder.PanelSetOrderCollection)
                {
                    if (panelSetOrder.AssignedToId == 0)
                    {
                        assignmentScan.AssignedTo = YellowstonePathology.Business.User.SystemIdentity.Instance.User.UserName;
                        panelSetOrder.AssignedToId = YellowstonePathology.Business.User.SystemIdentity.Instance.User.UserId;
                    }
                    else
                    {
                        assignmentScan.AssignedTo = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(panelSetOrder.AssignedToId).UserName;
                    }
                }
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this.m_PersistanceContext);
            }
        }

        private void AddTestSlides()
        {
            this.m_BarcodeScanPort.SimulateScanReceived("HSLD19-32076.1A1");
            this.m_BarcodeScanPort.SimulateScanReceived("HSLD19-32092.1A1");            
            this.NotifyPropertyChanged(string.Empty);
        }
    }
}
