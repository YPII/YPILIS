using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model
{
	public class Medium
	{
		protected YellowstonePathology.Business.Test.AliquotOrder m_AliquotOrder;
		protected Collection<YellowstonePathology.Business.Test.Model.TestOrder> m_TestOrderItems;
		protected string m_Label;
		protected string m_Type;
		protected int m_SpecimenNumber;
		protected int m_AliquotIndex;
		protected YellowstonePathology.Business.Specimen.Model.AliquotLetterList m_AliquotLetterList;

		public Medium(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder, Collection<YellowstonePathology.Business.Test.Model.TestOrder> testOrders, int specimenNumber, string specimenDescription, int aliquotIndex)
		{
			this.m_AliquotLetterList = new YellowstonePathology.Business.Specimen.Model.AliquotLetterList();
			this.m_AliquotOrder = aliquotOrder;
			this.m_TestOrderItems = testOrders;
			this.m_SpecimenNumber = specimenNumber;
			this.m_AliquotIndex = aliquotIndex;
		}

		public string Label
		{
			get { return this.m_AliquotOrder.Display; }
		}

		public string Type
		{
			get { return this.m_Type; }
		}

		public YellowstonePathology.Business.Test.AliquotOrder AliquotOrder
		{
			get { return this.m_AliquotOrder; }
		}

		public Collection<YellowstonePathology.Business.Test.Model.TestOrder> TestOrders
		{
			get { return this.m_TestOrderItems; }
		}
	}
}
