using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Rules
{
    public class RuleAcceptBoneMarrowSummaryOnLastFinal
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        protected YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;
        protected YellowstonePathology.Business.Rules.Rule m_Rule;

        public RuleAcceptBoneMarrowSummaryOnLastFinal()
        {
            this.m_Rule = new Rules.Rule();
            this.m_Rule.ActionList.Add(AcceptSummary);
        }

        public void AcceptSummary()
        {
            YellowstonePathology.Business.Test.BoneMarrowSummary.BoneMarrowSummaryTest boneMarrowSummaryTest = new Test.BoneMarrowSummary.BoneMarrowSummaryTest(); ;
            if (this.m_AccessionOrder.PanelSetOrderCollection.DoesPanelSetExist(boneMarrowSummaryTest.PanelSetId) == true)
            {
                List < int > exclusionList = this.m_AccessionOrder.PanelSetOrderCollection.GetBoneMarrowSummaryExclusionList();
                if(this.m_AccessionOrder.PanelSetOrderCollection.IsLastReportInSummaryToFinal(exclusionList, this.m_PanelSetOrder.PanelSetId) == true)
                {
                    Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(boneMarrowSummaryTest.PanelSetId);
                    if (panelSetOrder.Final == false)
                    {
                        panelSetOrder.ExpectedFinalTime = DateTime.Now;
                        panelSetOrder.Accept();
                        panelSetOrder.AcceptedBy = "Administrator";
                        panelSetOrder.AcceptedById = 5051;
                    }
                }
            }

        }

        public void Execute(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = panelSetOrder;
            this.m_ExecutionStatus = new Rules.ExecutionStatus();
            this.m_Rule.Execute(this.m_ExecutionStatus);
        }
    }
}
