using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login
{
    public class FlowCytometryOrderPath
    {
        public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
        public event ReturnEventHandler Return;        

        private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
        private YellowstonePathology.Business.PanelSet.Model.PanelSet m_PanelSet;
        private YellowstonePathology.Business.Test.PanelSetOrder m_AddedPanelSetOrder;

        public FlowCytometryOrderPath(YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet, YellowstonePathology.UI.Navigation.PageNavigator pageNavigator, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_PanelSet = panelSet;
            this.m_PageNavigator = pageNavigator;
            this.m_AccessionOrder = accessionOrder;
        }

		public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
        {
            get { return this.m_SpecimenOrder; }
            set { this.m_SpecimenOrder = value; }
        }

        public YellowstonePathology.Business.PanelSet.Model.PanelSet PanelSet
        {
            get { return this.m_PanelSet; }
            set { this.m_PanelSet = value; }
        }

        public void Start()
        {
            this.ShowSpecimenSelectionPage();
        }        

        private void ShowSpecimenSelectionPage()
        {
            YellowstonePathology.UI.Login.Receiving.FlowSpecimenSelectionPage specimenSelectionPage = new Receiving.FlowSpecimenSelectionPage(this.m_AccessionOrder.SpecimenOrderCollection);
            specimenSelectionPage.Back += new Receiving.FlowSpecimenSelectionPage.BackEventHandler(SpecimenSelectionPage_Back);
            specimenSelectionPage.UseThisSpecimen += new Receiving.FlowSpecimenSelectionPage.UseThisSpecimenEventHandler(SpecimenSelectionPage_UseThisSpecimen);
            this.m_PageNavigator.Navigate(specimenSelectionPage);
        }

        private void SpecimenSelectionPage_UseThisSpecimen(object sender, CustomEventArgs.SpecimenOrderReturnEventArgs e)
        {
            this.m_SpecimenOrder = e.SpecimenOrder;
            this.ShowFinishPage();
        }

        private void SpecimenSelectionPage_Back(object sender, EventArgs e)
        {
            this.EndPath();
        }        

        private void ShowFinishPage()
        {            
            YellowstonePathology.UI.Login.Receiving.FlowCytometryOrderFinishPage finishPage = new Receiving.FlowCytometryOrderFinishPage(this.m_AccessionOrder, this.m_SpecimenOrder, this.m_PanelSet);
            finishPage.Return += new Receiving.FlowCytometryOrderFinishPage.ReturnEventHandler(FinishPage_Return);
            this.m_PageNavigator.Navigate(finishPage);
        }

        private void FinishPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {
            switch (e.PageNavigationDirectionEnum)
            {
                case UI.Navigation.PageNavigationDirectionEnum.Finish:
                    this.OrderTheReport();
                    this.ShowReportNumberPage();
                    break;
                case UI.Navigation.PageNavigationDirectionEnum.Back:
                    this.ShowSpecimenSelectionPage();
                    break;
            }
        }

        private void ShowReportNumberPage()
        {            
			YellowstonePathology.UI.Login.Receiving.ReportOrderInfoPage reportOrderInfoPage = new Receiving.ReportOrderInfoPage(this.m_AccessionOrder, this.m_AddedPanelSetOrder);
            reportOrderInfoPage.Return += new Receiving.ReportOrderInfoPage.ReturnEventHandler(ReportOrderInfoPage_Return);
            this.m_PageNavigator.Navigate(reportOrderInfoPage);
        }

        private void ReportOrderInfoPage_Return(object sender, EventArgs e)
        {
            if (this.ShowSurgicalDiagnosisPage() == false)
            {
                this.Return(this, null);
            }
        }

        private bool ShowSurgicalDiagnosisPage()
        {
            bool result = false;
            if (this.m_AccessionOrder.PanelSetOrderCollection.HasSurgical() == true)
            {
                YellowstonePathology.UI.Login.Receiving.SurgicalDiagnosisPage surgicalDiagnosisPage = new Receiving.SurgicalDiagnosisPage(this.m_AccessionOrder);
                surgicalDiagnosisPage.Return += new Receiving.SurgicalDiagnosisPage.ReturnEventHandler(SurgicalDiagnosisPage_Return);
                this.m_PageNavigator.Navigate(surgicalDiagnosisPage);
                result = true;
            }
            return result;
        }

        private void SurgicalDiagnosisPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {
            this.Return(this, null);
        }

        private void OrderTheReport()
        {

            YellowstonePathology.Business.User.SystemIdentity systemIdentity = YellowstonePathology.Business.User.SystemIdentity.Instance;
            YellowstonePathology.Business.PanelSet.Model.FlowCytometry.PanelSetFlowCytometry panelSetFlowCytometry = null;

            switch (this.m_PanelSet.PanelSetId)
            {
                case 20: //LLP
                    panelSetFlowCytometry = (YellowstonePathology.Business.Test.LLP.LeukemiaLymphomaTest)this.m_PanelSet;                    
                    break;
                case 21: //TPP
					panelSetFlowCytometry = (YellowstonePathology.Business.Test.ThrombocytopeniaProfile.ThrombocytopeniaProfileTest)this.m_PanelSet;                    
                    break;
                case 22: //PAA
					panelSetFlowCytometry = (YellowstonePathology.Business.Test.PlateletAssociatedAntibodies.PlateletAssociatedAntibodiesTest)this.m_PanelSet;
                    break;
                case 23: //RTIC
					panelSetFlowCytometry = (YellowstonePathology.Business.Test.ReticulatedPlateletAnalysis.ReticulatedPlateletAnalysisTest)this.m_PanelSet;
                    break;
                case 24: //Stem Cell
					panelSetFlowCytometry = (YellowstonePathology.Business.Test.StemCellEnumeration.StemCellEnumerationTest)this.m_PanelSet;
                    break;
                case 28: //Fetal Hemoglobin
					panelSetFlowCytometry = (YellowstonePathology.Business.Test.FetalHemoglobin.FetalHemoglobinTest)this.m_PanelSet;
                    break;
            }

			string reportNo = null;
			bool newStyleReportNo = this.m_AccessionOrder.UseNewStyleReportNo();
			if (newStyleReportNo == true)
			{
				reportNo = YellowstonePathology.Business.OrderIdParser.GetNextReportNo(this.m_AccessionOrder.PanelSetOrderCollection, panelSetFlowCytometry, this.m_AccessionOrder.MasterAccessionNo);
			}
			else
			{
				reportNo = YellowstonePathology.Business.Gateway.AccessionOrderGateway.NextReportNo(this.m_PanelSet.PanelSetId, this.m_AccessionOrder.MasterAccessionNo);
			}

			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma panelSetOrderLeukemiaLymphoma = new Business.Test.LLP.PanelSetOrderLeukemiaLymphoma(this.m_AccessionOrder.MasterAccessionNo, reportNo, objectId, panelSetFlowCytometry, this.m_SpecimenOrder, true);            
            this.m_AccessionOrder.PanelSetOrderCollection.Add(panelSetOrderLeukemiaLymphoma);
            this.m_AddedPanelSetOrder = panelSetOrderLeukemiaLymphoma;

            if (this.m_AccessionOrder.ClientId != 0 && this.m_AccessionOrder.PhysicianId != 0)
            {
                YellowstonePathology.Business.Client.Model.PhysicianClientDistributionList physicianClientDistributionCollection = YellowstonePathology.Business.Gateway.ReportDistributionGateway.GetPhysicianClientDistributionCollection(this.m_AccessionOrder.PhysicianId, this.m_AccessionOrder.ClientId);
                foreach (YellowstonePathology.Business.Client.Model.PhysicianClientDistributionListItem physicianClientDistribution in physicianClientDistributionCollection)
                {
                    physicianClientDistribution.SetDistribution(panelSetOrderLeukemiaLymphoma, this.m_AccessionOrder);
                }
            }            
        }
        
        private void EndPath()
        {
            if (this.Return != null)
                this.Return(this, null);
        }
    }
}
