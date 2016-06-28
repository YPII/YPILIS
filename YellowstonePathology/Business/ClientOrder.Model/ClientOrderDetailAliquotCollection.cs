using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Runtime.Serialization;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	public class ClientOrderDetailAliquotCollection : ObservableCollection<YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailAliquot>
	{
		public ClientOrderDetailAliquotCollection()
		{
		}

		public ClientOrderDetailAliquot GetNextItem(string clientOrderDetailId)
		{
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			string clientOrderDetailAliquotId = Guid.NewGuid().ToString();
			ClientOrderDetailAliquot result = new ClientOrderDetailAliquot(objectId, clientOrderDetailAliquotId, clientOrderDetailId);
			return result;
		}
	}
}
