using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace YellowstonePathology.Business.Test.FetalHemoglobin
{
	public class FetalHemoglobinWordDocument : YellowstonePathology.Business.Document.CaseReport
    {
        string m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\FetalHemoglobin.5.xml";        
		YellowstonePathology.Business.Flow.FlowMarkerPanelList m_PanelList;

        public FetalHemoglobinWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {
            this.m_PanelList = new YellowstonePathology.Business.Flow.FlowMarkerPanelList();
            this.m_PanelList.SetFillCommandByPanelId(8);
            this.m_PanelList.Fill();
        }        

		public override void Render()
        {                        
            base.OpenTemplate(m_TemplateName);

			YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma panelSetOrderLeukemiaLymphoma = (YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma)this.m_PanelSetOrder;

			string finalDate = BaseData.GetShortDateString(panelSetOrderLeukemiaLymphoma.FinalDate) + " - " + BaseData.GetMillitaryTimeString(panelSetOrderLeukemiaLymphoma.FinalTime);
            this.SetXmlNodeData("final_date", finalDate);            

            this.SetXmlNodeData("client_case", this.m_AccessionOrder.PCAN);

			string accessionTime = this.m_AccessionOrder.AccessionTime.Value.ToString("MM/dd/yyyy - HH:mm");
			this.SetXmlNodeData("accession_date", accessionTime);

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();                

			string collectionDate = this.m_AccessionOrder.CollectionDate.Value.ToShortDateString();
            this.SetXmlNodeData("collection_date", collectionDate);            
            
			this.SetXmlNodeData("specimen_type", this.m_AccessionOrder.SpecimenOrderCollection[0].Description);
            
            foreach (YellowstonePathology.Business.Flow.FlowMarkerItem markerItem in panelSetOrderLeukemiaLymphoma.FlowMarkerCollection)
            {
                switch(markerItem.Name.ToUpper())
                {
                    case "HB-F":                        
                        this.SetXmlNodeData("test_result", markerItem.Result);
                        break;                    
                }
            }

			this.SetXmlNodeData("specimen_type", this.m_AccessionOrder.SpecimenOrderCollection[0].Description);
			this.SetXmlNodeData("report_comment", panelSetOrderLeukemiaLymphoma.ReportComment);

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(panelSetOrderLeukemiaLymphoma.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, false);     

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
