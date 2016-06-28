using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Xml.Linq;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Domain.Cytology
{
	public class OtherCondition : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private int m_LineID;
		private string m_OtherConditionText;

		public OtherCondition()
        {
            //TODO: added to make assignment SH.
            this.m_OtherConditionText = string.Empty;
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		[PersistentProperty()]
		public int LineID
		{
			get { return this.m_LineID; }
			set
			{
				if (this.m_LineID != value)
				{
					this.m_LineID = value;
					this.NotifyPropertyChanged("LineID");
				}
			}
		}

		[PersistentProperty()]
		public string OtherConditionText
		{
			get { return this.m_OtherConditionText; }
			set
			{
				if (this.m_OtherConditionText != value)
				{
					this.m_OtherConditionText = value;
					this.NotifyPropertyChanged("OtherConditionText");
				}
			}
		}
	}
}
