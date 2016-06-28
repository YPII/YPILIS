using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BRAFV600EK
{
    public class BRAFV600EKResult : YellowstonePathology.Business.Test.TestResult
    {                        
		public static string ColoRectalCancerReferences = "1.   Benvenuti S, Sartore-Bianchi A, Di Nicolantonio F, et al. Oncogenic activation of the RAS/RAF signaling " +
			"pathway impairs the response of metastatic colorectal cancers to anti-epidermal growth factor receptor antibody therapies. Cancer Res. 2007; 67(6): 2643-2648.\r\n" +
			"2.  Di Nicolantonio F, Martini M, Molinari F, et al. Wild-type BRAF is required for response to panitumumab or cetuximab in metastatic colorectal cancer. J " +
			"Clin Oncol. 2008; 26(35): 5705-5712.\r\n" +
			"3.  Cappuzzo F, Varella-Garcia M, Finocchiaro G, et al. Primary resistance to cetuximab therapy in EGFR FISH-positive colorectal cancer patients. Br J Cancer " +
			"2008; 99: 83-89.\r\n" +
			"4.  Barault L, Veyrie N, Jooste V, et al. Mutations in the RAS-MAPK, PI(3)K (phosphotidylinositol-3-OH kinase) signaling network correlate with poor survival " +
			"in a population-based series of colon cancers. Int J Cancer. 2008; 122(10): 2255-2259.\r\n" +
			"5.  Di Nicolantonio F, Sartori-Bianchi A, Molinari F, et al. BRAF, PIK3CA, and KRAS mutations and loss of PTEN expression impair response to EGFR-targeted " +
			"therapies in metastatic colorectal cancer. In: Proceedings of the 100th Annual Meeting of the American Association for Cancer Research; April 18-22, 2009; " +
			"Denver, CO: AACR; 2009. Abstract LB-93.";

		public static string PapillaryThyroidReference = "1.  Elisei R, et al.  BRAF (V600E) mutation and outcome of patients with papillary thyroid carcinoma: a 15-year " +
			"median follow-up study.  J Clin Endocrinol Metab. 2008 Oct; 93(10): 3943-9.\r\n" +
			"2.  Nikiforov YE, et al.  Molecular Testing for Mutations in Improving the Fine Needle Aspiration Diagnosis of Thyroid Nodules.  J Clin Endocrinol Metab. 2009 " +
			"Mar; 94(6): 2092-2098.\r\n" +
			"3.  Xing M, et al.  BRAF Mutation Testing of Thyroid Fine-Needle Aspiration Biopsy Specimens for Preoperative Risk Stratification in Papillary Thyroid Cancer.  " +
			"J Clin Oncol. 2009 Jun 20; 27(18): 2977-82.";

		public static string MetastaticMelanomaReference = "1. Flaherty KT, Puzanov I, Kim KB, et al. Inhibition of mutated, activated BRAF in metastatic melanoma. N Engl " +
			"J Med 2010; 363:809-19.\r\n" +
			"2. Davies H, Bignell GR, Cox C, et al. Mutations of the BRAF gene in human cancer. Nature 2002;417:949-54.";

		protected string m_Interpretation;
		protected string m_Indication;
        protected string m_IndicationComment;
		protected string m_Comment;
		protected string m_Method;
		protected string m_References;

		public BRAFV600EKResult()
		{
            this.m_Method = "DNA was first extracted from the patient's paraffin-embedded specimen using an automated DNA extraction system.  KRAS allele-specific PCR was then performed on the patient's sample.  " +
                "The products generated from this reaction were then subjected to a second PCR step employing fluorescently-labeled nucleotides and primers designed to detect both mutant and wild-type forms " +
                "of the KRAS gene.  Utilizing the SHIFTED TERMINATION ASSAY (STA), the presence of any of the 12 KRAS point mutations causes termination of complementary DNA chain synthesis during " +
                "amplification. Thus, complementary DNA strands are formed with lengths that are specific to the particular KRAS point mutation present. The products of the STA reaction were analyzed using " +
                "capillary electrophoresis to detect the presence of DNA fragments indicative of a KRAS mutation.  BRAF allele-specific PCR using fluorescently-labeled primers was then performed, which results " +
                "in the amplification of a 107-base fragment of DNA if the BRAF V600E mutation is present.  The products of the allele-specific PCR were then analyzed by capillary electrophoresis for the " +
                "presence of the 107-base DNA fragment indicative of a BRAF V600E mutation.";
		}

        public string Indication
        {
            get { return this.m_Indication; }
        }

        public virtual void SetResults(YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder brafv600ekTestOrder)
        {
            brafv600ekTestOrder.Result = this.m_Result;
            brafv600ekTestOrder.ResultCode = this.m_ResultCode;
            brafv600ekTestOrder.Interpretation = this.m_Interpretation;            
            brafv600ekTestOrder.IndicationComment = this.m_IndicationComment;
            brafv600ekTestOrder.Comment = this.m_Comment;
            brafv600ekTestOrder.Method = this.m_Method;
            brafv600ekTestOrder.References = this.m_References;
        }

		public virtual void FinalizeResults(YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder panelSetOrder, Business.Test.AccessionOrder accessionOrder)
		{
			panelSetOrder.Finish(accessionOrder);
			panelSetOrder.AssignedToId = Business.User.SystemIdentity.Instance.User.UserId;
		}

		public virtual void UnFinalizeResults(YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder panelSetOrder)
		{
			panelSetOrder.AssignedToId = 0;
			panelSetOrder.Unfinalize();
		}

		public virtual void AcceptResults(YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder panelSetOrder, YellowstonePathology.Business.Test.PanelOrder panelToAccept, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{			
			panelSetOrder.Accept();
			panelToAccept.AcceptResults();
		}

		public virtual void UnacceptResults(YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder panelSetOrder)
		{
			YellowstonePathology.Business.Test.PanelOrder acceptedPanelOrder = panelSetOrder.PanelOrderCollection.GetLastAcceptedPanelOrder();
			acceptedPanelOrder.UnacceptResults();
			panelSetOrder.Unaccept();
		}

		public static YellowstonePathology.Business.Rules.MethodResult IsOkToFinal(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder brafv600ekTestOrder)
		{
			YellowstonePathology.Business.Rules.MethodResult result = brafv600ekTestOrder.IsOkToFinalize();
			if (result.Success == true)
			{
				YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest krasStandardReflexTest = new KRASStandardReflex.KRASStandardReflexTest();
				if (accessionOrder.PanelSetOrderCollection.Exists(krasStandardReflexTest.PanelSetId, brafv600ekTestOrder.OrderedOnId, true) == false)
				{
					if (string.IsNullOrEmpty(brafv600ekTestOrder.TumorNucleiPercentage) == true)
					{
						result.Success = false;
						result.Message = "This case cannot be finalized because the Tumor Nuclei Percent is not set.";
					}
				}
				else if (string.IsNullOrEmpty(brafv600ekTestOrder.Result) == true)
				{
					result.Success = false;
					result.Message = "We are unable to finalize this case because the result is blank.";
				}
			}
			return result;
		}

        public static YellowstonePathology.Business.Rules.MethodResult IsOkToUnFinalize(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
		{
			YellowstonePathology.Business.Rules.MethodResult result = panelSetOrder.IsOkToUnfinalize();
			return result;
		}

        public static YellowstonePathology.Business.Rules.MethodResult IsOkToAccept(BRAFV600EKTestOrder testOrder)
		{
			YellowstonePathology.Business.Rules.MethodResult result = testOrder.IsOkToAccept();
			if (result.Success == true)
			{
				if (string.IsNullOrEmpty(testOrder.ResultCode) == true)
				{
					result.Success = false;
					result.Message = "The results cannot be accepted because the Result is not set.";
				}
				else if (string.IsNullOrEmpty(testOrder.Indication) == true)
				{
					result.Success = false;
					result.Message = "The results cannot be accepted because the BRAF indicator is not set.";
				}
			}
			return result;
		}

        public static YellowstonePathology.Business.Rules.MethodResult IsOkToUnaccept(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
		{
			YellowstonePathology.Business.Rules.MethodResult result = panelSetOrder.IsOkToUnaccept();
			return result;
		}

		public static YellowstonePathology.Business.Rules.MethodResult IsOkToSetResult(BRAFV600EKTestOrder panelSetOrder)
		{
			YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
			if (panelSetOrder.Accepted == true)
			{
				result.Success = false;
				result.Message = "The results cannot be set because the results have been accepted.";
			}
			else if(string.IsNullOrEmpty(panelSetOrder.Indication) == true)
			{
				result.Success = false;
				result.Message = "The results cannot be set because the BRAF indication is not set.";
			}
			else if (string.IsNullOrEmpty(panelSetOrder.ResultCode) == true)
			{
				result.Success = false;
				result.Message = "A result must be selected before the results can be set.";
			}
			return result;
		}
	}
}
