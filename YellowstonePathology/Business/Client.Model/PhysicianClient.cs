using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Client.Model
{
    public class PhysicianClient
    {
        private string m_PhysicianClientId;
        private int m_ClientId;
		private int m_PhysicianId;
		private string m_ProviderId;
		private string m_ClientName;
        private string m_NPI;
        private string m_FacilityType;
        private string m_DistributionType;
        private string m_PhysicianName;        
        private string m_FaxNumber;
        private string m_Telephone;
        private bool m_LongDistance;

        public PhysicianClient()
        {

        }

        [PersistentProperty()]
        public string PhysicianClientId
        {
            get { return m_PhysicianClientId; }
            set { m_PhysicianClientId = value; }
        }

        [PersistentProperty()]
        public int ClientId
        {
            get { return m_ClientId; }
            set { m_ClientId = value; }
        }

        [PersistentProperty()]
		public int PhysicianId
        {
            get { return m_PhysicianId; }
            set { m_PhysicianId = value; }
        }

		[PersistentProperty()]
		public string ProviderId
		{
			get { return m_ProviderId; }
			set { m_ProviderId = value; }
		}

        [PersistentProperty()]
        public string NPI
        {
            get { return m_NPI; }
            set { m_NPI = value; }
        }

        [PersistentProperty()]
        public string ClientName
        {
            get { return m_ClientName; }
            set { m_ClientName = value; }
        }

        [PersistentProperty()]
        public string PhysicianName
        {
            get { return m_PhysicianName; }
            set { m_PhysicianName = value; }
        }        

        [PersistentProperty()]
        public string FacilityType
        {
            get { return m_FacilityType; }
            set { m_FacilityType = value; }
        }                

        [PersistentProperty()]
        public string DistributionType
        {
            get { return m_DistributionType; }
            set { m_DistributionType = value; }
        }

        [PersistentProperty()]
        public string FaxNumber
        {
            get { return m_FaxNumber; }
            set { m_FaxNumber = value; }
        }

        [PersistentProperty()]
        public string Telephone
        {
            get { return m_Telephone; }
            set { m_Telephone = value; }
        }

        [PersistentProperty()]
        public bool LongDistance
        {
            get { return m_LongDistance; }
            set { m_LongDistance = value; }
        }
    }
}
