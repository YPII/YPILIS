using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Web;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	[PersistentClass("tblClientOrderMedia", "YPIDATA")]
	public class ClientOrderMedia : INotifyPropertyChanged
	{
        protected delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private string m_ClientOrderMediaId;
		private string m_SpecimenNumber;
		private bool m_HasPatientId;
		private bool m_HasSpecimenId;
		private bool m_HasBarcode;
		private string m_Description;
		private string m_DescriptionToAccession;
		private string m_FirstName;
		private string m_LastName;
		private Nullable<DateTime> m_Birthdate;
		private string m_ContainerId;
        private string m_ClientOrderDetailId;
		private string m_ClientFixation;
		private string m_LabFixation;
		private Nullable<DateTime> m_FixationStartTime;
		private Nullable<DateTime> m_FixationEndTime;
		private Nullable<DateTime> m_CollectionDate;
		private string m_SpecimenNumberMatchStatus;
		private string m_SpecimenDescriptionMatchStatus;
        private bool m_Received;

		private ClientOrderMediaEnum m_ClientOrderMediaEnum;

		public ClientOrderMedia()
		{
			this.SetDefaults();
		}

		public ClientOrderMedia(ClientOrderMediaEnum clientOrderMediaEnum)
		{
			this.m_ClientOrderMediaEnum = clientOrderMediaEnum;			
			this.SetDefaults();
		}

		public ClientOrderMedia(string containerId)
		{
			this.m_ClientOrderMediaEnum = ClientOrderMediaEnum.Specimen;
			this.SetDefaults();
			this.ContainerId = containerId;
		}

        public ClientOrderMedia(ClientOrderDetail clientOrderDetail)
        {
            this.m_ClientOrderMediaEnum = ClientOrderMediaEnum.Specimen;
            this.SetDefaults();
            this.ContainerId = clientOrderDetail.ContainerId;
        }

		public ClientOrderMediaEnum ClientOrderMediaEnum
		{
			get { return this.m_ClientOrderMediaEnum; }
		}        

        public string SpecimenNumberString
        {
            get 
            {
                string result = string.Empty;
                if (this.m_SpecimenNumber == "0")
                {
                    result = "Not Numbered";
                }
                else
                {
                    result = this.m_SpecimenNumber.ToString();
                }
                return result;
            }
        }


		public string PatientId
		{
			get
			{
				System.Text.StringBuilder result = new System.Text.StringBuilder();
				result.Append(YellowstonePathology.Business.Helper.PatientHelper.GetPatientDisplayName(this.m_LastName, this.m_FirstName, string.Empty));
				if (this.m_Birthdate.HasValue)
				{
					result.Append(" " + this.m_Birthdate.Value.ToShortDateString());
				}

				return result.ToString();
			}
		}

        public string PatientName
        {
            get
            {
				return YellowstonePathology.Business.Helper.PatientHelper.GetPatientDisplayName(this.m_LastName, this.m_FirstName, string.Empty);
            }
        }       

		private void SetDefaults()
		{
			this.m_ClientOrderMediaId = Guid.NewGuid().ToString();
			this.m_ObjectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			this.m_LastName = "Last";
			this.m_FirstName = "First";
			this.m_Birthdate = DateTime.Parse("1/1/1900");
			this.m_ContainerId = "Barcode";
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

		[DataMember]
		[PersistentPrimaryKeyProperty(false)]
		public string ClientOrderMediaId
		{
			get { return this.m_ClientOrderMediaId; }
			set
			{
				if (this.m_ClientOrderMediaId != value)
				{
					this.m_ClientOrderMediaId = value;
					this.NotifyPropertyChanged("ClientOrderMediaId");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string SpecimenNumber
		{
			get { return this.m_SpecimenNumber; }
			set
			{
				if (this.m_SpecimenNumber != value)
				{
					this.m_SpecimenNumber = value;
					this.NotifyPropertyChanged("SpecimenNumber");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public bool HasPatientId
		{
			get { return this.m_HasPatientId; }
			set
			{
				if (this.m_HasPatientId != value)
				{
					this.m_HasPatientId = value;
					this.NotifyPropertyChanged("HasPatientId");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public bool HasSpecimenId
		{
			get { return this.m_HasSpecimenId; }
			set
			{
				if (this.m_HasSpecimenId != value)
				{
					this.m_HasSpecimenId = value;
					this.NotifyPropertyChanged("HasSpecimenId");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public bool HasBarcode
		{
			get { return this.m_HasBarcode; }
			set
			{
				if (this.m_HasBarcode != value)
				{
					this.m_HasBarcode = value;
					this.NotifyPropertyChanged("HasBarcode");
				}
			}
		}

		[DataMember]
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

		[DataMember]
		[PersistentProperty()]
		public string DescriptionToAccession
		{
			get { return this.m_DescriptionToAccession; }
			set
			{
				if (this.m_DescriptionToAccession != value)
				{
					this.m_DescriptionToAccession = value;
					this.NotifyPropertyChanged("DescriptionToAccession");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string FirstName
		{
			get { return this.m_FirstName; }
			set
			{
				if (this.m_FirstName != value)
				{
					this.m_FirstName = value;
					this.NotifyPropertyChanged("FirstName");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string LastName
		{
			get { return this.m_LastName; }
			set
			{
				if (this.m_LastName != value)
				{
					this.m_LastName = value;
					this.NotifyPropertyChanged("LastName");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public Nullable<DateTime> Birthdate
		{
			get { return this.m_Birthdate; }
			set
			{
				if (this.m_Birthdate != value)
				{
					this.m_Birthdate = value;
					this.NotifyPropertyChanged("Birthdate");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string ContainerId
		{
			get { return this.m_ContainerId; }
			set
			{
				if (this.m_ContainerId != value)
				{
					this.m_ContainerId = value;
					this.NotifyPropertyChanged("ContainerId");
				}
			}
		}

        [DataMember]
        [PersistentProperty()]
        public string ClientOrderDetailId
        {
            get { return this.m_ClientOrderDetailId; }
            set
            {
                if (this.m_ClientOrderDetailId != value)
                {
                    this.m_ClientOrderDetailId = value;
                    this.NotifyPropertyChanged("ClientOrderDetailId");
                }
            }
        }

		[DataMember]
		[PersistentProperty()]
		public string ClientFixation
		{
			get { return this.m_ClientFixation; }
			set
			{
				if (this.m_ClientFixation != value)
				{
					this.m_ClientFixation = value;
					this.NotifyPropertyChanged("ClientFixation");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string LabFixation
		{
			get { return this.m_LabFixation; }
			set
			{
				if (this.m_LabFixation != value)
				{
					this.m_LabFixation = value;
					this.NotifyPropertyChanged("LabFixation");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public Nullable<DateTime> FixationStartTime
		{
			get { return this.m_FixationStartTime; }
			set
			{
				if (this.m_FixationStartTime != value)
				{
					this.m_FixationStartTime = value;
					this.NotifyPropertyChanged("FixationStartTime");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public Nullable<DateTime> FixationEndTime
		{
			get { return this.m_FixationEndTime; }
			set
			{
				if (this.m_FixationEndTime != value)
				{
					this.m_FixationEndTime = value;
					this.NotifyPropertyChanged("FixationEndTime");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
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

		[DataMember]
		[PersistentProperty()]
		public string SpecimenNumberMatchStatus
		{
			get { return this.m_SpecimenNumberMatchStatus; }
			set
			{
				if (this.m_SpecimenNumberMatchStatus != value)
				{
					this.m_SpecimenNumberMatchStatus = value;
					this.NotifyPropertyChanged("SpecimenNumberMatchStatus");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string SpecimenDescriptionMatchStatus
		{
			get { return this.m_SpecimenDescriptionMatchStatus; }
			set
			{
				if (this.m_SpecimenDescriptionMatchStatus != value)
				{
					this.m_SpecimenDescriptionMatchStatus = value;
					this.NotifyPropertyChanged("SpecimenDescriptionMatchStatus");
				}
			}
		}

        [DataMember]
        [PersistentProperty()]
        public bool Received
        {
            get { return this.m_Received; }
            set
            {
                if (this.m_Received != value)
                {
                    this.m_Received = value;
                    this.NotifyPropertyChanged("Received");
                }
            }
        }	
	}
}
