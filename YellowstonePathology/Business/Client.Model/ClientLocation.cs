using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Xml.Linq;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Client.Model
{
	[Table(Name = "tblClientLocation")]
	[PersistentClass("tblClientLocation", "YPIDATA")]
	public partial class ClientLocation : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private int m_ClientLocationId;
		private int m_ClientId;
		private string m_Location;
		private string m_OrderType;
		private string m_SpecimenTrackingInitiated;
		private bool m_AllowMultipleOrderTypes;
		private int m_DefaultOrderPanelSetId;
		private bool m_AllowMultipleOrderDetailTypes;
		private string m_DefaultOrderDetailTypeCode;

		public ClientLocation()
		{
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
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

		[PersistentPrimaryKeyProperty(false)]
		[PersistentDataColumnProperty(false, "11", "null", "int")]
		public int ClientLocationId
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

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "11", "null", "int")]
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
		[PersistentDataColumnProperty(false, "500", "null", "varchar")]
		public string Location
		{
			get { return this.m_Location; }
			set
			{
				if (this.m_Location != value)
				{
					this.m_Location = value;
					this.NotifyPropertyChanged("Location");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public bool AllowMultipleOrderTypes
		{
			get { return this.m_AllowMultipleOrderTypes; }
			set
			{
				if (this.m_AllowMultipleOrderTypes != value)
				{
					this.m_AllowMultipleOrderTypes = value;
					this.NotifyPropertyChanged("AllowMultipleOrderTypes");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "11", "null", "int")]
		public int DefaultOrderPanelSetId
		{
			get { return this.m_DefaultOrderPanelSetId; }
			set
			{
				if (this.m_DefaultOrderPanelSetId != value)
				{
					this.m_DefaultOrderPanelSetId = value;
					this.NotifyPropertyChanged("DefaultOrderPanelSetId");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "null", "bit")]
		public bool AllowMultipleOrderDetailTypes
		{
			get { return this.m_AllowMultipleOrderDetailTypes; }
			set
			{
				if (this.m_AllowMultipleOrderDetailTypes != value)
				{
					this.m_AllowMultipleOrderDetailTypes = value;
					this.NotifyPropertyChanged("AllowMultipleOrderDetailTypes");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string DefaultOrderDetailTypeCode
		{
			get { return this.m_DefaultOrderDetailTypeCode; }
			set
			{
				if (this.m_DefaultOrderDetailTypeCode != value)
				{
					this.m_DefaultOrderDetailTypeCode = value;
					this.NotifyPropertyChanged("DefaultOrderDetailTypeCode");
				}
			}
		}
	}
}
