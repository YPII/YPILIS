using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class AccessionOrderAudit : Audit
    {
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public AccessionOrderAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
        }        

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }
        
        public override string ToString()
        {
            return "Master AccessionNo: " + this.m_AccessionOrder.MasterAccessionNo + ": " + this.m_Message;
        }
    }
}
