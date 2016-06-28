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
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for TumerRegistryDistributionDialog.xaml
    /// </summary>
    public partial class TumerRegistryDistributionDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionCollection m_TestOrderReportDistributionCollection;
        Nullable<DateTime> m_StartDate;
        Nullable<DateTime> m_EndDate;
        string m_DistributionType;
        string m_LocalFolderPath;

        public TumerRegistryDistributionDialog()
        {
            this.m_DistributionType = "WYDOH";
            this.m_StartDate = DateTime.Parse(DateTime.Today.AddMonths(-1).Month +  "/1/" + DateTime.Today.AddMonths(-1).Year);
            this.m_EndDate = DateTime.Parse(DateTime.Today.AddMonths(-1).Month + "/" + this.m_StartDate.Value.AddMonths(1).AddDays(-1).Day + "/" + DateTime.Today.AddMonths(-1).Year);
            this.m_LocalFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            this.m_TestOrderReportDistributionCollection = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionCollection();

            InitializeComponent();

            this.DataContext = this;
        }        

        public Nullable<DateTime> StartDate
        {
            get { return this.m_StartDate; }
            set { this.m_StartDate = value; }
        }

        public Nullable<DateTime> EndDate
        {
            get { return this.m_EndDate; }
            set { this.m_EndDate = value; }
        }

        public string DistributionType
        {
            get { return this.m_DistributionType; }
            set { this.m_DistributionType = value; }
        }

        public string LocalFolderPath
        {
            get { return this.m_LocalFolderPath; }
            set { this.m_LocalFolderPath = value; }
        }

        public YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionCollection TestOrderReportDistributionCollection
        {
            get { return this.m_TestOrderReportDistributionCollection; }
        }

        private void ButtonGetDistributions_Click(object sender, RoutedEventArgs e)
        {
            this.m_TestOrderReportDistributionCollection = YellowstonePathology.Business.Gateway.ReportDistributionGateway.GetReportDistributionCollectionByDateRangeTumorRegistry(this.m_StartDate.Value, this.m_EndDate.Value, this.m_DistributionType);
            this.NotifyPropertyChanged("TestOrderReportDistributionCollection");
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonCopyLocal_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.m_LocalFolderPath) == false)
            {
                if (this.m_TestOrderReportDistributionCollection.Count != 0)
                {
                    string localFolderName = this.m_LocalFolderPath + @"\" + this.m_DistributionType + DateTime.Today.Month.ToString().PadLeft(2, '0') + DateTime.Today.Day.ToString().PadLeft(2, '0');
                    if (System.IO.File.Exists(localFolderName) == true)
                    {
                        System.IO.Directory.Delete(localFolderName, true);
                    }
                    System.IO.Directory.CreateDirectory(localFolderName);

                    foreach (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution in this.m_TestOrderReportDistributionCollection)
                    {
						YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(testOrderReportDistribution.ReportNo);
						string filePath = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser);
                        string localReportNoDirectoryPath = localFolderName + @"\" + testOrderReportDistribution.ReportNo;
                        System.IO.Directory.CreateDirectory(localReportNoDirectoryPath);

                        string[] files = System.IO.Directory.GetFiles(filePath);
                        foreach (string file in files)
                        {                        
                            string serverFileNameOnly = System.IO.Path.GetFileName(file);
                            System.IO.File.Copy(file, localReportNoDirectoryPath + @"\" + serverFileNameOnly, true);
                        }
                    }
                    MessageBox.Show("The files have been copied to the local machine.");
                }
                else
                {
                    MessageBox.Show("There are no distributions in the list to copy.");
                }
            }
            else
            {
                MessageBox.Show("You must select a local folder path.");
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonFileBrowserDialog_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();            
            folderBrowserDialog.ShowDialog();
            this.m_LocalFolderPath = folderBrowserDialog.SelectedPath;
            this.NotifyPropertyChanged("LocalFolderPath");
        }
    }
}
