using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test
{
    public class TestResult
    {
        public static string Detected = "Detected";
        public static string NotDetected = "Not Detected";
        public static string NotApplicable = "Not Applicable";

        protected string m_ResultCode;
        protected string m_Result;

        public TestResult()
        {

        }

        public string ResultCode
        {
            get { return this.m_ResultCode; }
        }

        public string Result
        {
            get { return this.m_Result; }
        }
	}
}
