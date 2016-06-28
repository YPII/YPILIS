using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Rules.Cytology
{
	public class AddScreeningReview
	{
        YellowstonePathology.Business.Rules.Rule m_Rule;
        YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;

        string m_ScreeningType;
		YellowstonePathology.Business.Test.AccessionOrder m_CytologyAccessionOrder;
		YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology m_InitiatingPanelOrder;
		YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology m_PanelSetOrderCytology;

        ProcessingModeEnum m_ProcessingMode;

        public AddScreeningReview()
        {
            this.m_Rule = new YellowstonePathology.Business.Rules.Rule();
            this.m_Rule.ActionList.Add(AddPanelOrder);
            this.m_Rule.ActionList.Add(SetResultsFromOrderingPanelOrder);
        }

        private void AddPanelOrder()
        {
            YellowstonePathology.Business.User.SystemIdentity systemIdentity = YellowstonePathology.Business.User.SystemIdentity.Instance;
            YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapScreeningPanel thinPrepPapScreeningPanel = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapScreeningPanel();
            thinPrepPapScreeningPanel.ScreeningType = this.m_ScreeningType;

            string panelOrderId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrderCytology = new YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology(this.m_PanelSetOrderCytology.ReportNo, panelOrderId, panelOrderId, thinPrepPapScreeningPanel, systemIdentity.User.UserId);
            panelOrderCytology.FromExistingPanelOrder(this.m_InitiatingPanelOrder, this.m_ScreeningType, false, systemIdentity.User.UserId);
            this.m_PanelSetOrderCytology.PanelOrderCollection.Add(panelOrderCytology);
        }

        private void SetResultsFromOrderingPanelOrder()
        {

        }

        public void Execute(string screeningType, YellowstonePathology.Business.Test.AccessionOrder cytologyAccessionOrder,
            YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology initiatingPanelOrder, ProcessingModeEnum processingMode,
            YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
        {
            this.m_ScreeningType = screeningType;
            this.m_CytologyAccessionOrder = cytologyAccessionOrder;
            this.m_InitiatingPanelOrder = initiatingPanelOrder;
			this.m_PanelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)cytologyAccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(initiatingPanelOrder.ReportNo);
            this.m_ProcessingMode = processingMode;

            this.m_ExecutionStatus = executionStatus;
            this.m_Rule.Execute(this.m_ExecutionStatus);
        }
	}
}
