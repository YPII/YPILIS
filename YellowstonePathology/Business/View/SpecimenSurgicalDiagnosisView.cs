using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.View
{
    public class SpecimenSurgicalDiagnosisView
    {
		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
		private YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen m_SurgicalSpecimen;
        private bool m_SurgicalDiagnosisIsOrdered;

        public SpecimenSurgicalDiagnosisView()
        {
            
        }

		public static SpecimenSurgicalDiagnosisView Parse(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            SpecimenSurgicalDiagnosisView result = new SpecimenSurgicalDiagnosisView();
            result.SpecimenOrder = specimenOrder;

            if (accessionOrder.PanelSetOrderCollection.HasSurgical() == true)
            {
				YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrderSurgical = accessionOrder.PanelSetOrderCollection.GetSurgical();
				if (panelSetOrderSurgical.SurgicalSpecimenCollection.SpecimenOrderExists(specimenOrder.SpecimenOrderId) == true)
                {
					YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen = panelSetOrderSurgical.SurgicalSpecimenCollection.GetBySpecimenOrderId(specimenOrder.SpecimenOrderId);
                    result.SurgicalSpecimen = surgicalSpecimen;
                    result.SurgicalDiagnosisIsOrdered = true;
                }
                else
                {
                    result.SurgicalDiagnosisIsOrdered = false;
                }
            }
            else
            {
                result.SurgicalDiagnosisIsOrdered = false;                
            }

            return result;
        }

		public YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen SurgicalSpecimen
        {
            get { return this.m_SurgicalSpecimen; }
            set { this.m_SurgicalSpecimen = value; }
        }

		public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
        {
            get { return this.m_SpecimenOrder; }
            set { this.m_SpecimenOrder = value; }
        }

        public bool SurgicalDiagnosisIsOrdered
        {
            get { return this.m_SurgicalDiagnosisIsOrdered; }
            set { this.m_SurgicalDiagnosisIsOrdered = value; }
        }
    }
}
