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
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.FAX:
                    physicianClientDistribution = new FaxPhysicianClientDistribution();
                    break;                                                       
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ECW:
                    physicianClientDistribution = new ECWPhysicianClientDistribution();
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.ATHENA:
                    physicianClientDistribution = new AthenaPhysicianClientDistribution();
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPIC:
                    physicianClientDistribution = new EPICPhysicianClientDistribution();
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MEDITECH:
                    physicianClientDistribution = new MediTechPhysicianClientDistribution();
                    break;
                case YellowstonePathology.Business.ReportDistribution.Model.DistributionType.EPICANDFAX:
                    physicianClientDistribution = new EPICAndFaxPhysicianClientDistribution();
                    break;                                                
                default:
                    physicianClientDistribution = new PhysicianClientDistributionListItem();
                    break;
            }
            return physicianClientDistribution;
        }
    }
}
