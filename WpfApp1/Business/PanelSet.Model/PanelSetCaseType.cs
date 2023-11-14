using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetCaseType : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_CaseType;

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		[PersistentProperty()]
		public string CaseType
		{
			get { return this.m_CaseType; }
			set
			{
				if (this.m_CaseType != value)
				{
					this.m_CaseType = value;
					this.NotifyPropertyChanged("CaseType");
				}
			}
		}
	}
}
