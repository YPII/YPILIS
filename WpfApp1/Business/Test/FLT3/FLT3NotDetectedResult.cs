using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.FLT3
{
	public class FLT3NotDetectedResult : FLT3Result
	{
		public FLT3NotDetectedResult()
		{
			this.m_Result = "Not Detected";
			this.m_ITDMutation = "Not Detected";
			this.m_ITDPercentage = "0.0%";
			this.m_TKDMutation = "Not Detected";
			this.m_Interpretation = "FLT3 internal tandem duplications (ITD) occur in approximately 25% of acute myeloid leukemia (AML) patients; while the FLT3 tyrosine " +
				"kinase domain (TKD) mutations have the overall incidence around 5 to 10% in AML patients. FLT3 mutations are frequently associated with " +
				"leukocytosis. The presence of FLT3 mutations in patient with AML implies aggressive disease. Testing for FLT3 and NPM1 in AML patients " +
				"with intermediate-risk cytogenetic abnormalities can help classify patients as favorable, intermediate-I or intermediate-II, depending on the " +
				"specific combination of findings.";
		}
	}
}
