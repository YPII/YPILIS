using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace YellowstonePathology.Business.Test.ReticulatedPlateletAnalysis
{
	public class ReticulatedPlateletAnalysisWordDocument : YellowstonePathology.Business.Document.CaseReport
    {        
        string m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\ReticulatedPlateletAnalysis.7.xml";        
		YellowstonePathology.Business.Flow.FlowMarkerPanelList m_PanelList;		

        public ReticulatedPlateletAnalysisWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {
            this.m_PanelList = new YellowstonePathology.Business.Flow.FlowMarkerPanelList();
            this.m_PanelList.SetFillCommandByPanelId(13);
            this.m_PanelList.Fill();
        }

        public override void Render()
        {                        
            base.OpenTemplate(m_TemplateName);

			YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma panelSetOrderLeukemiaLymphoma = (YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma)this.m_PanelSetOrder;

			string finalDate = BaseData.GetShortDateString(panelSetOrderLeukemiaLymphoma.FinalDate) + " - " + BaseData.GetMillitaryTimeString(panelSetOrderLeukemiaLymphoma.FinalTime);
            this.SetXmlNodeData("final_date", finalDate);
			this.SetXmlNodeData("report_comment", panelSetOrderLeukemiaLymphoma.ReportComment);

            this.SetXmlNodeData("client_case", this.m_AccessionOrder.PCAN);

			if (this.m_AccessionOrder.AccessionTime.HasValue == true)
            {
				this.SetXmlNodeData("accession_time", this.m_AccessionOrder.AccessionTime.Value.ToShortTimeString());
            }
            else
            {
                this.SetXmlNodeData("accession_time", string.Empty);
            }

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrderLeukemiaLymphoma.OrderedOn, panelSetOrderLeukemiaLymphoma.OrderedOnId);
            this.SetXmlNodeData("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(panelSetOrderLeukemiaLymphoma.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, false);

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

            string result = string.Empty;
			if (panelSetOrderLeukemiaLymphoma.FlowMarkerCollection.Count == 1)
            {
				result = panelSetOrderLeukemiaLymphoma.FlowMarkerCollection[0].Result;
            }
            this.SetXmlNodeData("test_result", result);
                            
            this.SaveReport();
        }        
    }
}
