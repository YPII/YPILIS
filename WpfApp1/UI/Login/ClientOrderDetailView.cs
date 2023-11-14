using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.UI.Login
{
	public class ClientOrderDetailView : INotifyPropertyChanged, IComparable
	{
		public event PropertyChangedEventHandler PropertyChanged;        
		private YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail m_ClientOrderDetail;

		public ClientOrderDetailView(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail)
		{            
			this.m_ClientOrderDetail = clientOrderDetail;
		}

		public YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail ClientOrderDetail
		{
			get { return this.m_ClientOrderDetail; }
		}        

		public string DisplayStatus
		{
			get
			{
				string result = "Entered";
				if (this.ClientOrderDetail.Submitted)
				{
					result = "Submitted";
				}
				if (this.ClientOrderDetail.Received)
				{
					result = "Received";
				}
				if(this.ClientOrderDetail.Accessioned)
				{
					result = "Accessioned";
				}
				if(this.ClientOrderDetail.Validated)
				{
					result = "Verified";
				}
				return result;
			}
		}		

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public int CompareTo(object compareObject)
		{
			ClientOrderDetailView x = (ClientOrderDetailView)compareObject;
			return this.ClientOrderDetail.SpecimenNumber.CompareTo(x.ClientOrderDetail.SpecimenNumber);
		}
	}
}
