using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVReflexOrderRule5 : ReflexOrder
    {
        public HPVReflexOrderRule5()
        {
            this.m_RuleNumber = 5;
            this.m_ReflexOrderCode = "RFLXHPVRL5";
            this.m_Description = "Perform reflex HPV testing on patients who are greater than 20 years old, have a PAP result of ASCUS, AGUS, LSIL or HSIL and have not had an HPV in the last year.";
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
                    if (accessionOrder.PBirthdate < DateTime.Today.AddYears(-20))
                    {
						if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsDiagnosisAscusAgusLsilHsil(panelSetOrderCytology.ResultCode) == true)
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
