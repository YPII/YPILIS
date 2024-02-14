using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.Cytology
{
	public class DotReviewFinal
	{
        YellowstonePathology.Business.Rules.Rule m_Rule;
		
        YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;

        YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology m_PanelOrder;                
		YellowstonePathology.Business.User.SystemUser m_UserPerformingFinal;

		public DotReviewFinal()
        {
            this.m_Rule = new YellowstonePathology.Business.Rules.Rule();

            this.m_Rule.ActionList.Add(IsItOkToFinalDotReview);
            this.m_Rule.ActionList.Add(FinalDotReview);                        
        }

        private void IsItOkToFinalDotReview()
        {
            if (this.m_PanelOrder.Accepted == true)
            {
                this.m_Rule.ExecutionStatus.AddMessage("Unable to final Dot Review because it's already final.", true);
            }
        }

        public void FinalDotReview()
        {
            this.m_PanelOrder.ResultCode = "55000";
            this.m_PanelOrder.ScreeningImpression = "No Result";
            this.m_PanelOrder.SpecimenAdequacy = "No Result";
            this.m_PanelOrder.NoCharge = true;
            this.m_PanelOrder.Accepted = true;
            this.m_PanelOrder.AcceptedDate = DateTime.Today;
            this.m_PanelOrder.AcceptedTime = DateTime.Now;
            this.m_PanelOrder.AcceptedById = this.m_UserPerformingFinal.UserId;
            this.m_PanelOrder.AssignedToId = this.m_UserPerformingFinal.UserId;
            this.m_PanelOrder.ScreenedById = this.m_UserPerformingFinal.UserId;
            this.m_PanelOrder.ScreenedByName = this.m_UserPerformingFinal.DisplayName;
        }

        public void Execute(YellowstonePathology.Business.User.SystemUser userPerformingFinal, YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrder, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
		{
            this.m_UserPerformingFinal = userPerformingFinal;
            this.m_PanelOrder = panelOrder;			
            this.m_ExecutionStatus = executionStatus;                   
            this.m_Rule.Execute(this.m_ExecutionStatus);
        }        
	}
}
