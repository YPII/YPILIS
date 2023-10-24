using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandardReflex
{
    public class KRASStandardReflexKRASNotDetecedBRAFDetectedResult : KRASStandardReflexKRASWithBRAFResult
    {
        public KRASStandardReflexKRASNotDetecedBRAFDetectedResult(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
            : base(reportNo, accessionOrder)
        {
            this.m_Interpretation = "The use of monoclonal antibodies such as cetuximab has expanded the treatment options for patients with metastatic colorectal cancer (CRC). These agents selectively inhibit the " +
                "epidermal growth factor receptor (EGFR) and thus prevent activation of the RAS-RAF-MAPK pathway that drives tumor growth.  Recent studies have demonstrated that as many as 35 to " +
                "45% of metastatic colorectal cancers harbor oncogenic point mutations in codons 12 or 13 of the KRAS gene, resulting in constitutive activation of the RAS-RAF-MAPK pathway.  Both " +
                "KRAS and BRAF mutations result in constitutive activation of the RAS-RAF-MAPK pathway, leading to uncontrolled proliferation of tumor cells.  Tumors with either KRAS or BRAF " +
                "mutations exhibit resistance to anti-EGFR therapies.  Therefore, testing for both KRAS and BRAF mutations provides useful prognostic information and allows for individualized " +
                "treatment of patients with metastatic CRC.  A product indicative of a KRAS mutation was not detected; however, a 107-base product indicative of a BRAFV600E mutation was detected.  " +
                "This confirms that the patient has a metastatic CRC that is unlikely to respond to anti-EGFR therapy.";         
        }        
    }
}
