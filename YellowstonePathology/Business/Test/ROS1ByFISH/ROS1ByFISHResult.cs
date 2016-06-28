using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.Business.Test.ROS1ByFISH
{
	public class ROS1ByFISHResult
	{
        protected string m_ResultDisplayText;
        protected string m_ReferenceRange;
        protected string m_References;
        protected string m_ResultCode;
		protected string m_Result;        
        protected string m_ResultAbbreviation;
		protected string m_Interpretation;
        protected string m_Method;
		protected string m_ProbeSetDetail;        
        protected string m_ReportDisclaimer;

        public ROS1ByFISHResult()
		{
            this.m_ReferenceRange =  "The sample is considered positive if >50% of the first 50 cells scored are positive, and considered negative if <10% cells are positive. If 10- " +
                "50% of cells are positive, an additional 50 cells are evaluated by a second technologist. The sample is then considered positive if > or = 15% " +
                "of all 100 cells scored are positive.";

            this.m_References = "";

            this.m_Method = "Interphase FISH analysis was performed using a ROS1 Break Apart FISH Probe.";

            this.m_ReportDisclaimer = "NeoGenomics Laboratories FISH test uses either FDA cleared and/or analyte specific reagent (ASR) probes. This test was developed and its performance characteristics determined by NeoGenomics Laboratories in Irvine CA. It has not been " +
                "cleared or approved by the U.S. Food and Drug Administration (FDA). The FDA has determined that such clearance or approval is not necessary. This test is used for clinical purposes and should not be regarded as investigational or for " +
                "research. This laboratory is regulated under CLIA '88 as qualified to perform high complexity testing.Interphase FISH does not include examination of the entire chromosomal complement. Clinically significant anomalies detectable by routine " +
                "banded cytogenetic analysis may still be present. Consider reflex banded cytogenetic analysis.";
		}

        public string ResultCode
        {
            get { return this.m_ResultCode; }

        }

		public string Result
		{
			get { return this.m_Result; }
		}

        public string ResultDisplayText
        {
            get { return this.m_ResultDisplayText; }
        }

        public string ResultAbbreviation
        {
            get { return this.m_ResultAbbreviation; }
        }

		public string Interpretation
		{
			get { return this.m_Interpretation; }
		}

        public string Method
        {
            get { return this.m_Method; }
        }

		public string ProbeSetDetail
		{
			get { return this.m_ProbeSetDetail; }
		}

		public string ReferenceRange
		{
			get { return this.m_ReferenceRange; }
		}

		public string References
		{
			get { return this.m_References; }
		}        

        public void Clear(YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder testOrder)
		{
            testOrder.ResultCode = null;
			testOrder.Result = null;            
			testOrder.Interpretation = null;
			testOrder.ReferenceRange = null;
            testOrder.Method = null;
			testOrder.References = null;
			testOrder.ProbeSetDetail = null;
            testOrder.ReportDisclaimer = null;
		}

        public virtual void SetResult(YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder testOrder)
		{
            testOrder.ResultCode = this.m_ResultCode;
			testOrder.Result = this.m_Result;
			testOrder.Interpretation = this.m_Interpretation;
            testOrder.Method = this.m_Method;
			testOrder.ReferenceRange = this.m_ReferenceRange;
			testOrder.References = this.m_References;
			testOrder.ProbeSetDetail = this.m_ProbeSetDetail;
            testOrder.ReportDisclaimer = testOrder.GetLocationPerformedComment() + Environment.NewLine + Environment.NewLine + this.m_ReportDisclaimer;
		}
	}
}
