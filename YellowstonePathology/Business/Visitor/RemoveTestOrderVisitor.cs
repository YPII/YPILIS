using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Visitor
{
    public class RemoveTestOrderVisitor : AccessionTreeVisitor
    {
        private string m_TestOrderId;
        private YellowstonePathology.Business.Test.PanelOrder m_PanelOrder;

        public RemoveTestOrderVisitor(string testOrderId) 
            : base(true, true)
        {
            this.m_TestOrderId = testOrderId;
        }        
        
        public override void Visit(Test.PanelSetOrder panelSetOrder)
        {
            foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
            {
                if (panelOrder.TestOrderCollection.Exists(this.m_TestOrderId) == true)
                {
                    this.m_PanelOrder = panelOrder;
                    this.m_PanelOrder.TestOrderCollection.Remove(this.m_TestOrderId);
                    if (this.m_PanelOrder.TestOrderCollection.Count == 0)
                    {
                        panelSetOrder.PanelOrderCollection.Remove(this.m_PanelOrder);
                    }
                    break;
                }
            }
        }

        public override void Visit(Test.AliquotOrder aliquotOrder)
        {
            if (aliquotOrder.TestOrderCollection.Exists(this.m_TestOrderId) == true)
            {
                aliquotOrder.TestOrderCollection.Remove(this.m_TestOrderId);
            }
        }

        public override void Visit(YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen)
        {
            if (surgicalSpecimen.StainResultItemCollection.TestOrderExists(this.m_TestOrderId) == true)
            {
                YellowstonePathology.Business.SpecialStain.StainResultItem stainResult = surgicalSpecimen.StainResultItemCollection.GetStainResult(this.m_TestOrderId);
                surgicalSpecimen.StainResultItemCollection.Remove(stainResult);
            }

            if (surgicalSpecimen.IntraoperativeConsultationResultCollection.TestOrderIdExists(this.m_TestOrderId) == true)
            {
                YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult icResult = surgicalSpecimen.IntraoperativeConsultationResultCollection.GetIntraoperativeConsultationResult(this.m_TestOrderId);
                surgicalSpecimen.IntraoperativeConsultationResultCollection.Remove(icResult);
            }
        }        
    }
}
