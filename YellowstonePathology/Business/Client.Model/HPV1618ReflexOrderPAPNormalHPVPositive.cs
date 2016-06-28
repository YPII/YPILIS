using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPV1618ReflexOrderPAPNormalHPVPositive : ReflexOrder
    {
        public HPV1618ReflexOrderPAPNormalHPVPositive()
        {
            this.m_ReflexOrderCode = "RFLXHPV1618PAPNRMLHPVPOS";
            this.m_Description = "Order HPV 16/18 when the PAP result is normal and the HPV result is positive";
			this.m_PanelSet = new YellowstonePathology.Business.Test.HPV1618.HPV1618Test();
        }

        public override bool IsRequired(Business.Test.AccessionOrder accessionOrder)
        {
            bool result = false;

			YellowstonePathology.Business.Test.HPV.HPVTest panelSetHPV = new YellowstonePathology.Business.Test.HPV.HPVTest();
		   YellowstonePathology.Business.Test.HPV1618.HPV1618Test panelSetHPV1618 = new YellowstonePathology.Business.Test.HPV1618.HPV1618Test();
			YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest panelSetThinPrepPap = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest();            

            if(accessionOrder.PanelSetOrderCollection.Exists(panelSetThinPrepPap.PanelSetId) == true)
            {
				YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelsetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetThinPrepPap.PanelSetId);
                if (panelsetOrderCytology.Final == true)
                {
                    string papResultCode = panelsetOrderCytology.ResultCode;
					if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsResultCodeNormal(papResultCode) == true || YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsResultCodeReactive(papResultCode) == true)
                    {
                        if (accessionOrder.PanelSetOrderCollection.Exists(panelSetHPV.PanelSetId) == true)
                        {
                            YellowstonePathology.Business.Test.HPV.HPVTestOrder hpvTestOrder = (YellowstonePathology.Business.Test.HPV.HPVTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetHPV.PanelSetId);
                            if (hpvTestOrder.ResultCode == YellowstonePathology.Business.Test.HPV.HPVResult.OveralResultCodePositive)
                            {
                                result = true;                             
                            }                            
                        }                        
                    }
                }
            }

            return result;
        }       
    }
}
