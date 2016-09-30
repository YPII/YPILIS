using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class IsOkToAddToDistributionAudit : AccessionOrderAudit
    {
        private YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution m_TestOrderReportDistribution;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;

        public IsOkToAddToDistributionAudit(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder) 
            : base(accessionOrder)
        {
            this.m_TestOrderReportDistribution = testOrderReportDistribution;
            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_TestOrderReportDistribution.ReportNo);
        }

        public override void Run()
        {
            if (this.m_TestOrderReportDistribution.DistributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPIC ||
                this.m_TestOrderReportDistribution.DistributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPICANDFAX)
            {
                if (this.m_PanelSetOrder.TestOrderReportDistributionCollection.HasDistribution(YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPIC) == true ||
                    this.m_PanelSetOrder.TestOrderReportDistributionCollection.HasDistribution(YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPICANDFAX) == true)
                {
                    this.m_ActionRequired = true;
                    this.m_Message.AppendLine("This distribution cannot be added because an EPIC distribution already exists.");
                }
            }

            if (this.m_TestOrderReportDistribution.DistributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MEDITECH)
            {
                if (this.m_PanelSetOrder.TestOrderReportDistributionCollection.HasDistribution(YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MEDITECH) == true)
                {
                    this.m_ActionRequired = true;
                    this.m_Message.AppendLine("This distribution cannot be added because an MEDITECH distribution already exists.");
                }
            }

            if (this.m_TestOrderReportDistribution.DistributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ECW)
            {
                if (this.m_PanelSetOrder.TestOrderReportDistributionCollection.HasDistribution(YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ECW) == true)
                {
                    this.m_ActionRequired = true;
                    this.m_Message.AppendLine("This distribution cannot be added because an ECW distribution already exists.");
                }
            }

            if (this.m_TestOrderReportDistribution.DistributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ATHENA)
            {
                if (this.m_PanelSetOrder.TestOrderReportDistributionCollection.HasDistribution(YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ATHENA) == true)
                {
                    this.m_ActionRequired = true;
                    this.m_Message.AppendLine("This distribution cannot be added because an ATHENA distribution already exists.");
                }
            }
        }
    }
}
