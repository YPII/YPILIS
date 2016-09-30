using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.Cytology
{   
	public class SetResult 
	{
        YellowstonePathology.Business.Rules.Rule m_Rule;
        YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;

        YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology m_PanelOrder;
        string m_ResultCode;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

		public SetResult()
        {
            this.m_Rule = new YellowstonePathology.Business.Rules.Rule();

            this.m_Rule.ActionList.Add(IsItOkToSetTheResult);
            this.m_Rule.ActionList.Add(SetSpecimenAdequacy);            
            this.m_Rule.ActionList.Add(SetScreeningImpression);
            this.m_Rule.ActionList.Add(SetResultCode);     
        }

        private void IsItOkToSetTheResult()
        {
            if (this.m_PanelOrder.Accepted == true)
            {
                this.m_Rule.ExecutionStatus.AddMessage("Unable to set results because screening is final.", true);
            }
        }

        private void SetSpecimenAdequacy()
        {
			string adequacy = this.m_ResultCode.ToString().Substring(1, 2);
			YellowstonePathology.Business.Cytology.Model.SpecimenAdequacy specimenAdequacy = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSpecimenAdequacyByResultCode(adequacy);

			List<YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyComment> selectedSpecimenAdequacyComments = new List<YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyComment>();
            YellowstonePathology.Business.Rules.Cytology.SetSpecimenAdequacy setSpecimenAdequacy = new SetSpecimenAdequacy();            
            setSpecimenAdequacy.Execute(specimenAdequacy, selectedSpecimenAdequacyComments, this.m_PanelOrder, this.m_AccessionOrder, this.m_ExecutionStatus);
        }        

        private void SetScreeningImpression()
        {
			string impression = this.m_ResultCode.ToString().Substring(3, 2);
			YellowstonePathology.Business.Cytology.Model.ScreeningImpression screeningImpression = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetScreeningImpressionByResultCode(impression);
			this.m_PanelOrder.ScreeningImpression = screeningImpression.Description;
        }

        private void SetResultCode()
        {
            this.m_PanelOrder.ResultCode = this.m_ResultCode;
        }

        public void Execute(string resultCode, YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
        {
            this.m_ResultCode = resultCode;
            this.m_PanelOrder = panelOrder;
			this.m_AccessionOrder = accessionOrder;
            this.m_ExecutionStatus = executionStatus;                   
            this.m_Rule.Execute(this.m_ExecutionStatus);
        }        
	}    
}
