using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.CCNDIBCLIGHByPCR
{
    public class CCNDIBCLIGHByPCRNotDetectedResult : CCNDIBCLIGHByPCRResult
    {
        public CCNDIBCLIGHByPCRNotDetectedResult()
        {
            this.m_Result = "Not Detected";
            this.m_ResultCode = "CCNDIBCLIGHBPCRNTDCTD";
            this.m_Interpretation = "The CCND1/IgH t(11;14) translocation is highly specific (70-95%) for mantle cell lymphoma (MCL) and " +
                "can be used for diagnosis, monitoring efficacy of therapy, and the detection of minimal residual disease.The quantitative " +
                "results should only be used to monitor the patient’s own tumor load sequentially.Changes in the trend of measurements of " +
                "BCL1/ IgH fusion DNA rather than one measurement is important in monitoring minimal residual disease. This translocation " +
                "is also seen in 16 % of plasma cell myeloma cases and has been associated with  good prognosis in some studies. Due to " +
                "variation in breakpoint, not all CCND1 / IgH t(11; 14) translocations that detected by FISH or cytogenetic studies are " +
                "detected by PCR-based assays.";
            this.m_Method = "This assay is a real-time quantitative PCR assay. Extracted sample DNA is subjected to two tubes real-time PCR " +
                "reaction to measure the quantity of the t(11; 14) fused gene and the internal control gene.The result is reported as a ratio " +
                "between the quantities of the CCND1/IgH  fused gene to the internal control gene.The positivity (%) of t(11;14) is calculated " +
                "as: (CCND1 pg)/ (Internal CTRL pg) *100%. This assay has a sensitivity of detecting one t(11;14) cell in a background of " +
                "approximately 1000 normal cells.Various factors including quantity and quality of nucleic acid, sample preparation and sample " +
                "age can affect assay performance.";
            this.m_References = "1. R Luthra, AH Sarris, S Hai, AV Paladugu, JE Romaguera, FF Cabanillas, and LJ Medeiros. Real-Time 5' - 3' " +
                "exonuclease-based PCR assay for detection of the t(11; 14) (q13; q32). Am J Clin Pathol 1999; 112: 524 - 530.";
        }
    }
}
