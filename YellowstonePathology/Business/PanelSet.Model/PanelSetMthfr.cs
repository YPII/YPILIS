using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetMthfr : PanelSetMolecularTest
	{
		public PanelSetMthfr()
		{
			this.m_PanelSetId = 34;
			this.m_PanelSetName = "MTHFR";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;			
			this.m_ResultDocumentSource = ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterM();
            this.m_Active = true;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;

            this.m_SurgicalAmendmentRequired = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
			this.m_AllowMultiplePerAccession = true;
            
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(Business.Task.Model.TaskAssignment.Molecular, "Gather materials and send to ARUP for testing.", new Facility.Model.ARUP()));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.ARUP();
            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.ARUP();

            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.ARUP();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.ARUP();
            
            this.m_HasSplitCPTCode = true;

            Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT81291(), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);
            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());
		}
	}
}
