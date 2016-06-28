using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.Cytology
{
	public class SetResultToAgree
	{
        private YellowstonePathology.Business.Rules.Rule m_Rule;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology m_PanelOrderToSet;
        private YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;

		public SetResultToAgree()
        {
            this.m_Rule = new YellowstonePathology.Business.Rules.Rule();
            this.m_Rule.ActionList.Add(IsOkToSetResults);
            this.m_Rule.ActionList.Add(SetResults);            
        }

        private void IsOkToSetResults()
        {
            if (this.m_PanelOrderToSet.Comment == "Primary")
            {                
                this.m_Rule.ExecutionStatus.AddMessage("Not able to set results to agree on a primary screening panel", true);
            }
        }

        private void SetResults()
        {
			YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelOrderToSet.ReportNo);
            YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrderResult = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)panelSetOrderCytology.PanelOrderCollection[0];
            this.m_PanelOrderToSet.ScreeningImpression = panelOrderResult.ScreeningImpression;
            this.m_PanelOrderToSet.SpecimenAdequacy = panelOrderResult.SpecimenAdequacy;
            this.m_PanelOrderToSet.OtherConditions = panelOrderResult.OtherConditions;
            this.m_PanelOrderToSet.ReportComment = panelOrderResult.ReportComment;
            this.m_PanelOrderToSet.ResultCode = panelOrderResult.ResultCode;            
        }

        public void Execute(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrderToSet, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
		{
            this.m_PanelOrderToSet = panelOrderToSet;
            this.m_AccessionOrder = accessionOrder;
            this.m_ExecutionStatus = executionStatus;
            this.m_Rule.Execute(this.m_ExecutionStatus);
        }
	}
}
