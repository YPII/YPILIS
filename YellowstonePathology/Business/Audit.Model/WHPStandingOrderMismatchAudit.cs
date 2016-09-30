using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class WHPStandingOrderMismatchAudit : AccessionOrderAudit
    {
        public WHPStandingOrderMismatchAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
            : base(accessionOrder)
        {
            
        }

        public override void Run()
        {
			YellowstonePathology.Business.Domain.Physician physician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(this.m_AccessionOrder.PhysicianId);

			YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(womensHealthProfileTest.PanelSetId) == true)
            {
                YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(womensHealthProfileTest.PanelSetId);
				if (physician.HPVStandingOrderCode != womensHealthProfileTestOrder.HPVStandingOrderCode)
                {
					this.m_Message.AppendLine("The physician standing order for HPV does not match the Womens Health Profile standing order for HPV.");
                    this.m_ActionRequired = true;
                }
				if (physician.HPV1618StandingOrderCode != womensHealthProfileTestOrder.HPV1618StandingOrderCode)
                {
					this.m_Message.AppendLine("The physician standing order for HPV 16/18 does not match the Womens Health Profile standing order for HPV 16/18.");
                    this.m_ActionRequired = true;
                }
            }
        }       
    }
}
