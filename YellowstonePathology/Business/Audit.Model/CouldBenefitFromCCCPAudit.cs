using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class CouldBenefitFromCCCPAudit : AccessionOrderAudit
    {
        public CouldBenefitFromCCCPAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder) 
            : base(accessionOrder)
        {

        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
			YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest comprehensiveColonCancerProfileTest = new Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest();
            YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest lynchSyndromeEvaluationTest = new Test.LynchSyndrome.LynchSyndromeEvaluationTest();

            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(lynchSyndromeEvaluationTest.PanelSetId) == true)
            {
                YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(lynchSyndromeEvaluationTest.PanelSetId);
                if (panelSetOrderLynchSyndromeEvaluation.LynchSyndromeEvaluationType == YellowstonePathology.Business.Test.LynchSyndrome.LSEType.COLON)
                {
                    if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(comprehensiveColonCancerProfileTest.PanelSetId) == false)
                    {
                        this.m_Status = AuditStatusEnum.Warning;
                        this.m_Message.AppendLine("This case may benefit from a Comprehensive Colon Cancer Profile. Click YES if you would like to stop and order one or click NO to continue without ordering one.");
                    }
                }
            }                           
        }        
    }
}
