using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.AuthorizationForVerbalTestRequest
{
    public class AuthorizationForVerbalTestRequestWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public AuthorizationForVerbalTestRequestWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\AuthorizationForVerbalTestRequest.xml";
            base.OpenTemplate();

            PanelSetOrder panelSetOrder = this.m_PanelSetOrder;
            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(363);

            base.SetDemographicsV2();

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, false);

            AuthorizationForVerbalTestRequestTestOrder testOrder = (AuthorizationForVerbalTestRequestTestOrder)this.m_PanelSetOrder;

            if (string.IsNullOrEmpty(testOrder.Comment) == false) base.ReplaceText("report_comment", testOrder.Comment);
            else base.DeleteRow("report_comment");

            this.SetReportDistribution();
            this.SetCaseHistory();

            this.SaveReport();
        }
    }
}
