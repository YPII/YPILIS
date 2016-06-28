using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using System.Collections.ObjectModel;
using YellowstonePathology.Business.Persistence;
using YellowstonePathology.Business.Audit.Model;
using YellowstonePathology.Business.User;
using YellowstonePathology.Business.Rules;

namespace YellowstonePathology.Business.Test.Surgical
{
	[PersistentClass("tblSurgicalTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class SurgicalTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
		private bool m_PapCorrelationRequired;
		private bool m_ReportableCase;
		private bool m_PQRIRequired;
		private int m_PhysicianId;
		private int m_ImmediateCorrelation;
		private int m_PapCorrelation;
		private int m_PQRIInstructions;		
		private string m_GrossX;
		private string m_ImmediateX;
		private string m_MicroscopicX;
		private string m_Comment;
		private string m_CancerSummary;
		private string m_Status;		
		private string m_ImmunoComment;
		private string m_LocumPerformedForInitials;
		private string m_AJCCStage;
		private string m_ImmediateCorrelationComment;
		private string m_PapCorrelationComment;
		private string m_PapCorrelationAccessionNo;
        private bool m_PQRSIsIndicated;
        private bool m_PQRSNotApplicable;

		private SurgicalSpecimenCollection m_SurgicalSpecimenCollection;
		private YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection m_SpecimenOrderCollection;
		private YellowstonePathology.Business.SpecialStain.StainResultItemCollection m_TypingStainCollection;
		private SurgicalAuditCollection m_SurgicalAuditCollection;		

		public SurgicalTestOrder()
		{
            this.m_SurgicalSpecimenCollection = new SurgicalSpecimenCollection();
            this.m_SurgicalAuditCollection = new SurgicalAuditCollection();
            this.m_TypingStainCollection = new SpecialStain.StainResultItemCollection();
            this.m_SpecimenOrderCollection = new YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection();
		}

		public SurgicalTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, distribute)
		{
			this.m_SurgicalSpecimenCollection = new SurgicalSpecimenCollection();
            this.m_SurgicalAuditCollection = new SurgicalAuditCollection();
            this.m_TypingStainCollection = new SpecialStain.StainResultItemCollection();
            this.m_SpecimenOrderCollection = new YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection();			
		}

        public SurgicalTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, distribute)
        {
            this.m_SurgicalSpecimenCollection = new SurgicalSpecimenCollection();
            this.m_SurgicalAuditCollection = new SurgicalAuditCollection();
            this.m_TypingStainCollection = new SpecialStain.StainResultItemCollection();
            this.m_SpecimenOrderCollection = new YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection();
        }

        public override YellowstonePathology.Business.Amendment.Model.Amendment AddAmendment()
		{
            YellowstonePathology.Business.Amendment.Model.Amendment amendment = base.AddAmendment();
			SurgicalAudit surgicalAudit = this.m_SurgicalAuditCollection.GetNextItem(amendment.AmendmentId, this, this.m_AssignedToId, this.AssignedToId);

			foreach (SurgicalSpecimen surgicalSpecimen in this.m_SurgicalSpecimenCollection)
			{
				YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenAudit surgicalSpecimenAudit = surgicalAudit.SurgicalSpecimenAuditCollection.GetNextItem(surgicalAudit.SurgicalAuditId, surgicalSpecimen, amendment.AmendmentId);
				surgicalAudit.SurgicalSpecimenAuditCollection.Add(surgicalSpecimenAudit);
			}

			this.m_SurgicalAuditCollection.Add(surgicalAudit);
			return amendment;
		}

		public override void DeleteAmendment(string amendmentId)
		{
			base.DeleteAmendment(amendmentId);

			foreach (SurgicalAudit surgicalAudit in this.m_SurgicalAuditCollection)
			{
				if (surgicalAudit.AmendmentId == amendmentId)
				{
					this.m_SurgicalAuditCollection.Remove(surgicalAudit);
					break;
				}
			}
		}

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();
			foreach (SurgicalSpecimen surgicalSpecimenResult in this.SurgicalSpecimenCollection)
			{
				result.AppendLine(surgicalSpecimenResult.Diagnosis);
			}

			return result.ToString();
		}

		public YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection SpecimenOrderCollection
		{
			get { return m_SpecimenOrderCollection; }
			set { m_SpecimenOrderCollection = value; }
		}

		[PersistentCollection()]
		public YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenCollection SurgicalSpecimenCollection
		{
			get { return m_SurgicalSpecimenCollection; }
			set { this.m_SurgicalSpecimenCollection = value; }
		}

		[PersistentCollection()]
		public SurgicalAuditCollection SurgicalAuditCollection
		{
			get { return m_SurgicalAuditCollection; }
			set { this.m_SurgicalAuditCollection = value; }
		}

		[PersistentProperty()]
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
		public int PhysicianId
		{
			get { return this.m_PhysicianId; }
			set
			{
				if (this.m_PhysicianId != value)
				{
					this.m_PhysicianId = value;
					this.NotifyPropertyChanged("PhysicianId");
				}
			}
		}

		[PersistentProperty()]
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
        public bool PQRSIsIndicated
        {
            get { return this.m_PQRSIsIndicated; }
            set
            {
                if (this.m_PQRSIsIndicated != value)
                {
                    this.m_PQRSIsIndicated = value;
                    this.NotifyPropertyChanged("PQRSIsIndicated");
                }
            }
        }

        [PersistentProperty()]
        public bool PQRSNotApplicable
        {
            get { return this.m_PQRSNotApplicable; }
            set
            {
                if (this.m_PQRSNotApplicable != value)
                {
                    this.m_PQRSNotApplicable = value;
                    this.NotifyPropertyChanged("PQRSNotApplicable");
                }
            }
        }

        public YellowstonePathology.Business.SpecialStain.StainResultItemCollection TypingStainCollection
		{
			get	{ return this.m_TypingStainCollection; }
		}

		public string GetAncillaryStudyComment()
		{
			string comment = string.Empty;
			int cytoCnt = 0;
			int immunoCnt = 0;
			foreach (SurgicalSpecimen surgicalSpecimen in this.m_SurgicalSpecimenCollection)
			{
				foreach (YellowstonePathology.Business.SpecialStain.StainResultItem stainResultItem in surgicalSpecimen.StainResultItemCollection)
				{
					switch (stainResultItem.StainType.ToUpper())
					{
						case "CYTOCHEMICAL":
							cytoCnt += 1;
							break;
						case "IMMUNOHISTOCHEMICAL":
							immunoCnt += 1;
							break;
						default:
							break;
					}
				}
			}

			if (cytoCnt == 1 && immunoCnt == 0)
				comment = "A cytochemical study was performed, with appropriately reacting controls. Results are listed below.";
			if (cytoCnt > 1 && immunoCnt == 0)
				comment = "Cytochemical studies were performed, with appropriately reacting controls. Results are listed below.";
			if (cytoCnt > 0 && immunoCnt > 0)
				comment = "Cytochemical and immunohistochemical studies were performed, with appropriately reacting controls. Results are listed below.";
			if (cytoCnt == 0 && immunoCnt == 1)
				comment = "An immunohistochemical study was performed, with appropriately reacting controls. Results are listed below.";
			if (cytoCnt == 0 && immunoCnt > 1)
				comment = "Immunohistochemical studies were performed, with appropriately reacting controls. Results are listed below.";
			return comment;
		}

		public string GetImmunoComment()
		{
			string ret = string.Empty;
            List<string> comments = new List<string>();
			
			foreach (SurgicalSpecimen surgicalSpecimen in this.m_SurgicalSpecimenCollection)
			{
				foreach (YellowstonePathology.Business.SpecialStain.StainResultItem stainResultItem in surgicalSpecimen.StainResultItemCollection)
				{
                    if (comments.Contains(stainResultItem.ImmunoComment) == false)
                    {
                        comments.Add(stainResultItem.ImmunoComment);
                    }                    
				}
			}

			foreach (string comment in comments)
			{
				ret += comment + " ";
			}

			return ret.Trim();
		}

		public class ImmunoCommentsListItem
		{
			private int m_Seq;
			private string m_Comment;

			public ImmunoCommentsListItem(int seq, string comment)
			{
				m_Seq = seq;
				m_Comment = comment;
			}

			public int Sequence
			{
				get { return this.m_Seq; }
				set { this.m_Seq = value; }
			}

			public string Comment
			{
				get { return this.m_Comment; }
				set { m_Comment = value; }
			}
		}

		public void SetProcedureCommentsToGood()
		{
			foreach (YellowstonePathology.Business.SpecialStain.StainResultItem stainResultItem in this.TypingStainCollection)
			{
				if (string.IsNullOrEmpty(stainResultItem.ProcedureComment) == true)
				{
					stainResultItem.ProcedureComment = "Good.";
				}
			}
		}

		public bool ContainsString(string text)
		{
			return this.m_SurgicalSpecimenCollection.ContainsString(text);
		}

		public YellowstonePathology.Business.SpecialStain.StainResultItemCollection GetAllStains()
		{
			YellowstonePathology.Business.SpecialStain.StainResultItemCollection result = new YellowstonePathology.Business.SpecialStain.StainResultItemCollection();
			foreach (SurgicalSpecimen surgicalSpecimen in this.m_SurgicalSpecimenCollection)
			{
				foreach (YellowstonePathology.Business.SpecialStain.StainResultItem stainResultItem in surgicalSpecimen.StainResultItemCollection)
				{
					result.Add(stainResultItem);
				}
			}
			return result;
		}

		public YellowstonePathology.Business.SpecialStain.StainResultItem GetStainResult(string testOrderId)
		{
			YellowstonePathology.Business.SpecialStain.StainResultItem result = null;
			foreach (SurgicalSpecimen surgicalSpecimen in this.m_SurgicalSpecimenCollection)
			{
				foreach (YellowstonePathology.Business.SpecialStain.StainResultItem stainResult in surgicalSpecimen.StainResultItemCollection)
				{
					if (stainResult.TestOrderId == testOrderId)
					{
						result = stainResult;
						break;
					}
				}
			}
			return result;
		}		

        public override void PullOver(Business.Visitor.AccessionTreeVisitor accessionTreeVisitor)
        {
            base.PullOver(accessionTreeVisitor);
            accessionTreeVisitor.Visit(this);

            this.TakeASideTrip(accessionTreeVisitor);
        }

        public void TakeASideTrip(Business.Visitor.AccessionTreeVisitor accessionTreeVisitor)
        {
            this.m_SurgicalSpecimenCollection.PullOver(accessionTreeVisitor);
            foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in this.m_SurgicalSpecimenCollection)
            {                
                surgicalSpecimen.PullOver(accessionTreeVisitor);
                surgicalSpecimen.StainResultItemCollection.PullOver(accessionTreeVisitor);

                foreach (YellowstonePathology.Business.SpecialStain.StainResultItem stainResult in surgicalSpecimen.StainResultItemCollection)
                {
                    stainResult.PullOver(accessionTreeVisitor);
                }

                surgicalSpecimen.IntraoperativeConsultationResultCollection.PullOver(accessionTreeVisitor);

                foreach (YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult intraoperativeConsultationResult in surgicalSpecimen.IntraoperativeConsultationResultCollection)
                {
                    intraoperativeConsultationResult.PullOver(accessionTreeVisitor);
                }
            }
        }

        public override AuditResult IsOkToFinalize(AccessionOrder accessionOrder)
        {
            Audit.Model.PathologistSignoutAuditCollection pathologistSignoutAuditCollection = new PathologistSignoutAuditCollection(accessionOrder);
            AuditResult auditResult = pathologistSignoutAuditCollection.Run2();
            return auditResult;
        }

        public override void Finish(Business.Test.AccessionOrder accessionOrder)
        {
            this.m_ProfessionalComponentFacilityId = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId;
            base.Finish(accessionOrder);
        }
    }
}
