using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Her2AmplificationByIHC
{
	public class Her2AmplificationByIHCWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public Her2AmplificationByIHCWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{			
			PanelSetOrderHer2AmplificationByIHC panelSetOrder = (PanelSetOrderHer2AmplificationByIHC)this.m_PanelSetOrder;

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\Her2AmplificationByIHC.1.xml";
			base.OpenTemplate();

			base.SetDemographicsV2();

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
			base.ReplaceText("specimen_description", specimenOrder.Description);

			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(this.m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			base.ReplaceText("report_result", panelSetOrder.Result);
			base.ReplaceText("report_score", panelSetOrder.Score);
			base.ReplaceText("report_percent", panelSetOrder.IntenseCompleteMembraneStainingPercent);
			base.ReplaceText("report_fixative", panelSetOrder.BreastTestingFixative);
			base.ReplaceText("report_method", panelSetOrder.Method);
			base.ReplaceText("report_interpretation", panelSetOrder.Interpretation);
			base.ReplaceText("report_reference", panelSetOrder.Reference);
			base.ReplaceText("report_disclaimer", panelSetOrder.ReportDisclaimer);

			this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.ReferenceLabFinalDate));
			this.SetXmlNodeData("pathologist_signature", this.m_PanelSetOrder.Signature);

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
