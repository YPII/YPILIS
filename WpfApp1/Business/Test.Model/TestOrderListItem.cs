using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.Model
{
	public class TestOrderListItem : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private string m_TestOrderId;
		private string m_AliquotOrderId;
		private int m_TestId;
		private string m_TestName;
		private string m_OrderedBy;

		public TestOrderListItem()
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
		public string TestOrderId
		{
			get { return this.m_TestOrderId; }
			set
			{
				if (this.m_TestOrderId != value)
				{
					this.m_TestOrderId = value;
					this.NotifyPropertyChanged("TestOrderId");
				}
			}
		}

		[PersistentProperty()]
		public string AliquotOrderId
		{
			get { return this.m_AliquotOrderId; }
			set
			{
				if (this.m_AliquotOrderId != value)
				{
					this.m_AliquotOrderId = value;
					this.NotifyPropertyChanged("AliquotOrderId");
				}
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
					this.NotifyPropertyChanged("TestId");
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
					this.NotifyPropertyChanged("TestName");
				}
			}
		}

		[PersistentProperty()]
		public string OrderedBy
		{
			get { return this.m_OrderedBy; }
			set
			{
				if (this.m_OrderedBy != value)
				{
					this.m_OrderedBy = value;
					this.NotifyPropertyChanged("OrderedBy");
				}
			}
		}
	}
}
