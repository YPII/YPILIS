using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Prothrombin
{
	public class ProthrombinHomozygousMutationDetectedResult : ProthrombinResult
	{
		public ProthrombinHomozygousMutationDetectedResult()
		{
			this.m_Result = "Homozygous Mutation Detected";
			this.m_ResultCode = "PRTHBNHMZGS";
			this.m_Interpretation = "Hypercoagulability typically results from a combination of acquired and heritable risk factors.  Among the many well " +
				"documented heritable risk factors, mutation of the prothrombin gene is the second most common, occurring in up to 1% of the general population " +
				"and in up to 5% of patients who suffer thromboembolic events.  The mutation, which is characterized by a G>A substitution at position 20210 of the " +
				"prothrombin gene, results in increased plasma prothrombin levels and increased clotting activity.  Studies have demonstrated that individuals who " +
				"harbor the Prothrombin 20210G>A mutant allele are approximately 3 times more likely to experience a venous thromboembolic event and up to 4 times " +
				"more likely to suffer from premature myocardial infarction when compared to the general population.  Knowledge of a patient's Prothrombin 20210G>A " +
				"mutation status is valuable, as it may influence treatment regimens as well as allow for timely pre-operative and genetic counseling.  Molecular " +
				"testing of the patient's specimen reveals that this individual harbors a homozygous Prothrombin 20210G>A mutation.  Individuals homozygous for the " +
				"Prothrombin 20210G>A mutation are an increased risk for venous thrombosis and pulmonary embolism when compared to individuals who do not have this " +
				"mutation.  The risk is higher than the risk associated with heterozygous mutation, but is not well defined.  Genetic counseling is recommended for " +
				"individuals who test positive for Prothrombin 20210G>A mutation.";
			this.m_ResultDescription = "(see interpretation)";
		}
	}
}
