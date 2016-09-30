using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain
{
	public class OrderableTestIcd9CodeCollection : ObservableCollection<OrderableTestIcd9Code>
	{
		public OrderableTestIcd9CodeCollection()
		{
		}
	}
}
