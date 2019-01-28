using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEGeneralAllIntact : LSERule
    {
        public static string Interpretation = "Mismatch repair protein expression is intact, indicating that the tumor is unlikely to respond to PD-1 blockade therapy.";

        public LSEGeneralAllIntact()
		{
            this.m_RuleName = "All Intact";
            this.m_Indication = LSEType.GENERAL;
            this.m_AdditionalTesting = LSERule.NoFurtherTesting;
        }

        public override bool IncludeInIHCCollection(YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC)
        {
            bool result = false;
            if (panelSetOrderLynchSyndromeIHC.MLH1Result == LSEIHCResult.IntactDescription &&
                panelSetOrderLynchSyndromeIHC.MSH2Result == LSEIHCResult.IntactDescription &&
                panelSetOrderLynchSyndromeIHC.MSH6Result == LSEIHCResult.IntactDescription &&
                panelSetOrderLynchSyndromeIHC.PMS2Result == LSEIHCResult.IntactDescription)
            {
                result = true;
            }
            return result;
        }

        public override void SetResults(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            panelSetOrderLynchSyndromeEvaluation.Result = LSERule.IHCAllIntactResult;
            panelSetOrderLynchSyndromeEvaluation.Interpretation = LSEColonAllIntact.Interpretation;
            panelSetOrderLynchSyndromeEvaluation.Method = IHCMethod;
            panelSetOrderLynchSyndromeEvaluation.ReportReferences = LSEGENReferences;
            panelSetOrderLynchSyndromeEvaluation.ReflexToBRAFMeth = false;
        }
    }
}
