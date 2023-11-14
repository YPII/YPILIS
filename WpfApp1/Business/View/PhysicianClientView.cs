using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.View
{
	public class PhysicianClientView : YellowstonePathology.Business.Domain.Physician
	{
		private ObservableCollection<YellowstonePathology.Business.Client.Model.Client> m_Clients;

		public PhysicianClientView()
		{
			this.m_Clients = new ObservableCollection<YellowstonePathology.Business.Client.Model.Client>();
		}

		public ObservableCollection<YellowstonePathology.Business.Client.Model.Client> Clients
		{
			get { return this.m_Clients; }
		}

		public bool ClientExists(int clientId)
		{
			bool result = false;
			foreach (YellowstonePathology.Business.Client.Model.Client client in this.Clients)
			{
				if (client.ClientId == clientId)
				{
					result = true;
					break;
				}
			}
			return result;
		}
	}
}
