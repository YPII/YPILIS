﻿using System;
using System.Windows;
using System.ComponentModel;

namespace YellowstonePathology.UI.Test
{
    /// <summary>
    /// Interaction logic for HER2AmplificationSummaryResultPage.xaml
    /// </summary>
    public partial class HER2AmplificationSummaryResultPage : ResultControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.HER2AnalysisSummary.HER2AnalysisSummaryTestOrder m_PanelSetOrder;

        private YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder m_HER2AmplificationByISHTestOrder;
        private YellowstonePathology.Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC m_PanelSetOrderHer2AmplificationByIHC;
        private YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTestOrder m_HER2AmplificationRecountTestOrder;

        private YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHIndicatorCollection m_IndicatorCollection;
        private YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHSampleAdequacyCollection m_SampleAdequacyCollection;
        private YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHProbeSignalIntensityCollection m_ProbeSignalIntensityCollection;
        private YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHGeneticHeterogeneityCollection m_GeneticHeterogeneityCollection;
        private string m_PageHeaderText;
        private string m_OrderedOnDescription;

        private string m_CellsCounted;
        private string m_Her2SignalsCounted;
        private string m_Chr17SignalsCounted;
        private string m_NumberOfObservers;


        public HER2AmplificationSummaryResultPage(YellowstonePathology.Business.Test.HER2AnalysisSummary.HER2AnalysisSummaryTestOrder testOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(testOrder, accessionOrder)
        {
            this.m_PanelSetOrder = testOrder;
            this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = systemIdentity;

            this.m_PageHeaderText = this.m_PanelSetOrder.PanelSetName + " Results For: " + this.m_AccessionOrder.PatientDisplayName;

            this.m_IndicatorCollection = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHIndicatorCollection();
            this.m_SampleAdequacyCollection = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHSampleAdequacyCollection();
            this.m_ProbeSignalIntensityCollection = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHProbeSignalIntensityCollection();
            this.m_GeneticHeterogeneityCollection = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHGeneticHeterogeneityCollection();

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(this.m_PanelSetOrder.OrderedOnId);
            string blockDescription = string.Empty;
            if (aliquotOrder != null)
            {
                blockDescription = " - Block " + aliquotOrder.Label;
            }

            this.m_OrderedOnDescription = specimenOrder.Description + blockDescription;

            YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest ishTest = new Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest();
            YellowstonePathology.Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCTest ihcTest = new Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCTest();
            YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTest recountTest = new Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTest();
            if(this.m_AccessionOrder.PanelSetOrderCollection.Exists(ishTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
            {
                this.m_HER2AmplificationByISHTestOrder = (Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(ishTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true);
            }
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(ihcTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
            {
                this.m_PanelSetOrderHer2AmplificationByIHC = (Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(ihcTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true);
            }
            if(this.m_AccessionOrder.PanelSetOrderCollection.Exists(recountTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) ==true)
            {
                this.m_HER2AmplificationRecountTestOrder = (Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(recountTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true);
                this.m_CellsCounted = this.m_HER2AmplificationRecountTestOrder.CellsCounted.ToString();
                this.m_Her2SignalsCounted = this.m_HER2AmplificationRecountTestOrder.Her2SignalsCounted.ToString();
                this.m_Chr17SignalsCounted = this.m_HER2AmplificationRecountTestOrder.Chr17SignalsCounted.ToString();
                this.m_NumberOfObservers = this.m_HER2AmplificationRecountTestOrder.NumberOfObservers.ToString();
            }
            else
            {
                this.m_CellsCounted = "NA";
                this.m_Her2SignalsCounted = "NA";
                this.m_Chr17SignalsCounted = "NA";
                this.m_NumberOfObservers = "NA";
            }

            InitializeComponent();

            DataContext = this;


            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockShowDocument);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
        }

        public YellowstonePathology.Business.Test.HER2AnalysisSummary.HER2AnalysisSummaryTestOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder HER2AmplificationByISHTestOrder
        {
            get { return this.m_HER2AmplificationByISHTestOrder; }
        }

        public YellowstonePathology.Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC PanelSetOrderHer2AmplificationByIHC
        {
            get { return this.m_PanelSetOrderHer2AmplificationByIHC; }
        }

        public YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTestOrder HER2AmplificationRecountTestOrder
        {
            get { return this.m_HER2AmplificationRecountTestOrder; }
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

        public string CellsCounted
        {
            get { return this.m_CellsCounted; }
        }

        public string Her2SignalsCounted
        {
            get { return this.m_Her2SignalsCounted; }
        }

        public string Chr17SignalsCounted
        {
            get { return this.m_Chr17SignalsCounted; }
        }

        public string NumberOfObservers
        {
            get { return this.m_NumberOfObservers; }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            this.Next(this, new EventArgs());
        }

        private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Audit.Model.AuditResult result = this.m_PanelSetOrder.IsOkToFinalize(this.m_AccessionOrder);
            if (result.Status == Business.Audit.Model.AuditStatusEnum.OK)
            {
                YellowstonePathology.Business.Test.FinalizeTestResult finalizeTestResult = this.m_PanelSetOrder.Finish(this.m_AccessionOrder);
                this.HandleFinalizeTestResult(finalizeTestResult);
                YellowstonePathology.Business.Test.Surgical.SurgicalTest panelSetSurgical = new YellowstonePathology.Business.Test.Surgical.SurgicalTest();

                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetSurgical.PanelSetId) == true)
                {
                    YellowstonePathology.Business.Test.PanelSetOrder surgicalPanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetSurgical.PanelSetId);
                    YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(surgicalPanelSetOrder.ReportNo);
                    if (amendmentCollection.HasAmendmentForReport(this.m_PanelSetOrder.ReportNo) == false)
                    {
                        string amendmentText = Business.Test.HER2AnalysisSummary.HER2AnalysisSummarySystemGeneratedAmendmentText.AmendmentText(this.m_PanelSetOrder);
                        YellowstonePathology.Business.Amendment.Model.Amendment amendment = this.m_AccessionOrder.AddAmendment(surgicalPanelSetOrder.ReportNo);
                        amendment.TestResultAmendmentFill(surgicalPanelSetOrder.ReportNo, surgicalPanelSetOrder.AssignedToId, amendmentText);
                        amendment.ReferenceReportNo = this.m_PanelSetOrder.ReportNo;
                        amendment.SystemGenerated = true;
                    }
                }
            }
            else
            {
                MessageBox.Show(result.Message);
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

        private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
        {
                YellowstonePathology.Business.Test.HER2AnalysisSummary.HER2AnalysisSummaryWordDocument report = new YellowstonePathology.Business.Test.HER2AnalysisSummary.HER2AnalysisSummaryWordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
                report.Render();
                YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWord(report.SaveFileName);
        }

        private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Audit.Model.AuditResult result = this.m_PanelSetOrder.IsOkToAccept(this.m_AccessionOrder);
            if (result.Status == Business.Audit.Model.AuditStatusEnum.OK)
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
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToUnaccept();
            if (methodResult.Success == true)
            {
                this.m_PanelSetOrder.Unaccept();
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkSetResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Audit.Model.AuditResult result = this.m_PanelSetOrder.IsOkToSetResults(this.m_AccessionOrder);
            if(result.Status == Business.Audit.Model.AuditStatusEnum.OK)
            {
                YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationResultCollection her2AmplificationResultCollection = new Business.Test.HER2AmplificationByISH.HER2AmplificationResultCollection(this.m_AccessionOrder.PanelSetOrderCollection, this.m_PanelSetOrder);
                YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationResult her2AmplificationResult = her2AmplificationResultCollection.FindMatch();
                if(her2AmplificationResult != null)
                {
                    YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
                    her2AmplificationResult.SetSummaryResults(specimenOrder);

                    if (string.IsNullOrEmpty(this.m_PanelSetOrder.Result) == true)
                    {
                        MessageBox.Show("The result needs to be determined by internal adjudication.");
                    }
                }    
                else
                {
                    MessageBox.Show("A matching result was not found for the provided counts.");
                }
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }
    }
}

