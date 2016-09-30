using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.Test.ChromosomeAnalysis
{
	public class ChromosomeAnalysisWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public ChromosomeAnalysisWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{			
			ChromosomeAnalysisTestOrder panelSetOrderChromosomeAnalysis = (ChromosomeAnalysisTestOrder)this.m_PanelSetOrder;

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\ChromosomeAnalysis.1.xml";
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			this.ReplaceText("report_result", panelSetOrderChromosomeAnalysis.Result);
			this.ReplaceText("report_karyotype", panelSetOrderChromosomeAnalysis.Karyotype);
			this.ReplaceText("report_interpretation", panelSetOrderChromosomeAnalysis.Interpretation);
			this.ReplaceText("report_comment", panelSetOrderChromosomeAnalysis.Comment);
			this.ReplaceText("metaphases_counted", panelSetOrderChromosomeAnalysis.MetaphasesCounted);
			this.ReplaceText("metaphases_analyzed", panelSetOrderChromosomeAnalysis.MetaphasesAnalyzed);
			this.ReplaceText("metaphases_karyotyped", panelSetOrderChromosomeAnalysis.MetaphasesKaryotyped);
			this.ReplaceText("culture_type", panelSetOrderChromosomeAnalysis.CultureType);
			this.ReplaceText("banding_technique", panelSetOrderChromosomeAnalysis.BandingTechnique);
			this.ReplaceText("banding_resolution", panelSetOrderChromosomeAnalysis.BandingResolution);

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
