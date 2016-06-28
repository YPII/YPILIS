using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	public class ShipmentCollection : ObservableCollection<YellowstonePathology.Business.ClientOrder.Model.Shipment>
	{
		public ShipmentCollection()
		{
            
		}

		public Shipment GetShipment(string shipmentId)
		{
			Shipment result = null;
			foreach (Shipment item in this)
			{
				if (item.ShipmentId == shipmentId)
				{
					result = item;
					break;
				}
			}            
			return result;
		}        
	}
}
