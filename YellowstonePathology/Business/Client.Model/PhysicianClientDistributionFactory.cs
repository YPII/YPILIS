using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class PhysicianClientDistributionFactory
    {
        public static PhysicianClientDistributionListItem GetPhysicianClientDistribution(string distributionType)
        {
            PhysicianClientDistributionListItem physicianClientDistribution = null;
            switch (distributionType)
            {
                case YellowstonePathology.Business.Client.Model.FaxPhysicianClientDistribution.FAX:
                    physicianClientDistribution = new FaxPhysicianClientDistribution();
                    break;                                                       
                case YellowstonePathology.Business.Client.Model.ECWPhysicianClientDistribution.ECW:
                    physicianClientDistribution = new ECWPhysicianClientDistribution();
                    break;
                case YellowstonePathology.Business.Client.Model.AthenaPhysicianClientDistribution.ATHENA:
                    physicianClientDistribution = new AthenaPhysicianClientDistribution();
                    break;
                case YellowstonePathology.Business.Client.Model.EPICPhysicianClientDistribution.EPIC:
                case "EPIC->Fax":
                case "Eclinical Works->Fax":
                case "Athena Health->Fax":
                case "Meditech->Fax":
                    physicianClientDistribution = new EPICPhysicianClientDistribution();
                    break;
                case YellowstonePathology.Business.Client.Model.MediTechPhysicianClientDistribution.MEDITECH:
                    physicianClientDistribution = new MediTechPhysicianClientDistribution();
                    break;
                case YellowstonePathology.Business.Client.Model.EPICAndFaxPhysicianClientDistribution.EPICANDFAX:
                    physicianClientDistribution = new EPICAndFaxPhysicianClientDistribution();
                    break;
                case YellowstonePathology.Business.Client.Model.WYDOHPhysicianClientDistribution.WYDOH:
                    physicianClientDistribution = new WYDOHPhysicianClientDistribution();
                    break;
                case YellowstonePathology.Business.Client.Model.MTDOHPhysicianClientDistribution.MTDOH:
                    physicianClientDistribution = new MTDOHPhysicianClientDistribution();
                    break;
                case YellowstonePathology.Business.Client.Model.WebServicePhysicianClientDistribution.WEBSERVICE:
                    physicianClientDistribution = new WebServicePhysicianClientDistribution();
                    break;
                case YellowstonePathology.Business.Client.Model.WebServiceAndFaxPhysicianClientDistribution.WEBSERVICEANDFAX:
                    physicianClientDistribution = new WebServiceAndFaxPhysicianClientDistribution();
                    break;
                case YellowstonePathology.Business.Client.Model.PrintPhysicianClientDistribution.PRINT:
                    physicianClientDistribution = new PrintPhysicianClientDistribution();
                    break;
                case YellowstonePathology.Business.Client.Model.DoNotDistributePhysicianClientDistribution.DONOTDISTRIBUTE:
                    physicianClientDistribution = new DoNotDistributePhysicianClientDistribution();
                    break;
                default:
                    physicianClientDistribution = new NotSetPhysicianClientDistribution();
                    break;
            }
            return physicianClientDistribution;
        }
    }
}
