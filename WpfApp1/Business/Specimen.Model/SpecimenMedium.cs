using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model
{
	public class SpecimenMedium : Medium
	{
		public SpecimenMedium(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder, Collection<YellowstonePathology.Business.Test.Model.TestOrder> testOrders, int specimenNumber, string specimenDescription, int aliquotIndex)
			: base(aliquotOrder, testOrders, specimenNumber, specimenDescription, aliquotIndex)
		{
			this.m_Label = specimenDescription;
			this.m_Type = "Specimen";
		}
	}
}
