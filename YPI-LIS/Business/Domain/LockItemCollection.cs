using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Domain
{
	public class LockItemCollection : ObservableCollection<LockItem>
	{
		public LockItemCollection()
		{

		}

        public bool Exists(string key, string lockedBy)
        {
            bool result = false;
            foreach(LockItem lockItem in this)
            {
                if(lockItem.KeyString == key && lockItem.LockedBy == lockedBy)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
	}
}
