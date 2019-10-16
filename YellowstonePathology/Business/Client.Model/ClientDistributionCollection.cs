using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Client.Model
{
    public class ClientDistributionCollection : ObservableCollection<ClientDistribution>
    {
        public ClientDistributionCollection()
        { }

        /*public void UpdateDistributionType(Client clientWithChangedDistributionType, string distributionType)
        {
            List<string> pcids = this.GetUniquePhysicianClientIds();
            foreach (string pcId in pcids)
            {
                ClientDistributionCollection distributionsToSet = this.GetMatchingClientDistributions(pcId);
                this.SetDistributionType(clientWithChangedDistributionType, distributionType, distributionsToSet);
            }
        }
        private List<string> GetUniquePhysicianClientIds()
        {
            List<string> result = new List<string>();
            foreach (ClientDistribution clientDistribution in this)
            {
                if (result.Contains(clientDistribution.ClientPhysicianClientId) == false)
                {
                    result.Add(clientDistribution.ClientPhysicianClientId);
                }
                if (result.Contains(clientDistribution.DistributionClientPhysicianClientId) == false)
                {
                    result.Add(clientDistribution.DistributionClientPhysicianClientId);
                }
            }
            return result;
        }

        private ClientDistributionCollection GetMatchingClientDistributions(string pcId)
        {
            ClientDistributionCollection result = new Model.ClientDistributionCollection();
            foreach (ClientDistribution clientDistribution in this)
            {
                if (clientDistribution.ClientPhysicianClientId == pcId)
                {
                    result.Add(clientDistribution);
                }
                if (clientDistribution.DistributionClientPhysicianClientId == pcId)
                {
                    result.Add(clientDistribution);
                }
            }
            return result;
        }*/

        //private void SetDistributionType(Client clientWithChangedDistributionType, string distributionType) //, ClientDistributionCollection distributionsToSet)
        public void UpdateDistributionType(Client clientWithChangedDistributionType, string distributionType, string suggestedAlternativeDistributionType)
        {
            foreach(ClientDistribution clientDistribution in this) //distributionsToSet)
            {
                if(clientDistribution.ClientId == clientWithChangedDistributionType.ClientId)
                {
                    ResetClientDistribution(clientWithChangedDistributionType, distributionType, suggestedAlternativeDistributionType, clientDistribution);
                }
                else
                {
                    ResetDistributionClientDistribution(distributionType, suggestedAlternativeDistributionType, clientDistribution);
                }
            }
        }

        private void ResetClientDistribution(Client clientWithChangedDistributionType, string distributionType, string suggestedAlternativeDistributionType, ClientDistribution clientDistribution)
        {
            YellowstonePathology.Business.ReportDistribution.Model.IncompatibleDistributionTypeCollection incompatibleDistributionTypeCollection = new Business.ReportDistribution.Model.IncompatibleDistributionTypeCollection();
            if (clientWithChangedDistributionType.ClientId == clientDistribution.DistributionClientId)
            {
                clientDistribution.SuggestedDistributionType = distributionType;
            }
            else
            {
                if (incompatibleDistributionTypeCollection.TypesAreIncompatible(distributionType, clientDistribution.DistributionClientDistributionType) == true)
                {
                    clientDistribution.SuggestedDistributionType = clientDistribution.DistributionClientAlternateDistributionType;
                }
                else
                {
                    clientDistribution.SuggestedDistributionType = clientDistribution.DistributionClientDistributionType;
                }
            }
        }

        private void ResetDistributionClientDistribution(string distributionType, string suggestedAlternativeDistributionType, ClientDistribution clientDistribution)
        {
            YellowstonePathology.Business.ReportDistribution.Model.IncompatibleDistributionTypeCollection incompatibleDistributionTypeCollection = new Business.ReportDistribution.Model.IncompatibleDistributionTypeCollection();
            if (incompatibleDistributionTypeCollection.TypesAreIncompatible(clientDistribution.DistributionClientDistributionType, distributionType) == true)
            {
                clientDistribution.SuggestedDistributionType = suggestedAlternativeDistributionType;
            }
            else
            {
                clientDistribution.SuggestedDistributionType = distributionType;
            }
        }
    }
}
