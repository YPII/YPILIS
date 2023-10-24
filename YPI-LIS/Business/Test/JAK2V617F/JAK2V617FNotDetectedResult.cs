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
			this.m_Interpretation = "The JAK2 V617F mutation has been reported in >80% of the patients with polycythemia vera (PV), 30-50% of " +
                "patients with either essential thrombocythemia(ET) or primary myelofibrosis(PMF).  This mutation is not detected in normal " +
                "individuals.  A small subset of patients with myeloproliferative neoplasms (MPN)that are negative for the JAK2 V617F mutation " +
                "will harbor JAK2 mutations in exon 12.  More rare mutations are detected in exons 13 and 14.  JAK2 mutations can be used to " +
                "differentiate reactive conditions from neoplastic process.";
			this.m_Comment = "See Interpretation for additional information.";
		}
	}
}
