using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace YellowstonePathology.Business.Test.NGCT
{
	public class NGCTWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public NGCTWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{            
            YellowstonePathology.Business.Test.NGCT.NGCTTestOrder testOrder = (YellowstonePathology.Business.Test.NGCT.NGCTTestOrder)this.m_PanelSetOrder;

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\NGCT.7.xml";
			base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(this.m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, false);

			this.SetXmlNodeData("final_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
            base.ReplaceText("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			base.ReplaceText("neisseria_gonorrhoeae_result", testOrder.NeisseriaGonorrhoeaeResult);
			base.ReplaceText("chlamydia_trachomatis_result", testOrder.ChlamydiaTrachomatisResult);

			if (string.IsNullOrEmpty(testOrder.Comment) == false)
			{
				base.ReplaceText("report_comment", testOrder.Comment);
			}                            
			else
			{
				base.DeleteRow("report_comment");
			}

			base.ReplaceText("report_method", testOrder.Method);
			base.ReplaceText("report_references", testOrder.ReportReferences);
			base.ReplaceText("test_information", testOrder.TestInformation);
				
            this.SaveReport();
        }

        public override void Publish()
        {
            base.Publish();
        }
    }
}
