using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class MultiTestDistributionHandlerFactory
    {
        public static MultiTestDistributionHandler GetHandler(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            MultiTestDistributionHandler result = null;
            YellowstonePathology.Business.Client.Model.ClientGroupClientCollection clientGroupStVincent = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientGroupClientCollectionByClientGroupId("1");

            if (accessionOrder.PanelSetOrderCollection.HasWomensHealthProfileOrder() == true)
            {
                YellowstonePathology.Business.Domain.Physician physician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(accessionOrder.PhysicianId);

                if (accessionOrder.PhysicianId == 185) //Dian Nagy
                {
                    result = new MultiTestDistributionHandlerNoWHP(accessionOrder);
                }
                else if (physician != null && physician.DistributeWHPOnly == true)
                {
                    result = new MultiTestDistributionHandlerWHPOnly(accessionOrder);
                }
                else if(physician != null && physician.HoldForWHP == true)
                {
                    result = new MultiTestDistributionHandlerWHPHold(accessionOrder);
                }
                else
                {
                    if (clientGroupStVincent.ClientIdExists(accessionOrder.ClientId) == true)
                    {
                        result = new MultiTestDistributionHandlerWHPHold(accessionOrder);
                    }
                    else
                    {
                        result = new MultiTestDistributionHandlerWHPStandard(accessionOrder);
                    }
                }
            }
            else
            {
                result = new MultiTestDistributionHandler(accessionOrder);
            }
            return result;
        }
    }
}
