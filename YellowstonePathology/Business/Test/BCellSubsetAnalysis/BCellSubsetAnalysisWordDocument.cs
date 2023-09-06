using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.BCellSubsetAnalysis
{
    public class BCellSubsetAnalysisWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public BCellSubsetAnalysisWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            BCellSubsetAnalysisTestOrder testOrder = (BCellSubsetAnalysisTestOrder)this.m_PanelSetOrder;

            this.m_TemplateName = @"\\fileserver\Documents\ReportTemplates\XmlTemplates\BCellSubsetAnalysis.1.xml";
            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            ReferenceRanges referenceRanges = new ReferenceRanges();

            ReferenceRangeValue cellType0 = referenceRanges.Get(0);
            this.ReplaceText("report_mature", cellType0.GetResultString(testOrder.MatureBCellsPlusPercent));

            ReferenceRangeValue cellType1 = referenceRanges.Get(1);
            this.ReplaceText("report_mm", cellType1.GetResultString(testOrder.MatureBCellsMinusPercent));

            ReferenceRangeValue cellType2 = referenceRanges.Get(2);
            this.ReplaceText("report_memory", cellType2.GetResultString(testOrder.MemoryBCellPercent));

            ReferenceRangeValue cellType3 = referenceRanges.Get(3);
            this.ReplaceText("report_nonswitched", cellType3.GetResultString(testOrder.NonSwitchedMemoryBCellPercent));

            ReferenceRangeValue cellType4 = referenceRanges.Get(4);
            this.ReplaceText("report_marginal_zone", cellType4.GetResultString(testOrder.MarginalZoneBCellPercent));

            ReferenceRangeValue cellType5 = referenceRanges.Get(5);
            this.ReplaceText("report_class_switched", cellType5.GetResultString(testOrder.ClassSwitchedMemoryBCellPercent));

            ReferenceRangeValue cellType6 = referenceRanges.Get(6);
            this.ReplaceText("report_naive", cellType6.GetResultString(testOrder.NaiveBCellPercent));

            ReferenceRangeValue cellType7 = referenceRanges.Get(7);
            this.ReplaceText("report_transitional", cellType7.GetResultString(testOrder.TransitionalBCellPercent));

            ReferenceRangeValue cellType8 = referenceRanges.Get(8);
            this.ReplaceText("report_plasmablasts", cellType8.GetResultString(testOrder.PlasmaBlastsPercent));       
                 
            this.ReplaceText("report_total_nucleated", testOrder.TotalNucleatedPercent);
            this.ReplaceText("report_total_lymphocytes", testOrder.TotalLymphocytesPercent);            
            this.ReplaceText("report_method", testOrder.Method);
            this.ReplaceText("report_references", testOrder.ReportReferences);
            this.ReplaceText("asr_comment", testOrder.ASRComment);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
            base.ReplaceText("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            this.ReplaceText("report_date", BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
            this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);

            this.SaveReport();
        }

        public override void Publish()
        {
            base.Publish();
        }
    }
}
