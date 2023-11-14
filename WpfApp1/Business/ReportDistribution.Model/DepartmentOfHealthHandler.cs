using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class DepartmentOfHealthHandler
    {
        public static void HandleDistribution(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder)
        {
			Business.Client.Model.Client client = Business.Gateway.PhysicianClientGateway.GetClientByClientId(accessionOrder.ClientId);
			string testOrderReportDistributionId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

			Business.Client.Model.Client distClient = null;
			Business.Domain.Physician distPhysician = Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(2678);

			switch (client.State.ToUpper())
			{
				case "MT":
					distClient = Business.Gateway.PhysicianClientGateway.GetClientByClientId(1337);					
					break;
				case "WY":
					distClient = Business.Gateway.PhysicianClientGateway.GetClientByClientId(1335);
					break;
				case "ID":
					distClient = Business.Gateway.PhysicianClientGateway.GetClientByClientId(1730);
					break;
			}

			if(panelSetOrder.TestOrderReportDistributionCollection.Exists(distPhysician.PhysicianId, distClient.ClientId, distClient.DistributionType) == false)
			{
				panelSetOrder.TestOrderReportDistributionCollection.AddNext(testOrderReportDistributionId, testOrderReportDistributionId,
				panelSetOrder.ReportNo, distPhysician.PhysicianId, distPhysician.FullName, distClient.ClientId,
				distClient.ClientName, distClient.DistributionType, distClient.Fax);
			}			
		}
    }
}
