using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class TallmanMedicalRecordAudit : AccessionOrderAudit
    {
        public TallmanMedicalRecordAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
            : base(accessionOrder)
        {

        }

        public override void Run()
        {            
            if (this.m_AccessionOrder.ClientId == 579)
            {
                if (string.IsNullOrEmpty(this.m_AccessionOrder.SvhMedicalRecord) == true)
                {
                    this.m_ActionRequired = true;
                    this.m_Message.Append("The Medical Record Number must be set for Tallman cases.");
                }
            }            
        }        
    }
}
