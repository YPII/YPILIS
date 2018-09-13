using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace YellowstonePathology.Business.Test.Her2AmplificationByFish
{
	public class Her2AmplificationByFishWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public Her2AmplificationByFishWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{			
			PanelSetOrderHer2AmplificationByFish panelSetOrderHer2AmplificationByFish = (PanelSetOrderHer2AmplificationByFish)this.m_PanelSetOrder;

            if (panelSetOrderHer2AmplificationByFish.NonBreast == false)
            {
                this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\HER2AmplificationByFish.1.xml";
            }
            else
            {
                this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\HER2AmplificationByFishNonBreast.1.xml";
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
				this.SetXmlNodeData("test_result", panelSetOrderHer2AmplificationByFish.Result);

                if (string.IsNullOrEmpty(panelSetOrderHer2AmplificationByFish.HER2CEN17SignalRatio) == false)
                {
                    this.SetXmlNodeData("test_ratio", "Ratio = " + panelSetOrderHer2AmplificationByFish.HER2CEN17SignalRatio);                    
                }
                else
                {
                    this.SetXmlNodeData("test_ratio", string.Empty);
                }

				this.SetXmlNodeData("averageher2_copynumber", "Average HER2 Copy Number = " + panelSetOrderHer2AmplificationByFish.AverageHER2SignalsPerNucleus);

				this.SetXmlNodeData("final_date", BaseData.GetShortDateString(this.m_PanelSetOrder.ReferenceLabFinalDate));
				this.SetXmlNodeData("result_comment", panelSetOrderHer2AmplificationByFish.Comment);

				this.SetXmlNodeData("cell_cnt", panelSetOrderHer2AmplificationByFish.NucleiScored);
				this.SetXmlNodeData("avg_her", panelSetOrderHer2AmplificationByFish.AverageHER2SignalsPerNucleus);
				this.SetXmlNodeData("avg_chr", panelSetOrderHer2AmplificationByFish.AverageCEN17SignalsPerNucleus);
				this.SetXmlNodeData("tst_ratio", panelSetOrderHer2AmplificationByFish.HER2CEN17SignalRatio);
				this.SetXmlNodeData("report_reference_ranges", panelSetOrderHer2AmplificationByFish.ReferenceRanges);               

				XmlNode tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='report_interpretation']", this.m_NameSpaceManager);
				this.SetXMLNodeParagraphDataNode(tableNode, "report_interpretation", panelSetOrderHer2AmplificationByFish.Interpretation);
                               
				YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(this.m_PanelSetOrder.OrderedOnId);
				string blockDescription = string.Empty;
				if (aliquotOrder != null)
				{
					blockDescription = " - Block " + aliquotOrder.Label;
				}

				SetXmlNodeData("specimen_type", specimenOrder.Description + blockDescription);
				SetXmlNodeData("specimen_fixation", specimenOrder.LabFixation);
				SetXmlNodeData("time_to_fixation", specimenOrder.TimeToFixationHourString);

				this.SetXmlNodeData("report_reference", panelSetOrderHer2AmplificationByFish.Reference);                
                SetXmlNodeData("duration_of_fixation", specimenOrder.FixationDurationString);				

				if (panelSetOrderHer2AmplificationByFish.Result != "NOT INTERPRETABLE")
				{
					this.SetXmlNodeData("fixation_comment", specimenOrder.FixationComment);
				}
				else
				{
					this.DeleteRow("fixation_comment");
					SetXmlNodeData("avg_her", string.Empty);
					SetXmlNodeData("tst_ratio", string.Empty);
				}

				SetXmlNodeData("date_time_collected", collectionDateTimeString);
				SetXmlNodeData("report_distribution", "No Distribution Selected");

				this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.ReferenceLabSignature);
			}
			else
			{
				this.SetXmlNodeData("result_comment", panelSetOrderHer2AmplificationByFish.Comment);
				this.SetXmlNodeData("final_date", BaseData.GetShortDateString(this.m_PanelSetOrder.ReferenceLabFinalDate));
			}

			this.SaveReport();
		}

		public override void Publish()
		{
			base.Publish();
		}
	}
}
