using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class WHPIsRequiredByStandingOrderAudit : AccessionOrderAudit
    {
        public WHPIsRequiredByStandingOrderAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
            : base(accessionOrder)
        {
            
        }

        public override void Run()
        {
			YellowstonePathology.Business.Domain.Physician physician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(this.m_AccessionOrder.PhysicianId);

			YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(womensHealthProfileTest.PanelSetId) == false)
            {
                YellowstonePathology.Business.Client.Model.StandingOrderNone standingOrderNone = new YellowstonePathology.Business.Client.Model.StandingOrderNone();
                if (physician.HPVStandingOrderCode != standingOrderNone.StandingOrderCode)
                {
					this.m_Message.AppendLine("The physician has a standing order for HPV and a Womens Health Profile does not exist.");
                    this.m_ActionRequired = true;
                }
                if (physician.HPV1618StandingOrderCode != standingOrderNone.StandingOrderCode)
                {
					this.m_Message.AppendLine("The physician has a standing order for HPV 16/18 and a Womens Health Profile does not exist.");
                    this.m_ActionRequired = true;
                }
            }
        }       
    }
}
