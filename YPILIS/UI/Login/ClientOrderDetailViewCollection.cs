using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Collections;

namespace YellowstonePathology.UI.Login
{
	public class ClientOrderDetailViewCollection : ObservableCollection<ClientOrderDetailView>
	{
        public ClientOrderDetailViewCollection(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailCollection clientOrderDetailCollection)
		{            
            foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail in clientOrderDetailCollection)
            {
                if (!clientOrderDetail.Inactive)
                {
                    ClientOrderDetailView clientOrderDetailView = new ClientOrderDetailView(clientOrderDetail);
                    this.Add(clientOrderDetailView);
                }
            }
            this.SortAscending();
		}		        		

		public void SetClientOrderDetailReceived(string containerId)
		{
			foreach (ClientOrderDetailView clientOrderDetailView in this)
			{
                if (clientOrderDetailView.ClientOrderDetail.ContainerId == containerId)
				{
					clientOrderDetailView.ClientOrderDetail.Received = true;
                    clientOrderDetailView.ClientOrderDetail.DateReceived = DateTime.Now;
					clientOrderDetailView.NotifyPropertyChanged("");
					break;
				}
			}
		}

		public void AddClientOrderDetailForValidation(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail)
		{
			bool found = false;
			foreach (ClientOrderDetailView clientOrderDetailView in this)
			{
				if (clientOrderDetailView.ClientOrderDetail.ContainerId == clientOrderDetail.ContainerId)
				{
					found = true;
					break;
				}
			}

			if (!found && !clientOrderDetail.Inactive)
			{
				ClientOrderDetailView clientOrderDetailView = new ClientOrderDetailView(clientOrderDetail);
				if (clientOrderDetail.Accessioned)
				{
					clientOrderDetailView.ClientOrderDetail.Validated = true;
				}
				this.Add(clientOrderDetailView);
			}
		}

		public void SortAscending()
		{
            
			for (int i = this.Count - 1; i >= 0; i--)
			{
				for (int j = 1; j <= i; j++)
				{
					object o1 = this[j - 1];
					object o2 = this[j];
					if (((IComparable)o1).CompareTo(o2) > 0)
					{
						((IList)this).Remove(o1);
						((IList)this).Insert(j, o1);
					}
				}
			}
		}
	}
}
