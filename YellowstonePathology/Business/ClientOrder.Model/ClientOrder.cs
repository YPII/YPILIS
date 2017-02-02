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
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.ClientOrder.Model
{
    
    [DataContract(Namespace="YellowstonePathology.Business.ClientOrder.Model")]
	[PersistentClass("tblClientOrder", "YPIDATA")]
	public partial class ClientOrder : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private YellowstonePathology.Business.Client.Model.ClientLocation m_ClientLocation;
        private ClientOrderDetailCollection m_ClientOrderDetailCollection;

		private string m_ObjectId;
		private string m_ClientOrderId;
		private bool m_Received;
		private bool m_Submitted;
		private Nullable<DateTime> m_OrderDate;
		private Nullable<DateTime> m_OrderTime;
		private string m_OrderedBy;
		private string m_PFirstName;
		private string m_PLastName;
		private string m_PMiddleInitial;
		private Nullable<DateTime> m_PBirthdate;
		private string m_PSex;
		private string m_PSSN;
		private string m_PAddress1;
		private string m_PAddress2;
		private string m_PCity;
		private string m_PState;
		private string m_PZipCode;
		private string m_SvhMedicalRecord;
		private string m_SvhAccountNo;
		private int m_ClientId;
		private string m_ClientName;
		private Nullable<int> m_ClientLocationId;
		private string m_ProviderId;
		private string m_ProviderName;
		private string m_ClinicalHistory;
		private string m_ReportCopyTo;
		private string m_OrderType;
		private Nullable<int> m_PanelSetId;
		private bool m_Accessioned;
		private bool m_Validated;
		private Nullable<DateTime> m_CollectionDate;
		private string m_ExternalOrderId;
		private string m_IncomingHL7;
		private string m_MasterAccessionNo;
		private string m_OrderedByFirstName;
		private string m_OrderedByLastName;
		private string m_OrderedById;
		private string m_ProviderFirstName;
		private string m_ProviderLastName;
		private bool m_Hold;
		private string m_HoldMessage;
		private string m_HoldBy;
		private bool m_Acknowledged;
		private int m_AcknowledgedById;
		private Nullable<DateTime> m_AcknowledgedDate;
		private string m_SystemInitiatingOrder;
		private string m_LocationCode;
		private string m_SpecialInstructions;
        private string m_UniversalServiceId;
        private string m_OrderStatus;
        private string m_PatientType;

        public ClientOrder()
        {
            this.m_ClientOrderDetailCollection = new ClientOrderDetailCollection();
        }

		public ClientOrder(string objectId)
		{
			this.m_ObjectId = objectId;
			this.m_ClientOrderDetailCollection = new ClientOrderDetailCollection();
		}

        [DataMember]
        [YellowstonePathology.Business.Persistence.PersistentCollection()]
        public ClientOrderDetailCollection ClientOrderDetailCollection
        {
            get { return this.m_ClientOrderDetailCollection; }
            set { this.m_ClientOrderDetailCollection = value; }
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
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
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
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
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
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
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
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
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

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
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

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
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

        [DataMember]
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

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
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

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
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

        [DataMember]
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

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string SvhAccountNo
        {
            get { return this.m_SvhAccountNo; }
            set
            {
                if (this.m_SvhAccountNo != value)
                {
                    this.m_SvhAccountNo = value;
                    this.NotifyPropertyChanged("SvhAccountNo");
                }
            }
        }

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "0", "int")]
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

        [DataMember]
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

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "null", "int")]
        public Nullable<int> ClientLocationId
        {
            get { return this.m_ClientLocationId; }
            set
            {
                if (this.m_ClientLocationId != value)
                {
                    this.m_ClientLocationId = value;
                    this.NotifyPropertyChanged("ClientLocationId");
                }
            }
        }

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
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

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "200", "null", "varchar")]
        public string ProviderName
        {
            get { return this.m_ProviderName; }
            set
            {
                if (this.m_ProviderName != value)
                {
                    this.m_ProviderName = value;
                    this.NotifyPropertyChanged("ProviderName");
                }
            }
        }

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
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

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
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

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "'Routine Surgical Pathology'", "varchar")]
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
        [PersistentDataColumnProperty(true, "11", "null", "int")]
        public Nullable<int> PanelSetId
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

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
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
        [PersistentDataColumnProperty(false, "1", "0", "tinyint")]
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

        [DataMember]
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

        [DataMember]
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

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
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

        [DataMember]
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

        [DataMember]
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

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
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

        [DataMember]
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

        [DataMember]
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

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "1", "0", "tinyint")]
        public bool Hold
        {
            get { return this.m_Hold; }
            set
            {
                if (this.m_Hold != value)
                {
                    this.m_Hold = value;
                    this.NotifyPropertyChanged("Hold");
                }
            }
        }

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "250", "null", "varchar")]
        public string HoldMessage
        {
            get { return this.m_HoldMessage; }
            set
            {
                if (this.m_HoldMessage != value)
                {
                    this.m_HoldMessage = value;
                    this.NotifyPropertyChanged("HoldMessage");
                }
            }
        }

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "0", "varchar")]
        public string HoldBy
        {
            get { return this.m_HoldBy; }
            set
            {
                if (this.m_HoldBy != value)
                {
                    this.m_HoldBy = value;
                    this.NotifyPropertyChanged("HoldBy");
                }
            }
        }

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "1", "0", "tinyint")]
        public bool Acknowledged
        {
            get { return this.m_Acknowledged; }
            set
            {
                if (this.m_Acknowledged != value)
                {
                    this.m_Acknowledged = value;
                    this.NotifyPropertyChanged("Acknowledged");
                }
            }
        }

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "11", "0", "int")]
        public int AcknowledgedById
        {
            get { return this.m_AcknowledgedById; }
            set
            {
                if (this.m_AcknowledgedById != value)
                {
                    this.m_AcknowledgedById = value;
                    this.NotifyPropertyChanged("AcknowledgedById");
                }
            }
        }

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> AcknowledgedDate
        {
            get { return this.m_AcknowledgedDate; }
            set
            {
                if (this.m_AcknowledgedDate != value)
                {
                    this.m_AcknowledgedDate = value;
                    this.NotifyPropertyChanged("AcknowledgedDate");
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
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string LocationCode
        {
            get { return this.m_LocationCode; }
            set
            {
                if (this.m_LocationCode != value)
                {
                    this.m_LocationCode = value;
                    this.NotifyPropertyChanged("LocationCode");
                }
            }
        }

        [DataMember]
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

        [DataMember]
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

        [DataMember]
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

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
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

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
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

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
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

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
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

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string OrderStatus
        {
            get { return this.m_OrderStatus; }
            set
            {
                if (this.m_OrderStatus != value)
                {
                    this.m_OrderStatus = value;
                    this.NotifyPropertyChanged("OrderStatus");
                }
            }
        }

        [DataMember]
        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
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

        public void SetDefaults(int clientId, string clientName)
        {
            this.m_ClientOrderId = Guid.NewGuid().ToString();
            this.m_OrderDate = DateTime.Today;
            this.m_OrderTime = DateTime.Now;
            this.m_CollectionDate = DateTime.Today;
            this.m_ClientId = clientId;
            this.m_ClientName = clientName;
            this.m_ProviderId = "0";            
        }

        public string PatientDisplayName
        {
			get { return YellowstonePathology.Business.Helper.PatientHelper.GetPatientDisplayName(PLastName, PFirstName, PMiddleInitial); }
        }		

        [DataMember]
        public YellowstonePathology.Business.Client.Model.ClientLocation ClientLocation
        {
            get { return this.m_ClientLocation; }
            set { this.m_ClientLocation = value; }
        }

		public void Accession(string masterAccessionNo)
		{
			this.MasterAccessionNo = masterAccessionNo;
			this.Accessioned = true;
		}        	        

		public void Receive()
		{
			this.Received = true;
		}        	

		public void Join(ClientOrder clientOrderToReadFrom)
		{            
            YellowstonePathology.Business.Persistence.ObjectPropertyWriter objectPropertyWriter = new Persistence.ObjectPropertyWriter(this, clientOrderToReadFrom);
            objectPropertyWriter.WriteProperties();                        

			this.ClientLocation = clientOrderToReadFrom.ClientLocation;

            foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail in clientOrderToReadFrom.ClientOrderDetailCollection)
            {
                this.ClientOrderDetailCollection.AddIfNotExist(clientOrderDetail);
            }
		}

        public void ThisOrderIsAnLISOrder()
        {                        
            this.SystemInitiatingOrder = "YPIILIS";            
            this.OrderDate = DateTime.Today;
            this.OrderTime = DateTime.Now;            
        }        

        public string GetProcessingContainerIds()
        {
            StringBuilder result = new StringBuilder();            
            string shortIdString = this.ClientOrderDetailCollection.GetContainerIdString();
            if (!string.IsNullOrEmpty(shortIdString))
            {
                result.Append("," + shortIdString);
            }            
            if (result.Length > 0)
            {
                result.Remove(0, 1);
            }
            return result.ToString();
        }

        public ClientOrderDetail AddToOrderAcceptingDetails(string containerId)
        {            
            YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail  result = this.MakeNewDetail(this.ClientOrderId, containerId);
            this.ClientOrderDetailCollection.Add(result);            
            return result;
        }        

        private ClientOrderDetail MakeNewDetail(string clientOrderId, string containerId)
        {
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            ClientOrderDetail result = new ClientOrderDetail(YellowstonePathology.Business.Persistence.PersistenceModeEnum.AddNewObject, objectId);            
            result.Description = "None";
            result.SpecimenTrackingInitiated = "Ypii Lab";
            return result;
        }

		public virtual XElement ToXML(bool includeChildren)
		{
			XElement clientOrderElement = new XElement("ClientOrder");
			YellowstonePathology.Business.Persistence.XmlPropertyReader clientOrderPropertyWriter = new Persistence.XmlPropertyReader(this, clientOrderElement);
			clientOrderPropertyWriter.Write();

            foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail in this.ClientOrderDetailCollection)
            {
                XElement clientOrderDetailElement = clientOrderDetail.AsXML();
                clientOrderElement.Add(clientOrderDetailElement);
            }

			return clientOrderElement;
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














