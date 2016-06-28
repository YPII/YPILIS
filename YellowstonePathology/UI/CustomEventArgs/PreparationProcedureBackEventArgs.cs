using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class PreparationProcedureBackEventArgs : System.EventArgs
    {
        private YellowstonePathology.Business.Test.TestOrderInfo m_TestOrderInfo;
        private Type m_CallingPage;

        public PreparationProcedureBackEventArgs(YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo, Type callingPage)
        {
            this.m_TestOrderInfo = testOrderInfo;
            this.m_CallingPage = callingPage;
        }

        public YellowstonePathology.Business.Test.TestOrderInfo ReportOrderInfo
        {
            get { return this.m_TestOrderInfo; }
            set { this.m_TestOrderInfo = value; }
        }

        public Type CallingPage
        {
            get { return this.m_CallingPage; }
            set { this.m_CallingPage = value; }
        }
    }
}
