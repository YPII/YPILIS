using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class SpecimenOrderAliquotOrderReturnEventArgs : System.EventArgs
    {
		YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
        YellowstonePathology.Business.Test.AliquotOrder m_AliquotOrder;

		public SpecimenOrderAliquotOrderReturnEventArgs(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, 
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder)
        {
            this.m_SpecimenOrder = specimenOrder;
            this.m_AliquotOrder = aliquotOrder;
        }

		public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
        {
            get { return this.m_SpecimenOrder; }
        }

        public YellowstonePathology.Business.Test.AliquotOrder AliquotOrder
        {
            get { return this.m_AliquotOrder; }
        }
    }
}
