using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class ComprehensiveColonCancerProfileFinalAudit : Audit
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public ComprehensiveColonCancerProfileFinalAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();

            YellowstonePathology.Business.Test.Surgical.SurgicalTest surgicalTest = new Test.Surgical.SurgicalTest();
            YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTest lynchSyndromeIHCPanelTest = new Test.LynchSyndrome.LynchSyndromeIHCPanelTest();
            YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest lynchSyndromeEvaluationTest = new Test.LynchSyndrome.LynchSyndromeEvaluationTest();
            YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest comprehensiveColonCancerProfileTest = new Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest();

            if (this.m_AccessionOrder.PanelSetOrderCollection.Count >= 4 &&
                this.m_AccessionOrder.PanelSetOrderCollection.Exists(surgicalTest.PanelSetId) &&
                this.m_AccessionOrder.PanelSetOrderCollection.Exists(lynchSyndromeIHCPanelTest.PanelSetId) &&
                this.m_AccessionOrder.PanelSetOrderCollection.Exists(lynchSyndromeEvaluationTest.PanelSetId) &&
                this.m_AccessionOrder.PanelSetOrderCollection.Exists(comprehensiveColonCancerProfileTest.PanelSetId) == false)
            {
                this.m_Status = AuditStatusEnum.Failure;
                this.m_Message.Append("This Comprehensive Colon Cancer Profile Test is not necessary. Notify IT to remove it.");
            }
        }
    }
}
