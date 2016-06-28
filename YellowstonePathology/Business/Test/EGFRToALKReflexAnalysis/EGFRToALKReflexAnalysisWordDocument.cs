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
            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\EGFRToALKReflexAnalysis.6.xml";
            this.OpenTemplate();
            this.SetDemographicsV2();
            this.SetReportDistribution();

            YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder egfrToALKReflexAnalysisTestOrder = (YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(124);

            base.ReplaceText("report_title", egfrToALKReflexAnalysisTestOrder.PanelSetName);
            
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder = (YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(60);
            base.ReplaceText("egfr_result", egfrMutationAnalysisTestOrder.Result);
            base.SetXMLNodeParagraphData("egfr_comment", egfrMutationAnalysisTestOrder.Comment);

            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(egfrToALKReflexAnalysisTestOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult egfrMutationAnalysisDetectedResult = new EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult();
            if (egfrMutationAnalysisTestOrder.ResultCode == egfrMutationAnalysisDetectedResult.ResultCode)
            {
                base.ReplaceText("alk_result", "Not Indicated");
                base.ReplaceText("ros1_result", "Not Indicated");
            }

            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(131) == true)
            {
                YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder alkForNSCLCByFISHTestOrder = (YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(131);
                base.ReplaceText("alk_result", alkForNSCLCByFISHTestOrder.Result);
            }            
            else if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(68) == true)
            {
                YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrderReportedSeparately alkForNSCLCByFISHTestOrderReportedSeparately = new YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrderReportedSeparately();
				base.ReplaceText("alk_result", alkForNSCLCByFISHTestOrderReportedSeparately.Result);
            }
            else if (egfrToALKReflexAnalysisTestOrder.QNSForALK == true)
            {
                base.ReplaceText("alk_result", "Quantity not sufficient to perform ALK");
                base.ReplaceText("ros1_result", "Quantity not sufficient to perform ROS1");
            }
            else
            {
                base.ReplaceText("alk_result", "ALK not performed");
            }

            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(204) == true)
            {                
                YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder ros1ByFISHTestOrder = (YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(204);
                base.ReplaceText("ros1_result", ros1ByFISHTestOrder.Result);
            }

            base.SetXMLNodeParagraphData("report_interpretation", egfrToALKReflexAnalysisTestOrder.Interpretation);

			this.ReplaceText("report_date", BaseData.GetShortDateString(egfrToALKReflexAnalysisTestOrder.FinalDate));
			this.ReplaceText("pathologist_signature", egfrToALKReflexAnalysisTestOrder.Signature);

			this.SetXMLNodeParagraphData("report_method", egfrToALKReflexAnalysisTestOrder.Method);
			this.SetXMLNodeParagraphData("report_references", egfrToALKReflexAnalysisTestOrder.References);

            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(egfrToALKReflexAnalysisTestOrder.OrderedOnId);
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(egfrToALKReflexAnalysisTestOrder.OrderedOn, egfrMutationAnalysisTestOrder.OrderedOnId);

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
