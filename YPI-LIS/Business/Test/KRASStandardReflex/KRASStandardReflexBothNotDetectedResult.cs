using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandardReflex
{
    public class KRASStandardReflexBothNotDetectedResult : KRASStandardReflexKRASWithBRAFResult
    {
        public KRASStandardReflexBothNotDetectedResult(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
            : base(reportNo, accessionOrder)
        {
            this.m_Comment = "This patient may benefit from the use of EGFR inhibitors.";
            this.m_Interpretation = "The use of monoclonal antibodies such as cetuximab and panitumumab has significantly expanded the treatment options for " +
                "patients with metastatic colorectal cancer (CRC). These agents, when used alone or in combination with chemotherapy, selectively inhibit the epidermal growth factor " +
                "receptor (EGFR) and thus prevent activation of the RAS-RAF-MAPK pathway that drives tumor growth and progression.  Mutations of KRAS and BRAF, two genes in the EGFR " +
                "pathway, occur at a similar stage in the adenoma-carcinoma sequence of colorectal cancers, and these mutations have been shown to be mutually exclusive.  Recent " +
                "studies have demonstrated that as many as 35 to 45% of metastatic colorectal cancers harbor oncogenic point mutations in codons 12 or 13 of the KRAS gene, resulting " +
                "in constitutive activation of the RAS-RAF-MAPK pathway. Moreover, up to 15% of metastatic colorectal cancers, many of which are negative for activating KRAS mutations, " +
                "harbor a V600E/K mutation in the BRAF gene. Both KRAS and BRAF mutations result in constitutive activation of the RAS-RAF-MAPK pathway, leading to uncontrolled " +
                "proliferation of tumor cells.  Tumors with either KRAS or BRAF mutations exhibit resistance to anti-EGFR therapies and are associated with shorter progression-free " +
                "and overall survival.  Therefore, testing for both KRAS and BRAF mutations provides useful prognostic information and allows for individualized treatment of patients " +
                "with metastatic CRC.  Neither an STA product indicative of a KRAS mutation nor a 107-base product indicative of a BRAFV600E/K mutation were detected by high resolution " +
                "capillary electrophoresis, indicating that this patient has a metastatic CRC that may respond to anti-EGFR therapy.";		            
        }        
    }
}
