﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.BCL2t1418ByFISH
{
    public class BCL2t1418ByFISHWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public BCL2t1418ByFISHWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            BCL2t1418ByFISHTestOrder panelSetOrder = (BCL2t1418ByFISHTestOrder)this.m_PanelSetOrder;

            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\BCL2t1418ByFISH.2.xml";
            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            this.ReplaceText("report_result", panelSetOrder.Result);
            this.ReplaceText("report_interpretation", panelSetOrder.Interpretation);
            this.ReplaceText("report_method", panelSetOrder.Method);
            this.ReplaceText("report_reference", panelSetOrder.ReportReferences);
            this.ReplaceText("report_disclaimer", panelSetOrder.ReportDisclaimer);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
            base.ReplaceText("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            this.ReplaceText("report_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.ReferenceLabFinalDate));
            this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.ReferenceLabSignature);

            this.SaveReport();
        }

        public override void Publish()
        {
            base.Publish();
        }
    }
}

