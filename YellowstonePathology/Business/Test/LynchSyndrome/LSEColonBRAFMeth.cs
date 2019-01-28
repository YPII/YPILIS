using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Audit.Model;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class LSEColonBRAFMeth : LSERule
    {
        public static string Interpretation = "The results are compatible with a sporadic tumor and further genetic evaluation is not indicated.";

        public LSEColonBRAFMeth()
        {
            this.m_RuleName = "Reflex to BRAF/Meth";
            this.m_Indication = LSEType.COLON;
            this.m_AdditionalTesting = "BRAF -> Meth";
        }

        public override bool IncludeInIHCCollection(YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC)
        {
            bool result = false;
            if (panelSetOrderLynchSyndromeIHC.MLH1Result == LSEIHCResult.LossDescription &&
                panelSetOrderLynchSyndromeIHC.MSH2Result == LSEIHCResult.IntactDescription &&
                panelSetOrderLynchSyndromeIHC.MSH6Result == LSEIHCResult.IntactDescription &&
                panelSetOrderLynchSyndromeIHC.PMS2Result == LSEIHCResult.IntactDescription)
            {
                result = true;
            }
            else if (panelSetOrderLynchSyndromeIHC.MLH1Result == LSEIHCResult.LossDescription &&
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
            panelSetOrderLynchSyndromeEvaluation.Interpretation = this.BuildInterpretation(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            panelSetOrderLynchSyndromeEvaluation.Method = this.BuildMethod(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            panelSetOrderLynchSyndromeEvaluation.ReportReferences = LSEColonReferences;
            panelSetOrderLynchSyndromeEvaluation.ReflexToBRAFMeth = true;
        }

        public override AuditResult IsOkToSetResults(AccessionOrder accessionOrder, PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            AuditResult result = base.IsOkToSetResults(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            Rules.MethodResult brafResult = null;
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                brafResult = this.HasFinalBRAFResult(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
                if (brafResult.Success == false)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message = brafResult.Message;
                }
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                if (brafResult.Message == TestResult.NotDetected)
                {
                    Rules.MethodResult methResult = this.HasFinalMethResult(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
                    if (methResult.Success == false)
                    {
                        result.Status = AuditStatusEnum.Failure;
                        result.Message = methResult.Message;
                    }
                }
            }
            return result;
        }

        private string BuildInterpretation(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            string result = LSEColonBRAFMeth.Interpretation;
            Rules.MethodResult brafResult = this.HasFinalBRAFResult(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            Rules.MethodResult methResult = this.HasFinalMethResult(accessionOrder, panelSetOrderLynchSyndromeEvaluation);

            if (brafResult.Message == TestResult.NotDetected && methResult.Message == TestResult.NotDetected)
            {
                result = LSEColonSendOut.Interpretation;
            }

            return result;
        }
    }
}
