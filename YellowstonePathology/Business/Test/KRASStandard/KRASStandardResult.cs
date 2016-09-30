using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandard
{
	public class KRASStandardResult
	{        
		protected string m_Result;
		protected string m_ResultCode;		
		protected string m_Interpretation;		
		protected string m_Comment;
		protected string m_Method;
		protected string m_References;
		protected string m_ResultDescription;

		public KRASStandardResult()
		{
            this.m_Interpretation = "The use of monoclonal antibodies such as cetuximab and panitumumab has significantly expanded the treatment " +
                "options for patients with metastatic colorectal cancer (CRC).  These agents, when used alone or in combination with chemotherapy, selectively inhibit " +
                "the epidermal growth factor receptor (EGFR) and thus prevent activation of the RAS-RAF-MAPK pathway that drives tumor growth and progression.  Recent " +
                "studies have demonstrated that 35 to 45% of metastatic colorectal cancers harbor oncogenic point mutations in codons 12 or 13 of the KRAS gene, " +
                "resulting in constitutive activation of the RAS-RAF-MAPK pathway.  Tumors with KRAS mutations exhibit resistance to anti-EGFR therapy and are " +
                "associated with shorter progression-free and overall survival.  Therefore, testing for KRAS point mutations in codons 12 and 13 provides useful " +
                "prognostic information and allows for individualized treatment of patients with metastatic CRC.\r\n\r\n" +
                "An STA product indicative of a KRAS mutation was not detected by high resolution capillary electrophoresis, indicating that the patient has a " +
                "metastatic CRC that may respond to anti-EGFR therapy.";

			this.m_Method = "DNA was first extracted from the patient's paraffin-embedded specimen using an automated DNA extraction system.  KRAS " +
			    "allele-specific PCR was then performed on the patient's sample and on positive and negative controls.  The products generated from this reaction " +
			    "were then subjected to a second PCR step employing fluorescently-labeled nucleotides and primers designed to detect both mutant and wild-type forms " +
			    "of the KRAS gene.  Utilizing a procedure similar to traditional DNA sequencing, termed the SHIFTED TERMINATION ASSAY (STA), the presence of any of " +
			    "the 12 KRAS point mutations causes termination of complementary DNA chain synthesis during amplification.  Thus, complementary DNA strands are " +
			    "formed with lengths that are specific to the particular KRAS point mutation present.  The products of the STA reaction were analyzed using " +
			    "high-resolution capillary electrophoresis to detect the presence of DNA fragments indicative of a KRAS mutation.";		    

		    this.m_References = "1.   Amado RG, Wolf M, Peeters M, et al. Wild-type KRAS is required for panitumumab efficacy in patients with " +
			    "metastatic colorectal cancer. J Clin Oncol. 2008; 26(10): 1626-1634.\r\n" +
			    "2.   Bokemeyer C, Bondarenko I, Makhson A, et al. Fluorouracil, leucovorin, and oxiplatin with and without cituximab in the first-line " +
			    "treatment of metastatic colorectal cancer.    J Clin Oncol. 2009; 27(5): 663-671.";
		}

		public string ResultCode
		{
			get { return this.m_ResultCode; }
		}

		public string Result
		{
			get { return this.m_Result; }
		}

		public string ResultDescription
		{
			get { return this.m_ResultDescription; }
		}		

		public string Interpretation
		{
			get { return this.m_Interpretation; }
		}		

		public string Comment
		{
			get { return this.m_Comment; }
		}

		public string Method
		{
			get { return this.m_Method; }
		}

		public string Reference
		{
			get { return this.m_References; }
		}		

		public virtual void SetResults(KRASStandardTestOrder testOrder)
		{
            YellowstonePathology.Business.Test.IndicationCollection indicationCollection = IndicationCollection.GetAll();
            YellowstonePathology.Business.Test.Indication indication = indicationCollection.GetIndication(testOrder.Indication);
            testOrder.IndicationComment = indication.Description;

			testOrder.ResultCode = this.m_ResultCode;
			testOrder.Result = this.m_Result;
			testOrder.MutationDetected = this.m_ResultDescription;
			testOrder.Method = this.m_Method;			
			testOrder.Interpretation = this.m_Interpretation;
			testOrder.Comment = this.m_Comment;
			testOrder.ReportReferences = this.m_References;			
		}
	}
}
