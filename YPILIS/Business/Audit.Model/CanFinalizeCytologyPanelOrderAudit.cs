using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class CanFinalizeCytologyPanelOrderAudit : Audit
    {
        private YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology m_PanelOrderToFinal;
        private YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology m_PanelSetOrderCytology;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;

        public CanFinalizeCytologyPanelOrderAudit(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrderToFinal,
            YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity,
            YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
        {
            this.m_PanelOrderToFinal = panelOrderToFinal;
            this.m_PanelSetOrderCytology = panelSetOrderCytology;
            this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = systemIdentity;
            this.m_ExecutionStatus = executionStatus;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();

            switch (this.m_PanelOrderToFinal.ScreeningType.ToUpper())
            {
                case "DOT REVIEW":
                    YellowstonePathology.Business.Rules.Cytology.DotReviewFinal dotReviewFinal = new YellowstonePathology.Business.Rules.Cytology.DotReviewFinal();
                    dotReviewFinal.Execute(this.m_SystemIdentity.User, this.m_PanelOrderToFinal, this.m_ExecutionStatus);
                    break;
                case "PATHOLOGIST REVIEW":
                    this.m_PanelSetOrderCytology.UpdateFromScreening(this.m_PanelOrderToFinal);
                    YellowstonePathology.Business.Rules.Cytology.ScreeningFinal screeningFinal1 = new YellowstonePathology.Business.Rules.Cytology.ScreeningFinal(YellowstonePathology.Business.ProcessingModeEnum.Production);
                    screeningFinal1.Execute(this.m_SystemIdentity.User, this.m_AccessionOrder, this.m_PanelOrderToFinal, this.m_ExecutionStatus);
                    break;
                default:
                    this.m_PanelSetOrderCytology.UpdateFromScreening(this.m_PanelOrderToFinal);
                    YellowstonePathology.Business.Rules.Cytology.ScreeningFinal screeningFinal2 = new YellowstonePathology.Business.Rules.Cytology.ScreeningFinal(YellowstonePathology.Business.ProcessingModeEnum.Production);
                    screeningFinal2.Execute(this.m_SystemIdentity.User, this.m_AccessionOrder, this.m_PanelOrderToFinal, this.m_ExecutionStatus);
                    break;
            }

            if(this.m_ExecutionStatus.Halted == true)
            {
                this.m_Status = AuditStatusEnum.Failure;
                if(this.m_ExecutionStatus.ShowMessage == true)
                {
                    this.m_Message.Append(this.m_ExecutionStatus.ExecutionMessagesString);
                }
            }
        }
    }
}
