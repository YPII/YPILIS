using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
    public class HER2AmplificationResultGroup2Breast : HER2AmplificationResultBreast
    {
        public HER2AmplificationResultGroup2Breast(PanelSetOrderCollection panelSetOrderCollection, string reportNo) : base(panelSetOrderCollection, reportNo)
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
            if (this.m_HER2ISH.Indicator == HER2AmplificationByISHIndicatorCollection.BreastIndication &&
                this.m_HER2ISH.AverageHer2Chr17SignalAsDouble.HasValue &&
                this.m_HER2ISH.AverageHer2Chr17SignalAsDouble >= 2.0 &&
                this.m_HER2ISH.AverageHer2NeuSignal.HasValue &&
                this.m_HER2ISH.AverageHer2NeuSignal < 4.0)
            {
                result = true;
                this.HandleIHC();

                if (this.m_HER2AmplificationRecountTestOrder != null && this.m_HER2AmplificationRecountTestOrder.Accepted == true &&
                    this.m_PanelSetOrderHer2AmplificationByIHC != null && this.m_PanelSetOrderHer2AmplificationByIHC.Accepted == true)
                {
                    if (this.m_PanelSetOrderHer2AmplificationByIHC.Score.Contains("2+"))
                    {
                        this.m_Result = HER2AmplificationResultEnum.Negative;
                    }
                }
            }
            return result;
        }
    }
}
