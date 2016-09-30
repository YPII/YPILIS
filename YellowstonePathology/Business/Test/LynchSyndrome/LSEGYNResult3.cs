using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEGYNResult3 : LSEResult
	{
        public LSEGYNResult3()
		{
			this.m_MLH1Result = LSEResultEnum.Positive;
			this.m_MSH2Result = LSEResultEnum.Negative;
			this.m_MSH6Result = LSEResultEnum.Negative;
			this.m_PMS2Result = LSEResultEnum.Positive;
			this.m_BrafResult = LSEResultEnum.NotApplicable;
			this.m_MethResult = LSEResultEnum.NotApplicable;
            this.m_BRAFIsIndicated = false;

            this.m_Interpretation = "Loss of nuclear expression of MSH2 and MSH6 mismatch repair proteins.";
            this.m_Comment = "This staining pattern is highly suggestive of Lynch Syndrome and is associated with germline MSH2, EPCAM, or MSH6 mutations.  Recommend genetic counseling and further evaluation.";
        }

		public override void SetResults(AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromEvaluation)
        {			
            panelSetOrderLynchSyndromEvaluation.Interpretation = this.m_Interpretation;
            panelSetOrderLynchSyndromEvaluation.Comment = this.m_Comment;
            panelSetOrderLynchSyndromEvaluation.BRAFIsIndicated = this.m_BRAFIsIndicated;
            panelSetOrderLynchSyndromEvaluation.Method = "IHC: " + IHCMethod;
            panelSetOrderLynchSyndromEvaluation.ReportReferences = "IHC: " + LSEGYNReferences;
        }
	}
}
