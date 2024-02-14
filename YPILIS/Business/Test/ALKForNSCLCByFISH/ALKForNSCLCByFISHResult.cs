using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.Business.Test.ALKForNSCLCByFISH
{
	public class ALKForNSCLCByFISHResult
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
        protected string m_FusionsComment;

        public ALKForNSCLCByFISHResult()
		{
            this.m_ReferenceRange =  "The sample is considered positive if >50% of the first 50 cells scored are positive, and considered negative if " +
			"<10% cells are positive.  If 10-50% of cells are positive, an additional 50 cells are evaluated by a second technologist.  The sample is then " +
			"considered positive if > or = 15% of all 100 cells scored are positive.";

            this.m_References = "1) Perner S, et al. EML4-ALK fusion lung cancer. Neoplasia. 2008;10(3):298-302. \r\n" +
			"2) Salido M, et al. Increased ALK gene copy number and amplification are frequent in non-small cell lung cancer. J Thor Oncol. 2011;6(1):21-27.";

            this.m_Method =  "Interphase FISH analysis was performed using the Vysis ALK Break Apart FISH Probe Kit according to the FDA-approved methodology " +
                "(Vysis, Inc., Des Plaines, IL).";            
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

        public void Clear(YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder testOrder)
		{
            testOrder.ResultCode = null;
			testOrder.Result = null;            
			testOrder.Interpretation = null;
			testOrder.ReferenceRange = null;
            testOrder.Method = null;
			testOrder.ReportReferences = null;
			testOrder.ProbeSetDetail = null;
            testOrder.ReportDisclaimer = null;
		}

        public virtual void SetResult(YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder testOrder)
		{
            testOrder.ResultCode = this.m_ResultCode;
			testOrder.Result = this.m_Result;
			testOrder.Interpretation = this.m_Interpretation;
            testOrder.Method = this.m_Method;
			testOrder.ReferenceRange = this.m_ReferenceRange;
			testOrder.ReportReferences = this.m_References;
			testOrder.ProbeSetDetail = this.m_ProbeSetDetail;
            testOrder.ReportDisclaimer = testOrder.GetLocationPerformedComment();
		}
	}
}
