using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Prothrombin
{
	public class ProthrombinResult : YellowstonePathology.Business.Test.TestResult
	{
		protected string m_ResultDescription;
		protected string m_Interpretation;
        protected string m_Method = "This test was perforemed by an FDA approved Molecular PCR Assay.";
		protected string m_References = "1.  Poort SR et al. A common genetic variation in the 3'-untranslated region of the prothrombin gene is associated with " +
			"elevated plasma prothrombin levels and an increase in venous thrombosis. Blood 1996 (88): 3698-3703." + Environment.NewLine +
			"2.  Cumming AM et al. The prothrombin gene G20210A variant: prevalence in a U.K. anticoagulant clinic population. British J. Haematology 1997 (98): 353-355.";
		protected string m_TestDevelopment = "Test developed and characteristics determined by ARUP Laboratories. See Compliance Statement C: aruplab.com/CS";
		
		public ProthrombinResult()
		{

		}

		public void SetResults(ProthrombinTestOrder testOrder)
		{
			testOrder.ResultDescription = this.m_ResultDescription;
			testOrder.Interpretation = this.m_Interpretation;
			testOrder.Method = this.m_Method;
			testOrder.ReportReferences = this.m_References;
			testOrder.TestDevelopment = this.m_TestDevelopment;
		}
	}
}
