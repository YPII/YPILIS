using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
    public class HER2AmplificationResultGroup5Breast : HER2AmplificationResultBreast
    {

        public HER2AmplificationResultGroup5Breast(PanelSetOrderCollection panelSetOrderCollection, string reportNo) : base(panelSetOrderCollection, reportNo)
        {
        }

        public override bool IsAMatch()
        {
            bool result = false;
            if (this.m_HER2ISH.Indicator == HER2AmplificationByISHIndicatorCollection.BreastIndication &&
                this.m_HER2ISH.AverageHer2Chr17SignalAsDouble.HasValue &&
                this.m_HER2ISH.AverageHer2Chr17SignalAsDouble < 2.0)
            {
                result = true;
                this.m_Result = HER2AmplificationResultEnum.Negative;
            }
            return result;
        }

        public override void SetResults(Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            this.m_InterpretiveComment = InterpretiveComment;
            this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*RATIO*", this.m_HER2ISH.Her2Chr17Ratio.Value.ToString());
            this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*CELLSCOUNTED*", this.m_HER2ISH.CellCountToUse.ToString());
            this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*HER2STATUS*", this.m_Result.ToString());
            if (this.m_HER2ISH.AverageHer2NeuSignal.HasValue == true)
            {
                this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*HER2COPY*", this.m_HER2ISH.AverageHer2NeuSignal.Value.ToString());
            }

            base.SetResults(specimenOrder);
        }
    }
}
