using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
	public class HER2AmplificationByISHGastricAdenocarcinomaResult : HER2AmplificationByISHResult
	{
		public HER2AmplificationByISHGastricAdenocarcinomaResult()
		{
			this.m_InterpretiveComment = "Human epidermal growth factor receptor 2 gene (HER2) is amplified in up to 25% of gastric cancers, particularly " +
				"those that arise at the gastroesophageal junction.  Amplification of the HER2 gene in gastric tumors is associated with a worse prognosis " +
				"and is also predictive of response to chemotherapeutic agents.  Dual in situ hybridization (ISH) studies for HER2 amplification were performed " +
				"on the submitted sample, in accordance with ASCO/CAP guidelines." + Environment.NewLine + Environment.NewLine + 
				"For this patient, the HER2:Chr17 ratio was *RATIO*. A total of *CELLSCOUNTED* nuclei were examined. Therefore, HER2 status is *HER2STATUS*.";
			this.m_ReportReference = "Gravalos, C. et al., HER2 in gastric cancer: a new prognostic factor and a novel therapeutic target. Ann Oncology 2008, 19:1523-1529.";
		}

		public override void SetResults(HER2AmplificationByISHTestOrder testOrder)
		{
			this.m_ResultComment = null;
			this.m_ResultDescription = "Ratio = " + testOrder.Her2Chr17Ratio;

			this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*RATIO*", testOrder.Her2Chr17Ratio.Value.ToString());
			this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*CELLSCOUNTED*", testOrder.CellsCounted.ToString());
			if (testOrder.AverageHer2NeuSignal.HasValue == true) this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*HER2COPY*", testOrder.AverageHer2NeuSignal.Value.ToString());

			Nullable<double> her2Chr17Ratio = testOrder.AverageHer2Chr17SignalAsDouble;
			if (her2Chr17Ratio.HasValue)
			{
				if (her2Chr17Ratio < 2.0)
				{
					this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*HER2STATUS*", NegativeResult);
					this.m_Result = NegativeResult;
					this.m_ResultCode = NegativeResultCode;
				}
				else if (her2Chr17Ratio >= 2.0)
				{
					this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*HER2STATUS*", PositiveResult);
					this.m_Result = PositiveResult;
					this.m_ResultCode = PositiveResultCode;
				}
			}
			else
			{
				this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*HER2STATUS*", IndeterminateResult);
				this.m_Result = IndeterminateResult;
				this.m_ResultCode = IndeterminateResultCode;
			}

			//this.m_InterpretiveComment = this.m_InterpretiveComment;

			if (testOrder.Her2byIHCOrder == 1)
			{
				this.m_InterpretiveComment += Environment.NewLine + Environment.NewLine + this.m_InterpretiveCommentP4IHCOrder;
			}

			base.SetResults(testOrder);
		}
	}
}
