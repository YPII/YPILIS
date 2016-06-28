using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Xml.Linq;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Domain
{
    [PersistentClass("tblPhysician", "YPIDATA")]
    public class Physician : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

		private int m_PhysicianId;
		private string m_ObjectId;
		private string m_FirstName;
		private string m_LastName;
		private string m_MiddleInitial;
		private string m_Credentials;
		private bool m_Active;
		private string m_Address;
		private string m_City;
		private string m_State;
		private string m_Zip;
		private string m_Phone;
		private string m_Fax;
		private bool m_OutsideConsult;
		private bool m_HPVTest;
		private int m_HPVInstructionID;
		private int m_HPVTestToPerformID;
		private string m_FullName;		
		private int m_ReportDeliveryMethod;
		private string m_DisplayName;
		private int? m_HomeBaseClientId;
		private bool m_KRASBRAFStandingOrder;
		private string m_Npi;
        private string m_HPVStandingOrderCode;
		private string m_HPV1618StandingOrderCode;
        private string m_MDFirstName;
        private string m_MDLastName;
        private bool m_SendPublishNotifications;
        private string m_PublishNotificationEmailAddress;

		public Physician()
		{

		}

		public Physician(string objectId, int physicianId, string lastName, string firstName)
		{
			this.ObjectId = objectId;
			this.m_PhysicianId = physicianId;
			this.m_LastName = lastName;
			this.m_FirstName = firstName;
			this.m_DisplayName = lastName + " " + firstName;
			this.m_Active = true;
			Business.Client.Model.StandingOrderNotSet standingOrderNotSet = new Client.Model.StandingOrderNotSet();
			this.m_HPVStandingOrderCode = standingOrderNotSet.StandingOrderCode;
			this.m_HPV1618StandingOrderCode = standingOrderNotSet.StandingOrderCode;
		}

        [PersistentPrimaryKeyProperty(false)]
		public int PhysicianId
		{
			get { return this.m_PhysicianId; }
			set
			{
				if(this.m_PhysicianId != value)
				{
					this.m_PhysicianId = value;
					this.NotifyPropertyChanged("PhysicianId");					
				}
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

        [PersistentProperty()]
		public string FirstName
		{
			get { return this.m_FirstName; }
			set
			{
				if(this.m_FirstName != value)
				{
					this.m_FirstName = value;
					this.NotifyPropertyChanged("FirstName");					
				}
			}
		}

        [PersistentProperty()]
		public string LastName
		{
			get { return this.m_LastName; }
			set
			{
				if(this.m_LastName != value)
				{
					this.m_LastName = value;
					this.NotifyPropertyChanged("LastName");					
				}
			}
		}

        [PersistentProperty()]
		public string MiddleInitial
		{
			get { return this.m_MiddleInitial; }
			set
			{
				if(this.m_MiddleInitial != value)
				{
					this.m_MiddleInitial = value;
					this.NotifyPropertyChanged("MiddleInitial");					
				}
			}
		}

        [PersistentProperty()]
		public string Credentials
		{
			get { return this.m_Credentials; }
			set
			{
				if(this.m_Credentials != value)
				{
					this.m_Credentials = value;
					this.NotifyPropertyChanged("Credentials");					
				}
			}
		}

        [PersistentProperty()]
		public bool Active
		{
			get { return this.m_Active; }
			set
			{
				if(this.m_Active != value)
				{
					this.m_Active = value;
					this.NotifyPropertyChanged("Active");					
				}
			}
		}

        [PersistentProperty()]
		public string Address
		{
			get { return this.m_Address; }
			set
			{
				if(this.m_Address != value)
				{
					this.m_Address = value;
					this.NotifyPropertyChanged("Address");					
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
		public string Zip
		{
			get { return this.m_Zip; }
			set
			{
				if(this.m_Zip != value)
				{
					this.m_Zip = value;
					this.NotifyPropertyChanged("Zip");					
				}
			}
		}

        [PersistentProperty()]
		public string Phone
		{
			get { return this.m_Phone; }
			set
			{
				if(this.m_Phone != value)
				{
					this.m_Phone = value;
					this.NotifyPropertyChanged("Phone");					
				}
			}
		}

        [PersistentProperty()]
		public string Fax
		{
			get { return this.m_Fax; }
			set
			{
				if(this.m_Fax != value)
				{
					this.m_Fax = value;
					this.NotifyPropertyChanged("Fax");					
				}
			}
		}

        [PersistentProperty()]
		public bool OutsideConsult
		{
			get { return this.m_OutsideConsult; }
			set
			{
				if(this.m_OutsideConsult != value)
				{
					this.m_OutsideConsult = value;
					this.NotifyPropertyChanged("OutsideConsult");					
				}
			}
		}

        [PersistentProperty()]
		public bool HPVTest
		{
			get { return this.m_HPVTest; }
			set
			{
				if(this.m_HPVTest != value)
				{
					this.m_HPVTest = value;
					this.NotifyPropertyChanged("HPVTest");					
				}
			}
		}

        [PersistentProperty()]
		public int HPVInstructionID
		{
			get { return this.m_HPVInstructionID; }
			set
			{
				if(this.m_HPVInstructionID != value)
				{
					this.m_HPVInstructionID = value;
					this.NotifyPropertyChanged("HPVInstructionID");					
				}
			}
		}

        [PersistentProperty()]
		public int HPVTestToPerformID
		{
			get { return this.m_HPVTestToPerformID; }
			set
			{
				if(this.m_HPVTestToPerformID != value)
				{
					this.m_HPVTestToPerformID = value;
					this.NotifyPropertyChanged("HPVTestToPerformID");					
				}
			}
		}

        [PersistentProperty()]
		public string FullName
		{
			get { return this.m_FullName; }
			set
			{
				if(this.m_FullName != value)
				{
					this.m_FullName = value;
					this.NotifyPropertyChanged("FullName");					
				}
			}
		}

        [PersistentProperty()]
		public string HPVStandingOrderCode
		{
			get { return this.m_HPVStandingOrderCode; }
			set
			{
				if(this.m_HPVStandingOrderCode != value)
				{
					this.m_HPVStandingOrderCode = value;
					this.NotifyPropertyChanged("HPVStandingOrderCode");					
				}
			}
		}

        [PersistentProperty()]
		public int ReportDeliveryMethod
		{
			get { return this.m_ReportDeliveryMethod; }
			set
			{
				if(this.m_ReportDeliveryMethod != value)
				{
					this.m_ReportDeliveryMethod = value;
					this.NotifyPropertyChanged("ReportDeliveryMethod");					
				}
			}
		}

        [PersistentProperty()]
		public string DisplayName
		{
			get { return this.m_DisplayName; }
			set
			{
				if(this.m_DisplayName != value)
				{
					this.m_DisplayName = value;
					this.NotifyPropertyChanged("DisplayName");					
				}
			}
		}

        [PersistentProperty()]
		public int? HomeBaseClientId
		{
			get { return this.m_HomeBaseClientId; }
			set
			{
				if(this.m_HomeBaseClientId != value)
				{
					this.m_HomeBaseClientId = value;
					this.NotifyPropertyChanged("HomeBaseClientId");					
				}
			}
		}

        [PersistentProperty()]
		public bool KRASBRAFStandingOrder
		{
			get { return this.m_KRASBRAFStandingOrder; }
			set
			{
				if(this.m_KRASBRAFStandingOrder != value)
				{
					this.m_KRASBRAFStandingOrder = value;
					this.NotifyPropertyChanged("KRASBRAFStandingOrder");					
				}
			}
		}

        [PersistentProperty()]
		public string Npi
		{
			get { return this.m_Npi; }
			set
			{
				if(this.m_Npi != value)
				{
					this.m_Npi = value;
					this.NotifyPropertyChanged("Npi");					
				}
			}
		}

        [PersistentProperty()]
		public string HPV1618StandingOrderCode
		{
            get { return this.m_HPV1618StandingOrderCode; }
			set
			{
                if (this.m_HPV1618StandingOrderCode != value)
				{
                    this.m_HPV1618StandingOrderCode = value;
                    this.NotifyPropertyChanged("HPV1618StandingOrderCode");					
				}
			}
		}

        public string GetNormalizedMiddleInitial()
        {
            string result = this.m_MiddleInitial;
            if (string.IsNullOrEmpty(result) == false)
            {
                result = result.Replace(".", string.Empty);
                result = result.Trim();
            }
            return result;
        }

        [PersistentProperty()]
        public string MDFirstName
        {
            get { return this.m_MDFirstName; }
            set
            {
                if (this.m_MDFirstName != value)
                {
                    this.m_MDFirstName = value;
                    this.NotifyPropertyChanged("MDFirstName");
                }
            }
        }

        [PersistentProperty()]
        public string MDLastName
        {
            get { return this.m_MDLastName; }
            set
            {
                if (this.m_MDLastName != value)
                {
                    this.m_MDLastName = value;
                    this.NotifyPropertyChanged("MDLastName");
                }
            }
        }

        [PersistentProperty()]
        public bool SendPublishNotifications
        {
            get { return this.m_SendPublishNotifications; }
            set
            {
                if (this.m_SendPublishNotifications != value)
                {
                    this.m_SendPublishNotifications = value;
                    this.NotifyPropertyChanged("SendPublishNotifications");
                }
            }
        }

        [PersistentProperty()]
        public string PublishNotificationEmailAddress
        {
            get { return this.m_PublishNotificationEmailAddress; }
            set
            {
                if (this.m_PublishNotificationEmailAddress != value)
                {
                    this.m_PublishNotificationEmailAddress = value;
                    this.NotifyPropertyChanged("PublishNotificationEmailAddress");
                }
            }
        }

        public YellowstonePathology.Business.Client.Model.StandingOrderCollection GetStandingOrderCollection()
        {
            YellowstonePathology.Business.Client.Model.StandingOrderCollection result = new YellowstonePathology.Business.Client.Model.StandingOrderCollection();
            YellowstonePathology.Business.Client.Model.StandingOrder hpvStandingOrder = YellowstonePathology.Business.Client.Model.StandingOrderCollection.GetByStandingOrderCode(this.m_HPVStandingOrderCode);
            result.Add(hpvStandingOrder);
            YellowstonePathology.Business.Client.Model.StandingOrder hpv1618StandingOrder = YellowstonePathology.Business.Client.Model.StandingOrderCollection.GetByStandingOrderCode(this.m_HPV1618StandingOrderCode);
            result.Add(hpv1618StandingOrder);
            return result;
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
