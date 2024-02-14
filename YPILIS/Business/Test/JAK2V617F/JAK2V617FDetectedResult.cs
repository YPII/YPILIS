using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.JAK2V617F
{
	public class JAK2V617FDetectedResult : JAK2V617FResult
	{		
		public JAK2V617FDetectedResult()
		{
            this.m_ResultCode = "JAK2V617FDTCTD";
            this.m_Result = "Detected";
			this.m_Interpretation = "JAK2 V617F is an activating mutation in a tyrosine kinase that leads to clonal proliferation.  V617F is present " +
			"in greater than 95% polycythemia vera, and in ~50% of cases of essential thrombocythemia and primary myelofibrosis.  The mutation has not " +
			"been detected in normal patients.  Thus, detection of the V617F mutation provides strong evidence of a chronic myeloproliferative " +
			"disorder.   The molecular analysis is positive for the V617F mutation within the JAK2 gene. These findings strongly support the diagnosis " +
			"of a chronic myeloproliferative disorder.";
			this.m_Comment = "Result strongly supports the diagnosis of a chronic myeloproliferative disorder.";
		}
	}
}
