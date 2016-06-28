using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Client.Model
{
	[PersistentClass("tblClientSupplyOrder", "YPIDATA")]
	public class ClientSupplyOrder : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;				

		private string m_ClientSupplyOrderId;
		private int m_ClientId;
		private string m_ClientName;
		private string m_ClientAddress;
		private string m_ClientCity;
		private string m_ClientState;
		private string m_ClientZip;
		private DateTime? m_OrderDate;
		private DateTime? m_DateOrderSent;
		private bool m_OrderFinal;
		private int m_OrderTakenById;
		private int m_OrderFilledById;
		private string m_ContactName;
		private string m_Comment;
		private string m_ObjectId;
        private string m_OrderTakenBy;
        private string m_OrderFilledBy;

		private ClientSupplyOrderDetailCollection m_ClientSupplyOrderDetailCollection;

		public ClientSupplyOrder()
		{
			this.m_ClientSupplyOrderDetailCollection = new ClientSupplyOrderDetailCollection();
		}

		public ClientSupplyOrder(string objectId, YellowstonePathology.Business.Client.Model.Client client)
		{
			this.m_ObjectId = objectId;
			this.m_ClientSupplyOrderId = objectId;
			this.m_ClientAddress = client.Address;
			this.m_ClientCity = client.City;
			this.m_ClientId = client.ClientId;
			this.m_ClientName = client.ClientName;
			this.m_ClientState = client.State;
			this.m_ClientZip = client.ZipCode;
			this.m_ContactName = client.ContactName;
			this.m_OrderDate = DateTime.Today;

			this.m_ClientSupplyOrderDetailCollection = new ClientSupplyOrderDetailCollection();
		}

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

		[PersistentCollection()]
		public ClientSupplyOrderDetailCollection ClientSupplyOrderDetailCollection
		{
			get { return this.m_ClientSupplyOrderDetailCollection; }
			set { this.m_ClientSupplyOrderDetailCollection = value; }
		}

        [PersistentPrimaryKeyProperty(false)]
		public string ClientSupplyOrderId
		{
			get { return this.m_ClientSupplyOrderId; }
			set
			{
				if (this.m_ClientSupplyOrderId != value)
				{
					this.m_ClientSupplyOrderId = value;
					this.NotifyPropertyChanged("ClientSupplyOrderId");
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
		public string ClientAddress
		{
			get { return this.m_ClientAddress; }
			set
			{
				if (this.m_ClientAddress != value)
				{
					this.m_ClientAddress = value;
					this.NotifyPropertyChanged("ClientAddress");
				}
			}
		}

        [PersistentProperty()]
		public string ClientCity
		{
			get { return this.m_ClientCity; }
			set
			{
				if (this.m_ClientCity != value)
				{
					this.m_ClientCity = value;
					this.NotifyPropertyChanged("ClientCity");
				}
			}
		}

        [PersistentProperty()]
		public string ClientState
		{
			get { return this.m_ClientState; }
			set
			{
				if (this.m_ClientState != value)
				{
					this.m_ClientState = value;
					this.NotifyPropertyChanged("ClientState");
				}
			}
		}

        [PersistentProperty()]
		public string ClientZip
		{
			get { return this.m_ClientZip; }
			set
			{
				if (this.m_ClientZip != value)
				{
					this.m_ClientZip = value;
					this.NotifyPropertyChanged("ClientZip");
				}
			}
		}

        [PersistentProperty()]
		public DateTime? OrderDate
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

        [PersistentProperty()]
		public DateTime? DateOrderSent
		{
			get { return this.m_DateOrderSent; }
			set
			{
				if (this.m_DateOrderSent != value)
				{
					this.m_DateOrderSent = value;
					this.NotifyPropertyChanged("DateOrderSent");
				}
			}
		}

        [PersistentProperty()]
		public bool OrderFinal
		{
			get { return this.m_OrderFinal; }
			set
			{
				if (this.m_OrderFinal != value)
				{
					this.m_OrderFinal = value;
					this.NotifyPropertyChanged("OrderFinal");
				}
			}
		}

        [PersistentProperty()]
		public int OrderTakenById
		{
			get { return this.m_OrderTakenById; }
			set
			{
				if (this.m_OrderTakenById != value)
				{
					this.m_OrderTakenById = value;
					this.NotifyPropertyChanged("OrderTakenById");
				}
			}
		}

        [PersistentProperty()]
		public int OrderFilledById
		{
			get { return this.m_OrderFilledById; }
			set
			{
				if (this.m_OrderFilledById != value)
				{
					this.m_OrderFilledById = value;
					this.NotifyPropertyChanged("OrderFilledById");
				}
			}
		}

        [PersistentProperty()]
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
		public string Comment
		{
			get { return this.m_Comment; }
			set
			{
				if (this.m_Comment != value)
				{
					this.m_Comment = value;
					this.NotifyPropertyChanged("Comment");
				}
			}
		}

        [PersistentProperty()]
        public string OrderTakenBy
        {
            get { return this.m_OrderTakenBy; }
            set
            {
                if (this.m_OrderTakenBy != value)
                {
                    this.m_OrderTakenBy = value;
                    this.NotifyPropertyChanged("OrderTakenBy");
                }
            }
        }

        [PersistentProperty()]
        public string OrderFilledBy
        {
            get { return this.m_OrderFilledBy; }
            set
            {
                if (this.m_OrderFilledBy != value)
                {
                    this.m_OrderFilledBy = value;
                    this.NotifyPropertyChanged("OrderFilledBy");
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

        public string ToJsonString()
        {
            string result = MongoDB.Bson.BsonExtensionMethods.ToJson(this, typeof(YellowstonePathology.Business.Client.Model.ClientSupplyOrder));
            return result;
        }
	}
}
