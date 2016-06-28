using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test
{
    public class TestResultNoResult : TestResult
    {
        public TestResultNoResult()
        {
            this.m_ResultCode = null;
            this.m_Result = null;
        }
    }
}
