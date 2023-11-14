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

namespace YellowstonePathology.UI.Test
{	
	public partial class ExtractAndHoldForPreauthorizationResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText;

		private YellowstonePathology.Business.Test.ExtractAndHoldForPreauthorization.ExtractAndHoldForPreauthorizationTestOrder m_PanelSetOrder;        

        public ExtractAndHoldForPreauthorizationResultPage(YellowstonePathology.Business.Test.ExtractAndHoldForPreauthorization.ExtractAndHoldForPreauthorizationTestOrder panelSetOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(panelSetOrder, accessionOrder)
		{

            this.m_SystemIdentity = Business.User.SystemIdentity.Instance;
            this.m_PanelSetOrder = panelSetOrder;
			this.m_AccessionOrder = accessionOrder;			

			this.m_PageHeaderText = "Extract and Hold for Preauthorization Result For: " + this.m_AccessionOrder.PatientDisplayName;			

			InitializeComponent();

			DataContext = this;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockShowDocument);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
        }        

		public YellowstonePathology.Business.Test.ExtractAndHoldForPreauthorization.ExtractAndHoldForPreauthorizationTestOrder PanelSetOrder
		{
			get { return this.m_PanelSetOrder; }
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}
        
        public YellowstonePathology.Business.PanelSet.Model.PanelSetCollection PanelSetCollection
        {
            get { return YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll(); }
        }

        private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Test.ExtractAndHoldForPreauthorization.ExtractAndHoldForPreauthorizationWordDocument report = new Business.Test.ExtractAndHoldForPreauthorization.ExtractAndHoldForPreauthorizationWordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
			report.Render();
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWord(report.SaveFileName);
		}

		private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_PanelSetOrder.Final == false)
			{
                YellowstonePathology.Business.Test.FinalizeTestResult finalizeTestResult = this.m_PanelSetOrder.Finish(this.m_AccessionOrder);
                this.HandleFinalizeTestResult(finalizeTestResult);
            }
            else
			{
				MessageBox.Show("This case cannot be finalized because it is already final.");
			}
		}

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_PanelSetOrder.Final == true)
			{
				this.m_PanelSetOrder.Unfinalize();
			}
			else
			{
				MessageBox.Show("This case cannot be unfinalized because it is not final.");
			}
		}

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToAccept();
			if (result.Success == true)
			{
				this.m_PanelSetOrder.Accept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}
		}

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToUnaccept();
			if (result.Success == true)
			{
				this.m_PanelSetOrder.Unaccept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			if (this.Next != null) this.Next(this, new EventArgs());
		}        

        private void TryShowSecondMonitorWindow(YellowstonePathology.UI.Surgical.DictationTemplatePage dictationTemplatePage)
        {
            PageNavigationWindow pageNavigationWindow = null;

            if (System.Windows.Forms.Screen.AllScreens.Length == 2)
            {
                pageNavigationWindow = new PageNavigationWindow(this.m_SystemIdentity);

                System.Windows.Forms.Screen screen2 = System.Windows.Forms.Screen.AllScreens[1];
                System.Drawing.Rectangle screen2Rectangle = screen2.WorkingArea;

                pageNavigationWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
                pageNavigationWindow.Width = 1500;
                pageNavigationWindow.Height = 800;
                pageNavigationWindow.Left = screen2Rectangle.Left + (screen2Rectangle.Width - pageNavigationWindow.Width) / 2;
                pageNavigationWindow.Top = screen2Rectangle.Top + (screen2Rectangle.Height - pageNavigationWindow.Height) / 2;
                pageNavigationWindow.Show();

                pageNavigationWindow.PageNavigator.Navigate(dictationTemplatePage);
            }            
        }

        private void ComboBoxTests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.ComboBoxTests.SelectedItem != null)
            {
                YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = (YellowstonePathology.Business.PanelSet.Model.PanelSet)this.ComboBoxTests.SelectedItem;
                string cpts = panelSet.PanelSetCptCodeCollection.GetCommaSeparatedString();
                if(string.IsNullOrEmpty(cpts) == false) this.m_PanelSetOrder.CPTCodes = cpts;
                this.NotifyPropertyChanged("PanelSetOrder.CPTCodes");
            }
        }

        private void HyperLinkPublish_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.ExtractAndHoldForPreauthorization.ExtractAndHoldForPreauthorizationWordDocument reportPreauth =
                new YellowstonePathology.Business.Test.ExtractAndHoldForPreauthorization.ExtractAndHoldForPreauthorizationWordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Normal);
            reportPreauth.Render();
            reportPreauth.Publish();
            MessageBox.Show("The document has been published.");
        }

        private void HyperLinkFax_Click(object sender, RoutedEventArgs e)
        {
            Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            string preauthFileName = Business.Document.CaseDocument.GetCaseFileNameTifPreAuth(orderIdParser);
            if (System.IO.File.Exists(preauthFileName) == true)
            {
                Business.ReportDistribution.Model.FaxSubmission.Submit(this.m_PanelSetOrder.Fax, this.m_PanelSetOrder.ReportNo + "Preauthorization Notification", preauthFileName, this.m_PanelSetOrder.ReportNo);
                MessageBox.Show("The fax was successfully submitted.");
            }
            else
            {
                MessageBox.Show("The document must be published first.");
            }
        }
    }
}
