using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Client.Model
{
    public class ProviderClient
    {
        private YellowstonePathology.Business.Domain.Physician m_Physician;
        private Client m_Client;
        private string m_PhysicianClientId;

        public ProviderClient() { }

        [PersistentProperty()]
        public string PhysicianClientId
        {
            get { return this.m_PhysicianClientId; }
            set { this.m_PhysicianClientId = value; }
        }

        public Client Client
        {
            get { return this.m_Client; }
            set { this.m_Client = value; }
        }

        public YellowstonePathology.Business.Domain.Physician Physician
        {
            get { return this.m_Physician; }
            set { this.m_Physician = value; }
        }
    }
}
