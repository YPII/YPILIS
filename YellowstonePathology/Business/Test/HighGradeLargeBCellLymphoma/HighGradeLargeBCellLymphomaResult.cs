using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HighGradeLargeBCellLymphoma
{
	public class HighGradeLargeBCellLymphomaResult
	{
		public static string NucleiScored = "200";
		protected string m_Result;
		protected string m_Interpretation;
		protected string m_ProbeSetDetail;

		public HighGradeLargeBCellLymphomaResult()
		{
		}

		public string Result
		{
			get { return this.m_Result; }
		}

		public string Interpretation
		{
			get { return this.m_Interpretation; }
		}

		public string ProbeSetDetail
		{
			get { return this.m_ProbeSetDetail; }
		}

		public void SetResults(PanelSetOrderHighGradeLargeBCellLymphoma panelSetOrderHighGradeLargeBCellLymphoma)
		{
			panelSetOrderHighGradeLargeBCellLymphoma.Result = this.m_Result;
			panelSetOrderHighGradeLargeBCellLymphoma.Interpretation = this.m_Interpretation;
			panelSetOrderHighGradeLargeBCellLymphoma.ProbeSetDetail = this.m_ProbeSetDetail;
			panelSetOrderHighGradeLargeBCellLymphoma.NucleiScored = HighGradeLargeBCellLymphomaResult.NucleiScored;
		}
	}
}
