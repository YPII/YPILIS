using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Rules.Cytology
{
	public class DeletePanelOrder
	{		
		YellowstonePathology.Business.Interface.IPanelOrder m_PanelOrderToDelete;

		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.User.SystemUser m_CurrentUser;
		YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology m_PanelSetOrderCytology;

        YellowstonePathology.Business.Rules.Rule m_Rule;
        YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;

		public DeletePanelOrder()
        {
            this.m_Rule = new YellowstonePathology.Business.Rules.Rule();

            this.m_Rule.ActionList.Add(HaltIfNotOkToDelete);                     
            this.m_Rule.ActionList.Add(RemovePanelOrder);           			
		}

        private void HaltIfNotOkToDelete()
        {           
            Type objectType = this.m_PanelOrderToDelete.GetType();
            if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
            {
                YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology cytologyPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)this.m_PanelOrderToDelete;
                if (cytologyPanelOrder.ScreeningType.ToUpper() != "DOT REVIEW")
                {
					if (this.m_PanelSetOrderCytology.Final == true)
                    {
                        if (cytologyPanelOrder.Accepted == true)
                        {
                            this.m_ExecutionStatus.AddMessage("Case Is Final", true);
                        }
                    }                    
                }
                if (cytologyPanelOrder.ScreeningType.ToUpper() == "PRIMARY SCREENING")
                {
                    this.m_ExecutionStatus.AddMessage("Can't Delete Primary Screening", true);
                }
            }            
        }        

		private void RemovePanelOrder()
		{
			this.m_PanelSetOrderCytology.PanelOrderCollection.Remove(m_PanelOrderToDelete);            
		}

		public void Execute(YellowstonePathology.Business.Interface.IPanelOrder panelOrderToDelete, 
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Rules.ExecutionStatus executionStatus,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
			this.m_ExecutionStatus = executionStatus;
			this.m_AccessionOrder = accessionOrder;
			this.m_PanelOrderToDelete = panelOrderToDelete;
			this.m_PanelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelOrderToDelete.ReportNo);
            this.m_CurrentUser = systemIdentity.User;
			this.m_Rule.Execute(executionStatus);
		}
	}
}
