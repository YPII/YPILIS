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
        private YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection m_SpecimenOrderHoldCollection;
        private EmbeddingNotScannedList m_EmbeddingNotScannedList;
        private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
        private YellowstonePathology.Business.Surgical.ProcessorRunCollection m_ProcessorRunCollection;
        private EmbeddingBreastCaseList m_EmbeddingBreastCaseList;
        private string m_StatusMessage;
        private string m_ScanCount;
        private List<DateTime> m_ProcessorStartHourList;

        private BackgroundWorker m_BackgroundWorker;

        public EmbeddingDialog()
        {            
            this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;
            this.m_WorkDate = DateTime.Today;
            this.m_EmbeddingScanCollection = Business.BarcodeScanning.EmbeddingScanCollection.GetByScanDate(this.m_WorkDate);                       
            
            this.m_StatusMessage = "Status: OK";
            this.m_ScanCount = "Block Count: " + this.m_EmbeddingScanCollection.Count.ToString();

            this.m_ProcessorStartHourList = new List<DateTime>();
            this.m_ProcessorStartHourList.Add(DateTime.Parse(DateTime.Today.ToShortDateString() + " 04:00"));
            this.m_ProcessorStartHourList.Add(DateTime.Parse(DateTime.Today.ToShortDateString() + " 05:00"));
            this.m_ProcessorStartHourList.Add(DateTime.Parse(DateTime.Today.ToShortDateString() + " 06:00"));
            this.m_ProcessorStartHourList.Add(DateTime.Parse(DateTime.Today.ToShortDateString() + " 07:00"));
            this.m_ProcessorStartHourList.Add(DateTime.Parse(DateTime.Today.ToShortDateString() + " 08:00"));
            this.m_ProcessorStartHourList.Add(DateTime.Parse(DateTime.Today.ToShortDateString() + " 09:00"));
            this.m_ProcessorStartHourList.Add(DateTime.Parse(DateTime.Today.ToShortDateString() + " 10:00"));
            this.m_ProcessorStartHourList.Add(DateTime.Parse(DateTime.Today.ToShortDateString() + " 10:00"));
            this.m_ProcessorStartHourList.Add(DateTime.Parse(DateTime.Today.ToShortDateString() + " 11:00"));
            this.m_ProcessorStartHourList.Add(DateTime.Parse(DateTime.Today.ToShortDateString() + " 12:00"));
            this.m_ProcessorStartHourList.Add(DateTime.Parse(DateTime.Today.ToShortDateString() + " 13:00"));

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
                
                this.m_SpecimenOrderHoldCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSpecimenOrderHoldCollection();                
                this.m_ProcessorRunCollection = YellowstonePathology.Business.Surgical.ProcessorRunCollection.GetAll(false);

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
                YellowstonePathology.Business.Surgical.HoldProcessor holdProcessor = new Business.Surgical.HoldProcessor();
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullSpecimenOrderByContainerId(containerBarcode.ToString(), this);
                specimenOrder.ProcessorRun = holdProcessor.ProcessorRunCollection[0].Name;
                specimenOrder.ProcessorRunId = holdProcessor.ProcessorRunCollection[0].ProcessorRunId;
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);

                this.m_SpecimenOrderHoldCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSpecimenOrderHoldCollection();
                this.NotifyPropertyChanged("SpecimenOrderHoldCollection");

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
                    YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = (YellowstonePathology.Business.Specimen.Model.SpecimenOrder)this.ListViewHoldList.SelectedItem;                    
                    YellowstonePathology.Business.Specimen.Model.SpecimenOrder dbSpecimenOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullSpecimenOrder(specimenOrder.SpecimenOrderId, this);
                    dbSpecimenOrder.ProcessorRun = null;
                    dbSpecimenOrder.ProcessorRunId = null;
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
                    this.m_SpecimenOrderHoldCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSpecimenOrderHoldCollection();
                    this.NotifyPropertyChanged("SpecimenOrderHoldCollection");
                }                
            }
            ));
        }

        private void HistologyBlockScanReceived(YellowstonePathology.Business.BarcodeScanning.Barcode barcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                if(barcode.ID.Contains("ALQ") == true)
                {
                    MessageBox.Show("The scan for this block was read correctly. Please try again.");
                }
                else
                {
                    YellowstonePathology.Business.Surgical.ProcessorRun processorRun = (YellowstonePathology.Business.Surgical.ProcessorRun)this.ComboBoxProcessorRuns.SelectedItem;                    
                    if (processorRun is Business.Surgical.CheechTodayShortMini)
                    {
                        if (this.ComboBoxProcessorStartHour.SelectedIndex < 0)
                        {
                            MessageBox.Show("You must select a start time from the combo box in the top right hand corner of the window.");
                            return;
                        }
                        else
                        {
                            DateTime startTime = (DateTime)this.ComboBoxProcessorStartHour.SelectedItem;
                            processorRun.StartTime = new TimeSpan(startTime.Hour, 0, 0);
                        }
                    }

                    YellowstonePathology.Business.BarcodeScanning.EmbeddingScan result = this.m_EmbeddingScanCollection.HandleScan(barcode.ID, processorRun.ProcessorRunId, processorRun.Name);
                    this.ListViewEmbeddingScans.SelectedIndex = 0;
                    this.m_ScanCount = "Block Count: " + this.m_EmbeddingScanCollection.Count.ToString();
                    this.NotifyPropertyChanged("ScanCount");
                }                
            }
            ));
        }   
        
        public string StatusMessage
        {
            get { return this.m_StatusMessage; }
        }

        public string ScanCount
        {
            get { return this.m_ScanCount; }
        }

        public List<DateTime> ProcessorStartHourList
        {
            get { return this.m_ProcessorStartHourList; }
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

        public YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection SpecimenOrderHoldCollection
        {
            get { return this.m_SpecimenOrderHoldCollection; }
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
            YellowstonePathology.Business.Surgical.ProcessorRunCollection processorRunCollection = Business.Surgical.ProcessorRunCollection.GetAll(false);

            foreach (YellowstonePathology.Business.BarcodeScanning.EmbeddingScan embeddingScan in this.ListViewEmbeddingScans.Items)
            {
                this.m_BackgroundWorker.ReportProgress(0, embeddingScan.AliquotOrderId);

                if (embeddingScan.Updated == false)
                {
                    YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAliquotOrder(embeddingScan.AliquotOrderId, this);
                    aliquotOrder.EmbeddingVerify(YellowstonePathology.Business.User.SystemIdentity.Instance.User);

                    YellowstonePathology.Business.Surgical.ProcessorRun processorRun = processorRunCollection.Get(embeddingScan.ProcessorRunId);

                    Business.ParseSpecimenOrderIdResult parseSpecimenOrderIdResult = aliquotOrder.ParseSpecimenOrderIdFromBlock();
                    if (parseSpecimenOrderIdResult.ParsedSuccessfully == true)
                    {
                        YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullSpecimenOrder(parseSpecimenOrderIdResult.SpecimenOrderId, this);
                        specimenOrder.ProcessorRun = embeddingScan.ProcessorRun;
                        specimenOrder.ProcessorRunId = embeddingScan.ProcessorRunId;
                        specimenOrder.ProcessorStartTime = processorRun.GetProcessorStartTime(specimenOrder.DateReceived);                        
                        specimenOrder.ProcessorFixationTime = Convert.ToInt32(processorRun.FixationTime.TotalMinutes);                        
                        specimenOrder.SetFixationEndTime();
                        specimenOrder.SetFixationDuration();
                    }
                    else
                    {
                        MessageBox.Show("Unable to parse the Block Id. Please tell Sid.");
                    }

                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                    this.m_SpecimenOrderHoldCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSpecimenOrderHoldCollection();
                    embeddingScan.Updated = true;
                    this.m_EmbeddingScanCollection.UpdateStatus(embeddingScan);
                }
            }            
        }
    }
}
