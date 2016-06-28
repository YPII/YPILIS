using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandard
{
	public class KRASStandardWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public KRASStandardWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{		
			KRASStandardTestOrder panelSetOrder = (KRASStandardTestOrder)this.m_PanelSetOrder;

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\KRASStandard.1.xml";
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			this.SetXmlNodeData("report_result", panelSetOrder.Result);
			this.SetXmlNodeData("final_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			if (m_PanelSetOrder.AmendmentCollection.Count == 0)
			{
				this.SetXmlNodeData("test_result_header", "Test Result");
			}
			else // If an amendment exists show as corrected
			{
				this.SetXmlNodeData("test_result_header", "Corrected Test Result");
			}

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
			this.ReplaceText("specimen_description", specimenOrder.Description);

			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			if (string.IsNullOrEmpty(panelSetOrder.Comment) == false) this.ReplaceText("result_comment", panelSetOrder.Comment);
			else this.DeleteRow("result_comment");
			this.ReplaceText("report_interpretation", panelSetOrder.Interpretation);
			this.ReplaceText("kras_result_detail", panelSetOrder.MutationDetected);
			this.ReplaceText("report_indication_comment", panelSetOrder.IndicationComment);
			this.ReplaceText("tumor_nuclei_percent", panelSetOrder.TumorNucleiPercentage);
			this.ReplaceText("report_method", panelSetOrder.Method);
			this.ReplaceText("report_reference", panelSetOrder.References);

			if (this.m_PanelSetOrder.ProfessionalComponentFacilityId == "YPBLGS" && this.m_PanelSetOrder.TechnicalComponentFacilityId == "YPIBLGS")
			{
				this.ReplaceText("test_developed_comment", TestDevelopedComment);
			}
			else
			{
				this.DeleteRow("test_developed_comment");
			}

			this.ReplaceText("report_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
			this.SetXmlNodeData("pathologist_signature", m_PanelSetOrder.Signature);
			this.SaveReport();
		}

        public override void Publish()
        {
            base.Publish();
        }        
	}
}
