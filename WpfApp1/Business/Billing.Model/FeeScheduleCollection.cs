using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class FeeScheduleCollection : ObservableCollection<FeeSchedule>
    {
        public FeeScheduleCollection()
        {

        }

        public static FeeScheduleCollection GetAll()
        {
            FeeScheduleCollection result = new FeeScheduleCollection();
            result.Add(new FeeSchedule("Standard"));
            result.Add(new FeeSchedule("Uninsured"));
            result.Add(new FeeScheduleCosmetic());
            return result;
        }
    }
}
