using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
    public class UniversalService
    {
        protected string m_UniversalServiceId;
        protected string m_ServiceName;
        protected UniversalServiceApplicationNameEnum m_ApplicationName;

        public UniversalService()
        {
            
        }

        public string UniversalServiceId
        {
            get { return this.m_UniversalServiceId; }
            set { this.m_UniversalServiceId = value; }
        }

        public string ServiceName
        {
            get { return this.m_ServiceName; }
            set { this.m_ServiceName = value; }
        }

        public UniversalServiceApplicationNameEnum ApplicationName
        {
            get { return this.m_ApplicationName; }
            set { this.m_ApplicationName = value; }
        }
    }
}
