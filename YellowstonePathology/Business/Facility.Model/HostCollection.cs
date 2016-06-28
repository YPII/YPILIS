using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model
{
    public class HostCollection : ObservableCollection<Host>
    {
        public HostCollection()
        {
            
        }

        public Host GetHostByLocationId(string locationId)
        {
            Host host = null;
            foreach (Host hst in this)
            {
                if (hst.LocationId.ToUpper() == locationId.ToUpper())
                {
                    host = hst;
                    break;
                }
            }
            return host;
        }

        public Host GetHost(string hostName)
        {
            Host host = null;
            foreach (Host hst in this)
            {               
                if (hst.HostName.ToUpper() == hostName.ToUpper())
                {
                    host = hst;
                    break;
                }
            }
            return host;
        }

        public bool LocationIdExists(string locationId)
        {
            bool result = false;
            foreach (Host hst in this)
            {
                if (hst.LocationId.ToUpper() == locationId.ToUpper())
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
