using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.StemCellCD34Enumeration
{
    public class StemCellCD34EnumerationTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public StemCellCD34EnumerationTest()
        {
            this.m_PanelSetId = 334;
            this.m_PanelSetName = "Stem Cell CD34 Enumeration";
            this.m_AllowMultiplePerAccession = true;
            this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterF();
            this.m_CaseType = this.m_CaseType = Business.CaseType.FlowCytometry;
            this.m_Active = true;
            this.m_ExpectedDuration = TimeSpan.FromHours(24);
            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.StemCellCD34Enumeration.StemCellCD34EnumerationTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.StemCellCD34Enumeration.StemCellCD34EnumerationWordDocument).AssemblyQualifiedName;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);

            string taskDescription = "Perform stem cell CD34 enumeration testing.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription));

            this.m_HasTechnicalComponent = true;
            this.m_TechnicalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_HasProfessionalComponent = false;
            this.m_ProfessionalComponentFacility = null;

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("86367", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceFLOWYPI());
        }
    }
}
