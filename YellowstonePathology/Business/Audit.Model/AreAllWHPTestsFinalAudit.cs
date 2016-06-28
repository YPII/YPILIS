using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class AreAllWHPTestsFinalAudit : AccessionOrderAudit
    {
        public AreAllWHPTestsFinalAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
            :base(accessionOrder)
        {

        }

        public override void Run()
        {
            YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest thinPrepPapTest = new Test.ThinPrepPap.ThinPrepPapTest();
			YellowstonePathology.Business.Test.HPV.HPVTest panelSetHPV = new Test.HPV.HPVTest();
            YellowstonePathology.Business.Test.HPV1618.HPV1618Test hpv1618Test = new Test.HPV1618.HPV1618Test();
            YellowstonePathology.Business.Test.NGCT.NGCTTest ngctTest = new Test.NGCT.NGCTTest();

            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {				
                if (panelSetOrder.PanelSetId == thinPrepPapTest.PanelSetId || panelSetOrder.PanelSetId == panelSetHPV.PanelSetId 
                    || panelSetOrder.PanelSetId == hpv1618Test.PanelSetId || panelSetOrder.PanelSetId == ngctTest.PanelSetId)
                {
                    if (panelSetOrder.Final == false)
                    {
                        this.m_ActionRequired = true;
                        this.m_Message.AppendLine("One or more tests are not final");
                    }
                }                
            }            
        }
    }
}
