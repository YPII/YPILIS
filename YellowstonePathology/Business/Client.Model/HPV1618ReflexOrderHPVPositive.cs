using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPV1618ReflexOrderHPVPositive : ReflexOrder
    {
        public HPV1618ReflexOrderHPVPositive()
        {
            this.m_ReflexOrderCode = "RFLXHPV1618HPVPOS";
            this.m_Description = "Order HPV 16/18 when the HPV result is positive";
			this.m_PanelSet = new YellowstonePathology.Business.Test.HPV1618.HPV1618Test();
        }

        public override bool IsRequired(Business.Test.AccessionOrder accessionOrder)
        {
            bool result = false;
			YellowstonePathology.Business.Test.HPV.HPVTest panelSetHPV = new YellowstonePathology.Business.Test.HPV.HPVTest();
		   YellowstonePathology.Business.Test.HPV1618.HPV1618Test panelSetHPV1618 = new YellowstonePathology.Business.Test.HPV1618.HPV1618Test();            

            if (accessionOrder.PanelSetOrderCollection.Exists(panelSetHPV.PanelSetId) == true)
            {
                YellowstonePathology.Business.Test.HPV.HPVTestOrder hpvTestOrder = (YellowstonePathology.Business.Test.HPV.HPVTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetHPV.PanelSetId);
                if (hpvTestOrder.ResultCode == YellowstonePathology.Business.Test.HPV.HPVResult.OveralResultCodePositive)
                {
                    result = true;                    
                }               
            }            
            return result;
        }
    }
}
