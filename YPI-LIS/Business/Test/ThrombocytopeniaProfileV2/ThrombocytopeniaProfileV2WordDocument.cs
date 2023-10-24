using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.ThrombocytopeniaProfileV2
{
    public class ThrombocytopeniaProfileV2WordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public ThrombocytopeniaProfileV2WordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode)
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {
        }

        public override void Render()
        {
            this.m_TemplateName = @"\\fileserver\Documents\ReportTemplates\XmlTemplates\ThrombocytopeniaProfileV2.xml";
            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

            ThrombocytopeniaProfileV2TestOrder testOrder = (ThrombocytopeniaProfileV2TestOrder)this.m_PanelSetOrder;

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(testOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendment = new YellowstonePathology.Business.Document.AmendmentSection();
            amendment.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testOrder.OrderedOn, testOrder.OrderedOnId);
            this.SetXmlNodeData("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            this.SetXmlNodeData("apa_igg", testOrder.AntiPlateletAntibodyIgG);
            this.SetXmlNodeData("apa_igm", testOrder.AntiPlateletAntibodyIgM);
            this.SetXmlNodeData("apa_igg_reference", testOrder.AntiPlateletAntibodyIgGReference);

            if(string.IsNullOrEmpty(testOrder.ReticulatedPlateletAnalysis) == false)
            {
                this.SetXmlNodeData("r_p_a", testOrder.ReticulatedPlateletAnalysis);
                this.SetXmlNodeData("r_p_a_reference", testOrder.ReticulatedPlateletAnalysisReference);
            }
            else
            {
                this.DeleteRow("r_p_a");
            }
            this.SetXmlNodeData("apa_igm_reference", testOrder.AntiPlateletAntibodyIgMReference);
            this.SetXMLNodeParagraphData("report_interpretation", testOrder.Interpretation);
            this.SetXMLNodeParagraphData("report_method", testOrder.Method);
            this.SetXmlNodeData("asr_comment", testOrder.ASRComment);
            
            this.SaveReport();
        }
    }
}
