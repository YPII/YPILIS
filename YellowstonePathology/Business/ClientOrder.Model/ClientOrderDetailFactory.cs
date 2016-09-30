using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
    public class ClientOrderDetailFactory
    {
        public static ClientOrderDetail GetClientOrderDetail(string orderDetailTypeCode, YellowstonePathology.Business.Persistence.PersistenceModeEnum persistenceMode)
        {
            ClientOrderDetail clientOrderDetail = null;
            switch (orderDetailTypeCode)
            {
                case "SRGCL":
                case "BPSY":
                    clientOrderDetail = new SurgicalClientOrderDetail(persistenceMode);
                    break;                
                case "PLCNT":
                    clientOrderDetail = new PlacentaClientOrderDetail(persistenceMode);
                    break;
                default:
                    clientOrderDetail = new ClientOrderDetail(persistenceMode);
                    break;
            }
            return clientOrderDetail;
        }        
    }
}