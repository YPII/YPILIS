using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandardReflex
{
    public class KRASStandardReflexKRASOnlyResult : KRASStandardReflexResult
    {
        public KRASStandardReflexKRASOnlyResult(string reportNo, Business.Test.AccessionOrder accessionOrder) : base(reportNo, accessionOrder)
        {
            YellowstonePathology.Business.Test.KRASStandard.KRASStandardTest krasStandardTest = new KRASStandard.KRASStandardTest();
            YellowstonePathology.Business.Test.KRASStandard.KRASStandardTestOrder krasStandardTestOrder = (YellowstonePathology.Business.Test.KRASStandard.KRASStandardTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(krasStandardTest.PanelSetId, this.KRASStandardReflexTestOrder.OrderedOnId, true);
            this.m_KRASStandardResult = krasStandardTestOrder.Result;

            if (krasStandardTestOrder.Final == true)
            {
                this.m_KRASStandardResult = krasStandardTestOrder.Result;                

                YellowstonePathology.Business.Test.KRASStandard.KRASStandardNotDetectedResult notDetectedResult = new KRASStandard.KRASStandardNotDetectedResult();
                YellowstonePathology.Business.Test.KRASStandard.KRASStandardDetectedResult detectedResult = new KRASStandard.KRASStandardDetectedResult();

                if (krasStandardTestOrder.ResultCode == detectedResult.ResultCode)
                {
					this.m_KRASStandardMutationDetected = this.m_KRASStandardTestOrder.MutationDetected;                    
                    this.m_BRAFV600EKResult = KRASStandardReflexResult.NotClinicallyIndicatedResult;
                    this.m_KRASStandardResult = this.m_KRASStandardResult + " - " + this.m_KRASStandardMutationDetected;
                }                
                else
                {
                    this.m_BRAFV600EKResult = KRASStandardReflexResult.NotOrderedResult;
                }
            }
            else
            {
                this.m_KRASStandardResult = KRASStandardReflexResult.PendingResult;
                this.m_BRAFV600EKResult = KRASStandardReflexResult.PendingResult;
            }            
        }        

        public override void SetResults()
        {            
            this.m_KRASStandardReflexTestOrder.Comment = this.m_KRASStandardTestOrder.Comment;
            //this.m_KRASStandardReflexTestOrder.TumorNucleiPercent = this.m_KRASStandardTestOrder.TumorNucleiPercent;
            this.m_KRASStandardReflexTestOrder.Interpretation = this.m_KRASStandardTestOrder.Interpretation;
            this.m_KRASStandardReflexTestOrder.Indication = this.m_KRASStandardTestOrder.Indication;
            this.m_KRASStandardReflexTestOrder.Method = this.m_KRASStandardTestOrder.Method;
            this.m_KRASStandardReflexTestOrder.ReportReferences = this.m_KRASStandardTestOrder.ReportReferences;
            this.m_KRASStandardReflexTestOrder.ReportDisclaimer = this.m_KRASStandardTestOrder.ReportDisclaimer;			
        }
    }
}
