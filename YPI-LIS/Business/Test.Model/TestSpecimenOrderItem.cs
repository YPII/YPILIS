using System;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.Model
{
	public class TestSpecimenOrderItem : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private int m_TestSpecimenOrderId;
		private string m_TestOrderId;
		private string m_SpecimenOrderId;

		public TestSpecimenOrderItem()
		{
		}

		[PersistentProperty()]
		public int TestSpecimenOrderId
		{
			get { return this.m_TestSpecimenOrderId; }
			set
			{
				if (this.m_TestSpecimenOrderId != value)
				{
					this.m_TestSpecimenOrderId = value;
					this.NotifyPropertyChanged("TestSpecimenOrderId");
				}
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
		public string SpecimenOrderId
		{
			get { return this.m_SpecimenOrderId; }
			set
			{
				if (this.m_SpecimenOrderId != value)
				{
					this.m_SpecimenOrderId = value;
					this.NotifyPropertyChanged("SpecimenOrderId");
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
