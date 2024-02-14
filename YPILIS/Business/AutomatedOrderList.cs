using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.ComponentModel;

namespace YellowstonePathology.Business
{
	[XmlType("AutomatedOrderList")]
	public class AutomatedOrderList : ObservableCollection<AutomatedOrderListItem>
	{
	}

	[XmlType("AutomatedOrderListItem")]
	public class AutomatedOrderListItem : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_ReportNo;
		private System.Nullable<DateTime> m_AccessionDate;
		private System.Nullable<DateTime> m_FinalDate;
		private string m_PatientName;
		private string m_Block;
		private string m_CaseType;

		public AutomatedOrderListItem()
		{
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public string ReportNo
		{
			get { return this.m_ReportNo; }
			set
			{
				if (this.m_ReportNo != value)
				{
					this.m_ReportNo = value;
					NotifyPropertyChanged("ReportNo");
				}
			}
		}

		public System.Nullable<DateTime> AccessionDate
		{
			get { return this.m_AccessionDate; }
			set
			{
				if (this.m_AccessionDate != value)
				{
					this.m_AccessionDate = value;
					NotifyPropertyChanged("AccessionDate");
				}
			}
		}

		public System.Nullable<DateTime> FinalDate
		{
			get { return this.m_FinalDate; }
			set
			{
				if (this.m_FinalDate != value)
				{
					this.m_FinalDate = value;
					NotifyPropertyChanged("FinalDate");
				}
			}
		}

		public string PatientName
		{
			get { return this.m_PatientName; }
			set
			{
				if (this.m_PatientName != value)
				{
					this.m_PatientName = value;
					NotifyPropertyChanged("PatientName");
				}
			}
		}

		public string Block
		{
			get { return this.m_Block; }
			set
			{
				if (this.m_Block != value)
				{
					this.m_Block = value;
					NotifyPropertyChanged("Block");
				}
			}
		}

		public string CaseType
		{
			get { return this.m_CaseType; }
			set
			{
				if (this.m_CaseType != value)
				{
					this.m_CaseType = value;
					NotifyPropertyChanged("CaseType");
				}
			}
		}
	}
}