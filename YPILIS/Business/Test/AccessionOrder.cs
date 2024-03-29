using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Test
{
	[PersistentClass("tblAccessionOrder", "YPIDATA")]
    public class AccessionOrder : INotifyPropertyChanged, Interface.IOrder, Interface.IPatientOrder
	{
		public event PropertyChangedEventHandler PropertyChanged;

        public delegate void MessageReceivedHandler(string message);
        public event MessageReceivedHandler MessageRecieved;

        private AccessionLock m_AccessionLock;
        private YellowstonePathology.Business.DataTemplateSpecimenOrderEnum m_SpecimenOrderDataTemplate;

		private YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection m_SpecimenOrderCollection;
		private YellowstonePathology.Business.Test.PanelSetOrderCollection m_PanelSetOrderCollection;
		private YellowstonePathology.Business.Task.Model.TaskOrderCollection m_TaskOrderCollection;
		private YellowstonePathology.Business.Billing.Model.ICD9BillingCodeCollection m_ICD9BillingCodeCollection;
        private YellowstonePathology.Business.Amendment.Model.AmendmentCollection m_AmendmentCollection;

		private XElement m_OrderInstructionsUpdate;

		private string m_ObjectId;
		private string m_MasterAccessionNo;
        private string m_ClientAccessionNo;
        private bool m_ClientAccessioned;
		private Nullable<DateTime> m_AccessionDate;
		private Nullable<DateTime> m_AccessionTime;
		private string m_AccessioningFacilityId;
		private string m_PatientId;
		private string m_PFirstName;
		private string m_PLastName;
		private string m_PMiddleInitial;
		private string m_PSSN;
		private string m_PSex;
		private Nullable<DateTime> m_PBirthdate;
		private string m_PAddress1;
		private string m_PAddress2;
		private string m_PCity;
		private string m_PState;
		private string m_PZipCode;
		private string m_PRace;
		private string m_PPhoneNumberHome;
		private string m_PPhoneNumberBusiness;
		private string m_PMaritalStatus;
		private string m_PCAN;
		private int m_ClientId;
		private string m_ClientName;
		private int m_PhysicianId;
		private string m_PhysicianName;
		private string m_SvhAccount;
		private string m_SvhMedicalRecord;
		private string m_PatientType;
		private string m_PrimaryInsurance;
		private string m_SecondaryInsurance;
		private string m_CassetteColor;
		private string m_PSuffix;
		private string m_ClientOrderId;
		private bool m_Accessioned;
		private int m_SpecimenLogId;
		private int m_LoggedById;
		private int m_AccessionedById;
		private bool m_RequisitionVerified;
		private Nullable<DateTime> m_CollectionDate;
		private Nullable<DateTime> m_CollectionTime;
		private string m_SurgicalAccessionNo;
		private string m_CytologyAccessionNo;
		private bool m_OrderCancelled;
		private string m_ExternalOrderId;
        private string m_SecondaryExternalOrderId;
        private bool m_Verified;
		private int m_VerifiedById;
		private Nullable<DateTime> m_VerifiedDate;
		private string m_IncomingHL7;
		private string m_BillingData;
		private string m_OrderedByFirstName;
		private string m_OrderedByLastName;
		private string m_OrderedById;
		private string m_ProviderFirstName;
		private string m_ProviderLastName;
		private string m_ProviderId;
		private string m_InsurancePlan1;
		private string m_InsurancePlan2;
		private bool m_Finalized;
		private int m_FinalizedById;
		private Nullable<DateTime> m_FinalizedDate;
		private bool m_RequiresBlindVerification;
		private string m_SystemInitiatingOrder;
		private string m_ClinicalHistory;
		private string m_SpecialInstructions;
		private string m_UniversalServiceId;
        private string m_FeeSchedule;
        private bool m_UseBillingAgent;
        private int m_CaseOwnerId;
        private bool m_ITAuditRequired;
        private bool m_ITAudited;
        private bool m_HoldBilling;
        private int m_ITAuditPriority;
        private string m_CaseDialog;
        private string m_PlaceOfService;
		private bool m_HoldSVHDistribution;
		private bool m_HighPriority;
		private string m_ReportCopyTo;
		private string m_PEmailAddress;
		private bool m_DistributeToPatient;
		private string m_PatientDistributionType;
		private string m_HoldBillingComment;
		private string m_CollectionFacilityId;
		private string m_ICD10Code;
		private string m_PatientPaymentInstructions;
		private string m_PaymentType;
		private Nullable<DateTime> m_DateOfDeath;
		private string m_WCAuthorizationId;
		private Nullable<DateTime> m_VAAuthorizationEnd;
        private Nullable<DateTime> m_VAAuthorizationStart;
        private string m_VAAuthorizationId;
		private bool m_AdditionalInformation;

        public AccessionOrder()
        {
            this.m_AccessionLock = new AccessionLock();
            this.m_SpecimenOrderCollection = new YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection();
			this.m_PanelSetOrderCollection = new YellowstonePathology.Business.Test.PanelSetOrderCollection();
			this.m_SpecimenOrderDataTemplate = Business.DataTemplateSpecimenOrderEnum.DataTemplateAccessionTreeView;			
			this.m_ICD9BillingCodeCollection = new Billing.Model.ICD9BillingCodeCollection();
			this.m_TaskOrderCollection = new YellowstonePathology.Business.Task.Model.TaskOrderCollection();
            this.m_AmendmentCollection = new Amendment.Model.AmendmentCollection();
		}

        public AccessionOrder(string masterAccessionNo, string objectId)
        {            
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_AccessionLock = new AccessionLock();
            this.m_ObjectId = objectId;
			this.m_AccessionDate = DateTime.Today;
			this.m_AccessionTime = DateTime.Now;
            this.m_CollectionDate = DateTime.Today;
            this.m_CollectionTime = DateTime.Today;
            this.m_AccessioningFacilityId = Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId;
			this.m_SpecimenOrderCollection = new YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection();
            this.m_PanelSetOrderCollection = new YellowstonePathology.Business.Test.PanelSetOrderCollection();
			this.m_SpecimenOrderDataTemplate = Business.DataTemplateSpecimenOrderEnum.DataTemplateAccessionTreeView;            
			this.m_ICD9BillingCodeCollection = new Billing.Model.ICD9BillingCodeCollection();
			this.m_TaskOrderCollection = new YellowstonePathology.Business.Task.Model.TaskOrderCollection();
            this.m_AmendmentCollection = new Amendment.Model.AmendmentCollection();
        }

        public AccessionLock AccessionLock
        {
            get { return this.m_AccessionLock; }
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
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string ClientAccessionNo
        {
            get { return this.m_ClientAccessionNo; }
            set
            {
                if (this.m_ClientAccessionNo != value)
                {
                    this.m_ClientAccessionNo = value;
                    this.NotifyPropertyChanged("ClientAccessionNo");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool ClientAccessioned
        {
            get { return this.m_ClientAccessioned; }
            set
            {
                if (this.m_ClientAccessioned != value)
                {
                    this.m_ClientAccessioned = value;
                    this.NotifyPropertyChanged("ClientAccessioned");
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
		public Nullable<DateTime> AccessionDate
		{
			get { return this.m_AccessionDate; }
			set
			{
				if (this.m_AccessionDate != value)
				{
					this.m_AccessionDate = value;
					this.NotifyPropertyChanged("AccessionDate");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
		public Nullable<DateTime> AccessionTime
		{
			get { return this.m_AccessionTime; }
			set
			{
				if (this.m_AccessionTime != value)
				{
					this.m_AccessionTime = value;
					this.NotifyPropertyChanged("AccessionTime");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string AccessioningFacilityId
		{
			get { return this.m_AccessioningFacilityId; }
			set
			{
				if (this.m_AccessioningFacilityId != value)
				{
					this.m_AccessioningFacilityId = value;
					this.NotifyPropertyChanged("AccessioningFacilityId");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "20", "'0'", "varchar")]
		public string PatientId
		{
			get { return this.m_PatientId; }
			set
			{
				if (this.m_PatientId != value)
				{
					this.m_PatientId = value;
					this.NotifyPropertyChanged("PatientId");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string PFirstName
		{
			get { return this.m_PFirstName; }
			set
			{
				if (this.m_PFirstName != value)
				{
					this.m_PFirstName = value;
					this.NotifyPropertyChanged("PFirstName");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string PLastName
		{
			get { return this.m_PLastName; }
			set
			{
				if (this.m_PLastName != value)
				{
					this.m_PLastName = value;
					this.NotifyPropertyChanged("PLastName");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "20", "null", "varchar")]
		public string PMiddleInitial
		{
			get { return this.m_PMiddleInitial; }
			set
			{
				if (this.m_PMiddleInitial != value)
				{
					this.m_PMiddleInitial = value;
					this.NotifyPropertyChanged("PMiddleInitial");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "15", "null", "varchar")]
		public string PSSN
		{
			get { return this.m_PSSN; }
			set
			{
				if (this.m_PSSN != value)
				{
					this.m_PSSN = value;
					this.NotifyPropertyChanged("PSSN");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "10", "null", "varchar")]
		public string PSex
		{
			get { return this.m_PSex; }
			set
			{
				if (this.m_PSex != value)
				{
					this.m_PSex = value;
					this.NotifyPropertyChanged("PSex");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
		public Nullable<DateTime> PBirthdate
		{
			get { return this.m_PBirthdate; }
			set
			{
				if (this.m_PBirthdate != value)
				{
					this.m_PBirthdate = value;
					this.NotifyPropertyChanged("PBirthdate");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string PAddress1
		{
			get { return this.m_PAddress1; }
			set
			{
				if (this.m_PAddress1 != value)
				{
					this.m_PAddress1 = value;
					this.NotifyPropertyChanged("PAddress1");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string PAddress2
		{
			get { return this.m_PAddress2; }
			set
			{
				if (this.m_PAddress2 != value)
				{
					this.m_PAddress2 = value;
					this.NotifyPropertyChanged("PAddress2");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string PCity
		{
			get { return this.m_PCity; }
			set
			{
				if (this.m_PCity != value)
				{
					this.m_PCity = value;
					this.NotifyPropertyChanged("PCity");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string PState
		{
			get { return this.m_PState; }
			set
			{
				if (this.m_PState != value)
				{
					this.m_PState = value;
					this.NotifyPropertyChanged("PState");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "12", "null", "varchar")]
		public string PZipCode
		{
			get { return this.m_PZipCode; }
			set
			{
				if (this.m_PZipCode != value)
				{
					this.m_PZipCode = value;
					this.NotifyPropertyChanged("PZipCode");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string PRace
		{
			get { return this.m_PRace; }
			set
			{
				if (this.m_PRace != value)
				{
					this.m_PRace = value;
					this.NotifyPropertyChanged("PRace");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "10", "null", "varchar")]
		public string PPhoneNumberHome
		{
			get { return this.m_PPhoneNumberHome; }
			set
			{
				if (this.m_PPhoneNumberHome != value)
				{
					this.m_PPhoneNumberHome = value;
					this.NotifyPropertyChanged("PPhoneNumberHome");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "10", "null", "varchar")]
		public string PPhoneNumberBusiness
		{
			get { return this.m_PPhoneNumberBusiness; }
			set
			{
				if (this.m_PPhoneNumberBusiness != value)
				{
					this.m_PPhoneNumberBusiness = value;
					this.NotifyPropertyChanged("PPhoneNumberBusiness");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string PMaritalStatus
		{
			get { return this.m_PMaritalStatus; }
			set
			{
				if (this.m_PMaritalStatus != value)
				{
					this.m_PMaritalStatus = value;
					this.NotifyPropertyChanged("PMaritalStatus");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string PCAN
		{
			get { return this.m_PCAN; }
			set
			{
				if (this.m_PCAN != value)
				{
					this.m_PCAN = value;
					this.NotifyPropertyChanged("PCAN");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "11", "0", "int")]
		public int ClientId
		{
			get { return this.m_ClientId; }
			set
			{
				if (this.m_ClientId != value)
				{
					this.m_ClientId = value;
					this.NotifyPropertyChanged("ClientId");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "200", "null", "varchar")]
		public string ClientName
		{
			get { return this.m_ClientName; }
			set
			{
				if (this.m_ClientName != value)
				{
					this.m_ClientName = value;
					this.NotifyPropertyChanged("ClientName");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "11", "0", "int")]
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
		[PersistentDataColumnProperty(true, "200", "null", "varchar")]
		public string PhysicianName
		{
			get { return this.m_PhysicianName; }
			set
			{
				if (this.m_PhysicianName != value)
				{
					this.m_PhysicianName = value;
					this.NotifyPropertyChanged("PhysicianName");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string SvhAccount
		{
			get { return this.m_SvhAccount; }
			set
			{
				if (this.m_SvhAccount != value)
				{
					this.m_SvhAccount = value;
					this.NotifyPropertyChanged("SvhAccount");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string SvhMedicalRecord
		{
			get { return this.m_SvhMedicalRecord; }
			set
			{
				if (this.m_SvhMedicalRecord != value)
				{
					this.m_SvhMedicalRecord = value;
					this.NotifyPropertyChanged("SvhMedicalRecord");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "20", "'Not Selected'", "varchar")]
		public string PatientType
		{
			get { return this.m_PatientType; }
			set
			{
				if (this.m_PatientType != value)
				{
					this.m_PatientType = value;
					this.NotifyPropertyChanged("PatientType");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "'Not Selected'", "varchar")]
		public string PrimaryInsurance
		{
			get { return this.m_PrimaryInsurance; }
			set
			{
				if (this.m_PrimaryInsurance != value)
				{
					this.m_PrimaryInsurance = value;
					this.NotifyPropertyChanged("PrimaryInsurance");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "'Not Selected'", "varchar")]
        public string SecondaryInsurance
		{
			get { return this.m_SecondaryInsurance; }
			set
			{
				if (this.m_SecondaryInsurance != value)
				{
					this.m_SecondaryInsurance = value;
					this.NotifyPropertyChanged("SecondaryInsurance");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "50", "0", "varchar")]
		public string CassetteColor
		{
			get { return this.m_CassetteColor; }
			set
			{
				if (this.m_CassetteColor != value)
				{
					this.m_CassetteColor = value;
					this.NotifyPropertyChanged("CassetteColor");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string PSuffix
		{
			get { return this.m_PSuffix; }
			set
			{
				if (this.m_PSuffix != value)
				{
					this.m_PSuffix = value;
					this.NotifyPropertyChanged("PSuffix");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ClientOrderId
		{
			get { return this.m_ClientOrderId; }
			set
			{
				if (this.m_ClientOrderId != value)
				{
					this.m_ClientOrderId = value;
					this.NotifyPropertyChanged("ClientOrderId");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "1", "tinyint")]
		public bool Accessioned
		{
			get { return this.m_Accessioned; }
			set
			{
				if (this.m_Accessioned != value)
				{
					this.m_Accessioned = value;
					this.NotifyPropertyChanged("Accessioned");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "11", "0", "int")]
		public int SpecimenLogId
		{
			get { return this.m_SpecimenLogId; }
			set
			{
				if (this.m_SpecimenLogId != value)
				{
					this.m_SpecimenLogId = value;
					this.NotifyPropertyChanged("SpecimenLogId");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "11", "0", "int")]
		public int LoggedById
		{
			get { return this.m_LoggedById; }
			set
			{
				if (this.m_LoggedById != value)
				{
					this.m_LoggedById = value;
					this.NotifyPropertyChanged("LoggedById");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "11", "0", "int")]
		public int AccessionedById
		{
			get { return this.m_AccessionedById; }
			set
			{
				if (this.m_AccessionedById != value)
				{
					this.m_AccessionedById = value;
					this.NotifyPropertyChanged("AccessionedById");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "tinyint")]
		public bool RequisitionVerified
		{
			get { return this.m_RequisitionVerified; }
			set
			{
				if (this.m_RequisitionVerified != value)
				{
					this.m_RequisitionVerified = value;
					this.NotifyPropertyChanged("RequisitionVerified");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
		public Nullable<DateTime> CollectionDate
		{
			get { return this.m_CollectionDate; }
			set
			{
				if (this.m_CollectionDate != value)
				{
					this.m_CollectionDate = value;
					this.NotifyPropertyChanged("CollectionDate");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
		public Nullable<DateTime> CollectionTime
		{
			get { return this.m_CollectionTime; }
			set
			{
				if (this.m_CollectionTime != value)
				{
					this.m_CollectionTime = value;
					this.NotifyPropertyChanged("CollectionTime");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "20", "null", "varchar")]
		public string SurgicalAccessionNo
		{
			get { return this.m_SurgicalAccessionNo; }
			set
			{
				if (this.m_SurgicalAccessionNo != value)
				{
					this.m_SurgicalAccessionNo = value;
					this.NotifyPropertyChanged("SurgicalAccessionNo");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "20", "null", "varchar")]
		public string CytologyAccessionNo
		{
			get { return this.m_CytologyAccessionNo; }
			set
			{
				if (this.m_CytologyAccessionNo != value)
				{
					this.m_CytologyAccessionNo = value;
					this.NotifyPropertyChanged("CytologyAccessionNo");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "tinyint")]
		public bool OrderCancelled
		{
			get { return this.m_OrderCancelled; }
			set
			{
				if (this.m_OrderCancelled != value)
				{
					this.m_OrderCancelled = value;
					this.NotifyPropertyChanged("OrderCancelled");
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
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string SecondaryExternalOrderId
        {
            get { return this.m_SecondaryExternalOrderId; }
            set
            {
                if (this.m_SecondaryExternalOrderId != value)
                {
                    this.m_SecondaryExternalOrderId = value;
                    this.NotifyPropertyChanged("SecondaryExternalOrderId");
                }
            }
        }

        [PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "tinyint")]
		public bool Verified
		{
			get { return this.m_Verified; }
			set
			{
				if (this.m_Verified != value)
				{
					this.m_Verified = value;
					this.NotifyPropertyChanged("Verified");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "11", "0", "int")]
		public int VerifiedById
		{
			get { return this.m_VerifiedById; }
			set
			{
				if (this.m_VerifiedById != value)
				{
					this.m_VerifiedById = value;
					this.NotifyPropertyChanged("VerifiedById");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
		public Nullable<DateTime> VerifiedDate
		{
			get { return this.m_VerifiedDate; }
			set
			{
				if (this.m_VerifiedDate != value)
				{
					this.m_VerifiedDate = value;
					this.NotifyPropertyChanged("VerifiedDate");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "8000", "null", "varchar")]
		public string IncomingHL7
		{
			get { return this.m_IncomingHL7; }
			set
			{
				if (this.m_IncomingHL7 != value)
				{
					this.m_IncomingHL7 = value;
					this.NotifyPropertyChanged("IncomingHL7");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string BillingData
		{
			get { return this.m_BillingData; }
			set
			{
				if (this.m_BillingData != value)
				{
					this.m_BillingData = value;
					this.NotifyPropertyChanged("BillingData");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string OrderedByFirstName
		{
			get { return this.m_OrderedByFirstName; }
			set
			{
				if (this.m_OrderedByFirstName != value)
				{
					this.m_OrderedByFirstName = value;
					this.NotifyPropertyChanged("OrderedByFirstName");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string OrderedByLastName
		{
			get { return this.m_OrderedByLastName; }
			set
			{
				if (this.m_OrderedByLastName != value)
				{
					this.m_OrderedByLastName = value;
					this.NotifyPropertyChanged("OrderedByLastName");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string OrderedById
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
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string ProviderFirstName
		{
			get { return this.m_ProviderFirstName; }
			set
			{
				if (this.m_ProviderFirstName != value)
				{
					this.m_ProviderFirstName = value;
					this.NotifyPropertyChanged("ProviderFirstName");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string ProviderLastName
		{
			get { return this.m_ProviderLastName; }
			set
			{
				if (this.m_ProviderLastName != value)
				{
					this.m_ProviderLastName = value;
					this.NotifyPropertyChanged("ProviderLastName");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ProviderId
		{
			get { return this.m_ProviderId; }
			set
			{
				if (this.m_ProviderId != value)
				{
					this.m_ProviderId = value;
					this.NotifyPropertyChanged("ProviderId");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string InsurancePlan1
		{
			get { return this.m_InsurancePlan1; }
			set
			{
				if (this.m_InsurancePlan1 != value)
				{
					this.m_InsurancePlan1 = value;
					this.NotifyPropertyChanged("InsurancePlan1");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string InsurancePlan2
		{
			get { return this.m_InsurancePlan2; }
			set
			{
				if (this.m_InsurancePlan2 != value)
				{
					this.m_InsurancePlan2 = value;
					this.NotifyPropertyChanged("InsurancePlan2");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "tinyint")]
		public bool Finalized
		{
			get { return this.m_Finalized; }
			set
			{
				if (this.m_Finalized != value)
				{
					this.m_Finalized = value;
					this.NotifyPropertyChanged("Finalized");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "int")]
		public int FinalizedById
		{
			get { return this.m_FinalizedById; }
			set
			{
				if (this.m_FinalizedById != value)
				{
					this.m_FinalizedById = value;
					this.NotifyPropertyChanged("FinalizedById");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
		public Nullable<DateTime> FinalizedDate
		{
			get { return this.m_FinalizedDate; }
			set
			{
				if (this.m_FinalizedDate != value)
				{
					this.m_FinalizedDate = value;
					this.NotifyPropertyChanged("FinalizedDate");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "tinyint")]
		public bool RequiresBlindVerification
		{
			get { return this.m_RequiresBlindVerification; }
			set
			{
				if (this.m_RequiresBlindVerification != value)
				{
					this.m_RequiresBlindVerification = value;
					this.NotifyPropertyChanged("RequiresBlindVerification");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string SystemInitiatingOrder
		{
			get { return this.m_SystemInitiatingOrder; }
			set
			{
				if (this.m_SystemInitiatingOrder != value)
				{
					this.m_SystemInitiatingOrder = value;
					this.NotifyPropertyChanged("SystemInitiatingOrder");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "5000", "null", "varchar")]
		public string ClinicalHistory
		{
			get { return this.m_ClinicalHistory; }
			set
			{
				if (this.m_ClinicalHistory != value)
				{
					this.m_ClinicalHistory = value;
					this.NotifyPropertyChanged("ClinicalHistory");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "5000", "null", "varchar")]
		public string SpecialInstructions
		{
			get { return this.m_SpecialInstructions; }
			set
			{
				if (this.m_SpecialInstructions != value)
				{
					this.m_SpecialInstructions = value;
					this.NotifyPropertyChanged("SpecialInstructions");
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
        [PersistentDataColumnProperty(true, "50", "'Standard'", "varchar")]
        public string FeeSchedule
        {
            get { return this.m_FeeSchedule; }
            set
            {
                if (this.m_FeeSchedule != value)
                {
                    this.m_FeeSchedule = value;
                    this.NotifyPropertyChanged("FeeSchedule");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "1", "tinyint")]
        public bool UseBillingAgent
        {
            get { return this.m_UseBillingAgent; }
            set
            {
                if (this.m_UseBillingAgent != value)
                {
                    this.m_UseBillingAgent = value;
                    this.NotifyPropertyChanged("UseBillingAgent");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "0", "int")]
        public int CaseOwnerId
        {
            get { return this.m_CaseOwnerId; }
            set
            {
                if (this.m_CaseOwnerId != value)
                {
                    this.m_CaseOwnerId = value;
                    this.NotifyPropertyChanged("CaseOwnerId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool ITAudited
        {
            get { return this.m_ITAudited; }
            set
            {
                if (this.m_ITAudited != value)
                {
                    this.m_ITAudited = value;
                    this.NotifyPropertyChanged("ITAudited");
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
		[PersistentDataColumnProperty(true, "5000", "0", "varchar")]
		public string HoldBillingComment
		{
			get { return this.m_HoldBillingComment; }
			set
			{
				if (this.m_HoldBillingComment != value)
				{
					this.m_HoldBillingComment = value;
					this.NotifyPropertyChanged("HoldBillingComment");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "0", "tinyint")]
		public bool HoldSVHDistribution
		{
			get { return this.m_HoldSVHDistribution; }
			set
			{
				if (this.m_HoldSVHDistribution != value)
				{
					this.m_HoldSVHDistribution = value;
					this.NotifyPropertyChanged("HoldSVHDistribution");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "0", "tinyint")]
		public bool HighPriority
		{
			get { return this.m_HighPriority; }
			set
			{
				if (this.m_HighPriority != value)
				{
					this.m_HighPriority = value;
					this.NotifyPropertyChanged("HighPriority");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "0", "tinyint")]
		public string ReportCopyTo
		{
			get { return this.m_ReportCopyTo; }
			set
			{
				if (this.m_ReportCopyTo != value)
				{
					this.m_ReportCopyTo = value;
					this.NotifyPropertyChanged("ReportCopyTo");
				}
			}
		}

		[PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool ITAuditRequired
        {
            get { return this.m_ITAuditRequired; }
            set
            {
                if (this.m_ITAuditRequired != value)
                {
                    this.m_ITAuditRequired = value;
                    this.NotifyPropertyChanged("ITAuditRequired");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "0", "int")]
        public int ITAuditPriority
        {
            get { return this.m_ITAuditPriority; }
            set
            {
                if (this.m_ITAuditPriority != value)
                {
                    this.m_ITAuditPriority = value;
                    this.NotifyPropertyChanged("ITAuditPriority");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string CaseDialog
        {
            get { return this.m_CaseDialog; }
            set
            {
                if (this.m_CaseDialog != value)
                {
                    this.m_CaseDialog = value;
                    this.NotifyPropertyChanged("CaseDialog");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string PlaceOfService
        {
            get { return this.m_PlaceOfService; }
            set
            {
                if (this.m_PlaceOfService != value)
                {
                    this.m_PlaceOfService = value;
                    this.NotifyPropertyChanged("PlaceOfService");
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "200", "null", "varchar")]
		public string PEmailAddress
		{
			get { return this.m_PEmailAddress; }
			set
			{
				if (this.m_PEmailAddress != value)
				{
					this.m_PEmailAddress = value;
					this.NotifyPropertyChanged("PEmailAddress");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "0", "tinyint")]
		public bool DistributeToPatient
		{
			get { return this.m_DistributeToPatient; }
			set
			{
				if (this.m_DistributeToPatient != value)
				{
					this.m_DistributeToPatient = value;
					this.NotifyPropertyChanged("DistributeToPatient");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "200", "null", "varchar")]
		public string PatientDistributionType
		{
			get { return this.m_PatientDistributionType; }
			set
			{
				if (this.m_PatientDistributionType != value)
				{
					this.m_PatientDistributionType = value;
					this.NotifyPropertyChanged("PatientDistributionType");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "45", "null", "varchar")]
		public string CollectionFacilityId
		{
			get { return this.m_CollectionFacilityId; }
			set
			{
				if (this.m_CollectionFacilityId != value)
				{
					this.m_CollectionFacilityId = value;
					this.NotifyPropertyChanged("CollectionFacilityId");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "45", "null", "varchar")]
		public string ICD10Code
		{
			get { return this.m_ICD10Code; }
			set
			{
				if (this.m_ICD10Code != value)
				{
					this.m_ICD10Code = value;
					this.NotifyPropertyChanged("ICD10Code");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string PatientPaymentInstructions
		{
			get { return this.m_PatientPaymentInstructions; }
			set
			{
				if (this.m_PatientPaymentInstructions != value)
				{
					this.m_PatientPaymentInstructions = value;
					this.NotifyPropertyChanged("PatientPaymentInstructions");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string PaymentType
		{
			get { return this.m_PaymentType; }
			set
			{
				if (this.m_PaymentType != value)
				{
					this.m_PaymentType = value;
					this.NotifyPropertyChanged("PaymentType");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string VAAuthorizationId
        {
            get { return this.m_VAAuthorizationId; }
            set
            {
                if (this.m_VAAuthorizationId != value)
                {
                    this.m_VAAuthorizationId = value;
                    this.NotifyPropertyChanged("PaymentType");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string WCAuthorizationId
        {
            get { return this.m_WCAuthorizationId; }
            set
            {
                if (this.m_WCAuthorizationId != value)
                {
                    this.m_WCAuthorizationId = value;
                    this.NotifyPropertyChanged("WCAuthorizationId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "DateTime")]
        public Nullable<DateTime> VAAuthorizationStart
        {
            get { return this.m_VAAuthorizationStart; }
            set
            {
                if (this.m_VAAuthorizationStart != value)
                {
                    this.m_VAAuthorizationStart = value;
                    this.NotifyPropertyChanged("m_VAAuthorizationStart");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "DateTime")]
        public Nullable<DateTime> VAAuthorizationEnd
        {
            get { return this.m_VAAuthorizationEnd; }
            set
            {
                if (this.m_VAAuthorizationEnd != value)
                {
                    this.m_VAAuthorizationEnd = value;
                    this.NotifyPropertyChanged("m_VAAuthorizationEnd");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "DateTime")]
        public Nullable<DateTime> DateOfDeath
        {
            get { return this.m_DateOfDeath; }
            set
            {
                if (this.m_DateOfDeath != value)
                {
                    this.m_DateOfDeath = value;
                    this.NotifyPropertyChanged("m_DateOfDeath");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
        public bool AdditionalInformation
        {
            get { return this.m_AdditionalInformation; }
            set
            {
                if (this.m_AdditionalInformation != value)
                {
                    this.m_AdditionalInformation = value;
                    this.NotifyPropertyChanged("AdditionalInformation");
                }
            }
        }

        public void FromClientOrder(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder, int orderingUserId, string accessionOrderIds)
        {            
			this.ClientId = clientOrder.ClientId;
			this.ClientName = clientOrder.ClientName;
			this.PhysicianName = clientOrder.ProviderName;			

			this.CollectionFacilityId = clientOrder.CollectionFacilityId;
			this.ICD10Code = clientOrder.ICD10Code;

			if(clientOrder.CollectionDate.HasValue == true)
			{
				this.CollectionDate = DateTime.Parse(clientOrder.CollectionDate.Value.ToString("MM/dd/yyyy"));
			}
			else
			{
				if(clientOrder.ClientOrderDetailCollection.Count > 0)
                {
					this.CollectionDate = clientOrder.ClientOrderDetailCollection[0].CollectionDate;
                }
                else
                {
					this.CollectionDate = DateTime.Today;
				}				
			}
				
			this.CollectionTime = clientOrder.CollectionDate;
			this.PBirthdate = Helper.DateTimeExtensions.DateFromNullableDateTime(clientOrder.PBirthdate);
			this.PFirstName = clientOrder.PFirstName;
			this.PLastName = clientOrder.PLastName;
			this.PMiddleInitial = clientOrder.PMiddleInitial;
			this.PSex = clientOrder.PSex;
			this.PSSN = clientOrder.PSSN;
			this.SvhAccount = clientOrder.SvhAccountNo;
			this.SvhMedicalRecord = clientOrder.SvhMedicalRecord;
            this.PlaceOfService = clientOrder.PlaceOfService;

			

			this.HighPriority = clientOrder.HighPriority;
			if (clientOrder.ClientId == 1759) this.HighPriority = true;

			this.PatientType = clientOrder.PatientType;
            switch(clientOrder.PatientType)
            {
                case "IN":
                case "ER":
                case "INO":
                case "RCR":
                    this.m_PatientType = "IP";
                    break;
                case "CLI":
                case "SDC":
                case "REF":
                    this.m_PatientType = "OP";
                    break;
            }

			this.m_UseBillingAgent = true;
			if(clientOrder.PaymentType == "Credit Card" || clientOrder.PaymentType == "Cash")
			{
				this.m_UseBillingAgent = false;
			}			

            if (string.IsNullOrEmpty(clientOrder.ClinicalHistory) == false)
            {
                this.ClinicalHistory = clientOrder.ClinicalHistory;
            }
            else
            {
                this.ClinicalHistory = "???";
            }
            
            this.UniversalServiceId = clientOrder.UniversalServiceId;

			this.AccessionedById = orderingUserId;
			this.LoggedById = orderingUserId;

			this.ClientOrderId = clientOrder.ClientOrderId;
            if (clientOrder.SystemInitiatingOrder == "EPIC" || clientOrder.SystemInitiatingOrder == "Beaker")
            {
                this.ExternalOrderId = accessionOrderIds;
            }
            else
            {
                this.ExternalOrderId = clientOrder.ExternalOrderId;
            }

			this.ReportCopyTo = clientOrder.ReportCopyTo;
            this.SecondaryExternalOrderId = clientOrder.SecondaryExternalOrderId;
            this.IncomingHL7 = clientOrder.IncomingHL7;
			this.OrderedByFirstName = clientOrder.OrderedByFirstName;
			this.OrderedByLastName = clientOrder.OrderedByLastName;
			this.OrderedById = clientOrder.OrderedById;
			this.ProviderFirstName = clientOrder.ProviderFirstName;
			this.ProviderLastName = clientOrder.ProviderLastName;
			this.ProviderId = clientOrder.ProviderId;

			if (string.IsNullOrEmpty(this.ProviderId) == false && this.ProviderId == "1790832756")
			{
				this.CassetteColor = "Lilac";
			}

			this.SystemInitiatingOrder = clientOrder.SystemInitiatingOrder;
			this.Verified = clientOrder.Validated;

			this.PAddress1 = clientOrder.PAddress1;
			this.PAddress2 = clientOrder.PAddress2;
			this.PCity = clientOrder.PCity;
			this.PState = clientOrder.PState;
			this.PZipCode = clientOrder.PZipCode;

			this.m_PEmailAddress = clientOrder.EmailAddress;
			this.m_PPhoneNumberHome = clientOrder.PPhone;
			this.m_PatientDistributionType = clientOrder.PatientDistributionType;
			this.m_DistributeToPatient = clientOrder.DistributeToPatient;
			this.m_PatientPaymentInstructions = clientOrder.PaymentType;

			if (this.Verified)
			{
				this.VerifiedById = orderingUserId;
				this.VerifiedDate = DateTime.Now;
			}

			if(!string.IsNullOrEmpty(clientOrder.ProviderId))
			{
				YellowstonePathology.Business.Domain.Physician physician = Business.Gateway.PhysicianClientGateway.GetPhysicianByNpi(clientOrder.ProviderId);
				if(physician != null)
				{
					this.PhysicianId = physician.PhysicianId;
					this.PhysicianName = physician.DisplayName;
				}
			}

			Business.Client.Model.ClientGroupClientCollection clientGroupClientColletion = Business.Gateway.PhysicianClientGateway.GetClientGroupClientCollection();
			if(clientGroupClientColletion.IsInGroup(this.m_ClientId, "1"))
			{
				this.HoldSVHDistribution = true;
			}

            if (ShouldBillingBeHeld() == true) this.m_HoldBilling = true;
			this.HandleNoSCLOrderEmail();
		}  

		public void HandleNoSCLOrderEmail()
        {
			if(string.IsNullOrEmpty(this.m_ExternalOrderId) == true && string.IsNullOrEmpty(this.m_SecondaryExternalOrderId) == true)
            {
				Business.Client.Model.ClientCollection sclClients = Business.Gateway.PhysicianClientGateway.GetClientCollectionByClientGroupId("1");
				if(sclClients.Exists(this.m_ClientId) == true)
                {
					if(string.IsNullOrEmpty(this.m_PLastName) == false && string.IsNullOrEmpty(this.m_PFirstName) == false && this.m_PBirthdate.HasValue == true)
                    {
						Business.Billing.Model.SVHNoOrderMailMessage.SendMessage(this.m_PLastName, this.m_PFirstName, this.m_PBirthdate.Value.ToString("MM/dd/yyyy"));
					}					
                }
            }
        }
        
        public bool ShouldBillingBeHeld()
        {
            bool result = false;
            if(string.IsNullOrEmpty(this.m_SvhMedicalRecord) == false && this.m_SvhMedicalRecord.StartsWith("A"))
            {
                result = true;    
            }
            Business.Client.Model.HRHClinics hrhClinics = new Client.Model.HRHClinics();
            if(hrhClinics.Exists(this.m_ClientId))
            {
                result = true;
            }
            return result;
        }      

		public string PhysicianClientName
        {
            get
            {
                string result = string.Empty;
                if (this.PhysicianName != string.Empty)
                {
                    result = this.PhysicianName;
                }
                if (this.ClientName != string.Empty)
                {
                    result = result + " - " + this.ClientName;
                }
                return result;
            }            
        }

		public YellowstonePathology.Business.DataTemplateSpecimenOrderEnum SpecimenOrderDataTemplate
        {
            get { return this.m_SpecimenOrderDataTemplate; }
            set { this.m_SpecimenOrderDataTemplate = value; }
        }

        [PersistentCollection()]
		public YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection SpecimenOrderCollection
        {
            get { return this.m_SpecimenOrderCollection; }
            set { this.m_SpecimenOrderCollection = value; }
        }

        [PersistentCollection()]
		public YellowstonePathology.Business.Test.PanelSetOrderCollection PanelSetOrderCollection
		{
			get { return this.m_PanelSetOrderCollection; }
            set { this.m_PanelSetOrderCollection = value; }
		}
        
        public string PatientDisplayName
        {
            get
            {
				return YellowstonePathology.Business.Helper.PatientHelper.GetPatientDisplayName(PLastName, PFirstName, PMiddleInitial);
            }
        }

		public string PatientName
		{			
            get
            {
				return YellowstonePathology.Business.Helper.PatientHelper.GetPatientName(this.PLastName, this.PFirstName, this.PMiddleInitial);
            }
		}

		public string PatientAccessionAge
		{
			get
			{
                if (this.AccessionDate.HasValue)
                {
					return YellowstonePathology.Business.Helper.PatientHelper.GetAccessionAge(this.PBirthdate, this.AccessionDate.Value);
                }
                return string.Empty;
			}
		}							

		public string PLastNameProxy
		{
			get { return this.PLastName; }
			set
			{
				if (this.PLastName != value)
				{
					this.PLastName = value;
					this.NotifyPropertyChanged("PLastName");
					this.NotifyPropertyChanged("PatientName");
					this.NotifyPropertyChanged("PLastNameProxy");
				}
			}
		}

		public string PFirstNameProxy
		{
			get { return this.PFirstName; }
			set
			{
				if (this.PFirstName != value)
				{
					this.PFirstName = value;
					this.NotifyPropertyChanged("PFirstName");
					this.NotifyPropertyChanged("PatientName");
					this.NotifyPropertyChanged("PFirstNameProxy");
				}
			}
		}

		public Nullable<DateTime> AccessionDateTime
		{            
			get { return AccessionTime; }

			set
			{
				if (value != AccessionDate)
				{
					if (value.Value.Date == DateTime.Today)
					{
						this.AccessionDate = DateTime.Today;
						this.AccessionTime = DateTime.Now;
						NotifyPropertyChanged("AccessionDateTime");
					}
				}
			}
		}		

		public XElement OrderInstructionsUpdate
		{
			get { return this.m_OrderInstructionsUpdate; }
			set { this.m_OrderInstructionsUpdate = value; }
		}		

        [PersistentCollection()]
		public YellowstonePathology.Business.Billing.Model.ICD9BillingCodeCollection ICD9BillingCodeCollection
		{
			get { return this.m_ICD9BillingCodeCollection; }
			set { this.m_ICD9BillingCodeCollection = value; }
		}			
        		
		public void SubmitChanges(YellowstonePathology.Business.DataContext.YpiDataBase dataContext)
		{
			throw new NotImplementedException("Not Implemented Here.");
		}

        public string ProviderNameString
        {
            get
            {
                System.Text.StringBuilder providerString = new System.Text.StringBuilder();
                providerString.Append(this.PhysicianName);
                if (string.IsNullOrEmpty(this.ClientName) == false)
                {
                    providerString.Append(" - " + this.ClientName);
                }
                return providerString.ToString();
            }
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

        public void SetPhysicianClient(Business.Client.Model.PhysicianClientDistributionListItem physicianClientDistribution)
		{
            this.ClientId = physicianClientDistribution.ClientId;
            this.PhysicianId = physicianClientDistribution.PhysicianId;
            this.PhysicianName = physicianClientDistribution.PhysicianName;
            this.ClientName = physicianClientDistribution.ClientName;
            this.NotifyPropertyChanged("PhysicianClientName");
		}

        public void UpdateColorCode()
        {
            if (this.IsDermatologyClient() == true)
            {
                if(this.m_ClientId == 1260 || this.m_ClientId == 1511 || this.m_PhysicianId == 2722) //Advanced Dermatology, diagnositics
                {                    
                    this.m_CassetteColor = "Lilac";
                }                
                else
                {                 
                    this.m_CassetteColor = "Yellow";
                }                
            }
            else if(this.m_ClientId == 1520)
            {                
                this.m_CassetteColor = "Blue";
            }
			else if(this.m_PhysicianId == 2722)
            {
				this.m_CassetteColor = "Lilac";
			}
        }

        public void UpdateCaseAssignment(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {

            if (panelSetOrder.PanelSetId == 13 && this.IsDermatologyClient() == true)
            {
                if(this.m_ClientId == 1260 || this.m_ClientId == 1511) //If Advanced Dermatology or Big Sky Diagnosistics
                {
                    panelSetOrder.AssignedToId = 5132; //Assign to Dr. Shannon
                }
                else if(this.m_ClientId == 579)
                {
                    panelSetOrder.AssignedToId = 5153;  //tallman cases to Rozelle
                } 
				else if(this.m_ClientId == 1799)
				{
					panelSetOrder.AssignedToId = 5153;  //Benefis Derm to Rozelle.
				}
            }
            else if (this.m_CaseOwnerId != 0)
            {
                PanelSet.Model.PanelSetCollection collection = PanelSet.Model.PanelSetCollection.GetAll();
                PanelSet.Model.PanelSet panelSet = collection.GetPanelSet(panelSetOrder.PanelSetId);

                if (panelSet.RequiresAssignment == true)
                {
                    panelSetOrder.AssignedToId = this.m_CaseOwnerId;
                }
            }
        }

        public string GetLocationPerformedComment()
        {
            StringBuilder result = new StringBuilder();
            YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = Business.PanelSet.Model.PanelSetCollection.GetAll();
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this.PanelSetOrderCollection)
            {
                YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(panelSetOrder.PanelSetId);
                if (panelSet.IsReflexPanel == false)
                {
                    result.Append(panelSetOrder.PanelSetName + ": ");
                    result.Append(panelSetOrder.GetLocationPerformedComment());
                    result.Append(" ");
                }
            }
            return result.ToString();
        }

        public bool IsDermatologyClient()
        {
            //Dermatology clients = Advanced Dermatology, Tallman and BSD
            bool result = false;
            if (this.m_ClientId == 1260 || this.m_ClientId == 579 || this.m_ClientId == 14 || this.m_ClientId == 1511)
            {
                result = true;
            }
            return result;
        }
       
		public virtual void AccessionSpecimen(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailCollection clientOrderDetailCollection)
		{
			foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail in clientOrderDetailCollection)
			{				
				if (clientOrderDetail.Received == true && clientOrderDetail.Accessioned == false)
				{
					YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_SpecimenOrderCollection.Add(this.m_MasterAccessionNo, clientOrderDetail);					
					clientOrderDetail.Accessioned = true;

					if(clientOrderDetail.ClientAccessioned == true) this.ClientAccessioned = true;
                    YellowstonePathology.Business.Visitor.AddSpecimenOrderVisitor addSpecimenOrderVisitor = new Visitor.AddSpecimenOrderVisitor(specimenOrder);
                    this.TakeATrip(addSpecimenOrderVisitor);                    
				}			
			}
		}		

		public bool Exists(string containerId)
		{
			bool result = false;
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.SpecimenOrderCollection)
			{
				if (specimenOrder.ContainerId == containerId)
				{
					result = true;
					break;
				}
			}
			return result;
		}		

		[PersistentCollection()]
		public YellowstonePathology.Business.Task.Model.TaskOrderCollection TaskOrderCollection
		{
			get { return this.m_TaskOrderCollection; }
			set { this.m_TaskOrderCollection = value; }
		}

		public bool UseNewStyleReportNo()
		{
			bool result = true;
			int year = DateTime.Today.Year;
			if (this.m_AccessionDate.HasValue) year = this.m_AccessionDate.Value.Year;
			if (year <= 2013) result = false;
			return result;
		}

        public string GetNextReportNo(YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet)
        {
            string reportNo = null;
            bool newStyleReportNo = this.UseNewStyleReportNo();
            if (newStyleReportNo == true)
            {
                reportNo = Business.OrderIdParser.GetNextReportNo(this.PanelSetOrderCollection, panelSet, this.MasterAccessionNo);
            }
            else
            {
				reportNo = Business.Gateway.AccessionOrderGateway.NextReportNo(panelSet.PanelSetId, this.MasterAccessionNo);
            }
            return reportNo;
        }               

        public void TakeATrip(YellowstonePathology.Business.Visitor.AccessionTreeVisitor accessionTreeVisitor)
        {
            this.PullOver(accessionTreeVisitor);
            if (accessionTreeVisitor.VisitLeftSide == true) this.TakeATripLeftSide(accessionTreeVisitor);
            if (accessionTreeVisitor.VisitRightSide == true) this.TakeATripRightSide(accessionTreeVisitor);            
        }

        public void TakeATripLeftSide(YellowstonePathology.Business.Visitor.AccessionTreeVisitor accessionTreeVisitor)
        {
            this.m_SpecimenOrderCollection.PullOver(accessionTreeVisitor);
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_SpecimenOrderCollection)
            {
                specimenOrder.PullOver(accessionTreeVisitor);
                specimenOrder.AliquotOrderCollection.PullOver(accessionTreeVisitor);

                foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    aliquotOrder.PullOver(accessionTreeVisitor);
                    aliquotOrder.TestOrderCollection.PullOver(accessionTreeVisitor);

                    foreach (YellowstonePathology.Business.Test.Model.TestOrder_Base testOrder in aliquotOrder.TestOrderCollection)
                    {
                        testOrder.PullOver(accessionTreeVisitor);
                    }
                }
            }
        }

        public void TakeATripRightSide(YellowstonePathology.Business.Visitor.AccessionTreeVisitor accessionTreeVisitor)
        {
            this.m_PanelSetOrderCollection.PullOver(accessionTreeVisitor);
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this.m_PanelSetOrderCollection)
            {
                panelSetOrder.PullOver(accessionTreeVisitor);
                panelSetOrder.PanelOrderCollection.PullOver(accessionTreeVisitor);

                foreach(YellowstonePathology.Business.Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
                {
                    panelOrder.PullOver(accessionTreeVisitor);
                    panelOrder.TestOrderCollection.PullOver(accessionTreeVisitor);

                    foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in panelOrder.TestOrderCollection)
                    {
                        testOrder.PullOver(accessionTreeVisitor);                        
                    }
                }
            }
        }

        public YellowstonePathology.Business.Task.Model.TaskOrder CreateTask(YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo)
        {
			string taskOrderId = Business.OrderIdParser.GetNextTaskOrderId(this.TaskOrderCollection, this.MasterAccessionNo);
            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.Task.Model.TaskOrder taskOrder = new Business.Task.Model.TaskOrder(taskOrderId, objectId, this.MasterAccessionNo,
                testOrderInfo.PanelSetOrder.ReportNo, testOrderInfo.PanelSet.TaskCollection[0], testOrderInfo.OrderTarget, testOrderInfo.PanelSet.PanelSetName, YellowstonePathology.Business.Task.Model.TaskAcknowledgementType.Immediate);

            foreach (YellowstonePathology.Business.Task.Model.Task task in testOrderInfo.PanelSet.TaskCollection)
            {
				string taskOrderDetailId = Business.OrderIdParser.GetNextTaskOrderDetailId(taskOrder.TaskOrderDetailCollection, taskOrderId);
                string taskOrderDetailObjectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail = Business.Task.Model.TaskOrderDetailFactory.GetTaskOrderDetail(taskOrderDetailId, taskOrderId, taskOrderDetailObjectId, task, this.m_ClientId);

                if (this.ClientAccessioned == true)
                {                    
                    taskOrderDetail.AppendComment("Client Accessioned: " + this.m_ClientName + ", " + this.m_ClientAccessionNo);
                }

                taskOrder.TaskOrderDetailCollection.Add(taskOrderDetail);
            }

            this.HandleTaskForSendoutTests(testOrderInfo, taskOrder);
            return taskOrder;
        }	      

        private void HandleTaskForSendoutTests(YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo, YellowstonePathology.Business.Task.Model.TaskOrder taskOrder)
        {
            Business.Facility.Model.FacilityCollection ypiFacilities = Business.Facility.Model.FacilityCollection.GetAllYPFacilities();            
            if(ypiFacilities.Exists(testOrderInfo.PanelSetOrder.TechnicalComponentFacilityId) == false)
            {
                PanelSetOrder panelSetOrder = this.PanelSetOrderCollection.GetPanelSetOrder(testOrderInfo.PanelSetOrder.ReportNo);
                foreach (ReportDistribution.Model.TestOrderReportDistribution reportDistribution in panelSetOrder.TestOrderReportDistributionCollection)
                {
                    Client.Model.Client client = Gateway.PhysicianClientGateway.GetClientByClientId(reportDistribution.ClientId);
                    if (client.SendAdditionalTestingNotifications == true)
                    {
                        YellowstonePathology.Business.Task.Model.TaskFax task = new Business.Task.Model.TaskFax(string.Empty, string.Empty, "AdditionalTestingNotification");
                        string taskOrderDetailId = Business.OrderIdParser.GetNextTaskOrderDetailId(taskOrder.TaskOrderDetailCollection, taskOrder.TaskOrderId);
                        string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                        YellowstonePathology.Business.Task.Model.TaskOrderDetailFax taskOrderDetail = new Business.Task.Model.TaskOrderDetailFax(taskOrderDetailId, taskOrder.TaskOrderId, objectId, task, this.m_ClientId);
                        taskOrderDetail.FaxNumber = client.AdditionalTestingNotificationFax;
                        taskOrderDetail.SendToName = client.AdditionalTestingNotificationContact;
                        taskOrder.TaskOrderDetailCollection.Add(taskOrderDetail);
                    }
                }
            }
        }

        public virtual void PullOver(YellowstonePathology.Business.Visitor.AccessionTreeVisitor accessionTreeVisitor)
        {
            accessionTreeVisitor.Visit(this);
        }

        public string ToResultString(string reportNo)
        {
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
            StringBuilder result = new StringBuilder();
            result.AppendLine("Report No: " + reportNo);
            result.AppendLine("Last Name: " + this.m_PLastName);
            result.AppendLine("First Name: " + this.m_PFirstName);
            result.AppendLine("Birthdate: " + this.m_PBirthdate.Value.ToShortDateString());
            result.AppendLine();
			result.AppendLine("Report for: " + panelSetOrder.PanelSetName);
            result.AppendLine(panelSetOrder.ToResultString(this));
            return result.ToString();
        }                          

        public void OnMessageRecieved(string message)
        {
            if(this.MessageRecieved != null)
            {
                this.MessageRecieved(message);
            }
        }
        
        public void Sync(DataTable dataTable)
        {
            DataTableReader dataTableReader = new DataTableReader(dataTable);
            dataTableReader.Read();
            Persistence.SqlDataTableReaderPropertyWriter sqlDataTableReaderPropertyWriter = new SqlDataTableReaderPropertyWriter(this, dataTableReader);
            sqlDataTableReaderPropertyWriter.WriteProperties();
        }
        
        public void SetCaseOwnerId()
        {
            if (this.m_CaseOwnerId == 0)
            {
                int id = 0;
                if (this.m_PanelSetOrderCollection.HasSurgical() == true)
                {
                    id = this.m_PanelSetOrderCollection.GetSurgical().AssignedToId;
                }

                if (id == 0)
                {
                    LLP.LeukemiaLymphomaTest leukemiaLymphomaTest = new LLP.LeukemiaLymphomaTest();
                    foreach (PanelSetOrder panelSetOrder in this.m_PanelSetOrderCollection)
                    {
                        if (panelSetOrder.PanelSetId == leukemiaLymphomaTest.PanelSetId && panelSetOrder.AssignedToId != 0)
                        {
                            id = panelSetOrder.AssignedToId;
                            break;
                        }
                    }
                }

                if (id == 0)
                {
                    if (User.SystemIdentity.Instance.User.IsUserInRole(User.SystemUserRoleDescriptionEnum.Pathologist) == true)
                    {
                        id = User.SystemIdentity.Instance.User.UserId;
                    }
                }

                this.m_CaseOwnerId = id;
            }
        }

        [PersistentCollection()]
        public YellowstonePathology.Business.Amendment.Model.AmendmentCollection AmendmentCollection
        {
            get { return this.m_AmendmentCollection; }
            set { this.m_AmendmentCollection = value; }
        }

        public YellowstonePathology.Business.Amendment.Model.Amendment AddAmendment(string reportNo)
        {
            string amendmentId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.Amendment.Model.Amendment amendment = this.m_AmendmentCollection.GetNextItem(this.m_MasterAccessionNo, reportNo, amendmentId);

            if (string.IsNullOrEmpty(reportNo) == false)
            {
                if(this.m_PanelSetOrderCollection.GetPanelSetOrder(reportNo) is YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder)
                {
                    Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_PanelSetOrderCollection.GetSurgical();
                    surgicalTestOrder.HandleNewAmendment(amendment);
                }
            }

            this.m_AmendmentCollection.Add(amendment);
            return amendment;
        }

        public void DeleteAmendment(string amendmentId)
        {
            YellowstonePathology.Business.Amendment.Model.Amendment amendment = this.m_AmendmentCollection.GetAmendment(amendmentId);
            if (this.m_PanelSetOrderCollection.HasSurgical() == true)
            {
                Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_PanelSetOrderCollection.GetSurgical();
                surgicalTestOrder.DeleteAmendment(amendment.AmendmentId);
            }
            this.m_AmendmentCollection.Remove(amendment);
        }

        public void SetDistribution()
        {
			if (this.PanelSetOrderCollection.DoesPanelSetExist(400) == true && this.m_DistributeToPatient == true)
			{
				this.HandleCOVIDPatientDistribution();					
			}
			
			if (this.PanelSetOrderCollection.DoesPanelSetExist(415) == true && this.m_DistributeToPatient == true)
			{
				this.HandleAPTIMACOVIDPatientDistribution();
			}
			
			if (this.PanelSetOrderCollection.DoesPanelSetExist(378) == true || this.PanelSetOrderCollection.DoesPanelSetExist(379))
			{
				this.HandleElectroPhoresisDistribution();				
			}

			if (this.m_ClientId != 1134) //YPI
			{
				foreach (PanelSetOrder panelSetOrder in this.PanelSetOrderCollection)
				{
					YellowstonePathology.Business.Client.Model.PhysicianClientDistributionList physicianClientDistributionCollection = Business.Gateway.ReportDistributionGateway.GetPhysicianClientDistributionCollection(this.PhysicianId, this.ClientId);
					physicianClientDistributionCollection.SetDistribution(panelSetOrder, this);
					panelSetOrder.TestOrderReportDistributionCollection.AddWebSerivceDistribution(panelSetOrder.ReportNo, this.m_ClientName, this.m_ClientId, this.m_PhysicianName, this.m_PhysicianId);
				}
			}

			this.HandleReportCopyTo();
        }

		public void HandleReportCopyTo()
		{
			if(string.IsNullOrEmpty(this.m_ReportCopyTo) == false)
			{
				string[] commaSplit = this.m_ReportCopyTo.Split(',');
				if(commaSplit.Length == 2)
				{
					int physicianId = Convert.ToInt32(commaSplit[1]);
					int clientId = Convert.ToInt32(commaSplit[0]);

					foreach (PanelSetOrder panelSetOrder in this.PanelSetOrderCollection)
					{
						YellowstonePathology.Business.Client.Model.PhysicianClientDistributionList physicianClientDistributionCollection = Business.Gateway.ReportDistributionGateway.GetPhysicianClientDistributionCollection(physicianId, clientId);
						physicianClientDistributionCollection.SetDistribution(panelSetOrder, this);
					}						
				}
			}
		}

        public void SetDistribution(Business.Client.Model.PhysicianClientDistributionListItem physicianClientDistribution)
        {
			if (this.PanelSetOrderCollection.DoesPanelSetExist(400) == true && DistributeToPatient == true)
			{
				this.HandleCOVIDPatientDistribution();				
			}
			if (this.m_ClientId != 1134) //YPI
			{
				foreach (PanelSetOrder panelSetOrder in this.PanelSetOrderCollection)
				{
					physicianClientDistribution.SetDistribution(panelSetOrder, this);
				}
			}
        }

		private void HandleCOVIDPatientDistribution()
		{			
			Business.Test.SARSCoV2.SARSCoV2TestOrder testOrder = (Business.Test.SARSCoV2.SARSCoV2TestOrder)this.m_PanelSetOrderCollection.GetPanelSetOrder(400);
			if (string.IsNullOrEmpty(PatientDistributionType) == false)
			{										
				if (PatientDistributionType == "Text" || PatientDistributionType == "Email" || PatientDistributionType == "Text and Email")
				{
					if (testOrder.TestOrderReportDistributionCollection.Exists(0, 0, this.m_PatientDistributionType) == false)
					{
						string clientName = $"{this.m_PFirstName} {this.m_PLastName}";
						string testOrderReportDistributionId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
						YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution =
							new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution(testOrderReportDistributionId, testOrderReportDistributionId, testOrder.ReportNo, 0, null, 0, clientName, this.PatientDistributionType, null);
						testOrderReportDistribution.PhoneNumber = this.m_PPhoneNumberHome;
						testOrderReportDistribution.EmailAddress = this.m_PEmailAddress;
						testOrder.TestOrderReportDistributionCollection.Add(testOrderReportDistribution);
					}
				}				
			}			
		}

		private void HandleAPTIMACOVIDPatientDistribution()
		{
			Business.Test.APTIMASARSCoV2.APTIMASARSCoV2TestOrder testOrder = (Business.Test.APTIMASARSCoV2.APTIMASARSCoV2TestOrder)this.m_PanelSetOrderCollection.GetPanelSetOrder(415);
			if (string.IsNullOrEmpty(PatientDistributionType) == false)
			{
				if (PatientDistributionType == "Text" || PatientDistributionType == "Email" || PatientDistributionType == "Text and Email")
				{
					if (testOrder.TestOrderReportDistributionCollection.Exists(0, 0, this.m_PatientDistributionType) == false)
					{
						string clientName = $"{this.m_PFirstName} {this.m_PLastName}";
						string testOrderReportDistributionId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
						YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution =
							new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution(testOrderReportDistributionId, testOrderReportDistributionId, testOrder.ReportNo, 0, null, 0, clientName, this.PatientDistributionType, null);
						testOrderReportDistribution.PhoneNumber = this.m_PPhoneNumberHome;
						testOrderReportDistribution.EmailAddress = this.m_PEmailAddress;
						testOrder.TestOrderReportDistributionCollection.Add(testOrderReportDistribution);
					}
				}
			}
		}

		private void HandleElectroPhoresisDistribution()
		{
			List<Business.Test.PanelSetOrder> panelSetOrders = new List<Business.Test.PanelSetOrder>();
			Business.Test.PanelSetOrder spep = this.m_PanelSetOrderCollection.GetPanelSetOrder(379);
			if (spep != null) panelSetOrders.Add(spep);

			Business.Test.PanelSetOrder iep = this.m_PanelSetOrderCollection.GetPanelSetOrder(378);
			if (iep != null) panelSetOrders.Add(iep);

			foreach(Business.Test.PanelSetOrder pso in panelSetOrders)
            {
				if (pso.TestOrderReportDistributionCollection.HasEPICDistribution() == false)
				{					
					string testOrderReportDistributionId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
					YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution =
						new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution(testOrderReportDistributionId, testOrderReportDistributionId, pso.ReportNo, this.m_PhysicianId, this.m_PhysicianName, this.m_ClientId, this.m_ClientName, "EPIC", null);					
					pso.TestOrderReportDistributionCollection.Add(testOrderReportDistribution);
				}
			}			
		}
	}
}
