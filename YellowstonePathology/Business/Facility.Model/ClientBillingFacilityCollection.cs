using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class ClientBillingFacilityCollection : ObservableCollection<ClientBillingFacility>
    {
        public ClientBillingFacilityCollection()
        {
            NeogenomicsIrvine neogenomicsIrvine = new NeogenomicsIrvine();
            NeogenomicsFlorida neogenomicsFlorida = new NeogenomicsFlorida();

            YellowstonePathologyInstituteBillings ypii = new YellowstonePathologyInstituteBillings();
            YellowstonePathology.Business.Client.Model.ClientGroupClientCollection clientGroupStVincent = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientGroupClientCollectionByClientGroupId(1);
            YellowstonePathology.Business.Client.Model.ClientGroupClientCollection clientGroupAllClients = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientGroupClientCollection();

            this.Add(new ClientBillingFacility(neogenomicsIrvine, ypii, clientGroupStVincent, "Technical"));
            this.Add(new ClientBillingFacility(neogenomicsFlorida, ypii, clientGroupStVincent, "Technical"));

            this.Add(new ClientBillingFacility(neogenomicsIrvine, neogenomicsIrvine, clientGroupAllClients, "Professional"));
            this.Add(new ClientBillingFacility(neogenomicsFlorida, neogenomicsFlorida, clientGroupAllClients, "Professional"));
            
            this.Add(new ClientBillingFacility(ypii, ypii, clientGroupAllClients, "Technical"));
            this.Add(new ClientBillingFacility(ypii, ypii, clientGroupAllClients, "Professional"));
        }

        public YellowstonePathology.Business.Rules.MethodResult FindMatch(string performingFacilityId, string billingFacilityId, int clientId, string facilityComponent)
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            foreach (ClientBillingFacility clientBillingFacility in this)
            {
                if (clientBillingFacility.IsMatch(performingFacilityId, clientId, facilityComponent) == true)
                {
                    if (clientBillingFacility.DefaultBillingFacility.FacilityId == billingFacilityId)
                    {
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "The default billing facility for the " + facilityComponent + " component is " + clientBillingFacility.DefaultBillingFacility.FacilityName;
                    }
                }
            }
            return result;
        }
    }
}
