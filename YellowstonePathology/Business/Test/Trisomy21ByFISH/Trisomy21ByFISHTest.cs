using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Trisomy21ByFISH
{
	public class Trisomy21ByFISHTest : YellowstonePathology.Business.PanelSet.Model.PanelSetMolecularTest
	{
        public Trisomy21ByFISHTest()
		{
			this.m_PanelSetId = 224;
            this.m_PanelSetName = "Trisomy 21 By FISH";
            this.m_Abbreviation = "+21 By FISH";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_AttemptOrderTargetLookup = true;
			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;
            this.m_AllowMultiplePerAccession = true;
            this.m_ExpectedDuration = new TimeSpan(7, 0, 0, 0);            

            string taskDescription = "Gather materials and send out to Shodair.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription, new Facility.Model.NeogenomicsIrvine()));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.Showdair();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.Showdair();

            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.Showdair();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.Showdair();                       

            this.m_HasSplitCPTCode = false;
            this.m_RequireAssignmentOnOrder = true;            

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());                        
		}
	}
}
