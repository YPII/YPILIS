using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEGYNResult1 : LSEResult
	{
        public LSEGYNResult1()
		{
			this.m_MLH1Result = LSEResultEnum.Negative;
			this.m_MSH2Result = LSEResultEnum.Positive;
			this.m_MSH6Result = LSEResultEnum.Positive;
			this.m_PMS2Result = LSEResultEnum.Negative;
			this.m_BrafResult = LSEResultEnum.NotApplicable;
			this.m_MethResult = LSEResultEnum.Positive;
            this.m_BRAFIsIndicated = false;

            this.m_Interpretation = "Loss of nuclear expression of MLH1 and PMS2 mismatch repair proteins." + Environment.NewLine + "MLH1 Methylation Analysis detected.";
            this.m_Comment = "The results are compatible with a sporadic tumor and further genetic evaluation is not indicated.";            
		}

		public override void SetResults(AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromEvaluation)
        {
			YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest panelSetMLH1 = new YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest();
			YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis panelSetOrderMLH1MethylationAnalysis = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetMLH1.PanelSetId, panelSetOrderLynchSyndromEvaluation.OrderedOnId, true);

            panelSetOrderLynchSyndromEvaluation.Interpretation = this.m_Interpretation;
            panelSetOrderLynchSyndromEvaluation.Comment = this.m_Comment;
            panelSetOrderLynchSyndromEvaluation.BRAFIsIndicated = this.m_BRAFIsIndicated;
            panelSetOrderLynchSyndromEvaluation.Method = "IHC: " + IHCMethod + Environment.NewLine + Environment.NewLine + "MLH1: " + panelSetOrderMLH1MethylationAnalysis.Method;
            panelSetOrderLynchSyndromEvaluation.ReportReferences = "IHC: " + LSEGYNReferences + Environment.NewLine + Environment.NewLine + "MLH1: " + panelSetOrderMLH1MethylationAnalysis.ReportReferences;
        }
	}
}
