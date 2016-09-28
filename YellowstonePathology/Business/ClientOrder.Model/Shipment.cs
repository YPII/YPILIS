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
    [DataContract(Namespace = "YellowstonePathology.Business.ClientOrder.Model")]
	[PersistentClass("tblShipment", "YPIDATA")]
	public partial class Shipment : INotifyPropertyChanged
	{
		protected delegate void PropertyChangedNotificationHandler(String info);
		public event PropertyChangedEventHandler PropertyChanged;		

		public Shipment()
		{
            
		}

		public Shipment(string objectId, int clientId, string displayName, string clientName)
		{            
			this.m_ShipmentId = Guid.NewGuid().ToString();
			this.m_ObjectId = objectId;
			this.m_ShipmentFrom = clientName;
			this.m_ShipmentPreparedBy = displayName;
			this.m_ShipmentTo = "Yellowstone Pathology";
			this.m_ShipDate = DateTime.Now;
			this.m_Received = false;
			this.m_Shipped = false;
			this.m_ClientId = clientId;
		}				        		               

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

		private string m_ObjectId;
		private string m_ShipmentId;
		private string m_ShipmentFrom;
		private string m_ShipmentTo;
		private Nullable<DateTime> m_ShipDate;
		private bool m_Shipped;
		private bool m_Received;
		private Nullable<DateTime> m_ReceivedDate;
		private int m_ReceivedById;
		private string m_ShipmentPreparedBy;
		private int m_ClientId;

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
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string ShipmentFrom
		{
			get { return this.m_ShipmentFrom; }
			set
			{
				if (this.m_ShipmentFrom != value)
				{
					this.m_ShipmentFrom = value;
					this.NotifyPropertyChanged("ShipmentFrom");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string ShipmentTo
		{
			get { return this.m_ShipmentTo; }
			set
			{
				if (this.m_ShipmentTo != value)
				{
					this.m_ShipmentTo = value;
					this.NotifyPropertyChanged("ShipmentTo");
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
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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
		public Nullable<DateTime> ReceivedDate
		{
			get { return this.m_ReceivedDate; }
			set
			{
				if (this.m_ReceivedDate != value)
				{
					this.m_ReceivedDate = value;
					this.NotifyPropertyChanged("ReceivedDate");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "11", "0", "int")]
		public int ReceivedById
		{
			get { return this.m_ReceivedById; }
			set
			{
				if (this.m_ReceivedById != value)
				{
					this.m_ReceivedById = value;
					this.NotifyPropertyChanged("ReceivedById");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string ShipmentPreparedBy
		{
			get { return this.m_ShipmentPreparedBy; }
			set
			{
				if (this.m_ShipmentPreparedBy != value)
				{
					this.m_ShipmentPreparedBy = value;
					this.NotifyPropertyChanged("ShipmentPreparedBy");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "11", "0", "int")]
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
	}
}
