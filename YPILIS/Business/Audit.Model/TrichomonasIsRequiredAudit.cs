using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class TrichomonasIsRequiredAudit : AccessionOrderAudit
    {
        public TrichomonasIsRequiredAudit(Test.AccessionOrder accessionOrder) 
            :base(accessionOrder)
        {

        }

        public override void Run()
        {			
			YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
			if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(womensHealthProfileTest.PanelSetId) == true)
            {
                YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(womensHealthProfileTest.PanelSetId);
				if (womensHealthProfileTestOrder.OrderTrichomonas == true)
                {
					YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest panelSet = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest();
                    if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSet.PanelSetId) == false)
                    {
                        this.m_ActionRequired = true;
                        this.m_Message.AppendLine("A " + panelSet.PanelSetName + " is required but not ordered.");
                    }
                }
            }
        }
    }
}
