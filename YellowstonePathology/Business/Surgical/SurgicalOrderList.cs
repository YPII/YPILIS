using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Data;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Surgical
{
	public class SurgicalOrderList : ObservableCollection<SurgicalOrderListItem>
	{
		private ListCollectionView m_ListCollectionView;
		private int m_PathologistId = 0;

		public SurgicalOrderList()
		{
			this.m_ListCollectionView = new ListCollectionView(this);
		}

		public ListCollectionView ListCollectionView
		{
			get { return this.m_ListCollectionView; }
		}

		public void FilterByPathologistId(int pathologistId)
		{
			this.m_PathologistId = pathologistId;
			if (pathologistId <= 0)
			{
				this.m_ListCollectionView.Filter = null;
			}
			else
			{
				this.m_ListCollectionView.Filter = new Predicate<object>(this.PathologistIdFilter);
			}
		}

		private bool PathologistIdFilter(object listItem)
		{
			YellowstonePathology.Business.Surgical.SurgicalOrderListItem item = (YellowstonePathology.Business.Surgical.SurgicalOrderListItem)listItem;

			if (item.PathologistId == this.m_PathologistId)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public void FillByAccessionDate(DateTime date)
		{
			SurgicalOrderList surgicalOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSurgicalOrderListByAccessionDate(date);
			this.FillFromList(surgicalOrderList);
		}

		public void FillByFinalDate(DateTime date)
		{
			SurgicalOrderList surgicalOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSurgicalOrderListByFinalDate(date);
			this.FillFromList(surgicalOrderList);
		}

        public void FillByPqri(DateTime finalDate)
        {
			SurgicalOrderList surgicalOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSurgicalOrderListByAccessionDatePQRI(finalDate);
            this.FillFromList(surgicalOrderList);
        }

		public void FillByNotAudited()
		{
			SurgicalOrderList surgicalOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSurgicalOrderListByNotAudited();
			this.FillFromList(surgicalOrderList);
		}

		public void FillByIntraoperative()
		{
			SurgicalOrderList surgicalOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSurgicalOrderListByIntraoperative();
			this.FillFromList(surgicalOrderList);
		}

		public void FillByNoSignature()
		{
			SurgicalOrderList surgicalOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSurgicalOrderListByNoSignature();
			this.FillFromList(surgicalOrderList);
		}

        public void FillByNoGross()
        {
            SurgicalOrderList surgicalOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSurgicalOrderListByNoGross();
            this.FillFromList(surgicalOrderList);
        }

        public void FillByNoClinicalInfo()
        {
            SurgicalOrderList surgicalOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSurgicalOrderListByNoClinicalInfo();
            this.FillFromList(surgicalOrderList);
        }

		public void FillBySvhClientOrder(DateTime date)
		{
			SurgicalOrderList surgicalOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSurgicalOrderListForSvhClientOrder(date);
			this.FillFromList(surgicalOrderList);
		}

		private void FillFromList(SurgicalOrderList surgicalOrderList)
		{
			this.Clear();
			if (surgicalOrderList != null)
			{
				foreach (SurgicalOrderListItem item in surgicalOrderList)
				{
					this.Add(item);
				}
			}
		}
	}

	public class SurgicalOrderListItem : ListItem
    {
        private string m_ReportNo;
		private string m_PatientName;
		private System.Nullable<DateTime> m_AcceptedDate;
		private System.Nullable<DateTime> m_FinalDate;
		private string m_OriginatingLocation;
		private string m_Pathologist;
		private int m_PathologistId;
		private bool m_Audited;		

		public SurgicalOrderListItem()
        {
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
		public string PatientName
		{
			get
			{
				return this.m_PatientName;
			}
			set
			{
				if (this.m_PatientName != value)
				{
					this.m_PatientName = value;
					NotifyPropertyChanged("PatientName");
				}
			}
		}

		[PersistentProperty()]
		public System.Nullable<DateTime> AcceptedDate
		{
			get
			{
				return this.m_AcceptedDate;
			}
			set
			{
				if (this.m_AcceptedDate != value)
				{
					this.m_AcceptedDate = value;
					NotifyPropertyChanged("AcceptedDate");
				}
			}
		}

		[PersistentProperty()]
		public System.Nullable<DateTime> FinalDate
		{
			get
			{
				return this.m_FinalDate;
			}
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
		public string OriginatingLocation
		{
			get
			{
				return this.m_OriginatingLocation;
			}
			set
			{
				if (this.m_OriginatingLocation != value)
				{
					this.m_OriginatingLocation = value;
					NotifyPropertyChanged("OriginatingLocation");
				}
			}
		}

		[PersistentProperty()]
		public string Pathologist
		{
			get
			{
				return this.m_Pathologist;
			}
			set
			{
				if (this.m_Pathologist != value)
				{
					this.m_Pathologist = value;
					NotifyPropertyChanged("Pathologist");
				}
			}
		}

		[PersistentProperty()]
		public int PathologistId
		{
			get
			{
				return this.m_PathologistId;
			}
			set
			{
				if (this.m_PathologistId != value)
				{
					this.m_PathologistId = value;
					NotifyPropertyChanged("PathologistId");
				}
			}
		}

		[PersistentProperty()]
		public bool Audited
		{
			get
			{
				return this.m_Audited;
			}
			set
			{
				if (this.m_Audited != value)
				{
					this.m_Audited = value;
					NotifyPropertyChanged("Audited");
				}
			}
		}		
	}
}
