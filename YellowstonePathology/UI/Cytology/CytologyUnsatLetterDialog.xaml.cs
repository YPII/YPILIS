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

namespace YellowstonePathology.UI.Cytology
{
	/// <summary>
	/// Interaction logic for CytologyUnsatLetterDdialog.xaml
	/// </summary>
	public partial class CytologyUnsatLetterDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime m_StartDate;
		private DateTime m_EndDate;
		private int m_OpenCaseCount;

        private string m_FolderLocation;
        private string m_StatusMessage;
        private BackgroundWorker m_BackgroundWorker;

        public CytologyUnsatLetterDialog()
		{            
            DateTime firstDayOfThisMonth = DateTime.Parse(DateTime.Today.Month.ToString() + "/1/" + DateTime.Today.Year.ToString());
            this.m_StartDate = firstDayOfThisMonth.AddMonths(-1);
            this.m_EndDate = firstDayOfThisMonth.AddDays(-1);
            this.m_OpenCaseCount = Business.Gateway.AccessionOrderGateway.GetCountOpenCytologyCasesByCollectionDateRange(this.m_StartDate, this.m_EndDate);
            this.m_FolderLocation = @"\\CFileServer\Documents\Reports\Cytology\CytologyAbnormalUnsatLetter";

            InitializeComponent();
			DataContext = this;
		}

		public DateTime StartDate
		{
			get { return this.m_StartDate; }
			set { this.m_StartDate = value;}
		}

		public DateTime EndDate
		{
			get { return this.m_EndDate; }
			set { this.m_EndDate = value;}
		}

        public int OpenCaseCount
        {
            get { return this.m_OpenCaseCount; }
        }

        public string FolderLocation
        {
            get { return this.m_FolderLocation; }
        }

        public string StatusMessage
        {
            get { return this.m_StatusMessage; }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        /*private void ButtonOk_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}*/

		private void ButtonNavigateToFolder_Click(object sender, RoutedEventArgs e)
		{
			//string reportFolderPath = @"\\CFileServer\Documents\Reports\Cytology\CytologyAbnormalUnsatLetter";
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			System.Diagnostics.Process p = new System.Diagnostics.Process();
			System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("Explorer.exe", this.m_FolderLocation);
			p.StartInfo = info;
			p.Start();
		}

		/*private void ButtonOpenCaseCount_Click(object sender, RoutedEventArgs e)
		{
			this.m_OpenCaseCount = Business.Gateway.AccessionOrderGateway.GetCountOpenCytologyCasesByCollectionDateRange(this.m_StartDate, this.m_EndDate);
			this.TextBlockOpenCaseCount.Text = this.m_OpenCaseCount.ToString();
		}*/

		private void ButtonCreateLetters_Click(object sender, RoutedEventArgs e)
		{
            this.m_BackgroundWorker = new BackgroundWorker();
            this.m_BackgroundWorker.WorkerReportsProgress = true;
            this.m_BackgroundWorker.DoWork += BackgroundWorker_CreateLetters;
            this.m_BackgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            this.m_BackgroundWorker.RunWorkerCompleted += BackgroundWorker_CreateLettersRunWorkerCompleted;
            this.m_BackgroundWorker.RunWorkerAsync();
		}

        private void BackgroundWorker_CreateLetters(object sender, DoWorkEventArgs e)
        {
            YellowstonePathology.Reports.Cytology.CytologyAbnormalUnsatLetter unsatLetters = new YellowstonePathology.Reports.Cytology.CytologyAbnormalUnsatLetter(this.m_StartDate, this.m_EndDate);
            unsatLetters.ClearFolder();
            YellowstonePathology.Business.Reports.Cytology.CytologyUnsatLetters cytologyUnsatLetters = unsatLetters.CytologyUnsatLetters;
            foreach (YellowstonePathology.Business.Reports.Cytology.CytologyUnsatLetterItem item in cytologyUnsatLetters)
            {
                this.m_BackgroundWorker.ReportProgress(0, "Creating letter for: " + item.ClientName + " - " + item.PhysicianName);
                unsatLetters.CreateReport(item);
            }
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                this.m_StatusMessage = e.UserState.ToString();
                this.NotifyPropertyChanged("StatusMessage");
            }));
        }

        private void BackgroundWorker_CreateLettersRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                this.m_StatusMessage = "Letters Created.";
                this.NotifyPropertyChanged("StatusMessage");
            }));
        }

        private void ButtonFaxLetters_Click(object sender, RoutedEventArgs e)
		{
            this.m_BackgroundWorker = new BackgroundWorker();
            this.m_BackgroundWorker.WorkerReportsProgress = true;
            this.m_BackgroundWorker.DoWork += BackgroundWorker_FaxLetters;
            this.m_BackgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            this.m_BackgroundWorker.RunWorkerCompleted += BackgroundWorker_FaxLettersRunWorkerCompleted;
            this.m_BackgroundWorker.RunWorkerAsync();
		}

        private void BackgroundWorker_FaxLetters(object sender, DoWorkEventArgs e)
        {
            YellowstonePathology.Reports.Cytology.CytologyAbnormalUnsatLetter unsatLetters = new YellowstonePathology.Reports.Cytology.CytologyAbnormalUnsatLetter(this.m_StartDate, this.m_EndDate);
            YellowstonePathology.Business.Reports.Cytology.CytologyUnsatLetters cytologyUnsatLetters = unsatLetters.CytologyUnsatLetters;
            foreach (YellowstonePathology.Business.Reports.Cytology.CytologyUnsatLetterItem item in cytologyUnsatLetters)
            {
                this.m_BackgroundWorker.ReportProgress(0, "Faxing letter for: " + item.ClientName + " - " + item.PhysicianName);
                unsatLetters.FaxReport(item);
            }
        }

        private void BackgroundWorker_FaxLettersRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                this.m_StatusMessage = "Letters Faxed.";
                this.NotifyPropertyChanged("StatusMessage");
            }));
        }
    }
}
