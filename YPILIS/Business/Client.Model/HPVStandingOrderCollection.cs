using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVStandingOrderCollection : ObservableCollection<HPVStandingOrder>
    {
        public HPVStandingOrderCollection()
        {

        }

        public HPVStandingOrder GetStandingOrder(string hpvStandingOrderId)
        {
            HPVStandingOrder result = null;
            foreach(HPVStandingOrder hpvStandingOrder in this)
            {
                if(hpvStandingOrder.HPVStandingOrderId == hpvStandingOrderId)
                {
                    result = hpvStandingOrder;
                    break;
                }
            }
            return result;
        }
    }
}
