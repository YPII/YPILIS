using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.JAK2V617F
{
	public class JAK2V617FNotDetectedResult : JAK2V617FResult
	{		
		public JAK2V617FNotDetectedResult()
		{
            this.m_ResultCode = "JAK2V617FNTDTCTD";
            this.m_Result = "Not Detected";
			this.m_Interpretation = "JAK2 V617F is an activating mutation in a tyrosine kinase that leads to clonal proliferation.  V617F is present " +
			"in greater than 95% polycythemia vera (PV), and in ~50% of cases of essential thrombocythemia (ET) and primary myelofibrosis (MF).  The " +
			"mutation has not been detected in normal patients.  Thus, detection of the V617F mutation provides strong evidence of a chronic " +
			"myeloproliferative disorder.   The molecular analysis did not detect the V617F mutation within the JAK2 gene.  While these findings do not " +
			"support the diagnosis of a chronic myeloproliferative disorder, they also do not rule it out.  If PV is suspected consider testing JAK2 " +
			"Exon 12 – 14 (present in 5% of cases of PV).  If ET or MF is suspected consider testing MPL (present in 5% of MF and 5% of ET).";
			this.m_Comment = "See Interpretation for additional information.";
		}
	}
}
