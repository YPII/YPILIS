﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.AMLNonFavorableRisk
{
    public class AMLNonFavorableRiskWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public AMLNonFavorableRiskWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            this.m_TemplateName = @"\\fileserver\Documents\ReportTemplates\XmlTemplates\AMLNonFavorableRisk.2.xml";
            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            YellowstonePathology.Business.Test.AMLNonFavorableRisk.AMLNonFavorableRiskTestOrder testOrder = (YellowstonePathology.Business.Test.AMLNonFavorableRisk.AMLNonFavorableRiskTestOrder)this.m_PanelSetOrder;

            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(testOrder.OrderedOnId);
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testOrder.OrderedOn, testOrder.OrderedOnId);

            string specimenDescription = specimenOrder.Description;
            if (aliquotOrder != null) specimenDescription += ", Block " + aliquotOrder.Label;
            this.ReplaceText("specimen_description", specimenDescription);

            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            this.SetXMLNodeParagraphData("report_result", testOrder.Result);
            this.SetXMLNodeParagraphData("report_interpretation", testOrder.Interpretation);
            this.SetXMLNodeParagraphData("nuclei_scored", testOrder.NucleiScored);
            this.SetXMLNodeParagraphData("probeset_details", testOrder.ProbeSetDetail);

            this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.ReferenceLabFinalDate));
            this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.ReferenceLabSignature);

            this.ReplaceText("report_disclaimer", testOrder.ReportDisclaimer);

            this.SaveReport();
        }

        public override void Publish()
        {
            base.Publish();
        }
    }
}
