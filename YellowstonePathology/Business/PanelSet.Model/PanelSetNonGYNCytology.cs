using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetNonGYNCytology : YellowstonePathology.Business.Test.Surgical.SurgicalTest
	{
        public PanelSetNonGYNCytology()
		{
			this.m_PanelSetId = 128;
			this.m_PanelSetName = "Non GYN Cytology";

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder).AssemblyQualifiedName;
            string taskDescription = "Perform testing.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, taskDescription));

            this.m_TechnicalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_ProfessionalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");

            this.m_TechnicalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_ProfessionalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceCYTO());
		}
	}
}
