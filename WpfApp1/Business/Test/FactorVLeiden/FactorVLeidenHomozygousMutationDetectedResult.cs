using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.FactorVLeiden
{
	public class FactorVLeidenHomozygousMutationDetectedResult : FactorVLeidenResult
	{
		public FactorVLeidenHomozygousMutationDetectedResult()
		{
			this.m_Result = "Homozygous Mutation Detected";
			this.m_ResultCode = "FCTVLDNHMZGSDTCTD";
			this.m_ResultDescription = "(see interpretation)";
			this.m_Interpretation = "Hypercoagulability typically results from a combination of acquired and heritable risk factors.  Among the many " +
				"well documented heritable risk factors, mutation of the Factor V gene is the most common, occurring in up to 3% of the general " +
				"population and in up to 50% of patients with recurrent thromboembolic events.  The mutation (commonly referred to as Factor V Leiden), " +
				"which is characterized by a substitution of glutamine for arginine at position 506 (R506Q), alters a site on the factor V protein that " +
				"is normally cleaved by activated protein C, thereby causing impaired clot degradation.  Studies have demonstrated that individuals who " +
				"harbor heterozygous Factor V Leiden mutations are 5 to 10 times more likely to experience a thromboembolic event when compared to the " +
				"general population, while homozygous Factor V Leiden carriers are at significantly higher risk (50 to 80-fold).  Knowledge of a patient's " +
				"Factor V Leiden mutation status is valuable, as it may influence treatment regimens as well as allow for timely pre-operative and genetic " +
				"counseling.  Molecular testing of the patient's specimen reveals that this individual harbors a homozygous Factor V Leiden mutation.  " +
				"Individuals with homozygous Factor V Leiden (R506Q) mutation are at a 50 to 80 fold increased risk for venous thrombosis and pulmonary " +
				"embolism compared with individuals who do not have this mutation.  Genetic counseling is recommended for individuals who test positive for " +
				"a Factor V Leiden mutation.";
		}
	}
}
