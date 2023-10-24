using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Visitor
{
    public class RemoveSlideOrderVisitor : AccessionTreeVisitor
    {
        private YellowstonePathology.Business.Slide.Model.SlideOrder m_SlideOrder;

        public RemoveSlideOrderVisitor(YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder)
            : base(true, true)
        {
            this.m_SlideOrder = slideOrder;
        }

        public override void Visit(Test.AliquotOrder aliquotOrder)
        {
            if (aliquotOrder.SlideOrderCollection.Exists(this.m_SlideOrder.SlideOrderId) == true)
            {                
                aliquotOrder.SlideOrderCollection.Remove(this.m_SlideOrder.SlideOrderId);
            }
        }

        public override void Visit(YellowstonePathology.Business.Test.Model.TestOrder testOrder)
        {                        
            if (testOrder.SlideOrderCollection.Exists(this.m_SlideOrder.SlideOrderId) == true)
            {
                testOrder.SlideOrderCollection.Remove(this.m_SlideOrder.SlideOrderId);
            }
        }
    }
}
