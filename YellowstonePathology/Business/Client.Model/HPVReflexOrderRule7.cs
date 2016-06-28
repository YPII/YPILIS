using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVReflexOrderRule7 : ReflexOrder
    {
        public HPVReflexOrderRule7()
        {
            this.m_RuleNumber = 7;
            this.m_ReflexOrderCode = "RFLXHPVRL7";
            this.m_Description = "Perform reflex HPV testing on patients who are greater than 20 years old, have a PAP result of ASCUS or AGUS and have not had an HPV in the last year.";
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
						if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsDiagnosisAscusAgus(panelSetOrderCytology.ResultCode) == true)
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
            }
            return result;
        }             
    }
}
