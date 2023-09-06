using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace YellowstonePathology.Business.Test.StemCellCD34Enumeration
{
    public class StemCellCD34EnumerationWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {

        public StemCellCD34EnumerationWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode)
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {
        }

        public override void Render()
        {
            this.m_TemplateName = @"\\fileserver\Documents\ReportTemplates\XmlTemplates\StemCellCD34Enumeration.1.xml";
            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

            YellowstonePathology.Business.Test.StemCellCD34Enumeration.StemCellCD34EnumerationTestOrder stemCellCD34EnumerationTestOrder = (StemCellCD34EnumerationTestOrder)this.m_PanelSetOrder;

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(stemCellCD34EnumerationTestOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendment = new YellowstonePathology.Business.Document.AmendmentSection();
            amendment.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(stemCellCD34EnumerationTestOrder.OrderedOn, stemCellCD34EnumerationTestOrder.OrderedOnId);
            this.SetXmlNodeData("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            this.SetXmlNodeData("cd34_percentage", stemCellCD34EnumerationTestOrder.CD34Percentage);
            this.SetXmlNodeData("cd34_absolute", stemCellCD34EnumerationTestOrder.CD34Absolute);
            this.SetXmlNodeData("wbccount_result", stemCellCD34EnumerationTestOrder.WBCCount);
            this.SetXmlNodeData("wbc_absolute", stemCellCD34EnumerationTestOrder.WBCAbsolute);
            this.SetXmlNodeData("cd34_viability", stemCellCD34EnumerationTestOrder.CD34Viability);
            this.SetXmlNodeData("report_method", stemCellCD34EnumerationTestOrder.Method);

            this.SaveReport();
        }
    }
}
