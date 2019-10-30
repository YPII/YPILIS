using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
    public class HER2AmplificationResultGroup3Breast : HER2AmplificationResultBreast
    {
        public HER2AmplificationResultGroup3Breast(PanelSetOrderCollection panelSetOrderCollection, HER2AmplificationByISHTestOrder panelSetOrder) : base(panelSetOrderCollection, panelSetOrder)
        {
        }

        public HER2AmplificationResultGroup3Breast(PanelSetOrderCollection panelSetOrderCollection, HER2AnalysisSummary.HER2AnalysisSummaryTestOrder panelSetOrder) : base(panelSetOrderCollection, panelSetOrder)
        {
            this.m_InterpretiveComment = "There are insufficient data on the efficacy of human epidermal growth factor receptor 2 (HER2)-targeted " +
                "therapy in cases with a HER2 ratio of < 2.0 in the absence of protein overexpression because such patients were not eligible " +
                "for the first generation of adjuvant trastuzumab clinical trials.  When concurrent immunohistochemistry (IHC) results are " +
                "negative (0 or 1+), it is recommended that the specimen be considered HER2 negative.";
        }

        public override bool IsAMatch()
        {
            bool result = false;
            if (this.m_Indicator == HER2AmplificationByISH.HER2AmplificationByISHIndicatorCollection.BreastIndication)
            {
                if(this.m_HER2AmplificationByISHTestOrder.AverageHer2Chr17SignalAsDouble.HasValue && this.m_HER2AmplificationByISHTestOrder.AverageHer2NeuSignal.HasValue)
                {
                    if(this.m_HER2AmplificationByISHTestOrder.AverageHer2Chr17SignalAsDouble < 2.0 && this.m_HER2AmplificationByISHTestOrder.AverageHer2NeuSignal >= 6.0)
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
                    this.m_Result = HER2AmplificationByISH.HER2AmplificationResultEnum.Positive;
                }
            }

            base.SetSummaryResults(specimenOrder);
        }
    }
}
