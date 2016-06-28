using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace YellowstonePathology.Business.Test.StemCellEnumeration
{
	public class StemCellEnumerationWordDocument : YellowstonePathology.Business.Document.CaseReport
    {        
        string m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\StemCellEnumeration.6.xml";        
		YellowstonePathology.Business.Flow.FlowMarkerPanelList m_PanelList;		

        public StemCellEnumerationWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {
            this.m_PanelList = new YellowstonePathology.Business.Flow.FlowMarkerPanelList();
            this.m_PanelList.SetFillCommandByPanelId(8);
            this.m_PanelList.Fill();
        }

        public override void Render()
        {            
            base.OpenTemplate(m_TemplateName);            

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

			YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma panelSetOrderLeukemiaLymphoma = (YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma)this.m_PanelSetOrder;

			YellowstonePathology.Business.Document.AmendmentSection amendment = new YellowstonePathology.Business.Document.AmendmentSection();
			amendment.SetAmendment(panelSetOrderLeukemiaLymphoma.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrderLeukemiaLymphoma.OrderedOn, panelSetOrderLeukemiaLymphoma.OrderedOnId);
            this.SetXmlNodeData("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			foreach (YellowstonePathology.Business.Flow.FlowMarkerItem markerItem in panelSetOrderLeukemiaLymphoma.FlowMarkerCollection)
			{
                switch(markerItem.Name)
                {
                    case "Stem Cell Enumeration":                        
                        this.SetXmlNodeData("stemcell_result", markerItem.Result);
                        break;
                    case "Viability":
                        this.SetXmlNodeData("viability_result", markerItem.Result);
                        break;
                    case "WBC Count":
                        this.SetXmlNodeData("wbccount_result", markerItem.Result);
                        break;                
                }
            }            
            this.SaveReport();
        }        
    }
}
