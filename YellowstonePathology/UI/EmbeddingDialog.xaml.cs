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

        private YellowstonePathology.Business.Test.AliquotOrderCollection m_AliquotOrderCollection;
        private YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection m_SpecimenOrderHoldCollection;
        private YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection m_SpecimenOrderCollection;
        private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
        private YellowstonePathology.Business.Surgical.ProcessorRunCollection m_ProcessorRunCollection;

        public EmbeddingDialog()
        {
            this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;
            this.m_WorkDate = DateTime.Today;            

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

        private void EmbeddingDialog_Loaded(object sender, RoutedEventArgs e)
        {            
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                this.m_BarcodeScanPort.HistologyBlockScanReceived += this.HistologyBlockScanReceived;
                this.m_BarcodeScanPort.ContainerScanReceived += BarcodeScanPort_ContainerScanReceived;

                this.m_AliquotOrderCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetEmbeddingCollection(this.m_WorkDate);
                this.m_SpecimenOrderHoldCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSpecimenOrderHoldCollection();
                this.m_SpecimenOrderCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSpecimenOrderCollection(this.m_WorkDate);
                this.m_ProcessorRunCollection = YellowstonePathology.Business.Surgical.ProcessorRunCollection.GetAll(false);

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
                this.HandleVerification(barcode.ID);
            }
            ));
        }

        private void HandleVerification(string aliquotOrderId)
        {            
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAliquotOrder(aliquotOrderId, this);            
            aliquotOrder.EmbeddingVerify(YellowstonePathology.Business.User.SystemIdentity.Instance.User);            

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullSpecimenOrder(aliquotOrderId, this);
            YellowstonePathology.Business.Surgical.ProcessorRun processorRun = (YellowstonePathology.Business.Surgical.ProcessorRun)this.ComboBoxProcessorRuns.SelectedItem;
            specimenOrder.ProcessorRun = processorRun.Name;
            specimenOrder.ProcessorRunId = processorRun.ProcessorRunId;

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();

            this.m_AliquotOrderCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetEmbeddingCollection(this.m_WorkDate);
            this.m_SpecimenOrderCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSpecimenOrderCollection(this.m_WorkDate);
            this.m_SpecimenOrderHoldCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSpecimenOrderHoldCollection();

            this.NotifyPropertyChanged(string.Empty);
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

        public YellowstonePathology.Business.Test.AliquotOrderCollection AliquotOrderCollection
        {
            get { return this.m_AliquotOrderCollection; }
        }

        public YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection SpecimenOrderHoldCollection
        {
            get { return this.m_SpecimenOrderHoldCollection; }
        }

        public YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection SpecimenOrderCollection
        {
            get { return this.m_SpecimenOrderCollection; }
        }

        private void ButtonAccessionOrderBack_Click(object sender, RoutedEventArgs e)
        {
            this.WorkDate = this.WorkDate.AddDays(-1);
            this.m_AliquotOrderCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetEmbeddingCollection(this.WorkDate);
            this.m_SpecimenOrderCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSpecimenOrderCollection(this.m_WorkDate);

            this.NotifyPropertyChanged("AliquotOrderCollection");
            this.NotifyPropertyChanged("SpecimenOrderCollection");
        }

        private void ButtonAccessionOrderForward_Click(object sender, RoutedEventArgs e)
        {
            this.WorkDate = this.WorkDate.AddDays(1);
            this.m_AliquotOrderCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetEmbeddingCollection(this.WorkDate);
            this.m_SpecimenOrderCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSpecimenOrderCollection(this.m_WorkDate);

            this.NotifyPropertyChanged("AliquotOrderCollection");
            this.NotifyPropertyChanged("SpecimenOrderCollection");
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }        
    }
}
