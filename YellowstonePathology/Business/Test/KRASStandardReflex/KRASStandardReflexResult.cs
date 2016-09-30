using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandardReflex
{
	public class KRASStandardReflexResult
	{

        public static string PendingResult = "Pending";
        public static string NotOrderedResult = "Not Ordered";
        public static string NotClinicallyIndicatedResult = "Not Clinically Indicated";

        protected KRASStandardReflexTest m_KRASStandardReflexTest;
		protected KRASStandardReflexTestOrder m_KRASStandardReflexTestOrder;

        protected KRASStandard.KRASStandardTest m_KRASStandardTest;
		protected YellowstonePathology.Business.Test.KRASStandard.KRASStandardTestOrder m_KRASStandardTestOrder;

        protected YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest m_BRAFV600EKTest;
		protected YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder m_BRAFV600EKTestOrder;
		
		protected string m_Interpretation;
        protected string m_Comment;
		protected string m_Method;
		protected string m_References;
        protected string m_ReportDisclaimer;        

        protected string m_BRAFV600EKResult;
        protected string m_KRASStandardResult;
        protected string m_KRASStandardMutationDetected;        

        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public KRASStandardReflexResult(string reportNo, Business.Test.AccessionOrder accessionOrder)
		{
            this.m_AccessionOrder = accessionOrder;

            this.m_KRASStandardReflexTest = new KRASStandardReflexTest();
            this.m_KRASStandardReflexTestOrder = (YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);

            this.m_KRASStandardTest = new KRASStandard.KRASStandardTest();
            this.m_KRASStandardTestOrder = (YellowstonePathology.Business.Test.KRASStandard.KRASStandardTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_KRASStandardTest.PanelSetId, this.m_KRASStandardReflexTestOrder.OrderedOnId, true);

            this.m_BRAFV600EKTest = new BRAFV600EK.BRAFV600EKTest();
            this.m_BRAFV600EKTestOrder = (YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_BRAFV600EKTest.PanelSetId, this.m_KRASStandardReflexTestOrder.OrderedOn, true);

            this.m_References = "1. Amado RG, Wolf M, Peeters M, et al. Wild-type KRAS is required for panitumumab efficacy in patients with metastatic colorectal cancer. J Clin Oncol. 2008; 26(10): 1626-1634.  " +
                "2. Benvenuti S, Sartore-Bianchi A, Di Nicolantonio F, et al. Oncogenic activation of the RAS/RAF signaling pathway impairs the response of metastatic colorectal cancers to anti-epidermal growth factor receptor antibody therapies. Cancer Res. 2007; 67(6): 2643-2648.";            
		}

        public bool DoesBRAFV600EKExist()
        {
            bool result = false;
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(this.m_BRAFV600EKTest.PanelSetId) == true)
            {
                result = true;
            }
            return result;
        }

        public string KRASStandardResult
        {
            get { return this.m_KRASStandardResult; }
        }

        public string KRASStandardMutationDetected
        {
            get { return this.m_KRASStandardMutationDetected; }
        }        

        public string BRAFV600EKResult
        {
            get { return this.m_BRAFV600EKResult; }
        }

        public YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTestOrder KRASStandardReflexTestOrder
        {
            get { return this.m_KRASStandardReflexTestOrder; }
        }

        public YellowstonePathology.Business.Test.KRASStandard.KRASStandardTestOrder KRASStandardTestOrder
        {
            get { return this.m_KRASStandardTestOrder; }
        }

        public YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder BRAFV600EKTestOrder
        {
            get { return this.m_BRAFV600EKTestOrder; }
        }

        public virtual void SetResults()
        {            
            
        }							

        public virtual void FinalizeResults(KRASStandardReflexTestOrder testOrder)
		{
			testOrder.Finish(this.m_AccessionOrder);
		}

        public virtual void UnFinalizeResults(KRASStandardReflexTestOrder testOrder)
		{
			testOrder.Unfinalize();
		}

        public static YellowstonePathology.Business.Rules.MethodResult IsOkToFinal(KRASStandardReflexResult krasStandardReflexResult)
		{
            YellowstonePathology.Business.Rules.MethodResult result = new YellowstonePathology.Business.Rules.MethodResult();
            YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings ypi = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            if (krasStandardReflexResult.KRASStandardReflexTestOrder.Final == false)
			{
                if (krasStandardReflexResult.KRASStandardTestOrder.Final == false)
				{
						result.Success = false;
						result.Message = "This case cannot be finalized because the KRAS Standard Test Order is not final.";
				}
                else if (krasStandardReflexResult.BRAFV600EKTestOrder != null && krasStandardReflexResult.BRAFV600EKTestOrder.Final == false)
				{
					result.Success = false;
					result.Message = "This case cannot be finalized because it the BRAF V600E/K Test Order is not final.";
				}
                else if (krasStandardReflexResult.KRASStandardReflexTestOrder.TechnicalComponentBillingFacilityId == ypi.FacilityId)
                {
                    if (string.IsNullOrEmpty(krasStandardReflexResult.KRASStandardReflexTestOrder.TumorNucleiPercentage) == true)
                    {
                        result.Success = false;
                        result.Message = "This case cannot be finalized because the Tumor Nuclei Percent is not set.";
                    }
                }
			}
			else
			{
				result.Success = false;
				result.Message = "This case cannot be finalized because it is already finalized.";
			}            
			return result;
		}

        public static YellowstonePathology.Business.Rules.MethodResult IsOkToUnFinalize(KRASStandardReflexResult krasStandardReflexResult)
		{
			YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();            
            if (krasStandardReflexResult.KRASStandardTestOrder.Final == false)
			{
				result.Success = false;
				result.Message = "This case cannot be unfinalized because it is not final.";
			}            
			return result;
		}

        public static YellowstonePathology.Business.Rules.MethodResult IsOkToOrderBRAF(KRASStandardReflexResult krasStandardReflexResult)
		{            
			YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();            
            if (krasStandardReflexResult.DoesBRAFV600EKExist() == true)
			{
				result.Success = false;
				result.Message = "A BRAF V600E/K cannot be ordered because one already exists.";
			}
            else if (string.IsNullOrEmpty(krasStandardReflexResult.KRASStandardResult) == true)
			{
				result.Success = false;
				result.Message = "A BRAF V600E/K cannot be ordered because the KRAS result is not set.";
			}
			else
			{
				YellowstonePathology.Business.Test.KRASStandard.KRASStandardResultCollection resultCollection = YellowstonePathology.Business.Test.KRASStandard.KRASStandardResultCollection.GetAll();
                YellowstonePathology.Business.Test.KRASStandard.KRASStandardResult standardResult = resultCollection.GetResult(krasStandardReflexResult.KRASStandardTestOrder.ResultCode);
				if (standardResult is YellowstonePathology.Business.Test.KRASStandard.KRASStandardDetectedResult)
				{
					result.Success = false;
					result.Message = "A BRAF V600E/K cannot be ordered because the KRAS result shows a mutation has been detected.";
				}
			}           
			return result;
		}
	}
}
