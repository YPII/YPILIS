using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MPNFish
{
	public class MPNFishResult
	{
		public static string NucleiScored = "200";

		protected string m_Result;
		protected string m_Interpretation;
		protected string m_ProbeSetDetail;

		public MPNFishResult()
		{
		}

		public void SetResults(PanelSetOrderMPNFish panelSetOrder)
		{
			panelSetOrder.Result = this.m_Result;
			panelSetOrder.Interpretation = this.m_Interpretation;
			panelSetOrder.ProbeSetDetail = this.m_ProbeSetDetail;
			panelSetOrder.NucleiScored = MPNFishResult.NucleiScored;
		}
	}
}
