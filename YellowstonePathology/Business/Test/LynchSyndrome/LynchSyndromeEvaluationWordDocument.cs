using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LynchSyndromeEvaluationWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public LynchSyndromeEvaluationWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest lynchSyndromeEvaluationTest = new LynchSyndromeEvaluationTest();

            int molecularTestCount = 0;            
            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\LynchSyndromeEvaluation.7.xml";
            this.OpenTemplate();
            this.SetDemographicsV2();
            this.SetReportDistribution();

            YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation)this.m_PanelSetOrder;
            base.ReplaceText("report_interpretation", panelSetOrderLynchSyndromeEvaluation.Interpretation);
            base.ReplaceText("report_comment", panelSetOrderLynchSyndromeEvaluation.Comment);
            
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrderLynchSyndromeEvaluation.OrderedOn, panelSetOrderLynchSyndromeEvaluation.OrderedOnId);
            this.ReplaceText("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(102, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);
            if (panelSetOrderLynchSyndromeIHC != null)
            {                
                base.ReplaceText("mlh1_result", panelSetOrderLynchSyndromeIHC.MLH1Result);
                base.ReplaceText("msh2_result", panelSetOrderLynchSyndromeIHC.MSH2Result);
                base.ReplaceText("msh6_result", panelSetOrderLynchSyndromeIHC.MSH6Result);
                base.ReplaceText("pms2_result", panelSetOrderLynchSyndromeIHC.PMS2Result);
            }

            if (panelSetOrderLynchSyndromeEvaluation.BRAFIsIndicated == true)
            {
                molecularTestCount += 1;
                YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest brafV600EKTest = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest();
                YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTest rasRAFPanelTest = new YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTest();
                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(brafV600EKTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true) == true)
                {
                    YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder panelSetOrderBraf = (YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafV600EKTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);
					base.ReplaceText("braf_result", panelSetOrderBraf.Result);
                    base.ReplaceText("molecular_analysis_header", "Molecular Analysis");
                }
                else if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(rasRAFPanelTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true) == true)
                {
                    YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder panelSetOrderRASRAF = (YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(rasRAFPanelTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);
                    base.ReplaceText("braf_result", panelSetOrderRASRAF.BRAFResult);
                    base.ReplaceText("molecular_analysis_header", "Molecular Analysis");
                }
            }
            else
            {
                this.DeleteRow("braf_result");                
            }

			YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest panelSetMLH1 = new YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetMLH1.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true) == true)
            {
                molecularTestCount += 1;
				YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis panelSetOrderMLH1MethylationAnalysis = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetMLH1.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);
                base.ReplaceText("mlh1methylation_result", panelSetOrderMLH1MethylationAnalysis.Result);
                base.ReplaceText("molecular_analysis_header", "Molecular Analysis");
            }
            else
            {
                this.DeleteRow("mlh1methylation_result");                
            }

            if (molecularTestCount == 0)
            {
                this.DeleteRow("molecular_analysis_header");
            }

            base.ReplaceText("report_references", panelSetOrderLynchSyndromeEvaluation.References);            
			base.ReplaceText("report_method", panelSetOrderLynchSyndromeEvaluation.Method);
            base.ReplaceText("pathologist_signature", panelSetOrderLynchSyndromeEvaluation.Signature);

            
            base.ReplaceText("summary_location_performed", this.m_AccessionOrder.PanelSetOrderCollection.GetLocationPerformedSummary(lynchSyndromeEvaluationTest.PanelSetIDList));

            this.SaveReport();
        }
        
        public override void Publish()
        {
            base.Publish();
        }        
    }
}
