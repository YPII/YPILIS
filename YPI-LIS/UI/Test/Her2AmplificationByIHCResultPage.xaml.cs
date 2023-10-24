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
	/// <summary>
	/// Interaction logic for Her2AmplificationByIHCResultPage.xaml
	/// </summary>
	public partial class Her2AmplificationByIHCResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

        public delegate void OrderTestEventHandler(object sender, CustomEventArgs.PanelSetReturnEventArgs e);
        public event OrderTestEventHandler OrderTest;

        private Business.User.SystemIdentity m_SystemIdentity;
		private Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText;

		private Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC m_PanelSetOrder;
		private string m_OrderedOnDescription;

        private Business.Test.Her2AmplificationByIHC.HER2AmplificationByIHCIndicatorCollection m_IndicatorCollection;
        private List<string> m_ResultList;
        private List<string> m_ScoreList;
        private List<string> m_FixationList;
        private Business.Test.Her2AmplificationByIHC.Her2IHCResultCollection m_Her2IHCResultCollection;

        public Her2AmplificationByIHCResultPage(Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC panelSetOrderHer2AmplificationByIHC,
			Business.Test.AccessionOrder accessionOrder,
			Business.User.SystemIdentity systemIdentity) : base(panelSetOrderHer2AmplificationByIHC, accessionOrder)
		{            
            this.m_IndicatorCollection = new Business.Test.Her2AmplificationByIHC.HER2AmplificationByIHCIndicatorCollection();

            this.m_ResultList = new List<string>();
            this.m_ResultList.Add("Positive");
            this.m_ResultList.Add("Low Positive");
            this.m_ResultList.Add("Equivocal");
            this.m_ResultList.Add("Negative");

            this.m_ScoreList = new List<string>();
            this.m_ScoreList.Add("3+");
            this.m_ScoreList.Add("2+");
            this.m_ScoreList.Add("1+");
            this.m_ScoreList.Add("0");

            this.m_FixationList = new List<string>();
            this.m_FixationList.Add("10% Formaldehyde");
            this.m_FixationList.Add("Ethanol");

            this.m_PanelSetOrder = panelSetOrderHer2AmplificationByIHC;
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;

			this.m_PageHeaderText = "Her2 Amplification By IHC Result For: " + this.m_AccessionOrder.PatientDisplayName;

			Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
			this.m_OrderedOnDescription = specimenOrder.Description;

            if (string.IsNullOrEmpty(this.m_PanelSetOrder.Indicator) == false)
            {
                Business.Test.Her2AmplificationByIHC.Her2IHCResultCollection allResults = Business.Test.Her2AmplificationByIHC.Her2IHCResultCollection.GetALL();
                this.m_Her2IHCResultCollection = allResults.FilterByIndication(this.m_PanelSetOrder.Indicator);
            }

            InitializeComponent();

			DataContext = this;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockShowDocument);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);            
        }

        public List<string> ResultList
        {
            get { return this.m_ResultList; }
        }

        public List<string> ScoreList
        {
            get { return this.m_ScoreList; }
        }

        public List<string> FixationList
        {
            get { return this.m_FixationList; }
        }

        public Business.Test.Her2AmplificationByIHC.HER2AmplificationByIHCIndicatorCollection IndicatorCollection
        {
            get { return this.m_IndicatorCollection; }
        }

        public Business.Test.Her2AmplificationByIHC.Her2IHCResultCollection Her2IHCResultCollection
        {
            get { return this.m_Her2IHCResultCollection; }
        }

        public string OrderedOnDescription
		{
			get { return this.m_OrderedOnDescription; }
		}

		public YellowstonePathology.Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC PanelSetOrder
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

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCWordDocument report = new Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCWordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
			report.Render();
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWord(report.SaveFileName);
		}

		private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
		{
            Business.Audit.Model.AuditResult result = this.m_PanelSetOrder.IsOkToFinalize(this.m_AccessionOrder);

            if (result.Status == Business.Audit.Model.AuditStatusEnum.OK)
			{
                YellowstonePathology.Business.Test.FinalizeTestResult finalizeTestResult = this.m_PanelSetOrder.Finish(this.m_AccessionOrder);
                this.HandleFinalizeTestResult(finalizeTestResult);
            }
            else if(result.Status == Business.Audit.Model.AuditStatusEnum.Warning)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(result.Message, "Test information", MessageBoxButton.OKCancel, MessageBoxImage.Information, MessageBoxResult.OK);
                if (messageBoxResult == MessageBoxResult.OK)
                {

                    YellowstonePathology.Business.Test.FinalizeTestResult finalizeTestResult = this.m_PanelSetOrder.Finish(this.m_AccessionOrder);
                    this.HandleFinalizeTestResult(finalizeTestResult);
                    Business.Logging.EmailExceptionHandler.HandleException(this.m_PanelSetOrder, "This report has just been finalized, score = " + 
                        this.m_PanelSetOrder.Score + ".  A recount has been ordered on final.");

                    YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTest recountTest = new Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTest();
                    this.OrderATest(recountTest);
                    YellowstonePathology.Business.Test.HER2AnalysisSummary.HER2AnalysisSummaryTest summaryTest = new Business.Test.HER2AnalysisSummary.HER2AnalysisSummaryTest();
                    this.OrderATest(summaryTest);
                }
            }
            else
			{
				MessageBox.Show(result.Message);
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

        private void HyperLinkOrderHER2DISH_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest test = new Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest();
            this.OrderATest(test);
        }

        private YellowstonePathology.Business.Rules.MethodResult CanOrder()
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            result.Success = false;

            YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest test = new Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest();
            if(this.m_AccessionOrder.PanelSetOrderCollection.Exists(test.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
            {
                YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder testOrder = (YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(test.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true);
                if (testOrder.Final == true && testOrder.Result == Business.Test.HER2AmplificationByISH.HER2AmplificationResultEnum.Equivocal.ToString())
                {
                    result.Success = true;
                }
            }

            if(result.Success == false)
            {
                result.Message = "A " + test.PanelSetName + " must be ordered and final before this test may be ordered.";
            }

            return result;
        }

        private void HyperLinkOrderRecount_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult result = this.CanOrder();
            if (result.Success == true)
            {
                YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTest test = new Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTest();
                this.OrderATest(test);
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private void HyperLinkOrderHER2Summary_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult result = this.CanOrder();
            if (result.Success == true)
            {
                YellowstonePathology.Business.Test.HER2AnalysisSummary.HER2AnalysisSummaryTest test = new Business.Test.HER2AnalysisSummary.HER2AnalysisSummaryTest();
                this.OrderATest(test);
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private void OrderATest(YellowstonePathology.Business.PanelSet.Model.PanelSet test)
        {
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(test.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == false)
            {
                CustomEventArgs.PanelSetReturnEventArgs args = new CustomEventArgs.PanelSetReturnEventArgs(test);
                this.OrderTest(this, args);
            }            
        }


        private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			if (this.Next != null) this.Next(this, new EventArgs());
		}

        private void HyperLinkSetResults_Click(object sender, RoutedEventArgs e)
        {
            bool resultFound = this.m_Her2IHCResultCollection.Any(r => r.Indication == this.m_PanelSetOrder.Indicator &&
            r.Score == this.m_PanelSetOrder.Score && r.Result == this.m_PanelSetOrder.Result);

            if(resultFound == false)
            {
                MessageBox.Show("Didn't find a matching rule, the interpretation can not be set.");
            }
            else
            {
                Business.Test.Her2AmplificationByIHC.Her2IHCResult foundResult = this.m_Her2IHCResultCollection.First(r => r.Indication == this.m_PanelSetOrder.Indicator &&
                    r.Score == this.m_PanelSetOrder.Score && r.Result == this.m_PanelSetOrder.Result);
                this.m_PanelSetOrder.Interpretation = foundResult.Interpretation;
                this.m_PanelSetOrder.Method = foundResult.Method;
            }
        }

        private void ComboboxIndicator_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.IsLoaded == true)
            {
                if (this.ComboboxIndicator.SelectedItem != null)
                {
                    string indication = this.ComboboxIndicator.SelectedItem as string;
                    Business.Test.Her2AmplificationByIHC.Her2IHCResultCollection allResults = Business.Test.Her2AmplificationByIHC.Her2IHCResultCollection.GetALL();
                    this.m_Her2IHCResultCollection = allResults.FilterByIndication(indication);
                    this.m_PanelSetOrder.Result = null;
                    this.m_PanelSetOrder.Method = null;
                    this.m_PanelSetOrder.Score = null;
                    this.m_PanelSetOrder.Interpretation = null;
                    this.NotifyPropertyChanged(string.Empty);
                }
            }            
        }

        private void ComboboxInterpretation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.IsLoaded == true)
            {
                if (this.ComboboxInterpretation.SelectedItem != null)
                {
                    string indication = this.ComboboxIndicator.SelectedItem as string;
                    Business.Test.Her2AmplificationByIHC.Her2IHCResult selectedResult = (Business.Test.Her2AmplificationByIHC.Her2IHCResult)this.ComboboxInterpretation.SelectedItem;
                    this.m_PanelSetOrder.Score = selectedResult.Score;
                    this.m_PanelSetOrder.Result = selectedResult.Result;
                    this.m_PanelSetOrder.Method = selectedResult.Method;
                    this.NotifyPropertyChanged(string.Empty);
                }
            }            
        }
    }
}
