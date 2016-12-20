using System;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.Surgical
{
	[PersistentClass("tblSurgicalSpecimenAudit", "YPIDATA")]
	public class SurgicalSpecimenAudit : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private string m_SurgicalSpecimenAuditId;
		private string m_SurgicalAuditId;
		private string m_ReportNo;
		private bool m_Report;
		private bool m_ImmediatePerformed;
		private Nullable<DateTime> m_ImmediateStartTime;
		private Nullable<DateTime> m_ImmediateEndTime;
		private string m_AmendmentId;
		private string m_SurgicalSpecimenId;
		private string m_SpecimenOrderId;
		private int m_DiagnosisId;
		private int m_ImmediateCorrelation;
		private int m_ImmediatePerformedById;
		private string m_Diagnosis;
		private string m_SpecimenType;
		private string m_RescreenStatus;
		private string m_ImmediatePerformedBy;

		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;

		public SurgicalSpecimenAudit()
        {
		}

		public SurgicalSpecimenAudit(string objectId, string surgicalSpecimenAuditId, string surgicalAuditId, YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen, string amendmentId)
		{
			this.m_ObjectId = objectId;
			this.m_SurgicalSpecimenAuditId = surgicalSpecimenAuditId;
			this.m_SurgicalAuditId = surgicalAuditId;
			this.m_Report = surgicalSpecimen.Report;
			this.m_ImmediatePerformed = surgicalSpecimen.ImmediatePerformed;
			this.m_ImmediateStartTime = surgicalSpecimen.ImmediateStartTime;
			this.m_ImmediateEndTime = surgicalSpecimen.ImmediateEndTime;
			this.m_AmendmentId = amendmentId;
			this.m_SurgicalSpecimenId = surgicalSpecimen.SurgicalSpecimenId;
			this.m_ReportNo = surgicalSpecimen.ReportNo;
			this.m_SpecimenOrderId = surgicalSpecimen.SpecimenOrderId;
			this.m_DiagnosisId = surgicalSpecimen.DiagnosisId;
			this.m_ImmediateCorrelation = surgicalSpecimen.ImmediateCorrelation;
			this.m_ImmediatePerformedById = surgicalSpecimen.ImmediatePerformedById;
			this.m_Diagnosis = surgicalSpecimen.Diagnosis;
			this.m_SpecimenType = surgicalSpecimen.SpecimenType;
			this.m_RescreenStatus = surgicalSpecimen.RescreenStatus;
			this.m_ImmediatePerformedBy = surgicalSpecimen.ImmediatePerformedBy;
			this.m_SpecimenOrder = surgicalSpecimen.SpecimenOrder;
		}

		public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
		{
			get
			{
				return this.m_SpecimenOrder;
			}
			set
			{
				m_SpecimenOrder = value;
			}
		}

		public string DiagnosisIdFormatted
		{
			get { return DiagnosisId.ToString() + "."; }
		}

		[PersistentDocumentIdProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ObjectId
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
		public string SurgicalSpecimenAuditId
		{
			get { return this.m_SurgicalSpecimenAuditId; }
			set
			{
				if (this.m_SurgicalSpecimenAuditId != value)
				{
					this.m_SurgicalSpecimenAuditId = value;
					this.NotifyPropertyChanged("SurgicalSpecimenAuditId");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string SurgicalAuditId
		{
			get { return this.m_SurgicalAuditId; }
			set
			{
				if (this.m_SurgicalAuditId != value)
				{
					this.m_SurgicalAuditId = value;
					this.NotifyPropertyChanged("SurgicalAuditId");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "20", "null", "varchar")]
		public string ReportNo
		{
			get { return this.m_ReportNo; }
			set
			{
				if (this.m_ReportNo != value)
				{
					this.m_ReportNo = value;
					this.NotifyPropertyChanged("ReportNo");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "tinyint")]
		public bool Report
		{
			get { return this.m_Report; }
			set
			{
				if (this.m_Report != value)
				{
					this.m_Report = value;
					this.NotifyPropertyChanged("Report");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "tinyint")]
		public bool ImmediatePerformed
		{
			get { return this.m_ImmediatePerformed; }
			set
			{
				if (this.m_ImmediatePerformed != value)
				{
					this.m_ImmediatePerformed = value;
					this.NotifyPropertyChanged("ImmediatePerformed");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
		public Nullable<DateTime> ImmediateStartTime
		{
			get { return this.m_ImmediateStartTime; }
			set
			{
				if (this.m_ImmediateStartTime != value)
				{
					this.m_ImmediateStartTime = value;
					this.NotifyPropertyChanged("ImmediateStartTime");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
		public Nullable<DateTime> ImmediateEndTime
		{
			get { return this.m_ImmediateEndTime; }
			set
			{
				if (this.m_ImmediateEndTime != value)
				{
					this.m_ImmediateEndTime = value;
					this.NotifyPropertyChanged("ImmediateEndTime");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "50", "0", "varchar")]
		public string AmendmentId
		{
			get { return this.m_AmendmentId; }
			set
			{
				if (this.m_AmendmentId != value)
				{
					this.m_AmendmentId = value;
					this.NotifyPropertyChanged("AmendmentId");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "50", "0", "varchar")]
		public string SurgicalSpecimenId
		{
			get { return this.m_SurgicalSpecimenId; }
			set
			{
				if (this.m_SurgicalSpecimenId != value)
				{
					this.m_SurgicalSpecimenId = value;
					this.NotifyPropertyChanged("SurgicalSpecimenId");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "100", "0", "varchar")]
		public string SpecimenOrderId
		{
			get { return this.m_SpecimenOrderId; }
			set
			{
				if (this.m_SpecimenOrderId != value)
				{
					this.m_SpecimenOrderId = value;
					this.NotifyPropertyChanged("SpecimenOrderId");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "11", "0", "int")]
		public int DiagnosisId
		{
			get { return this.m_DiagnosisId; }
			set
			{
				if (this.m_DiagnosisId != value)
				{
					this.m_DiagnosisId = value;
					this.NotifyPropertyChanged("DiagnosisId");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "11", "0", "int")]
		public int ImmediateCorrelation
		{
			get { return this.m_ImmediateCorrelation; }
			set
			{
				if (this.m_ImmediateCorrelation != value)
				{
					this.m_ImmediateCorrelation = value;
					this.NotifyPropertyChanged("ImmediateCorrelation");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "11", "0", "int")]
		public int ImmediatePerformedById
		{
			get { return this.m_ImmediatePerformedById; }
			set
			{
				if (this.m_ImmediatePerformedById != value)
				{
					this.m_ImmediatePerformedById = value;
					this.NotifyPropertyChanged("ImmediatePerformedById");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "-1", "null", "text")]
		public string Diagnosis
		{
			get { return this.m_Diagnosis; }
			set
			{
				if (this.m_Diagnosis != value)
				{
					this.m_Diagnosis = value;
					this.NotifyPropertyChanged("Diagnosis");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "350", "null", "varchar")]
		public string SpecimenType
		{
			get { return this.m_SpecimenType; }
			set
			{
				if (this.m_SpecimenType != value)
				{
					this.m_SpecimenType = value;
					this.NotifyPropertyChanged("SpecimenType");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "30", "null", "varchar")]
		public string RescreenStatus
		{
			get { return this.m_RescreenStatus; }
			set
			{
				if (this.m_RescreenStatus != value)
				{
					this.m_RescreenStatus = value;
					this.NotifyPropertyChanged("RescreenStatus");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string ImmediatePerformedBy
		{
			get { return this.m_ImmediatePerformedBy; }
			set
			{
				if (this.m_ImmediatePerformedBy != value)
				{
					this.m_ImmediatePerformedBy = value;
					this.NotifyPropertyChanged("ImmediatePerformedBy");
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
