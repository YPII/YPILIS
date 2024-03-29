﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.IRF4F
{
	public class IRF4FTest : YellowstonePathology.Business.PanelSet.Model.PanelSetMolecularTest
	{
        public IRF4FTest()
		{
			this.m_PanelSetId = 241;
            this.m_PanelSetName = "IRF4F";
            this.m_Abbreviation = "IRF4F";
            this.m_CaseType = Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_AttemptOrderTargetLookup = true;
			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;
            this.m_AllowMultiplePerAccession = true;
            this.m_ExpectedDuration = TimeSpan.FromDays(6);

this.m_ImplementedResultTypes.Add(Business.Test.ResultType.REFLAB);

            YellowstonePathology.Business.Facility.Model.Facility facility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("MAYO");
            string taskDescription = "Gather materials and send out to Mayo Clinic.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription, facility));

            this.m_TechnicalComponentFacility = facility;
            this.m_TechnicalComponentBillingFacility = facility;

            this.m_ProfessionalComponentFacility = facility;
            this.m_ProfessionalComponentBillingFacility = facility;

            this.m_HasSplitCPTCode = false;
            //this.m_RequiresAssignment = true;            

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());                        
		}
	}
}
