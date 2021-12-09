using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for BoneMarrowSummaryResultPage.xaml
    /// </summary>
    public partial class BoneMarrowSummaryResultPage : ResultControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
        private YellowstonePathology.Business.Test.BoneMarrowSummary.OtherReportViewCollection m_AccessionReportsIncluded;
        private YellowstonePathology.Business.Test.BoneMarrowSummary.OtherReportViewCollection m_OtherReportViewCollection;
        private string m_PageHeaderText;

        public BoneMarrowSummaryResultPage(YellowstonePathology.Business.Test.PanelSetOrder testOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator) : base(testOrder, accessionOrder)
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = testOrder;
            this.m_SystemIdentity = systemIdentity;
            this.m_PageNavigator = pageNavigator;

            this.m_PageHeaderText = "Bone Marrow Summary Results For: " + this.m_AccessionOrder.PatientDisplayName;

            this.SetAccessionReportsIncluded();
            this.m_OtherReportViewCollection = Business.Gateway.AccessionOrderGateway.GetOtherReportViewCollection(this.m_AccessionOrder.PatientId, this.m_AccessionOrder.MasterAccessionNo);

            InitializeComponent();

            DataContext = this;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockShowDocument);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public string PageHeaderText
        {
            get { return this.m_PageHeaderText; }
        }

        public YellowstonePathology.Business.Test.BoneMarrowSummary.OtherReportViewCollection AccessionReportsIncluded
        {
            get { return this.m_AccessionReportsIncluded; }
        }

        public YellowstonePathology.Business.Test.BoneMarrowSummary.OtherReportViewCollection OtherReportViewCollection
        {
            get { return this.m_OtherReportViewCollection; }
            private set
            {
                this.m_OtherReportViewCollection = value;
                NotifyPropertyChanged("OtherReportViewCollection");

            }
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
            YellowstonePathology.Business.Audit.Model.AuditResult auditResult = this.m_PanelSetOrder.IsOkToFinalize(this.m_AccessionOrder);
            if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.OK)
            {                
                this.m_PanelSetOrder.Finish(this.m_AccessionOrder);
                if (this.m_PanelSetOrder.Accepted == false)
                {
                    this.m_PanelSetOrder.Accept();
                }                
            }
            else
            {
                MessageBox.Show(auditResult.Message);
            }
        }

        private bool AreAllResultsReportable()
        {
            bool result = true;
            Business.Test.PanelSetOrder pso = new Business.Test.PanelSetOrder();
            List<Business.Test.PanelSetOrder> panelSetOrders = this.m_AccessionOrder.PanelSetOrderCollection.GetBoneMarrowAccessionSummaryList(this.m_PanelSetOrder.ReportNo, true);
            foreach(Business.Test.PanelSetOrder panelSetOrder in panelSetOrders)
            {
                if(panelSetOrder.ToResultString(this.m_AccessionOrder) == pso.ToResultString(this.m_AccessionOrder))
                {
                    result = false;
                    break;
                }
            }
            return result;
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
                this.m_PanelSetOrder.Accept();
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
                this.m_PanelSetOrder.Unaccept();
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkSetResults_Click(object sender, RoutedEventArgs e)
        {
            /*YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToSetResults();
            if (methodResult.Success == true)
            {
                if (this.ComboBoxResult.SelectedItem != null)
                {
                    YellowstonePathology.Business.Test.TestResult testResult = (YellowstonePathology.Business.Test.TestResult)this.ComboBoxResult.SelectedItem;
                    YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FResult result = (YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FResult)testResult;
                    result.SetResults(this.m_PanelSetOrder);
                }
                else
                {
                    MessageBox.Show("A result must be Selected before results can be set.");
                }
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }*/
        }

        private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
            YellowstonePathology.Business.Test.BoneMarrowSummary.BoneMarrowSummaryWordDocument report = new YellowstonePathology.Business.Test.BoneMarrowSummary.BoneMarrowSummaryWordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
            report.Render();
            YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWord(report.SaveFileName);
        }

        private void HyperLinkAddSelectedReport_Click(object sender, RoutedEventArgs e)
        {
            if(ListViewOtherReports.SelectedItem != null)
            {
                YellowstonePathology.Business.Test.BoneMarrowSummary.OtherReportView otherReportView = ListViewOtherReports.SelectedItem as YellowstonePathology.Business.Test.BoneMarrowSummary.OtherReportView;
                Business.Rules.MethodResult methodResult = this.CanAddReportToSummary(otherReportView);
                if (methodResult.Success == true)
                {
                    this.AddOtherReport(otherReportView);
                }
                else
                {
                    MessageBox.Show(methodResult.Message);
                }
            }
            else
            {
                MessageBox.Show("Select a report to add");
            }
        }

        private Business.Rules.MethodResult CanAddReportToSummary(Business.Test.BoneMarrowSummary.OtherReportView otherReportView)
        {
            Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            if (string.IsNullOrEmpty(otherReportView.SummaryReportNo) == false)
            {
                methodResult.Success = false;
                methodResult.Message = "The selected report is included in a summary.";
            }

            if (methodResult.Success == true)
            {
                List<int> exclusionList = this.m_AccessionOrder.PanelSetOrderCollection.GetBoneMarrowSummaryExclusionList();
                if (exclusionList.IndexOf(otherReportView.PanelSetId) > -1)
                {
                    methodResult.Success = false;
                    methodResult.Message = "The selected report is not valid in this summary.";
                }
            }

            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Test.BoneMarrowSummary.BoneMarrowSummaryTest bmsTest = new Business.Test.BoneMarrowSummary.BoneMarrowSummaryTest();
                Business.Test.AccessionOrder accessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(otherReportView.MasterAccessionNo, this);

                if (accessionOrder.PanelSetOrderCollection.Exists(bmsTest.PanelSetId) == true)
                {
                    methodResult.Success = false;
                    methodResult.Message = "The selected report is included in previous summary and may not be included in this summary.";
                }
            }

            return methodResult;
        }

        private void AddOtherReport(YellowstonePathology.Business.Test.BoneMarrowSummary.OtherReportView otherReportView)
        {
            Business.Test.AccessionOrder accessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(otherReportView.MasterAccessionNo, this);
            if (accessionOrder.AccessionLock.IsLockAquiredByMe == true)
            {
                Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(otherReportView.ReportNo);
                panelSetOrder.SummaryReportNo = this.m_PanelSetOrder.ReportNo;
                Business.Persistence.DocumentGateway.Instance.Save();
                this.OtherReportViewCollection = Business.Gateway.AccessionOrderGateway.GetOtherReportViewCollection(this.m_AccessionOrder.PatientId, this.m_AccessionOrder.MasterAccessionNo);
            }
            else
            {
                MessageBox.Show("Unable to add the selected report to the summary as that accession is locked.");
            }
        }

        private void HyperLinkRemoveSelectedReport_Click(object sender, RoutedEventArgs e)
        {
            if (ListViewOtherReports.SelectedItem != null)
            {
                YellowstonePathology.Business.Test.BoneMarrowSummary.OtherReportView otherReportView = ListViewOtherReports.SelectedItem as YellowstonePathology.Business.Test.BoneMarrowSummary.OtherReportView;
                if (string.IsNullOrEmpty(otherReportView.SummaryReportNo) == false && otherReportView.SummaryReportNo == this.m_PanelSetOrder.ReportNo)
                {
                    Business.Test.AccessionOrder accessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(otherReportView.MasterAccessionNo, this);
                    if (accessionOrder.AccessionLock.IsLockAquiredByMe == true)
                    {
                        Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(otherReportView.ReportNo);
                        panelSetOrder.SummaryReportNo = null;
                        Business.Persistence.DocumentGateway.Instance.Save();
                        this.OtherReportViewCollection = Business.Gateway.AccessionOrderGateway.GetOtherReportViewCollection(this.m_AccessionOrder.PatientId, this.m_AccessionOrder.MasterAccessionNo);
                    }
                    else
                    {
                        MessageBox.Show("Unable to remove the selected report as that accession is locked.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a report to remove");
            }
        }

        private void SetAccessionReportsIncluded()
        {
            this.m_AccessionReportsIncluded = new YellowstonePathology.Business.Test.BoneMarrowSummary.OtherReportViewCollection();
            List<YellowstonePathology.Business.Test.PanelSetOrder> panelSetOrders = this.m_AccessionOrder.PanelSetOrderCollection.GetBoneMarrowAccessionSummaryList(this.m_PanelSetOrder.ReportNo, false);
            foreach (Business.Test.PanelSetOrder pso in panelSetOrders)
            {
                YellowstonePathology.Business.Test.BoneMarrowSummary.OtherReportView view = new Business.Test.BoneMarrowSummary.OtherReportView();
                view.ReportNo = pso.ReportNo;
                view.PanelSetName = pso.PanelSetName;
                view.IncludeOnSummaryReport = pso.IncludeOnSummaryReport;
                this.m_AccessionReportsIncluded.Add(view);
            }
        }

        private void HyperLinkExcludeSelectedReport_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListViewAccessionReports.SelectedItem!= null)
            {
                Business.Test.BoneMarrowSummary.OtherReportView otherReportView = (Business.Test.BoneMarrowSummary.OtherReportView)this.ListViewAccessionReports.SelectedItem;
                Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(otherReportView.ReportNo);
                panelSetOrder.IncludeOnSummaryReport = false;
                this.SetAccessionReportsIncluded();
                this.NotifyPropertyChanged(string.Empty);
            }
        }

        private void HyperLinkIncludeSelectedReport_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewAccessionReports.SelectedItem != null)
            {
                Business.Test.BoneMarrowSummary.OtherReportView otherReportView = (Business.Test.BoneMarrowSummary.OtherReportView)this.ListViewAccessionReports.SelectedItem;
                Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(otherReportView.ReportNo);
                panelSetOrder.IncludeOnSummaryReport = true;
                this.SetAccessionReportsIncluded();
                this.NotifyPropertyChanged(string.Empty);
            }
        }
    }
}
