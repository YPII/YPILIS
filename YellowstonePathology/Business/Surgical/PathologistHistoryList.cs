using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Surgical
{
	public class PathologistHistoryList : ObservableCollection<PathologistHistoryItem>, INotifyPropertyChanged
	{
		public PathologistHistoryList()
		{

		}

		public string NoHistoryFound
		{
			get
			{
				string result = string.Empty;
				if (this.Count == 0)
				{
					result = "No history found";
				}
				return result;
			}
		}

		public bool NoHistoryFoundVisibility
		{
			get
			{
				bool result = false;
				if (this.Count == 0)
				{
					result = true;
				}
				return result;
			}
			set
			{
				base.OnPropertyChanged(new PropertyChangedEventArgs("NoHistoryFoundVisibility"));
			}
		}		
	}

	public class PathologistHistoryItem : ListItem
	{
		private PathologistHistoryItemList m_PathologistHistoryItemList;
		private string m_ReportNo;
        private System.Nullable<DateTime> m_AccessionDate;
        private System.Nullable<DateTime> m_FinalDate;
        private string m_ForeColor;
        private string m_PanelSetName;

		public PathologistHistoryItem()
		{
			m_PathologistHistoryItemList = new PathologistHistoryItemList();
		}

		public PathologistHistoryItemList PathologistHistoryItemList
		{
			get { return this.m_PathologistHistoryItemList; }
		}

		[PersistentProperty()]
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

		[PersistentProperty()]
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

		[PersistentProperty()]
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

		[PersistentProperty()]
		public string Final
		{
			get
			{
				if (this.m_FinalDate.HasValue)
				{
					return "Final";
				}
				return "Not Final";
			}
			set
			{
			}
		}

		[PersistentProperty()]
		public string ForeColor
		{
			get { return this.m_ForeColor; }
			set
			{
				this.m_ForeColor = value;
				NotifyPropertyChanged("ForeColor");
			}
		}

        [PersistentProperty()]
        public string PanelSetName
        {
            get { return this.m_PanelSetName; }
            set
            {
                this.m_PanelSetName = value;
                NotifyPropertyChanged("PanelSetName");
            }
        }
	}

	public class PathologistHistoryItemList : ObservableCollection<PathologistHistoryItemListItem>
	{
		public PathologistHistoryItemList()
		{
		}
	}	
}
