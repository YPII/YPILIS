using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class LSEGYNBRAFMeth : LSERule
    {
        public static string Interpretation = "The results are compatible with a sporadic tumor and further genetic evaluation is not indicated.";

        public LSEGYNBRAFMeth()
        {
            this.m_ResultName = "Reflex to BRAF/Meth";
            this.m_Indication = LSEType.GYN;
        }

        public override bool IncludeInIHCCollection(YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC)
        {
            bool result = false;
            if (panelSetOrderLynchSyndromeIHC.MLH1Result == LSEIHCResult.LossDescription &&
                panelSetOrderLynchSyndromeIHC.MSH2Result == LSEIHCResult.IntactDescription &&
                panelSetOrderLynchSyndromeIHC.MSH6Result == LSEIHCResult.IntactDescription &&
                panelSetOrderLynchSyndromeIHC.PMS2Result == LSEIHCResult.LossDescription)
            {
                result = true;
            }
            return result;
        }

        public override void SetResults(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            string result = this.BuildLossResult(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            result += this.BuildBRAFResult(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            result += this.BuildMethResult(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            panelSetOrderLynchSyndromeEvaluation.Result = result;
            panelSetOrderLynchSyndromeEvaluation.Interpretation = LSEGYNBRAFMeth.Interpretation;
            panelSetOrderLynchSyndromeEvaluation.Method = this.BuildMethod(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            panelSetOrderLynchSyndromeEvaluation.ReportReferences = LSEGYNReferences;
            panelSetOrderLynchSyndromeEvaluation.ReflexToBRAFMeth = true;
        }
    }
}
