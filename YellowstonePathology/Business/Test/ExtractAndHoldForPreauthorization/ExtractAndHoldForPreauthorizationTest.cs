using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.ExtractAndHoldForPreauthorization
{
    public class ExtractAndHoldForPreauthorizationTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public ExtractAndHoldForPreauthorizationTest()
        {
            this.m_PanelSetId = 300;
            this.m_PanelSetName = "Extract And Hold For Preauthorization";
            this.m_Abbreviation = "Extract And Hold For Preauthorization";
            this.m_CaseType = YellowstonePathology.Business.CaseType.FlowCytometry;
            this.m_HasTechnicalComponent = false;
            this.m_HasProfessionalComponent = false;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.None;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterT();
            this.m_Active = true;
            this.m_NeverDistribute = true;
            this.m_ExpectedDuration = TimeSpan.FromDays(6);
            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.ExtractAndHoldForPreauthorization.ExtractAndHoldForPreauthorizationTestOrder).AssemblyQualifiedName;
            this.m_RequiresPathologistSignature = false;
            this.m_AcceptOnFinal = false;
            this.m_AllowMultiplePerAccession = true;
            this.m_ShowResultPageOnOrder = true;
            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
            this.m_MonitorPriority = MonitorPriorityNormal;

            YellowstonePathology.Business.Facility.Model.Facility ypi = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentFacility = ypi;
            this.m_TechnicalComponentBillingFacility = ypi;            
        }
    }
}
