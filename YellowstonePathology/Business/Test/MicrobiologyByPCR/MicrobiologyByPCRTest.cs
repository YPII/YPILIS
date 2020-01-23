using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.MicrobiologyByPCR
{
    public class MicrobiologyByPCRTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {

        public MicrobiologyByPCRTest()
        {
            this.m_PanelSetId = 296;
            this.m_PanelSetName = "Microbiology by PCR";
            this.m_Abbreviation = "Microbiology by PCR";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = true;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;
            this.m_AllowMultiplePerAccession = true;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.REFLAB);

            YellowstonePathology.Business.Facility.Model.Facility uwFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("UWRLS");
            string taskDescription = "Collect material from Histology and send to University Of Washington Medical Center.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription, uwFacility));

            this.m_TechnicalComponentFacility = uwFacility;
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = uwFacility;
            this.m_ProfessionalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());
        }
    }
}
