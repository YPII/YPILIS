using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class WHPStandingOrderNotSetAudit : AccessionOrderAudit
    {
        public WHPStandingOrderNotSetAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder) 
            :base(accessionOrder)
        {
            
        }

        public override void Run()
        {            
			YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
			if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(womensHealthProfileTest.PanelSetId) == true)
            {
                YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(womensHealthProfileTest.PanelSetId);
                YellowstonePathology.Business.Client.Model.StandingOrderNotSet standingOrderNotSet = new YellowstonePathology.Business.Client.Model.StandingOrderNotSet();
				if (womensHealthProfileTestOrder.HPVStandingOrderCode == standingOrderNotSet.StandingOrderCode)
                {
					this.m_Message.AppendLine("Womens Health Profile HPV Standing Order Code is Not Set.");
                    this.m_ActionRequired = true;
                }
				if (womensHealthProfileTestOrder.HPV1618StandingOrderCode == standingOrderNotSet.StandingOrderCode)
                {
					this.m_Message.AppendLine("Womens Health Profile HPV 16/18 Standing Order Code is Not Set.");
                    this.m_ActionRequired = true;
                }
            }
        }       
    }
}
