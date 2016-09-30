using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Visitor
{
    public class RemoveTestOrderAuditVisitor : AccessionTreeVisitor
    {
        private YellowstonePathology.Business.Test.Model.TestOrder m_TestOrder;
        private YellowstonePathology.Business.Test.PanelOrder m_PanelOrder;

        private bool m_TestOrderRightSideIsRemoved;
        private bool m_TestOrderLeftSideIsRemoved;        
        private bool m_PanelOrderIsRemoved;
        private bool m_StainResultIsRemoved;
        private bool m_ICIsRemoved;

        public RemoveTestOrderAuditVisitor(YellowstonePathology.Business.Test.Model.TestOrder testOrder, YellowstonePathology.Business.Test.PanelOrder panelOrder)
            : base(true, true)
        {
            this.m_TestOrder = testOrder;
            this.m_PanelOrder = panelOrder;

            this.m_TestOrderLeftSideIsRemoved = true;
            this.m_TestOrderRightSideIsRemoved = true;            
            this.m_PanelOrderIsRemoved = true;
            this.m_StainResultIsRemoved = true;
            this.m_ICIsRemoved = true;
        }

        public override void Visit(Test.PanelSetOrder panelSetOrder)
        {
            if (panelSetOrder.PanelOrderCollection.Exists(this.m_PanelOrder.PanelOrderId) == true)
            {
                this.m_PanelOrderIsRemoved = false;
            }
        }

        public override void Visit(Test.PanelOrder panelOrder)
        {
            if (panelOrder.TestOrderCollection.Exists(this.m_TestOrder.TestOrderId) == true)
            {
                this.m_TestOrderRightSideIsRemoved = false;
            }
        }        

        public override void Visit(Test.AliquotOrder aliquotOrder)
        {
            if (aliquotOrder.TestOrderCollection.Exists(this.m_TestOrder.TestOrderId) == true)
            {
                this.m_TestOrderLeftSideIsRemoved = false;
            }
        }

        public override void Visit(YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen)
        {
            if (surgicalSpecimen.StainResultItemCollection.TestOrderExists(this.m_TestOrder.TestOrderId) == true)
            {
                this.m_StainResultIsRemoved = false;
            }

            if (surgicalSpecimen.IntraoperativeConsultationResultCollection.TestOrderIdExists(this.m_TestOrder.TestOrderId) == true)
            {
                this.m_ICIsRemoved = false;
            }
        }

        public bool TestOrderRightSideIsRemoved
        {
            get { return this.m_TestOrderRightSideIsRemoved; }
        }

        public bool TestOrderLeftSideIsRemoved
        {
            get { return this.m_TestOrderLeftSideIsRemoved; }
        }        

        public bool PanelOrderIsRemoved
        {
            get { return this.m_PanelOrderIsRemoved; }
        }

        public bool StainResultIsRemoved
        {
            get { return this.m_StainResultIsRemoved; }
        }

        public bool ICIsRemoved
        {
            get { return this.m_ICIsRemoved; }
        }
    }
}
