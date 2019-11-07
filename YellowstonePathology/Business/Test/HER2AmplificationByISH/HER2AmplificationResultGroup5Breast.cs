using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
    public class HER2AmplificationResultGroup5Breast : HER2AmplificationResultBreast
    {

        public HER2AmplificationResultGroup5Breast(PanelSetOrderCollection panelSetOrderCollection, HER2AmplificationByISHTestOrder panelSetOrder) : base(panelSetOrderCollection, panelSetOrder)
        {
        }

        public HER2AmplificationResultGroup5Breast(PanelSetOrderCollection panelSetOrderCollection, HER2AnalysisSummary.HER2AnalysisSummaryTestOrder panelSetOrder) : base(panelSetOrderCollection, panelSetOrder)
        {
        }

        public override bool IsAMatch()
        {
            bool result = false;
            if (this.m_Indicator == HER2AmplificationByISHIndicatorCollection.BreastIndication)
            {
                if(this.m_AverageHer2Chr17SignalAsDouble.HasValue && this.m_AverageHer2NeuSignal.HasValue)
                {
                    if(this.m_HER2AmplificationByISHTestOrder.AverageHer2Chr17SignalAsDouble < 2.0 && this.m_HER2AmplificationByISHTestOrder.AverageHer2NeuSignal < 4.0)
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
            if (this.m_Indicator == HER2AmplificationByISHIndicatorCollection.BreastIndication)
            {
                if (this.m_AverageHer2Chr17SignalAsDouble.HasValue && this.m_AverageHer2NeuSignal.HasValue)
                {
                    if (this.m_HER2AnalysisSummaryTestOrder.AverageHer2Chr17SignalAsDouble < 2.0 && this.m_HER2AnalysisSummaryTestOrder.AverageHer2NeuSignal < 4.0)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        public override void SetISHResults(Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            this.m_Result = HER2AmplificationResultEnum.Negative;
            this.m_InterpretiveComment = InterpretiveComment;
            this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*RATIO*", this.m_HER2AmplificationByISHTestOrder.Her2Chr17Ratio.Value.ToString());
            this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*CELLSCOUNTED*", this.m_HER2AmplificationByISHTestOrder.CellCountToUse.ToString());
            this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*HER2STATUS*", this.m_Result.ToString());
            if (this.m_AverageHer2NeuSignal.HasValue == true)
            {
                this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*HER2COPY*", this.m_AverageHer2NeuSignal.Value.ToString());
            }

            base.SetISHResults(specimenOrder);
        }

        public override void SetSummaryResults(Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            this.m_Result = HER2AmplificationResultEnum.Negative;
            this.m_InterpretiveComment = InterpretiveComment;
            this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*RATIO*", this.m_HER2AnalysisSummaryTestOrder.Her2Chr17Ratio.Value.ToString());
            this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*CELLSCOUNTED*", this.m_HER2AnalysisSummaryTestOrder.CellsCounted.ToString());
            this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*HER2STATUS*", this.m_Result.ToString());
            if (this.m_AverageHer2NeuSignal.HasValue == true)
            {
                this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*HER2COPY*", this.m_AverageHer2NeuSignal.Value.ToString());
            }

            base.SetSummaryResults(specimenOrder);
        }
    }
}
