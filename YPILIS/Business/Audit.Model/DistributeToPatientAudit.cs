using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class DistributeToPatientAudit : AccessionOrderAudit
    {
        public DistributeToPatientAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder) 
            : base(accessionOrder)
        {

        }

        public override void Run()
        {
            if (this.m_AccessionOrder.DistributeToPatient == true)
            {
                if(this.m_AccessionOrder.PatientDistributionType == "Text" && string.IsNullOrEmpty(this.m_AccessionOrder.PPhoneNumberHome) == true)
                {
                    this.m_ActionRequired = true;
                    this.m_Message.Append("The patient distribution is set to Text and the phone number is blank, please enter a valid phone number.");
                }

                if (this.m_AccessionOrder.PatientDistributionType == "Email" && string.IsNullOrEmpty(this.m_AccessionOrder.PEmailAddress) == true)
                {
                    this.m_ActionRequired = true;
                    this.m_Message.Append("The patient distribution is set to Email and the email address is blank, please enter a valid email address.");
                }

                if (this.m_AccessionOrder.PatientDistributionType == "Text and Email")
                {
                    if (string.IsNullOrEmpty(this.m_AccessionOrder.PPhoneNumberHome) == true || string.IsNullOrEmpty(this.m_AccessionOrder.PEmailAddress) == true)
                    {
                        this.m_ActionRequired = true;
                        this.m_Message.Append("The patient distribution is set to Text and Email and the email/phone number is blank, please enter a valid phone number and email address.");
                    }                    
                }
            }            
        }        
    }
}
