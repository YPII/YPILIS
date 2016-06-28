using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Visitor
{
    public class OrderTestAuditVisitor : AccessionTreeVisitor
    {
        private YellowstonePathology.Business.Test.Model.TestOrder m_TestOrder;
        private YellowstonePathology.Business.Test.AliquotOrder m_AliquotOrder;

        private bool m_TestOrderHasBeenAddedToRightSide;
        private bool m_TestOrderHasBeenAddedToLeftSide;
        private bool m_TestOrderIsInUnacknowledgedPanelOrder;        
        private bool m_StainResultHasBeenAdded;
        private bool m_ICHasBeenAdded;

        public OrderTestAuditVisitor(YellowstonePathology.Business.Test.Model.TestOrder testOrder, YellowstonePathology.Business.Test.AliquotOrder aliquotOrder)
            : base(true, true)
        {
            this.m_TestOrder = testOrder;
            this.m_AliquotOrder = aliquotOrder;

            this.m_TestOrderHasBeenAddedToRightSide = false;
            this.m_TestOrderHasBeenAddedToLeftSide = false;
            this.m_TestOrderIsInUnacknowledgedPanelOrder = false;
            this.m_StainResultHasBeenAdded = false;
            this.m_ICHasBeenAdded = false;            

            this.HandleItemsWhereVisitingIsNotNecessary();
        }

        private void HandleItemsWhereVisitingIsNotNecessary()
        {            
            if (this.m_AliquotOrder.TestOrderCollection.Exists(this.m_TestOrder.TestOrderId) == true)
            {
                this.m_TestOrderHasBeenAddedToLeftSide = true;
            }            
        }

        public override void Visit(Test.PanelOrderCollection panelOrderCollection)
        {
            foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in panelOrderCollection)
            {
                if (panelOrder.TestOrderCollection.Exists(this.m_TestOrder.TestOrderId) == true)
                {
                    this.m_TestOrderHasBeenAddedToRightSide = true;
                    if (panelOrder.Acknowledged == false)
                    {
                        this.m_TestOrderIsInUnacknowledgedPanelOrder = true;
                    }                                        
                }
            }
        }

        public override void Visit(YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen)
        {
            if (surgicalSpecimen.StainResultItemCollection.TestOrderExists(this.m_TestOrder.TestOrderId) == true)
            {
                this.m_StainResultHasBeenAdded = true;
            }
            if (surgicalSpecimen.IntraoperativeConsultationResultCollection.TestOrderIdExists(this.m_TestOrder.TestOrderId) == true)
            {
                this.m_ICHasBeenAdded = true;
            }
        }        

        public bool TestOrderHasBeenAddedToRightSide
        {
            get { return this.m_TestOrderHasBeenAddedToRightSide; }
        }

        public bool TestOrderHasBeenAddedToLeftSide
        {
            get { return this.m_TestOrderHasBeenAddedToLeftSide; }
        }

        public bool TestOrderIsInUnacknowledgePanelOrder
        {
            get { return this.m_TestOrderIsInUnacknowledgedPanelOrder; }
        }        

        public bool StainResultHasBeenAdded
        {
            get { return this.m_StainResultHasBeenAdded; }
        }

        public bool ICHasBeenAdded
        {
            get { return this.m_ICHasBeenAdded; }
        }
    }
}
