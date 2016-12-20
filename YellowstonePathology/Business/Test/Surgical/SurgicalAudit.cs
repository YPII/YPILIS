using System;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.Surgical
{
	[PersistentClass("tblSurgicalAudit", "YPIDATA")]
	public class SurgicalAudit : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private string m_SurgicalAuditId;
		private string m_ReportNo;		
		private bool m_PapCorrelationRequired;
		private bool m_ReportableCase;
		private bool m_PQRIRequired;		
		private string m_AmendmentId;		
		private int m_ImmediateCorrelation;
		private int m_PapCorrelation;
		private int m_PQRIInstructions;
		private string m_ClinicalInfo;
		private string m_GrossX;
		private string m_ImmediateX;
		private string m_MicroscopicX;
		private string m_Comment;
		private string m_CancerSummary;
		private string m_Status;
		private string m_CaseType;
		private string m_ImmunoComment;
		private string m_LocumPerformedForInitials;
		private string m_AJCCStage;
		private string m_ImmediateCorrelationComment;
		private string m_PapCorrelationComment;
		private string m_PapCorrelationAccessionNo;
        private int m_PathologistId;
	
		private SurgicalSpecimenAuditCollection m_SurgicalSpecimenAuditCollection;
		private YellowstonePathology.Business.Amendment.Model.Amendment m_Amendment;

		public SurgicalAudit()
		{
			this.m_SurgicalSpecimenAuditCollection = new SurgicalSpecimenAuditCollection();
		}

		public SurgicalAudit(string objectId, string surgicalAuditId, string amendmentId, YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrderSurgical, int pathologistId, int assignedToId)
		{
			this.m_SurgicalSpecimenAuditCollection = new SurgicalSpecimenAuditCollection();

			this.m_ObjectId = objectId;
			this.SurgicalAuditId = surgicalAuditId;
			this.ReportNo = panelSetOrderSurgical.ReportNo;
			this.AmendmentId = amendmentId;
            this.PathologistId = pathologistId;
			this.ClinicalInfo = "None Provided";
			this.GrossX = panelSetOrderSurgical.GrossX;
			this.ImmediateX = panelSetOrderSurgical.ImmediateX;
			this.MicroscopicX = panelSetOrderSurgical.MicroscopicX;
			this.Comment = panelSetOrderSurgical.Comment;					
			this.ImmunoComment = panelSetOrderSurgical.ImmunoComment;
			this.ImmediateCorrelation = panelSetOrderSurgical.ImmediateCorrelation;
			this.ImmediateCorrelationComment = panelSetOrderSurgical.ImmediateCorrelationComment;
			this.PapCorrelation = panelSetOrderSurgical.PapCorrelation;
			this.PapCorrelationComment = panelSetOrderSurgical.PapCorrelationComment;
			this.PapCorrelationRequired = panelSetOrderSurgical.PapCorrelationRequired;
			this.PapCorrelationAccessionNo = panelSetOrderSurgical.PapCorrelationAccessionNo;
			this.LocumPerformedForInitials = panelSetOrderSurgical.LocumPerformedForInitials;
			this.ReportableCase = panelSetOrderSurgical.ReportableCase;
			this.CancerSummary = panelSetOrderSurgical.CancerSummary;
			this.AJCCStage = panelSetOrderSurgical.AJCCStage;
			this.PQRIRequired = panelSetOrderSurgical.PQRIRequired;
			this.PQRIInstructions = panelSetOrderSurgical.PQRIInstructions;
			this.Status = panelSetOrderSurgical.Status;
			this.CaseType = panelSetOrderSurgical.CaseType;
            
		}

		[PersistentCollection()]
		public SurgicalSpecimenAuditCollection SurgicalSpecimenAuditCollection
		{
			get { return m_SurgicalSpecimenAuditCollection; }
			set { this.m_SurgicalSpecimenAuditCollection = value; }
		}

        public YellowstonePathology.Business.Amendment.Model.Amendment Amendment
        {
            get { return this.m_Amendment; }
            set { this.m_Amendment = value; }
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
		public bool PapCorrelationRequired
		{
			get { return this.m_PapCorrelationRequired; }
			set
			{
				if (this.m_PapCorrelationRequired != value)
				{
					this.m_PapCorrelationRequired = value;
					this.NotifyPropertyChanged("PapCorrelationRequired");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "tinyint")]
		public bool ReportableCase
		{
			get { return this.m_ReportableCase; }
			set
			{
				if (this.m_ReportableCase != value)
				{
					this.m_ReportableCase = value;
					this.NotifyPropertyChanged("ReportableCase");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "tinyint")]
		public bool PQRIRequired
		{
			get { return this.m_PQRIRequired; }
			set
			{
				if (this.m_PQRIRequired != value)
				{
					this.m_PQRIRequired = value;
					this.NotifyPropertyChanged("PQRIRequired");
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
		public int PapCorrelation
		{
			get { return this.m_PapCorrelation; }
			set
			{
				if (this.m_PapCorrelation != value)
				{
					this.m_PapCorrelation = value;
					this.NotifyPropertyChanged("PapCorrelation");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "11", "0", "int")]
		public int PQRIInstructions
		{
			get { return this.m_PQRIInstructions; }
			set
			{
				if (this.m_PQRIInstructions != value)
				{
					this.m_PQRIInstructions = value;
					this.NotifyPropertyChanged("PQRIInstructions");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "-1", "null", "text")]
		public string ClinicalInfo
		{
			get { return this.m_ClinicalInfo; }
			set
			{
				if (this.m_ClinicalInfo != value)
				{
					this.m_ClinicalInfo = value;
					this.NotifyPropertyChanged("ClinicalInfo");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "-1", "null", "text")]
		public string GrossX
		{
			get { return this.m_GrossX; }
			set
			{
				if (this.m_GrossX != value)
				{
					this.m_GrossX = value;
					this.NotifyPropertyChanged("GrossX");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "-1", "null", "text")]
		public string ImmediateX
		{
			get { return this.m_ImmediateX; }
			set
			{
				if (this.m_ImmediateX != value)
				{
					this.m_ImmediateX = value;
					this.NotifyPropertyChanged("ImmediateX");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "-1", "null", "text")]
		public string MicroscopicX
		{
			get { return this.m_MicroscopicX; }
			set
			{
				if (this.m_MicroscopicX != value)
				{
					this.m_MicroscopicX = value;
					this.NotifyPropertyChanged("MicroscopicX");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "-1", "null", "text")]
		public string Comment
		{
			get { return this.m_Comment; }
			set
			{
				if (this.m_Comment != value)
				{
					this.m_Comment = value;
					this.NotifyPropertyChanged("Comment");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "-1", "null", "text")]
		public string CancerSummary
		{
			get { return this.m_CancerSummary; }
			set
			{
				if (this.m_CancerSummary != value)
				{
					this.m_CancerSummary = value;
					this.NotifyPropertyChanged("CancerSummary");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "10", "'Open'", "varchar")]
		public string Status
		{
			get { return this.m_Status; }
			set
			{
				if (this.m_Status != value)
				{
					this.m_Status = value;
					this.NotifyPropertyChanged("Status");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "20", "'Surgical'", "varchar")]
		public string CaseType
		{
			get { return this.m_CaseType; }
			set
			{
				if (this.m_CaseType != value)
				{
					this.m_CaseType = value;
					this.NotifyPropertyChanged("CaseType");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "-1", "null", "text")]
		public string ImmunoComment
		{
			get { return this.m_ImmunoComment; }
			set
			{
				if (this.m_ImmunoComment != value)
				{
					this.m_ImmunoComment = value;
					this.NotifyPropertyChanged("ImmunoComment");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string LocumPerformedForInitials
		{
			get { return this.m_LocumPerformedForInitials; }
			set
			{
				if (this.m_LocumPerformedForInitials != value)
				{
					this.m_LocumPerformedForInitials = value;
					this.NotifyPropertyChanged("LocumPerformedForInitials");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string AJCCStage
		{
			get { return this.m_AJCCStage; }
			set
			{
				if (this.m_AJCCStage != value)
				{
					this.m_AJCCStage = value;
					this.NotifyPropertyChanged("AJCCStage");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "250", "null", "varchar")]
		public string ImmediateCorrelationComment
		{
			get { return this.m_ImmediateCorrelationComment; }
			set
			{
				if (this.m_ImmediateCorrelationComment != value)
				{
					this.m_ImmediateCorrelationComment = value;
					this.NotifyPropertyChanged("ImmediateCorrelationComment");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string PapCorrelationComment
		{
			get { return this.m_PapCorrelationComment; }
			set
			{
				if (this.m_PapCorrelationComment != value)
				{
					this.m_PapCorrelationComment = value;
					this.NotifyPropertyChanged("PapCorrelationComment");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string PapCorrelationAccessionNo
		{
			get { return this.m_PapCorrelationAccessionNo; }
			set
			{
				if (this.m_PapCorrelationAccessionNo != value)
				{
					this.m_PapCorrelationAccessionNo = value;
					this.NotifyPropertyChanged("PapCorrelationAccessionNo");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "11", "0", "int")]
        public int PathologistId
        {
            get { return this.m_PathologistId; }
            set
            {
                if (this.m_PathologistId != value)
                {
                    this.m_PathologistId = value;
                    this.NotifyPropertyChanged("PathologistId");
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
