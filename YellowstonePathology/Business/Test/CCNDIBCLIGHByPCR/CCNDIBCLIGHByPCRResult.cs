using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.CCNDIBCLIGHByPCR
{
    public class CCNDIBCLIGHByPCRResult
    {
        public static string NucleiScored = "200";
        protected string m_Result;
        protected string m_ResultCode;
        protected string m_Interpretation;
        protected string m_Method;
        protected string m_References;

        public CCNDIBCLIGHByPCRResult()
        {
        }

        public void SetResults(CCNDIBCLIGHByPCRTestOrder testOrder)
        {
            testOrder.Result = this.m_Result;
            testOrder.ResultCode = this.m_ResultCode;
            testOrder.Interpretation = this.m_Interpretation;
            testOrder.Method = this.m_Method;
            testOrder.References = this.m_References;
        }
    }
}
