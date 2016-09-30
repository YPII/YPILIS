using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class LSEResultStatusMethylationAnalysisRequiredGYN : LSEResultStatus
    {
        public LSEResultStatusMethylationAnalysisRequiredGYN(LSEResult lseResult, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string orderedOnId)
            : base(lseResult, accessionOrder, orderedOnId)
        {
            LSEResult result1 = new LSEResult();
            result1.MLH1Result = LSEResultEnum.Negative;            
            m_LSEResultList.Add(result1);            

            if (this.IsMatch() == true)
            {
                this.m_IsMatch = true;
                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(64, orderedOnId, true) == true || this.m_AccessionOrder.PanelSetOrderCollection.Exists(144, orderedOnId, true))
                {
                    this.m_Status = "Methylation Analysis testing is required and has been ordered.";
                    this.m_IsOrdered = true;
                }
                else
                {
                    this.m_Status = "Methylation Analysis Testing is required and should be ordered.";
                    this.m_IsOrdered = false;
                }                
            }
        }

        public override bool IsMatch()
        {
            bool result = false;
            foreach (LSEResult item in this.m_LSEResultList)
            {
                if (item.MLH1Result == this.m_LSEResult.MLH1Result)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }        
    }
}
