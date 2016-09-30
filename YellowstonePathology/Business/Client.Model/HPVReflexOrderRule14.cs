using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVReflexOrderRule14 : ReflexOrder
    {
        public HPVReflexOrderRule14()
        {
            this.m_RuleNumber = 14;
            this.m_ReflexOrderCode = "RFLXHPVRL14";
            this.m_Description = "Perform reflex HPV testing on patients who are reported with ASCUS or higher results and have not had an HPV in the last year.";
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
					if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsDiagnosisThreeOrBetter(panelSetOrderCytology.ResultCode) == true)
                    {
                        YellowstonePathology.Business.Domain.PatientHistory patientHistory = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPatientHistory(accessionOrder.PatientId);
                        Nullable<DateTime> dateOfLastHPV = patientHistory.GetDateOfPreviousHpv(accessionOrder.AccessionDate.Value);

                        if (dateOfLastHPV.HasValue == true)
                        {
                            if (dateOfLastHPV < DateTime.Today.AddDays(-330))
                            {
                                result = true;
                            }
                        }
                        else
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
