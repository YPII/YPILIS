using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class MultiTestDistributionHandler
    {
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public MultiTestDistributionHandler(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
        }

        public virtual void Set()
        {
            //do nothing
        }
    }
}
