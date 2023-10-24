using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.GastricAdenocarcinoma
{
    public class GastricAdenocarcinomaTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public GastricAdenocarcinomaTest()
        {
            this.m_PanelSetId = 364;
            this.m_PanelSetName = "Gastric Adenocarcinoma Panel";
            this.m_Abbreviation = "Gastric Adenocarcinoma";
            this.m_CaseType = Business.CaseType.ALLCaseTypes;
            this.m_HasTechnicalComponent = false;
            this.m_HasProfessionalComponent = false;
            this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterQ();
            this.m_Active = false;
            this.m_ExpectedDuration = TimeSpan.FromDays(1);
            this.m_IsBillable = false;
            this.m_NeverDistribute = true;

            this.m_OrderInitialTestsOnly = true;

            this.m_AllowMultiplePerAccession = true;
            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.GastricAdenocarcinoma.GastricAdenocarcinomaTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.DoNotPublishReport).AssemblyQualifiedName;

            this.m_TechnicalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
            this.m_ProfessionalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            /*
            string task1Description = "Cut 2 unstained slides and an after H&E. Give to molecular.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, task1Description));

            string task2Description = "Recieve materials from histology and run test.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, task2Description));

            string task3Description = "Please check to make sure the Fixation is entered correctly for this case.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, task3Description));
            */

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
        }
    }
}
