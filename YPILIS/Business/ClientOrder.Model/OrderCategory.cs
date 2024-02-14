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
	[PersistentClass("tblOrderCategory", "YPIDATA")]
	public class OrderCategory : INotifyPropertyChanged
	{
		protected delegate void PropertyChangedNotificationHandler(String info);
		public event PropertyChangedEventHandler PropertyChanged;

		private YellowstonePathology.Business.ClientOrder.Model.OrderTypeCollection m_OrderTypeCollection;

		private string m_ObjectId;
		private string m_OrderCategoryId;
		private string m_OrderCategoryName;
		private int m_Priority;

		public OrderCategory()
		{
			this.m_OrderTypeCollection = new OrderTypeCollection();
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		[DataMember]
		public YellowstonePathology.Business.ClientOrder.Model.OrderTypeCollection OrderTypeCollection
		{
			get { return this.m_OrderTypeCollection; }
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
		public string OrderCategoryId
		{
			get { return this.m_OrderCategoryId; }
			set
			{
				if (this.m_OrderCategoryId != value)
				{
					this.m_OrderCategoryId = value;
					this.NotifyPropertyChanged("OrderCategoryId");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string OrderCategoryName
		{
			get { return this.m_OrderCategoryName; }
			set
			{
				if (this.m_OrderCategoryName != value)
				{
					this.m_OrderCategoryName = value;
					this.NotifyPropertyChanged("OrderCategoryName");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "11", "0", "int")]
		public int Priority
		{
			get { return this.m_Priority; }
			set
			{
				if (this.m_Priority != value)
				{
					this.m_Priority = value;
					this.NotifyPropertyChanged("Priority");
				}
			}
		}
	}
}
