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

namespace YellowstonePathology.UI.COVID
{   
    public partial class COVIDTestingDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;        
        
        private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
        private bool m_LoadedHasRun;

        private LC96SampleCollection m_LC96SampleCollection;
        private COVIDResultCollection m_COVIDResultCollection;

        private COVIDCaseCollection m_COVIDCaseCollection;
        private COVIDCaseCollection m_RecentCaseCollection;
        private string m_CategorySearchType;

        private Login.Receiving.LoginPageWindow m_LoginPageWindow;

        public COVIDTestingDialog()
        {
            this.m_CategorySearchType = "Not Run";
            this.m_LoadedHasRun = false;
            this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;            

            this.m_COVIDCaseCollection = COVIDCaseCollection.GetNotRunCOVIDCases();
            this.m_RecentCaseCollection = COVIDCaseCollection.GetRecentCOVIDCases();

            InitializeComponent();

            this.Loaded += COVIDTestingDialog_Loaded;
            this.Unloaded += COVIDTestingDialog_Unloaded;
            this.DataContext = this;
        }

        public string CategorySearchType
        {
            get { return this.m_CategorySearchType; }
            set
            {
                if (this.m_CategorySearchType != value)
                {
                    this.m_CategorySearchType = value;
                    this.NotifyPropertyChanged("CategorySearchType");
                }
            }
        }

        public COVIDCaseCollection COVIDCaseCollection
        {
            get { return this.m_COVIDCaseCollection; }
        }

        private void COVIDTestingDialog_Unloaded(object sender, RoutedEventArgs e)
        {
            this.m_BarcodeScanPort.ReportNoScanReceived -= BarcodeScanPort_ReportNoScanReceived;
        }

        private void COVIDTestingDialog_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.m_LoadedHasRun == false)
            {
                this.m_BarcodeScanPort.ReportNoScanReceived -= BarcodeScanPort_ReportNoScanReceived;
                this.m_BarcodeScanPort.ReportNoScanReceived += BarcodeScanPort_ReportNoScanReceived;
            }

            this.m_LoadedHasRun = true;
        }

        private void BarcodeScanPort_ReportNoScanReceived(Business.BarcodeScanning.Barcode barcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                if(this.m_LC96SampleCollection != null)
                {
                    if (this.m_COVIDCaseCollection.ReportNoExists(barcode.ID) == true)
                    {
                        COVIDCase covidCase = this.m_RecentCaseCollection.GetByReportNo(barcode.ID);
                        string patientName = $"{covidCase.PLastName}, {covidCase.PFirstName}";
                        this.m_LC96SampleCollection.SetNextSampleName(barcode.ID, patientName);
                    }
                    else
                    {
                        MessageBox.Show("I'm not able to find the ReportNo that was scanned.");
                    }
                }                
            }));
        }

        public LC96SampleCollection LC96SampleCollection
        {
            get { return this.m_LC96SampleCollection; }
        }

        public COVIDResultCollection COVIDResultCollection
        {
            get { return this.m_COVIDResultCollection; }
        }

        private void ButtonImport_Click(object sender, RoutedEventArgs e)
        {
            this.m_LC96SampleCollection = LC96SampleCollection.Import().GetAllCOV2();
            this.m_LC96SampleCollection.Padd();
            this.NotifyPropertyChanged(string.Empty);
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonImportResults_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                this.m_COVIDResultCollection = COVIDResultCollection.Import(openFileDialog.FileName).GetAllCOV2();
                this.NotifyPropertyChanged(string.Empty);
            }                       
        }

        private void ButtonScanSimulation_Click(object sender, RoutedEventArgs e)
        {
            List<string> reportNos = new List<string>();
            reportNos.Add("RPTN20-28750.M1");
            reportNos.Add("RPTN20-28749.M1");
            reportNos.Add("RPTN20-28748.M1");
            reportNos.Add("RPTN20-28746.M1");           

            foreach (string reportNo in reportNos)
            {
                this.m_BarcodeScanPort.SimulateScanReceived(reportNo);
            }
        }        

        private void ButtonUpdateResults_Click(object sender, RoutedEventArgs e)
        {
            COVIDResultCollection.UpdateResults(this.m_COVIDResultCollection, this);
            MessageBox.Show("The results have been updated.");
        }

        private void ButtonLoadSampleList_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ButtonParseSpecialInstructions_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            foreach(COVIDCase covidCase in this.m_COVIDCaseCollection)
            {
                Business.ClientOrder.Model.ClientOrder clientOrder = Business.Persistence.DocumentGateway.Instance.PullClientOrder(covidCase.ClientOrderId, this);
                if (string.IsNullOrEmpty(clientOrder.SpecialInstructions) == false)
                {                    
                    string q1 = "Employed in Healthcare Setting?: Yes";
                    if (clientOrder.SpecialInstructions.Contains(q1)) clientOrder.EmployedInHealthcare = true;

                    string q2 = "Symptomatic for COVID - 19 as defined by CDC ?: Yes";
                    if (clientOrder.SpecialInstructions.Contains(q2)) clientOrder.Symptomatic = true;

                    string q3 = "Resident in a congregate(group) care setting ?: Yes";
                    if (clientOrder.SpecialInstructions.Contains(q3)) clientOrder.ResidentInCongregateCare = true;

                    string q4 = "Pregnant ?: Yes";
                    if (clientOrder.SpecialInstructions.Contains(q4)) clientOrder.Pregnant = true;                    
                }
                count += 1;
            }

            MessageBox.Show("All done.");
            Business.Persistence.DocumentGateway.Instance.Push(this);
        }

        private void ButtonCategorySearchType_Click(object sender, RoutedEventArgs e)
        {
            if(this.CombBoxCategorySearchType.SelectedItem != null)
            {
                switch(this.m_CategorySearchType)
                {
                    case "Not Run":
                        this.m_COVIDCaseCollection = COVIDCaseCollection.GetNotRunCOVIDCases();
                        break;
                    case "Recent Cases":
                        this.m_COVIDCaseCollection = COVIDCaseCollection.GetRecentCOVIDCases();
                        break;
                    case "Detected Cases":
                        this.m_COVIDCaseCollection = COVIDCaseCollection.GetDetectedCases();
                        break;
                    case "Late Cases":
                        this.m_COVIDCaseCollection = COVIDCaseCollection.GetLateCases();
                        break;
                }
                this.NotifyPropertyChanged(string.Empty);
            }
        }

        private void MenuItemResult_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewCOVIDCases.SelectedItem != null)
            {
                COVIDCase covidCase = (COVIDCase)this.ListViewCOVIDCases.SelectedItem;

                this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(covidCase.MasterAccessionNo, this);
                this.m_LoginPageWindow.Show();

                UI.Test.SARSCoV2ResultPath sarsResultPath = new Test.SARSCoV2ResultPath(covidCase.ReportNo, accessionOrder, this.m_LoginPageWindow.PageNavigator, this);
                sarsResultPath.Finish += SarsResultPath_Finish;
                sarsResultPath.Start();
            }
        }

        private void SarsResultPath_Finish(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        private void MenuItemExportLC480Samples_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemExportLC96Samples_Click(object sender, RoutedEventArgs e)
        {            
            this.m_LC96SampleCollection.ExportLC96Samples();
            MessageBox.Show("The samples have been exported.");
        }

        private void HyperLinkCreateLC96Plate_Click(object sender, RoutedEventArgs e)
        {
            this.m_LC96SampleCollection = LC96SampleCollection.GetTemplate();
            this.NotifyPropertyChanged(string.Empty);
        }

        private void HyperLinkExportLC96Plate_Click(object sender, RoutedEventArgs e)
        {
            if(this.m_LC96SampleCollection != null)
            {
                this.m_LC96SampleCollection.ExportLC96Samples();
                MessageBox.Show("The plate has been exported.");
            }
            else
            {
                MessageBox.Show("This plate map has not been initialized.");
            }
        }

        private void HyperLinkImportLC96Plate_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                LC96SampleCollection imported = LC96SampleCollection.Import(openFileDialog.FileName);
                LC96SampleCollection.FillSampleCollection(this.m_LC96SampleCollection, imported);
                this.NotifyPropertyChanged(string.Empty);
            }
        }
    }
}
