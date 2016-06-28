using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain
{
    public class PhysicianClientCollection : ObservableCollection<PhysicianClient>
    {
        public PhysicianClientCollection()
        {

        }

        public PhysicianClient GetByPhysicianClientId(string physicianClientId)
        {
            PhysicianClient result = null;
            foreach (PhysicianClient physicianClient in this)
            {
                if (physicianClient.PhysicianClientId == physicianClientId)
                {
                    result = physicianClient;
                    break;
                }
            }
            return result;
        }
    }
}
