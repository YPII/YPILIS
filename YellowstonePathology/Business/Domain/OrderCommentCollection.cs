using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Domain
{
	public class OrderCommentCollection : ObservableCollection<YellowstonePathology.Business.Domain.OrderComment>
	{
		public OrderCommentCollection()
		{
		}
	}
}
