using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace YellowstonePathology.Business.ClientOrder.Model
{        
    public partial class ClientOrderCollection : ObservableCollection<YellowstonePathology.Business.ClientOrder.Model.ClientOrder>
    {                
        public YellowstonePathology.Business.ClientOrder.Model.ClientOrder GetClientOrderBySvhAccount(string svhAccount)
        {
            YellowstonePathology.Business.ClientOrder.Model.ClientOrder result = null;
            foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder in this)
            {
                if (clientOrder.SvhAccountNo == svhAccount)
                {
                    result = clientOrder;
                    break;
                }
            }
            return result;
        }

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrder GetClientOrderBySvhMedicalRecord(string svhMedicalRecord)
        {
            YellowstonePathology.Business.ClientOrder.Model.ClientOrder result = null;
            foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder in this)
            {
                if (clientOrder.SvhMedicalRecord == svhMedicalRecord)
                {
                    result = clientOrder;
                    break;
                }
            }
            return result;
        }        

		public YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail GetClientOrderDetail(string containerId)
		{
			ClientOrderDetail result = null;
			foreach (ClientOrder clientOrder in this)
			{
				if (clientOrder.ClientOrderDetailCollection.ExistsByContainerId(containerId))
				{
					result = clientOrder.ClientOrderDetailCollection.GetByContainerId(containerId);
					break;
				}
			}
			return result;
		}       

		public bool LisOrderExists()
		{
			bool result = false;
			foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder in this)
			{
				if (clientOrder.SystemInitiatingOrder == "YPIILIS")
				{
					result = true;
					break;
				}
			}
			return result;
		}

		public YellowstonePathology.Business.ClientOrder.Model.ClientOrder GetLisOrder()
		{
			YellowstonePathology.Business.ClientOrder.Model.ClientOrder result = null;
			foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder in this)
			{
				if (clientOrder.SystemInitiatingOrder == "YPIILIS")
				{
					result = clientOrder;
					break;
				}
			}
			return result;
		}        		

		public List<ClientOrder> GetClientOrdersForSystemInitiatingOrder(string systemOriginatingOrder)
		{
			List<ClientOrder> result = new List<ClientOrder>();
			foreach (ClientOrder clientOrder in this)
			{
				if (clientOrder.SystemInitiatingOrder == systemOriginatingOrder)
				{
					result.Add(clientOrder);
				}
			}
			return result;
		}

		public bool ClientOrderDetailExists(string containerId)
		{
			bool result = false;
			foreach (ClientOrder clientOrder in this)
			{
				if (clientOrder.ClientOrderDetailCollection.ExistsByContainerId(containerId))
				{
					result = true;
					break;
				}
			}
			return result;
		}		

		public void Receive()
		{
			foreach (ClientOrder clientOrder in this)
			{
				clientOrder.Received = true;
			}
		}		
	}
}
