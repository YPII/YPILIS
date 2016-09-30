using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVReflexOrderRule13 : ReflexOrder
    {
        public HPVReflexOrderRule13()
        {
            this.m_RuleNumber = 13;
            this.m_ReflexOrderCode = "RFLXHPVRL13";
            this.m_Description = "Perform reflex HPV testing on patients who are greater than 30, have a PAP result of Normal or Reactive and the endocervical component is absent.";
			this.m_PanelSet = new YellowstonePathology.Business.Test.HPV.HPVTest();
        }

        public override bool IsRequired(Business.Test.AccessionOrder accessionOrder)
        {
            bool result = false;
			YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest panelSetThinPrep = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(panelSetThinPrep.PanelSetId) == true)
            {
				YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetThinPrep.PanelSetId);
                if (panelSetOrderCytology.Final == true)
                {
					if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsResultCodeNormal(panelSetOrderCytology.ResultCode) == true ||
						YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsResultCodeReactive(panelSetOrderCytology.ResultCode) == true)
                    {
						if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsResultCodeTZoneAbsent(panelSetOrderCytology.ResultCode) == true)
                        {
                            if (accessionOrder.PBirthdate < DateTime.Today.AddYears(-30))
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
