using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Visitor
{
    public class AddSpecimenOrderVisitor : AccessionTreeVisitor
    {
		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;

		public AddSpecimenOrderVisitor(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
            : base(true, true)
        {
            this.m_SpecimenOrder = specimenOrder;
        }        

        public override void Visit(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder)
        {
            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ThinPrepFluid thinPrepFluid = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ThinPrepFluid();
            if (this.m_SpecimenOrder.SpecimenId != thinPrepFluid.SpecimenId)
            {
                if (surgicalTestOrder.SurgicalSpecimenCollection.SpecimenOrderExists(this.m_SpecimenOrder.SpecimenOrderId) == false)
                {
                    YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen = surgicalTestOrder.SurgicalSpecimenCollection.Add(surgicalTestOrder.ReportNo);
                    surgicalSpecimen.FromSpecimenOrder(this.m_SpecimenOrder);
                }
            }         
        }
    }
}
