using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVReflexOrderRule20 : ReflexOrder
    {
        public HPVReflexOrderRule20()
        {
            this.m_RuleNumber = 20;
            this.m_ReflexOrderCode = "RFLXHPVRL20";
            this.m_Description = "Perform reflex HPV testing on patients who are beween the ages of 21 and 24 and have a PAP result of ASCUS.";
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
                    if (accessionOrder.PBirthdate > DateTime.Today.AddYears(-25) && accessionOrder.PBirthdate <= DateTime.Today.AddYears(-21))
                    {
                        if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsDiagnosisASCUS(panelSetOrderCytology.ResultCode) == true)
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
