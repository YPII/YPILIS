using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ReflexTesting
{
    public class ReflexTestingOkToOrderResult
    {
        private bool m_OkToOrder;
        private string m_Message;

        public ReflexTestingOkToOrderResult()
        {
            
        }

        public bool OkToOrder
        {
            get { return this.m_OkToOrder; }
            set { this.m_OkToOrder = value; }
        }

        public string Message
        {
            get { return this.m_Message; }
            set { this.m_Message = value; }
        }
    }
}
