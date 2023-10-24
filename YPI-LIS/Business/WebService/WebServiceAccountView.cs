using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.WebService
{
    public class WebServiceAccountView
    {
        private string m_ClientName;
        private int m_WebServiceAccountId;
        private int m_PrimaryClientId;
        private string m_InitialPage;
        private string m_DisplayName;

        public WebServiceAccountView()
        { }

        [PersistentProperty()]
        public string ClientName
        {
            get { return this.m_ClientName; }
            set { this.m_ClientName = value; }
        }

        [PersistentProperty()]
        public int WebServiceAccountId
        {
            get { return this.m_WebServiceAccountId; }
            set { this.m_WebServiceAccountId = value; }
        }

        [PersistentProperty()]
        public int PrimaryClientId
        {
            get { return this.m_PrimaryClientId; }
            set { this.m_PrimaryClientId = value; }
        }

        [PersistentProperty()]
        public string InitialPage
        {
            get { return this.m_InitialPage; }
            set { this.m_InitialPage = value; }
        }

        [PersistentProperty()]
        public string DisplayName
        {
            get { return this.m_DisplayName; }
            set { this.m_DisplayName = value; }
        }
    }
}
