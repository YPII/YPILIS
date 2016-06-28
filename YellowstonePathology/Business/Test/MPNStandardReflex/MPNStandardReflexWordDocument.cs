using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.MPNStandardReflex
{
	public class MPNStandardReflexWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public MPNStandardReflexWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{			
			YellowstonePathology.Business.Test.MPNStandardReflex.MPNStandardReflexResult mpnStandardReflexResult = new Test.MPNStandardReflex.MPNStandardReflexResult(this.m_AccessionOrder);

			YellowstonePathology.Business.Test.MPNStandardReflex.MPNStandardReflexTest mpnStandardReflex = new YellowstonePathology.Business.Test.MPNStandardReflex.MPNStandardReflexTest();
			YellowstonePathology.Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex panelSetOrderMPNStandardReflex = (YellowstonePathology.Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(mpnStandardReflex.PanelSetId);

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\MPNStandardReflex.2.xml";
			base.OpenTemplate();            

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            this.ReplaceText("panelset_name", mpnStandardReflexResult.PanelSetOrderMPNStandardReflex.PanelSetName);
            this.ReplaceText("jak2v617f_result", mpnStandardReflexResult.JAK2V617FResult);
            this.ReplaceText("jak2exon1214_result", mpnStandardReflexResult.JAK2Exon1214Result);
            base.ReplaceText("specimen_description", mpnStandardReflexResult.SpecimenOrder.Description);
            this.ReplaceText("result_comment", panelSetOrderMPNStandardReflex.Comment);            

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(mpnStandardReflexResult.SpecimenOrder.CollectionDate, mpnStandardReflexResult.SpecimenOrder.CollectionTime);
			this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            this.ReplaceText("report_interpretation", panelSetOrderMPNStandardReflex.Interpretation);
            this.ReplaceText("report_reference", panelSetOrderMPNStandardReflex.References);
            this.ReplaceText("report_method", panelSetOrderMPNStandardReflex.Method);

			this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
            this.ReplaceText("pathologist_signature", panelSetOrderMPNStandardReflex.Signature);
            this.ReplaceText("global_location_performed", this.m_AccessionOrder.GetLocationPerformedComment());

			this.SaveReport();
		}

		public override void Publish()
		{
			base.Publish();
		}
	}
}
