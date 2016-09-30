using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MicrosatelliteInstabilityAnalysis
{
	public class MicrosatelliteInstabilityAnalysisWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public MicrosatelliteInstabilityAnalysisWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{			
			MicrosatelliteInstabilityAnalysisTestOrder testOrder = (MicrosatelliteInstabilityAnalysisTestOrder)this.m_PanelSetOrder;

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\MicrosatelliteInstabilityAnalysis.1.xml";
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			this.ReplaceText("report_result", testOrder.Result);
			this.ReplaceText("report_instability_level", testOrder.InstabilityLevel);
			this.ReplaceText("report_bat25_instability", testOrder.BAT25Instability);
			this.ReplaceText("report_bat26_instability", testOrder.BAT26Instability);
			this.ReplaceText("report_d5s346_instability", testOrder.D5S346Instability);
			this.ReplaceText("report_d17s250_instability", testOrder.D17S250Instability);
			this.ReplaceText("report_d2s123_instability", testOrder.D2S123Instability);
			this.ReplaceText("report_interpretation", testOrder.Interpretation);
			this.ReplaceText("report_method", testOrder.Method);
			this.ReplaceText("report_references", testOrder.ReportReferences);
			this.ReplaceText("test_development", testOrder.TestDevelopment);

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
			base.ReplaceText("specimen_description", specimenOrder.Description);

			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			this.ReplaceText("report_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.ReferenceLabFinalDate));
			this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.ReferenceLabSignature);

			this.SaveReport();
		}

		public override void Publish()
		{
			base.Publish();
		}
	}
}
