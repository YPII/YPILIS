using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Xps.Packaging;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test
{
    [PersistentClass("tblPanelSetOrder", "YPIDATA")]
    public class PanelSetOrder : INotifyPropertyChanged, Interface.IPanelSetOrder
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected PanelOrderCollection m_PanelOrderCollection;
        protected YellowstonePathology.Business.Amendment.Model.AmendmentCollection m_AmendmentCollection;
		protected YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection m_PanelSetOrderCPTCodeCollection;
		protected YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBillCollection m_PanelSetOrderCPTCodeBillCollection;
        protected YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionCollection m_TestOrderReportDistributionCollection;
        protected YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionLogCollection m_TestOrderReportDistributionLogCollection;

		private string m_ObjectId;
		protected string m_ReportNo;
        protected int m_PanelSetId;
        protected string m_PanelSetName;
        protected string m_MasterAccessionNo;
        protected string m_ExternalOrderId;
        protected int m_FinaledById;
        protected bool m_Final;
        protected Nullable<DateTime> m_FinalDate;
        protected Nullable<DateTime> m_FinalTime;
        protected int m_OrderedById;
		protected string m_OrderedByInitials;
		protected Nullable<DateTime> m_OrderDate;
        protected Nullable<DateTime> m_OrderTime;
        protected int m_AcceptedById;
        protected bool m_Accepted;
        protected Nullable<DateTime> m_AcceptedDate;
        protected Nullable<DateTime> m_AcceptedTime;
		protected string m_AcceptedBy;
		protected string m_Signature;        
        protected int m_AssignedToId;
        protected int m_TemplateId;
        protected bool m_HoldBilling;
        protected bool m_Audited;
        protected int m_AuditedById;
        protected Nullable<DateTime> m_AuditedDate;
        protected string m_ResultDocumentSource;
        protected string m_ResultDocumentPath;
        protected bool m_Published;
        protected Nullable<DateTime> m_TimeLastPublished;
        protected Nullable<DateTime> m_ScheduledPublishTime;
        protected bool m_PublishNotificationSent;
        protected Nullable<DateTime> m_TimeOfLastPublishNotification;        
        protected string m_TechnicalComponentFacilityId;
        protected string m_TechnicalComponentBillingFacilityId;
        protected string m_TechnicalComponentInstrumentId;
        protected bool m_HasTechnicalComponent;
        protected string m_ProfessionalComponentFacilityId;
        protected string m_ProfessionalComponentBillingFacilityId;
        protected bool m_HasProfessionalComponent;
        protected string m_OrderedOnId;
        protected string m_OrderedOn;
        protected string m_PreparationProcedure;
        protected string m_ResultCode;
        protected bool m_NoCharge;
        protected bool m_Ordered14DaysPostDischarge;
        protected string m_BillingType;
        protected bool m_IsBillable;
        protected bool m_IsPosted;
        protected bool m_Distribute;        
        protected string m_UniversalServiceId;
        protected string m_ReferenceLabSignature;
        protected Nullable<DateTime> m_ReferenceLabFinalDate;
        protected Nullable<DateTime> m_ExpectedFinalTime;
        protected bool m_IsDelayed;
        protected string m_DelayedBy;
        protected Nullable<DateTime> m_DelayedDate;
        protected string m_DelayComment;
        protected string m_CaseType;
        protected bool m_HoldForPeerReview;
        private string m_PeerReviewRequestComment;
        private string m_PeerReviewRequestType;		       
		private string m_SignatureButtonText;
		private bool m_SignatureButtonIsEnabled;
        private bool m_HoldDistribution;
        private bool m_AdditionalTestingEmailSent;
        private string m_AdditionalTestingEmailSentBy;
        private Nullable<DateTime> m_TimeAdditionalTestingEmailSent;
        private string m_AdditionalTestingEmailMessage;
        private string m_AdditionalTestingEmailAddress;
        protected string m_ReportReferences;
        protected bool m_ResearchTesting;  

        protected YellowstonePathology.Business.Document.CaseDocumentCollection m_CaseDocumentCollection;

		protected System.Windows.Documents.FixedDocumentSequence m_PublishedDocument;
		protected string m_OrderedOnDescription;

		public PanelSetOrder()
		{
			this.m_PanelOrderCollection = new PanelOrderCollection();
			this.m_PanelSetOrderCPTCodeCollection = new PanelSetOrderCPTCodeCollection();
			this.m_PanelSetOrderCPTCodeBillCollection = new PanelSetOrderCPTCodeBillCollection();
            this.m_TestOrderReportDistributionCollection = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionCollection();
            this.m_TestOrderReportDistributionLogCollection = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionLogCollection();

            this.m_AmendmentCollection = new YellowstonePathology.Business.Amendment.Model.AmendmentCollection();					
		}

		public PanelSetOrder(string masterAccessionNo, string reportNo, string objectId, YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet, bool distribute)
		{
			this.MasterAccessionNo = masterAccessionNo;
			this.ReportNo = reportNo;
			this.m_ObjectId = objectId;
			this.m_OrderedById = Business.User.SystemIdentity.Instance.User.UserId;
			this.m_OrderedByInitials = Business.User.SystemIdentity.Instance.User.Initials;
			this.OrderDate = DateTime.Today;
			this.OrderTime = DateTime.Now;
            this.m_ResearchTesting = panelSet.ResearchTesting;
			this.m_PanelSetId = panelSet.PanelSetId;
            this.m_CaseType = panelSet.CaseType;
			this.m_PanelSetName = panelSet.PanelSetName;

            this.m_HasTechnicalComponent = false;
            if (panelSet.HasTechnicalComponent == true)
            {
                this.m_HasTechnicalComponent = true;
                this.m_TechnicalComponentFacilityId = panelSet.TechnicalComponentFacility.FacilityId;
                this.m_TechnicalComponentBillingFacilityId = panelSet.TechnicalComponentBillingFacility.FacilityId;
            }

            this.m_HasProfessionalComponent = false;
            if (panelSet.HasProfessionalComponent == true)
            {
                this.m_HasProfessionalComponent = true;
                this.m_ProfessionalComponentFacilityId = panelSet.ProfessionalComponentFacility.FacilityId;
                this.m_ProfessionalComponentBillingFacilityId = panelSet.ProfessionalComponentBillingFacility.FacilityId;
            }
			
			this.m_ResultDocumentSource = panelSet.ResultDocumentSource.ToString();
			this.m_IsBillable = panelSet.IsBillable;
            this.m_ExpectedFinalTime = YellowstonePathology.Business.Helper.DateTimeExtensions.GetEndDateConsideringWeekends(this.m_OrderTime.Value, panelSet.ExpectedDuration);

			this.m_Distribute = distribute;
			if (panelSet.NeverDistribute == true)
			{
				this.m_Distribute = false;
			}

			this.m_PanelOrderCollection = new PanelOrderCollection();
			this.m_PanelSetOrderCPTCodeCollection = new PanelSetOrderCPTCodeCollection();
			this.m_PanelSetOrderCPTCodeBillCollection = new PanelSetOrderCPTCodeBillCollection();
            this.m_TestOrderReportDistributionCollection = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionCollection();
            this.m_TestOrderReportDistributionLogCollection = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionLogCollection();

            this.m_AmendmentCollection = new YellowstonePathology.Business.Amendment.Model.AmendmentCollection();			

            YellowstonePathology.Business.ClientOrder.Model.UniversalService universalService = panelSet.UniversalServiceIdCollection.GetByApplicationName(YellowstonePathology.Business.ClientOrder.Model.UniversalServiceApplicationNameEnum.EPIC);
            this.m_UniversalServiceId = universalService.UniversalServiceId;

            this.m_Final = false;
            this.m_Accepted = false;
            this.m_HoldBilling = false;
            this.m_Audited = false;
            this.m_Published = false;
            this.m_PublishNotificationSent = false;
            this.m_NoCharge = false;
            this.m_Ordered14DaysPostDischarge = false;
            this.m_IsPosted = false;
            this.m_IsDelayed = false;
            this.m_HoldForPeerReview = false;
            this.m_HoldDistribution = false;
            this.m_AdditionalTestingEmailSent = false;
        }

        public PanelSetOrder(string masterAccessionNo, string reportNo, string objectId, YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet, YellowstonePathology.Business.Interface.IOrderTarget orderTarget, bool distribute)
		{
			this.MasterAccessionNo = masterAccessionNo;
			this.ReportNo = reportNo;
			this.m_ObjectId = objectId;
            this.m_OrderedById = Business.User.SystemIdentity.Instance.User.UserId;
            this.m_OrderedByInitials = Business.User.SystemIdentity.Instance.User.Initials;
            this.OrderDate = DateTime.Today;
			this.OrderTime = DateTime.Now;
            this.m_CaseType = panelSet.CaseType;
            this.m_ResearchTesting = panelSet.ResearchTesting;
			if (orderTarget != null)
			{
				this.m_OrderedOnId = orderTarget.GetId();
				this.m_OrderedOn = orderTarget.GetOrderedOnType();
			}

			this.m_PanelSetId = panelSet.PanelSetId;
			this.m_PanelSetName = panelSet.PanelSetName;

            this.m_HasTechnicalComponent = false;
            if (panelSet.HasTechnicalComponent == true)
			{
				this.m_HasTechnicalComponent = true;
				this.m_TechnicalComponentFacilityId = panelSet.TechnicalComponentFacility.FacilityId;
				this.m_TechnicalComponentBillingFacilityId = panelSet.TechnicalComponentBillingFacility.FacilityId;
			}

            this.m_HasProfessionalComponent = false;
            if (panelSet.HasProfessionalComponent == true)
			{
				this.m_HasProfessionalComponent = true;
				this.m_ProfessionalComponentFacilityId = panelSet.ProfessionalComponentFacility.FacilityId;
				this.m_ProfessionalComponentBillingFacilityId = panelSet.ProfessionalComponentBillingFacility.FacilityId;
			}
            
            this.m_ExpectedFinalTime = YellowstonePathology.Business.Helper.DateTimeExtensions.GetExpectedFinalTime(this.m_OrderTime.Value, panelSet.ExpectedDuration);
			this.m_IsBillable = panelSet.IsBillable;
			this.m_ResultDocumentSource = panelSet.ResultDocumentSource.ToString();

			this.m_Distribute = distribute;
			if (panelSet.NeverDistribute == true)
			{
				this.m_Distribute = false;
			}

			this.m_PanelOrderCollection = new PanelOrderCollection();
			this.m_PanelSetOrderCPTCodeCollection = new PanelSetOrderCPTCodeCollection();
			this.m_PanelSetOrderCPTCodeBillCollection = new PanelSetOrderCPTCodeBillCollection();
            this.m_TestOrderReportDistributionCollection = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionCollection();
            this.m_TestOrderReportDistributionLogCollection = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionLogCollection();

            this.m_AmendmentCollection = new YellowstonePathology.Business.Amendment.Model.AmendmentCollection();

            YellowstonePathology.Business.ClientOrder.Model.UniversalService universalService = panelSet.UniversalServiceIdCollection.GetByApplicationName(YellowstonePathology.Business.ClientOrder.Model.UniversalServiceApplicationNameEnum.EPIC);
            this.m_UniversalServiceId = universalService.UniversalServiceId;

            this.m_Final = false;
            this.m_Accepted = false;
            this.m_HoldBilling = false;
            this.m_Audited = false;
            this.m_Published = false;
            this.m_PublishNotificationSent = false;
            this.m_NoCharge = false;
            this.m_Ordered14DaysPostDischarge = false;
            this.m_IsPosted = false;
            this.m_IsDelayed = false;
            this.m_HoldForPeerReview = false;
            this.m_HoldDistribution = false;
            this.m_AdditionalTestingEmailSent = false;
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
        [PersistentDataColumnProperty(false, "20", "null", "varchar")]
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

        [PersistentProperty(true)]
        [PersistentDataColumnProperty(true, "11", "null", "int")]
        public int PanelSetId
        {
            get { return this.m_PanelSetId; }
            set
            {
                if (this.m_PanelSetId != value)
                {
                    this.m_PanelSetId = value;
                    this.NotifyPropertyChanged("PanelSetId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
        public string PanelSetName
        {
            get { return this.m_PanelSetName; }
            set
            {
                if (this.m_PanelSetName != value)
                {
                    this.m_PanelSetName = value;
                    this.NotifyPropertyChanged("PanelSetName");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set
            {
                if (this.m_MasterAccessionNo != value)
                {
                    this.m_MasterAccessionNo = value;
                    this.NotifyPropertyChanged("MasterAccessionNo");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string ExternalOrderId
        {
            get { return this.m_ExternalOrderId; }
            set
            {
                if (this.m_ExternalOrderId != value)
                {
                    this.m_ExternalOrderId = value;
                    this.NotifyPropertyChanged("ExternalOrderId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "11", "0", "int")]
        public int FinaledById
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
        [PersistentDataColumnProperty(false, "1", "0", "tinyint")]
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
        public Nullable<DateTime> FinalDate
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
        public Nullable<DateTime> FinalTime
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
        [PersistentDataColumnProperty(true, "11", "0", "int")]
        public int AcceptedById
        {
            get { return this.m_AcceptedById; }
            set
            {
                if (this.m_AcceptedById != value)
                {
                    this.m_AcceptedById = value;
                    this.NotifyPropertyChanged("AcceptedById");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool Accepted
        {
            get { return this.m_Accepted; }
            set
            {
                if (this.m_Accepted != value)
                {
                    this.m_Accepted = value;
                    this.NotifyPropertyChanged("Accepted");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> AcceptedDate
        {
            get { return this.m_AcceptedDate; }
            set
            {
                if (this.m_AcceptedDate != value)
                {
                    this.m_AcceptedDate = value;
                    this.NotifyPropertyChanged("AcceptedDate");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> AcceptedTime
        {
            get { return this.m_AcceptedTime; }
            set
            {
                if (this.m_AcceptedTime != value)
                {
                    this.m_AcceptedTime = value;
                    this.NotifyPropertyChanged("AcceptedTime");
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string AcceptedBy
		{
			get { return this.m_AcceptedBy; }
			set
			{
				if (this.m_AcceptedBy != value)
				{
					this.m_AcceptedBy = value;
					this.NotifyPropertyChanged("AcceptedBy");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "11", "0", "int")]
        public int OrderedById
        {
            get { return this.m_OrderedById; }
            set
            {
                if (this.m_OrderedById != value)
                {
                    this.m_OrderedById = value;
                    this.NotifyPropertyChanged("OrderedById");
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string OrderedByInitials
		{
			get { return this.m_OrderedByInitials; }
			set
			{
				if (this.m_OrderedByInitials != value)
				{
					this.m_OrderedByInitials = value;
					this.NotifyPropertyChanged("OrderedByInitials");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> OrderDate
        {
            get { return this.m_OrderDate; }
            set
            {
                if (this.m_OrderDate != value)
                {
                    this.m_OrderDate = value;
                    this.NotifyPropertyChanged("OrderDate");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> OrderTime
        {
            get { return this.m_OrderTime; }
            set
            {
                if (this.m_OrderTime != value)
                {
                    this.m_OrderTime = value;
                    this.NotifyPropertyChanged("OrderTime");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "150", "null", "varchar")]
        public string Signature
        {
            get { return this.m_Signature; }
            set
            {
                if (this.m_Signature != value)
                {
                    this.m_Signature = value;
                    this.NotifyPropertyChanged("Signature");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "11", "0", "int")]
        public int AssignedToId
        {
            get { return this.m_AssignedToId; }
            set
            {
                if (this.m_AssignedToId != value)
                {
                    this.m_AssignedToId = value;
                    this.NotifyPropertyChanged("AssignedToId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "11", "0", "int")]
        public int TemplateId
        {
            get { return this.m_TemplateId; }
            set
            {
                if (this.m_TemplateId != value)
                {
                    this.m_TemplateId = value;
                    this.NotifyPropertyChanged("TemplateId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool Audited
        {
            get { return this.m_Audited; }
            set
            {
                if (this.m_Audited != value)
                {
                    this.m_Audited = value;
                    this.NotifyPropertyChanged("Audited");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "11", "0", "int")]
        public int AuditedById
        {
            get { return this.m_AuditedById; }
            set
            {
                if (this.m_AuditedById != value)
                {
                    this.m_AuditedById = value;
                    this.NotifyPropertyChanged("AuditedById");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> AuditedDate
        {
            get { return this.m_AuditedDate; }
            set
            {
                if (this.m_AuditedDate != value)
                {
                    this.m_AuditedDate = value;
                    this.NotifyPropertyChanged("AuditedDate");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool HoldBilling
        {
            get { return this.m_HoldBilling; }
            set
            {
                if (this.m_HoldBilling != value)
                {
                    this.m_HoldBilling = value;
                    this.NotifyPropertyChanged("HoldBilling");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string ResultDocumentSource
        {
            get { return this.m_ResultDocumentSource; }
            set
            {
                if (this.m_ResultDocumentSource != value)
                {
                    this.m_ResultDocumentSource = value;
                    this.NotifyPropertyChanged("ResultDocumentSource");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "150", "null", "varchar")]
        public string ResultDocumentPath
        {
            get { return this.m_ResultDocumentPath; }
            set
            {
                if (this.m_ResultDocumentPath != value)
                {
                    this.m_ResultDocumentPath = value;
                    this.NotifyPropertyChanged("ResultDocumentPath");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "1", "0", "tinyint")]
        public bool Published
        {
            get { return this.m_Published; }
            set
            {
                if (this.m_Published != value)
                {
                    this.m_Published = value;
                    this.NotifyPropertyChanged("Published");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> TimeLastPublished
        {
            get { return this.m_TimeLastPublished; }
            set
            {
                if (this.m_TimeLastPublished != value)
                {
                    this.m_TimeLastPublished = value;
                    this.NotifyPropertyChanged("TimeLastPublished");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> ScheduledPublishTime
        {
            get { return this.m_ScheduledPublishTime; }
            set
            {
                if (this.m_ScheduledPublishTime != value)
                {
                    this.m_ScheduledPublishTime = value;
                    this.NotifyPropertyChanged("ScheduledPublishTime");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool PublishNotificationSent
        {
            get { return this.m_PublishNotificationSent; }
            set
            {
                if (this.m_PublishNotificationSent != value)
                {
                    this.m_PublishNotificationSent = value;
                    this.NotifyPropertyChanged("PublishNotificationSent");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> TimeOfLastPublishNotification
        {
            get { return this.m_TimeOfLastPublishNotification; }
            set
            {
                if (this.m_TimeOfLastPublishNotification != value)
                {
                    this.m_TimeOfLastPublishNotification = value;
                    this.NotifyPropertyChanged("TimeOfLastPublishNotification");
                }
            }
        }        

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string TechnicalComponentFacilityId
        {
            get { return this.m_TechnicalComponentFacilityId; }
            set
            {
                if (this.m_TechnicalComponentFacilityId != value)
                {
                    this.m_TechnicalComponentFacilityId = value;
                    this.NotifyPropertyChanged("TechnicalComponentFacilityId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string TechnicalComponentInstrumentId
        {
            get { return this.m_TechnicalComponentInstrumentId; }
            set
            {
                if (this.m_TechnicalComponentInstrumentId != value)
                {
                    this.m_TechnicalComponentInstrumentId = value;
                    this.NotifyPropertyChanged("TechnicalComponentInstrumentId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string TechnicalComponentBillingFacilityId
        {
            get { return this.m_TechnicalComponentBillingFacilityId; }
            set
            {
                if (this.m_TechnicalComponentBillingFacilityId != value)
                {
                    this.m_TechnicalComponentBillingFacilityId = value;
                    this.NotifyPropertyChanged("TechnicalComponentBillingFacilityId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "1", "0", "tinyint")]
        public bool HasTechnicalComponent
        {
            get { return this.m_HasTechnicalComponent; }
            set
            {
                if (this.m_HasTechnicalComponent != value)
                {
                    this.m_HasTechnicalComponent = value;
                    this.NotifyPropertyChanged("HasTechnicalComponent");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string ProfessionalComponentFacilityId
        {
            get { return this.m_ProfessionalComponentFacilityId; }
            set
            {
                if (this.m_ProfessionalComponentFacilityId != value)
                {
                    this.m_ProfessionalComponentFacilityId = value;
                    this.NotifyPropertyChanged("ProfessionalComponentFacilityId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string ProfessionalComponentBillingFacilityId
        {
            get { return this.m_ProfessionalComponentBillingFacilityId; }
            set
            {
                if (this.m_ProfessionalComponentBillingFacilityId != value)
                {
                    this.m_ProfessionalComponentBillingFacilityId = value;
                    this.NotifyPropertyChanged("ProfessionalComponentBillingFacilityId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "1", "0", "tinyint")]
        public bool HasProfessionalComponent
        {
            get { return this.m_HasProfessionalComponent; }
            set
            {
                if (this.m_HasProfessionalComponent != value)
                {
                    this.m_HasProfessionalComponent = value;
                    this.NotifyPropertyChanged("HasProfessionalComponent");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
        public string OrderedOnId
        {
            get { return this.m_OrderedOnId; }
            set
            {
                if (this.m_OrderedOnId != value)
                {
                    this.m_OrderedOnId = value;
                    this.NotifyPropertyChanged("OrderedOnId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
        public string OrderedOn
        {
            get { return this.m_OrderedOn; }
            set
            {
                if (this.m_OrderedOn != value)
                {
                    this.m_OrderedOn = value;
                    this.NotifyPropertyChanged("OrderedOn");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
        public string PreparationProcedure
        {
            get { return this.m_PreparationProcedure; }
            set
            {
                if (this.m_PreparationProcedure != value)
                {
                    this.m_PreparationProcedure = value;
                    this.NotifyPropertyChanged("PreparationProcedure");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string ResultCode
        {
            get { return this.m_ResultCode; }
            set
            {
                if (this.m_ResultCode != value)
                {
                    this.m_ResultCode = value;
                    this.NotifyPropertyChanged("ResultCode");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "1", "0", "tinyint")]
        public bool NoCharge
        {
            get { return this.m_NoCharge; }
            set
            {
                if (this.m_NoCharge != value)
                {
                    this.m_NoCharge = value;
                    this.NotifyPropertyChanged("NoCharge");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool Ordered14DaysPostDischarge
        {
            get { return this.m_Ordered14DaysPostDischarge; }
            set
            {
                if (this.m_Ordered14DaysPostDischarge != value)
                {
                    this.m_Ordered14DaysPostDischarge = value;
                    this.NotifyPropertyChanged("Ordered14DaysPostDischarge");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string BillingType
        {
            get { return this.m_BillingType; }
            set
            {
                if (this.m_BillingType != value)
                {
                    this.m_BillingType = value;
                    this.NotifyPropertyChanged("BillingType");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool IsBillable
        {
            get { return this.m_IsBillable; }
            set
            {
                if (this.m_IsBillable != value)
                {
                    this.m_IsBillable = value;
                    this.NotifyPropertyChanged("IsBillable");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool IsPosted
        {
            get { return this.m_IsPosted; }
            set
            {
                if (this.m_IsPosted != value)
                {
                    this.m_IsPosted = value;
                    this.NotifyPropertyChanged("IsPosted");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool Distribute
        {
            get { return this.m_Distribute; }
            set
            {
                if (this.m_Distribute != value)
                {
                    this.m_Distribute = value;
                    this.NotifyPropertyChanged("Distribute");
                }
            }
        }        

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string UniversalServiceId
        {
            get { return this.m_UniversalServiceId; }
            set
            {
                if (this.m_UniversalServiceId != value)
                {
                    this.m_UniversalServiceId = value;
                    this.NotifyPropertyChanged("UniversalServiceId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string ReferenceLabSignature
        {
            get { return this.m_ReferenceLabSignature; }
            set
            {
                if (this.m_ReferenceLabSignature != value)
                {
                    this.m_ReferenceLabSignature = value;
                    this.NotifyPropertyChanged("ReferenceLabSignature");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> ReferenceLabFinalDate
        {
            get { return this.m_ReferenceLabFinalDate; }
            set
            {
                if (this.m_ReferenceLabFinalDate != value)
                {
                    this.m_ReferenceLabFinalDate = value;
                    this.NotifyPropertyChanged("ReferenceLabFinalDate");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> ExpectedFinalTime
        {
            get { return this.m_ExpectedFinalTime; }
            set
            {
                if (this.m_ExpectedFinalTime != value)
                {
                    this.m_ExpectedFinalTime = value;
                    this.NotifyPropertyChanged("ExpectedFinalTime");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool IsDelayed
        {
            get { return this.m_IsDelayed; }
            set
            {
                if (this.m_IsDelayed != value)
                {
                    this.m_IsDelayed = value;
                    this.NotifyPropertyChanged("IsDelayed");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string DelayedBy
        {
            get { return this.m_DelayedBy; }
            set
            {
                if (this.m_DelayedBy != value)
                {
                    this.m_DelayedBy = value;
                    this.NotifyPropertyChanged("DelayedBy");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> DelayedDate
        {
            get { return this.m_DelayedDate; }
            set
            {
                if (this.m_DelayedDate != value)
                {
                    this.m_DelayedDate = value;
                    this.NotifyPropertyChanged("DelayedDate");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string DelayComment
        {
            get { return this.m_DelayComment; }
            set
            {
                if (this.m_DelayComment != value)
                {
                    this.m_DelayComment = value;
                    this.NotifyPropertyChanged("DelayComment");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool HoldForPeerReview
        {
            get { return this.m_HoldForPeerReview; }
            set
            {
                if (this.m_HoldForPeerReview != value)
                {
                    this.m_HoldForPeerReview = value;
                    this.NotifyPropertyChanged("HoldForPeerReview");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string PeerReviewRequestComment
        {
            get { return this.m_PeerReviewRequestComment; }
            set
            {
                if (this.m_PeerReviewRequestComment != value)
                {
                    this.m_PeerReviewRequestComment = value;
                    this.NotifyPropertyChanged("PeerReviewRequestComment");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string PeerReviewRequestType
        {
            get { return this.m_PeerReviewRequestType; }
            set
            {
                if (this.m_PeerReviewRequestType != value)
                {
                    this.m_PeerReviewRequestType = value;
                    this.NotifyPropertyChanged("PeerReviewRequestType");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool HoldDistribution
        {
            get { return this.m_HoldDistribution; }
            set
            {
                if (this.m_HoldDistribution != value)
                {
                    this.m_HoldDistribution = value;
                    this.NotifyPropertyChanged("HoldDistribution");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool AdditionalTestingEmailSent
        {
            get { return this.m_AdditionalTestingEmailSent; }
            set
            {
                if (this.m_AdditionalTestingEmailSent != value)
                {
                    this.m_AdditionalTestingEmailSent = value;
                    this.NotifyPropertyChanged("AdditionalTestingEmailSent");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string AdditionalTestingEmailSentBy
        {
            get { return this.m_AdditionalTestingEmailSentBy; }
            set
            {
                if (this.m_AdditionalTestingEmailSentBy != value)
                {
                    this.m_AdditionalTestingEmailSentBy = value;
                    this.NotifyPropertyChanged("AdditionalTestingEmailSentBy");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> TimeAdditionalTestingEmailSent
        {
            get { return this.m_TimeAdditionalTestingEmailSent; }
            set
            {
                if (this.m_TimeAdditionalTestingEmailSent != value)
                {
                    this.m_TimeAdditionalTestingEmailSent = value;
                    this.NotifyPropertyChanged("TimeAdditionalTestingEmailSent");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string AdditionalTestingEmailMessage
        {
            get { return this.m_AdditionalTestingEmailMessage; }
            set
            {
                if (this.m_AdditionalTestingEmailMessage != value)
                {
                    this.m_AdditionalTestingEmailMessage = value;
                    this.NotifyPropertyChanged("AdditionalTestingEmailMessage");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "200", "null", "varchar")]
        public string AdditionalTestingEmailAddress
        {
            get { return this.m_AdditionalTestingEmailAddress; }
            set
            {
                if (this.m_AdditionalTestingEmailAddress != value)
                {
                    this.m_AdditionalTestingEmailAddress = value;
                    this.NotifyPropertyChanged("AdditionalTestingEmailAddress");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string ReportReferences
        {
            get { return this.m_ReportReferences; }
            set
            {
                if (this.m_ReportReferences != value)
                {
                    this.m_ReportReferences = value;
                    this.NotifyPropertyChanged("ReportReferences");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool ResearchTesting
        {
            get { return this.m_ResearchTesting; }
            set
            {
                if (this.m_ResearchTesting != value)
                {
                    this.m_ResearchTesting = value;
                    this.NotifyPropertyChanged("ResearchTesting");
                }
            }
        }

        public virtual void DeleteChildren()
		{

		}

		public string OrderedOnDescription
		{
			get { return this.m_OrderedOnDescription; }
			set { this.m_OrderedOnDescription = value; }
		}

		public System.Windows.Documents.FixedDocumentSequence PublishedDocument
		{
			get
			{
				if (this.m_PublishedDocument == null)
				{
					YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_ReportNo);
					string publishedDocumentName = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNameXPS(orderIdParser);
					if (System.IO.File.Exists(publishedDocumentName) == true)
					{
						XpsDocument xpsDocument = new XpsDocument(publishedDocumentName, System.IO.FileAccess.Read);
						this.m_PublishedDocument = xpsDocument.GetFixedDocumentSequence();
					}
				}
				return this.m_PublishedDocument;
			}
		}

        public virtual YellowstonePathology.Business.Audit.Model.AuditResult IsOkToFinalize(Test.AccessionOrder accessionOrder)
        {
            YellowstonePathology.Business.Audit.Model.AuditResult result = new Audit.Model.AuditResult();
            result.Status = Audit.Model.AuditStatusEnum.OK;
           
            YellowstonePathology.Business.Audit.Model.AuditCollection auditCollection = new Audit.Model.AuditCollection();
            auditCollection.Add(new Audit.Model.FinalizedAudit(this));
            auditCollection.Add(new Audit.Model.MRNAudit(accessionOrder));
            auditCollection.Add(new Audit.Model.AccountNoAudit(accessionOrder));
            auditCollection.Add(new Audit.Model.DistributionCanBeSetAudit(accessionOrder));
            result = auditCollection.Run2();

            return result;
        }
        
        public virtual YellowstonePathology.Business.Rules.MethodResult IsOkToFinalize()
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
            if (this.Final == true)
            {
                result.Success = false;
                result.Message = "This case cannot be finalized because it is already finalized.";
            }
            else if (this.Accepted == false)
            {
                result.Success = false;
                result.Message = "This case cannot be finalized because the results have not been accepted.";
            }
            return result;
        }                

		public virtual void Finish(Business.Test.AccessionOrder accessionOrder)
		{            			
			YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(this.PanelSetId);

            this.m_Final = true;
            this.m_FinalDate = DateTime.Today;
            this.m_FinalTime = DateTime.Now;
            this.m_FinaledById = Business.User.SystemIdentity.Instance.User.UserId;
            this.m_Signature = Business.User.SystemIdentity.Instance.User.Signature;

			if (panelSet.AcceptOnFinal == true)
			{
				this.m_Accepted = true;
				this.m_AcceptedDate = DateTime.Today;
				this.m_AcceptedTime = DateTime.Now;
				this.m_AcceptedById = Business.User.SystemIdentity.Instance.User.UserId;
				this.m_AcceptedBy = Business.User.SystemIdentity.Instance.User.DisplayName;
			}

            YellowstonePathology.Business.Client.Model.PhysicianClientDistributionList physicianClientDistributionCollection = YellowstonePathology.Business.Gateway.ReportDistributionGateway.GetPhysicianClientDistributionCollection(accessionOrder.PhysicianId, accessionOrder.ClientId);
            physicianClientDistributionCollection.SetDistribution(this, accessionOrder);

            this.NotifyPropertyChanged(string.Empty);
		}

		public virtual void Unfinalize()
		{
			this.Final = false;
			this.FinalDate = null;
			this.FinalTime = null;
			this.FinaledById = 0;
			this.Signature = null;

			YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(this.PanelSetId);

			if (panelSet.AcceptOnFinal == true)
			{
				this.Accepted = false;
				this.AcceptedDate = null;
				this.AcceptedTime = null;
				this.AcceptedById = 0;
				this.AcceptedBy = null;
			}

            this.m_Published = false;
            this.m_ScheduledPublishTime = null;
            this.m_TestOrderReportDistributionCollection.MarkAllAsNotDistributed();
            this.m_TestOrderReportDistributionCollection.UnscheduleAll();
		}

		public virtual bool ResultsAreSet()
		{
			bool result = true;
			return result;
		}

		public virtual void SetNormalResults()
		{

		}

        public YellowstonePathology.Business.Test.Model.TestOrder GetTestOrder(string testOrderId)
        {
            YellowstonePathology.Business.Test.Model.TestOrder result = null;
            foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in this.PanelOrderCollection)
            {
                foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in panelOrder.TestOrderCollection)
                {
                    if (testOrder.TestOrderId == testOrderId)
                    {
                        result = testOrder;
                        break;
                    }
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.PanelOrder Get(string panelOrderId)
        {
            YellowstonePathology.Business.Test.PanelOrder result = null;
            foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in this.PanelOrderCollection)
            {
                if (panelOrder.PanelOrderId == panelOrderId)
                {
                    result = panelOrder;
                    break;
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.Model.TestOrderCollection GetTestOrders()
		{
			YellowstonePathology.Business.Test.Model.TestOrderCollection result = new YellowstonePathology.Business.Test.Model.TestOrderCollection();
			foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in this.PanelOrderCollection)
			{
				foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in panelOrder.TestOrderCollection)
				{
					result.Add(testOrder);
				}
			}
			return result;
		}

		public void AssignPanelOrderToBatch(int panelOrderBatchId, int batchTypeId)
		{
			foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in this.PanelOrderCollection)
			{
				panelOrder.PanelOrderBatchId = panelOrderBatchId;
			}
		}

		[PersistentCollection()]
		public PanelOrderCollection PanelOrderCollection
		{
			get { return this.m_PanelOrderCollection; }
			set { this.m_PanelOrderCollection = value; }
		}

		public ObservableCollection<YellowstonePathology.Business.Interface.IPanelOrder> PanelOrders
		{
			get
			{
				ObservableCollection<YellowstonePathology.Business.Interface.IPanelOrder> panelOrders = new ObservableCollection<Interface.IPanelOrder>();
				foreach (PanelOrder panelOrder in this.m_PanelOrderCollection)
				{
					panelOrders.Add(panelOrder);
				}
				return panelOrders;
			}
			set { throw new Exception("Not Implemented Here."); }
		}

		[PersistentCollection()]
        public YellowstonePathology.Business.Amendment.Model.AmendmentCollection AmendmentCollection
		{
			get { return this.m_AmendmentCollection; }
			set { this.m_AmendmentCollection = value; }
		}

		public bool ColorCodeVisible
		{
			get
			{
				return this.m_PanelSetId == 13;
			}
		}        

		public bool SignatureButtonIsEnabled
		{
			get { return this.m_SignatureButtonIsEnabled; }
			set
			{
				this.m_SignatureButtonIsEnabled = value;
				this.NotifyPropertyChanged("SignatureButtonIsEnabled");
			}
		}

		public string SignatureButtonText
		{
			get { return this.m_SignatureButtonText; }

			set
			{
				this.m_SignatureButtonText = value;
				NotifyPropertyChanged("SignatureButtonText");
			}
		}

		public YellowstonePathology.Business.Domain.Core.TemplateList TemplateList
		{
			get { return YellowstonePathology.Business.Domain.Core.TemplateList.Instance; }
		}

		public int CurrentTemplateId
		{
			get { return this.TemplateId; }
			set
			{
				this.TemplateId = value;
				NotifyPropertyChanged("CurrentTemplateId");
			}
		}				

		public virtual string SignatureFromFinalById
		{
			get
			{
				string signature = string.Empty;
				if (Final && FinaledById > 0)
				{
					YellowstonePathology.Business.User.SystemUser systemUserItem = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(FinaledById);
					signature = systemUserItem.Signature;
				}
				return signature;
			}
		}


        public bool IsInCriticalState
        {
            get
            {
                bool result = false;
                if (this.m_Final == false && this.m_ExpectedFinalTime < DateTime.Now)
                {
                    result = true;
                }
                return result;
            }
        }


		[PersistentCollection()]
		public YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection PanelSetOrderCPTCodeCollection
		{
			get { return this.m_PanelSetOrderCPTCodeCollection; }
			set { this.m_PanelSetOrderCPTCodeCollection = value; }
		}

		[PersistentCollection()]
		public YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBillCollection PanelSetOrderCPTCodeBillCollection
		{
			get { return this.m_PanelSetOrderCPTCodeBillCollection; }
			set { this.m_PanelSetOrderCPTCodeBillCollection = value; }
		}

        [PersistentCollection()]
        public YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionCollection TestOrderReportDistributionCollection
        {
            get { return this.m_TestOrderReportDistributionCollection; }
            set { this.m_TestOrderReportDistributionCollection = value; }
        }

        [PersistentCollection()]
        public YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionLogCollection TestOrderReportDistributionLogCollection
        {
            get { return this.m_TestOrderReportDistributionLogCollection; }
            set { this.m_TestOrderReportDistributionLogCollection = value; }
        }

		public string GetAliquotIdFromTestId(int testId)
		{
			string result = string.Empty;
			foreach (PanelOrder panelOrder in this.PanelOrderCollection)
			{
                if (panelOrder.TestOrderCollection.Exists(testId) == true)
                {
                    YellowstonePathology.Business.Test.Model.TestOrder testOrder = panelOrder.TestOrderCollection.GetTestOrder(testId);
                    result = testOrder.AliquotOrderId;
                }				
			}
			return result;
		}		

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public YellowstonePathology.Business.Document.CaseDocumentCollection CaseDocumentCollection
		{
			get { return this.m_CaseDocumentCollection; }
			set
			{
				this.m_CaseDocumentCollection = value;
				NotifyPropertyChanged("CaseDocumentCollection");
			}
		}

        public void SchedulePublish(Nullable<DateTime> timeToSchedule)
        {
            this.m_Published = false;
            this.m_ScheduledPublishTime = timeToSchedule;
            this.NotifyPropertyChanged(string.Empty);
        }

        public void UnSchedulePublish()
        {
            if (this.m_TimeLastPublished.HasValue == true)
            {
                this.m_Published = true;
            }
            this.m_ScheduledPublishTime = null;
            this.NotifyPropertyChanged(string.Empty);
        }

        public virtual YellowstonePathology.Business.Amendment.Model.Amendment AddAmendment()
		{
			string amendmentId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.Amendment.Model.Amendment amendment = this.m_AmendmentCollection.GetNextItem(this.m_ReportNo, amendmentId, amendmentId);
			this.m_AmendmentCollection.Add(amendment);
			return amendment;
		}

		public virtual void DeleteAmendment(string amendmentId)
		{
            YellowstonePathology.Business.Amendment.Model.Amendment amendment = this.m_AmendmentCollection.GetAmendment(amendmentId);
			this.m_AmendmentCollection.Remove(amendment);
		}

		public virtual string GetLocationPerformedComment()
		{
			StringBuilder result = new StringBuilder();

			YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = YellowstonePathology.Business.Facility.Model.FacilityCollection.GetAllFacilities();
			YellowstonePathology.Business.Facility.Model.Facility technicalComponentFacility = new YellowstonePathology.Business.Facility.Model.NullFacility();
			YellowstonePathology.Business.Facility.Model.Facility professionalComponentFacility = new YellowstonePathology.Business.Facility.Model.NullFacility();

			if (this.HasTechnicalComponent == true)
			{
				technicalComponentFacility = facilityCollection.GetByFacilityId(this.m_TechnicalComponentFacilityId);
			}

			if (this.HasProfessionalComponent == true)
			{
				professionalComponentFacility = facilityCollection.GetByFacilityId(this.m_ProfessionalComponentFacilityId);
			}

			if (technicalComponentFacility.CLIALicense.LicenseNumber == professionalComponentFacility.CLIALicense.LicenseNumber)
			{
				if (this.HasTechnicalComponent == true && this.HasProfessionalComponent == false)
				{
					result.Append("Technical component(s) performed at ");
					result.Append(technicalComponentFacility.CLIALicense.GetAddressString());
				}
				else if (this.HasProfessionalComponent == true && this.HasTechnicalComponent == false)
				{
					if (result.Length != 0) result.Append(" ");
					result.Append("Professional component(s) performed at ");
					result.Append(professionalComponentFacility.CLIALicense.GetAddressString());
				}
				else if (this.HasProfessionalComponent == true && this.HasTechnicalComponent == true)
				{
					result.Append("Technical and professional component(s) performed at ");
					result.Append(professionalComponentFacility.CLIALicense.GetAddressString());
				}
			}
			else
			{
				if (this.HasTechnicalComponent == true)
				{
					result.Append("Technical component(s) performed at ");
					result.Append(technicalComponentFacility.CLIALicense.GetAddressString());
				}

				if (this.HasProfessionalComponent == true)
				{
					if (result.Length != 0) result.Append(" ");
					result.Append("Professional component(s) performed at ");
					result.Append(professionalComponentFacility.CLIALicense.GetAddressString());
				}
			}

			return result.ToString();
		}
        
		public virtual string GetResultWithTestName()
		{
			return null;
		}

		public YellowstonePathology.Business.Test.Model.TestOrderCollection GetTestOrderCollection(YellowstonePathology.Business.Test.AliquotOrderCollection aliquotOrderCollection)
		{
			YellowstonePathology.Business.Test.Model.TestOrderCollection result = new YellowstonePathology.Business.Test.Model.TestOrderCollection();
			foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in this.PanelOrderCollection)
			{
				foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in panelOrder.TestOrderCollection)
				{
                    if (aliquotOrderCollection.Exists(testOrder.AliquotOrderId) == true)
					{
						result.Add(testOrder);
					}					
				}
			}
			return result;
		}

        public YellowstonePathology.Business.Test.Model.TestOrderCollection GetTestOrderCollection(string aliquotOrderId)
        {
            YellowstonePathology.Business.Test.Model.TestOrderCollection result = new YellowstonePathology.Business.Test.Model.TestOrderCollection();
            foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in this.PanelOrderCollection)
            {
                foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in panelOrder.TestOrderCollection)
                {
                    if (testOrder.AliquotOrderId == aliquotOrderId)
                    {
                        result.Add(testOrder);
                    }                    
                }
            }
            return result;
        }

		public bool IsReferenceLabTest()
		{
			bool result = false;
			YellowstonePathology.Business.Facility.Model.FacilityCollection allYPIFacilities = YellowstonePathology.Business.Facility.Model.FacilityCollection.GetAllYPFacilities();
			if (this.HasProfessionalComponent == true && allYPIFacilities.Exists(this.ProfessionalComponentFacilityId) == false) result = true;
			if (this.HasTechnicalComponent == true && allYPIFacilities.Exists(this.TechnicalComponentFacilityId) == false) result = true;
			return result;
		}        

		public virtual bool IsOkToAddTasks()
		{
			return true;
		}		

		public virtual string ToResultString(AccessionOrder accessionOrder)
		{
			return "The result string for this test has not been implemented.";
		}

		public void Accept()
		{
			this.m_Accepted = true;
            this.m_AcceptedById = Business.User.SystemIdentity.Instance.User.UserId;
			this.m_AcceptedBy = Business.User.SystemIdentity.Instance.User.DisplayName;
            this.m_AcceptedDate = DateTime.Today;
			this.m_AcceptedTime = DateTime.Now;
			this.NotifyPropertyChanged(string.Empty);
		}

		public void Unaccept()
		{
			this.m_Accepted = false;
			this.m_AcceptedById = 0;
			this.m_AcceptedBy = null;
			this.m_AcceptedDate = null;
			this.m_AcceptedTime = null;
			this.NotifyPropertyChanged(string.Empty);
		}

		public virtual YellowstonePathology.Business.Rules.MethodResult IsOkToAccept()
		{
			YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
			if (this.Accepted == true)
			{
				result.Success = false;
				result.Message = "The results cannot be accepted because they are already accepted.";
			}            
			return result;
		}

		public virtual YellowstonePathology.Business.Rules.MethodResult IsOkToUnaccept()
		{
			YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
			if (this.Final == true)
			{
				result.Success = false;
				result.Message = "The results cannot be unaccepted because the case is final.";
			}
			else if (this.Accepted == false)
			{
				result.Success = false;
				result.Message = "The results cannot be unaccepted because they are not accepted.";
			}
			return result;
		}		

		public virtual YellowstonePathology.Business.Rules.MethodResult IsOkToUnfinalize()
		{
			YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
			if (this.Final == false)
			{
				result.Success = false;
				result.Message = "This case cannot be unfinalized because it is not final.";
			}
			return result;
		}                

        public virtual void PullOver(YellowstonePathology.Business.Visitor.AccessionTreeVisitor accessionTreeVisitor)
        {
            accessionTreeVisitor.Visit(this);
        }
        
        public Rules.MethodResult HaveResultsBeenSet(AccessionOrder accessionOrder)
        {
            YellowstonePathology.Business.Persistence.ObjectCloner objectCloner = new Business.Persistence.ObjectCloner();
            object clone = objectCloner.Clone(this);
            YellowstonePathology.Business.Persistence.DocumentId documentId = new Business.Persistence.DocumentId(clone, this);
            YellowstonePathology.Business.Persistence.DocumentUpdate document = new Business.Persistence.DocumentUpdate(documentId);
            Rules.MethodResult result = new Rules.MethodResult();
            this.CheckResults(accessionOrder, clone);
            if(document.IsDirty() == true)
            {
                result.Success = false;
                result.Message = "Results have not been set.";
            }
            return result;
        }

        protected virtual void CheckResults(AccessionOrder accessionOrder, object clone)
        {
        }
    }
}
