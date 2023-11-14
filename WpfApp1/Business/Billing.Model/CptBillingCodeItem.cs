using System;
using System.ComponentModel;
using System.Xml.Linq;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Billing.Model
{	
	public class CptBillingCodeItem : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private bool m_BillingIsOpen;

		private ICD9BillingCodeCollection m_ICD9BillingCodeCollection;

		private string m_CptBillingId;
		private string m_ReportNo;
		private string m_MasterAccessionNo;
		private string m_SpecimenOrderId;
		private string m_SurgicalSpecimenId;
		private bool m_NoCharge;
		private bool m_Ordered14DaysPostDischarge;
		private Nullable<DateTime> m_BillingDate;
		private Nullable<DateTime> m_ModifiedDate;
		private int m_SpecimenId;
		private int m_StainOrderId;
		private int m_Quantity;
		private int m_ClientId;
		private int m_BillingTypeId;
		private int m_PanelSetId;
		private string m_TestOrderId;
		private string m_CptCode;
		private string m_Modifier;
		private string m_Description;		
		private string m_ProfessionalCharge;
		private string m_PCCharge;
		private string m_BillingType;
		private string m_RuleApplied;
		private string m_CaseType;
		private string m_PatientType;
		private string m_PrimaryInsurance;
		private string m_SecondaryInsurance;
		private string m_TechnicalCharge;
		private Nullable<DateTime> m_DateOfService;
		private bool m_Reversed;
		private bool m_Locked;
		private bool m_Audited;
		private string m_ProfessionalComponentFacilityId;
		private string m_TechnicalComponentFacilityId;
		private string m_ObjectId;

		public CptBillingCodeItem()
        {
			this.m_ICD9BillingCodeCollection = new ICD9BillingCodeCollection();
		}		
		
		public ICD9BillingCodeCollection ICD9BillingCodeCollection
        {
            get { return this.m_ICD9BillingCodeCollection; }
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
		public string CptBillingId
		{
			get { return this.m_CptBillingId; }
			set
			{
				if (this.m_CptBillingId != value)
				{
					this.m_CptBillingId = value;
					this.NotifyPropertyChanged("CptBillingId");
				}
			}
		}

		[PersistentProperty()]
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
		public Nullable<DateTime> BillingDate
		{
			get { return this.m_BillingDate; }
			set
			{
				if (this.m_BillingDate != value)
				{
					this.m_BillingDate = value;
					this.NotifyPropertyChanged("BillingDate");
				}
			}
		}

		[PersistentProperty()]
		public Nullable<DateTime> ModifiedDate
		{
			get { return this.m_ModifiedDate; }
			set
			{
				if (this.m_ModifiedDate != value)
				{
					this.m_ModifiedDate = value;
					this.NotifyPropertyChanged("ModifiedDate");
				}
			}
		}

		[PersistentProperty()]
		public int SpecimenId
		{
			get { return this.m_SpecimenId; }
			set
			{
				if (this.m_SpecimenId != value)
				{
					this.m_SpecimenId = value;
					this.NotifyPropertyChanged("SpecimenId");
				}
			}
		}

		[PersistentProperty()]
		public int StainOrderId
		{
			get { return this.m_StainOrderId; }
			set
			{
				if (this.m_StainOrderId != value)
				{
					this.m_StainOrderId = value;
					this.NotifyPropertyChanged("StainOrderId");
				}
			}
		}

		[PersistentProperty()]
		public int Quantity
		{
			get { return this.m_Quantity; }
			set
			{
				if (this.m_Quantity != value)
				{
					this.m_Quantity = value;
					this.NotifyPropertyChanged("Quantity");
				}
			}
		}

		[PersistentProperty()]
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
		public int BillingTypeId
		{
			get { return this.m_BillingTypeId; }
			set
			{
				if (this.m_BillingTypeId != value)
				{
					this.m_BillingTypeId = value;
					this.NotifyPropertyChanged("BillingTypeId");
				}
			}
		}

		[PersistentProperty()]
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
		public string CptCode
		{
			get { return this.m_CptCode; }
			set
			{
				if (this.m_CptCode != value)
				{
					this.m_CptCode = value;
					this.NotifyPropertyChanged("CptCode");
				}
			}
		}

		[PersistentProperty()]
		public string Modifier
		{
			get { return this.m_Modifier; }
			set
			{
				if (this.m_Modifier != value)
				{
					this.m_Modifier = value;
					this.NotifyPropertyChanged("Modifier");
				}
			}
		}

		[PersistentProperty()]
		public string Description
		{
			get { return this.m_Description; }
			set
			{
				if (this.m_Description != value)
				{
					this.m_Description = value;
					this.NotifyPropertyChanged("Description");
				}
			}
		}		

		[PersistentProperty()]
		public string ProfessionalCharge
		{
			get { return this.m_ProfessionalCharge; }
			set
			{
				if (this.m_ProfessionalCharge != value)
				{
					this.m_ProfessionalCharge = value;
					this.NotifyPropertyChanged("ProfessionalCharge");
				}
			}
		}

		[PersistentProperty()]
		public string PCCharge
		{
			get { return this.m_PCCharge; }
			set
			{
				if (this.m_PCCharge != value)
				{
					this.m_PCCharge = value;
					this.NotifyPropertyChanged("PCCharge");
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
		public string RuleApplied
		{
			get { return this.m_RuleApplied; }
			set
			{
				if (this.m_RuleApplied != value)
				{
					this.m_RuleApplied = value;
					this.NotifyPropertyChanged("RuleApplied");
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

		[PersistentProperty()]
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
		public string TechnicalCharge
		{
			get { return this.m_TechnicalCharge; }
			set
			{
				if (this.m_TechnicalCharge != value)
				{
					this.m_TechnicalCharge = value;
					this.NotifyPropertyChanged("TechnicalCharge");
				}
			}
		}

		[PersistentProperty()]
		public Nullable<DateTime> DateOfService
		{
			get { return this.m_DateOfService; }
			set
			{
				if (this.m_DateOfService != value)
				{
					this.m_DateOfService = value;
					this.NotifyPropertyChanged("DateOfService");
				}
			}
		}

		[PersistentProperty()]
		public bool Reversed
		{
			get { return this.m_Reversed; }
			set
			{
				if (this.m_Reversed != value)
				{
					this.m_Reversed = value;
					this.NotifyPropertyChanged("Reversed");
				}
			}
		}

		[PersistentProperty()]
		public bool Locked
		{
			get { return this.m_Locked; }
			set
			{
				if (this.m_Locked != value)
				{
					this.m_Locked = value;
					this.NotifyPropertyChanged("Locked");
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

		public void FromXml(XElement xml)
		{
			throw new NotImplementedException("FromXml not implemented in CptBillingCode");
		}

		public XElement ToXml()
		{
			throw new NotImplementedException("ToXml not implemented in CptBillingCode");
		}

        public string CptCodeModifiedForPsa
        {
            get
            {
                string result = this.m_CptCode;
                if (result != null)
                {
                    if (this.m_NoCharge == true)
                    {
                        result = result + "/nc";
                    }                    
                }
                return result;
            }
        }

        public void AddDash26()
        {
            if (this.CptCode.EndsWith("-26") == false)
            {
                this.CptCode += "-26";
            }
        }

        public void RemoveDash26()
        {
            if (this.CptCode.EndsWith("-26") == true)
            {
                this.m_CptCode.Remove(this.m_CptCode.Length - 3);                
            }
        }

		public Boolean BillingIsOpen
        {
            get { return this.m_BillingIsOpen; }
            set
            {
                if (value != this.m_BillingIsOpen)
                {
                    this.m_BillingIsOpen = value;
                    this.NotifyPropertyChanged("BillingIsOpen");
                }
            }
        }        

        public void UpdateFromAccession(string patientType, string primaryInsurance, string secondaryInsurance, string professionalComponentFacilityId, string technicalComponentFacilityId, int clientId)
		{
			this.PatientType = patientType;
			this.PrimaryInsurance = primaryInsurance;
			this.SecondaryInsurance = secondaryInsurance;
            this.ProfessionalComponentFacilityId = professionalComponentFacilityId;
            this.TechnicalComponentFacilityId = technicalComponentFacilityId;
			this.ClientId = clientId;
		}

		public bool MatchesAccession(string patientType, string primaryInsurance, string secondaryInsurance, string professionalComponentFacilityid, string technicalComponentFacilityId, int clientId)
		{
			bool result = true;
			if (this.Reversed == false)
			{
				if (this.PatientType != patientType)
				{
					result = false;
				}
				if (this.PrimaryInsurance != primaryInsurance)
				{
					result = false;
				}
				if (this.SecondaryInsurance != secondaryInsurance)
				{
					result = false;
				}
				if (this.ProfessionalComponentFacilityId != professionalComponentFacilityid)
				{
					result = false;
				}
                if (this.TechnicalComponentFacilityId != technicalComponentFacilityId)
                {
                    result = false;
                }
				if (this.ClientId != clientId)
				{
					result = false;
				}
			}
			return result;
		}

		public void UpdateFromAccessionInformation(string patientType, string primaryInsurance, string secondaryInsurance, string professionalComponentFacilityId, string technicalComponentFacilityId, int clientId)
		{
			this.PatientType = patientType;
			this.PrimaryInsurance = primaryInsurance;
			this.SecondaryInsurance = secondaryInsurance;
            this.ProfessionalComponentFacilityId = professionalComponentFacilityId;
            this.TechnicalComponentFacilityId = technicalComponentFacilityId;
			this.ClientId = clientId;
		}

        public bool MatchesAccessionInformation(string patientType, string primaryInsurance, string secondaryInsurance, string professionalComponentFacilityId, string technicalComponentFacilityId, int clientId)
		{
			bool result = true;
			if (this.Reversed == false)
			{
				if (this.PatientType != patientType)
				{
					result = false;
				}
				if (this.PrimaryInsurance != primaryInsurance)
				{
					result = false;
				}
				if (this.SecondaryInsurance != secondaryInsurance)
				{
					result = false;
				}
				if (this.ProfessionalComponentFacilityId != professionalComponentFacilityId)
				{
					result = false;
				}
                if (this.TechnicalComponentFacilityId != technicalComponentFacilityId)
                {
                    result = false;
                }
				if (this.ClientId != clientId)
				{
					result = false;
				}
			}
			return result;
		}

		public bool IsOktoUpdate()
		{
			bool result = false;
			if (this.m_BillingDate.HasValue == false)
			{
				result = true;
			}
			else if(this.m_BillingDate == DateTime.Today)
			{
				result = true;
			}
			return result;
		}

		public CptBillingCodeItem Clone()
		{
            return (CptBillingCodeItem)this.MemberwiseClone();			
		}

		public CptBillingCodeItem CloneForReversal(string cloneId)
		{
			CptBillingCodeItem result = this.Clone();
			result.CptBillingId = cloneId;
			result.ObjectId = cloneId;
			result.BillingDate = null;
            result.Audited = false;
			return result;
		}

		public string BillingDateProxy
        {
            get
            {
                if (this.BillingDate.HasValue == false)
                {
                    return null;
                }
                else
                {
                    return this.BillingDate.Value.ToShortDateString();
                }
            }

            set
            {
                string strValue = value.ToString();
                if (strValue == string.Empty)
                {
                    this.BillingDate = null;
                }
                else
                {
                    this.BillingDate = DateTime.Parse(strValue);
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
