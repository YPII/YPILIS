using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class SpecimenOrderAudit : Audit
    {
        protected YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;

        public SpecimenOrderAudit(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            this.m_SpecimenOrder = specimenOrder;
        }        

        public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
        {
            get { return this.m_SpecimenOrder; }
        }
        
        public override string ToString()
        {
            return "Specimen: " + this.m_SpecimenOrder.Description + ": " + this.m_Message;
        }
    }
}
