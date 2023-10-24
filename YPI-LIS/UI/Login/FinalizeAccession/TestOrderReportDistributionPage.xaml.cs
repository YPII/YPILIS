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

namespace YellowstonePathology.UI.Login.FinalizeAccession
{	
	public partial class TestOrderReportDistributionPage : UserControl, INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, CustomEventArgs.TestOrderReportDistributionReturnEventArgs e);
		public event NextEventHandler Next;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;
		
		private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
        private YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution m_TestOrderReportDistribution;

        public TestOrderReportDistributionPage(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution,			
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
		{
            this.m_TestOrderReportDistribution = testOrderReportDistribution;			
			this.m_PageNavigator = pageNavigator;
            
			InitializeComponent();

			DataContext = this;
            this.Loaded += new RoutedEventHandler(TestOrderReportDistributionPage_Loaded);
		}

        private void TestOrderReportDistributionPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.TextBoxClientName.Focus();
        }

        public YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution TestOrderReportDistribution
        {
            get { return this.m_TestOrderReportDistribution; }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Back != null) this.Back(this.Back, new EventArgs());
        }

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            if (IsOkToGoNext() == true)
            {
                if (this.Next != null) this.Next(this.Next, new CustomEventArgs.TestOrderReportDistributionReturnEventArgs(this.m_TestOrderReportDistribution));
            }
		}

        private bool IsOkToGoNext()
        {            
            bool result = false;
            if(this.m_TestOrderReportDistribution.DistributionType == "Fax")
            {
                if (this.MaskedTextBoxFaxNumber.IsMaskCompleted == false)
                {
                    result = false;
                }
                else if (this.MaskedTextBoxFaxNumber.IsMaskFull == true && this.MaskedTextBoxFaxNumber.HasValidationError == false && this.MaskedTextBoxFaxNumber.HasParsingError == false)
                {
                    result = true;
                }
                else if (this.MaskedTextBoxFaxNumber.IsMaskCompleted == false && this.MaskedTextBoxFaxNumber.HasValidationError == false && this.MaskedTextBoxFaxNumber.HasParsingError == false)
                {
                    result = true;
                }

                if (result == false) MessageBox.Show("The Fax number must be 10 digits.");
            }   
            else
            {
                result = true;
            }
            return result;

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

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

		public void UpdateBindingSources()
		{

		}        
                
	}
}
