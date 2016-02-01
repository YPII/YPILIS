﻿using System;
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
    public partial class HER2AmplificationByISHResultPage : UserControl, INotifyPropertyChanged, Business.Interface.IPersistPageChanges, IResultPage
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event YellowstonePathology.UI.CustomEventArgs.EventHandlerDefinitions.CancelTestEventHandler CancelTest;

		public delegate void SpecimenDetailEventHandler(object sender, EventArgs e);
		public event SpecimenDetailEventHandler SpecimenDetail;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
        private YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder m_PanelSetOrder;
        private YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHResultCollection m_ResultCollection;
        private YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHIndicatorCollection m_IndicatorCollection;
        private YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHSampleAdequacyCollection m_SampleAdequacyCollection;
        private YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHProbeSignalIntensityCollection m_ProbeSignalIntensityCollection;
        private YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHGeneticHeterogeneityCollection m_GeneticHeterogeneityCollection;
        private string m_PageHeaderText;
        private string m_OrderedOnDescription;


        public HER2AmplificationByISHResultPage(YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder testOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
        {
            this.m_PanelSetOrder = testOrder;
            this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = systemIdentity;
            this.m_PageNavigator = pageNavigator;

            this.m_PageHeaderText = "HER2 Amplification By ISH Results For: " + this.m_AccessionOrder.PatientDisplayName;
            this.m_ResultCollection = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHResultCollection();
            this.m_IndicatorCollection = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHIndicatorCollection();
            this.m_SampleAdequacyCollection = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHSampleAdequacyCollection();
            this.m_ProbeSignalIntensityCollection = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHProbeSignalIntensityCollection();
            this.m_GeneticHeterogeneityCollection = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHGeneticHeterogeneityCollection();
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_PanelSetOrder.OrderedOnId);
            this.m_OrderedOnDescription = orderTarget.GetDescription();
            InitializeComponent();

            DataContext = this;

            Loaded += HER2AmplificationByISHResultPage_Loaded;
            Unloaded += HER2AmplificationByISHResultPage_Unloaded;
        }

        private void HER2AmplificationByISHResultPage_Loaded(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Persistence.ObjectTrackerV2.Instance.RegisterObject(this.m_AccessionOrder, this);
        }

        private void HER2AmplificationByISHResultPage_Unloaded(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Persistence.ObjectTrackerV2.Instance.CleanUp(this);
        }

        public YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        public YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHResultCollection ResultCollection
        {
            get { return this.m_ResultCollection; }
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

        public bool OkToSaveOnNavigation(Type pageNavigatingTo)
        {
            return true;
        }

        public bool OkToSaveOnClose()
        {
            return true;
        }

        public void Save()
        {
            YellowstonePathology.Business.Persistence.ObjectTrackerV2.Instance.SubmitChanges(this.m_AccessionOrder, this);
        }

        public void UpdateBindingSources()
        {

        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            this.Next(this, new EventArgs());
        }

        private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
        {            
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToFinalize();
            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);                    
                this.m_PanelSetOrder.Finalize(this.m_SystemIdentity.User);

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
            else
            {
                MessageBox.Show(methodResult.Message);
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
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToAccept();
            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);

                YellowstonePathology.Business.Audit.Model.AuditCollection auditCollection = new Business.Audit.Model.AuditCollection();                    
                auditCollection.Add(new Business.Audit.Model.HER2OKToAcceptAudit(this.m_PanelSetOrder));
                YellowstonePathology.Business.Audit.Model.AuditResult auditResult = auditCollection.Run2();

                if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.Failure)
                {
                    MessageBox.Show(auditResult.Message);
                }
                else
                {
                    YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHResult.AcceptResults(this.m_PanelSetOrder, this.m_SystemIdentity);
                }
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }            
        }

        private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToUnaccept();
            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHResult.UnacceptResults(this.m_PanelSetOrder);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkSetResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToSetResults();
            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHResult result = this.m_ResultCollection.GetResultFromIndication(this.m_PanelSetOrder);
                result.SetResults(this.m_PanelSetOrder);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.m_PanelSetOrder.Indicator) == false)
            {
                this.Save();
                YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHWordDocument report = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHWordDocument();
                report.Render(this.m_PanelSetOrder.MasterAccessionNo, this.m_PanelSetOrder.ReportNo, Business.Document.ReportSaveModeEnum.Draft);

				YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
                string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
                YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
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

        /*private void CancellATestPath_Finish(object sender, EventArgs e)
        {
            this.m_PageNavigator.Navigate(this);
        }*/

		private void HyperlinkSpecimenDetails_Click(object sender, RoutedEventArgs e)
		{
			if (this.SpecimenDetail != null)
			{
				this.SpecimenDetail(this, new EventArgs());
			}
		}
    }
}
