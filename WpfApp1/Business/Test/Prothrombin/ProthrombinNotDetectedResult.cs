using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Prothrombin
{
	public class ProthrombinNotDetectedResult : ProthrombinResult
	{
		public ProthrombinNotDetectedResult()
		{
			this.m_Result = "Mutation Not Detected ";
			this.m_ResultCode = "PRTHBNNTDTCTD";
			this.m_Interpretation = "Hypercoagulability typically results from a combination of acquired and heritable risk factors.  Among the many well documented " +
				"heritable risk factors, mutation of the prothrombin gene is the second most common, occurring in up to 1% of the general population and in up to 5% " +
				"of patients who suffer thromboembolic events.  The mutation, which is characterized by a G>A substitution at position 20210 of the prothrombin gene, " +
				"results in increased plasma prothrombin levels and increased clotting activity.  Studies have demonstrated that individuals who harbor the Prothrombin " +
				"20210G>A mutant allele are approximately 3 times more likely to experience a venous thromboembolic event and up to 4 times more likely to suffer from " +
				"premature myocardial infarction when compared to the general population.  Knowledge of a patient's Prothrombin 20210G>A mutation status is valuable, " +
				"as it may influence treatment regimens as well as allow for timely pre-operative and genetic counseling.  Molecular testing of the patient's specimen " +
				"reveals that this individual DOES NOT harbor the Prothrombin 20210G>A mutation.   Although the Prothrombin 20210G>A mutation is absent, the individual " +
				"may have other genetic and environmental risk factors for thrombosis. Consider additional testing for hemostatic disorders associated with increased " +
				"thrombosis risk, if indicated.";
			this.m_ResultDescription = "(see interpretation)";
		}
	}
}
