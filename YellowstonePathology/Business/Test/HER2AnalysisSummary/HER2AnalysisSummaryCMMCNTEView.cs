using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HER2AnalysisSummary
{
    public class HER2AnalysisSummaryCMMCNTEView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
    {
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

        public HER2AnalysisSummaryCMMCNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;
        }

        public override void ToXml(XElement document)
        {
            HER2AnalysisSummaryTestOrder panelSetOrder = (HER2AnalysisSummaryTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            HER2AmplificationByISH.HER2AmplificationResultCollection her2AmplificationResultCollection = new HER2AmplificationByISH.HER2AmplificationResultCollection(this.m_AccessionOrder.PanelSetOrderCollection, panelSetOrder);
            HER2AmplificationByISH.HER2AmplificationResult her2AmplificationResult = her2AmplificationResultCollection.FindSummaryMatch();
            YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest ishTest = new Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest();
            YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder her2AmplificationByISHTestOrder = null;
            YellowstonePathology.Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCTest ihcTest = new Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCTest();
            YellowstonePathology.Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC panelSetOrderHer2AmplificationByIHC = null;
            YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTest recountTest = new Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTest();
            YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTestOrder her2AmplificationRecountTestOrder = null;
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(ishTest.PanelSetId, panelSetOrder.OrderedOnId, true) == true)
            {
                her2AmplificationByISHTestOrder = (Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(ishTest.PanelSetId, panelSetOrder.OrderedOnId, true);
            }
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(ihcTest.PanelSetId, panelSetOrder.OrderedOnId, true) == true)
            {
                panelSetOrderHer2AmplificationByIHC = (Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(ihcTest.PanelSetId, panelSetOrder.OrderedOnId, true);
            }
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(recountTest.PanelSetId, panelSetOrder.OrderedOnId, true) == true)
            {
                her2AmplificationRecountTestOrder = (Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(recountTest.PanelSetId, panelSetOrder.OrderedOnId, true);
            }

            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("HER2 Analysis Summary", document);
            this.AddNextNteElement("Master Accession #: " + panelSetOrder.MasterAccessionNo, document);
            this.AddNextNteElement("Report #: " + panelSetOrder.ReportNo, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Result", document);
            string result = panelSetOrder.Result;
            if (result == null) result = string.Empty;
            if (result.ToUpper() == "NEGATIVE") result += " (see interpretation)";
            this.AddNextNteElement("HER2 Status: " + result, document);

            if (panelSetOrder.Her2Chr17Ratio.HasValue == true)
            {
                this.AddNextNteElement("HER2 by ISH", document);
                this.AddNextNteElement("HER2/Chr17 Ratio = " + panelSetOrder.AverageHer2Chr17Signal, document);
            }
            if (panelSetOrder.AverageHer2NeuSignal.HasValue == true)
            {
                this.AddNextNteElement("Average HER2 Copy Number = " + panelSetOrder.AverageHer2NeuSignal.Value.ToString(), document);
            }
            this.AddNextNteElement("Cells Counted:" + panelSetOrder.CellsCounted.ToString(), document);
            this.AddNextNteElement("HER2 Signals Counted:" + panelSetOrder.TotalHer2SignalsCounted.ToString(), document);
            this.AddNextNteElement("Chr17 Signals Counted:" + panelSetOrder.TotalChr17SignalsCounted.ToString(), document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("HER2 by IHC:" + panelSetOrderHer2AmplificationByIHC.Score, document);
            this.AddBlankNteElement(document);

            if (panelSetOrder.RecountRequired == true)
            {
                this.AddNextNteElement("HER2 By ISH Recount", document);
                this.AddNextNteElement("Cells Counted: " + panelSetOrder.CellsCounted.ToString(), document);
                this.AddNextNteElement("HER2 Signals Counted: " + panelSetOrder.TotalHer2SignalsCounted.ToString(), document);
                this.AddNextNteElement("Chr17 Signals Counted: " + panelSetOrder.TotalChr17SignalsCounted.ToString(), document);
                this.AddBlankNteElement(document);
            }

            if (string.IsNullOrEmpty(panelSetOrder.ResultComment) == false)
            {
                this.HandleLongString("Comment: " + panelSetOrder.ResultComment, document);
                this.AddBlankNteElement(document);
            }

            this.AddNextNteElement("Pathologist: " + panelSetOrder.Signature, document);
            if (panelSetOrder.FinalDate.HasValue == true)
            {
                this.AddNextNteElement("E-signed " + panelSetOrder.FinalDate.Value.ToString("MM/dd/yyyy HH:mm"), document);
            }

            this.AddBlankNteElement(document);
            this.AddAmendments(document, panelSetOrder, this.m_AccessionOrder);
            this.AddBlankNteElement(document);


            this.AddNextNteElement("Result Data", document);
            this.AddNextNteElement("Number of invasive tumor cells counted: " + panelSetOrder.CellsCounted.ToString(), document);
            this.AddNextNteElement("Number of observers: " + panelSetOrder.NumberOfObservers, document);
            if (panelSetOrder.AverageHer2NeuSignal.HasValue == true)
            {
                this.AddNextNteElement("HER2 average copy number per nucleus: " + panelSetOrder.AverageHer2NeuSignal.Value.ToString(), document);
            }
            else
            {
                this.AddNextNteElement("HER2 average copy number per nucleus: Unable to calculate", document);
            }
            this.AddNextNteElement("Chr17 average copy number per nucleus: " + panelSetOrder.AverageChr17Signal, document);
            if (panelSetOrder.Her2Chr17Ratio.HasValue == true)
            {
                this.AddNextNteElement("Ratio of average HER2 / Chr17 signals: " + panelSetOrder.Her2Chr17Ratio.Value.ToString(), document);
            }
            else
            {
                this.AddNextNteElement("Ratio of average HER2 / Chr17 signals: Unable to calculate", document);
            }
            this.AddBlankNteElement(document);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(panelSetOrder.OrderedOnId);
            string blockDescription = string.Empty;
            if (aliquotOrder != null)
            {
                blockDescription = " - Block " + aliquotOrder.Label;
            }
            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);

            this.AddNextNteElement("Specimen Information", document);
            this.AddNextNteElement("Specimen site and type: " + specimenOrder.Description + blockDescription, document);
            this.AddNextNteElement("Specimen fixation type: " + specimenOrder.LabFixation, document);
            this.AddNextNteElement("Time to fixation: " + specimenOrder.TimeToFixationHourString, document);
            this.AddNextNteElement("Duration of fixation: " + specimenOrder.FixationDurationString, document);
            this.AddNextNteElement("Sample adequacy: " + her2AmplificationByISHTestOrder.SampleAdequacy, document);
            this.AddNextNteElement("Collection Date / Time: " + collectionDateTimeString, document);
            this.AddBlankNteElement(document);

            if (string.IsNullOrEmpty(specimenOrder.FixationComment) == false)
            {
                this.AddNextNteElement("Fixation Comment:", document);
                this.HandleLongString(specimenOrder.FixationComment, document);
                this.AddBlankNteElement(document);
            }

            if (panelSetOrder.InterpretiveComment != null)
            {
                this.AddNextNteElement("Interpretation:", document);
                this.HandleLongString(panelSetOrder.InterpretiveComment, document);
                this.AddBlankNteElement(document);
            }

            this.AddNextNteElement("Reference Ranges:", document);
            this.AddNextNteElement("Positive", document);
            this.AddNextNteElement("HER2/ Chr17 Ratio by ISH ≥2.0", document);
            this.AddNextNteElement("Average HER2 Copy Number Per Cell by ISH ≥4.0", document);
            this.AddNextNteElement("HER2 Result by IHC N/A", document);
            this.AddNextNteElement("or", document);
            this.AddNextNteElement("HER2/ Chr17 Ratio by ISH ≥2.0", document);
            this.AddNextNteElement("Average HER2 Copy Number Per Cell by ISH <4.0", document);
            this.AddNextNteElement("HER2 Result by IHC 3+", document);
            this.AddNextNteElement("or", document);
            this.AddNextNteElement("HER2/ Chr17 Ratio by ISH <2.0", document);
            this.AddNextNteElement("Average HER2 Copy Number Per Cell by ISH ≥4.0 but <6.0", document);
            this.AddNextNteElement("HER2 Result by IHC 3+", document);
            this.AddNextNteElement("or", document);
            this.AddNextNteElement("HER2/ Chr17 Ratio by ISH <2.0", document);
            this.AddNextNteElement("Average HER2 Copy Number Per Cell by ISH ≥6.0", document);
            this.AddNextNteElement("HER2 Result by IHC 2+ or 3+", document);

            this.AddNextNteElement("Negative", document);
            this.AddNextNteElement("HER2/ Chr17 Ratio by ISH <2.0", document);
            this.AddNextNteElement("Average HER2 Copy Number Per Cell by ISH <4.0", document);
            this.AddNextNteElement("HER2 Result by IHC N/A", document);
            this.AddNextNteElement("or", document);
            this.AddNextNteElement("HER2/ Chr17 Ratio by ISH ≥2.0", document);
            this.AddNextNteElement("Average HER2 Copy Number Per Cell by ISH <4.0", document);
            this.AddNextNteElement(" HER2 Result by IHC 0, 1+, or 2+", document);
            this.AddNextNteElement("or", document);
            this.AddNextNteElement("HER2/ Chr17 Ratio by ISH <2.0", document);
            this.AddNextNteElement("Average HER2 Copy Number Per Cell by ISH ≥4.0 but <6.0", document);
            this.AddNextNteElement("HER2 Result by IHC 0, 1+, or 2+", document);
            this.AddNextNteElement("or", document);
            this.AddNextNteElement("HER2/ Chr17 Ratio by ISH <2.0", document);
            this.AddNextNteElement("Average HER2 Copy Number Per Cell by ISH ≥6.0", document);
            this.AddNextNteElement("HER2 Result by IHC 0 or 1+", document);
            this.AddBlankNteElement(document);


            this.AddNextNteElement("References: ", document);
            this.HandleLongString(panelSetOrder.ReportReference, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement(her2AmplificationByISHTestOrder.ASRComment, document);
            this.AddBlankNteElement(document);

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextNteElement(locationPerformed, document);
            this.AddBlankNteElement(document);
        }
    }
}
