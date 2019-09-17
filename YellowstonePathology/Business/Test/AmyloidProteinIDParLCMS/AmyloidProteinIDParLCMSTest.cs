using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.AmyloidProteinIDParLCMS
{
    public class AmyloidProteinIDParLCMSTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public AmyloidProteinIDParLCMSTest()
        {
            this.m_PanelSetId = 257;
            this.m_PanelSetName = "Amyloid Protein ID, Par, LC-MS";
            this.m_Abbreviation = "Amyloid Protein ID, Par, LC-MS";
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
            this.m_ExpectedDuration = TimeSpan.FromDays(6);

            YellowstonePathology.Business.Facility.Model.Facility facility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("MAYO");
            string taskDescription = "Gather materials and send out to Mayo Clinic.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription, facility));

            this.m_TechnicalComponentFacility = facility;
            this.m_TechnicalComponentBillingFacility = facility;

            this.m_ProfessionalComponentFacility = facility;
            this.m_ProfessionalComponentBillingFacility = facility;            

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
        }
    }
}
