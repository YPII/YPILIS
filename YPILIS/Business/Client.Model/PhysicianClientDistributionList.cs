using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Client.Model
{
    public class PhysicianClientDistributionList : ObservableCollection<PhysicianClientDistributionListItem>
    {
        public PhysicianClientDistributionList()
        {

        }

        public void SetDistribution(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.HandleReferringProvider(accessionOrder);
            this.HandlePathGroup(accessionOrder);
            this.HandlePAIF(accessionOrder);
            this.HandleSurgeonDistribution(accessionOrder);
            
            if(this.Count > 0)
            {
                this.HandleWebDistribution(accessionOrder);
            }

            foreach (PhysicianClientDistributionListItem physicianClientDistribution in this)
            {
                physicianClientDistribution.SetDistribution(panelSetOrder, accessionOrder);
            }            
        }        

        public void HandleSurgeonDistribution(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            if(accessionOrder.PanelSetOrderCollection.DoesPanelSetExist(400) == true)
            {
                if(string.IsNullOrEmpty(accessionOrder.ReportCopyTo) == false)
                {
                    string[] commaSplit = accessionOrder.ReportCopyTo.Split(',');
                    if(commaSplit.Length == 2)
                    {
                        int clientId = Convert.ToInt32(commaSplit[0]);
                        int physicianId = Convert.ToInt32(commaSplit[1]);
                        Domain.Physician physician = Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(physicianId);
                        Client client = Business.Gateway.PhysicianClientGateway.GetClientByClientId(clientId);

                        PhysicianClientDistributionListItem physicianClientDistribution = Business.Client.Model.PhysicianClientDistributionFactory.GetPhysicianClientDistribution("Web Service");
                        physicianClientDistribution.ClientId = client.ClientId;
                        physicianClientDistribution.ClientName = client.ClientName;
                        physicianClientDistribution.PhysicianId = physician.PhysicianId;
                        physicianClientDistribution.PhysicianName = physician.DisplayName;
                        physicianClientDistribution.DistributionType = client.DistributionType;
                        physicianClientDistribution.FaxNumber = client.Fax;
                        this.Add(physicianClientDistribution);
                    }
                }
            }
        }

        public void HandleWebDistribution(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {            
            if(this.DoesWebServiceDistributionExist() == false)
            {
                PhysicianClientDistributionListItem physicianClientDistribution = Business.Client.Model.PhysicianClientDistributionFactory.GetPhysicianClientDistribution("Web Service");
                physicianClientDistribution.ClientId = accessionOrder.ClientId;
                physicianClientDistribution.ClientName = accessionOrder.ClientName;
                physicianClientDistribution.PhysicianId = accessionOrder.PhysicianId;
                physicianClientDistribution.PhysicianName = accessionOrder.PhysicianName;
                physicianClientDistribution.DistributionType = "Web Service";                    
                this.Add(physicianClientDistribution);                    
            }            
        }

        public void HandlePAIF(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            List<int> paifPathIds = new List<int>();
            paifPathIds.Add(4315); //Haws
            paifPathIds.Add(3333); //Daines
            paifPathIds.Add(3563); //Ririe
            paifPathIds.Add(2731); //Teerman
            paifPathIds.Add(910); //Tannenbaum

            if(paifPathIds.Contains(accessionOrder.PhysicianId) == true)
            { 
                if(this.DoesStaffPathologistExist() == true)
                {
                    foreach (PhysicianClientDistributionListItem item in this)
                    {
                        if (item.PhysicianId == 728 && item.DistributionType != "Fax")
                        {
                            item.DistributionType = "Fax";
                        }
                    }
                }
                else
                {
                    int paifClientId = 1201;
                    Business.Client.Model.Client client = Business.Gateway.PhysicianClientGateway.GetClientByClientId(paifClientId);
                    PhysicianClientDistributionListItem physicianClientDistribution = Business.Client.Model.PhysicianClientDistributionFactory.GetPhysicianClientDistribution(client.DistributionType);
                    physicianClientDistribution.ClientId = client.ClientId;
                    physicianClientDistribution.ClientName = client.ClientName;
                    physicianClientDistribution.PhysicianId = 728;
                    physicianClientDistribution.PhysicianName = "Staff Pathologist";
                    physicianClientDistribution.DistributionType = "Fax";
                    physicianClientDistribution.FaxNumber = client.Fax;
                    this.Add(physicianClientDistribution);
                }                                                         
            }
        }

        public void HandlePathGroup(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            Business.Client.Model.Client client = Business.Gateway.PhysicianClientGateway.GetClientByClientId(accessionOrder.ClientId);            

            if(client.PathologyGroupId != "YPBLGS")
            {
                if (IsPAOIFWH(accessionOrder, client) == true) return;

                Business.Facility.Model.Facility pathFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(client.PathologyGroupId);
                Business.Client.Model.Client pathClient = Business.Gateway.PhysicianClientGateway.GetClientByClientId(pathFacility.ClientId);

                PhysicianClientDistributionListItem physicianClientDistribution = Business.Client.Model.PhysicianClientDistributionFactory.GetPhysicianClientDistribution(client.DistributionType);
                physicianClientDistribution.ClientId = pathClient.ClientId;
                physicianClientDistribution.ClientName = pathClient.ClientName;
                physicianClientDistribution.PhysicianId = 728;
                physicianClientDistribution.PhysicianName = "Staff Pathologist";
                physicianClientDistribution.DistributionType = pathClient.DistributionType;
                physicianClientDistribution.FaxNumber = pathClient.Fax;
                this.Add(physicianClientDistribution);  
                
                if(client.PathologyGroupId == "PAOIF")
                {
                    PhysicianClientDistributionListItem physicianClientDistributionFax = Business.Client.Model.PhysicianClientDistributionFactory.GetPhysicianClientDistribution(client.DistributionType);
                    physicianClientDistributionFax.ClientId = pathClient.ClientId;
                    physicianClientDistributionFax.ClientName = pathClient.ClientName;
                    physicianClientDistributionFax.PhysicianId = 728;
                    physicianClientDistributionFax.PhysicianName = "Staff Pathologist";
                    physicianClientDistributionFax.DistributionType = "Fax";
                    physicianClientDistributionFax.FaxNumber = pathClient.Fax;
                    this.Add(physicianClientDistributionFax);
                }
            }
        }

        private bool IsPAOIFWH(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, Business.Client.Model.Client client)
        {
            bool result = false;
            if(accessionOrder.PanelSetOrderCollection.HasWomensHealthProfileOrder() == true || 
                accessionOrder.PanelSetOrderCollection.HasThinPrepPapOrder() == true)
            {
                if(client.PathologyGroupId == "PAOIF")
                {
                    result = true;
                }
            }
            return result;
        }
        
        private void HandleReferringProvider(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            YellowstonePathology.Business.Client.Model.Client client = Business.Gateway.PhysicianClientGateway.GetClientByClientId(accessionOrder.ClientId);
            if(client.HasReferringProvider == true)
            {
                PhysicianClientDistributionListItem physicianClientDistribution = Business.Gateway.ReportDistributionGateway.GetPhysicianClientDistributionCollection(client.ReferringProviderClientId);
                this.Add(physicianClientDistribution);
            }
        }

        public bool DoesEpicDistributionExist()
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Client.Model.PhysicianClientDistributionListItem physicianClientDistributionListItem in this)
            {
                if (physicianClientDistributionListItem.DistributionType == Business.Client.Model.EPICPhysicianClientDistribution.EPIC ||
                    physicianClientDistributionListItem.DistributionType == Business.Client.Model.EPICAndFaxPhysicianClientDistribution.EPICANDFAX)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool DoesWebServiceDistributionExist()
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Client.Model.PhysicianClientDistributionListItem physicianClientDistributionListItem in this)
            {
                if (physicianClientDistributionListItem.DistributionType == "Web Service")
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool DoesStaffPathologistExist()
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Client.Model.PhysicianClientDistributionListItem physicianClientDistributionListItem in this)
            {
                if (physicianClientDistributionListItem.PhysicianId == 728)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
