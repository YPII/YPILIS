using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;

namespace YellowstonePathology.Business.Test.Autopsy
{
	public class AutopsyWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public AutopsyWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{			
			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\FinalAutopsyReport.xml";

			base.OpenTemplate();

			this.SetXmlNodeData("case_number", this.m_PanelSetOrder.ReportNo);
			this.SetXmlNodeData("formatted_name", this.m_AccessionOrder.PatientDisplayName);

			string birthdate = string.Empty;
			if (this.m_AccessionOrder.PBirthdate.HasValue)
			{
				birthdate = this.m_AccessionOrder.PBirthdate.Value.ToShortDateString();
			}
			this.SetXmlNodeData("birth_date", birthdate);

			string mrn = string.Empty;
			if (this.m_AccessionOrder.SvhMedicalRecord != null)
			{
				mrn = this.m_AccessionOrder.SvhMedicalRecord;
			}
			this.SetXmlNodeData("medical_record_no", mrn);
			this.SetXmlNodeData("patient_gender", this.m_AccessionOrder.PSex);
			this.SetXmlNodeData("patient_race", this.m_AccessionOrder.PRace);

			//string dateOfDeath = string.Empty;
			//if (autopsyResult.DateTimeOfDeath.HasValue)
			//{
			//	dateOfDeath = autopsyResult.DateTimeOfDeath.Value.ToShortDateString() + " " + autopsyResult.DateTimeOfDeath.Value.ToShortTimeString();
			//}
			//this.SetXmlNodeData("datetime_of_death", dateOfDeath);
			//this.SetXmlNodeData("place_of_death", autopsyResult.PlaceOfDeath);
			this.SetXmlNodeData("autopsy_datetime", this.m_AccessionOrder.AccessionDate.Value.ToShortDateString() + " " + this.m_AccessionOrder.AccessionTime.Value.ToShortTimeString());
			//this.SetXmlNodeData("autopsy_location", autopsyResult.AutopsyLocation);
			this.SetXmlNodeData("requesting_provider", this.m_AccessionOrder.PhysicianName);

			this.SetXmlNodeData("autopsy_pathologist", this.m_PanelSetOrder.Signature);
			//this.SetXmlNodeData("autopsy_assistants", autopsyResult.AutopsyAssistants);
			//this.SetXmlNodeData("examination_limits", autopsyResult.LimitsOfExamination);
			//this.SetXmlNodeData("autopsy_diagnosis", autopsyResult.Diagnosis);
			//this.SetXmlNodeData("cause_of_death", autopsyResult.CauseOfDeath);
			//this.SetXmlNodeData("manner_of_death", autopsyResult.MannerOfDeath);
			//this.SetXmlNodeData("clinical_history", autopsyResult.ClinicalHistory);
			//this.SetXmlNodeData("gross_examination", autopsyResult.GrossExamination);
			//this.SetXmlNodeData("external_examination", autopsyResult.ExternalExamination);
			//this.SetXmlNodeData("internal_examination", autopsyResult.InternalExamination);
			//this.SetXmlNodeData("cassette_summary", autopsyResult.CassetteSummary);
			//this.SetXmlNodeData("microscopic_examination", autopsyResult.MicroscopicExamination);
			//this.SetXmlNodeData("ancillary_tests", autopsyResult.AncillaryTests);
			//this.SetXmlNodeData("summary_comment", autopsyResult.SummaryComment);

			if (this.m_PanelSetOrder.Final)
			{
				this.SetXmlNodeData("pathologist_signature", this.m_PanelSetOrder.Signature);
				this.SetXmlNodeData("report_date", this.m_PanelSetOrder.FinalDate.Value.ToShortDateString());
				this.SetXmlNodeData("report_time", this.m_PanelSetOrder.FinalTime.Value.ToShortTimeString());
			}

			this.SetReportDistribution();

			this.SaveReport();
		}

        public override void Publish()
        {
            base.Publish();
        }
	}
}
