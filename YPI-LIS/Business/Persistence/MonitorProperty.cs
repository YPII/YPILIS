using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{    
    public class MonitorProperty : System.Attribute
    {
        private bool m_IsMonitored;

        public MonitorProperty()
        {
            this.m_IsMonitored = false;
        }

        public MonitorProperty(bool isMonitored)
        {
            this.m_IsMonitored = isMonitored;
        }

        public bool IsMonitored
        {
            get { return this.m_IsMonitored; }
        }
    }
}

