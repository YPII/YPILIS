using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Client.Model
{
	[PersistentClass("tblPhysicianClientDistribution", "YPIDATA")]
	public class PhysicianClientDistribution
	{
		private string m_ObjectId;
		private int m_PhysicianClientDistributionID;
		private string m_PhysicianClientID;
		private string m_DistributionID;

        public PhysicianClientDistribution()
        {

        }

		public PhysicianClientDistribution(string objectId, string physicianClientID, string distributionID)
		{
			this.m_ObjectId = objectId;
			this.m_PhysicianClientID = physicianClientID;
			this.m_DistributionID = distributionID;
		}

		[PersistentDocumentIdProperty()]
		public string ObjectId
		{
			get { return this.m_ObjectId; }
			set { this.m_ObjectId = value; }
		}

		[PersistentPrimaryKeyProperty(true)]
		public int PhysicianClientDistributionID
		{
			get { return this.m_PhysicianClientDistributionID; }
			set { this.m_PhysicianClientDistributionID = value; }
		}

		[PersistentProperty()]
		public string PhysicianClientID
		{
			get { return this.m_PhysicianClientID; }
			set { this.m_PhysicianClientID = value; }
		}

		[PersistentProperty()]
		public string DistributionID
		{
			get { return this.m_DistributionID; }
			set { this.m_DistributionID = value; }
		}
	}
}
