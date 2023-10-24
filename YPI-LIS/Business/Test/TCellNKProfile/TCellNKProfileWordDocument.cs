using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using YellowstonePathology.Business.Helper;

namespace YellowstonePathology.Business.Test.TCellNKProfile
{
    public class TCellNKProfileWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public TCellNKProfileWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            TCellNKProfileTestOrder testOrder = (TCellNKProfileTestOrder)this.m_PanelSetOrder;

            this.m_TemplateName = @"\\fileserver\Documents\ReportTemplates\XmlTemplates\TCellNKProfile.3.xml";

            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrder.OrderedOnId);
            this.ReplaceText("specimen_description", specimenOrder.Description);

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            string signature = string.Empty;
            if (this.m_PanelSetOrder.Final == true && this.m_PanelSetOrder.PanelSetId != 66)
            {
                signature = this.m_PanelSetOrder.Signature;
            }
            else if (this.m_PanelSetOrder.Final == false)
            {
                signature = "This Case Is Not Final";
            }
            this.SetXmlNodeData("pathologist_signature", signature);

            string wbcCount = string.Empty;
            if (testOrder.WBC.HasValue == true) wbcCount = testOrder.WBC.Value.ToString();
            this.SetXmlNodeData("wbc_count", wbcCount + "/uL (from client)");
            this.SetXmlNodeData("lymphocyte_percentage", testOrder.LymphocytePercentage.ToString().StringAsPercent());
            this.SetXmlNodeData("tcell_percent", testOrder.CD3TPercent.ToString());
            this.SetXmlNodeData("tcell_count", testOrder.CD3TCount);
            this.SetXmlNodeData("cd4t_percent", testOrder.CD4TPercent.ToString());
            this.SetXmlNodeData("cd4t_count", testOrder.CD4TCount);
            this.SetXmlNodeData("cd8t_percent", testOrder.CD8TPercent.ToString());
            this.SetXmlNodeData("cd8t_count", testOrder.CD8TCount);
            this.SetXmlNodeData("nkcell_percent", testOrder.CD16CD56NKPercent.ToString());
            this.SetXmlNodeData("nkcell_count", testOrder.CD16CD56NKCount);
            this.SetXmlNodeData("cd4cd8_ratio", testOrder.CD4CD8Ratio);

            this.SetXmlNodeData("cd3_percent", testOrder.CD3Percent.ToString().StringAsPercent());
            this.SetXmlNodeData("cd4_percent", testOrder.CD4Percent.ToString().StringAsPercent());
            this.SetXmlNodeData("cd8_percent", testOrder.CD8Percent.ToString().StringAsPercent());
            this.SetXmlNodeData("cd16_percent", testOrder.CD16Percent.ToString().StringAsPercent());
            this.SetXmlNodeData("cd19_percent", testOrder.CD19Percent.ToString().StringAsPercent());
            this.SetXmlNodeData("cd45_percent", testOrder.CD45Percent.ToString().StringAsPercent());
            this.SetXmlNodeData("cd56_percent", testOrder.CD56Percent.ToString().StringAsPercent());

            this.SetXmlNodeData("clinical_history", this.m_AccessionOrder.ClinicalHistory);

            string dateTimeCollected = string.Empty;
            if (specimenOrder.CollectionTime.HasValue == true)
            {
                dateTimeCollected = specimenOrder.CollectionTime.Value.ToString("MM/dd/yyyy HH:mm");
            }
            else if (specimenOrder.CollectionDate.HasValue == true)
            {
                dateTimeCollected = specimenOrder.CollectionDate.Value.ToString("MM/dd/yyyy");
            }

            this.SetXmlNodeData("report_method", testOrder.Method);
            this.SetXmlNodeData("date_time_collected", dateTimeCollected);
            this.SetXmlNodeData("specimen_adequacy", specimenOrder.SpecimenAdequacy);
            this.SetXmlNodeData("report_references", testOrder.ReportReferences);
            this.SetXmlNodeData("asr_comment", testOrder.ASRComment);

            this.SaveReport();
        }

        public override void Publish()
        {
            base.Publish();
        }
    }
}
