using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.FetalHemoglobin
{
	public class FetalHemoglobinTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
		public FetalHemoglobinTest()
        {
            this.m_PanelSetId = 28;
            this.m_PanelSetName = "Fetal Hemoglobin - Retired";            
			this.m_AllowMultiplePerAccession = true;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterF();
            this.m_CaseType = this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
            this.m_Active = false;
            this.m_ExpectedDuration = TimeSpan.FromHours(24);

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.FetalHemoglobin.FetalHemoglobinTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.FetalHemoglobin.FetalHemoglobinWordDocument).AssemblyQualifiedName;

            string taskDescription = "Perform fetal hemoglobin testing.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription));

            this.m_HasProfessionalComponent = false;
            this.m_ProfessionalComponentFacility = null;

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("86356", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
        }
    }
}
