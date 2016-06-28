using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BRAFV600EK
{
	public class BRAFV600EKNotDetectedCRCResult : BRAFV600EKNotDetectedResult
	{		
		public BRAFV600EKNotDetectedCRCResult()
		{
            YellowstonePathology.Business.Test.IndicationColorectalCancer indication = new IndicationColorectalCancer();
            this.m_Indication = indication.IndicationCode;
            this.m_IndicationComment = indication.Description;
		    this.m_Interpretation = "The use of monoclonal antibodies such as cetuximab and panitumumab has significantly expanded the treatment " +
			    "options for patients with metastatic colorectal cancer (CRC).  These agents, when used alone or in combination with chemotherapy, selectively inhibit the epidermal " +
			    "growth factor receptor (EGFR) and thus prevent activation of the RAS-RAF-MAPK pathway that drives tumor growth and progression.  Both BRAF and KRAS mutations occur " +
			    "at a similar stage in the adenoma-carcinoma sequence of colorectal cancers, and recent studies have demonstrated that these mutations are mutually exclusive.  Up " +
			    "to 15% of all metastatic colorectal cancers, a significant percentage of which are negative for activating KRAS mutations, harbor a V600E/K mutation in the BRAF " +
			    "gene.  Both KRAS and BRAF mutations result in constitutive activation of the RAS-RAF-MAPK pathway, leading to uncontrolled proliferation of tumor cells.  Tumors " +
			    "with either KRAS or BRAF mutations exhibit resistance to anti-EGFR therapies and are associated with shorter progression-free and overall survival.  Therefore, " +
			    "testing for the BRAF V600E/K mutation provides useful prognostic information and allows for individualized treatment of patients with metastatic CRC.  A 107-base " +
			    "product indicative of a BRAF V600E/K mutation was NOT detected by high resolution capillary electrophoresis.";		    
            this.m_References = BRAFV600EKResult.ColoRectalCancerReferences;
		}
	}
}
