using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Surgical
{
	public class SurgicalRescreenItem
	{
		private string m_SpecimenOrderId;
		private DateTime m_AccessionDate;
		private string m_AccessionNo;
		private string m_Description;
		private string m_PLastName;
		private string m_RescreenStatus;

		public SurgicalRescreenItem()
		{
		}

		[PersistentProperty()]
		public string SpecimenOrderId
		{
			get { return this.m_SpecimenOrderId; }
			set { this.m_SpecimenOrderId = value; }
		}

		[PersistentProperty()]
		public DateTime AccessionDate
		{
			get { return this.m_AccessionDate; }
			set { this.m_AccessionDate = value; }
		}

		[PersistentProperty()]
		public string AccessionNo
		{
			get { return this.m_AccessionNo; }
			set { this.m_AccessionNo = value; }
		}

		[PersistentProperty()]
		public string Description
		{
			get { return this.m_Description; }
			set { this.m_Description = value; }
		}

		[PersistentProperty()]
		public string PLastName
		{
			get { return this.m_PLastName; }
			set { this.m_PLastName = value; }
		}

		[PersistentProperty()]
		public string RescreenStatus
		{
			get { return this.m_RescreenStatus; }
			set { this.m_RescreenStatus = value; }
		}

	}
}
