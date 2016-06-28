using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.Business.Facility.Model
{
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

		protected LocationCollection m_Locations;
        protected CLIALicense m_CliaLicense;        

		public Facility()
		{            
            this.m_Locations = new LocationCollection();            
		}

        public Facility GetDefaultBillingFacility(int clientId)
        {
            Facility result = null;
            return result;
        }        
		
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

		public LocationCollection Locations
        {
            get { return this.m_Locations; }
        }

        public CLIALicense CLIALicense
        {
            get { return this.m_CliaLicense; }
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

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
	}
}
