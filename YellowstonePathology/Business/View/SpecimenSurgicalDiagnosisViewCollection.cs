using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.View
{
    public class SpecimenSurgicalDiagnosisViewCollection : ObservableCollection<SpecimenSurgicalDiagnosisView>
    {        
        public SpecimenSurgicalDiagnosisViewCollection(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {            
            this.BuildCollection(accessionOrder);
        }

        public void Refresh(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.Clear();
            this.BuildCollection(accessionOrder);
        }

        private void BuildCollection(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in accessionOrder.SpecimenOrderCollection)
            {
                SpecimenSurgicalDiagnosisView item = new SpecimenSurgicalDiagnosisView();
                item.SpecimenOrder = specimenOrder;

				YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrderSurgical = accessionOrder.PanelSetOrderCollection.GetSurgical();
				YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen = panelSetOrderSurgical.SurgicalSpecimenCollection.GetBySpecimenOrderId(specimenOrder.SpecimenOrderId);
                if (surgicalSpecimen != null)
                {
                    item.SurgicalSpecimen = surgicalSpecimen;
                    item.SurgicalDiagnosisIsOrdered = true;
                }
                else
                {
                    item.SurgicalDiagnosisIsOrdered = false;
                }

                this.Add(item);
            }
        }        
    }
}
