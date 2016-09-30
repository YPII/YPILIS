using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
	public class FixationDetailsCollection : ObservableCollection<FixationDetails>
	{
		public FixationDetailsCollection()
		{
		}

		public FixationDetailsCollection(YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection specimenOrderCollection, YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection)
		{
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in specimenOrderCollection)
			{
				FixationDetails fixationDetails = new FixationDetails(specimenOrder);

				foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder in clientOrderCollection)
				{
					bool found = false;
					foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail in clientOrder.ClientOrderDetailCollection)
					{
						if (clientOrderDetail.ContainerId == specimenOrder.ContainerId)
						{
							fixationDetails.SetCollectionDate(clientOrderDetail.CollectionDate);
							found = true;
							break;
						}
					}

					if (!found)
					{
						fixationDetails.SetCollectionDate(null);
					}
				}
				this.Add(fixationDetails);
			}
		}
	}
}
