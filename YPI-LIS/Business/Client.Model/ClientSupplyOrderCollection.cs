using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Client.Model
{
	public class ClientSupplyOrderCollection : ObservableCollection<ClientSupplyOrder>
	{
		public ClientSupplyOrderCollection()
		{
		}

		public ClientSupplyOrder GetClientSupplyOrder(string objectId)
		{
			ClientSupplyOrder result = null;
			foreach (ClientSupplyOrder order in this)
			{
				if (order.ObjectId == objectId)
				{
					result = order;
					break;
				}
			}
			return result;
		}
	}
}
