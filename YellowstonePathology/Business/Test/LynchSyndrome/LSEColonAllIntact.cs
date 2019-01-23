using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEColonAllIntact : LSERule
	{
        public static string Interpretation = "The results are compatible with a sporadic tumor and indicate a low risk for Lynch Syndrome.  " +
                "If clinical suspicion for Lynch Syndrome is high, microsatellite instability (MSI) testing by PCR is recommended. " +
                "If MSI testing is desired, please contact Yellowstone Pathology with the request.";

        public LSEColonAllIntact()
		{
            this.m_ResultName = "All Intact";
            this.m_Indication = LSEType.COLON;
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
            panelSetOrderLynchSyndromeEvaluation.ReportReferences = LSEColonReferences;
            panelSetOrderLynchSyndromeEvaluation.ReflexToBRAFMeth = false;
        }
    }
}
