using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class LSEResultStatusBRAFRequired : LSEResultStatus
    {        

        public LSEResultStatusBRAFRequired(LSEResult lseResult, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string orderedOnId) 
            : base(lseResult, accessionOrder, orderedOnId)
        {
            LSEResult result1 = new LSEResult();
            result1.MLH1Result = LSEResultEnum.Negative;
            result1.MSH2Result = LSEResultEnum.Positive;
            result1.MSH6Result = LSEResultEnum.Positive;
            result1.PMS2Result = LSEResultEnum.Negative;
            m_LSEResultList.Add(result1);

            LSEResult result2 = new LSEResult();
            result2.MLH1Result = LSEResultEnum.Negative;
            result2.MSH2Result = LSEResultEnum.Positive;
            result2.MSH6Result = LSEResultEnum.Positive;
            result2.PMS2Result = LSEResultEnum.Positive;
            m_LSEResultList.Add(result2);            

            if (this.IsMatch() == true)
            {
                this.m_IsMatch = true;
                if (accessionOrder.PanelSetOrderCollection.Exists(18, orderedOnId, true) ||
                    accessionOrder.PanelSetOrderCollection.Exists(30, orderedOnId, true) == true)
                {
                    YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(30, orderedOnId, true);
                    if (panelSetOrder == null) panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(18, orderedOnId, true);

                    if (panelSetOrder.Final == true)
                    {
                        this.m_IsOrdered = true;
                        this.m_Status = "LSE Complete.";
                    }
                    else
                    {
                        this.m_IsOrdered = true;
                        this.m_Status = "BRAF testing is required and has been ordered.";
                    }
                }
                else
                {
                    this.m_IsOrdered = false;
                    this.m_Status = "BRAF Testing is required and should be ordered.";
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
                    item.PMS2Result == this.m_LSEResult.PMS2Result)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }        
    }
}
