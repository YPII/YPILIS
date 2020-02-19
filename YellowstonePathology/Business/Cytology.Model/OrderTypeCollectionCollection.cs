using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Cytology.Model
{
	public class OrderTypeCollection : ObservableCollection<OrderType>
	{
		public OrderTypeCollection()
		{
            this.Add(new OrderType("10", "ThinPrep Pap test screen(acceptable for ages 21 and above) with management of abnormal screening results per ASCCP preferred guidelines."));
            this.Add(new OrderType("11", "ThinPrep Pap with High Risk HPV screen(Co-test, preferred use in ages 30 and above) with management of abnormal screening results per ASCCP preferred guidelines."));
        }

        public OrderType Get(Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder)
        {
            OrderType result = null;
            if (womensHealthProfileTestOrder.ManagePerASCCP == true) result = Get("10");
            if (womensHealthProfileTestOrder.ManagePerASCCPWithCotest == true) result = Get("11");
            return result;
        }

        public OrderType Get(string orderCode)
        {
            OrderType result = null;
            foreach (OrderType orderType in this)
            {
                if (orderType.OrderCode == orderCode)
                {
                    result = orderType;
                    break;
                }
            }
            return result;
        }
	}
}
