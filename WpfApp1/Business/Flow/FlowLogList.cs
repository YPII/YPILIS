using System;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Flow
{
    public class FlowLogList : ObservableCollection<FlowLogListItem>
    {	
        public FlowLogList()
        {
         
        }

        public void SetLockIsAquiredByMe(Business.Test.AccessionOrder accessionOrder)
        {
            foreach (FlowLogListItem item in this)
            {
                if (item.MasterAccessionNo == accessionOrder.MasterAccessionNo)
                {
                    item.LockAquired = accessionOrder.AccessionLock.IsLockAquired;
                    item.IsLockAquiredByMe = accessionOrder.AccessionLock.IsLockAquiredByMe;
                }
                else
                {
                    item.IsLockAquiredByMe = false;
                    item.LockAquired = false;
                }
            }
        }
    }
}
