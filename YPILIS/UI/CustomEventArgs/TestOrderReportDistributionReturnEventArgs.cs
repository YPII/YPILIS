using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class TestOrderReportDistributionReturnEventArgs : System.EventArgs
    {
        YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution m_TestOrderReportDistribution;

        public TestOrderReportDistributionReturnEventArgs(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution)
        {
            this.m_TestOrderReportDistribution = testOrderReportDistribution;
        }

        public YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution TestOrderReportDistribution
        {
            get { return this.m_TestOrderReportDistribution; }
        }
    }
}
