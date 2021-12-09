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
        protected int m_PanelSetId;

        public UniversalService()
        {
            
        }

        public UniversalService(string serviceId, string serviceName, UniversalServiceApplicationNameEnum applicationName, int panelSetId)
        {
            this.m_UniversalServiceId = serviceId;
            this.m_ServiceName = serviceName;
            this.m_ApplicationName = applicationName;
            this.m_PanelSetId = panelSetId;
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

        public int PanelSetId
        {
            get { return this.m_PanelSetId; }
            set { this.m_PanelSetId = value; }
        }
    }
}
