using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.Cytology
{
	public class ClearCase
	{
		YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology m_PanelSetOrderCytology;
		YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology m_PanelOrderCytology;

        YellowstonePathology.Business.Rules.Rule m_Rule;
        YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;

		public ClearCase()
        {
            this.m_Rule = new YellowstonePathology.Business.Rules.Rule();
            
            this.m_Rule.ActionList.Add(ClearPanelSetOrder);
            this.m_Rule.ActionList.Add(ClearPanelOrder);
        }

        public void ClearPanelSetOrder()
        {            
            if (this.m_PanelSetOrderCytology.FinaledById == this.m_PanelOrderCytology.AcceptedById ||
				this.m_PanelSetOrderCytology.FinaledById == 0)
            {
				this.m_PanelSetOrderCytology.SpecimenAdequacy =null;
				this.m_PanelSetOrderCytology.ScreeningImpression = null;
				this.m_PanelSetOrderCytology.ReportComment = null;
				this.m_PanelSetOrderCytology.OtherConditions = null;
				this.m_PanelSetOrderCytology.ScreenedById = 0;
				this.m_PanelSetOrderCytology.ScreenedByName = null;
				this.m_PanelSetOrderCytology.Signature = null;
				this.m_PanelSetOrderCytology.ResultCode = "59999";
			    this.m_PanelSetOrderCytology.Final = false;
				this.m_PanelSetOrderCytology.FinaledById = 0;
				this.m_PanelSetOrderCytology.FinalDate = null;
				this.m_PanelSetOrderCytology.FinalTime = null;
            }            
        }

        public void ClearPanelOrder()
        {
			if (this.m_PanelSetOrderCytology.Final == false)
            {                
                this.m_PanelOrderCytology.Accepted = false;
                this.m_PanelOrderCytology.AcceptedById = 0;
                this.m_PanelOrderCytology.AcceptedDate = null;
                this.m_PanelOrderCytology.AcceptedTime = null;

                this.m_PanelOrderCytology.ScreenedById = 0;
                this.m_PanelOrderCytology.ScreenedByName = null;
                this.m_PanelOrderCytology.SpecimenAdequacy = null;
                this.m_PanelOrderCytology.ScreeningImpression = null;

                this.m_PanelOrderCytology.ResultCode = "59999";
                this.m_PanelOrderCytology.ReportComment = null;
                this.m_PanelOrderCytology.OtherConditions = null;
            }
		}

		public void Execute(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrderCytology, YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
		{
            this.m_PanelSetOrderCytology = panelSetOrderCytology;
			this.m_PanelOrderCytology = panelOrderCytology;            
			this.m_ExecutionStatus = executionStatus;
			this.m_Rule.Execute(executionStatus);
		}
	}
}
