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
using System.ComponentModel;
using StackExchange.Redis;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for EmbeddingDialog.xaml
    /// </summary>
    public partial class EmbeddingDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime m_WorkDate;        

        private YellowstonePathology.Business.BarcodeScanning.EmbeddingScanCollection m_EmbeddingScanCollection;
        private YellowstonePathology.Business.Test.AliquotOrderCollection m_AliquotOrderHoldCollection;
        private EmbeddingNotScannedList m_EmbeddingNotScannedList;
        private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
        private YellowstonePathology.Business.Surgical.ProcessorRunCollection m_ProcessorRunCollection;
        private EmbeddingBreastCaseList m_EmbeddingBreastCaseList;
        private string m_StatusMessage;
        private string m_ScanCount;

        private Nullable<DateTime> m_ProcessorStartTime;
        private TimeSpan m_ProcessorFixationDuration;

        private BackgroundWorker m_BackgroundWorker;

        public EmbeddingDialog()
        {            
            this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;
            this.m_WorkDate = DateTime.Today;
                        
            this.m_EmbeddingScanCollection = Business.BarcodeScanning.EmbeddingScanCollection.GetByScanDate(this.m_WorkDate);                       
            
            this.m_StatusMessage = "Status: OK";
            this.m_ScanCount = "Block Count: " + this.m_EmbeddingScanCollection.Count.ToString();            

            InitializeComponent();

            
            this.DataContext = this;
            this.Loaded += EmbeddingDialog_Loaded;
            this.Unloaded += EmbeddingDialog_Unloaded;
            this.ComboBoxProcessorRuns.SelectedIndex = 0;
        }

        private void EmbeddingDialog_Unloaded(object sender, RoutedEventArgs e)
        {
            this.m_BarcodeScanPort.HistologyBlockScanReceived -= this.HistologyBlockScanReceived;
            this.m_BarcodeScanPort.ContainerScanReceived -= this.BarcodeScanPort_ContainerScanReceived;            
        }

        private DateTime GetWorkingAccessionDate()
        {
            DateTime accessionDate = this.m_WorkDate.AddDays(-1);
            return accessionDate;
        }

        private void EmbeddingDialog_Loaded(object sender, RoutedEventArgs e)
        {            
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                this.m_BarcodeScanPort.HistologyBlockScanReceived += this.HistologyBlockScanReceived;
                this.m_BarcodeScanPort.ContainerScanReceived += BarcodeScanPort_ContainerScanReceived;
                
                this.m_AliquotOrderHoldCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAliquotOrderHoldCollection();
                this.m_ProcessorRunCollection = YellowstonePathology.Business.Surgical.ProcessorRunCollection.GetAll();

                this.m_EmbeddingNotScannedList = Business.Gateway.AccessionOrderGateway.GetEmbeddingNotScannedCollection(this.GetWorkingAccessionDate());
                this.m_EmbeddingBreastCaseList = Business.Gateway.AccessionOrderGateway.GetEmbeddingBreastCasesCollection();

                this.NotifyPropertyChanged(string.Empty);
            }
            ));
        }

        private void BarcodeScanPort_ContainerScanReceived(Business.BarcodeScanning.ContainerBarcode containerBarcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {                
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullSpecimenOrderByContainerId(containerBarcode.ToString(), this);
                foreach(Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    if(aliquotOrder.Status == "Hold")
                    {
                        aliquotOrder.Status = null;
                    }
                    else
                    {
                        aliquotOrder.Status = "Hold";
                    }                    
                }

                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
                this.m_AliquotOrderHoldCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAliquotOrderHoldCollection();
                this.NotifyPropertyChanged("AliquotOrderHoldCollection");

                this.m_ScanCount = "Block Count: " + this.m_EmbeddingScanCollection.Count.ToString();
                this.NotifyPropertyChanged("ScanCount");
            }
            ));
        }

        private void ContextMenuRemoveHold_Click(object sender, RoutedEventArgs e)
        {            
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                if(this.ListViewHoldList.SelectedItem != null)
                {
                    YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = (YellowstonePathology.Business.Test.AliquotOrder)this.ListViewHoldList.SelectedItem;                    
                    YellowstonePathology.Business.Test.AliquotOrder dbAliquotOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAliquotOrder(aliquotOrder.AliquotOrderId, this);
                    dbAliquotOrder.Status = null;                    

                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
                    this.m_AliquotOrderHoldCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAliquotOrderHoldCollection();
                    this.NotifyPropertyChanged("AliquotOrderHoldCollection");
                }                
            }
            ));         
        }

        private void HistologyBlockScanReceived(YellowstonePathology.Business.BarcodeScanning.Barcode barcode)
        {
            if (barcode.ID.Contains("ALQ") == true)
            {
                MessageBox.Show("The scan for this block was read correctly. Please try again.");
            }
            else
            {
                this.RecieveScan(barcode.ID);
            }            
        } 
        
        private void RecieveScan(string aliquotOrderId)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {                                
                if (this.IsProcessorStartTimeValid() == true)
                {
                    YellowstonePathology.Business.BarcodeScanning.EmbeddingScan result = this.m_EmbeddingScanCollection.HandleScan(aliquotOrderId, this.m_ProcessorStartTime.Value, this.m_ProcessorFixationDuration);
                    this.ListViewEmbeddingScans.SelectedIndex = 0;
                    this.m_ScanCount = "Block Count: " + this.m_EmbeddingScanCollection.Count.ToString();
                    this.NotifyPropertyChanged("ScanCount");
                }
                else
                {
                    MessageBox.Show("I can't add the scan until a processor start time is entered.");
                }                
            }
            ));
        } 
        
        private bool IsProcessorStartTimeValid()
        {
            bool result = false;
            if (this.m_ProcessorStartTime.HasValue == true) result = true;
            return result;
        } 
        
        public string StatusMessage
        {
            get { return this.m_StatusMessage; }
        }

        public string ScanCount
        {
            get { return this.m_ScanCount; }
        }      
        
        public Nullable<DateTime> ProcessorStartTime
        {
            get { return this.m_ProcessorStartTime; }
            set { this.m_ProcessorStartTime = value; }
        }  

        public Nullable<TimeSpan> ProcessorFixationDuration
        {
            get { return this.m_ProcessorFixationDuration; }
        }

        public YellowstonePathology.UI.EmbeddingNotScannedList EmbeddingNotScannedList
        {
            get { return this.m_EmbeddingNotScannedList; }
        }

        public YellowstonePathology.UI.EmbeddingBreastCaseList EmbeddingBreastCaseList
        {
            get { return this.m_EmbeddingBreastCaseList; }
        }

        public YellowstonePathology.Business.Surgical.ProcessorRunCollection ProcessorRunCollection
        {
            get { return this.m_ProcessorRunCollection; }
        }

        public DateTime WorkDate
        {
            get { return this.m_WorkDate; }
            set
            {
                if(this.m_WorkDate != value)
                {
                    this.m_WorkDate = value;
                    this.NotifyPropertyChanged("WorkDate");
                }
            }
        }    
        
        public YellowstonePathology.Business.BarcodeScanning.EmbeddingScanCollection EmbeddingScanCollection
        {
            get { return this.m_EmbeddingScanCollection; }
        }            

        public YellowstonePathology.Business.Test.AliquotOrderCollection AliquotOrderHoldCollection
        {
            get { return this.m_AliquotOrderHoldCollection; }
        }        

        private void ButtonAccessionOrderBack_Click(object sender, RoutedEventArgs e)
        {
            this.WorkDate = this.WorkDate.AddDays(-1);
            this.m_EmbeddingScanCollection = YellowstonePathology.Business.BarcodeScanning.EmbeddingScanCollection.GetByScanDate(this.m_WorkDate);
            this.m_EmbeddingNotScannedList = Business.Gateway.AccessionOrderGateway.GetEmbeddingNotScannedCollection(this.GetWorkingAccessionDate());

            this.m_ScanCount = "Block Count: " + this.m_EmbeddingScanCollection.Count.ToString();

            this.NotifyPropertyChanged("EmbeddingNotScannedList");
            this.NotifyPropertyChanged("EmbeddingScanCollection");
            this.NotifyPropertyChanged("ScanCount");
        }

        private void ButtonAccessionOrderForward_Click(object sender, RoutedEventArgs e)
        {
            this.WorkDate = this.WorkDate.AddDays(1);
            this.m_EmbeddingScanCollection = YellowstonePathology.Business.BarcodeScanning.EmbeddingScanCollection.GetByScanDate(this.m_WorkDate);
            this.m_EmbeddingNotScannedList = Business.Gateway.AccessionOrderGateway.GetEmbeddingNotScannedCollection(this.GetWorkingAccessionDate());

            this.m_ScanCount = "Block Count: " + this.m_EmbeddingScanCollection.Count.ToString();
            this.NotifyPropertyChanged("EmbeddingNotScannedList");
            this.NotifyPropertyChanged("EmbeddingScanCollection");
            this.NotifyPropertyChanged("ScanCount");
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {            
            this.m_BackgroundWorker = new BackgroundWorker();
            this.m_BackgroundWorker.WorkerReportsProgress = true;
            this.m_BackgroundWorker.DoWork += Bgw_DoWork;
            this.m_BackgroundWorker.ProgressChanged += Bgw_ProgressChanged;
            this.m_BackgroundWorker.RunWorkerCompleted += Bgw_RunWorkerCompleted;
            this.m_BackgroundWorker.RunWorkerAsync();                     
        }

        private void Bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                this.m_StatusMessage = "Update complete";
                this.NotifyPropertyChanged("StatusMessage");
            }));
        }

        private void Bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                this.m_StatusMessage = "Updating: " + e.UserState;
                this.NotifyPropertyChanged("StatusMessage");
            }));            
        }

        private void Bgw_DoWork(object sender, DoWorkEventArgs e)
        {            
            YellowstonePathology.Business.Surgical.ProcessorRunCollection processorRunCollection = Business.Surgical.ProcessorRunCollection.GetAll();

            foreach (YellowstonePathology.Business.BarcodeScanning.EmbeddingScan embeddingScan in this.ListViewEmbeddingScans.Items)
            {
                this.m_BackgroundWorker.ReportProgress(0, embeddingScan.AliquotOrderId);

                if (embeddingScan.Updated == false)
                {
                    YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAliquotOrder(embeddingScan.AliquotOrderId, this);
                    aliquotOrder.EmbeddingVerify(YellowstonePathology.Business.User.SystemIdentity.Instance.User);                    

                    Business.ParseSpecimenOrderIdResult parseSpecimenOrderIdResult = aliquotOrder.ParseSpecimenOrderIdFromBlock();
                    if (parseSpecimenOrderIdResult.ParsedSuccessfully == true)
                    {
                        YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullSpecimenOrder(parseSpecimenOrderIdResult.SpecimenOrderId, this);
                        specimenOrder.ProcessorStartTime = embeddingScan.ProcessorStartTime;
                        specimenOrder.ProcessorFixationTime = Convert.ToInt32(embeddingScan.ProcessorFixationDuration.Value.TotalMinutes);
                        specimenOrder.SetFixationEndTime();
                        specimenOrder.SetFixationDuration();                        
                    }
                    else
                    {
                        MessageBox.Show("Unable to parse the Block Id. Please tell Sid.");
                    }

                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                    this.m_AliquotOrderHoldCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAliquotOrderHoldCollection();
                    embeddingScan.Updated = true;
                    this.m_EmbeddingScanCollection.UpdateStatus(embeddingScan);
                }
            }            
        }

        private void ComboBoxProcessorRun_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.ComboBoxProcessorRuns.SelectedItem != null)
            {
                Business.Surgical.ProcessorRun run = (Business.Surgical.ProcessorRun)this.ComboBoxProcessorRuns.SelectedItem;
                this.m_ProcessorStartTime = run.StartTime;
                this.m_ProcessorFixationDuration = run.FixationDuration;
                this.NotifyPropertyChanged(string.Empty);
            }
        }

        private void ContextMenuManualScan_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListViewNotScannedList.SelectedItem != null)
            {
                EmbeddingNotScannedListItem item = (EmbeddingNotScannedListItem)this.ListViewNotScannedList.SelectedItem;
                this.RecieveScan(item.AliquotOrderId);
                this.NotifyPropertyChanged("EmbeddingNotScannedList");
            }
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.m_EmbeddingScanCollection = Business.BarcodeScanning.EmbeddingScanCollection.GetByScanDate(this.m_WorkDate);
            this.m_StatusMessage = "Status: OK";
            this.m_ScanCount = "Block Count: " + this.m_EmbeddingScanCollection.Count.ToString();

            this.m_AliquotOrderHoldCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAliquotOrderHoldCollection();            
            this.m_EmbeddingNotScannedList = Business.Gateway.AccessionOrderGateway.GetEmbeddingNotScannedCollection(this.GetWorkingAccessionDate());
            this.m_EmbeddingBreastCaseList = Business.Gateway.AccessionOrderGateway.GetEmbeddingBreastCasesCollection();

            this.NotifyPropertyChanged(string.Empty);
        }
    }
}
