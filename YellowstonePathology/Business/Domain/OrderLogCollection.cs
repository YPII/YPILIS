using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Domain
{
	public class OrderLogCollection : ObservableCollection<OrderLogItem>
	{
		public OrderLogCollection()
		{
		}
	}
}
