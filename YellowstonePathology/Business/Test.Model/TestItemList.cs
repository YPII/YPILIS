using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.Model
{
	public class TestItemList : ObservableCollection<TestItemListItem>
	{
        public TestItemList()
        {
		}

		public void SetTestNameById(int testId, string testName)
		{
			foreach (TestItemListItem item in this)
			{
				if (item.TestId == testId)
				{
					item.TestName = testName;
					break;
				}
			}
		}
	}

	public class TestItemListItem : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private int m_TestId;
		private string m_TestName;
		private string m_ReportLabel;

		public TestItemListItem()
		{
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		[PersistentProperty()]
        public int TestId
        {
			get { return this.m_TestId; }
			set
			{
				if (this.m_TestId != value)
				{
					this.m_TestId = value;
					NotifyPropertyChanged(string.Empty);
				}
			}
        }

		[PersistentProperty()]
		public string TestName
        {
			get { return this.m_TestName; }
			set
			{
				if (this.m_TestName != value)
				{
					this.m_TestName = value;
					NotifyPropertyChanged(string.Empty);
				}
			}
        }

		[PersistentProperty()]
		public string ReportLabel
        {
            get { return this.m_ReportLabel; }
            set
            {
                if (this.m_ReportLabel != value)
                {
					this.m_ReportLabel = value;
                    NotifyPropertyChanged(string.Empty);
				}
            }
        }
	}
}
