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

        private Business.BarcodeScanning.EmbeddingScanCollection m_EmbeddingScanCollection;
        private Business.Specimen.Model.AliquotOrderHoldCollection m_AliquotOrderHoldCollection;
        private EmbeddingNotScannedList m_EmbeddingNotScannedList;
        private Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
        private Business.Surgical.ProcessorRunCollection m_ProcessorRunCollection;
        private EmbeddingBreastCaseList m_EmbeddingBreastCaseList;
        private EmbeddingAutopsyList m_EmbeddingAutopsyList;
        private string m_StatusMessage;
        private string m_ScanCount;

        private Business.Test.AccessionOrder m_AccessionOrder;
        private string m_CurrentGrossDescription;

        private List<string> m_AliquotsNotFoundList;

        private Nullable<DateTime> m_ProcessorStartTime;
        private TimeSpan m_ProcessorFixationDuration;

        private BackgroundWorker m_BackgroundWorker;

        public EmbeddingDialog()
        {
            this.m_BarcodeScanPort = Business.BarcodeScanning.BarcodeScanPort.Instance;
            this.m_WorkDate = DateTime.Today;

            this.m_EmbeddingScanCollection = Business.BarcodeScanning.EmbeddingScanCollection.GetByScanDate(this.m_WorkDate);

            this.m_StatusMessage = "Status: OK";
            this.m_ScanCount = "Block Count: " + this.m_EmbeddingScanCollection.Count.ToString();

            this.m_AliquotsNotFoundList = new List<string>();

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

                this.m_AliquotOrderHoldCollection = Business.Gateway.AccessionOrderGateway.GetAliquotOrderHoldCollection();
                this.m_ProcessorRunCollection = Business.Surgical.ProcessorRunCollection.GetAll();

                this.m_EmbeddingNotScannedList = Business.Gateway.AccessionOrderGateway.GetEmbeddingNotScannedCollection(this.GetWorkingAccessionDate());
                this.m_EmbeddingBreastCaseList = Business.Gateway.AccessionOrderGateway.GetEmbeddingBreastCasesCollection();
                this.m_EmbeddingAutopsyList = Business.Gateway.AccessionOrderGateway.GetEmbeddingAutopsyUnverifiedList();
                this.CalculateEstimatedFixationDuration();

                this.NotifyPropertyChanged(string.Empty);
            }
            ));
        }

        private void CalculateEstimatedFixationDuration()
        {
            foreach (EmbeddingBreastCaseListItem item in this.m_EmbeddingBreastCaseList)
            {
                if (item.FixationStartTime.HasValue == true)
                {
                    if (item.FixationEndTime.HasValue == false)
                    {
                        if (DateTime.Today.DayOfWeek == DayOfWeek.Friday)
                        {
                            DateTime sundayAt550 = DateTime.Parse(DateTime.Today.AddDays(2).ToString("yyyy-MM-dd") + "T17:50");
                            Business.Surgical.ProcessorRun run = new Business.Surgical.ProcessorRun("Sunday", sundayAt550, new TimeSpan(2, 30, 0));
                            DateTime expectedFixationEndTime = run.GetFixationEndTime(item.FixationStartTime.Value);
                            TimeSpan expectedDurationTS = expectedFixationEndTime.Subtract(item.FixationStartTime.Value);
                            item.FixationDurationExpected = Convert.ToInt32(Math.Round(expectedDurationTS.TotalHours, 0));
                        }
                        else
                        {
                            DateTime todayAtFive = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd") + "T17:00");
                            Business.Surgical.ProcessorRun run = new Business.Surgical.ProcessorRun("Today", todayAtFive, new TimeSpan(2, 30, 0));
                            DateTime expectedFixationEndTime = run.GetFixationEndTime(item.FixationStartTime.Value);
                            TimeSpan expectedDurationTS = expectedFixationEndTime.Subtract(item.FixationStartTime.Value);
                            item.FixationDurationExpected = Convert.ToInt32(Math.Round(expectedDurationTS.TotalHours, 0));
                        }
                    }
                }
            }
        }

        private void BarcodeScanPort_ContainerScanReceived(Business.BarcodeScanning.ContainerBarcode containerBarcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = Business.Persistence.DocumentGateway.Instance.PullSpecimenOrderByContainerId(containerBarcode.ToString(), this);
                foreach (Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    if (aliquotOrder.Status == "Hold")
                    {
                        aliquotOrder.Status = null;
                    }
                    else
                    {
                        aliquotOrder.Status = "Hold";
                    }
                }

                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
                this.m_AliquotOrderHoldCollection = Business.Gateway.AccessionOrderGateway.GetAliquotOrderHoldCollection();
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
                if (this.ListViewHoldList.SelectedItem != null)
                {
                    YellowstonePathology.Business.Specimen.Model.AliquotOrderHold aliquotOrderHold = (YellowstonePathology.Business.Specimen.Model.AliquotOrderHold)this.ListViewHoldList.SelectedItem;
                    YellowstonePathology.Business.Test.AliquotOrder dbAliquotOrder = Business.Persistence.DocumentGateway.Instance.PullAliquotOrder(aliquotOrderHold.AliquotOrderId, this);
                    dbAliquotOrder.Status = "Created";

                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
                    this.m_AliquotOrderHoldCollection = Business.Gateway.AccessionOrderGateway.GetAliquotOrderHoldCollection();
                    this.NotifyPropertyChanged("AliquotOrderHoldCollection");
                }
            }
            ));
        }

        private void HistologyBlockScanReceived(YellowstonePathology.Business.BarcodeScanning.Barcode barcode)
        {
            if (barcode.ID.Contains("ALQ") == true)
            {
                MessageBox.Show("The scan for this block was not read correctly. Please try again.");
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
                    this.ComboBoxProcessorRuns.Focus();
                    YellowstonePathology.Business.BarcodeScanning.EmbeddingScan result = this.m_EmbeddingScanCollection.HandleScan(aliquotOrderId, this.m_ProcessorStartTime.Value, this.m_ProcessorFixationDuration);
                    this.ListViewEmbeddingScans.SelectedIndex = 0;
                    this.m_ScanCount = "Block Count: " + this.m_EmbeddingScanCollection.Count.ToString();                    
                    string masterAccessionNo = aliquotOrderId.Split('.')[0];
                    this.NotifyPropertyChanged(string.Empty);
                    this.ListViewEmbeddingScans.ScrollIntoView(this.ListViewEmbeddingScans.Items[0]);                }
                else
                {
                    MessageBox.Show("I can't add the scan until a processor start time is entered.");
                }
            }
            ));
        }

        private void GetCurrentGross(string masterAccessionNo)
        {
            this.m_AccessionOrder = Business.Persistence.DocumentGateway.Instance.GetAccessionOrderByMasterAccessionNo(masterAccessionNo);
            Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            if (surgicalTestOrder != null)
            {
                this.m_CurrentGrossDescription = surgicalTestOrder.GrossX;
                this.NotifyPropertyChanged("CurrentGrossDescription");
            }
        }

        public string CurrentGrossDescription
        {
            get { return this.m_CurrentGrossDescription; }
            set { this.m_CurrentGrossDescription = value; }
        }

        private bool IsProcessorStartTimeValid()
        {
            bool result = false;
            if (this.m_ProcessorStartTime.HasValue == true)
            {
                return true;
            }
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

        public EmbeddingAutopsyList EmbeddingAutopsyList
        {
            get { return this.m_EmbeddingAutopsyList; }
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
                if (this.m_WorkDate != value)
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

        public YellowstonePathology.Business.Specimen.Model.AliquotOrderHoldCollection AliquotOrderHoldCollection
        {
            get { return this.m_AliquotOrderHoldCollection; }
        }

        private void ButtonAccessionOrderBack_Click(object sender, RoutedEventArgs e)
        {
            this.WorkDate = this.WorkDate.AddDays(-1);
            this.m_EmbeddingScanCollection = Business.BarcodeScanning.EmbeddingScanCollection.GetByScanDate(this.m_WorkDate);
            this.m_EmbeddingNotScannedList = Business.Gateway.AccessionOrderGateway.GetEmbeddingNotScannedCollection(this.GetWorkingAccessionDate());

            this.m_ScanCount = "Block Count: " + this.m_EmbeddingScanCollection.Count.ToString();

            this.NotifyPropertyChanged("EmbeddingNotScannedList");
            this.NotifyPropertyChanged("EmbeddingScanCollection");
            this.NotifyPropertyChanged("ScanCount");
        }

        private void ButtonAccessionOrderForward_Click(object sender, RoutedEventArgs e)
        {
            this.WorkDate = this.WorkDate.AddDays(1);
            this.m_EmbeddingScanCollection = Business.BarcodeScanning.EmbeddingScanCollection.GetByScanDate(this.m_WorkDate);
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
                    bool aliquotExists = Business.Gateway.AccessionOrderGateway.DoesAliquotExist(embeddingScan.AliquotOrderId);
                    if (aliquotExists == true)
                    {
                        YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = Business.Persistence.DocumentGateway.Instance.PullAliquotOrder(embeddingScan.AliquotOrderId, this);
                        aliquotOrder.EmbeddingVerify(YellowstonePathology.Business.User.SystemIdentity.Instance.User);

                        YellowstonePathology.Business.Facility.Model.Facility thisFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
                        string thisLocation = Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;

                        this.AddMaterialTrackingLog(aliquotOrder, thisFacility, thisLocation);
                        aliquotOrder.SetLocation(thisFacility, thisLocation);

                        Business.ParseSpecimenOrderIdResult parseSpecimenOrderIdResult = aliquotOrder.ParseSpecimenOrderIdFromBlock();
                        if (parseSpecimenOrderIdResult.ParsedSuccessfully == true)
                        {
                            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = Business.Persistence.DocumentGateway.Instance.PullSpecimenOrder(parseSpecimenOrderIdResult.SpecimenOrderId, this);
                            if (specimenOrder.OkToSetProcessorTimes(embeddingScan.ProcessorStartTime) == true)
                            {
                                specimenOrder.ProcessorStartTime = embeddingScan.ProcessorStartTime;
                                specimenOrder.ProcessorFixationTime = Convert.ToInt32(embeddingScan.ProcessorFixationDuration.Value.TotalMinutes);
                                specimenOrder.SetFixationEndTime();
                                specimenOrder.SetFixationDuration();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Unable to parse the Block Id. Please tell Sid.");
                        }

                        YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                        this.m_AliquotOrderHoldCollection = Business.Gateway.AccessionOrderGateway.GetAliquotOrderHoldCollection();
                        embeddingScan.Updated = true;
                        this.m_EmbeddingScanCollection.UpdateStatus(embeddingScan);
                    }
                    else
                    {
                        this.m_AliquotsNotFoundList.Add(embeddingScan.AliquotOrderId);
                    }
                }
            }

            if (this.m_AliquotsNotFoundList.Count > 0)
            {
                StringBuilder msg = new StringBuilder();
                foreach (string id in this.m_AliquotsNotFoundList)
                {
                    msg.AppendLine(id);
                }
                MessageBox.Show("The following Blocks were not found." + Environment.NewLine + msg.ToString());
                this.m_AliquotsNotFoundList.Clear();
            }
        }

        private void AddMaterialTrackingLog(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder, YellowstonePathology.Business.Facility.Model.Facility thisFacility, string thisLocation)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                Business.OrderIdParser orderIdParse = new Business.OrderIdParser(aliquotOrder.AliquotOrderId);

                YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog = new Business.MaterialTracking.Model.MaterialTrackingLog(objectId, aliquotOrder.AliquotOrderId, null, thisFacility.FacilityId, thisFacility.FacilityName,
                    thisLocation, "Block Scanned", "Block Scanned At Embeding", "Block", orderIdParse.MasterAccessionNo, aliquotOrder.Label, aliquotOrder.ClientAccessioned, null);
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingLog, Window.GetWindow(this));
            }));
        }

        private void ComboBoxProcessorRun_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBoxProcessorRuns.SelectedItem != null)
            {
                Business.Surgical.ProcessorRun run = (Business.Surgical.ProcessorRun)this.ComboBoxProcessorRuns.SelectedItem;
                this.m_ProcessorStartTime = run.StartTime;
                this.m_ProcessorFixationDuration = run.FixationDuration;
                this.NotifyPropertyChanged(string.Empty);
            }
        }

        private void ContextMenuManualScan_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewNotScannedList.SelectedItem != null)
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

            this.m_AliquotOrderHoldCollection = Business.Gateway.AccessionOrderGateway.GetAliquotOrderHoldCollection();
            this.m_EmbeddingNotScannedList = Business.Gateway.AccessionOrderGateway.GetEmbeddingNotScannedCollection(this.GetWorkingAccessionDate());
            this.m_EmbeddingBreastCaseList = Business.Gateway.AccessionOrderGateway.GetEmbeddingBreastCasesCollection();
            this.m_EmbeddingAutopsyList = Business.Gateway.AccessionOrderGateway.GetEmbeddingAutopsyUnverifiedList();
            this.CalculateEstimatedFixationDuration();

            this.NotifyPropertyChanged(string.Empty);
        }

        private void ContextMenuAutopsyManualScan_Click(object sender, RoutedEventArgs e)
        {
            foreach (EmbeddingAutopsyItem item in this.ListViewAutopsyScans.SelectedItems)
            {
                this.RecieveScan(item.AliquotOrderId);
            }
        }

        private void ButtonScanId_Click(object sender, RoutedEventArgs e)
        {
            Business.BarcodeScanning.Barcode barcode = new Business.BarcodeScanning.Barcode();
            //barcode.ID = "19-20106.1A";
            //barcode.ID = "19-20106.1B";
            barcode.ID = "19-23403.1A";
            this.HistologyBlockScanReceived(barcode);
        }

        private void ListViewScans_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListViewEmbeddingScans.SelectedItem != null)
            {
                Business.BarcodeScanning.EmbeddingScan embeddingScan = this.ListViewEmbeddingScans.SelectedItem as Business.BarcodeScanning.EmbeddingScan;
                string masterAccessionNo = embeddingScan.AliquotOrderId.Split('.')[0];
                this.GetCurrentGross(masterAccessionNo);
            }
        }

        private void ButtonPrintHoldList_Click(object sender, RoutedEventArgs e)
        {            
            Business.XPSDocument.Result.Xps.HoldListReport report = new Business.XPSDocument.Result.Xps.HoldListReport(this.m_AliquotOrderHoldCollection);
            XpsDocumentViewer xpsDocumentViewer = new XpsDocumentViewer();
            xpsDocumentViewer.LoadDocument(report.FixedDocument);
            xpsDocumentViewer.ShowDialog();
        }
    }
}