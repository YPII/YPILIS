using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Client.Model
{
	[PersistentClass("tblClientSupplyOrderDetail", "YPIDATA")]
	public class ClientSupplyOrderDetail : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;				

		private string m_clientsupplyorderdetailid;
		private string m_clientsupplyorderid;
		private int m_clientsupplyid;
		private string m_supplyname;
		private string m_supplydescription;
		private string m_quantityordered;
		private int m_quantity;
		private string m_ObjectId;

		public ClientSupplyOrderDetail()
		{
		}

		public ClientSupplyOrderDetail(string objectId, string clientSupplyOrderId, int clientSupplyId, string supplyName, string supplyDescription, string quantityOrdered)
		{
			this.m_ObjectId = objectId;
			this.m_clientsupplyorderdetailid = objectId;
			this.m_clientsupplyorderid = clientSupplyOrderId;
			this.m_clientsupplyid = clientsupplyid;
			this.m_supplyname = supplyName;
			this.m_supplydescription = supplyDescription;
			this.m_quantityordered = quantityOrdered;
		}

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        [PersistentPrimaryKeyProperty(false)]
        [PersistentDataColumnProperty(false, "50", "null", "varchar")]
		public string clientsupplyorderdetailid
		{
			get { return this.m_clientsupplyorderdetailid; }
			set
			{
				if (this.m_clientsupplyorderdetailid != value)
				{
					this.m_clientsupplyorderdetailid = value;
					this.NotifyPropertyChanged("clientsupplyorderdetailid");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "0", "varchar")]
		public string clientsupplyorderid
		{
			get { return this.m_clientsupplyorderid; }
			set
			{
				if (this.m_clientsupplyorderid != value)
				{
					this.m_clientsupplyorderid = value;
					this.NotifyPropertyChanged("clientsupplyorderid");					
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "0", "int")]
		public int clientsupplyid
		{
			get { return this.m_clientsupplyid; }
			set
			{
				if (this.m_clientsupplyid != value)
				{
					this.m_clientsupplyid = value;
					this.NotifyPropertyChanged("clientsupplyid");					
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string supplyname
		{
			get { return this.m_supplyname; }
			set
			{
				if (this.m_supplyname != value)
				{
					this.m_supplyname = value;
					this.NotifyPropertyChanged("supplyname");					
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string supplydescription
		{
			get { return this.m_supplydescription; }
			set
			{
				if (this.m_supplydescription != value)
				{
					this.m_supplydescription = value;
					this.NotifyPropertyChanged("supplydescription");					
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string quantityordered
		{
			get { return this.m_quantityordered; }
			set
			{
				if (this.m_quantityordered != value)
				{
					this.m_quantityordered = value;
					this.NotifyPropertyChanged("quantityordered");					
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "null", "int")]
		public int quantity
		{
			get { return this.m_quantity; }
			set
			{
				if (this.m_quantity != value)
				{
					this.m_quantity = value;
					this.NotifyPropertyChanged("quantity");					
				}
			}
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
	}
}
