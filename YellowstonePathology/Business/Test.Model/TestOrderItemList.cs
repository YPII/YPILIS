using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Test.Model
{
	public class TestOrderItemList : ObservableCollection<TestOrderListItem>
	{
        public TestOrderItemList()
        {

		}
	}
}
