using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MDSByFish
{
	public class MDSByFishResult
	{
		public static string NucleiScored = "200";

		protected string m_Result;
		protected string m_Interpretation;
		protected string m_ProbeSetDetail;

		public MDSByFishResult()
		{
		}

		public void SetResults(PanelSetOrderMDSByFish panelSetOrder)
		{
			panelSetOrder.Result = this.m_Result;
			panelSetOrder.Interpretation = this.m_Interpretation;
			panelSetOrder.ProbeSetDetail = this.m_ProbeSetDetail;
			panelSetOrder.NucleiScored = MDSByFishResult.NucleiScored;
		}
	}
}
