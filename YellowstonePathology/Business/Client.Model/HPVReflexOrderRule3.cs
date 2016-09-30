using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVReflexOrderRule3 : ReflexOrder
    {
        public HPVReflexOrderRule3()
        {
            this.m_RuleNumber = 3;
            this.m_ReflexOrderCode = "RFLXHPVRL3";
            this.m_Description = "Perform reflex HPV testing on patients greater than 20 years old with a PAP result of ASCUS, AGUS, LSIL or HSIL and have not had an HPV within the past year.";
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
					if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsDiagnosisAscusAgusLsilHsil(panelSetOrderCytology.ResultCode) == true)
                    {						
                        if (accessionOrder.PBirthdate < DateTime.Today.AddYears(-20))
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
