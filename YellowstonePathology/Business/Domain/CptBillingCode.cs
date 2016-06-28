using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Workflow.Activities.Rules;
using YellowstonePathology.Business.Persistence;
using System.ComponentModel;

namespace YellowstonePathology.Business.Domain
{	
	public class CptBillingCode : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		string m_EffectiveFacilityType;

		private string m_CptBillingId;
		private string m_ReportNo;
		private string m_CptCode;
		private Nullable<DateTime> m_BillingDate;
		private string m_Description;
		private int m_Quantity;
		private string m_FeeType;
		private string m_PatientType;
		private string m_PrimaryInsurance;
		private string m_SecondaryInsurance;
		private int m_ClientId;
		private string m_BillingType;
		private bool m_Ordered14DaysPostDischarge;
		private int m_PanelSetId;
		private string m_RuleApplied;
		private string m_SurgicalSpecimenId;
		private string m_SpecimenOrderId;
		private string m_CaseOrigination;
		private bool m_Locked;
		private string m_ProfessionalCharge;
		private string m_TechnicalCharge;
		private Nullable<DateTime> m_DateOfService;
		private string m_Modifier;
		private string m_BillTo;

        public CptBillingCode()
        {
            
        }

        public CptBillingCode Clone()
        {
            return (CptBillingCode)this.MemberwiseClone();
        }

        public void SetBillingType()
        {           
            YellowstonePathology.Business.Rules.Billing.RulesSetBillingType rulesSetBillingType = YellowstonePathology.Business.Rules.Billing.RulesSetBillingType.Instance;
            rulesSetBillingType.CptBillingCode = this;
            rulesSetBillingType.Execute();            
        }

        public string EffectiveFacilityType
        {
            get { return this.m_EffectiveFacilityType; }
            set { this.m_EffectiveFacilityType = value; }
        }

        public string EffectiveInsurance
        {
            get
            {
                string result = this.m_PrimaryInsurance;                
                return result;
            }
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		[PersistentPrimaryKeyProperty(true)]
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
		public string FeeType
		{
			get { return this.m_FeeType; }
			set
			{
				if (this.m_FeeType != value)
				{
					this.m_FeeType = value;
					this.NotifyPropertyChanged("FeeType");
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
		public string CaseOrigination
		{
			get { return this.m_CaseOrigination; }
			set
			{
				if (this.m_CaseOrigination != value)
				{
					this.m_CaseOrigination = value;
					this.NotifyPropertyChanged("CaseOrigination");
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
		public string BillTo
		{
			get { return this.m_BillTo; }
			set
			{
				if (this.m_BillTo != value)
				{
					this.m_BillTo = value;
					this.NotifyPropertyChanged("BillTo");
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

		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_CptBillingId = propertyWriter.WriteString("CptBillingId");
			this.m_ReportNo = propertyWriter.WriteString("ReportNo");
			this.m_CptCode = propertyWriter.WriteString("CptCode");
			this.m_BillingDate = propertyWriter.WriteNullableDateTime("BillingDate");
			this.m_Description = propertyWriter.WriteString("Description");
			this.m_Quantity = propertyWriter.WriteInt("Quantity");
			this.m_FeeType = propertyWriter.WriteString("FeeType");
			this.m_PatientType = propertyWriter.WriteString("PatientType");
			this.m_PrimaryInsurance = propertyWriter.WriteString("PrimaryInsurance");
			this.m_SecondaryInsurance = propertyWriter.WriteString("SecondaryInsurance");
			this.m_ClientId = propertyWriter.WriteInt("ClientId");
			this.m_BillingType = propertyWriter.WriteString("BillingType");
			this.m_Ordered14DaysPostDischarge = propertyWriter.WriteBoolean("Ordered14DaysPostDischarge");
			this.m_PanelSetId = propertyWriter.WriteInt("PanelSetId");
			this.m_RuleApplied = propertyWriter.WriteString("RuleApplied");
			this.m_SurgicalSpecimenId = propertyWriter.WriteString("SurgicalSpecimenId");
			this.m_SpecimenOrderId = propertyWriter.WriteString("SpecimenOrderId");
			this.m_CaseOrigination = propertyWriter.WriteString("CaseOrigination");
			this.m_Locked = propertyWriter.WriteBoolean("Locked");
			this.m_ProfessionalCharge = propertyWriter.WriteString("ProfessionalCharge");
			this.m_TechnicalCharge = propertyWriter.WriteString("TechnicalCharge");
			this.m_DateOfService = propertyWriter.WriteNullableDateTime("DateOfService");
			this.m_BillTo = propertyWriter.WriteString("BillTo");
			this.m_Modifier = propertyWriter.WriteString("Modifier");
		}

		public void ReadProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyReader propertyReader)
		{
			propertyReader.ReadString("CptBillingId", CptBillingId);
			propertyReader.ReadString("ReportNo", ReportNo);
			propertyReader.ReadString("Code", CptCode);
			propertyReader.ReadNullableDateTime("BillingDate", BillingDate);
			propertyReader.ReadString("Description", Description);
			propertyReader.ReadInt("Quantity", Quantity);
			propertyReader.ReadString("FeeType", FeeType);
			propertyReader.ReadString("PatientType", PatientType);
			propertyReader.ReadString("PrimaryInsurance", PrimaryInsurance);
			propertyReader.ReadString("SecondaryInsurance", SecondaryInsurance);
			propertyReader.ReadInt("ClientId", ClientId);
			propertyReader.ReadString("BillingType", BillingType);
			propertyReader.ReadBoolean("Ordered14DaysPostDischarge", Ordered14DaysPostDischarge);
			propertyReader.ReadInt("PanelSetId", PanelSetId);
			propertyReader.ReadString("RuleApplied", RuleApplied);
			propertyReader.ReadString("SurgicalSpecimenId", SurgicalSpecimenId);
			propertyReader.ReadString("SpecimenOrderId", SpecimenOrderId);
			propertyReader.ReadString("CaseOrigination", CaseOrigination);
			propertyReader.ReadBoolean("Locked", Locked);
			propertyReader.ReadString("ProfessionalCharge", ProfessionalCharge);
			propertyReader.ReadString("TechnicalCharge", TechnicalCharge);
			propertyReader.ReadNullableDateTime("DateOfService", DateOfService);
			propertyReader.ReadString("BillTo", BillTo);
			propertyReader.ReadString("Modifier", Modifier);
		}
	}
}
