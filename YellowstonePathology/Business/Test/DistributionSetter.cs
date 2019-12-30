using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test
{
    public class DistributionSetter
    {
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private int m_PhysicianId;
        private string m_PhysicianName;
        private int m_ClientId;
        private string m_ClientName;
        private string m_DistributionTypeToCheck;
        private string m_FaxNumber;

        public DistributionSetter(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, int physicianId, string physicianName, int clientId, string clientName, string distributionType, string faxNumber)
        {
            this.m_PanelSetOrder = panelSetOrder;
            this.m_PhysicianId = physicianId;
            this.m_PhysicianName = physicianName;
            this.m_ClientId = clientId;
            this.m_ClientName = clientName;
            this.m_DistributionTypeToCheck = distributionType;
            this.m_FaxNumber = faxNumber;
        }

        public ReportDistribution.Model.TestOrderReportDistribution GetDistributionResult()
        {
            ReportDistribution.Model.TestOrderReportDistribution result = null;
            Business.PanelSet.Model.PanelSet panelSet = Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(this.m_PanelSetOrder.PanelSetId);
            CanSetDistributionResult canSetDistributionResult = new CanSetDistributionResult(panelSet, this.m_DistributionTypeToCheck);
            ResultType.GetImplementedDistributionType(canSetDistributionResult);


            return result;
        }
    }
}
