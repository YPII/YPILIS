using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.Surgical
{
	[PersistentClass("tblSurgicalSpecimen", true, "YPIDATA")]
	public class SurgicalSpecimen : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private string m_SurgicalSpecimenId;
		private string m_ReportNo;
		private bool m_Report;
		private bool m_ImmediatePerformed;
		private Nullable<DateTime> m_ImmediateStartTime;
		private Nullable<DateTime> m_ImmediateEndTime;
		private string m_SpecimenOrderId;
		private int m_DiagnosisId;
		private int m_ImmediateCorrelation;
		private int m_ImmediatePerformedById;
		private string m_Diagnosis;
		private string m_SpecimenType;
		private string m_RescreenStatus;
		private string m_ImmediatePerformedBy;

		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
		private YellowstonePathology.Business.SpecialStain.StainResultItemCollection m_StainResultItemCollection;		
		private YellowstonePathology.Business.Billing.Model.ICD9BillingCodeCollection m_ICD9BillingCodeCollection;
		private IntraoperativeConsultationResultCollection m_IntraoperativeConsultationResultCollection;

		public SurgicalSpecimen()
        {
			this.m_StainResultItemCollection = new YellowstonePathology.Business.SpecialStain.StainResultItemCollection();
			this.m_IntraoperativeConsultationResultCollection = new IntraoperativeConsultationResultCollection();			
			this.m_ICD9BillingCodeCollection = new YellowstonePathology.Business.Billing.Model.ICD9BillingCodeCollection();
		}

		public SurgicalSpecimen(string reportNo, string objectId, string surgicalSpecimenId)
		{
			this.m_ReportNo = reportNo;
			this.m_ObjectId = objectId;
			this.m_SurgicalSpecimenId = surgicalSpecimenId;
			this.m_StainResultItemCollection = new YellowstonePathology.Business.SpecialStain.StainResultItemCollection();
			this.m_IntraoperativeConsultationResultCollection = new IntraoperativeConsultationResultCollection();
			this.m_ICD9BillingCodeCollection = new YellowstonePathology.Business.Billing.Model.ICD9BillingCodeCollection();
		}

		public void FromSpecimenOrder(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
		{
			this.m_SpecimenOrderId = specimenOrder.SpecimenOrderId;
			this.m_DiagnosisId = specimenOrder.SpecimenNumber;
			this.m_SpecimenOrder = specimenOrder;
		}

		public string GetBlockFromTestOrderId(string testOrderId)
        {
            string result = string.Empty;
            foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in this.SpecimenOrder.AliquotOrderCollection)
            {
                if (aliquotOrder.TestOrderCollection.Exists(testOrderId) == true)
                {
                    result = aliquotOrder.Label;
                }                
            }
            return result;
        }

		public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
		{
			get { return m_SpecimenOrder; }
			set { m_SpecimenOrder = value; }
		}

		[PersistentCollection()]
		public YellowstonePathology.Business.SpecialStain.StainResultItemCollection StainResultItemCollection
		{
			get { return m_StainResultItemCollection; }
            set { this.m_StainResultItemCollection = value; }
		}		

		[PersistentCollection()]
		public YellowstonePathology.Business.Billing.Model.ICD9BillingCodeCollection ICD9BillingCodeCollection
		{
			get { return m_ICD9BillingCodeCollection; }
            set { this.m_ICD9BillingCodeCollection = value; }
            
		}

		[PersistentCollection()]
		public IntraoperativeConsultationResultCollection IntraoperativeConsultationResultCollection
		{
			get { return m_IntraoperativeConsultationResultCollection; }
            set { this.m_IntraoperativeConsultationResultCollection = value; }
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
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
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

        public virtual void PullOver(YellowstonePathology.Business.Visitor.AccessionTreeVisitor accessionTreeVisitor)
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
	}
}
