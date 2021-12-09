using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace YellowstonePathology.Business.Test.StemCellEnumeration
{
	public class StemCellEnumerationWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {        

        public StemCellEnumerationWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {
        }

        public override void Render()
        {
            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\StemCellEnumeration.1.xml";        
            base.OpenTemplate();            

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

			YellowstonePathology.Business.Test.StemCellEnumeration.StemCellEnumerationTestOrder stemCellEnumerationTestOrder = (StemCellEnumerationTestOrder)this.m_PanelSetOrder;

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(stemCellEnumerationTestOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendment = new YellowstonePathology.Business.Document.AmendmentSection();
			amendment.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(stemCellEnumerationTestOrder.OrderedOn, stemCellEnumerationTestOrder.OrderedOnId);
            this.SetXmlNodeData("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            this.SetXmlNodeData("stemcell_result", stemCellEnumerationTestOrder.StemCellEnumeration);
            this.SetXmlNodeData("viability_result", stemCellEnumerationTestOrder.Viability);
            this.SetXmlNodeData("wbccount_result", stemCellEnumerationTestOrder.WBCCount);
            this.SetXmlNodeData("report_method", stemCellEnumerationTestOrder.Method);

            this.SaveReport();
        }        
    }
}
