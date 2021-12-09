using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.Test.TestCancelled
{
	public class TestCancelledWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public TestCancelledWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{
            YellowstonePathology.Business.Test.TestCancelled.TestCancelledTestOrder reportOrderTestCancelled = (YellowstonePathology.Business.Test.TestCancelled.TestCancelledTestOrder)this.m_PanelSetOrder;
            this.m_PanelSetOrder = reportOrderTestCancelled;

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\TestCancelled.1.xml";
			base.OpenTemplate();

			base.SetDemographicsV2();

			string testName = "Test Canceled";
			if (string.IsNullOrEmpty(reportOrderTestCancelled.CancelledTestName) == false)
			{
				testName = reportOrderTestCancelled.CancelledTestName;
			}

			this.ReplaceText("test_canceled_name", testName);
            this.ReplaceText("test_cancelled_comment",  reportOrderTestCancelled.Comment);
            this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrder.OrderedOnId);
            if(specimenOrder != null)
            {
                this.ReplaceText("specimen_description", specimenOrder.Description);
                string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
                this.SetXmlNodeData("date_time_collected", collectionDateTimeString);
            }
            else
            {
                this.ReplaceText("specimen_description", string.Empty);
                this.SetXmlNodeData("date_time_collected", string.Empty);
            }

            string finalDate = Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate) + " - " + YellowstonePathology.Business.BaseData.GetMillitaryTimeString(this.m_PanelSetOrder.FinalTime);
            this.SetXmlNodeData("final_date", finalDate);

            this.SetReportDistribution();
			this.SetCaseHistory();

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            this.SaveReport();
		}

        public override void Publish()
        {
            base.Publish();
        }
	}
}
