using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class CanWomensHealthProfileBeFinaledAudit : AccessionOrderAudit
    {
        public CanWomensHealthProfileBeFinaledAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
            :base(accessionOrder)
        {

        }

        public override void Run()
        {
            List<int> whpTestList = new List<int>();
            whpTestList.Add(15);
            whpTestList.Add(14);
            whpTestList.Add(61);
            whpTestList.Add(3);
            whpTestList.Add(62);

            YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
			if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(womensHealthProfileTest.PanelSetId) == true)
            {
                YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(womensHealthProfileTest.PanelSetId);
				if (womensHealthProfileTestOrder.Final == false)
                {                                        
                    if (this.m_AccessionOrder.PanelSetOrderCollection.HasUnfinaledTests(whpTestList) == true)
                    {
                        this.m_ActionRequired = true;
						this.m_Message.Append("The Womens Health Profile cannot be finalized as there are tests that are pending.");
                    }
                }                
            }            
        }
    }
}
