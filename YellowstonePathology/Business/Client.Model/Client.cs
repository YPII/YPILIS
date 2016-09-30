using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Xml.Linq;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Client.Model
{
    [PersistentClass("tblClient", "YPIDATA")]
	public class Client : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;				

		private ClientLocationCollection m_ClientLocationCollection;

        private string m_ObjectId;
        private int m_ClientId;
		private string m_ClientName;
        private string m_Abbreviation;
		private string m_Address;
		private string m_City;
		private string m_State;
		private string m_ZipCode;
		private string m_Telephone;
		private string m_FacilityType;						
		private string m_Fax;
		private bool m_LongDistance;		
		private bool m_ShowPhysiciansOnRequisition;
		private string m_BillingRuleSetId;
        private string m_BillingRuleSetId2;
		private string m_DistributionType;		
		private bool m_Inactive;
        private string m_ContactName;
        private bool m_HasReferringProvider;
        private string m_ReferringProviderClientId;
        private string m_ReferringProviderClientName;
        private string m_AdditionalTestingNotificationEmail;

        public Client()
        {
			this.m_ClientLocationCollection = new ClientLocationCollection();
        }

		public Client(string objectId, string clientName, int clientId)
		{
			this.m_ObjectId = objectId;
			this.m_ClientName = clientName;
			this.m_ClientId = clientId;
			this.m_ClientLocationCollection = new ClientLocationCollection();
		}

        [PersistentCollection()]
        public ClientLocationCollection ClientLocationCollection
		{
			get { return this.m_ClientLocationCollection; }
            set { this.m_ClientLocationCollection = value; }
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
        [PersistentDataColumnProperty(false, "11", "null", "int")]
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
        [PersistentDataColumnProperty(true, "300", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string Abbreviation
        {
            get { return this.m_Abbreviation; }
            set
            {
                if (this.m_Abbreviation != value)
                {
                    this.m_Abbreviation = value;
                    this.NotifyPropertyChanged("m_Abbreviation");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "300", "null", "varchar")]
		public string Address
		{
			get { return this.m_Address; }
			set
			{
				if (this.m_Address != value)
				{
					this.m_Address = value;
					this.NotifyPropertyChanged("Address");					
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string City
		{
			get { return this.m_City; }
			set
			{
				if (this.m_City != value)
				{
					this.m_City = value;
					this.NotifyPropertyChanged("City");					
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string State
		{
			get { return this.m_State; }
			set
			{
				if (this.m_State != value)
				{
					this.m_State = value;
					this.NotifyPropertyChanged("State");					
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ZipCode
		{
			get { return this.m_ZipCode; }
			set
			{
				if (this.m_ZipCode != value)
				{
					this.m_ZipCode = value;
					this.NotifyPropertyChanged("ZipCode");					
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string Telephone
		{
			get { return this.m_Telephone; }
			set
			{
				if (this.m_Telephone != value)
				{
					this.m_Telephone = value;
					this.NotifyPropertyChanged("Telephone");
                    this.NotifyPropertyChanged("FormattedTelephone");
                }
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "'Non-Hospital'", "varchar")]
		public string FacilityType
		{
			get { return this.m_FacilityType; }
			set
			{
				if (this.m_FacilityType != value)
				{
					this.m_FacilityType = value;
					this.NotifyPropertyChanged("FacilityType");				
				}
			}
		}                                        

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string Fax
		{
			get { return this.m_Fax; }
			set
			{
				if (this.m_Fax != value)
				{
					this.m_Fax = value;
					this.NotifyPropertyChanged("Fax");
                    this.NotifyPropertyChanged("FormattedFax");
                }
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "bit")]
		public bool LongDistance
		{
			get { return this.m_LongDistance; }
			set
			{
				if (this.m_LongDistance != value)
				{
					this.m_LongDistance = value;
					this.NotifyPropertyChanged("LongDistance");					
				}
			}
		}        

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "1", "bit")]
		public bool ShowPhysiciansOnRequisition
		{
			get { return this.m_ShowPhysiciansOnRequisition; }
			set
			{
				if (this.m_ShowPhysiciansOnRequisition != value)
				{
					this.m_ShowPhysiciansOnRequisition = value;
					this.NotifyPropertyChanged("ShowPhysiciansOnRequisition");					
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string BillingRuleSetId
		{
			get { return this.m_BillingRuleSetId; }
			set
			{
				if (this.m_BillingRuleSetId != value)
				{
					this.m_BillingRuleSetId = value;
					this.NotifyPropertyChanged("BillingRuleSetId");					
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string BillingRuleSetId2
        {
            get { return this.m_BillingRuleSetId2; }
            set
            {
                if (this.m_BillingRuleSetId2 != value)
                {
                    this.m_BillingRuleSetId2 = value;
                    this.NotifyPropertyChanged("BillingRuleSetId2");                    
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string DistributionType
		{
			get { return this.m_DistributionType; }
			set
			{
				if (this.m_DistributionType != value)
				{
					this.m_DistributionType = value;
					this.NotifyPropertyChanged("DistributionType");					
				}
			}
		}               

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "bit")]
		public bool Inactive
		{
			get { return this.m_Inactive; }
			set
			{
				if (this.m_Inactive != value)
				{
					this.m_Inactive = value;
					this.NotifyPropertyChanged("Inactive");					
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "200", "null", "nvarchar")]
        public string ContactName
        {
            get { return this.m_ContactName; }
            set
            {
                if (this.m_ContactName != value)
                {
                    this.m_ContactName = value;
                    this.NotifyPropertyChanged("ContactName");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "bit")]
        public bool HasReferringProvider
        {
            get { return this.m_HasReferringProvider; }
            set
            {
                if (this.m_HasReferringProvider != value)
                {
                    this.m_HasReferringProvider = value;
                    this.NotifyPropertyChanged("HasReferringProvider");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string ReferringProviderClientId
        {
            get { return this.m_ReferringProviderClientId; }
            set
            {
                if (this.m_ReferringProviderClientId != value)
                {
                    this.m_ReferringProviderClientId = value;
                    this.NotifyPropertyChanged("ReferringProviderClientId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string ReferringProviderClientName
        {
            get { return this.m_ReferringProviderClientName; }
            set
            {
                if (this.m_ReferringProviderClientName != value)
                {
                    this.m_ReferringProviderClientName = value;
                    this.NotifyPropertyChanged("ReferringProviderClientName");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string AdditionalTestingNotificationEmail
        {
            get { return this.m_AdditionalTestingNotificationEmail; }
            set
            {
                if (this.m_AdditionalTestingNotificationEmail != value)
                {
                    this.m_AdditionalTestingNotificationEmail = value;
                    this.NotifyPropertyChanged("AdditionalTestingNotificationEmail");
                }
            }
        }

        public string FormattedTelephone
        {
            get
            {
                string result = string.Empty;
                if (string.IsNullOrEmpty(this.m_Telephone) == false && this.m_Telephone.Length == 10)
                {
                    result = "(" + m_Telephone.Substring(0, 3) + ") " + m_Telephone.Substring(3, 3) + "-" + m_Telephone.Substring(6, 4);
                }
                return result;
            }            
        }

        public string FormattedFax
        {
            get
            {                
                string result = string.Empty;
                if (string.IsNullOrEmpty(this.m_Fax) == false && this.m_Fax.Length == 10)
                {
                    result = "(" + m_Fax.Substring(0, 3) + ") " + m_Fax.Substring(3, 3) + "-" + m_Fax.Substring(6, 4);
                }
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
	}
}
