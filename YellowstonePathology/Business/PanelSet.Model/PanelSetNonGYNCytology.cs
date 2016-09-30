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

            this.m_EpicDistributionIsImplemented = true;

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder).AssemblyQualifiedName;
            string taskDescription = "Perform testing.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, taskDescription));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();

            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceCYTO());
		}
	}
}
