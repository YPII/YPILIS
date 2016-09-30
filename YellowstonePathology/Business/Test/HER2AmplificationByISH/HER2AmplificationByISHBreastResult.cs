using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
	public class HER2AmplificationByISHBreastResult :HER2AmplificationByISHResult
	{

		public HER2AmplificationByISHBreastResult()
		{
			this.m_InterpretiveComment = "Human epidermal growth factor receptor 2 gene (HER2) is amplified in approximately 20% of breast cancers.  " +
				"Amplification of the HER2 gene in breast tumors is associated with a worse prognosis.  HER2 status is also predictive of response to " +
				"chemotherapeutic agents.  Dual in situ hybridization (ISH) studies for HER2 amplification were performed on the submitted sample, in " +
				"accordance with current ASCO/CAP guidelines. For this patient, the HER2:Chr17 ratio was *RATIO* and average HER2 copy number per cell " +
				"was *HER2COPY* (*CELLSCOUNTED* nuclei examined).  Therefore, HER2 status is *HER2STATUS*.";
			this.m_ReportReference = "Wolff AC, Hammond MEH, Hicks DG, et al. Recommendations for Human Epidermal Growth Factor Receptor 2 Testing in Breast " +
				"Cancer. Arch Pathol Lab Med. doi: 10.5858/arpa.2013-0953-SA.";

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
					if (testOrder.AverageHer2NeuSignal >= 6)
					{
						this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*HER2STATUS*", PositiveResult);
						this.m_Result = PositiveResult;
						this.m_ResultCode = PositiveResultCode;
					}
					else if (testOrder.AverageHer2NeuSignal >= 4 && testOrder.AverageHer2NeuSignal < 6)
					{
						this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*HER2STATUS*", EquivocalResult);
						this.m_Result = EquivocalResult;
						this.m_ResultCode = EquivocalResultCode;
					}
					else if (testOrder.AverageHer2NeuSignal < 4)
					{
						this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*HER2STATUS*", NegativeResult);
						this.m_Result = NegativeResult;
						this.m_ResultCode = NegativeResultCode;
					}
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

			if (testOrder.Her2byIHCOrder == 1)
			{
				this.m_InterpretiveComment += Environment.NewLine + Environment.NewLine + m_InterpretiveCommentP4IHCOrder;
			}

			base.SetResults(testOrder);
		}
	}
}
