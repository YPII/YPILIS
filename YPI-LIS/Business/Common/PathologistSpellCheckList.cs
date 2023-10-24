using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace YellowstonePathology.Business.Common
{
    public class PathologistSpellCheckList : List<SpellCheckListItem>
    {
		public PathologistSpellCheckList(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
			YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrderSurgical = (YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder)panelSetOrder;
			foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
			{
				Type ssrType = surgicalSpecimen.GetType();
				PropertyInfo diagnosisProperty = ssrType.GetProperty("Diagnosis");
				this.Add(SpellCheckListItem.CreateSpellCheckListItem(diagnosisProperty, surgicalSpecimen));
			}

			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in accessionOrder.SpecimenOrderCollection)
			{
				Type soType = specimenOrder.GetType();
				PropertyInfo descriptionProperty = soType.GetProperty("Description");
				this.Add(SpellCheckListItem.CreateSpellCheckListItem(descriptionProperty, specimenOrder));
			}

			Type srType = panelSetOrderSurgical.GetType();
			PropertyInfo microscopicXProperty = srType.GetProperty("MicroscopicX");
			this.Add(SpellCheckListItem.CreateSpellCheckListItem(microscopicXProperty, panelSetOrderSurgical));

            Type aoType = accessionOrder.GetType();
			PropertyInfo clinicalHistoryProperty = aoType.GetProperty("ClinicalHistory");
			this.Add(SpellCheckListItem.CreateSpellCheckListItem(clinicalHistoryProperty, accessionOrder));

			PropertyInfo grossXProperty = srType.GetProperty("GrossX");
			this.Add(SpellCheckListItem.CreateSpellCheckListItem(grossXProperty, panelSetOrderSurgical));

			PropertyInfo commentProperty = srType.GetProperty("Comment");
			this.Add(SpellCheckListItem.CreateSpellCheckListItem(commentProperty, panelSetOrderSurgical));
			
			PropertyInfo cancerSummaryProperty = srType.GetProperty("CancerSummary");
			this.Add(SpellCheckListItem.CreateSpellCheckListItem(cancerSummaryProperty, panelSetOrderSurgical));
		}
    }
}
