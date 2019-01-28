using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class LSEGYNSendOut : LSERule
    {

        public static string Interpretation = "This staining pattern is highly suggestive of Lynch Syndrome   Recommend genetic counseling and further evaluation.";

        public LSEGYNSendOut()
        {
            this.m_RuleName = "Send out";
            this.m_Indication = LSEType.GYN;
            this.m_AdditionalTesting = LSERule.SendOutForTesting;
        }

        public override bool IncludeInIHCCollection(YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC)
        {
            bool result = false;
            if (panelSetOrderLynchSyndromeIHC.MLH1Result == LSEIHCResult.IntactDescription &&
                panelSetOrderLynchSyndromeIHC.MSH2Result == LSEIHCResult.LossDescription &&
                panelSetOrderLynchSyndromeIHC.MSH6Result == LSEIHCResult.LossDescription &&
                panelSetOrderLynchSyndromeIHC.PMS2Result == LSEIHCResult.IntactDescription)
            {
                result = true;
            }
            else if (panelSetOrderLynchSyndromeIHC.MLH1Result == LSEIHCResult.IntactDescription &&
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
            panelSetOrderLynchSyndromeEvaluation.Result = this.BuildLossResult(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            panelSetOrderLynchSyndromeEvaluation.Interpretation = LSEGYNSendOut.Interpretation;
            panelSetOrderLynchSyndromeEvaluation.Method = IHCMethod;
            panelSetOrderLynchSyndromeEvaluation.ReportReferences = LSEGYNReferences;
            panelSetOrderLynchSyndromeEvaluation.ReflexToBRAFMeth = false;
        }
    }
}
