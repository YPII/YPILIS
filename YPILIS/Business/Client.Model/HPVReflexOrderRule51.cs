using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVReflexOrderRule51 : ReflexOrder
    {
        public HPVReflexOrderRule51()
        {
            this.m_RuleNumber = 51;
            this.m_ReflexOrderCode = "RFLXHPVRL51";
            this.m_Description = "Perform reflex HPV testing on patients who are between 21 and 29 years old and have an abnormal PAP result.";
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
                    if (accessionOrder.PBirthdate >= DateTime.Today.AddYears(-29) && accessionOrder.PBirthdate <= DateTime.Today.AddYears(-21))
                    {
						if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsDiagnosisGreaterThanThree(panelSetOrderCytology.ResultCode) == true)
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
