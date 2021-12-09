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
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
using System.ComponentModel;
using System.Diagnostics;

namespace YellowstonePathology.UI.ManagementReports
{    
    public partial class ReportSelection : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ManagementReports.ManagementReportCollection m_ManagementReportCollection;
        private List<string> m_DocumentList;

        public ReportSelection()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            this.m_ManagementReportCollection = new ManagementReportCollection();            

            InitializeComponent();

            this.DataContext = this;
        }

        public void BuilDocumentList()
        {
            string path = @"\\fileserver\YPI Shared Documents\Management Reports\COVID\DepartmentOfHealthDailyCOVIDCases";
            string[] files = System.IO.Directory.GetFiles(path);
            this.m_DocumentList = new List<string>();
            foreach(string file in files)
            {
                string extension = System.IO.Path.GetExtension(file);
                if(extension == ".xlsx")
                {
                    this.m_DocumentList.Add(System.IO.Path.GetFileName(file));
                }            
            }
            this.NotifyPropertyChanged("DocumentList");
        }

        private void ListViewManagementReports_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListViewManagementReports.SelectedItem != null)
            {
                ManagementReport managementReport = (ManagementReport)this.ListViewManagementReports.SelectedItem;
                DepartmentOfHealthDailyCOVID report = new DepartmentOfHealthDailyCOVID(this);
                this.ContentControlReport.Content = report;
                this.BuilDocumentList();
            }
        }

        public ManagementReports.ManagementReportCollection ManagementReportCollection
        {
            get { return this.m_ManagementReportCollection; }
        }

        public List<string> DocumentList
        {
            get { return this.m_DocumentList; }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ListViewDocuments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            string path = @"\\fileserver\YPI Shared Documents\Management Reports\COVID\DepartmentOfHealthDailyCOVIDCases\";
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("Explorer.exe", path);
            p.StartInfo = info;
            p.Start();
        }

        private void ButtonOpenSheet_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListViewDocuments.SelectedItem != null)
            {
                string name = (string)this.ListViewDocuments.SelectedItem;
                string fileName = $@"\\fileserver\YPI Shared Documents\Management Reports\COVID\DepartmentOfHealthDailyCOVIDCases\{name}";
                Process p = new Process();
                ProcessStartInfo info = new ProcessStartInfo(fileName);
                p.StartInfo = info;
                p.Start();
            }            
        }
    }
}
