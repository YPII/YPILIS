using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVRulePreviousTesting2 : HPVRule
    {
        public HPVRulePreviousTesting2()
        {
            this.m_Description = "No HPV testing within the past year";
        }

        public override bool SatisfiesCondition(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            bool result = false;
            YellowstonePathology.Business.Domain.PatientHistory patientHistory = Business.Gateway.AccessionOrderGateway.GetPatientHistory(accessionOrder.PatientId);
            Nullable<DateTime> dateOfLastHPV = patientHistory.GetDateOfPreviousHpv(accessionOrder.AccessionDate.Value);

            if (dateOfLastHPV.HasValue == true)
            {
                if (dateOfLastHPV < accessionOrder.AccessionDate.Value.AddDays(-330))
                {
                    result = true;
                }
            }
            else
            {
                result = true;
            }
            return result;
        }
    }
}
