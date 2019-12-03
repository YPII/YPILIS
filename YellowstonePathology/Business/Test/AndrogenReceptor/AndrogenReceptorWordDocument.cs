using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.AndrogenReceptor
{
    public class AndrogenReceptorWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public AndrogenReceptorWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        { }

        public override void Render()
        {
            AndrogenReceptorTestOrder testOrder = (AndrogenReceptorTestOrder)this.m_PanelSetOrder;

            if(testOrder.ResultedOnSurgical == true)
            {
                return;
            }

            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\AndrogenReceptor.1.xml";
            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

            this.ReplaceText("report_title", this.m_PanelSetOrder.PanelSetName);

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            this.ReplaceText("report_result", testOrder.Result);

            this.ReplaceText("report_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
            this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);

            this.SaveReport();
        }

        public override void Publish()
        {
            AndrogenReceptorTestOrder testOrder = (AndrogenReceptorTestOrder)this.m_PanelSetOrder;
            if (testOrder.ResultedOnSurgical == false)
            {
                base.Publish();
            }
            else
            {
                Business.OrderIdParser orderIdParser = new OrderIdParser(this.m_PanelSetOrder.ReportNo);
                YellowstonePathology.Business.Helper.FileConversionHelper.ConvertDocumentTo(orderIdParser, Document.CaseDocumentTypeEnum.CaseReport, Document.CaseDocumentFileTypeEnum.xps, Document.CaseDocumentFileTypeEnum.tif);
            }
        }
    }
}
