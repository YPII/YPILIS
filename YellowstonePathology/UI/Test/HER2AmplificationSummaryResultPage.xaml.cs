using System;
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
        private YellowstonePathology.Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryTestOrder m_PanelSetOrder;
        private YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHIndicatorCollection m_IndicatorCollection;
        private YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHSampleAdequacyCollection m_SampleAdequacyCollection;
        private YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHProbeSignalIntensityCollection m_ProbeSignalIntensityCollection;
        private YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHGeneticHeterogeneityCollection m_GeneticHeterogeneityCollection;
        private string m_PageHeaderText;
        private string m_OrderedOnDescription;

        private Visibility m_RecountVisibility;


        public HER2AmplificationSummaryResultPage(YellowstonePathology.Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryTestOrder testOrder,
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
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_PanelSetOrder.OrderedOnId);
            this.m_OrderedOnDescription = orderTarget.GetDescription();

            YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTest test = new Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTest();
            this.m_RecountVisibility = this.m_AccessionOrder.PanelSetOrderCollection.Exists(test.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) ? Visibility.Visible : Visibility.Hidden;

            InitializeComponent();

            DataContext = this;


            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
        }

        public YellowstonePathology.Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryTestOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
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

        public Visibility RecountVisability
        {
            get { return this.m_RecountVisibility; }
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
            YellowstonePathology.Business.Audit.Model.AuditResult result = this.m_PanelSetOrder.IsOkToFinalize(this.m_AccessionOrder);
            if (result.Status == Business.Audit.Model.AuditStatusEnum.OK)
            {
                this.m_PanelSetOrder.Finish(this.m_AccessionOrder);
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
                YellowstonePathology.Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryWordDocument report = new YellowstonePathology.Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryWordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
                report.Render();
                YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(report.SaveFileName);
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
                this.m_PanelSetOrder.SetResults(this.m_AccessionOrder);
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }
    }
}

