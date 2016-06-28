using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Slide.Model
{
	public class SlideLabel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_SlideLabelId;
		private string m_ReportNo;
		private int m_Quantity;
		private string m_PLastName;
		private string m_AccessioningFacilityId;
		private string m_SlideNumber;
		private string m_TestName;

		public SlideLabel()
        {
            this.m_Quantity = 0;            
        }		 

		[PersistentProperty()]
		public string SlideLabelId
		{
			get { return this.m_SlideLabelId; }
			set
			{
				if (this.m_SlideLabelId != value)
				{
					this.m_SlideLabelId = value;
					this.NotifyPropertyChanged("SlideLabelId");
				}
			}
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
					this.NotifyPropertyChanged("ReportNo");
				}
			}
		}

		[PersistentProperty()]
		public int Quantity
		{
			get { return this.m_Quantity; }
			set
			{
				if (this.m_Quantity != value)
				{
					this.m_Quantity = value;
					this.NotifyPropertyChanged("Quantity");
				}
			}
		}

		[PersistentProperty()]
		public string PLastName
		{
			get { return this.m_PLastName; }
			set
			{
				if (this.m_PLastName != value)
				{
					this.m_PLastName = value;
					this.NotifyPropertyChanged("PLastName");
				}
			}
		}

		[PersistentProperty()]
		public string AccessioningFacilityId
		{
			get { return this.m_AccessioningFacilityId; }
			set
			{
				if (this.m_AccessioningFacilityId != value)
				{
					this.m_AccessioningFacilityId = value;
					this.NotifyPropertyChanged("AccessioningFacilityId");
				}
			}
		}

		[PersistentProperty()]
		public string SlideNumber
		{
			get { return this.m_SlideNumber; }
			set
			{
				if (this.m_SlideNumber != value)
				{
					this.m_SlideNumber = value;
					this.NotifyPropertyChanged("SlideNumber");
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

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }      
	}
}
