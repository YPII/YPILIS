using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;

namespace YellowstonePathology.Business.Document
{
	public class HER2AmplificationReport : CaseReportV2
	{
        public HER2AmplificationReport(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{            			
			YellowstonePathology.Business.Test.Retired.PanelSetOrderHer2AmplificationByFishRetired2 panelSetOrderHer2AmplificationByFishRetired2 = (YellowstonePathology.Business.Test.Retired.PanelSetOrderHer2AmplificationByFishRetired2)this.m_PanelSetOrder;
			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\HER2AmplificationByFish.5.xml";

			base.OpenTemplate();

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();			

            if (this.m_AccessionOrder.OrderCancelled == false)
            {
                Document.AmendmentSection amendmentSection = new AmendmentSection();
                amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

				YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);                                
                string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);

                this.SetXmlNodeData("date_time_collected", collectionDateTimeString);
				this.SetXmlNodeData("test_result", panelSetOrderHer2AmplificationByFishRetired2.Result);

				if (panelSetOrderHer2AmplificationByFishRetired2.Her2Chr17Ratio.HasValue == true)
                {
					this.SetXmlNodeData("test_ratio", "Ratio = " + panelSetOrderHer2AmplificationByFishRetired2.Her2Chr17Ratio);
                }
                else
                {
                    this.DeleteRow("test_ratio");
                }

				if (panelSetOrderHer2AmplificationByFishRetired2.AverageHer2NeuSignal.HasValue == true)
                {
					this.SetXmlNodeData("averageher2_copynumber", "Average HER2 Copy Number = " + panelSetOrderHer2AmplificationByFishRetired2.AverageHer2NeuSignal.Value.ToString());
                }
                else
                {
                    this.DeleteRow("averageher2_copynumber");
                }

                this.SetXmlNodeData("final_date", BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));

				if (string.IsNullOrEmpty(panelSetOrderHer2AmplificationByFishRetired2.ResultComment) == false)
                {
					this.SetXmlNodeData("result_comment", panelSetOrderHer2AmplificationByFishRetired2.ResultComment);
                }
                else
                {
                    this.DeleteRow("result_comment");                        
                }

				this.SetXmlNodeData("cell_cnt", panelSetOrderHer2AmplificationByFishRetired2.CellsCounted.ToString());
				this.SetXmlNodeData("obs_cnt", panelSetOrderHer2AmplificationByFishRetired2.NumberOfObservers.ToString());
				if (panelSetOrderHer2AmplificationByFishRetired2.AverageHer2NeuSignal.HasValue == true) this.SetXmlNodeData("avg_her", panelSetOrderHer2AmplificationByFishRetired2.AverageHer2NeuSignal.Value.ToString());
				this.SetXmlNodeData("avg_chr", panelSetOrderHer2AmplificationByFishRetired2.AverageChr17Signal);
				if (panelSetOrderHer2AmplificationByFishRetired2.Her2Chr17Ratio.HasValue == true) this.SetXmlNodeData("tst_ratio", panelSetOrderHer2AmplificationByFishRetired2.Her2Chr17Ratio.Value.ToString());

                XmlNode tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='report_interpretation']", this.m_NameSpaceManager);
				this.SetXMLNodeParagraphDataNode(tableNode, "report_interpretation", panelSetOrderHer2AmplificationByFishRetired2.InterpretiveComment);
                this.SetXmlNodeData("time_of_fixation", specimenOrder.FixationDurationString);
                                
                YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(this.m_PanelSetOrder.OrderedOnId);
                string blockDescription = string.Empty;
                if (aliquotOrder != null)
                {
                    blockDescription = " - Block " + aliquotOrder.Label;
                }

                SetXmlNodeData("specimen_type", specimenOrder.Description + blockDescription);
                SetXmlNodeData("specimen_fixation", specimenOrder.LabFixation);
                SetXmlNodeData("time_to_fixation", specimenOrder.TimeToFixationHourString);

				this.SetXmlNodeData("report_reference", panelSetOrderHer2AmplificationByFishRetired2.ReportReference);
                SetXmlNodeData("duration_of_fixation", specimenOrder.FixationDurationString);				

				if (panelSetOrderHer2AmplificationByFishRetired2.Result != "NOT INTERPRETABLE")
                {
                    this.SetXmlNodeData("fixation_comment", specimenOrder.FixationComment);
                }
                else
                {
                    this.DeleteRow("fixation_comment");
                    SetXmlNodeData("avg_her", string.Empty);
                    SetXmlNodeData("tst_ratio", string.Empty);                    
                }

				SetXmlNodeData("sample_adequacy", panelSetOrderHer2AmplificationByFishRetired2.SampleAdequacy);
                SetXmlNodeData("date_time_collected", collectionDateTimeString);
                SetXmlNodeData("report_distribution", "No Distribution Selected");

				this.SetXmlNodeData("pathologist_signature", this.m_PanelSetOrder.Signature);
            }
            else
            {
				this.SetXmlNodeData("result_comment", panelSetOrderHer2AmplificationByFishRetired2.ResultComment);
                this.SetXmlNodeData("final_date", BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
            }
			
			this.SaveReport();
		}

        public override void Publish()
        {
            base.Publish();
        }        
	}
}
