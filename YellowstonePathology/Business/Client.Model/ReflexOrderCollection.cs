using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class ReflexOrderCollection : ObservableCollection<ReflexOrder>
    {
        public ReflexOrderCollection()
        {

        }

        public static ReflexOrder GetByReflexByOrderCode(string reflexOrderCode)
        {
            ReflexOrder result = null;
            ReflexOrderCollection collection = GetAll();
            foreach (ReflexOrder reflexOrder in collection)
            {
                if (reflexOrder.ReflexOrderCode == reflexOrderCode)
                {
                    result = reflexOrder;
                    break;
                }
            }
            return result;
        }

        public static ReflexOrderCollection GetAll()
        {
            ReflexOrderCollection result = new ReflexOrderCollection();
            result.Add(new ReflexOrderNone());
            result.Add(new HPVReflexOrderRule1());
            result.Add(new HPVReflexOrderRule2());
            result.Add(new HPVReflexOrderRule3());
            result.Add(new HPVReflexOrderRule4());
            result.Add(new HPVReflexOrderRule5());
            result.Add(new HPVReflexOrderRule6());
            result.Add(new HPVReflexOrderRule7());
            result.Add(new HPVReflexOrderRule8());
            result.Add(new HPVReflexOrderRule9());            
            result.Add(new HPVReflexOrderRule10());
            result.Add(new HPVReflexOrderRule11());
            result.Add(new HPVReflexOrderRule14());
            result.Add(new HPV1618ReflexOrderHPVPositive());
            result.Add(new HPV1618ReflexOrderPAPNormalHPVPositive());            
            return result;
        }

        public static ReflexOrderCollection GetHPV1618ReflexOrders()
        {
            ReflexOrderCollection result = new ReflexOrderCollection();
            result.Add(new ReflexOrderNone());
            result.Add(new HPV1618ReflexOrderHPVPositive());
            result.Add(new HPV1618ReflexOrderPAPNormalHPVPositive());
            return result;
        }

        public static ReflexOrderCollection GetHPVReflexOrders()
        {
            ReflexOrderCollection result = new ReflexOrderCollection();
            result.Add(new ReflexOrderNone());
            result.Add(new HPVReflexOrderRule1());
            result.Add(new HPVReflexOrderRule2());
            result.Add(new HPVReflexOrderRule3());
            result.Add(new HPVReflexOrderRule4());
            result.Add(new HPVReflexOrderRule5());
            result.Add(new HPVReflexOrderRule6());
            result.Add(new HPVReflexOrderRule7());
            result.Add(new HPVReflexOrderRule8());
            result.Add(new HPVReflexOrderRule9());
            result.Add(new HPVReflexOrderRule10());
            result.Add(new HPVReflexOrderRule11());
            return result;
        }

        public static ReflexOrderCollection GetHPVRequisitionReflexOrders()
        {
            ReflexOrderCollection result = new ReflexOrderCollection();
            result.Add(new ReflexOrderNone());            
            result.Add(new HPVReflexOrderRule2());
            result.Add(new HPVReflexOrderRule14());
            return result;
        }
    }
}
