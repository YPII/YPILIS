using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Domain
{
	[Table(Name = "tblPatient")]
	public class Patient : DomainBase
	{
		private int m_PatientId;

		public Patient()
		{ }

		[Column(Name = "PatientId", Storage = "m_PatientId", IsPrimaryKey = true, CanBeNull = false, IsDbGenerated = true)]
		public int PatientId
		{
			get { return this.m_PatientId; }
			set
			{
				if (this.m_PatientId != value)
				{
					this.m_PatientId = value;
					NotifyPropertyChanged("PatientId");
				}
			}
		}

		public string PatientIdString
		{
			get { return this.m_PatientId.ToString(); }
		}
	}
}
