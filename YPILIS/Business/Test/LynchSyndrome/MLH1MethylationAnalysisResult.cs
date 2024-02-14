using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class MLH1MethylationAnalysisResult
	{        
		public static string Method = "This assay use bisulfite treated DNA followed by methylation specific real-time PCR.  Each patient's bisulfite treated DNA " +
			"sample will have 2 reaction wells using methylated and unmethylated MMX. Standard curve is used to quantify methylated and unmethylated DNA. The % of " +
			"methylation for each sample is calculated according to the formula:\r\n\r\n% of Methylation = methylated DNA pg / (methylated pg + unmethylated pg) *100%";

		public static string References = "Marcus Bettstetter et al, Distinction of Hereditary Nonpolyposis Colorectal Cancer and Sporadic Microsatellite-Unstable " +
			"Colorectal Cancer through Quantification of MLH1 Methylation by realtime PCR. Clinical Cancer Research 2007;13(11), p3221";

		protected string m_Result;
        protected string m_ResultCode;
		protected string m_Interpretation;

        public MLH1MethylationAnalysisResult()
		{
		}

		public string Result
		{
			get { return this.m_Result; }
		}

        public string ResultCode
        {
            get { return this.m_ResultCode; }
        }

		public string Interpretation
		{
			get { return this.m_Interpretation; }
		}

		public void SetResults(PanelSetOrderMLH1MethylationAnalysis panelSetOrderMLH1MethylationAnalysis)
		{
			panelSetOrderMLH1MethylationAnalysis.Result = this.m_Result;
            panelSetOrderMLH1MethylationAnalysis.ResultCode = this.m_ResultCode;
			panelSetOrderMLH1MethylationAnalysis.Interpretation = Interpretation;
			panelSetOrderMLH1MethylationAnalysis.Method = Method;
			panelSetOrderMLH1MethylationAnalysis.ReportReferences = References;
		}
	}
}
