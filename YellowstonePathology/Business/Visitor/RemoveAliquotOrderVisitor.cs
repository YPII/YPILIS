using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Visitor
{
    public class RemoveAliquotOrderVisitor : AccessionTreeVisitor
    {
        private YellowstonePathology.Business.Test.AliquotOrder m_AliquotOrder;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public RemoveAliquotOrderVisitor(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder)
            : base(true, true)
        {
            this.m_AliquotOrder = aliquotOrder;            
        }

        public override void Visit(Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
        }

		public override void Visit(Specimen.Model.SpecimenOrder specimenOrder)
        {
            if (specimenOrder.AliquotOrderCollection.Exists(this.m_AliquotOrder.AliquotOrderId) == true)
            {
                while (this.m_AliquotOrder.TestOrderCollection.Count != 0)
                {
                    RemoveTestOrderVisitor removeTestOrderVisitor = new RemoveTestOrderVisitor(m_AliquotOrder.TestOrderCollection[0].TestOrderId);
                    this.m_AccessionOrder.TakeATrip(removeTestOrderVisitor);
                }
                specimenOrder.AliquotOrderCollection.Remove(this.m_AliquotOrder);
            }
        }
    }
}
