using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV
{
	public class HPVWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public HPVWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{            
            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\HPV.3.xml";
            base.OpenTemplate();

			base.SetDemographicsV2();

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, false);

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
            base.ReplaceText("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			HPVTestOrder panelSetOrder = (HPVTestOrder)this.m_PanelSetOrder;
			
			if(string.IsNullOrEmpty(panelSetOrder.Result) == false) base.ReplaceText("test_result", panelSetOrder.Result);            

			if (string.IsNullOrEmpty(panelSetOrder.Comment) == false) base.ReplaceText("report_comment", panelSetOrder.Comment);
			else base.DeleteRow("report_comment");            

			base.ReplaceText("test_information", panelSetOrder.TestInformation);
			base.ReplaceText("report_references", panelSetOrder.ReportReferences);
            base.ReplaceText("asr_comment", panelSetOrder.ASRComment);

            this.SetReportDistribution();
			this.SetCaseHistory();            

			this.SaveReport();
		}

        public override void Publish()
        {
            base.Publish();
        }
	}
}
