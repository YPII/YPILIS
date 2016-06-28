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
using System.IO;
using System.ComponentModel;

namespace YellowstonePathology.UI.Test
{
    public partial class TecanImportExportPage : UserControl, INotifyPropertyChanged 
	{
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void NextEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.ExcelSpreadsheetReturnEventArgs e);
        public event NextEventHandler Next;

        public delegate void CloseEventHandler(object sender, EventArgs e);
        public event CloseEventHandler Close;

        private Microsoft.Office.Interop.Excel.Application m_ExcelApplication;
        private Microsoft.Office.Interop.Excel.Workbook m_WorkBook;
        private YellowstonePathology.Business.User.UserPreference m_UserPreference;        
        private List<string> m_FileList;
        private Visibility m_NextButtonVisibility;
        private Visibility m_CloseButtonVisibility;
		private YellowstonePathology.Business.Search.ReportSearchList m_CaseList;

		public TecanImportExportPage(YellowstonePathology.Business.Search.ReportSearchList caseList, Visibility nextButtonVisibility, Visibility closeButtonVisibility)
		{
            this.m_CaseList = caseList;
            this.m_NextButtonVisibility = nextButtonVisibility;
            this.m_CloseButtonVisibility = closeButtonVisibility;

            this.m_UserPreference = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference;			
            this.BuildFileList();

			InitializeComponent();
			DataContext = this;            
		}

        public Visibility NextButtonVisibility
        {
            get { return this.m_NextButtonVisibility; }
        }

        public Visibility CloseButtonVisibility
        {
            get { return this.m_CloseButtonVisibility; }
        }

        private void BuildFileList()
        {
            this.m_FileList = new List<string>();
            if (string.IsNullOrEmpty(this.m_UserPreference.TecanImportExportPath) == false)
            {
                string [] files = System.IO.Directory.GetFiles(this.m_UserPreference.TecanImportExportPath);
                foreach (string file in files)
                {
                    this.m_FileList.Add(file);
                }
            }
            this.NotifyPropertyChanged("FileList");
        }

        public YellowstonePathology.Business.User.UserPreference UserPreference
        {
            get { return this.m_UserPreference; }
        }

        public List<string> FileList
        {
            get { return this.m_FileList; }
        }        

        public bool OkToSaveOnNavigation(Type pageNavigatingTo)
        {
            return true;
        }

        public bool OkToSaveOnClose()
        {
            return true;
        }

        public void Save(bool releaseLock)
        {
            
        }

        public void UpdateBindingSources()
        {

        }
        
		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            //YellowstonePathology.Business.Persistence.DocumentGateway.Instance.SubmitChanges(this.m_UserPreference, false);            
            if (this.Next != null) this.Next(this, new YellowstonePathology.UI.CustomEventArgs.ExcelSpreadsheetReturnEventArgs(this.m_ExcelApplication, this.m_WorkBook));
		}                

        private void HyperLinkOpenSpreadsheet_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListBoxFileList.SelectedItem != null)
            {
                this.m_ExcelApplication = new Microsoft.Office.Interop.Excel.Application();
                this.m_ExcelApplication.Visible = true;
                this.m_ExcelApplication.DisplayAlerts = true;
                this.m_WorkBook = this.m_ExcelApplication.Workbooks.Open(this.ListBoxFileList.SelectedItem.ToString());
                Microsoft.Office.Interop.Excel.Worksheet workSheet = this.m_WorkBook.Sheets["SamplePlacement"];
                workSheet.Select(Type.Missing);
            }
            else
            {
                MessageBox.Show("Please select a file to open.");
            }
        }

        private void HyperLinkWriteWellData_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_CaseList != null)
            {
                if (this.m_WorkBook != null)
                {
                    Microsoft.Office.Interop.Excel.Worksheet workSheet = this.m_WorkBook.Sheets["SamplePlacement"];
                    workSheet.Select(Type.Missing);

                    TecanSamplePlacementQueue tecanSamplePlacementQueue = new TecanSamplePlacementQueue();
                    for (int i = this.m_CaseList.Count - 1; i >= 0; i--)
                    {
                        if (tecanSamplePlacementQueue.Queue.Count != 0)
                        {
                            TecanSample tecanSample = tecanSamplePlacementQueue.Queue.Dequeue();
                            workSheet.Cells[tecanSample.WellCell.RowIndex, tecanSample.WellCell.ColumnIndex] = TecanSample.GetWellCellValue(this.m_CaseList[i].ReportNo, this.m_CaseList[i].PLastName);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The spread sheet doesn't appear to be open.");
                }
            }
            else
            {
                MessageBox.Show("The well data cannot be written in this mode.");
            }
        }

        private void ButtonTecanImportExportPathBrowse_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.Description = "Custom Description";

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.m_UserPreference.TecanImportExportPath = fbd.SelectedPath;
                this.NotifyPropertyChanged("TecanImportExportPath");
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {            
            //YellowstonePathology.Business.Persistence.DocumentGateway.Instance.SubmitChanges(this.m_UserPreference, true);
            if (this.Close != null) this.Close(this, new EventArgs());
        }

        private void HyperLinkOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.m_UserPreference.TecanImportExportPath) == false)
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("Explorer.exe", this.m_UserPreference.TecanImportExportPath);
                p.StartInfo = info;
                p.Start();
            }
            else
            {
                MessageBox.Show("The folder path has not been set.");
            }
        }

        private void HyperLinkRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.BuildFileList();
        }
	}
}
