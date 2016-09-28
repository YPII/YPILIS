using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Domain
{
	public partial class MaterialLocation : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private int m_MaterialLocationId;
		private string m_Name;
		private string m_Address;
		private string m_City;
		private string m_State;
		private string m_Zip;

		public MaterialLocation()
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
		public int MaterialLocationId
		{
			get { return this.m_MaterialLocationId; }
			set
			{
				if(this.m_MaterialLocationId != value)
				{
					this.m_MaterialLocationId = value;
					this.NotifyPropertyChanged("MaterialLocationId");
				}
			}
		}

        [PersistentProperty()]
		public string Name
		{
			get { return this.m_Name; }
			set
			{
				if(this.m_Name != value)
				{
					this.m_Name = value;
					this.NotifyPropertyChanged("Name");
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
	}
}
