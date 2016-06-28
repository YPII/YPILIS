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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI.Login
{
	/// <summary>
	/// Interaction logic for SearchSelectionPage.xaml
	/// </summary>
    public partial class SearchSelectionPage : UserControl, INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime m_AccessionDate;
        private string m_ReportNo;
        private string m_SpecimenDescription;
        private DateTime m_KeyWordStartDate;
        private DateTime m_KeyWordEndDate;
        private DateTime m_PanelSetFinalDate;
        private DateTime m_PostDate;
        private DateTime m_HPyloriStartDate;
        private DateTime m_HPyloriEndDate;
        private LoginUIV2 m_LoginUI;
        private DateTime m_TestStartDate;
        private DateTime m_TestEndDate;
        private YellowstonePathology.Business.PanelSet.Model.PanelSetCollection m_PanelSetCollection;

		private string m_PageHeaderText = "Select Search Type";

        public SearchSelectionPage(LoginUIV2 loginUI)
		{
            this.m_LoginUI = loginUI;
            this.m_AccessionDate = DateTime.Today;
            this.m_PanelSetFinalDate = DateTime.Today;
            this.m_PostDate = DateTime.Today;            

            TimeSpan oneMonth = new TimeSpan(30,0,0,0,0);
            this.m_HPyloriStartDate = DateTime.Today.Subtract(oneMonth);
            this.m_HPyloriEndDate = DateTime.Today;

            this.m_KeyWordStartDate = DateTime.Today.AddDays(-30);
            this.m_KeyWordEndDate = DateTime.Today;

            this.m_TestStartDate = DateTime.Today.AddMonths(-1);
            this.m_TestEndDate = DateTime.Today;
            this.m_PanelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();

            InitializeComponent();

			DataContext = this;            
		}        

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

        public DateTime AccessionDate
        {
            get { return this.m_AccessionDate; }
            set { this.m_AccessionDate = value; }
        }

        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set { this.m_ReportNo = value; }
        }

        public string SpecimenDescription
        {
            get { return this.m_SpecimenDescription; }
            set { this.m_SpecimenDescription = value; }
        }

        public DateTime KeyWordStartDate
        {
            get { return this.m_KeyWordStartDate; }
            set { this.m_KeyWordStartDate = value; }
        }

        public DateTime KeyWordEndDate
        {
            get { return this.m_KeyWordEndDate; }
            set { this.m_KeyWordEndDate = value; }
        }

        public DateTime PanelSetFinalDate
        {
            get { return this.m_PanelSetFinalDate; }
            set { this.m_PanelSetFinalDate = value; }
        }

        public DateTime PostDate
        {
            get { return this.m_PostDate; }
            set { this.m_PostDate = value; }
        }

        public DateTime HPyloriStartDate
        {
            get { return this.m_HPyloriStartDate; }
            set { this.m_HPyloriStartDate = value; }
        }

        public DateTime HPyloriEndDate
        {
            get { return this.m_HPyloriEndDate; }
            set { this.m_HPyloriEndDate = value; }
        }

        public YellowstonePathology.Business.PanelSet.Model.PanelSetCollection PanelSetCollection
        {
            get { return this.m_PanelSetCollection; }
        }

        public DateTime TestStartDate
        {
            get { return this.m_TestStartDate; }
            set { this.m_TestStartDate = value; }
        }

        public DateTime TestEndDate
        {
            get { return this.m_TestEndDate; }
            set { this.m_TestEndDate = value; }
        }        

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonReportNoSearch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.m_ReportNo) == false)
            {
                this.m_LoginUI.GetReportSearchListByReportNo(this.m_ReportNo);
                Window.GetWindow(this).Close();
            }
        }

        private void TextBoxReportNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (string.IsNullOrEmpty(this.m_ReportNo) == false)
                {
                    if (this.m_ReportNo.Length >= 3)
                    {
                        this.m_LoginUI.GetReportSearchListByReportNo(this.m_ReportNo);
                        Window.GetWindow(this).Close();
                    }
                }
            }
        }

        private void ButtonAccessionDateSearch_Click(object sender, RoutedEventArgs e)
        {            
            this.m_LoginUI.AccessionOrderDate = this.m_AccessionDate;
            this.m_LoginUI.GetReportSearchList();            
            Window.GetWindow(this).Close();            
        }

        private void ButtonSpecimenDescriptionSearch_Click(object sender, RoutedEventArgs e)
        {
            this.DoKeyWordSearch();
        }

        private void TextBoxSpecimenKeyWord_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.DoKeyWordSearch();
            }
        }

        private void DoKeyWordSearch()
        {
            if (string.IsNullOrEmpty(this.m_SpecimenDescription) == false)
            {
                if (this.m_SpecimenDescription.Length >= 3)
                {
                    
                    this.m_LoginUI.GetReportSearchListBySpecimenKeyword(this.m_SpecimenDescription, this.m_KeyWordStartDate, this.m_KeyWordEndDate);
                    Window.GetWindow(this).Close();
                }
            }
        }

        private void ButtonPanelSetFinalDateSearch_Click(object sender, RoutedEventArgs e)
        {
            this.m_LoginUI.AccessionOrderDate = this.m_AccessionDate;
            this.m_LoginUI.GetReportSearchListByPanelSetFinalDate(this.m_PanelSetFinalDate);
            Window.GetWindow(this).Close();    
        }

        private void ButtonNotPostedSearch_Click(object sender, RoutedEventArgs e)
        {
            this.m_LoginUI.AccessionOrderDate = this.m_AccessionDate;
            this.m_LoginUI.GetReportSearchListByNotPosted();
            Window.GetWindow(this).Close();    
        }

        private void ButtonPosted_Click(object sender, RoutedEventArgs e)
        {
            this.m_LoginUI.AccessionOrderDate = this.m_AccessionDate;
            this.m_LoginUI.GetReportSearchListByPostDate(this.m_PostDate);
            Window.GetWindow(this).Close();   
        }

        private void ButtonPositiveHPyloriSearch_Click(object sender, RoutedEventArgs e)
        {            
            this.m_LoginUI.GetReportSearchListByPositiveHPylori(this.m_HPyloriStartDate, this.m_HPyloriEndDate);
            Window.GetWindow(this).Close();   
        }

        private void ButtonAutopsySearch_Click(object sender, RoutedEventArgs e)
        {
            this.m_LoginUI.GetReportSearchListByAutopsies();
            Window.GetWindow(this).Close();   
        }

        private void ButtonClientAccessionedSearch_Click(object sender, RoutedEventArgs e)
        {
            this.m_LoginUI.GetReportSearchListByClientAccessioned();
            Window.GetWindow(this).Close();   
        }        

        private void ButtonTestSearch_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListBoxTest.SelectedItem != null)
            {
                YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = (YellowstonePathology.Business.PanelSet.Model.PanelSet)this.ListBoxTest.SelectedItem;
                this.m_LoginUI.GetReportSearchListByTest(panelSet.PanelSetId, this.m_TestStartDate, this.m_TestEndDate);
                Window.GetWindow(this).Close();
            }
            else
            {
                MessageBox.Show("Select a test from the list.", "Test not selected");
            }
        }

        private void ButtonDrKurtzmanCaseSearch_Click(object sender, RoutedEventArgs e)
        {
            this.m_LoginUI.GetReportSearchListByDrKurtzman();
            Window.GetWindow(this).Close();   
        }
    }
}
