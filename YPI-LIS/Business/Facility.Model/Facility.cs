using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Facility.Model
{
    [PersistentClass("tblFacility", "YPIDATA")]
    public class Facility : INotifyPropertyChanged
	{        
		public event PropertyChangedEventHandler PropertyChanged;
       
		protected string m_FacilityId;
		protected string m_FacilityIdOLD;
		protected string m_FacilityName;
		protected string m_Address1;
		protected string m_Address2;
		protected string m_City;
		protected string m_State;
		protected string m_ZipCode;
		protected string m_PhoneNumber;
		protected string m_TollFreePhoneNumber;
        protected bool m_IsReferenceLab;
        protected string m_AccessioningLocation;
        protected string m_LocationAbbreviation;
        protected string m_FedexAccountNo;
        protected string m_FedexPaymentType;
        protected int m_ClientId;
        protected string m_EmailAddress;
        protected string m_CLIALicenseNumber;

		public Facility()
		{            
		}

        public Facility GetDefaultBillingFacility(int clientId)
        {
            Facility result = null;
            return result;
        }

        [PersistentPrimaryKeyProperty(false)]
        public string FacilityId
		{
			get { return this.m_FacilityId; }
			set
			{
				if(this.m_FacilityId != value)
				{
					this.m_FacilityId = value;
					this.NotifyPropertyChanged("FacilityId");					
				}
			}
		}

        [PersistentProperty()]
        public string FacilityIdOLD
        {
            get { return this.m_FacilityIdOLD; }
            set
            {
                if (this.m_FacilityIdOLD != value)
                {
                    this.m_FacilityIdOLD = value;
                    this.NotifyPropertyChanged("FacilityIdOLD");
                }
            }
        }

        [PersistentProperty()]
        public string FacilityName
		{
			get { return this.m_FacilityName; }
			set
			{
				if(this.m_FacilityName != value)
				{
					this.m_FacilityName = value;
					this.NotifyPropertyChanged("FacilityName");					
				}
			}
		}

        [PersistentProperty()]
        public string Address1
		{
			get { return this.m_Address1; }
			set
			{
				if(this.m_Address1 != value)
				{
					this.m_Address1 = value;
					this.NotifyPropertyChanged("Address1");					
				}
			}
		}

        [PersistentProperty()]
        public string Address2
		{
			get { return this.m_Address2; }
			set
			{
				if(this.m_Address2 != value)
				{
					this.m_Address2 = value;
					this.NotifyPropertyChanged("Address2");					
				}
			}
		}

        [PersistentProperty()]
        public string City
		{
			get { return this.m_City; }
			set
			{
				if(this.m_City != value)
				{
					this.m_City = value;
					this.NotifyPropertyChanged("City");					
				}
			}
		}

        [PersistentProperty()]
        public string State
		{
			get { return this.m_State; }
			set
			{
				if(this.m_State != value)
				{
					this.m_State = value;
					this.NotifyPropertyChanged("State");					
				}
			}
		}

        [PersistentProperty()]
        public string ZipCode
		{
			get { return this.m_ZipCode; }
			set
			{
				if(this.m_ZipCode != value)
				{
					this.m_ZipCode = value;
					this.NotifyPropertyChanged("ZipCode");					
				}
			}
		}

        [PersistentProperty()]
        public string PhoneNumber
		{
			get { return this.m_PhoneNumber; }
			set
			{
				if(this.m_PhoneNumber != value)
				{
					this.m_PhoneNumber = value;
					this.NotifyPropertyChanged("PhoneNumber");					
				}
			}
		}

        [PersistentProperty()]
        public string TollFreePhoneNumber
		{
			get { return this.m_TollFreePhoneNumber; }
			set
			{
				if(this.m_TollFreePhoneNumber != value)
				{
					this.m_TollFreePhoneNumber = value;
					this.NotifyPropertyChanged("TollFreePhoneNumber");					
				}
			}
		}

        [PersistentProperty()]
        public bool IsReferenceLab
        {
            get { return this.m_IsReferenceLab; }
            set
            {
                if (this.m_IsReferenceLab != value)
                {
                    this.m_IsReferenceLab = value;
                    this.NotifyPropertyChanged("IsReferenceLab");
                }
            }
        }

        [PersistentProperty()]
        public string AccessioningLocation
        {
            get { return this.m_AccessioningLocation; }
            set
            {
                if (this.m_AccessioningLocation != value)
                {
                    this.m_AccessioningLocation = value;
                    this.NotifyPropertyChanged("AccessioningLocation");
                }
            }
        }

        [PersistentProperty()]
        public string LocationAbbreviation
        {
            get { return this.m_LocationAbbreviation; }
            set
            {
                if (this.m_LocationAbbreviation != value)
                {
                    this.m_LocationAbbreviation = value;
                    this.NotifyPropertyChanged("LocationAbbreviation");
                }
            }
        }

        [PersistentProperty()]
        public string FedexAccountNo
        {
            get { return this.m_FedexAccountNo; }
            set
            {
                if (this.m_FedexAccountNo != value)
                {
                    this.m_FedexAccountNo = value;
                    this.NotifyPropertyChanged("FedexAccountNo");
                }
            }
        }

        [PersistentProperty()]
        public string FedexPaymentType
        {
            get { return this.m_FedexPaymentType; }
            set
            {
                if (this.m_FedexPaymentType != value)
                {
                    this.m_FedexPaymentType = value;
                    this.NotifyPropertyChanged("FedexPaymentType");
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
        public string EmailAddress
        {
            get { return this.m_EmailAddress; }
            set
            {
                if (this.m_EmailAddress != value)
                {
                    this.m_EmailAddress = value;
                    this.NotifyPropertyChanged("EmailAddress");
                }
            }
        }

        [PersistentProperty()]
        public string CLIALicenseNumber
        {
            get { return this.m_CLIALicenseNumber; }
            set
            {
                if (this.m_CLIALicenseNumber != value)
                {
                    this.m_CLIALicenseNumber = value;
                    this.NotifyPropertyChanged("CLIALicenseNumber");
                }
            }
        }

        public string GetCLIAAddressString()
        {
            StringBuilder result = new StringBuilder();
            result.Append(this.GetAddressString() + " (CLIA:" + this.m_CLIALicenseNumber + ").");
            return result.ToString();
        }

        public string GetAddressString()
        {
            StringBuilder result = new StringBuilder();
            result.Append(this.m_FacilityName);
            result.Append(", ");
            result.Append(this.m_Address1);
            result.Append(", ");

            if (string.IsNullOrEmpty(this.Address2) == false)
            {                
                result.Append(this.m_Address2);
                result.Append(", ");
            }

            result.Append(this.m_City + ", " + this.m_State + " " + this.m_ZipCode);
            return result.ToString();
        }

        public string PhoneNumberProxy
        {
            get { return YellowstonePathology.Business.Helper.PhoneNumberHelper.CorrectPhoneNumber(this.m_PhoneNumber); }
            set
            {
                if (this.m_PhoneNumber != value)
                {
                    this.m_PhoneNumber = value;
                    this.NotifyPropertyChanged("PhoneNumber");
                    this.NotifyPropertyChanged("PhoneNumberProxy");
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
