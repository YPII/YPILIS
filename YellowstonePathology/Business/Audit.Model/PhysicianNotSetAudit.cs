using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class PhysicianNotSetAudit : AccessionOrderAudit
    {
        public PhysicianNotSetAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
            : base(accessionOrder)
        {

        }

        public override void Run()
        {            
            if (this.m_AccessionOrder.PhysicianId == 0)
            {
                this.m_ActionRequired = true;
                this.m_Message.Append("The Physician Id has not been set.");
            }
            if (string.IsNullOrEmpty(this.m_AccessionOrder.PhysicianName) == true)
            {
                this.m_ActionRequired = true;
                this.m_Message.Append("The Physician Name has not been set.");
            }
        }        
    }
}
