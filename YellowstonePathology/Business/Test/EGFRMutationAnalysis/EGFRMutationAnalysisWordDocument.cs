using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.EGFRMutationAnalysis
{
	public class EGFRMutationAnalysisWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public EGFRMutationAnalysisWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{                        
			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\EGFRMutationAnalysis.2.xml";
			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

            this.ReplaceText("report_title", this.m_PanelSetOrder.PanelSetName);

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder = (YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder)this.m_PanelSetOrder;

            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(egfrMutationAnalysisTestOrder.OrderedOnId);
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(egfrMutationAnalysisTestOrder.OrderedOn, egfrMutationAnalysisTestOrder.OrderedOnId);

            string specimenDescription = specimenOrder.Description + ", Block " + aliquotOrder.Label;
            this.ReplaceText("specimen_description", specimenDescription);            

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            this.SetXMLNodeParagraphData("report_result", egfrMutationAnalysisTestOrder.Result);
            this.SetXMLNodeParagraphData("report_comment", egfrMutationAnalysisTestOrder.Comment);
            this.SetXMLNodeParagraphData("report_interpretation", egfrMutationAnalysisTestOrder.Interpretation);

            this.ReplaceText("report_indication", egfrMutationAnalysisTestOrder.Indication);
            this.ReplaceText("tumor_nuclei_percentage", egfrMutationAnalysisTestOrder.TumorNucleiPercentage);

            if (egfrMutationAnalysisTestOrder.MicrodissectionPerformed == true)
            {
                this.ReplaceText("microdissection_performed", "Yes");
            }
            else
            {
                this.ReplaceText("microdissection_performed", "No");
            }

            this.SetXMLNodeParagraphData("report_method", egfrMutationAnalysisTestOrder.Method);
            this.SetXMLNodeParagraphData("report_references", egfrMutationAnalysisTestOrder.ReportReferences);
            this.SetXMLNodeParagraphData("report_disclaimer", egfrMutationAnalysisTestOrder.ReportDisclaimer);

            if(string.IsNullOrEmpty(this.m_PanelSetOrder.ReferenceLabSignature) == false)
            {
                this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.ReferenceLabFinalDate));
                this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.ReferenceLabSignature);
            }
            else
            {
                this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
                this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);

            }

			this.SaveReport();
		}

        public override void Publish()
        {
            base.Publish();
        }
	}
}
