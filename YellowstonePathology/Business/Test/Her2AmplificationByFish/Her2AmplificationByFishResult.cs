using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Her2AmplificationByFish
{
	public class Her2AmplificationByFishResult
	{
		public static string ReferenceRanges = "Based on 2013 CAP/ASCO guidelines, a case is considered POSITIVE when the HER2 to CEN17 ratio is >/=2.0 with any average " +
			"HER2 copy number or when the HER2 to CEN17 ratio is <2.0 with an average HER2 copy number >/=6.0 signals/nucleus, " +
			"EQUIVOCAL when the HER2 to CEN17 ratio is <2.0 with an average HER2 copy number >/=4.0 and <6.0 signals/ nucleus, and " +
			"NEGATIVE when the HER2 to CEN17 ratio is <2.0 with an average HER2 copy number < 4.0 signals/ nucleus.";
		public static string NucleiScored = "40";

		protected string m_Result;
		protected string m_Interpretation;
		protected string m_Comment;
		protected string m_Reference;

		public Her2AmplificationByFishResult()
		{
		}

		public void SetResults(PanelSetOrderHer2AmplificationByFish panelSetOrderHer2AmplificationByFish)
		{
			panelSetOrderHer2AmplificationByFish.Result = this.m_Result;
			if (string.IsNullOrEmpty(panelSetOrderHer2AmplificationByFish.AverageHER2SignalsPerNucleus) == false) this.m_Interpretation = this.m_Interpretation.Replace("$AVGHER2SIGNALS$", panelSetOrderHer2AmplificationByFish.AverageHER2SignalsPerNucleus);
			if (string.IsNullOrEmpty(panelSetOrderHer2AmplificationByFish.AverageCEN17SignalsPerNucleus) == false) this.m_Interpretation = this.m_Interpretation.Replace("$AVGCEN17SIGNALS$", panelSetOrderHer2AmplificationByFish.AverageCEN17SignalsPerNucleus);
			if (string.IsNullOrEmpty(panelSetOrderHer2AmplificationByFish.HER2CEN17SignalRatio) == false) this.m_Interpretation = this.m_Interpretation.Replace("$HER2CEN17SIGNALRATIO$", panelSetOrderHer2AmplificationByFish.HER2CEN17SignalRatio);
			panelSetOrderHer2AmplificationByFish.Interpretation = this.m_Interpretation;
			panelSetOrderHer2AmplificationByFish.Comment = this.m_Comment;
			panelSetOrderHer2AmplificationByFish.Reference = this.m_Reference;
			panelSetOrderHer2AmplificationByFish.ReferenceRanges = Her2AmplificationByFishResult.ReferenceRanges;
			panelSetOrderHer2AmplificationByFish.NucleiScored = Her2AmplificationByFishResult.NucleiScored;
		}
	}
}
