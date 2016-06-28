using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.Cytology
{
	public class AssignScreening
	{
		string m_MasterAccessionNo;
        int m_AssignedToId;

		YellowstonePathology.Business.Test.AccessionOrder m_CytologyAccessionOrder;
		YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology m_PanelSetOrderCytology;

        YellowstonePathology.Business.Rules.Rule m_Rule;
        YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;

        object m_Writer;

		public AssignScreening(object writer)
        {
            this.m_Writer = writer;
            this.m_Rule = new YellowstonePathology.Business.Rules.Rule();
            this.m_Rule.ActionList.Add(this.Assign);
        }        

        public void Assign()
        {
			this.m_CytologyAccessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(this.m_MasterAccessionNo, this.m_Writer);
			this.m_PanelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_CytologyAccessionOrder.PanelSetOrderCollection.GetPAP();			

			this.m_PanelSetOrderCytology.AssignedToId = this.m_AssignedToId;
			foreach (YellowstonePathology.Business.Interface.IPanelOrder panelOrder in this.m_PanelSetOrderCytology.PanelOrderCollection)
            {
                Type objectType = panelOrder.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                {
                    YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology cytologyPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)panelOrder;
                    if (cytologyPanelOrder.Accepted == false)
                    {
                        cytologyPanelOrder.AssignedToId = this.m_AssignedToId;
                    }
                }
            }            
        }

		public void Execute(string masterAccessionNo, int assignToId, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
		{
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_AssignedToId = assignToId;
			this.m_ExecutionStatus = executionStatus;
			this.m_Rule.Execute(executionStatus);
		}
	}
}
