using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Visitor
{
    public class GetTestOrderVisitor : AccessionTreeVisitor
    {
        private YellowstonePathology.Business.Test.Model.Test m_Test;
        private YellowstonePathology.Business.Test.Model.TestOrder m_TestOrder;
        private bool m_TestOrderFound;        

        public GetTestOrderVisitor(YellowstonePathology.Business.Test.Model.Test test) 
            : base(true, false)
        {
            this.m_Test = test;
            this.m_TestOrderFound = false;            
        }

        public override void Visit(YellowstonePathology.Business.Test.Model.TestOrderCollection testOrderCollection)
        {
            if (testOrderCollection.Exists(this.m_Test.TestId) == true)
            {
                this.m_TestOrder = testOrderCollection.GetTestOrder(this.m_Test.TestId);
            }
        }

        public YellowstonePathology.Business.Test.Model.TestOrder TestOrder
        {
            get { return this.m_TestOrder; }
        }

        public bool TestOrderFound
        {
            get { return this.m_TestOrderFound; }
        }
    }
}
