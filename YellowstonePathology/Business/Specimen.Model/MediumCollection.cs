using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model
{
	public class MediumCollection : ObservableCollection<Medium>
	{
		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;


		public MediumCollection(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			this.m_SpecimenOrder = specimenOrder;
			this.Refresh(accessionOrder);
		}

		public void Refresh(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			this.Clear();
			int count = 0;
			foreach(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in this.m_SpecimenOrder.AliquotOrderCollection)
			{
				Collection<YellowstonePathology.Business.Test.Model.TestOrder> testOrders = new Collection<YellowstonePathology.Business.Test.Model.TestOrder>();
                foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in aliquotOrder.TestOrderCollection)
                {
                    testOrders.Add(testOrder);
                }

				YellowstonePathology.Business.Specimen.Model.Medium medium = this.GetMedium(aliquotOrder, testOrders, count);
				this.Add(medium);
				count++;
			}
		}

		private YellowstonePathology.Business.Specimen.Model.Medium GetMedium(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder, Collection<YellowstonePathology.Business.Test.Model.TestOrder> testOrders, int index)
		{
			YellowstonePathology.Business.Specimen.Model.Medium medium = null;
			switch (aliquotOrder.AliquotType)
			{
				case "Specimen":
					medium = new SpecimenMedium(aliquotOrder, testOrders, this.m_SpecimenOrder.SpecimenNumber, this.m_SpecimenOrder.Description, index);
					break;
				case "Slide":
					medium = new SlideMedium(aliquotOrder, testOrders, this.m_SpecimenOrder.SpecimenNumber, this.m_SpecimenOrder.Description, index);
					break;
				case "Block":
					medium = new BlockMedium(aliquotOrder, testOrders, this.m_SpecimenOrder.SpecimenNumber, this.m_SpecimenOrder.Description, index);					
					break;
				case "FrozenBlock":
					medium = new FrozenBlockMedium(aliquotOrder, testOrders, this.m_SpecimenOrder.SpecimenNumber, this.m_SpecimenOrder.Description, index);
					break;
				case "CellBlock":
					medium = new CellBlockMedium(aliquotOrder, testOrders, this.m_SpecimenOrder.SpecimenNumber, this.m_SpecimenOrder.Description, index);
					break;
			}
			return medium;
		}
	}
}
