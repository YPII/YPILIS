using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	[XmlType("OrderTypeCollection")]
	public class OrderTypeCollection : ObservableCollection<YellowstonePathology.Business.ClientOrder.Model.OrderType>
	{
		public OrderTypeCollection()
		{
		}
	}
}
