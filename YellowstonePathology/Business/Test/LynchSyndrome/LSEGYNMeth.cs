using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class LSEGYNMeth : LSERule
    {
        public static string Interpretation = "The results are compatible with a sporadic tumor and further genetic evaluation is not indicated.";

        public LSEGYNMeth()
        {
            this.m_RuleName = "Reflex to Meth";
            this.m_Indication = LSEType.GYN;
            this.m_AdditionalTesting = "Meth";
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
            result += this.BuildMethResult(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            panelSetOrderLynchSyndromeEvaluation.Result = result;
            panelSetOrderLynchSyndromeEvaluation.Interpretation = LSEGYNMeth.Interpretation;
            panelSetOrderLynchSyndromeEvaluation.Method = this.BuildMethod(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            panelSetOrderLynchSyndromeEvaluation.ReportReferences = LSEGYNReferences;
            panelSetOrderLynchSyndromeEvaluation.ReflexToBRAFMeth = true;
        }

        public override Audit.Model.AuditResult IsOkToSetResults(AccessionOrder accessionOrder, PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            Audit.Model.AuditResult result = base.IsOkToSetResults(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                Rules.MethodResult methResult = this.HasFinalMethResult(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
                if (methResult.Success == false)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message = methResult.Message;
                }
            }
            return result;
        }

        private string BuildInterpretation(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            string result = LSEGYNMeth.Interpretation;
            Rules.MethodResult methResult = this.HasFinalMethResult(accessionOrder, panelSetOrderLynchSyndromeEvaluation);

            if (methResult.Message == TestResult.NotDetected)
            {
                result = LSEColonSendOut.Interpretation;
            }

            return result;
        }
    }
}
