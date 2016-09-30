using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandard
{
	public class KRASStandardNotDetectedResult : KRASStandardResult
	{		
		public KRASStandardNotDetectedResult()
		{
            this.m_Result = "Not Detected";
			this.m_ResultCode = "KRSSTDNTDTCTD";
            this.m_Comment = "This patient may benefit from the use of EGFR inhibitors.";
			this.m_Interpretation = "The use of monoclonal antibodies such as cetuximab and panitumumab has significantly expanded the treatment " +
			    "options for patients with metastatic colorectal cancer (CRC). These agents, when used alone or in combination with chemotherapy, selectively inhibit " +
			    "the epidermal growth factor receptor (EGFR) and thus prevent activation of the RAS-RAF-MAPK pathway that drives tumor growth and progression. Recent " +
			    "studies have demonstrated that 35 to 45% of metastatic colorectal cancers harbor oncogenic point mutations in codons 12 or 13 of the KRAS gene, " +
			    "resulting in constitutive activation of the RAS-RAF-MAPK pathway. Tumors with KRAS mutations exhibit resistance to anti-EGFR therapy and are " +
			    "associated with shorter progression-free and overall survival. Therefore, testing for KRAS point mutations in codons 12 and 13 provides useful " +
			    "prognostic information and allows for individualized treatment of patients with metastatic CRC.\r\n\r\n" +
			    "An STA product indicative of a KRAS mutation was not detected by high resolution capillary electrophoresis, indicating that the patient has a " +
			    "metastatic CRC that may respond to anti-EGFR therapy.";
		}
	}
}
