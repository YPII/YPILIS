using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.JAK2V617F
{
	public class JAK2V617FResult : YellowstonePathology.Business.Test.TestResult
	{
		public static string Method = "Highly purified DNA was extracted from the specimen using an automated method.  The extracted DNA was amplified " +
			"using real-time PCR with 2 hydrolysis probes.  One probe targeted the mutated sequence c.1849G>T (V617F) and one targeted the wild-type sequence " +
			"of the target in exon 14 of the JAK2 gene.  The real-time PCR curves were analyzed to determine the presence of the mutated JAK2 gene. The assay " +
			"has a sensitivity of 1% for detecting the V617F mutation in a background of wild type DNA.";
		public static string References = "1.  Tefferi A, Vardiman JW.  Classification and diagnosis of myeloproliferative neoplasms: The 2008 World Health Organization criteria and point-of-care diagnostic algorithms.  Leukemia (2008) 22, 14–22\r\n" +
			"2.  Levine RL, Gilliland DG.  Myeloproliferative disorders.  Blood. 2008 112: 2190-2198\r\n" +
			"3.  Kralovics R, et al.  A Gain-of-Function Mutation of JAK2 in Myeloproliferative Disorders.  N Engl J Med 2005;352:1779-90";

		protected string m_Interpretation;
		protected string m_Comment;

		public JAK2V617FResult()
		{

		}

        public void SetResults(JAK2V617FTestOrder panelSetOrderJAK2V617F)
        {
            panelSetOrderJAK2V617F.Interpretation = this.m_Interpretation;            
            panelSetOrderJAK2V617F.Comment = this.m_Comment;
            panelSetOrderJAK2V617F.Reference = References;
            panelSetOrderJAK2V617F.Method = Method;            
        }
	}
}
