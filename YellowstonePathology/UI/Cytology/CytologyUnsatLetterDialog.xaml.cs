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

namespace YellowstonePathology.UI.Cytology
{
	/// <summary>
	/// Interaction logic for CytologyUnsatLetterDdialog.xaml
	/// </summary>
	public partial class CytologyUnsatLetterDialog : Window
	{
		private DateTime m_StartDate;
		private DateTime m_EndDate;
		private int m_OpenCaseCount;

		public CytologyUnsatLetterDialog()
		{            
            DateTime firstDayOfThisMonth = DateTime.Parse(DateTime.Today.Month.ToString() + "/1/" + DateTime.Today.Year.ToString());
            this.m_StartDate = firstDayOfThisMonth.AddMonths(-1);
            this.m_EndDate = firstDayOfThisMonth.AddDays(-1);
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

		private void ButtonOk_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void ButtonNavigateToFolder_Click(object sender, RoutedEventArgs e)
		{
			string reportFolderPath = @"\\CFileServer\Documents\Reports\Cytology\CytologyAbnormalUnsatLetter";
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			System.Diagnostics.Process p = new System.Diagnostics.Process();
			System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("Explorer.exe", reportFolderPath);
			p.StartInfo = info;
			p.Start();
		}

		private void ButtonOpenCaseCount_Click(object sender, RoutedEventArgs e)
		{
			this.m_OpenCaseCount = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetCountOpenCytologyCasesByCollectionDateRange(this.m_StartDate, this.m_EndDate);
			this.TextBlockOpenCaseCount.Text = this.m_OpenCaseCount.ToString();
		}

		private void ButtonCreateLetters_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Reports.Cytology.CytologyAbnormalUnsatLetter unsatLetters = new YellowstonePathology.Reports.Cytology.CytologyAbnormalUnsatLetter(this.m_StartDate, this.m_EndDate);
			unsatLetters.CreateReports();
		}

		private void ButtonFaxLetters_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Reports.Cytology.CytologyAbnormalUnsatLetter unsatLetters = new YellowstonePathology.Reports.Cytology.CytologyAbnormalUnsatLetter(this.m_StartDate, this.m_EndDate);
			unsatLetters.FaxReports();
		}
	}
}
