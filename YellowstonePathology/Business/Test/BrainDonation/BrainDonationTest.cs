using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.BrainDonation
{
    public class BrainDonationTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public BrainDonationTest()
        {
            this.m_PanelSetId = 242;
            this.m_PanelSetName = "Brain Donation";
            this.m_Abbreviation = "Brain Donation";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
            this.m_HasTechnicalComponent = false;
            this.m_HasProfessionalComponent = false;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_IsBillable = false;

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;
            this.m_AllowMultiplePerAccession = false;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.REFLAB);

            YellowstonePathology.Business.Facility.Model.Facility ypi = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentFacility = ypi;
            this.m_TechnicalComponentBillingFacility = ypi;

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());
        }
    }
}
