using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CysticFibrosis
{
	public class CysticFibrosisResult : YellowstonePathology.Business.Test.TestResult
	{
		public static string Method = "DNA was extracted from the patient's specimen.  Multiplex PCR was performed to amplify the target genes.  Enzymatic digestion " +
			"was performed to generate single stranded DNA.  Sample DNA was hybridized to capture probes with signal detection by alternating current voltammetry.";
		private const string MutationsTestedString = "DeltaF508, DeltaI507, G542X, G551D, W1282X, N1303K, R553X, 621+1G>T, R117H, 1717-1G>A, A455E, R560T, R1162X, G85E, R334W, " +
			"R347P, 711+1G>T, 1898+1G>A, 2184delA, 3849+10kbC>T, 2789+5G>A, 3659delC, 3120+1G>A.";
		public static string Reference = "Obstectrics and Gynecology (2005) 106:1465";
        
		protected string m_Interpretation;
		protected string m_MutationsTested;
		protected string m_MutationsDetected;

		public CysticFibrosisResult()
		{
			this.m_MutationsTested = CysticFibrosisResult.MutationsTestedString;
		}        

		public virtual void SetResults(CysticFibrosisTestOrder testOrder, CysticFibrosisEthnicGroup cysticFibrosisEthnicGroup)
		{
			testOrder.Result = this.m_Result;
			testOrder.ResultCode = this.m_ResultCode;			
			testOrder.Method = CysticFibrosisResult.Method;
			testOrder.MutationsTested = m_MutationsTested;
			testOrder.MutationsDetected = m_MutationsDetected;
			testOrder.ReportReferences = CysticFibrosisResult.Reference;
            testOrder.TemplateId = cysticFibrosisEthnicGroup.TemplateId;
		}		
	}
}
