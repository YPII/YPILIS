using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain
{
    public class PhysicianCollection : ObservableCollection<Physician>
    {
        public PhysicianCollection()
        {

        }

        public Physician GetByPhysicianId(int physicianId)
        {
            Physician result = null;
            foreach (Physician physician in this)
            {
                if (physician.PhysicianId == physicianId)
                {
                    result = physician;
                    break;
                }
            }
            return result;
        }
    }
}
