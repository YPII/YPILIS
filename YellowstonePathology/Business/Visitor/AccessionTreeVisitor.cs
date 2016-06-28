using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Visitor
{
    public abstract class AccessionTreeVisitor
    {
        private bool m_VisitLeftSide;        
        private bool m_VisitRightSide;        

        public AccessionTreeVisitor(bool visitLeftSide, bool visitRightSide)
        {
            this.m_VisitLeftSide = visitLeftSide;
            this.m_VisitRightSide = visitRightSide;            
        }

        public bool VisitLeftSide
        {
            get { return this.m_VisitLeftSide; }
        }

        public bool VisitRightSide
        {
            get { return this.m_VisitRightSide; }
        }        

        public virtual void Visit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {

        }

		public virtual void Visit(YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection specimenOrderCollection)
        {

        }

		public virtual void Visit(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {

        }

        public virtual void Visit(YellowstonePathology.Business.Test.AliquotOrderCollection aliquotOrderCollection)
        {

        }        

        public virtual void Visit(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder)
        {

        }

        public virtual void Visit(YellowstonePathology.Business.Test.PanelSetOrderCollection panelSetOrderCollection)
        {

        }

        public virtual void Visit(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {

        }

        public virtual void Visit(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder)
        {

        }

        public virtual void Visit(YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenCollection surgicalSpecimenCollection)
        {

        }

        public virtual void Visit(YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen)
        {

        }

        public virtual void Visit(YellowstonePathology.Business.SpecialStain.StainResultItemCollection stainResultCollection)
        {

        }

        public virtual void Visit(YellowstonePathology.Business.SpecialStain.StainResultItem stainResult)
        {

        }

        public virtual void Visit(YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResultCollection intraoperativeConsultationResultCollection)
        {

        }

        public virtual void Visit(YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult intraoperativeConsultationResult)
        {

        }

        public virtual void Visit(YellowstonePathology.Business.Test.PanelOrderCollection panelOrderCollection)
        {

        }

        public virtual void Visit(YellowstonePathology.Business.Test.PanelOrder panelOrder)
        {

        }

        public virtual void Visit(YellowstonePathology.Business.Test.Model.TestOrderCollection testOrderCollection)
        {

        }

        public virtual void Visit(YellowstonePathology.Business.Test.Model.TestOrderCollection_Base testOrderCollection)
        {

        }

        public virtual void Visit(YellowstonePathology.Business.Test.Model.TestOrder testOrder)
        {

        }

        public virtual void Visit(YellowstonePathology.Business.Test.Model.TestOrder_Base testOrder)
        {

        }  
    }
}
