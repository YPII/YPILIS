using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NPM1
{
	public class NPM1DetectedResult : NPM1Result
	{
		public NPM1DetectedResult()
		{
			this.m_Result = "Detected";
			this.m_PercentageNPM1Mutation = "38.3%";
			this.m_Interpretation = "Mutation at exon 12 of nucleophosmin 1 (NPM1) gene is a predictor of favorable prognosis in AML with intermediate-risk and associated with " +
				"good response to induction chemotherapy; if there is no mutation in FLT3.  Mutations in the NPM1 gene also are frequently associated with " +
				"normal karyotype in adult and pediatric acute myeloid leukemia (AML).  The ratio of the mutant peak to total can be used to monitor disease " +
				"and response to therapy.";
		}
	}
}
