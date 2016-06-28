using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class LSEResultStatusMethylationAnalysisRequired : LSEResultStatus
    {
        public LSEResultStatusMethylationAnalysisRequired(LSEResult lseResult, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string orderedOnId)
            : base(lseResult, accessionOrder, orderedOnId)
        {
            LSEResult result1 = new LSEResult();
            result1.MLH1Result = LSEResultEnum.Negative;
            result1.MSH2Result = LSEResultEnum.Positive;
            result1.MSH6Result = LSEResultEnum.Positive;
            result1.PMS2Result = LSEResultEnum.Negative;
            result1.BrafResult = LSEResultEnum.Negative;
            this.m_LSEResultList.Add(result1);

            LSEResult result2 = new LSEResult();
            result2.MLH1Result = LSEResultEnum.Negative;
            result2.MSH2Result = LSEResultEnum.Positive;
            result2.MSH6Result = LSEResultEnum.Positive;
            result2.MSH6Result = LSEResultEnum.Positive;
            result2.BrafResult = LSEResultEnum.Negative;
            this.m_LSEResultList.Add(result2);            

            if (this.IsMatch() == true)
            {
                this.m_IsMatch = true;
                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(144, orderedOnId, true) == true || this.m_AccessionOrder.PanelSetOrderCollection.Exists(64, orderedOnId, true) == true)
                {
                    YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(144, orderedOnId, true);
                    if (panelSetOrder == null) panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(64, orderedOnId, true);

                    if (panelSetOrder.Final == true)
                    {
                        this.m_Status = "LSE is complete.";
                        this.m_IsOrdered = true;
                    }
                    else
                    {
                        this.m_Status = "Methylation Analysis testing is required and has been ordered.";
                        this.m_IsOrdered = true;
                    }                    
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
                if (item.MLH1Result == this.m_LSEResult.MLH1Result &&
                    item.MSH2Result == this.m_LSEResult.MSH2Result &&
                    item.MSH6Result == this.m_LSEResult.MSH6Result &&
                    item.PMS2Result == this.m_LSEResult.PMS2Result &&
                    item.BrafResult == this.m_LSEResult.BrafResult)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }        
    }
}
