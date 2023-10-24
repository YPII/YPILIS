using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.AMLStandardByFish
{
	public class AMLStandardByFishResult
	{
		public static string NucleiScored = "200 - 284";

		protected string m_Result;
		protected string m_Interpretation;
		protected string m_ProbeSetDetail;

		public AMLStandardByFishResult()
		{
		}

		public void SetResults(AMLStandardByFishTestOrder testOrder)
		{
			testOrder.Result = this.m_Result;
			testOrder.Interpretation = this.m_Interpretation;
			testOrder.ProbeSetDetail = this.m_ProbeSetDetail;
			testOrder.NucleiScored = AMLStandardByFishResult.NucleiScored;
		}
	}
}
