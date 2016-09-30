using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.ClientOrder.Model
{        
    [DataContract(Namespace="YellowstonePathology.Business.ClientOrder.Model")]
	[PersistentClass("tblClientOrderDetail", "YPIDATA")]
    public partial class ClientOrderDetail : INotifyPropertyChanged, System.ComponentModel.IDataErrorInfo
    {        
		protected string m_ObjectId;
        protected string m_ClientOrderDetailId;
        protected string m_ClientOrderId;
        protected string m_SpecimenId;
        protected string m_ContainerId;
        protected int m_SpecimenNumber;
        protected bool m_Submitted;
        protected bool m_Accessioned;
        protected bool m_Received;
        protected Nullable<DateTime> m_DateReceived;
        protected bool m_Validated;
        protected string m_Description;
        protected string m_DescriptionToAccession;
        protected Nullable<DateTime> m_OrderDate;
        protected Nullable<DateTime> m_OrderTime;
        protected string m_OrderedBy;
        protected string m_OrderType;
        protected string m_SpecialInstructions;
        protected bool m_Inactive;
        protected Nullable<DateTime> m_CollectionDate;        
        protected string m_CallbackNumber;
        protected string m_ClientFixation;
        protected string m_LabFixation;
        protected bool m_Shipped;
        protected Nullable<DateTime> m_ShipDate;
        protected string m_ShipmentId;
        protected string m_SpecimenNumberMatchStatus;
        protected string m_SpecimenDescriptionMatchStatus;
        protected string m_SpecimenTrackingInitiated;
        protected string m_SystemInitiatingOrder;
        protected string m_SpecimenSource;
        protected string m_OrderTypeCode;
        protected bool m_RequiresGrossExamination;
        
        protected Nullable<DateTime> m_FixationStartTime;        
        protected bool m_FixationStartTimeManuallyEntered;
        protected string m_FixationComment;
        protected bool m_ClientAccessioned;

        public event PropertyChangedEventHandler PropertyChanged;

		private ClientOrderDetailAliquotCollection m_ClientOrderDetailAliquotCollection;

		public ClientOrderDetail()
        {
            this.m_ValidationErrors = new Dictionary<string, string>();
			this.m_ClientOrderDetailAliquotCollection = new ClientOrderDetailAliquotCollection();
        }

		public ClientOrderDetail(string containerId, YellowstonePathology.Business.Persistence.PersistenceModeEnum persistenceMode)
		{
            this.m_ValidationErrors = new Dictionary<string, string>();
			this.ClientOrderId = string.Empty;
			this.ClientOrderDetailId = string.Empty;
			this.m_ContainerId = containerId;

			this.m_ClientOrderDetailAliquotCollection = new ClientOrderDetailAliquotCollection();
		}
        
		public ClientOrderDetail(YellowstonePathology.Business.Persistence.PersistenceModeEnum persistenceMode) 
        {
            this.m_ValidationErrors = new Dictionary<string, string>();
			this.m_ClientOrderDetailAliquotCollection = new ClientOrderDetailAliquotCollection();
		}

		public ClientOrderDetail(YellowstonePathology.Business.Persistence.PersistenceModeEnum persistenceMode, string objectId)
		{
			this.m_ObjectId = objectId;
            this.m_ValidationErrors = new Dictionary<string, string>();
			this.m_ClientOrderDetailAliquotCollection = new ClientOrderDetailAliquotCollection();
		}

        public string DescriptionDisplayString
        {
            get 
            {
                string result = this.m_Description;                
                result = this.m_SpecimenNumber + ".) " + this.m_Description;                
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

        public void Receive()
        {
            if (this.m_Received == false)
            {
                this.m_Received = true;
                this.m_DateReceived = YellowstonePathology.Business.Helper.DateTimeExtensions.DateTimeNowNoSeconds();
                this.NotifyPropertyChanged(string.Empty);
            }
        }

        public string SpecimenNumberAndDescription
        {
            get
            {
                return this.m_SpecimenNumber + ". " + this.Description;
            }
        }

		public void Join(ClientOrderDetail objectToReadFrom)
		{
            YellowstonePathology.Business.Persistence.ObjectPropertyWriter objectPropertyWriter = new Persistence.ObjectPropertyWriter(this, objectToReadFrom);
            objectPropertyWriter.WriteProperties();            
		}

		public XElement AsXML()
		{
			XElement clientOrderDetailElement = new XElement("ClientOrderDetail");
			YellowstonePathology.Business.Persistence.XmlPropertyReader clientOrderDetailPropertyWriter = new Persistence.XmlPropertyReader(this, clientOrderDetailElement);
			clientOrderDetailPropertyWriter.Write();

			foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailAliquot clientOrderDetailAliquot in this.ClientOrderDetailAliquotCollection)
			{
				XElement clientOrderDetailAliquotElement = clientOrderDetailAliquot.AsXML();
				clientOrderDetailElement.Add(clientOrderDetailAliquotElement);
			}
			return clientOrderDetailElement;
		}        

		[DataMember]
		[YellowstonePathology.Business.Persistence.PersistentCollection()]
		public ClientOrderDetailAliquotCollection ClientOrderDetailAliquotCollection
		{
			get { return this.m_ClientOrderDetailAliquotCollection; }
			set { this.m_ClientOrderDetailAliquotCollection = value; }
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
		
		[DataMember]
		[PersistentPrimaryKeyProperty(false)]
		[PersistentDataColumnProperty(false, "50", "null", "varchar")]
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

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string SpecimenId
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "11", "0", "int")]
		public int SpecimenNumber
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
		[PersistentDataColumnProperty(true, "1", "0", "bit")]
		public bool Submitted
		{
			get { return this.m_Submitted; }
			set
			{
				if (this.m_Submitted != value)
				{
					this.m_Submitted = value;
					this.NotifyPropertyChanged("Submitted");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
		public Nullable<DateTime> DateReceived
		{
			get { return this.m_DateReceived; }
			set
			{
				if (this.m_DateReceived != value)
				{
					this.m_DateReceived = value;
					this.NotifyPropertyChanged("DateReceived");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "0", "bit")]
		public bool Validated
		{
			get { return this.m_Validated; }
			set
			{
				if (this.m_Validated != value)
				{
					this.m_Validated = value;
					this.NotifyPropertyChanged("Validated");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "3", "null", "datetime")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string OrderedBy
		{
			get { return this.m_OrderedBy; }
			set
			{
				if (this.m_OrderedBy != value)
				{
					this.m_OrderedBy = value;
					this.NotifyPropertyChanged("OrderedBy");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string OrderType
		{
			get { return this.m_OrderType; }
			set
			{
				if (this.m_OrderType != value)
				{
					this.m_OrderType = value;
					this.NotifyPropertyChanged("OrderType");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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

		[DataMember]
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

		[DataMember]
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
                    NotifyPropertyChanged("CollectionDate");
				}
			}
		}        

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string CallbackNumber
		{
			get { return this.m_CallbackNumber; }
			set
			{
				if (this.m_CallbackNumber != value)
				{
					this.m_CallbackNumber = value;
					this.NotifyPropertyChanged("CallbackNumber");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
		public bool Shipped
		{
			get { return this.m_Shipped; }
			set
			{
				if (this.m_Shipped != value)
				{
					this.m_Shipped = value;
					this.NotifyPropertyChanged("Shipped");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
		public Nullable<DateTime> ShipDate
		{
			get { return this.m_ShipDate; }
			set
			{
				if (this.m_ShipDate != value)
				{
					this.m_ShipDate = value;
					this.NotifyPropertyChanged("ShipDate");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ShipmentId
		{
			get { return this.m_ShipmentId; }
			set
			{
				if (this.m_ShipmentId != value)
				{
					this.m_ShipmentId = value;
					this.NotifyPropertyChanged("ShipmentId");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "'Not Determined'", "varchar")]
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
		[PersistentDataColumnProperty(true, "50", "'Not Determined'", "varchar")]
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
		[PersistentDataColumnProperty(true, "50", "'Bedside'", "varchar")]
		public string SpecimenTrackingInitiated
		{
			get { return this.m_SpecimenTrackingInitiated; }
			set
			{
				if (this.m_SpecimenTrackingInitiated != value)
				{
					this.m_SpecimenTrackingInitiated = value;
					this.NotifyPropertyChanged("SpecimenTrackingInitiated");
				}
			}
		}

		[DataMember]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string SpecimenSource
		{
			get { return this.m_SpecimenSource; }
			set
			{
				if (this.m_SpecimenSource != value)
				{
					this.m_SpecimenSource = value;
					this.NotifyPropertyChanged("SpecimenSource");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string OrderTypeCode
		{
			get { return this.m_OrderTypeCode; }
			set
			{
				if (this.m_OrderTypeCode != value)
				{
					this.m_OrderTypeCode = value;
					this.NotifyPropertyChanged("OrderTypeCode");
				}
			}
		}

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "1", "bit")]
        public bool RequiresGrossExamination
        {
            get { return this.m_RequiresGrossExamination; }
            set
            {
                if (this.m_RequiresGrossExamination != value)
                {
                    this.m_RequiresGrossExamination = value;
                    this.NotifyPropertyChanged("RequiresGrossExamination");
                }
            }
        }                   

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string FixationComment
        {
            get { return this.m_FixationComment; }
            set
            {
                if (this.m_FixationComment != value)
                {
                    this.m_FixationComment = value;
                    this.NotifyPropertyChanged("FixationComment");
                }
            }
        }

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
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
        [PersistentDataColumnProperty(true, "1", "0", "bit")]
        public bool FixationStartTimeManuallyEntered
        {
            get { return this.m_FixationStartTimeManuallyEntered; }
            set
            {
                if (this.m_FixationStartTimeManuallyEntered != value)
                {
                    this.m_FixationStartTimeManuallyEntered = value;
                    this.NotifyPropertyChanged("FixationStartTimeManuallyEntered");
                }
            }
        }

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "bit")]
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

        public Nullable<int> TimeToFixation
        {
            get
            {
                Nullable<int> result = null;
                if (this.ClientAccessioned == false)
                {
                    if (this.m_CollectionDate.HasValue == true && this.m_FixationStartTime.HasValue == true)
                    {
                        if (YellowstonePathology.Business.Helper.DateTimeExtensions.DoesDateHaveTime(this.m_CollectionDate) == true)
                        {
                            TimeSpan timeSpan = new TimeSpan(this.m_FixationStartTime.Value.Ticks - this.m_CollectionDate.Value.Ticks);
                            result = Convert.ToInt32(timeSpan.TotalMinutes);
                        }
                    }                                                                
                }
                return result;
            }
        }

        public string TimeToFixationString
        {
            get
            {
                string result = null;                
                if (this.TimeToFixation.HasValue == true)
                {
                    TimeSpan timeSpan = new TimeSpan(0, this.TimeToFixation.Value, 0);
                    if (timeSpan.TotalMinutes == 0)
                    {
                        result = "Immediate";
                    }
                    else if (timeSpan.TotalMinutes < 60)
                    {
                        result = timeSpan.TotalMinutes.ToString() + "min";
                    }
                    else
                    {
                        result = timeSpan.Hours + "hrs ";
                        if (timeSpan.Minutes > 0)
                        {
                            result += timeSpan.Minutes.ToString() + "min";
                        }
                    }
                }
                else
                {
                    result = "Unknown";
                }                
                return result;
            }  
        }

        public string TimeToFixationHourString
        {
            get
            {
                string result = null;
                if (this.TimeToFixation.HasValue == true)
                {
                    TimeSpan timeSpan = new TimeSpan(0, this.TimeToFixation.Value, 0);
                    if (timeSpan.TotalMinutes < 60)
                    {
                        result = "< 1 hr";
                    }
                    else
                    {
                        result = "> 1 hr";
                    }
                }
                else
                {
                    result = "< 1 hr"; // "Unknown";
                }
                return result;
            }
        }

        public void SetFixationStartTime()
        {
            if (this.m_ClientAccessioned == false)
            {
                if (this.m_FixationStartTimeManuallyEntered == false)
                {
                    if (this.m_ClientFixation == YellowstonePathology.Business.Specimen.Model.FixationType.Fresh)
                    {
                        this.m_FixationStartTime = this.m_DateReceived;
                    }
                    else if (this.m_ClientFixation == YellowstonePathology.Business.Specimen.Model.FixationType.NotApplicable)
                    {
                        this.m_FixationStartTime = null;
                    }
                    else
                    {
                        if (YellowstonePathology.Business.Helper.DateTimeExtensions.DoesDateHaveTime(this.m_CollectionDate) == true)
                        {
                            this.m_FixationStartTime = this.m_CollectionDate;
                        }
                        else
                        {
                            this.m_FixationStartTime = null;
                        }
                    }
                }
            }
            else
            {
                this.m_FixationStartTime = null;
            }

            this.NotifyPropertyChanged("FixationStartTime");
            this.NotifyPropertyChanged("FixationStartTimeBinding");
        }        
    }
}
