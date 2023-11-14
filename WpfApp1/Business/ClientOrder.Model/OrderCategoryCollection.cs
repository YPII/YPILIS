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
	[XmlType("OrderCategoryCollection")]
	public class OrderCategoryCollection : ObservableCollection<YellowstonePathology.Business.ClientOrder.Model.OrderCategory>
	{
		public OrderCategoryCollection()
		{
		}
	}
}
