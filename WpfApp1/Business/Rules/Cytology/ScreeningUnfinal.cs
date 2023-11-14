using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.Cytology
{
	public class ScreeningUnfinal
	{
		YellowstonePathology.Business.Test.AccessionOrder m_CytologyAccessionOrder;
        YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology m_PanelOrderToUnfinal;
		YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology m_PanelSetOrderCytology;

        YellowstonePathology.Business.Rules.Rule m_Rule;
        YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;

		public ScreeningUnfinal()
        {
            this.m_Rule = new YellowstonePathology.Business.Rules.Rule();
            this.m_Rule.ActionList.Add(HasFinalWHP);            
            this.m_Rule.ActionList.Add(UnfinalPanelSetOrder);            
            this.m_Rule.ActionList.Add(UnfinalPanelOrder);
        }

        public void UnfinalPanelSetOrder()
        {
			if (this.m_PanelSetOrderCytology.Final == true &&
					this.m_PanelSetOrderCytology.FinaledById == this.m_PanelOrderToUnfinal.AcceptedById)
            {
				this.m_PanelSetOrderCytology.Unfinalize();
				this.m_PanelSetOrderCytology.ScreenedById = 0;
				this.m_PanelSetOrderCytology.ScreenedByName = null;
			}
        }
        
        public void UnfinalPanelOrder()
        {
			if (this.m_PanelSetOrderCytology.Final == false)
            {
                YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology cytologyPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)this.m_PanelOrderToUnfinal;

                cytologyPanelOrder.Accepted = false;
                cytologyPanelOrder.AcceptedById = 0;
                cytologyPanelOrder.AssignedToId = 0;
                cytologyPanelOrder.AcceptedDate = null;
                cytologyPanelOrder.AcceptedTime = null;

                cytologyPanelOrder.ScreenedById = 0;
                cytologyPanelOrder.ScreenedByName = null;
            }
		}

        private void HasFinalWHP()
        {
            if (this.m_CytologyAccessionOrder.PanelSetOrderCollection.Exists(116) == true)
            {
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_CytologyAccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(116);
                if (panelSetOrder.Final == true)
                {
                    this.m_ExecutionStatus.Halted = true;
                    this.m_ExecutionStatus.AddMessage("This case cannot be unfinaled because a Womens Health Profile exists and is final.", true);
                    this.m_ExecutionStatus.ShowMessage = true;
                }
            }
        }

        public void Execute(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology cytologyPanelOrderToUnfinal, YellowstonePathology.Business.Test.AccessionOrder cytologyAccessionOrder, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
		{
			this.m_CytologyAccessionOrder = cytologyAccessionOrder;
			this.m_PanelOrderToUnfinal = cytologyPanelOrderToUnfinal;
			this.m_PanelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_CytologyAccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelOrderToUnfinal.ReportNo);
			this.m_ExecutionStatus = executionStatus;
			this.m_Rule.Execute(executionStatus);
		}
	}
}
