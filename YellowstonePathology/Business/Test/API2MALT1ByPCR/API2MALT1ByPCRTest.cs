using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.API2MALT1ByPCR
{
    public class API2MALT1ByPCRTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public API2MALT1ByPCRTest()
        {
            this.m_PanelSetId = 228;
            this.m_PanelSetName = "API2/MALT1 t(11;18) By PCR";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = true;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;


            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.API2MALT1ByPCR.API2MALT1ByPCRTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.API2MALT1ByPCR.API2MALT1ByPCRWordDocument).AssemblyQualifiedName;

            this.m_AllowMultiplePerAccession = true;
            //Changed by MS and TK;
            this.m_EpicDistributionIsImplemented = true;
            this.m_CMMCDistributionIsImplemented = true;

            string taskDescription1 = "Cut H&E slide and give to pathologist to circle tumor for tech only. Give the paraffin block to Flow so they can send to NEO.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, taskDescription1));

            string taskDescription2 = "Collect slide from pathologist and paraffin block from histology, or collect (Peripheral blood: 2-5 mL in sodium heparin tube, 2x5 mL in EDTA tube; " +
            "Bone marrow: 1-2 mL in sodium heparin tube or 2 mL in EDTA tube; Fresh unfixed tissue in RPMI) and send to Neogenomics.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription2, new Facility.Model.NeogenomicsIrvine()));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
        }
    }
}
