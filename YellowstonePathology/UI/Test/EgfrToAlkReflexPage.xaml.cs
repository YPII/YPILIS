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
	public partial class EGFRToALKReflexPage : ResultControl, INotifyPropertyChanged
	{
		public delegate void PropertyChangedNotificationHandler(String info);
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void FinishEventHandler(object sender, EventArgs e);
		public event FinishEventHandler Finish;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;        

        public delegate void OrderPDL122C3EventHandler(object sender, EventArgs e);
        public event OrderPDL122C3EventHandler OrderPDL122C3;

        public delegate void OrderALKEventHandler(object sender, EventArgs e);
        public event OrderALKEventHandler OrderALK;

        public delegate void OrderROS1EventHandler(object sender, EventArgs e);
        public event OrderROS1EventHandler OrderROS1;

        public delegate void OrderBRAFEventHandler(object sender, EventArgs e);
        public event OrderBRAFEventHandler OrderBRAF;

        public delegate void OrderPDL122C3EsophagealSquamousCellEventHandler(object sender, EventArgs e);
        public event OrderPDL122C3EsophagealSquamousCellEventHandler OrderPDL122C3EsophagealSquamousCell;

        public delegate void OrderPDL122C3GastricGEAEventHandler(object sender, EventArgs e);
        public event OrderPDL122C3GastricGEAEventHandler OrderPDL122C3GastricGEA;

        public delegate void OrderPDL122C3HeadandNeckEventHandler(object sender, EventArgs e);
        public event OrderPDL122C3HeadandNeckEventHandler OrderPDL122C3HeadandNeck;

        public delegate void OrderPDL122C3NonsmallCellLungEventHandler(object sender, EventArgs e);
        public event OrderPDL122C3NonsmallCellLungEventHandler OrderPDL122C3NonsmallCellLung;

        public delegate void OrderPDL122C3UrothelialEventHandler(object sender, EventArgs e);
        public event OrderPDL122C3UrothelialEventHandler OrderPDL122C3Urothelial;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

        private YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder m_EGFRToALKReflexAnalysisTestOrder;
        private YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder m_ALKForNSCLCByFISHTestOrder;
        private YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder m_EGFRMutationAnalysisTestOrder;

        private string m_PageHeaderText;
        private string m_OrderedOnDescription;
        private System.Windows.Visibility m_BackButtonVisibility;

        public EGFRToALKReflexPage(YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder testOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity,
            System.Windows.Visibility backButtonVisibility) : base(testOrder, accessionOrder)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;
            this.m_BackButtonVisibility = backButtonVisibility;

			this.m_EGFRToALKReflexAnalysisTestOrder = testOrder;
            this.m_EGFRToALKReflexAnalysisTestOrder.SetStatus(this.m_AccessionOrder.PanelSetOrderCollection);

            this.m_EGFRMutationAnalysisTestOrder = (YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(60);
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(131) == true)
            {
                this.m_ALKForNSCLCByFISHTestOrder = (YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(131);
                
            }
            else if(this.m_AccessionOrder.PanelSetOrderCollection.Exists(68) == true)
            {
                this.m_ALKForNSCLCByFISHTestOrder = new YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder();
            }

            this.m_PageHeaderText = this.m_EGFRToALKReflexAnalysisTestOrder.PanelSetName + " for: " + this.m_AccessionOrder.PatientDisplayName;

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_EGFRToALKReflexAnalysisTestOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(this.m_EGFRToALKReflexAnalysisTestOrder.OrderedOnId);
            this.m_OrderedOnDescription = specimenOrder.Description;
            if (aliquotOrder != null) this.m_OrderedOnDescription += ": " + aliquotOrder.Label;

            InitializeComponent();

			this.DataContext = this;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonBack);
            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonFinish);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockShowDocument);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
        }

        public System.Windows.Visibility BackButtonVisibility
        {
            get { return this.m_BackButtonVisibility; }
        }

        public string OrderedOnDescription
        {
            get { return this.m_OrderedOnDescription; }
        }

        public YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder EGFRToALKReflexAnalysisTestOrder
		{
			get { return this.m_EGFRToALKReflexAnalysisTestOrder; }
		}

        public YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder ALKForNSCLCByFISHTestOrder
        {
            get { return this.m_ALKForNSCLCByFISHTestOrder; }
        }

        public YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder EGFRMutationAnalysisTestOrder
        {
            get { return this.m_EGFRMutationAnalysisTestOrder; }
        }

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}				

		public string PageHeaderText
		{
            get { return this.m_PageHeaderText; }
		}                

        private void HyperLinkOrderPDL122C3_Click(object sender, RoutedEventArgs e)
        {
            this.OrderPDL122C3(this, new EventArgs());
        }

        private void HyperLinkOrderROS1_Click(object sender, RoutedEventArgs e)
        {
            this.OrderROS1(this, new EventArgs());
        }

        private void HyperLinkOrderALK_Click(object sender, RoutedEventArgs e)
        {
            this.OrderALK(this, new EventArgs());
        }

        private void HyperLinkOrderBRAF_Click(object sender, RoutedEventArgs e)
        {
            this.OrderBRAF(this, new EventArgs());
        }

        private void HyperLinkOrderPDL122C3EsophagealSquamousCell_Click(object sender, RoutedEventArgs e)
        {
            this.OrderPDL122C3EsophagealSquamousCell(this, new EventArgs());
        }

        private void HyperLinkOrderPDL122C3GastricGEA_Click(object sender, RoutedEventArgs e)
        {
            this.OrderPDL122C3GastricGEA(this, new EventArgs());
        }

        private void HyperLinkOrderPDL122C3HeadandNeck_Click(object sender, RoutedEventArgs e)
        {
            this.OrderPDL122C3HeadandNeck(this, new EventArgs());
        }

        private void HyperLinkOrderPDL122C3NonsmallCellLung_Click(object sender, RoutedEventArgs e)
        {
            this.OrderPDL122C3NonsmallCellLung(this, new EventArgs());
        }

        private void HyperLinkOrderPDL122C3Urothelial_Click(object sender, RoutedEventArgs e)
        {
            this.OrderPDL122C3Urothelial(this, new EventArgs());
        }


        private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{            
            YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisWordDocument report = new Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisWordDocument(this.m_AccessionOrder, this.m_EGFRToALKReflexAnalysisTestOrder, Business.Document.ReportSaveModeEnum.Draft);
            report.Render();
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWord(report.SaveFileName);
		}

		private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
		{
            bool okToFinal = false;
            Business.Audit.Model.AuditResult auditResult = this.m_EGFRToALKReflexAnalysisTestOrder.IsOkToFinalize(this.m_AccessionOrder);
            if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.OK)
            {
                okToFinal = true;
            }
            else if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.Warning)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(auditResult.Message, "Results do not match the component report results",
                    MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    okToFinal = true;
                }
            }
            else
            {
                MessageBox.Show(auditResult.Message, "Unable to final");
            }

            if (okToFinal == true)
            {
                YellowstonePathology.Business.Test.FinalizeTestResult finalizeTestResult = this.m_EGFRToALKReflexAnalysisTestOrder.Finish(this.m_AccessionOrder);
                this.HandleFinalizeTestResult(finalizeTestResult);
                if (this.m_EGFRToALKReflexAnalysisTestOrder.Accepted == false)
                {
                    this.m_EGFRToALKReflexAnalysisTestOrder.Accept();
                }
            }
        }

        private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_EGFRToALKReflexAnalysisTestOrder.Final == true)
			{
				this.m_EGFRToALKReflexAnalysisTestOrder.Unfinalize();				
			}
		}

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Business.Audit.Model.AuditResult result = this.m_EGFRToALKReflexAnalysisTestOrder.IsOkToAccept(this.m_AccessionOrder);
            if (result.Status == Business.Audit.Model.AuditStatusEnum.OK)
            {
                this.m_EGFRToALKReflexAnalysisTestOrder.Accept();
            }
            else if (result.Status == Business.Audit.Model.AuditStatusEnum.Warning)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(result.Message, "Results do not match the component report results",
                    MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    this.m_EGFRToALKReflexAnalysisTestOrder.Accept();
                }
            }
            else
            {
                MessageBox.Show(result.Message, "Unable to Accept.");
            }
        }

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_EGFRToALKReflexAnalysisTestOrder.IsOkToUnaccept();
			if (result.Success == true)
			{
				this.m_EGFRToALKReflexAnalysisTestOrder.Unaccept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}
		}

        private void ButtonFinish_Click(object sender, RoutedEventArgs e)
        {
            if (this.Finish != null) this.Finish(this, new EventArgs());   
        }        

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Back != null) this.Back(this, new EventArgs());   
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

        private void HyperLinkSetResults_Click(object sender, RoutedEventArgs e)
        {
            Business.Audit.Model.AuditResult result = this.m_EGFRToALKReflexAnalysisTestOrder.IsOkToSetPreviousResults(this.m_EGFRToALKReflexAnalysisTestOrder, this.m_AccessionOrder);
            if (result.Status == Business.Audit.Model.AuditStatusEnum.OK)
            {
                this.m_EGFRToALKReflexAnalysisTestOrder.SetResults(this.m_AccessionOrder.PanelSetOrderCollection);
            }
            else
            {
                MessageBox.Show(result.Message, "Unable to set Results");
            }
        }

        private void HyperLinkSetQNS_Click(object sender, RoutedEventArgs e)
        {
            this.m_EGFRToALKReflexAnalysisTestOrder.Comment = Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder.QNSStatement;
        }
    }
}
