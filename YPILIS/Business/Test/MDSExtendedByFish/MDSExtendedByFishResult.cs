using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MDSExtendedByFish
{
	public class MDSExtendedByFishResult
	{
		public static string NucleiScored = "200 - 266";

		protected string m_Result;
		protected string m_Interpretation;
		protected string m_ProbeSetDetail;

		public MDSExtendedByFishResult()
		{
		}

		public void SetResults(PanelSetOrderMDSExtendedByFish panelSetOrder)
		{
			panelSetOrder.Result = this.m_Result;
			panelSetOrder.Interpretation = this.m_Interpretation;
			panelSetOrder.ProbeSetDetail = this.m_ProbeSetDetail;
			panelSetOrder.NucleiScored = MDSExtendedByFishResult.NucleiScored;
		}
	}
}
