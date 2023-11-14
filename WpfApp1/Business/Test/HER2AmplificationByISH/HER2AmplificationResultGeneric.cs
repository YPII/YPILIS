using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Specimen.Model;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
    public class HER2AmplificationResultGeneric : HER2AmplificationResult
    {

        public HER2AmplificationResultGeneric(PanelSetOrderCollection panelSetOrderCollection, HER2AmplificationByISHTestOrder panelSetOrder) : base(panelSetOrderCollection, panelSetOrder)
        {
            //Per ASCO/CAP 2018 HER2 Testing Guidelines, samples with HER2 ISH results of HER2/CEP17 ratio 1.52 and average HER2 copy number of 6.45 signals/cell (group 3) are subsequently tested by HER2 IHC.  HER2 IHC analysis of this sample yielded an equivocal (2+) result, necessitating a blinded observer recount of the HER2 dual-probe ISH slide.  The blinded observer ISH recount (20 cells in the area of strongest HER2 IHC staining) generated results of HER2/CEP17 ratio = 1.41 and average HER2 copy number = 3.8, representing a final result of negative (per internal adjudication procedure).
        }

        public override void SetISHResults(Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            /*
            this.m_Result = HER2AmplificationResultEnum.Positive;
            this.m_InterpretiveComment = InterpretiveComment;
            this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*RATIO*", this.m_HER2AmplificationByISHTestOrder.Her2Chr17Ratio.Value.ToString());
            this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*CELLSCOUNTED*", this.m_HER2AmplificationByISHTestOrder.CellCountToUse.ToString());
            this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*HER2STATUS*", this.m_Result.ToString());
            if (this.m_AverageHer2NeuSignal.HasValue == true)
            {
                this.m_InterpretiveComment = this.m_InterpretiveComment.Replace("*HER2COPY*", this.m_AverageHer2NeuSignal.Value.ToString());
            }

            base.SetISHResults(specimenOrder);
            */
        }
    }
}
