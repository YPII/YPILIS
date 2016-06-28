using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace YellowstonePathology.Business.Test.PlateletAssociatedAntibodies
{
	public class PlateletAssociatedAntibodiesWordDocument : YellowstonePathology.Business.Document.CaseReport
    {        
        string m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\PlateletAssociatedAntibodies.6.xml";        
		YellowstonePathology.Business.Flow.FlowMarkerPanelList m_PanelList;

        public PlateletAssociatedAntibodiesWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {
            this.m_PanelList = new YellowstonePathology.Business.Flow.FlowMarkerPanelList();
            this.m_PanelList.SetFillCommandByPanelId(8);
            this.m_PanelList.Fill();
        }        

		public override void Render()
        {            			
			YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma panelSetOrderLeukemiaLymphoma = (YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma)this.m_PanelSetOrder;

            base.OpenTemplate(m_TemplateName);

			string finalDate = string.Empty;
			if (panelSetOrderLeukemiaLymphoma.FinalTime.HasValue)
			{
				finalDate = YellowstonePathology.Business.Document.CaseReportV2.ReportDateTimeFormat(panelSetOrderLeukemiaLymphoma.FinalTime.Value);
			}
            this.SetXmlNodeData("final_date", finalDate);

            this.SetXmlNodeData("client_case", this.m_AccessionOrder.PCAN);

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrderLeukemiaLymphoma.OrderedOn, panelSetOrderLeukemiaLymphoma.OrderedOnId);
            this.SetXmlNodeData("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            if (panelSetOrderLeukemiaLymphoma.AmendmentCollection.Count > 0)
            {
                string amendmentTitle = panelSetOrderLeukemiaLymphoma.AmendmentCollection[0].AmendmentType;
                if (amendmentTitle == "Correction") amendmentTitle = "Corrected Report";
                this.SetXmlNodeData("Amendment", amendmentTitle);
            }
            else
            {
                this.SetXmlNodeData("Amendment", "");
            }

            //AmendmentChange
			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(panelSetOrderLeukemiaLymphoma.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, false);

            XmlNode tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='marker_name']", this.m_NameSpaceManager);
            XmlNode rowMarkerNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='marker_name']", this.m_NameSpaceManager);
            XmlNode insertAfterRow = rowMarkerNode;

            foreach (YellowstonePathology.Business.Flow.FlowMarkerItem markerItem in panelSetOrderLeukemiaLymphoma.FlowMarkerCollection)
            {
                XmlNode rowMarkerNodeClone = rowMarkerNode.Clone();

                rowMarkerNodeClone.SelectSingleNode("descendant::w:r[w:t='marker_name']/w:t", this.m_NameSpaceManager).InnerText = markerItem.Name;
                rowMarkerNodeClone.SelectSingleNode("descendant::w:r[w:t='marker_result']/w:t", this.m_NameSpaceManager).InnerText = markerItem.Result;

                foreach (YellowstonePathology.Business.Flow.FlowMarkerPanelListItem panelItem in this.m_PanelList)
                {
                    if (panelItem.MarkerName == markerItem.Name)
                    {
                        rowMarkerNodeClone.SelectSingleNode("descendant::w:r[w:t='marker_reference']/w:t", this.m_NameSpaceManager).InnerText = panelItem.Reference;
                    }
                }

                tableNode.InsertAfter(rowMarkerNodeClone, insertAfterRow);
                insertAfterRow = rowMarkerNodeClone;
            }

            tableNode.RemoveChild(rowMarkerNode);            
                    
            this.SaveReport();
        }

        public override void Publish()
        {
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
			YellowstonePathology.Business.Document.CaseDocument.SaveXMLAsPDF(orderIdParser);
            YellowstonePathology.Business.Helper.FileConversionHelper.SaveXpsReportToTiff(this.m_PanelSetOrder.ReportNo);
        }
    }
}
