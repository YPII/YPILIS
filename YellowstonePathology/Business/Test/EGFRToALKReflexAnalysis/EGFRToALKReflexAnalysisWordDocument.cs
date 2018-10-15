using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis
{
    public class EGFRToALKReflexAnalysisWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public EGFRToALKReflexAnalysisWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {            
            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\EGFRALKROS1PDLBRAF.1.xml";
            this.OpenTemplate();
            this.SetDemographicsV2();
            this.SetReportDistribution();

            YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder egfrToALKReflexAnalysisTestOrder = (YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(124);

            base.ReplaceText("report_title", egfrToALKReflexAnalysisTestOrder.PanelSetName);
            
            if(string.IsNullOrEmpty(egfrToALKReflexAnalysisTestOrder.EGFRMutationAnalysisResult) == false)
            {
                base.ReplaceText("egfr_result", egfrToALKReflexAnalysisTestOrder.EGFRMutationAnalysisResult);
                base.SetXMLNodeParagraphData("egfr_comment", egfrToALKReflexAnalysisTestOrder.EGFRMutationAnalysisComment);
            }
            else
            {
                this.DeleteRow("egfr_result");
                this.DeleteRow("egfr_comment");
            }

            if (string.IsNullOrEmpty(egfrToALKReflexAnalysisTestOrder.ALKForNSCLCByFISHResult) == false)
            {
                base.ReplaceText("alk_result", egfrToALKReflexAnalysisTestOrder.ALKForNSCLCByFISHResult);
            }
            else
            {
                this.DeleteRow("alk_result");
            }

            if (string.IsNullOrEmpty(egfrToALKReflexAnalysisTestOrder.ROS1ByFISHResult) == false)
            {
                base.ReplaceText("ros1_result", egfrToALKReflexAnalysisTestOrder.ROS1ByFISHResult);
            }
            else
            {
                this.DeleteRow("ros1_result");
            }

            if (string.IsNullOrEmpty(egfrToALKReflexAnalysisTestOrder.PDL1SP142StainPercent) == false)
            {
                base.ReplaceText("pdl1sp142_result", egfrToALKReflexAnalysisTestOrder.PDL1SP142StainPercent);
            }
            else
            {
                this.DeleteRow("pdl1sp142_result");
            }

            if (string.IsNullOrEmpty(egfrToALKReflexAnalysisTestOrder.PDL122C3Result) == false)
            {
                base.ReplaceText("pdl122c3_result", egfrToALKReflexAnalysisTestOrder.PDL122C3Result);
            }
            else
            {
                this.DeleteRow("pdl122c3_result");
            }

            if (string.IsNullOrEmpty(egfrToALKReflexAnalysisTestOrder.BRAFMutationAnalysisResult) == false)
            {
                base.ReplaceText("braf_result", egfrToALKReflexAnalysisTestOrder.BRAFMutationAnalysisResult);
            }
            else
            {
                this.DeleteRow("braf_result");
            }

            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(egfrToALKReflexAnalysisTestOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            base.SetXMLNodeParagraphData("report_interpretation", egfrToALKReflexAnalysisTestOrder.Interpretation);

			this.ReplaceText("report_date", BaseData.GetShortDateString(egfrToALKReflexAnalysisTestOrder.FinalDate));
			this.ReplaceText("pathologist_signature", egfrToALKReflexAnalysisTestOrder.Signature);

			this.SetXMLNodeParagraphData("report_method", egfrToALKReflexAnalysisTestOrder.Method);
			this.SetXMLNodeParagraphData("report_references", egfrToALKReflexAnalysisTestOrder.ReportReferences);

            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(egfrToALKReflexAnalysisTestOrder.OrderedOnId);
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(egfrToALKReflexAnalysisTestOrder.OrderedOn, egfrToALKReflexAnalysisTestOrder.OrderedOnId);

            string specimenDescription = specimenOrder.Description + ", Block " + aliquotOrder.Label;
            this.ReplaceText("specimen_description", specimenDescription);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            this.ReplaceText("tumor_nuclei_percentage", egfrToALKReflexAnalysisTestOrder.TumorNucleiPercentage);

            this.SaveReport();
        }
        
        public override void Publish()
        {
            base.Publish();
        }
    }
}
