using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
    public class HER2AmplificationResultGroup2Breast : HER2AmplificationResultBreast
    {

        public HER2AmplificationResultGroup2Breast(PanelSetOrderCollection panelSetOrderCollection, HER2AmplificationByISHTestOrder panelSetOrder) : base(panelSetOrderCollection, panelSetOrder)
        {
        }

        public HER2AmplificationResultGroup2Breast(PanelSetOrderCollection panelSetOrderCollection, HER2AnalysisSummary.HER2AnalysisSummaryTestOrder panelSetOrder) : base(panelSetOrderCollection, panelSetOrder)
        {
            this.m_InterpretiveComment = "Evidence is limited on the efficacy of human epidermal growth factor 2 (HER2)-targeted therapy in the " +
            "small subset of cases with a HER2/chromosome enumeration probe 17 (CEP17) ratio ≥ 2.0 and an average HER2 copy number " +
            "of < 4.0 per cell.  In the first generation of adjuvant trastuzumab trials, patients in this subgroup who were randomly " +
            "assigned to the trastuzumab arm did not seem to derive an improvement in disease-free or overall survival, but there were " +
            "too few such cases to draw definitive conclusions.  Immunohistochemistry (IHC) expression for HER2 should be used to " +
            "complement in situ hybridization (ISH) and define HER2 status.  If the IHC result is not 3+ positive, it is recommended " +
            "that the specimen be considered HER2 negative because of the low HER2 copy number by ISH and the lack of protein expression.";
        }

        public override bool IsAMatch()
        {
            bool result = false;
            if (this.m_Indicator == HER2AmplificationByISH.HER2AmplificationByISHIndicatorCollection.BreastIndication)
            {
                if(this.m_HER2AmplificationByISHTestOrder.AverageHer2Chr17SignalAsDouble.HasValue && this.m_HER2AmplificationByISHTestOrder.AverageHer2NeuSignal.HasValue)
                {
                    if(this.m_HER2AmplificationByISHTestOrder.AverageHer2Chr17SignalAsDouble >= 2.0 && this.m_HER2AmplificationByISHTestOrder.AverageHer2NeuSignal < 4.0)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        public override bool IsASummaryMatch()
        {
            bool result = false;
            if (this.m_Indicator == HER2AmplificationByISH.HER2AmplificationByISHIndicatorCollection.BreastIndication)
            {
                if (this.m_HER2AnalysisSummaryTestOrder.AverageHer2Chr17SignalAsDouble.HasValue && this.m_HER2AnalysisSummaryTestOrder.AverageHer2NeuSignal.HasValue)
                {
                    if (this.m_HER2AnalysisSummaryTestOrder.AverageHer2Chr17SignalAsDouble >= 2.0 && this.m_HER2AnalysisSummaryTestOrder.AverageHer2NeuSignal < 4.0)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        public override void SetISHResults(Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            this.m_Result = HER2AmplificationResultEnum.Equivocal;
            this.m_InterpretiveComment = InterpretiveComment;
            this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*RATIO*", this.m_HER2AmplificationByISHTestOrder.Her2Chr17Ratio.Value.ToString());
            this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*CELLSCOUNTED*", this.m_HER2AmplificationByISHTestOrder.CellCountToUse.ToString());
            this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*HER2STATUS*", this.m_Result.ToString());
            if (this.m_HER2AmplificationByISHTestOrder.AverageHer2NeuSignal.HasValue == true)
            {
                this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*HER2COPY*", this.m_HER2AmplificationByISHTestOrder.AverageHer2NeuSignal.Value.ToString());
            }

            base.SetISHResults(specimenOrder);
        }

        public override void SetSummaryResults(Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            this.HandleIHC();

            if (this.m_HER2AmplificationRecountTestOrder != null && this.m_HER2AmplificationRecountTestOrder.Accepted == true &&
                this.m_PanelSetOrderHer2AmplificationByIHC != null && this.m_PanelSetOrderHer2AmplificationByIHC.Accepted == true)
            {
                if (this.m_PanelSetOrderHer2AmplificationByIHC.Score.Contains("2+"))
                {
                    this.m_Result = HER2AmplificationByISH.HER2AmplificationResultEnum.Negative;
                }
            }

            base.SetSummaryResults(specimenOrder);
        }
    }
}
