using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class StandingOrderCollection : ObservableCollection<StandingOrder>
    {
        public StandingOrderCollection()
        {

        }

        public static StandingOrder GetByStandingOrderCode(string standingOrderCode)
        {
            StandingOrder result = null;
            StandingOrderCollection collection = GetAll();
            foreach (StandingOrder standingOrder in collection)
            {
                if (standingOrder.StandingOrderCode == standingOrderCode)
                {
                    result = standingOrder;
                    break;
                }
            }
            return result;
        }

        public static StandingOrderCollection GetAll()
        {
            StandingOrderCollection result = new StandingOrderCollection();
            result.Add(new StandingOrderNotSet());
            result.Add(new StandingOrderNone());
            result.Add(new HPVStandingOrderRule1());
            result.Add(new HPVStandingOrderRule2());
            result.Add(new HPVStandingOrderRule3());
            result.Add(new HPVStandingOrderRule4());
            result.Add(new HPVStandingOrderRule5());
            result.Add(new HPVStandingOrderRule6());
            result.Add(new HPVStandingOrderRule7());
            result.Add(new HPVStandingOrderRule8());
            result.Add(new HPVStandingOrderRule9());
            result.Add(new HPVStandingOrderRule10());
            result.Add(new HPVStandingOrderRule11());
            result.Add(new HPVStandingOrderRule12());
            result.Add(new HPVStandingOrderRule13());
            result.Add(new HPVCompoundStandingOrderRule1());
            result.Add(new HPVCompoundStandingOrderRule2());
            result.Add(new HPVCompoundStandingOrderRule3());
			result.Add(new HPVCompoundStandingOrderRule4());
            result.Add(new HPVCompoundStandingOrderRule5());
            result.Add(new HPV1618StandingOrderHPVPositive());
            result.Add(new HPV1618StandingOrderPAPNormalHPVPositive());
            return result;
        }

        public static StandingOrderCollection GetHPVStandingOrders()
        {
            StandingOrderCollection result = new StandingOrderCollection();
            result.Add(new StandingOrderNotSet());
            result.Add(new StandingOrderNone());
            result.Add(new HPVStandingOrderRule1());
            result.Add(new HPVStandingOrderRule2());
            result.Add(new HPVStandingOrderRule3());
            result.Add(new HPVStandingOrderRule4());
            result.Add(new HPVStandingOrderRule5());
            result.Add(new HPVStandingOrderRule6());
            result.Add(new HPVStandingOrderRule7());
            result.Add(new HPVStandingOrderRule8());
            result.Add(new HPVStandingOrderRule9());
            result.Add(new HPVStandingOrderRule10());
            result.Add(new HPVStandingOrderRule11());
            result.Add(new HPVStandingOrderRule12());
            result.Add(new HPVStandingOrderRule13());
            result.Add(new HPVCompoundStandingOrderRule1());
            result.Add(new HPVCompoundStandingOrderRule2());
            result.Add(new HPVCompoundStandingOrderRule3());
            result.Add(new HPVCompoundStandingOrderRule4());
            result.Add(new HPVCompoundStandingOrderRule5());
            return result;
        }

        public static StandingOrderCollection GetHPV1618StandingOrders()
        {
            StandingOrderCollection result = new StandingOrderCollection();
            result.Add(new StandingOrderNotSet());
            result.Add(new StandingOrderNone());
            result.Add(new HPV1618StandingOrderHPVPositive());
            result.Add(new HPV1618StandingOrderPAPNormalHPVPositive());
            return result;
        }
    }
}
