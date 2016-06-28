using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Surgical
{
	public class SurgicalMasterLogList : ObservableCollection<SurgicalMasterLogItem>
	{
		public SurgicalMasterLogList()
		{

		}

		public void FillByReportDateAndLocation(DateTime reportDate)
		{
			this.Clear();

			SurgicalMasterLogList list = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSurgicalMasterLogList(reportDate);
			if (list != null)
			{
				foreach (SurgicalMasterLogItem item in list) this.Add(item);
			}
		}        
	}

	public class SurgicalMasterLogItem : ListItem
	{
		private MasterLogList m_MasterLogList;
		private string m_ReportNo;
		private string m_AccessioningFacilityId;
		private string m_PFirstName;
		private string m_PLastName;
		private Nullable<DateTime> m_PBirthdate;
		private string m_PhysicianName;
		private string m_ClientName;
		private int m_AliquotCount;

		public SurgicalMasterLogItem()
		{
			this.m_MasterLogList = new MasterLogList();
		}

		public MasterLogList MasterLogList
		{
			get { return this.m_MasterLogList; }
			set { this.m_MasterLogList = value; }
		}

		[PersistentProperty()]
		public string ReportNo
		{
			get { return this.m_ReportNo; }
			set { this.m_ReportNo = value; }
		}

		[PersistentProperty()]
		public string AccessioningFacilityId
		{
			get { return this.m_AccessioningFacilityId; }
			set { this.m_AccessioningFacilityId = value; }
		}

		[PersistentProperty()]
		public string PFirstName
		{
			get { return this.m_PFirstName; }
			set { this.m_PFirstName = value; }
		}

		[PersistentProperty()]
		public string PLastName
		{
			get { return this.m_PLastName; }
			set { this.m_PLastName = value; }
		}

		[PersistentProperty()]
		public Nullable<DateTime> PBirthdate
		{
			get { return this.m_PBirthdate; }
			set { this.m_PBirthdate = value; }
		}

		[PersistentProperty()]
		public string PhysicianName
		{
			get { return this.m_PhysicianName; }
			set { this.m_PhysicianName = value; }
		}

		[PersistentProperty()]
		public string ClientName
		{
			get { return this.m_ClientName; }
			set { this.m_ClientName = value; }
		}

		[PersistentProperty()]
		public int AliquotCount
		{
            get { return this.m_AliquotCount; }
            set { this.m_AliquotCount = value; }
		}
	}

	public class MasterLogList : ObservableCollection<MasterLogItem>
	{
		public MasterLogList()
		{
		}
	}

	public class MasterLogItem : ListItem
	{
		private int m_DiagnosisId;
		private string m_Description;

		public MasterLogItem()
		{
		}

		[PersistentProperty()]
		public int DiagnosisId
		{
			get { return this.m_DiagnosisId; }
			set { this.m_DiagnosisId = value; }
		}

		[PersistentProperty()]
		public string Description
		{
			get { return this.m_Description; }
			set { this.m_Description = value; }
		}
	}
}
