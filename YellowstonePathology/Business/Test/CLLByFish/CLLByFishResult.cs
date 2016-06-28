using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CLLByFish
{
	public class CLLByFishResult
	{
		protected string m_Result;
		protected string m_Interpretation;
		protected string m_ProbeSetDetail;
		protected string m_References;

		public CLLByFishResult()
		{
		}

		public void SetResults(CLLByFishTestOrder testOrder)
		{
			testOrder.Result = this.m_Result;
			testOrder.Interpretation = this.m_Interpretation;
			testOrder.ProbeSetDetail = this.m_ProbeSetDetail;
			testOrder.References = this.m_References;
		}
	}
}
