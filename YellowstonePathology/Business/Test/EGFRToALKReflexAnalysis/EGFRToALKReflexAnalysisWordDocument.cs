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
            
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder = (YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(60);
            if(egfrMutationAnalysisTestOrder != null)
            {
                base.ReplaceText("egfr_result", egfrMutationAnalysisTestOrder.Result);
                base.SetXMLNodeParagraphData("egfr_comment", egfrMutationAnalysisTestOrder.Comment);
            }
            else
            {
                base.ReplaceText("egfr_result", "Not Performed");
                base.SetXMLNodeParagraphData("egfr_comment", string.Empty);
            }  

            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(egfrToALKReflexAnalysisTestOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            //YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult egfrMutationAnalysisDetectedResult = new EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult();

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
            //else if (egfrMutationAnalysisTestOrder.ResultCode == egfrMutationAnalysisDetectedResult.ResultCode)
            else if (egfrMutationAnalysisTestOrder.Result.ToUpper().Contains("POSITIVE"))
            {
                base.ReplaceText("alk_result", "Not Indicated");
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
            //else if (egfrMutationAnalysisTestOrder.ResultCode == egfrMutationAnalysisDetectedResult.ResultCode)
            else if (egfrMutationAnalysisTestOrder.Result.ToUpper().Contains("POSITIVE"))
            {
                base.ReplaceText("ros1_result", "Not Indicated");
            }
            else
            {
                base.ReplaceText("ros1_result", "ROS1 not performed");
            }

            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(215) == true)
            {
                YellowstonePathology.Business.Test.PDL1SP142.PDL1SP142TestOrder pdl1sp142TestOrder = (YellowstonePathology.Business.Test.PDL1SP142.PDL1SP142TestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(215);
                base.ReplaceText("pdl1sp142_result", pdl1sp142TestOrder.StainPercent);
            }
            else
            {
                this.DeleteRow("pdl1sp142_result");
            }

            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(245) == true)
            {
                YellowstonePathology.Business.Test.PDL122C3.PDL122C3TestOrder pdl122C3TestOrder = (YellowstonePathology.Business.Test.PDL122C3.PDL122C3TestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(245);
                base.ReplaceText("pdl122c3_result", pdl122C3TestOrder.Result);
            }
            else
            {
                this.DeleteRow("pdl122c3_result");
            }

            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(274) == true)
            {
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder brafTestOrder = (YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(274);
                base.ReplaceText("braf_result", brafTestOrder.Result);
            }
            else
            {
                this.DeleteRow("braf_result");
            }

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
