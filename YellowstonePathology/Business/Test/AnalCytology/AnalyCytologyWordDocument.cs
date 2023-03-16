using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.AnalCytology
{
    public class AnalCytologyWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public AnalCytologyWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        { }

        public override void Render()
        {
            AnalCytologyTestOrder testOrder = (AnalCytologyTestOrder)this.m_PanelSetOrder;            

            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\AnalCytology.1.xml";
            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

            this.ReplaceText("report_title", this.m_PanelSetOrder.PanelSetName);

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            this.ReplaceText("screening_impression", testOrder.ScreeningImpression);
            this.ReplaceText("specimen_adequacy", testOrder.SpecimenAdequacy);
            this.ReplaceText("other_conditions", testOrder.OtherConditions);
            this.ReplaceText("report_comment", testOrder.ReportComment);
            this.ReplaceText("report_method", testOrder.Method);

            this.ReplaceText("report_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
            this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);

            this.SaveReport();
        }

        public override void Publish()
        {
            AnalCytologyTestOrder testOrder = (AnalCytologyTestOrder)this.m_PanelSetOrder;            
            base.Publish();            
        }
    }
}
