﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.NeoTYPEBreastTumorProfile
{
    public class NeoTYPEBreastTumorProfileTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public NeoTYPEBreastTumorProfileTest()
        {
            this.m_PanelSetId = 299;
            this.m_PanelSetName = "NeoTYPE Breast Tumor Profile (FISH-Molecuar)";
            this.m_Abbreviation = "NTBreastTumor";
            this.m_CaseType = Business.CaseType.Molecular;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = true;
            this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_IsBillable = true;
            this.m_ExpectedDuration = TimeSpan.FromDays(8);

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;
            this.m_AllowMultiplePerAccession = true;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.REFLAB);

            string taskDescription = "Gather materials and send to Neo.";

            YellowstonePathology.Business.Facility.Model.Facility neogenomicsIrvine = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, "Make an H&E slide for the pathologists to circle the tumor and return the block and slide to Flow for send out.", neogenomicsIrvine));
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription, neogenomicsIrvine));            

            this.m_TechnicalComponentFacility = neogenomicsIrvine;
            this.m_ProfessionalComponentFacility = neogenomicsIrvine;

            this.m_TechnicalComponentBillingFacility = neogenomicsIrvine;
            this.m_ProfessionalComponentBillingFacility = neogenomicsIrvine;

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("81479", null), 1);
            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode2 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88377", null), 2);
            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode3 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88360", null), 1);
            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode4 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88342", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode2);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode3);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode4);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());
        }
    }
}