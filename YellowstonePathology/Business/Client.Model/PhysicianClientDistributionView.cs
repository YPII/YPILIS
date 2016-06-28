using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Client.Model
{
	public class PhysicianClientDistributionView
	{
		private PhysicianClientDistribution m_PhysicianClientDistribution;
		private int m_PhysicianId;
		private string m_ProviderId;
        private string m_PhysicianName;
        private int m_ClientId;
        private string m_ClientName;
        private string m_DistributionType;

		public PhysicianClientDistributionView(PhysicianClientDistribution physicianClientDistribution)
		{
			this.m_PhysicianClientDistribution = physicianClientDistribution;
		}

		public PhysicianClientDistribution PhysicianClientDistribution
		{
			get { return this.m_PhysicianClientDistribution; }
		}

		[PersistentProperty()]
		public int PhysicianId
        {
            get { return this.m_PhysicianId; }
            set { this.m_PhysicianId = value; }
        }

		[PersistentProperty()]
		public string ProviderId
		{
			get { return this.m_ProviderId; }
			set { this.m_ProviderId = value; }
		}

		[PersistentProperty()]
		public string PhysicianName
        {
            get { return this.m_PhysicianName; }
            set { this.m_PhysicianName = value; }
        }

		[PersistentProperty()]
		public int ClientId
        {
            get { return this.m_ClientId; }
            set { this.m_ClientId = value; }
        }

		[PersistentProperty()]
		public string ClientName
        {
            get { return this.m_ClientName; }
            set { this.m_ClientName = value; }
        }

		[PersistentProperty()]
		public string DistributionType
        {
            get { return this.m_DistributionType; }
            set { this.m_DistributionType = value; }
        }

        /*public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
        {
			this.m_PhysicianId = propertyWriter.WriteInt("PhysicianId");
            this.m_PhysicianName = propertyWriter.WriteString("PhysicianName");
            this.m_ClientId = propertyWriter.WriteInt("ClientId");
            this.m_ClientName = propertyWriter.WriteString("ClientName");
            this.m_DistributionType = propertyWriter.WriteString("DistributionType");
        }*/
	}
}
