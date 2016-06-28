using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Billing.Model
{
	[PersistentClass("tblICD9BillingCode", "YPIDATA")]
	public class ICD9BillingCode : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private bool m_BillingIsOpen;

		private string m_ObjectId;
		private string m_Icd9BillingId;
		private string m_SpecimenOrderId;
		private string m_SurgicalSpecimenId;
		private Nullable<DateTime> m_BillingDate;
		private Nullable<DateTime> m_ModifiedDate;
		private int m_Quantity;
		private int m_SpecimenId;
		private int m_UserId;
		private string m_ICD9Code;
        private string m_ICD10Code;
		private string m_ReportNo;
		private int m_SpecimenLogId;
		private string m_MasterAccessionNo;
		private string m_DesignatedFor;
		private string m_Source;

		public ICD9BillingCode()
        {

		}

		public ICD9BillingCode(string icd9BillingId, string reportNo, string masterAccessionNo, string specimenOrderId, string icd9Code, string icd10Code, string objectId, int quantity)
		{
			this.m_Icd9BillingId = icd9BillingId;
            this.m_ReportNo = reportNo;
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_SpecimenOrderId = specimenOrderId;
            this.m_ICD9Code = icd9Code;
            this.m_ICD10Code = icd10Code;
			this.m_ObjectId = objectId;
            this.m_Quantity = quantity;
            this.NotifyPropertyChanged(string.Empty);
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
		public string Icd9BillingId
		{
			get { return this.m_Icd9BillingId; }
			set
			{
				if (this.m_Icd9BillingId != value)
				{
					this.m_Icd9BillingId = value;
					this.NotifyPropertyChanged("Icd9BillingId");
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
		public int UserId
		{
			get { return this.m_UserId; }
			set
			{
				if (this.m_UserId != value)
				{
					this.m_UserId = value;
					this.NotifyPropertyChanged("UserId");
				}
			}
		}

		[PersistentProperty()]
		public string ICD9Code
		{
			get { return this.m_ICD9Code; }
			set
			{
                if (this.m_ICD9Code != value)
				{
                    this.m_ICD9Code = value;
                    this.NotifyPropertyChanged("ICD9Code");
				}
			}
		}

        [PersistentProperty()]
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
		public string DesignatedFor
		{
			get { return this.m_DesignatedFor; }
			set
			{
				if (this.m_DesignatedFor != value)
				{
					this.m_DesignatedFor = value;
					this.NotifyPropertyChanged("DesignatedFor");
				}
			}
		}

		[PersistentProperty()]
		public string Source
		{
			get { return this.m_Source; }
			set
			{
				if (this.m_Source != value)
				{
					this.m_Source = value;
					this.NotifyPropertyChanged("Source");
				}
			}
		}
	}
}
