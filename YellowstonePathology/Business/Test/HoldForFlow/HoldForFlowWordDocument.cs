using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HoldForFlow
{
    public class HoldForFlowWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public HoldForFlowWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            HoldForFlowTestOrder holdForFlowTestOrder = (HoldForFlowTestOrder)this.m_PanelSetOrder;
            this.m_PanelSetOrder = holdForFlowTestOrder;

            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\HoldForFlow.1.xml";
            base.OpenTemplate();

            base.SetDemographicsV2();

            this.ReplaceText("report_comment", holdForFlowTestOrder.Comment);
            this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);

            string finalDate = YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate) + " - " + YellowstonePathology.Business.BaseData.GetMillitaryTimeString(this.m_PanelSetOrder.FinalTime);
            this.SetXmlNodeData("final_date", finalDate);

            this.SetReportDistribution();
            this.SetCaseHistory();

            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            this.SaveReport();
        }

        public override void Publish()
        {
            base.Publish();
        }
    }
}
