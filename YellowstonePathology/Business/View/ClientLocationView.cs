using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.View
{
	[XmlType("ClientLocationView")]
	public class ClientLocationView
	{
		private int m_ClientId;
		private int m_ClientLocationId;
		private string m_ClientName;
		private string m_Location;

		public ClientLocationView()
		{

		}

        public ClientLocationView(int clientId, int clientLocationId, string clientName, string location)
        {
            this.ClientId = clientId;
            this.ClientLocationId = clientLocationId;
            this.m_ClientName = clientName;
            this.m_Location = location;
        }

        [PersistentProperty()]
		public int ClientId
		{
			get { return this.m_ClientId; }
			set { this.m_ClientId = value; }
		}

        [PersistentProperty()]
        public int ClientLocationId
		{
			get { return this.m_ClientLocationId; }
			set { this.m_ClientLocationId = value; }
		}

        [PersistentProperty()]
        public string ClientName
		{
			get { return this.m_ClientName; }
			set { this.m_ClientName = value; }
		}

        [PersistentProperty()]
        public string Location
		{
			get { return this.m_Location; }
			set { this.m_Location = value; }
		}
	}
}
