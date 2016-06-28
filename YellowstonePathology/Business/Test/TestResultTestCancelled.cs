using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test
{
    public class TestResultTestCancelled : TestResult
    {
        public TestResultTestCancelled()
        {
            this.m_ResultCode = "JAK2V617FTSTCNCLLD";
            this.m_Result = "Test Cancelled";
        }
    }
}
