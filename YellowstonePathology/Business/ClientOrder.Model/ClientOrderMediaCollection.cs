using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	public class ClientOrderMediaCollection : ObservableCollection<ClientOrderMedia>
	{
		public ClientOrderMediaCollection()
		{

		}

		public ClientOrderMediaCollection(string orderType)
		{
			if (orderType == "HL7 Order" || orderType == "HL7 Blanket Order with Requisition" || orderType == "Requisition")
			{
				ClientOrderMedia clientOrderMedia = new ClientOrderMedia(ClientOrderMediaEnum.Requisition);
				clientOrderMedia.HasPatientId = true;
				clientOrderMedia.HasSpecimenId = true;
				this.Add(clientOrderMedia);
			}
		}

		public void SetItem(ClientOrderMedia clientOrderMedia)
		{
			foreach (ClientOrderMedia item in this)
			{
				if (clientOrderMedia.ClientOrderMediaEnum == item.ClientOrderMediaEnum)
				{
					this.Remove(item);
					return;
				}
			}

			this.Add(clientOrderMedia);
		}

		public bool HasRequisition
		{
			get
			{
				bool result = false;
				foreach (ClientOrderMedia item in this)
				{
					if (item.ClientOrderMediaEnum == ClientOrderMediaEnum.Requisition)
					{
						result = true;
						break;
					}
				}
				return result;
			}
		}

		public bool HasBarcode
		{
			get
			{
				bool result = false;
				foreach (ClientOrderMedia item in this)
				{
					if (item.ClientOrderMediaEnum == ClientOrderMediaEnum.Specimen)
					{
						if (item.HasBarcode)
						{
							result = true;
							break;
						}
					}
				}
				return result;
			}
		}

		public bool HasSpecimen
		{
			get
			{
				bool result = false;
				foreach (ClientOrderMedia item in this)
				{
					if (item.ClientOrderMediaEnum == ClientOrderMediaEnum.Specimen)
					{
						result = true;
						break;
					}
				}
				return result;
			}
		}

		public int NextSpecimenNumber()
		{
			int result = 1;
			foreach (ClientOrderMedia clientOrderMedia in this)
			{
				if (clientOrderMedia.ClientOrderMediaEnum == ClientOrderMediaEnum.Specimen)
				{
					result++;
				}
			}
			return result;
		}

		public string GetContainerIds()
		{
			StringBuilder containerIds = new StringBuilder();
			foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderMedia clientOrderMedia in this)
			{
                if (clientOrderMedia.ClientOrderMediaEnum == Business.ClientOrder.Model.ClientOrderMediaEnum.Specimen)
				{
					containerIds.Append(clientOrderMedia.ContainerId + ",");
				}
			}
			containerIds.Remove(containerIds.Length - 1, 1);

			return containerIds.ToString();
		}
	}
}
