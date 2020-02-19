using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Audit.Model
{
    public class ASCCPAudit : AccessionOrderAudit
    {
        private Business.ASCCPRule.Woman m_Woman;
        private bool m_IsOkToRunAudit;

        public ASCCPAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
            : base(accessionOrder)
        {

        }

        public override void Run()
        {
            
        }

        public bool IsOkToRunAudit
        {
            get { return this.m_IsOkToRunAudit; }
        }

        public Business.ASCCPRule.Woman Woman
        {
            get { return this.m_Woman; }
        }
    }
}
