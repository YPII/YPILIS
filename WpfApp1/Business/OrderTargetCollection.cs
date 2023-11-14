using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business
{
    public class OrderTargetCollection : ObservableCollection<YellowstonePathology.Business.Interface.IOrderTarget>
    {
        public OrderTargetCollection()
        {

        }
    }
}
