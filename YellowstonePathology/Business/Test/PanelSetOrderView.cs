using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test
{
    //[PersistentClass("tblPanelSetOrder", "YPIDATA")]
    public class PanelSetOrderView : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected string m_ObjectId;
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
        protected string m_TechnicalComponentFacilityId;
        protected string m_TechnicalComponentBillingFacilityId;
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
        protected bool m_PublishNotificationSent;
        protected Nullable<DateTime> m_TimeOfLastPublishNotification;        

		public PanelSetOrderView()
		{
			
		}

		[PersistentDocumentIdProperty()]
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
		
		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}		       		 		
	}
}
