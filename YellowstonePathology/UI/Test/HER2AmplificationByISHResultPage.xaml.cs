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
    /// Interaction logic for HER2AmplificationByISHResultPage.xaml
    /// </summary>
    public partial class HER2AmplificationByISHResultPage : ResultControl, INotifyPropertyChanged , IResultPage
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event YellowstonePathology.UI.CustomEventArgs.EventHandlerDefinitions.CancelTestEventHandler CancelTest;

		public delegate void SpecimenDetailEventHandler(object sender, EventArgs e);
		public event SpecimenDetailEventHandler SpecimenDetail;

        public delegate void OrderTestEventHandler(object sender, CustomEventArgs.PanelSetReturnEventArgs e);
        public event OrderTestEventHandler OrderTest;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
        private YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder m_PanelSetOrder;
        private YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHIndicatorCollection m_IndicatorCollection;
        private YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHSampleAdequacyCollection m_SampleAdequacyCollection;
        private YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHProbeSignalIntensityCollection m_ProbeSignalIntensityCollection;
        private YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHGeneticHeterogeneityCollection m_GeneticHeterogeneityCollection;
        private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
        private string m_PageHeaderText;
        private string m_OrderedOnDescription;


        public HER2AmplificationByISHResultPage(YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder testOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator) : base(testOrder, accessionOrder)
        {
            this.m_PanelSetOrder = testOrder;
            this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = systemIdentity;
            this.m_PageNavigator = pageNavigator;

            this.m_PageHeaderText = this.m_PanelSetOrder.PanelSetName + " Results For: " + this.m_AccessionOrder.PatientDisplayName;
            this.m_IndicatorCollection = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHIndicatorCollection();
            this.m_SampleAdequacyCollection = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHSampleAdequacyCollection();
            this.m_ProbeSignalIntensityCollection = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHProbeSignalIntensityCollection();
            this.m_GeneticHeterogeneityCollection = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHGeneticHeterogeneityCollection();
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_PanelSetOrder.OrderedOnId);
            this.m_SpecimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);

            this.m_OrderedOnDescription = orderTarget.GetDescription();
            InitializeComponent();

            DataContext = this;

            
            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockShowDocument);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
        }

        public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
        {
            get { return this.m_SpecimenOrder; }
        }

        public YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        public YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHIndicatorCollection IndicatorCollection
        {
            get { return this.m_IndicatorCollection; }
        }

        public YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHSampleAdequacyCollection SampleAdequacyCollection
        {
            get { return this.m_SampleAdequacyCollection; }
        }

        public YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHProbeSignalIntensityCollection ProbeSignalIntensityCollection
        {
            get { return this.m_ProbeSignalIntensityCollection; }
        }

        public YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHGeneticHeterogeneityCollection GeneticHeterogeneityCollection
        {
            get { return this.m_GeneticHeterogeneityCollection; }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public string OrderedOnDescription
        {
            get { return this.m_OrderedOnDescription; }
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
            this.Next(this, new EventArgs());
        }

        private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
        {
            bool canFinal = false;            
            YellowstonePathology.Business.Audit.Model.AuditResult auditResult = this.m_PanelSetOrder.IsOkToFinalize(this.m_AccessionOrder);
            if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.OK)
            {
                canFinal = true;
            }
            else if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.Warning)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(auditResult.Message, "Additional testing required", MessageBoxButton.OKCancel, MessageBoxImage.Information, MessageBoxResult.OK);
                if(messageBoxResult == MessageBoxResult.OK)
                {
                    canFinal = true;
                    YellowstonePathology.Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCTest her2AmplificationByIHCTest = new Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCTest();
                    this.OrderATest(her2AmplificationByIHCTest);
                    YellowstonePathology.Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryTest her2AmplificationSummaryTest = new Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryTest();
                    this.OrderATest(her2AmplificationSummaryTest);
                }
            }
            else
            {
                MessageBox.Show(auditResult.Message);
            }

            if(canFinal == true)
            {
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
                YellowstonePathology.Business.Test.FinalizeTestResult finalizeTestResult = this.m_PanelSetOrder.Finish(this.m_AccessionOrder);
                this.HandleFinalizeTestResult(finalizeTestResult);

                if (this.m_PanelSetOrder.HER2ByIHCRequired == false)
                {
                    YellowstonePathology.Business.Test.Surgical.SurgicalTest panelSetSurgical = new YellowstonePathology.Business.Test.Surgical.SurgicalTest();

                    if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetSurgical.PanelSetId) == true)
                    {
                        YellowstonePathology.Business.Test.PanelSetOrder surgicalPanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetSurgical.PanelSetId);
                        if (surgicalPanelSetOrder.AmendmentCollection.HasAmendmentForReport(this.m_PanelSetOrder.ReportNo) == false)
                        {
                            string amendmentText = YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHSystemGeneratedAmendmentText.AmendmentText(this.m_PanelSetOrder);
                            YellowstonePathology.Business.Amendment.Model.Amendment amendment = surgicalPanelSetOrder.AddAmendment();
                            amendment.TestResultAmendmentFill(surgicalPanelSetOrder.ReportNo, surgicalPanelSetOrder.AssignedToId, amendmentText);
                            amendment.ReferenceReportNo = this.m_PanelSetOrder.ReportNo;
                            amendment.SystemGenerated = true;
                        }
                    }
                }
            }
        }

        private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToUnfinalize();
            if (methodResult.Success == true)
            {
                this.m_PanelSetOrder.Unfinalize();
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Audit.Model.AuditResult auditResult = this.m_PanelSetOrder.IsOkToAccept(this.m_AccessionOrder);
            if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.OK)
            {
                Business.Test.HER2AmplificationByISH.HER2AmplificationResult.AcceptResults(this.m_PanelSetOrder);
            }
            else
            {
                MessageBox.Show(auditResult.Message);
            }
        }

        private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToUnaccept();
            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationResult.UnacceptResults(this.m_PanelSetOrder);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkSetResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Audit.Model.AuditResult auditResult = this.m_PanelSetOrder.IsOkToSetResults(this.m_AccessionOrder);
            if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.OK)
            {
                this.m_PanelSetOrder.SetResults(this.m_AccessionOrder);
            }
            else
            {
                MessageBox.Show(auditResult.Message);
            }
        }

        private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.m_PanelSetOrder.Indicator) == false)
            {
                YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHWordDocument report = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHWordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
                report.Render();
                YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(report.SaveFileName);
            }
            else
            {
                MessageBox.Show("We cannot show the document until the indication has been set.");
            }
        }

        private void HyperLinkCancelTestInsufficient_Click(object sender, RoutedEventArgs e)
        {
            string reasonForTestCancelation = this.m_PanelSetOrder.PanelSetName + " testing has been cancelled due to insufficient specimen.";
            YellowstonePathology.UI.CustomEventArgs.CancelTestEventArgs cancelTestEventArgs = new CustomEventArgs.CancelTestEventArgs(this.m_PanelSetOrder, this.m_AccessionOrder, reasonForTestCancelation, this);
            this.CancelTest(this, cancelTestEventArgs);
        }

        private void HyperLinkCancelTestUniterpretable_Click(object sender, RoutedEventArgs e)
        {
            string reasonForTestCancelation = this.m_PanelSetOrder.PanelSetName + " testing has been cancelled due to an uninterpretable result.";
            YellowstonePathology.UI.CustomEventArgs.CancelTestEventArgs cancelTestEventArgs = new CustomEventArgs.CancelTestEventArgs(this.m_PanelSetOrder, this.m_AccessionOrder, reasonForTestCancelation, this);
            this.CancelTest(this, cancelTestEventArgs);
        }

		private void HyperlinkSpecimenDetails_Click(object sender, RoutedEventArgs e)
		{
			if (this.SpecimenDetail != null)
			{
				this.SpecimenDetail(this, new EventArgs());
			}
		}

        private void HyperLinkOrderHER2IHC_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCTest test = new Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCTest();
            this.OrderATest(test);
        }

        private void HyperLinkOrderRecount_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTest test = new Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTest();
            this.OrderATest(test);
        }

        private void HyperLinkOrderHER2Summary_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryTest test = new Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryTest();
            this.OrderATest(test);
        }

        private void OrderATest(YellowstonePathology.Business.PanelSet.Model.PanelSet test)
        {
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(test.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == false)
            {
                CustomEventArgs.PanelSetReturnEventArgs args = new CustomEventArgs.PanelSetReturnEventArgs(test);
                this.OrderTest(this, args);
            }
            else
            {
                MessageBox.Show("Unable to order a " + test.PanelSetName + " as one already exists.");
            }
        }
    }
}
