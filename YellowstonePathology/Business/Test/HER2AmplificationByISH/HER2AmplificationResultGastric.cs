using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
    public class HER2AmplificationResultGastric : HER2AmplificationResult
    {

        public HER2AmplificationResultGastric(PanelSetOrderCollection panelSetOrderCollection, string reportNo) : base(panelSetOrderCollection, reportNo)
        {
            this.m_InterpretiveComment = "Human epidermal growth factor receptor 2 gene (HER2) is amplified in up to 25% of gastric cancers, " +
                "particularly those that arise at the gastroesophageal junction.  Amplification of the HER2 gene in gastric tumors is associated with " +
                "a worse prognosis and is also predictive of response to chemotherapeutic agents.  Dual in situ hybridization (ISH) studies for HER2 " +
                "amplification were performed on the submitted sample, in accordance with ASCO/CAP guidelines." + Environment.NewLine + Environment.NewLine +
                "For this patient, the HER2:Chr17 ratio was *RATIO*. A total of *CELLSCOUNTED* nuclei were examined. Therefore, HER2 status is *HER2STATUS*.";
            this.m_ReportReference = "Gravalos, C. et al., HER2 in gastric cancer: a new prognostic factor and a novel therapeutic target. Ann " +
                "Oncology 2008, 19:1523-1529.";
        }

        public override bool IsAMatch()
        {
            bool result = false;
            if (this.m_HER2ISH.Indicator == HER2AmplificationByISHIndicatorCollection.GastricIndication) result = true;
            return result;
        }

        public override void SetResults(Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            this.m_ResultComment = null;
            this.m_ResultDescription = "Ratio = " + this.m_HER2ISH.Her2Chr17Ratio;

            this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*RATIO*", this.m_HER2ISH.Her2Chr17Ratio.Value.ToString());
            this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*CELLSCOUNTED*", this.m_HER2ISH.CellCountToUse.ToString());
            if (this.m_HER2ISH.AverageHer2NeuSignal.HasValue == true) this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*HER2COPY*", this.m_HER2ISH.AverageHer2NeuSignal.Value.ToString());

            Nullable<double> her2Chr17Ratio = this.m_HER2ISH.AverageHer2Chr17SignalAsDouble;
            if (her2Chr17Ratio.HasValue)
            {
                if (her2Chr17Ratio < 2.0)
                {
                    this.m_Result = HER2AmplificationResultEnum.Negative;
                    this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*HER2STATUS*", this.m_Result.ToString());
                }
                else if (her2Chr17Ratio >= 2.0)
                {
                    this.m_Result = HER2AmplificationResultEnum.Positive;
                    this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*HER2STATUS*", this.m_Result.ToString());
                }
            }

            base.SetResults(specimenOrder);
        }
    }
}
