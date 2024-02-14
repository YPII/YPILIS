using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Domain
{
    [PersistentClass("tblPhysicianClient", "YPIDATA")]
	public partial class PhysicianClient
	{
        public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private string m_PhysicianClientId;
		private int m_PhysicianId;
        private int m_ClientId;
		private string m_ProviderId;

		public PhysicianClient()
		{

		}

		public PhysicianClient(string objectId, string physicianClientId, int physicianId, string providerId, int clientId)
		{
			this.m_ObjectId = objectId;
			this.m_PhysicianClientId = physicianClientId;
			this.m_PhysicianId = physicianId;
			this.m_ProviderId = providerId;
			this.m_ClientId = clientId;
		}

		[PersistentDocumentIdProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public String ObjectId
		{
			get { return this.m_ObjectId; }
			set
			{
				if (this.m_ObjectId != value)
				{
					this.m_ObjectId = value;
					this.NotifyPropertyChanged("ObjectId");
				}
			}
		}

        [PersistentPrimaryKeyProperty(false)]
        [PersistentDataColumnProperty(false, "50", "null", "varchar")]
        public string PhysicianClientId
        {
            get { return this.m_PhysicianClientId; }
            set
            {
                if (this.m_PhysicianClientId != value)
                {
                    this.m_PhysicianClientId = value;
                    this.NotifyPropertyChanged("PhysicianClientId");                    
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "11", "0", "int")]
		public int PhysicianId
        {
            get { return this.m_PhysicianId; }
            set
            {
                if (this.m_PhysicianId != value)
                {
                    this.m_PhysicianId = value;
                    this.NotifyPropertyChanged("PhysicianId");                    
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "11", "0", "int")]
        public int ClientId
        {
            get { return this.m_ClientId; }
            set
            {
                if (this.m_ClientId != value)
                {
                    this.m_ClientId = value;
                    this.NotifyPropertyChanged("ClientId");                    
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ProviderId
		{
			get { return this.m_ProviderId; }
			set
			{
				if (this.m_ProviderId != value)
				{
					this.m_ProviderId = value;
					this.NotifyPropertyChanged("ProviderId");
				}
			}
		}

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }   
	}
}
