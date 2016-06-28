using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandardReflex
{
    public class KRASStandardReflexKRASWithBRAFResult : KRASStandardReflexResult
    {
        public KRASStandardReflexKRASWithBRAFResult(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder) : base(reportNo, accessionOrder)
        {
            this.m_Method = "DNA was isolated from cells or microdissection-enriched FFPE tissue. Tumor in FFPE must be present in at least 20% of the tissue. " +
                "Mutations were evaluated for entire KRAS exons 2 and 3 high-sensitivity Sanger sequencing which improves the lower detection limit in mutation hotspot " +
                "regions to approximately 1% abnormal DNA. This includes codons 12, 13, 14, and 61. Mutation detection outside these hotspot regions has a typical lower " +
                "detection limit of 10-15% mutated KRAS in a wild-type background. The patient's sequence is compared to the GenBank database: AF493917. The DNA was also " +
                "evaluated for the presence of BRAF V600E/K by PCR and fragment size anaylsis. Various factors including quantity and quality of nucleic acid, sample " +
                "preparation and sample age can affect assay performance.";            

            YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest brafV600EKTest = new BRAFV600EK.BRAFV600EKTest();
            this.m_BRAFV600EKTestOrder = (YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafV600EKTest.PanelSetId, this.m_KRASStandardReflexTestOrder.OrderedOnId, true);

            if (this.KRASStandardTestOrder.Final == true)
            {
                this.m_KRASStandardResult = this.m_KRASStandardTestOrder.Result;
            }
            else
            {
                this.m_KRASStandardResult = KRASStandardReflexResult.PendingResult;
            }

            if (this.m_BRAFV600EKTestOrder.Final == true)
            {
                this.m_BRAFV600EKResult = this.m_BRAFV600EKTestOrder.Result;
            }
            else
            {
                this.m_BRAFV600EKResult = KRASStandardReflexResult.PendingResult;
            }
        }        

        public override void SetResults()
        {
            this.m_KRASStandardReflexTestOrder.Comment = this.m_Comment;
            this.m_KRASStandardReflexTestOrder.Method = this.m_Method;
            this.m_KRASStandardReflexTestOrder.References = this.m_References;
            this.m_KRASStandardReflexTestOrder.Interpretation = this.m_Interpretation;
            this.m_KRASStandardReflexTestOrder.ReportDisclaimer = this.m_ReportDisclaimer;

            //this.m_KRASStandardReflexTestOrder.TumorNucleiPercent = this.m_BRAFV600EKTestOrder.TumorNucleiPercent;
            this.m_KRASStandardReflexTestOrder.Indication = this.m_BRAFV600EKTestOrder.Indication;
            this.m_KRASStandardReflexTestOrder.IndicationComment = this.m_BRAFV600EKTestOrder.IndicationComment;                   
        }
    }
}
