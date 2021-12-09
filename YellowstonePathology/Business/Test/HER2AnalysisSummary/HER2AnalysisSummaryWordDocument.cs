using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace YellowstonePathology.Business.Test.HER2AnalysisSummary
{
    public class HER2AnalysisSummaryWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public HER2AnalysisSummaryWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode)
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            HER2AnalysisSummaryTestOrder her2AmplificationSummaryTestOrder = (HER2AnalysisSummaryTestOrder)this.m_PanelSetOrder;
            HER2AmplificationByISH.HER2AmplificationResultCollection her2AmplificationResultCollection = new HER2AmplificationByISH.HER2AmplificationResultCollection(this.m_AccessionOrder.PanelSetOrderCollection, her2AmplificationSummaryTestOrder);
            HER2AmplificationByISH.HER2AmplificationResult her2AmplificationResult = her2AmplificationResultCollection.FindMatch();
            YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest ishTest = new Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest();
            YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder her2AmplificationByISHTestOrder = null;
            YellowstonePathology.Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCTest ihcTest = new Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCTest();
            YellowstonePathology.Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC panelSetOrderHer2AmplificationByIHC = null;
            YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTest recountTest = new Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTest();
            YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTestOrder her2AmplificationRecountTestOrder = null;
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(ishTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
            {
                her2AmplificationByISHTestOrder = (Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(ishTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true);
            }
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(ihcTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
            {
                panelSetOrderHer2AmplificationByIHC = (Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(ihcTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true);
            }
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(recountTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
            {
                her2AmplificationRecountTestOrder = (Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(recountTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true);
            }

            if (her2AmplificationByISHTestOrder.Indicator.ToUpper() == "BREAST")
            {
                if (this.m_AccessionOrder.AccessionDate >= DateTime.Parse("1/1/2014") == true)
                {
                    this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\Her2AmplificationSummary.Breast.xml";
                }
                else
                {
                    this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\HER2AmplificationByISH.ASCOPre2014.1.xml";
                }
            }
            else if (her2AmplificationByISHTestOrder.Indicator.ToUpper() == "GASTRIC")
            {
                if (this.m_AccessionOrder.AccessionDate >= DateTime.Parse("1/1/2014") == true)
                {
                    this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\Her2AmplificationByISH.Gastric.2.xml";
                }
                else
                {
                    this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\HER2AmplificationByISH.ASCOPre2014.1.xml";
                }
            }

            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

            if (this.m_AccessionOrder.OrderCancelled == false)
            {

                YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(m_PanelSetOrder.ReportNo);
                YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
                amendmentSection.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
                string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);

                this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

                string result = her2AmplificationSummaryTestOrder.Result;
                if (result == null) result = string.Empty;
                if (result.ToUpper() == "NEGATIVE")
                {
                    result += " (see interpretation)";
                }
                this.SetXmlNodeData("test_result", result);

                this.SetXmlNodeData("ihc_score", panelSetOrderHer2AmplificationByIHC.Score);                

                if (her2AmplificationByISHTestOrder.RecountRequired == true)
                {
                    this.SetXmlNodeData("re_cells_counted", her2AmplificationRecountTestOrder.CellsCounted.ToString());
                    this.SetXmlNodeData("re_her2_counted", her2AmplificationRecountTestOrder.Her2SignalsCounted.ToString());
                    this.SetXmlNodeData("re_chr17_counted", her2AmplificationRecountTestOrder.Chr17SignalsCounted.ToString());
                }
                else
                {
                    this.DeleteRow("HER2 By ISH Recount");
                    this.DeleteRow("re_cells_counted");
                    this.DeleteRow("re_her2_counted");
                    this.DeleteRow("re_chr17_counted");
                }

                if (her2AmplificationByISHTestOrder.Her2Chr17Ratio.HasValue == true)
                {
                    this.SetXmlNodeData("test_ratio", "HER2/Chr17 Ratio = " + her2AmplificationByISHTestOrder.AverageHer2Chr17Signal);
                }
                else
                {
                    this.DeleteRow("test_ratio");
                }

                if (her2AmplificationByISHTestOrder.AverageHer2NeuSignal.HasValue == true)
                {
                    this.SetXmlNodeData("copy_number", "Average HER2 Copy Number = " + her2AmplificationByISHTestOrder.AverageHer2NeuSignal.Value.ToString());
                }
                else
                {
                    this.DeleteRow("copy_number");
                }

                this.SetXmlNodeData("cell_cnt", her2AmplificationByISHTestOrder.CellsCounted.ToString());

                if (her2AmplificationByISHTestOrder.AverageHer2NeuSignal.HasValue == true)
                {
                    this.SetXmlNodeData("avg_her", her2AmplificationByISHTestOrder.AverageHer2NeuSignal.Value.ToString());
                }
                else
                {
                    this.SetXmlNodeData("avg_her", "Unable to calculate");
                }

                this.SetXmlNodeData("avg_chr", her2AmplificationByISHTestOrder.AverageChr17Signal);

                if (her2AmplificationByISHTestOrder.Her2Chr17Ratio.HasValue == true)
                {
                    this.SetXmlNodeData("tst_ratio", her2AmplificationByISHTestOrder.Her2Chr17Ratio.Value.ToString());
                }
                else
                {
                    this.SetXmlNodeData("tst_ratio", "Unable to calculate");
                }

                this.SetXmlNodeData("obs_cnt", her2AmplificationByISHTestOrder.NumberOfObservers.ToString());

                this.SetXmlNodeData("final_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));

                if (string.IsNullOrEmpty(her2AmplificationSummaryTestOrder.ResultComment) == false)
                {
                    this.SetXmlNodeData("result_comment", her2AmplificationSummaryTestOrder.ResultComment);
                    this.SetXmlNodeData("comment_up", string.Empty);
                }
                else
                {
                    this.DeleteRow("result_comment");
                    this.DeleteRow("comment_up");
                }

                XmlNode tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='report_interpretation']", this.m_NameSpaceManager);


                if (her2AmplificationSummaryTestOrder.InterpretiveComment != null)
                {
                    this.SetXMLNodeParagraphDataNode(tableNode, "report_interpretation", her2AmplificationSummaryTestOrder.InterpretiveComment);
                }
                else
                {
                    this.SetXMLNodeParagraphDataNode(tableNode, "report_interpretation", string.Empty);
                }

                YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(this.m_PanelSetOrder.OrderedOnId);
                string blockDescription = string.Empty;
                if (aliquotOrder != null)
                {
                    blockDescription = " - Block " + aliquotOrder.Label;
                }

                SetXmlNodeData("specimen_type", specimenOrder.Description + blockDescription);
                SetXmlNodeData("specimen_fixation", specimenOrder.LabFixation);
                SetXmlNodeData("time_to_fixation", specimenOrder.TimeToFixationHourString);

                this.SetXmlNodeData("report_reference", her2AmplificationSummaryTestOrder.ReportReference);
                SetXmlNodeData("duration_of_fixation", specimenOrder.FixationDurationString);

                if (string.IsNullOrEmpty(specimenOrder.FixationComment) == false)
                {
                    this.SetXmlNodeData("fixation_comment", specimenOrder.FixationComment);
                }
                else
                {
                    this.SetXmlNodeData("fixation_comment", string.Empty);
                }

                SetXmlNodeData("report_method", her2AmplificationByISHTestOrder.Method);
                SetXmlNodeData("asr_comment", her2AmplificationByISHTestOrder.ASRComment);
                SetXmlNodeData("sample_adequacy", her2AmplificationByISHTestOrder.SampleAdequacy);
                SetXmlNodeData("date_time_collected", collectionDateTimeString);
                SetXmlNodeData("report_distribution", "No Distribution Selected");

                this.SetXmlNodeData("pathologist_signature", this.m_PanelSetOrder.Signature);
            }
            else
            {
                this.SetXmlNodeData("result_comment", her2AmplificationSummaryTestOrder.ResultComment);
                this.SetXmlNodeData("final_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
            }

            this.SaveReport();
        }

        public override void Publish()
        {
            base.Publish();
        }
    }
}
