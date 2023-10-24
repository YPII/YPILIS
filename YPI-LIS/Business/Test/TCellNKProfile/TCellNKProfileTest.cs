using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.TCellNKProfile
{
    public class TCellNKProfileTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public TCellNKProfileTest()
        {
            this.m_PanelSetId = 247;
            this.m_PanelSetName = "T-Cell/NK Profile";
            this.m_CaseType = Business.CaseType.FlowCytometry;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = false;
            this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterF();
            this.m_Active = true;

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.TCellNKProfile.TCellNKProfileTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.TCellNKProfile.TCellNKProfileWordDocument).AssemblyQualifiedName;

            this.m_AllowMultiplePerAccession = true;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WPH);

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode cpt86357 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("86357", null), 1);
            YellowstonePathology.Business.Billing.Model.PanelSetCptCode cpt86359 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("86359", null), 1);
            YellowstonePathology.Business.Billing.Model.PanelSetCptCode cpt86360 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("86360", null), 1);

            this.m_PanelSetCptCodeCollection.Add(cpt86357);
            this.m_PanelSetCptCodeCollection.Add(cpt86359);
            this.m_PanelSetCptCodeCollection.Add(cpt86360);

            string taskDescription = "Perform BAL T-Cell/NK Profile testing.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription));

            this.m_TechnicalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceFLOWYPI());
        }
    }
}
