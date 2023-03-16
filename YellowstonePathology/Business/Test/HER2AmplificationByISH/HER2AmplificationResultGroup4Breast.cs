using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
    public class HER2AmplificationResultGroup4Breast : HER2AmplificationResultBreast
    {
        public HER2AmplificationResultGroup4Breast(PanelSetOrderCollection panelSetOrderCollection, HER2AmplificationByISHTestOrder panelSetOrder) : base(panelSetOrderCollection, panelSetOrder)
        {
        }

        public HER2AmplificationResultGroup4Breast(PanelSetOrderCollection panelSetOrderCollection, HER2AnalysisSummary.HER2AnalysisSummaryTestOrder panelSetOrder) : base(panelSetOrderCollection, panelSetOrder)
        {
        }

        public override bool IsAMatch()
        {
            bool result = false;
            if (this.m_Indicator.ToString().StartsWith("Breast"))
            {
                if( this.m_AverageHer2Chr17SignalAsDouble.HasValue && this.m_AverageHer2NeuSignal.HasValue)
                {
                    if(this.m_HER2AmplificationByISHTestOrder.AverageHer2Chr17SignalAsDouble < 2.0)
                    {
                        if(this.m_HER2AmplificationByISHTestOrder.AverageHer2NeuSignal >= 4.0 && this.m_HER2AmplificationByISHTestOrder.AverageHer2NeuSignal < 6.0)
                        {
                            result = true;
                        }
                    }
                }
            }
               
            return result;
        }

        public override void SetISHResults(Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            this.m_Result = HER2AmplificationResultEnum.Equivocal;
            this.m_InterpretiveComment = InterpretiveComment;
            this.SetInterpretiveCommentValues(this.m_HER2AmplificationByISHTestOrder.Her2Chr17Ratio.Value.ToString(), this.m_HER2AmplificationByISHTestOrder.CellCountToUse.ToString(),
                this.m_Result.ToString(), this.m_HER2AmplificationByISHTestOrder.AverageHer2NeuSignal);

            base.SetISHResults(specimenOrder);
        }

        public override void SetSummaryResults(Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            this.HandleIHC();

            if (this.m_PanelSetOrderHer2AmplificationByIHC != null && this.m_PanelSetOrderHer2AmplificationByIHC.Final == true &&
                this.m_PanelSetOrderHer2AmplificationByIHC.Score.Contains("2+"))
            {
                if (this.m_HER2AmplificationRecountTestOrder != null && this.m_HER2AmplificationRecountTestOrder.Final == true)
                {
                    if (this.m_HER2AmplificationRecountTestOrder.AverageHer2Chr17SignalAsDouble.HasValue && this.m_HER2AmplificationRecountTestOrder.AverageHer2NeuSignal.HasValue)
                    {
                        if (this.m_HER2AmplificationRecountTestOrder.AverageHer2Chr17SignalAsDouble < 2.0 &&
                            this.m_HER2AmplificationRecountTestOrder.AverageHer2NeuSignal >= 4.0 && this.m_HER2AmplificationRecountTestOrder.AverageHer2NeuSignal < 6.0)
                        {
                            this.m_Result = HER2AmplificationByISH.HER2AmplificationResultEnum.Negative;
                        }
                        else
                        {
                            this.m_Result = HER2AmplificationByISH.HER2AmplificationResultEnum.Equivocal;
                        }
                    }
                }
            }

            if (this.m_Result == HER2AmplificationByISH.HER2AmplificationResultEnum.Negative)
            {
                this.m_InterpretiveComment = "It is uncertain whether patients with an average of ≥ 4.0 and < 6.0 human epidermal growth factor 2 " +
                    "receptor(HER2) signals per cell and HER2 / chromosome enumeration probe 17(CEP17) ratio of < 2.0 benefit from HER2 - " +
                    "targeted therapy in the absence of protein overexpression(immunohistochemistry[IHC] 3 +).  If the specimen test result is " +
                    "close to the in situ hybridization (ISH)ratio threshold for positive, there is a higher likelihood that repeat testing will " +
                    "result in different results by chance alone.  Therefore, when IHC results are not 3 + positive, it is recommended that the " +
                    "sample be considered HER2 negative without additional testing on the same specimen.";
            }
            else if (this.m_Result == HER2AmplificationByISH.HER2AmplificationResultEnum.Positive)
            {
                this.m_InterpretiveComment = InterpretiveComment;
                if (this.m_HER2AmplificationRecountTestOrder != null && this.m_HER2AmplificationRecountTestOrder.Final == true)
                {
                    this.SetInterpretiveCommentValues(this.m_HER2AmplificationRecountTestOrder.AverageHer2Chr17Signal, this.m_HER2AmplificationRecountTestOrder.CellsCounted.ToString(),
                        this.m_Result.ToString(), this.m_HER2AmplificationRecountTestOrder.AverageHer2NeuSignal);
                }
                else
                {
                    this.SetInterpretiveCommentValues(this.m_HER2AmplificationByISHTestOrder.Her2Chr17Ratio.Value.ToString(), this.m_HER2AmplificationByISHTestOrder.CellCountToUse.ToString(),
                        this.m_Result.ToString(), this.m_HER2AmplificationByISHTestOrder.AverageHer2NeuSignal);
                }
            }

            base.SetSummaryResults(specimenOrder);
        }
    }
}
