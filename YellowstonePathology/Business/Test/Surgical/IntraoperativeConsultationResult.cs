using System;
using System.Collections.Generic;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.Surgical
{
	[PersistentClass("tblIntraoperativeConsultationResult", true, "YPIDATA")]
	public partial class IntraoperativeConsultationResult : INotifyPropertyChanged, System.ComponentModel.IDataErrorInfo
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private string m_IntraoperativeConsultationResultId;
		private string m_SurgicalSpecimenId;
		private Nullable<DateTime> m_StartDate;
		private Nullable<DateTime> m_EndDate;
		private string m_TestOrderId;
		private int m_PerformedById;
		private string m_Result;
		private string m_Correlation;
		private string m_CorrelationDiscrepancyType;
		private bool m_CorrelationAffectsPatientCare;
		private string m_CorrelationEffectOnPatientCare;
		private string m_CallbackContact;
		private bool m_Final;
		private DateTime? m_FinalDate;
		private DateTime? m_FinalTime;
		private int? m_FinaledById;
		private string m_FinaledBy;

		public IntraoperativeConsultationResult()
        {
			this.m_ValidationErrors = new Dictionary<string, string>();
		}

		public IntraoperativeConsultationResult(string intraoperativeConsultationResultId, string objectId, string surgicalSpecimenId)
		{
			this.m_IntraoperativeConsultationResultId = intraoperativeConsultationResultId;
			this.m_ObjectId = objectId;
			this.m_SurgicalSpecimenId = surgicalSpecimenId;
			this.m_ValidationErrors = new Dictionary<string, string>();
            this.m_Final = false;
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
		public string IntraoperativeConsultationResultId
		{
			get { return this.m_IntraoperativeConsultationResultId; }
			set
			{
				if (this.m_IntraoperativeConsultationResultId != value)
				{
					this.m_IntraoperativeConsultationResultId = value;
					this.NotifyPropertyChanged("IntraoperativeConsultationResultId");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
		public Nullable<DateTime> StartDate
		{
			get { return this.m_StartDate; }
			set
			{
				if (this.m_StartDate != value)
				{
					this.m_StartDate = value;
					this.NotifyPropertyChanged("StartDate");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
		public Nullable<DateTime> EndDate
		{
			get { return this.m_EndDate; }
			set
			{
				if (this.m_EndDate != value)
				{
					this.m_EndDate = value;
					this.NotifyPropertyChanged("EndDate");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string TestOrderId
		{
			get { return this.m_TestOrderId; }
			set
			{
				if (this.m_TestOrderId != value)
				{
					this.m_TestOrderId = value;
					this.NotifyPropertyChanged("TestOrderId");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "11", "0", "int")]
		public int PerformedById
		{
			get { return this.m_PerformedById; }
			set
			{
				if (this.m_PerformedById != value)
				{
					this.m_PerformedById = value;
					this.NotifyPropertyChanged("PerformedById");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "5000", "null", "varchar")]
		public string Result
		{
			get { return this.m_Result; }
			set
			{
				if (this.m_Result != value)
				{
					this.m_Result = value;
					this.NotifyPropertyChanged("Result");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "'Not Correlated'", "varchar")]
		public string Correlation
		{
			get { return this.m_Correlation; }
			set
			{
				if (this.m_Correlation != value)
				{
					this.m_Correlation = value;
					this.NotifyPropertyChanged("Correlation");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "'Not Correlated'", "varchar")]
		public string CorrelationDiscrepancyType
		{
			get { return this.m_CorrelationDiscrepancyType; }
			set
			{
				if (this.m_CorrelationDiscrepancyType != value)
				{
					this.m_CorrelationDiscrepancyType = value;
					this.NotifyPropertyChanged("CorrelationDiscrepancyType");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "tinyint")]
		public bool CorrelationAffectsPatientCare
		{
			get { return this.m_CorrelationAffectsPatientCare; }
			set
			{
				if (this.m_CorrelationAffectsPatientCare != value)
				{
					this.m_CorrelationAffectsPatientCare = value;
					this.NotifyPropertyChanged("CorrelationAffectsPatientCare");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "'Not Correlated'", "varchar")]
		public string CorrelationEffectOnPatientCare
		{
			get { return this.m_CorrelationEffectOnPatientCare; }
			set
			{
				if (this.m_CorrelationEffectOnPatientCare != value)
				{
					this.m_CorrelationEffectOnPatientCare = value;
					this.NotifyPropertyChanged("CorrelationEffectOnPatientCare");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "250", "null", "varchar")]
		public string CallbackContact
		{
			get { return this.m_CallbackContact; }
			set
			{
				if (this.m_CallbackContact != value)
				{
					this.m_CallbackContact = value;
					this.NotifyPropertyChanged("CallbackContact");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "0", "tinyint")]
		public bool Final
		{
			get { return this.m_Final; }
			set
			{
				if (this.m_Final != value)
				{
					this.m_Final = value;
					this.NotifyPropertyChanged("Final");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
		public DateTime? FinalDate
		{
			get { return this.m_FinalDate; }
			set
			{
				if (this.m_FinalDate != value)
				{
					this.m_FinalDate = value;
					this.NotifyPropertyChanged("FinalDate");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
		public DateTime? FinalTime
		{
			get { return this.m_FinalTime; }
			set
			{
				if (this.m_FinalTime != value)
				{
					this.m_FinalTime = value;
					this.NotifyPropertyChanged("FinalTime");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "11", "null", "int")]
		public int? FinaledById
		{
			get { return this.m_FinaledById; }
			set
			{
				if (this.m_FinaledById != value)
				{
					this.m_FinaledById = value;
					this.NotifyPropertyChanged("FinaledById");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string FinaledBy
		{
			get { return this.m_FinaledBy; }
			set
			{
				if (this.m_FinaledBy != value)
				{
					this.m_FinaledBy = value;
					this.NotifyPropertyChanged("FinaledBy");
				}
			}
		}

        public void PullOver(Business.Visitor.AccessionTreeVisitor accessionTreeVisitor)
        {            
            accessionTreeVisitor.Visit(this);         
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public YellowstonePathology.Business.Rules.MethodResult IsOkToFinalize()
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = new Rules.MethodResult();
			if (this.m_Final == true)
			{
				methodResult.Success = false;
				methodResult.Message = "Unable to finalize the Intraoperative Consultation as it is already finaled.";
			}
			return methodResult;
		}

		public void SetFinal()
		{
			this.Final = true;
			this.FinalDate = DateTime.Today;
			this.FinalTime = DateTime.Now;
		}

		public YellowstonePathology.Business.Rules.MethodResult IsOkToUnfinalize()
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = new Rules.MethodResult();
			if (this.m_Final == false)
            {
				methodResult.Success = false;
				methodResult.Message = "Unable to unfinalize the Intraoperative Consultation as it is not finaled.";
			}
			return methodResult;
		}

		public void Unfinalize()
		{
			this.Final = false;
			this.FinalDate = null;
			this.FinalTime = null;
		}
	}
}
