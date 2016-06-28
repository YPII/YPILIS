using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business
{
	[PersistentClass("tblApplicationVersion", "YPIDATA")]
	public class ApplicationVersion
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private string m_Version;
		private bool m_EnforceChange;

		public ApplicationVersion()
		{
		}

		[PersistentDocumentIdProperty()]
		[PersistentPrimaryKeyProperty(false)]
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

		[PersistentProperty()]
		public string Version
		{
			get { return this.m_Version; }
			set
			{
				if (this.m_Version != value)
				{
					this.m_Version = value;
					this.NotifyPropertyChanged("Version");
				}
			}
		}

		[PersistentProperty()]
		public bool EnforceChange
		{
			get { return this.m_EnforceChange; }
			set
			{
				if (this.m_EnforceChange != value)
				{
					this.m_EnforceChange = value;
					this.NotifyPropertyChanged("EnforceChange");
				}
			}
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
