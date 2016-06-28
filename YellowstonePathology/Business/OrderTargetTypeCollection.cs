using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business
{
    public class OrderTargetTypeCollection : ObservableCollection<YellowstonePathology.Business.Interface.IOrderTargetType>
    {
        public OrderTargetTypeCollection()
        {

        }

        public bool Exists(YellowstonePathology.Business.Interface.IOrderTarget orderTarget)
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Interface.IOrderTargetType orderTargetType in this)
            {
                if (orderTargetType.TypeId == orderTarget.GetTargetType().TypeId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
