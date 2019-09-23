using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVReflexOrderRule18 : ReflexOrder
    {
        public HPVReflexOrderRule18()
        {
            this.m_RuleNumber = 18;
            this.m_ReflexOrderCode = "RFLXHPVRL18";
            this.m_Description = "Perform reflex HPV testing on patients who have a PAP result of ASCUS."; //no positive HPV result in the last year
            this.m_PanelSet = new YellowstonePathology.Business.Test.HPV.HPVTest();
        }

        public override bool IsRequired(Business.Test.AccessionOrder accessionOrder)
        {
            bool result = this.MeetsBaseRequirements(accessionOrder);
            if(result == true)
            {
                result = this.HasNoPositiveHPVInLastYear(accessionOrder);
            }
            return result;
        }

        public override bool MeetsBaseRequirements(Business.Test.AccessionOrder accessionOrder)
        {
            bool result = false;
            YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest panelSetThinPrep = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(panelSetThinPrep.PanelSetId) == true)
            {
                YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetThinPrep.PanelSetId);
                if (panelSetOrderCytology.Final == true)
                {
                    if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsDiagnosisASCUS(panelSetOrderCytology.ResultCode) == true)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public override bool HasNoPositiveHPVInLastYear(Business.Test.AccessionOrder accessionOrder)
        {
            bool result = true;
            YellowstonePathology.Business.Domain.PatientHistory patientHistory = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPatientHistory(accessionOrder.PatientId);
            Nullable<DateTime> dateOfLastHPV = patientHistory.GetDateOfPreviousHpv(accessionOrder.AccessionDate.Value);

            if (dateOfLastHPV.HasValue == true)
            {
                if (dateOfLastHPV >= DateTime.Today.AddDays(-330))
                {
                    List<string> priorResults = patientHistory.GetPriorHPVResult(accessionOrder.MasterAccessionNo, DateTime.Today.AddDays(-330));
                    foreach (string hpvResult in priorResults)
                    {
                        if (hpvResult == YellowstonePathology.Business.Test.HPV.HPVResult.OveralResultCodePositive)
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }
            return result;
        }
    }
}
