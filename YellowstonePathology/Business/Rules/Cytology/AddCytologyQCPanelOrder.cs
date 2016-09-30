using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.Cytology
{
	public class AddCytologyQCPanelOrder
	{        
        YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology m_PanelOrderToFinal;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology m_PanelSetOrderCytology;

        YellowstonePathology.Business.Rules.Rule m_Rule;
        YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;

        public AddCytologyQCPanelOrder()
        {
            this.m_Rule = new YellowstonePathology.Business.Rules.Rule();

            this.m_Rule.ActionList.Add(IsModulusNotZero);
            this.m_Rule.ActionList.Add(DoesPathologistReviewExist);
            this.m_Rule.ActionList.Add(DoesCytotechReviewExist);
            this.m_Rule.ActionList.Add(AddQCPanel);

            this.m_SystemIdentity = YellowstonePathology.Business.User.SystemIdentity.Instance;
		}

        private void IsModulusNotZero()
        {
			int reportNoInt;
			int modValue = 12;

			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelOrderToFinal.ReportNo);
			if (orderIdParser.IsLegacyReportNo == true)
			{
				reportNoInt = orderIdParser.ReportNoNumber.Value;
			}
			else
			{
				reportNoInt = orderIdParser.MasterAccessionNoNumber.Value;
				modValue = 11;
			}

			if (reportNoInt % modValue != 0)
			{
				this.m_ExecutionStatus.AddMessage("Not Qualified", true);
			}
        }

        private void DoesPathologistReviewExist()
        {
            bool pathologistReviewExists = false;
			foreach (YellowstonePathology.Business.Interface.IPanelOrder panelOrder in this.m_PanelSetOrderCytology.PanelOrderCollection)
            {
                Type objectType = panelOrder.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                {
                    YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology cytologyPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)panelOrder;
                    if (cytologyPanelOrder.ScreeningType.ToUpper() == "PATHOLOGIST REVIEW")
                    {
                        pathologistReviewExists = true;
                        break;
                    }
                }
            }
            if (pathologistReviewExists == true)
            {
                this.m_ExecutionStatus.AddMessage("Not Qualified", true);
            }
        }

        private void DoesCytotechReviewExist()
        {
            bool cytotechReviewExists = false;
			foreach (YellowstonePathology.Business.Interface.IPanelOrder panelOrder in this.m_PanelSetOrderCytology.PanelOrderCollection)
            {
                Type objectType = panelOrder.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                {
                    YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology cytologyPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)panelOrder;
                    if (cytologyPanelOrder.ScreeningType.ToUpper() == "CYTOTECH REVIEW" || cytologyPanelOrder.ScreeningType.ToUpper() == "PEER REVIEW")
                    {
                        cytotechReviewExists = true;
                        break;
                    }
                }
            }            
            if (cytotechReviewExists == true)
            {                
                this.m_ExecutionStatus.AddMessage("Not Qualified", true);
            }            
        }

        private void AddQCPanel()
        {
            YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapQCPanel thinPrepPapQCPanel = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapQCPanel();
            string panelOrderId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrder = new YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology(this.m_PanelSetOrderCytology.ReportNo, panelOrderId, panelOrderId, thinPrepPapQCPanel, this.m_SystemIdentity.User.UserId);
            panelOrder.FromExistingPanelOrder(this.m_PanelOrderToFinal, thinPrepPapQCPanel.ScreeningType, true, m_SystemIdentity.User.UserId);
			this.m_PanelSetOrderCytology.PanelOrderCollection.Add(panelOrder);			
		}

        public void Execute(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrderToFinal, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
		{
            this.m_ExecutionStatus = executionStatus;
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelOrderToFinal = panelOrderToFinal;
			this.m_PanelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelOrderToFinal.ReportNo);
			this.m_Rule.Execute(executionStatus);
        }
	}
}
