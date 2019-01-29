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
using System.Xml.Linq;

namespace YellowstonePathology.UI.Test
{
	/// <summary>
	/// Interaction logic for LynchSyndromeEvaluationResultPage.xaml
	/// </summary>
	public partial class LynchSyndromeEvaluationResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

        public delegate void OrderBrafEventHandler(object sender, EventArgs e);
		public event OrderBrafEventHandler OrderBraf;

		public delegate void OrderMLH1MethylationAnalysisEventHandler(object sender, EventArgs e);
		public event OrderMLH1MethylationAnalysisEventHandler OrderMLH1MethylationAnalysis;

        public delegate void OrderColonCancerProfileEventHandler(object sender, EventArgs e);
        public event OrderColonCancerProfileEventHandler OrderColonCancerProfile;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText;
		private string m_OrderedOnDescription;

        private Business.Test.LynchSyndrome.LSERuleCollection m_LSERuleCollection;
        private YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation m_PanelSetOrderLynchSyndromeEvaluation;        
        private YellowstonePathology.Business.Test.LynchSyndrome.LSETypeCollection m_LSETypeCollection;

        private System.Windows.Visibility m_BackButtonVisibility;

		public LynchSyndromeEvaluationResultPage(YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity,
            System.Windows.Visibility backButtonVisibility) : base(panelSetOrderLynchSyndromeEvaluation, accessionOrder)
		{
			this.m_PanelSetOrderLynchSyndromeEvaluation = panelSetOrderLynchSyndromeEvaluation;
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;
            this.m_BackButtonVisibility = backButtonVisibility;

            this.m_LSETypeCollection = new Business.Test.LynchSyndrome.LSETypeCollection();
            this.m_PageHeaderText = "Lynch Syndrome Evaluation Results For: " + this.m_AccessionOrder.PatientDisplayName + " (" + this.m_PanelSetOrderLynchSyndromeEvaluation.ReportNo + ")";

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrderLynchSyndromeEvaluation.OrderedOnId);
			YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(this.m_PanelSetOrderLynchSyndromeEvaluation.OrderedOnId);
			this.m_OrderedOnDescription = specimenOrder.Description;
			if (aliquotOrder != null) this.m_OrderedOnDescription += ": " + aliquotOrder.Label;

            this.FillLSERuleCollection();

            InitializeComponent();
			DataContext = this;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonBack);
            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockShowDocument);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
        }

        public YellowstonePathology.Business.Test.LynchSyndrome.LSETypeCollection LSETypeCollection
        {
            get { return this.m_LSETypeCollection; }
        }

        public System.Windows.Visibility BackButtonVisibility
        {
            get { return this.m_BackButtonVisibility; }
        }        

		public YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation PanelSetOrder
		{
			get { return this.m_PanelSetOrderLynchSyndromeEvaluation; }
		}

		public string OrderedOnDescription
		{
			get { return this.m_OrderedOnDescription; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

        public Business.Test.LynchSyndrome.LSERuleCollection LSERuleCollection
        {
            get { return this.m_LSERuleCollection; }
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

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            if (this.IWantToOrderColonCancerProfileMessage() == false)
            {
                if (this.Next != null) this.Next(this, new EventArgs());
            }
		}

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Back != null) this.Back(this, new EventArgs());
        }

        private void HyperLinkSetResults_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewResults.Items.Count == 1)
            {
                YellowstonePathology.Business.Test.LynchSyndrome.LSERule lseRule = (YellowstonePathology.Business.Test.LynchSyndrome.LSERule)this.ListViewResults.Items[0];
                YellowstonePathology.Business.Audit.Model.AuditResult result = lseRule.IsOkToSetResults(this.m_AccessionOrder, this.m_PanelSetOrderLynchSyndromeEvaluation);
                if (result.Status == Business.Audit.Model.AuditStatusEnum.OK)
                {
                    lseRule.SetResults(this.m_AccessionOrder, this.m_PanelSetOrderLynchSyndromeEvaluation);
                }
                else
                {
                    MessageBox.Show(result.Message);
                }
            }
            else
            {
                MessageBox.Show("Results can be set as the evaluation is not complete.");
            }
        }

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
        {            
			YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationWordDocument lynchSyndromeEvaluation = new Business.Test.LynchSyndrome.LynchSyndromeEvaluationWordDocument(this.m_AccessionOrder, this.m_PanelSetOrderLynchSyndromeEvaluation, Business.Document.ReportSaveModeEnum.Draft);
            lynchSyndromeEvaluation.Render();

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrderLynchSyndromeEvaluation.ReportNo);
            string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
            YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
        }

		private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Audit.Model.AuditCollection auditCollection = new Business.Audit.Model.AuditCollection();
            auditCollection.Add(new YellowstonePathology.Business.Audit.Model.LSETypeIsNotSetAudit(this.m_AccessionOrder));
            auditCollection.Run();

            if (auditCollection.ActionRequired == false)
            {
                YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrderLynchSyndromeEvaluation.IsOkToFinalize();
                if (methodResult.Success == true)
                {
                    YellowstonePathology.Business.Test.FinalizeTestResult finalizeTestResult = this.m_PanelSetOrderLynchSyndromeEvaluation.Finish(this.m_AccessionOrder);
                    this.HandleFinalizeTestResult(finalizeTestResult);

                    // Temporary use for checking results
                    YellowstonePathology.Business.Test.LynchSyndrome.LSERule lseRule = (YellowstonePathology.Business.Test.LynchSyndrome.LSERule)this.ListViewResults.Items[0];
                    Business.Logging.EmailExceptionHandler.HandleException(this.m_PanelSetOrderLynchSyndromeEvaluation, "This LSE has just been finalized using " + lseRule.Indication + " - " + lseRule.RuleName);
                }
                else
                {
                    MessageBox.Show(methodResult.Message);
                }
            }
            else
            {
               MessageBox.Show("We are unable to finalize this report because: " + auditCollection.Message);
            }
        }

        private bool IWantToOrderColonCancerProfileMessage()
        {
            bool result = false;
            YellowstonePathology.Business.Audit.Model.CouldBenefitFromCCCPAudit couldBenefitFromCCCPAudit = new Business.Audit.Model.CouldBenefitFromCCCPAudit(this.m_AccessionOrder);
            couldBenefitFromCCCPAudit.Run();
            if (couldBenefitFromCCCPAudit.Status == Business.Audit.Model.AuditStatusEnum.Warning)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(couldBenefitFromCCCPAudit.Message.ToString(), "Continue!", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    result = true;
                }
            }
            return result;
        }

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrderLynchSyndromeEvaluation.IsOkToUnfinalize();
            if (result.Success == true)
            {
                this.m_PanelSetOrderLynchSyndromeEvaluation.Unfinalize();
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }		

		private void HyperLinkOrderBraf_Click(object sender, RoutedEventArgs e)
		{
			this.OrderBraf(this, new EventArgs());
		}

		private void HyperLinkOrderMLH1_Click(object sender, RoutedEventArgs e)
		{
            this.OrderMLH1MethylationAnalysis(this, new EventArgs());
		}

        private void HyperLinkOrderCCCP_Click(object sender, RoutedEventArgs e)
        {
            this.OrderColonCancerProfile(this, new EventArgs());
        }

        private void ComboBoxLSEType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.FillLSERuleCollection();
            this.NotifyPropertyChanged("LSERuleCollection");
        }

        private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Business.Audit.Model.AuditResult result = this.m_PanelSetOrderLynchSyndromeEvaluation.IsOkToAccept(this.m_AccessionOrder);
            if (result.Status == Business.Audit.Model.AuditStatusEnum.OK)
            {
                this.m_PanelSetOrderLynchSyndromeEvaluation.Accept();
            }
            else
            {
                MessageBox.Show(result.Message);
            }
		}

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrderLynchSyndromeEvaluation.IsOkToUnaccept();
			if (result.Success == true)
			{
				this.m_PanelSetOrderLynchSyndromeEvaluation.Unaccept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}
		}

        private void FillLSERuleCollection()
        {
            this.m_LSERuleCollection = YellowstonePathology.Business.Test.LynchSyndrome.LSERuleCollection.GetMatchCollection(this.m_AccessionOrder, this.m_PanelSetOrderLynchSyndromeEvaluation);
        }
    }
}
