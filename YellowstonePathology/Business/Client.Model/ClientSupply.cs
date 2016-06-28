using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Client.Model
{
	[PersistentClass("tblClientSupply", "YPIDATA")]
	public class ClientSupply : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;				

		private int m_clientsupplyid;
		private string m_supplyname;
		private string m_description;
		private string m_supplycategory;
		private string m_ObjectId;

		public ClientSupply()
		{
		}

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        [PersistentPrimaryKeyProperty(true)]
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
		public string description
		{
			get { return this.m_description; }
			set
			{
				if (this.m_description != value)
				{
					this.m_description = value;
					this.NotifyPropertyChanged("description");
				}
			}
		}

        [PersistentProperty()]
		public string supplycategory
		{
			get { return this.m_supplycategory; }
			set
			{
				if (this.m_supplycategory != value)
				{
					this.m_supplycategory = value;
					this.NotifyPropertyChanged("supplycategory");
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
	}
}
