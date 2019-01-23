using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class LSEGeneralSendOut : LSERule
    {
        public static string Interpretation = "Results indicate mismatch repair deficiency, which may render the tumor responsive to PD-1 " +
            "blockade therapy.  As a subset of patients with MMR deficient prostate cancers have Lynch Syndrome, genetic counseling is recommended.";

        public LSEGeneralSendOut()
        {
            this.m_ResultName = "Send out for further testing";
            this.m_Indication = LSEType.GENERAL;
        }

        public override bool IncludeInIHCCollection(YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC)
        {
            bool result = false;
            if (panelSetOrderLynchSyndromeIHC.MLH1Result == LSEIHCResult.LossDescription ||
                panelSetOrderLynchSyndromeIHC.MSH2Result == LSEIHCResult.LossDescription ||
                panelSetOrderLynchSyndromeIHC.MSH6Result == LSEIHCResult.LossDescription ||
                panelSetOrderLynchSyndromeIHC.PMS2Result == LSEIHCResult.LossDescription)
            {
                result = true;
            }
            return result;
        }

        public override void SetResults(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            panelSetOrderLynchSyndromeEvaluation.Result = this.BuildLossResult(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            panelSetOrderLynchSyndromeEvaluation.Interpretation = LSEGeneralSendOut.Interpretation;
            panelSetOrderLynchSyndromeEvaluation.Method = IHCMethod;
            panelSetOrderLynchSyndromeEvaluation.ReportReferences = LSEGENReferences;
            panelSetOrderLynchSyndromeEvaluation.ReflexToBRAFMeth = false;
        }
    }
}
