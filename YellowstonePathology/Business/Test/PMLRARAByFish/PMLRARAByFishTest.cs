﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.PMLRARAByFish
{
    public class PMLRARAByFishTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public PMLRARAByFishTest()
        {
            this.m_PanelSetId = 79;
            this.m_PanelSetName = "PML/RARA t(15;17) By FISH";
            this.m_CaseType = YellowstonePathology.Business.CaseType.FISH;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = true;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PMLRARAByFish.PMLRARAByFishTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.PMLRARAByFish.PMLRARAByFishWordDocument).AssemblyQualifiedName;


            this.m_AllowMultiplePerAccession = true;

            string taskDescription = "Gather materials (Bone Marrow Aspirate: 1-2 mL sodium heparin tube. EDTA tube is acceptable. " +
                "Peripheral Blood: 2-5 mL sodium heparin tube. EDTA tube is acceptable." +
                "Fresh, Unfixed Tissue: Tissue in RPMI. Fluids: Equal parts RPMI to specimen volume.) and send out to Neo.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription, new Facility.Model.NeogenomicsIrvine()));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();
            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();

            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
        }
    }
}