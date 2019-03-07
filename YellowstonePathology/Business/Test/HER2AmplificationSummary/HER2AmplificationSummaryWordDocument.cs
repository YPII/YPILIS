using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace YellowstonePathology.Business.Test.HER2AmplificationSummary
{
    public class HER2AmplificationSummaryWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public HER2AmplificationSummaryWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode)
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            HER2AmplificationSummaryTestOrder her2AmplificationSummaryTestOrder = (HER2AmplificationSummaryTestOrder)this.m_PanelSetOrder;

            if (her2AmplificationSummaryTestOrder.Indicator.ToUpper() == "BREAST")
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
            else if (her2AmplificationSummaryTestOrder.Indicator.ToUpper() == "GASTRIC")
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

                YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
                amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
                string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);

                this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

                string result = her2AmplificationSummaryTestOrder.Result;
                if (result == null) result = string.Empty;
                if (result.ToUpper() == "NEGATIVE")
                {
                    result += " (see interpretation)";
                }
                this.SetXmlNodeData("test_result", result);

                this.SetXmlNodeData("ihc_score", her2AmplificationSummaryTestOrder.IHCScore);

                this.SetXmlNodeData("ish_cells_counted", her2AmplificationSummaryTestOrder.CellsCounted.ToString());
                this.SetXmlNodeData("ish_her2_counted", her2AmplificationSummaryTestOrder.TotalHer2SignalsCounted.ToString());
                this.SetXmlNodeData("ish_chr17_counted", her2AmplificationSummaryTestOrder.TotalChr17SignalsCounted.ToString());

                if (her2AmplificationSummaryTestOrder.RecountRequired == true)
                {
                    this.SetXmlNodeData("re_cells_counted", her2AmplificationSummaryTestOrder.CellsRecount.ToString());
                    this.SetXmlNodeData("re_her2_counted", her2AmplificationSummaryTestOrder.TotalChr17SignalsRecount.ToString());
                    this.SetXmlNodeData("re_chr17_counted", her2AmplificationSummaryTestOrder.TotalHer2SignalsRecount.ToString());
                }
                else
                {
                    this.DeleteRow("Recount");
                    this.DeleteRow("re_cells_counted");
                    this.DeleteRow("re_her2_counted");
                    this.DeleteRow("re_chr17_counted");
                }

                if (her2AmplificationSummaryTestOrder.Her2Chr17Ratio.HasValue == true)
                {
                    this.SetXmlNodeData("test_ratio", "HER2/Chr17 Ratio = " + her2AmplificationSummaryTestOrder.AverageHer2Chr17Signal);
                }
                else
                {
                    this.DeleteRow("test_ratio");
                }

                if (her2AmplificationSummaryTestOrder.AverageHer2NeuSignal.HasValue == true)
                {
                    this.SetXmlNodeData("copy_number", "Average HER2 Copy Number = " + her2AmplificationSummaryTestOrder.AverageHer2NeuSignal.Value.ToString());
                }
                else
                {
                    this.DeleteRow("copy_number");
                }

                this.SetXmlNodeData("cell_cnt", her2AmplificationSummaryTestOrder.CellsCounted.ToString());

                if (her2AmplificationSummaryTestOrder.AverageHer2NeuSignal.HasValue == true)
                {
                    this.SetXmlNodeData("avg_her", her2AmplificationSummaryTestOrder.AverageHer2NeuSignal.Value.ToString());
                }
                else
                {
                    this.SetXmlNodeData("avg_her", "Unable to calculate");
                }

                this.SetXmlNodeData("avg_chr", her2AmplificationSummaryTestOrder.AverageChr17Signal);

                if (her2AmplificationSummaryTestOrder.Her2Chr17Ratio.HasValue == true)
                {
                    this.SetXmlNodeData("tst_ratio", her2AmplificationSummaryTestOrder.Her2Chr17Ratio.Value.ToString());
                }
                else
                {
                    this.SetXmlNodeData("tst_ratio", "Unable to calculate");
                }

                this.SetXmlNodeData("obs_cnt", her2AmplificationSummaryTestOrder.NumberOfObservers.ToString());

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

                SetXmlNodeData("report_method", her2AmplificationSummaryTestOrder.Method);
                SetXmlNodeData("asr_comment", her2AmplificationSummaryTestOrder.ASRComment);
                SetXmlNodeData("sample_adequacy", her2AmplificationSummaryTestOrder.SampleAdequacy);
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
