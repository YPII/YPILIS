using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASExon23Mutation
{
    public class KRASExon23MutationResult
    {
        protected const string m_TestDevelopment = "The performance characteristics of this test have been determined by NeoGenomics Laboratories. This test has " +
            "not been approved by the FDA. The FDA has determined such clearance or approval is not necessary. This laboratory is CLIA certified to perform high " +
            "complexity clinical testing.";
        protected const string m_DetectedResultCode = "KRSXN23MTTNDTCTD";
        protected const string m_NotDetectedResultCode = "KRSXN23MTTNNTDTCTD";

        public KRASExon23MutationResult()
        {

        }

        public void SetDetectedResults(KRASExon23MutationTestOrder testOrder)
        {
            testOrder.Result = "Detected";
            testOrder.ResultCode = KRASExon23MutationResult.m_DetectedResultCode;

            StringBuilder disclaimer = new StringBuilder();
            disclaimer.AppendLine(testOrder.GetLocationPerformedComment());
            disclaimer.Append(m_TestDevelopment);
            testOrder.ReportDisclaimer = disclaimer.ToString();
        }

        public void SetNotDetectedResults(KRASExon23MutationTestOrder testOrder)
        {
            testOrder.Result = "Not Detected";
            testOrder.ResultCode = KRASExon23MutationResult.m_NotDetectedResultCode;

            StringBuilder disclaimer = new StringBuilder();
            disclaimer.AppendLine(testOrder.GetLocationPerformedComment());
            disclaimer.Append(m_TestDevelopment);
            testOrder.ReportDisclaimer = disclaimer.ToString();
        }
    }
}
