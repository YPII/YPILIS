using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace YellowstonePathology.Business.Test.FetalHemoglobin
{
	public class FetalHemoglobinWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
		//YellowstonePathology.Business.Flow.FlowMarkerPanelList m_PanelList;

        public FetalHemoglobinWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {
        }        

		public override void Render()
        {                        
            this.m_TemplateName = @"\\fileserver\Documents\ReportTemplates\XmlTemplates\FetalHemoglobin.2.xml";        
            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

            YellowstonePathology.Business.Test.FetalHemoglobin.FetalHemoglobinTestOrder testOrder = (YellowstonePathology.Business.Test.FetalHemoglobin.FetalHemoglobinTestOrder)this.m_PanelSetOrder;

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(testOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendment = new YellowstonePathology.Business.Document.AmendmentSection();
            amendment.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testOrder.OrderedOn, testOrder.OrderedOnId);
            this.SetXmlNodeData("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            this.SetXmlNodeData("test_result", testOrder.HbFResult);
            this.SetXmlNodeData("reference_range", testOrder.ReferenceRange);
            this.SetXmlNodeData("test_sensitivity", "Sensitivity for Hb-F is " + testOrder.HbFResult);
			this.SetXmlNodeData("report_comment", testOrder.ReportComment);
            this.SetXmlNodeData("asr_comment", testOrder.ASRComment);			

            this.SaveReport();
        }
    }
}
