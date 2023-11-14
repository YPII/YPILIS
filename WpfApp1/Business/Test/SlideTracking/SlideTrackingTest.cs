using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.SlideTracking
{
    public class SlideTrackingTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public SlideTrackingTest()
        {
            this.m_PanelSetId = 290;
            this.m_PanelSetName = "Slide Tracking";
            this.m_Abbreviation = "Slide Tracking";
            this.m_CaseType = Business.CaseType.Technical;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = false;
            this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.None;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterT();
            this.m_Active = true;
            this.m_IsBillable = true;
            this.m_EnforceOrderTarget = false;
            this.m_HasNoOrderTarget = true;
            this.m_IsClientAccessioned = true;
            this.m_ShowResultPageOnOrder = true;
            this.m_AcceptOnFinal = true;
            this.m_MonitorPriority = MonitorPriorityNormal;

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;
            this.m_AllowMultiplePerAccession = false;

            this.m_TechnicalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());
        }
    }
}
