using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class ClientNotSetAudit : AccessionOrderAudit
    {
        public ClientNotSetAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder) 
            : base(accessionOrder)
        {

        }

        public override void Run()
        {
            if (this.m_AccessionOrder.ClientId == 0)
            {
                this.m_ActionRequired = true;
                this.m_Message.Append("The Client Id has not been set.");
            }
            if (string.IsNullOrEmpty(this.m_AccessionOrder.ClientName) == true)
            {
                this.m_ActionRequired = true;
                this.m_Message.Append("The Client Name has not been set.");
            }
        }        
    }
}
