using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.View
{
	[XmlType("ClientSearchViewItem")]
	public class ClientSearchViewItem
	{
		private string m_ObjectId;
		private int m_ClientId;
		private string m_ClientName;
        private string m_Address;
		private string m_Telephone;
		private string m_Fax;

		public ClientSearchViewItem()
		{
		}

		[PersistentDocumentIdProperty]
		public string ObjectId
		{
			get { return this.m_ObjectId; }
			set { this.m_ObjectId = value; }
		}

		[PersistentProperty]
		public int ClientId
		{
			get { return this.m_ClientId; }
			set { this.m_ClientId = value; }
		}

		[PersistentProperty]
		public string ClientName
		{
			get { return this.m_ClientName; }
			set { this.m_ClientName = value; }
		}

		[PersistentProperty]
        public string Address
        {
            get { return this.m_Address; }
            set { this.m_Address = value; }
        }

		[PersistentProperty]
		public string Telephone
		{
			get { return this.m_Telephone; }
			set { this.m_Telephone = value; }
		}

		[PersistentProperty]
		public string Fax
		{
			get { return this.m_Fax; }
			set { this.m_Fax = value; }
		}
	}
}
