using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Client.Model
{
    public class ClientDistributionCollection : ObservableCollection<ClientDistribution>
    {
        YellowstonePathology.Business.ReportDistribution.Model.IncompatibleDistributionTypeCollection m_IncompatibleDistributionTypeCollection;

        public ClientDistributionCollection()
        { }

        public void UpdateDistributionType(Client clientWithChangedDistributionType, string distributionType, string suggestedAlternativeDistributionType)
        {
           this.m_IncompatibleDistributionTypeCollection = new Business.ReportDistribution.Model.IncompatibleDistributionTypeCollection();
            List<string> pcids = this.GetUniquePhysicianClientIds();
            foreach (string pcId in pcids)
            {
                ClientDistributionCollection distributionsToSet = this.GetMatchingClientDistributions(pcId);
                this.SetDistributionType(clientWithChangedDistributionType, distributionType, suggestedAlternativeDistributionType, distributionsToSet);
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
        }

        private void SetDistributionType(Client clientWithChangedDistributionType, string distributionType, string suggestedAlternativeDistributionType, ClientDistributionCollection distributionsToSet)
        {
            foreach (ClientDistribution clientDistribution in distributionsToSet)
            {
                if(clientDistribution.ClientId == clientWithChangedDistributionType.ClientId)
                {
                    if(clientDistribution.ClientPhysicianClientId == clientDistribution.DistributionClientPhysicianClientId) // same client same provider
                    {
                        clientDistribution.SuggestedDistributionType = distributionType;
                    }
                    else
                    {
                        if(clientDistribution.ClientId == clientDistribution.DistributionClientId) //same client different provider
                        {
                            clientDistribution.SuggestedDistributionType = this.IsEmptySuggestedAlternativeDistributionType(distributionType, suggestedAlternativeDistributionType);
                        }
                        else //distributed to a different client
                        {
                            SetDistributionToDifferentClient(clientDistribution, distributionType, suggestedAlternativeDistributionType);
                        }
                    }
                }
                else //distributed from a different client
                {
                    clientDistribution.SuggestedDistributionType = this.IsEmptySuggestedAlternativeDistributionType(distributionType, suggestedAlternativeDistributionType);
                }
            }
        }

        private void SetDistributionToDifferentClient(ClientDistribution clientDistribution, string distributionType, string suggestedAlternativeDistributionType)
        {
            if (this.m_IncompatibleDistributionTypeCollection.TypesAreIncompatible(distributionType, clientDistribution.DistributionClientDistributionType) == true)
            {
                clientDistribution.SuggestedDistributionType = clientDistribution.DistributionClientAlternateDistributionType;
            }
            else
            {
                if (clientDistribution.DistributionClientDistributionType == distributionType &&
                    (distributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPIC ||
                    distributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPICANDFAX ||
                    distributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ECW ||
                    distributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ATHENA ||
                    distributionType == YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MEDITECH))
                {
                    clientDistribution.SuggestedDistributionType = this.IsEmptySuggestedAlternativeDistributionType(distributionType, suggestedAlternativeDistributionType);
                }
                else
                {
                    clientDistribution.SuggestedDistributionType = clientDistribution.DistributionClientDistributionType;
                }
            }
        }

        private string IsEmptySuggestedAlternativeDistributionType(string suggestedDistributionType, string suggestedAlternativeDistributionType)
        {
            string result = suggestedAlternativeDistributionType;
            if (string.IsNullOrEmpty(suggestedAlternativeDistributionType) == true)
            {
                result = suggestedDistributionType;
            }
            return result;
        }

        public void SetDistributions()
        {
            foreach(ClientDistribution clientDistribution in this)
            {
                if(string.IsNullOrEmpty(clientDistribution.SuggestedDistributionType) == false)
                {
                    if(clientDistribution.SuggestedDistributionType != clientDistribution.ClientDistributionType)
                    {
                        YellowstonePathology.Business.Client.Model.PhysicianClientDistribution physicianClientDistribution = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullPhysicianClientDistribution(clientDistribution.PhysicianClientDistribution.PhysicianClientDistributionID, this);
                        physicianClientDistribution.DistributionType = clientDistribution.SuggestedDistributionType;
                        YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
                        clientDistribution.PhysicianClientDistribution.DistributionType = clientDistribution.SuggestedDistributionType;
                    }
                }
            }
        }
    }
}
