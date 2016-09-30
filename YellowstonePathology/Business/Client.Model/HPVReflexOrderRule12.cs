using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVReflexOrderRule12 : ReflexOrder
    {
        public HPVReflexOrderRule12()
        {
            this.m_RuleNumber = 12;
            this.m_ReflexOrderCode = "RFLXHPVRL12";
            this.m_Description = "Perform reflex HPV testing on patients who are 25 years and older and have a PAP result of ASCUS or LSIL.";
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
					if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsDiagnosisAscusLsil(panelSetOrderCytology.ResultCode) == true)
                    {
                        if (accessionOrder.PBirthdate < DateTime.Today.AddYears(-25))
                        {
                            result = true;
                        }                        
                    }
                }
            }
            return result;
        }             
    }
}
