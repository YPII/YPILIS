using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.FactorVLeiden
{
	public class FactorVLeidenIndeterminateResult : FactorVLeidenResult
	{
		public FactorVLeidenIndeterminateResult()
		{
			this.m_Result = "Indeterminate";
			this.m_ResultCode = "FCTVLDNNDTRMNMT";
			this.m_ResultDescription = "(see comment).";
			this.m_Interpretation = "Hypercoagulability typically results from a combination of acquired and heritable risk factors.  Among the many " +
				"well documented heritable risk factors, mutation of the Factor V gene is the most common, occurring in up to 3% of the general population " +
				"and in up to 50% of patients with recurrent thromboembolic events.  The mutation (commonly referred to as Factor V Leiden), which is " +
				"characterized by a substitution of glutamine for arginine at position 506 (R506Q), alters a site on the factor V protein that is normally " +
				"cleaved by activated protein C, thereby causing impaired clot degradation.  Studies have demonstrated that individuals who harbor " +
				"heterozygous Factor V Leiden mutations are 5 to 10 times more likely to experience a thromboembolic event when compared to the general " +
				"population, while homozygous Factor V Leiden carriers are at significantly higher risk (50 to 80-fold).  Knowledge of a patient's Factor V " +
				"Leiden mutation status is valuable, as it may influence treatment regimens as well as allow for timely pre-operative and genetic " +
				"counseling.  Molecular analysis of the patient's specimen for Factor V Leiden (R506Q) mutation yielded indeterminate results (see comment " +
				"for details).  Submission of a new sample is recommended.";
		}
	}
}
