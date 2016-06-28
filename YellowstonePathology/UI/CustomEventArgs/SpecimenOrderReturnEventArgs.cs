using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class SpecimenOrderReturnEventArgs : System.EventArgs
    {
		YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;

		public SpecimenOrderReturnEventArgs(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            this.m_SpecimenOrder = specimenOrder;
        }

		public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
        {
            get { return this.m_SpecimenOrder; }
        }
    }
}
