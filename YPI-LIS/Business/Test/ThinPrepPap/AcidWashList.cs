using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Test.ThinPrepPap
{
    public class AcidWashList : ObservableCollection<AcidWashListItem>
    {
        public AcidWashList()
        {

        }

        public void SetState()
        {
            foreach(AcidWashListItem item in this)
            {
                if(item.Accepted == false)
                {
                    if(item.OrderTime.AddHours(4) > DateTime.Now)
                    {
                        item.State = Monitor.Model.MonitorStateEnum.Critical;
                    }
                    else
                    {
                        item.State = Monitor.Model.MonitorStateEnum.Warning;
                    }
                }
                else
                {
                    item.State = Monitor.Model.MonitorStateEnum.Normal;
                }
            }
        }
    }
}
