using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HER2AnalysisSummary
{
    class HER2AnalysisSummaryWPHOBXView : YellowstonePathology.Business.HL7View.WPH.WPHOBXView
    {
        public HER2AnalysisSummaryWPHOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{ }

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

            this.AddHeader(document, panelSetOrder, "HER2 Analysis Summary");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Result", document, "F");
            string result = panelSetOrder.Result;
            if (result == null) result = string.Empty;
            if (result.ToUpper() == "NEGATIVE") result += " (see interpretation)";
            this.AddNextObxElement("HER2 Status: " + result, document, "F");

            if (panelSetOrder.Her2Chr17Ratio.HasValue == true)
            {
                this.AddNextObxElement("HER2 by ISH", document, "F");
                this.AddNextObxElement("HER2/Chr17 Ratio = " + panelSetOrder.AverageHer2Chr17Signal, document, "F");
            }
            if (panelSetOrder.AverageHer2NeuSignal.HasValue == true)
            {
                this.AddNextObxElement("Average HER2 Copy Number = " + panelSetOrder.AverageHer2NeuSignal.Value.ToString(), document, "F");
            }
            this.AddNextObxElement("Cells Counted:" + panelSetOrder.CellsCounted.ToString(), document, "F");
            this.AddNextObxElement("HER2 Signals Counted:" + panelSetOrder.TotalHer2SignalsCounted.ToString(), document, "F");
            this.AddNextObxElement("Chr17 Signals Counted:" + panelSetOrder.TotalChr17SignalsCounted.ToString(), document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("HER2 by IHC:" + panelSetOrderHer2AmplificationByIHC.Score, document, "F");
            this.AddNextObxElement("", document, "F");

            if (panelSetOrder.RecountRequired == true)
            {
                this.AddNextObxElement("HER2 By ISH Recount", document, "F");
                this.AddNextObxElement("Cells Counted: " + panelSetOrder.CellsCounted.ToString(), document, "F");
                this.AddNextObxElement("HER2 Signals Counted: " + panelSetOrder.TotalHer2SignalsCounted.ToString(), document, "F");
                this.AddNextObxElement("Chr17 Signals Counted: " + panelSetOrder.TotalChr17SignalsCounted.ToString(), document, "F");
                this.AddNextObxElement("", document, "F");
            }

            if (string.IsNullOrEmpty(panelSetOrder.ResultComment) == false)
            {
                this.HandleLongString("Comment: " + panelSetOrder.ResultComment, document, "F");
                this.AddNextObxElement("", document, "F");
            }

            this.AddNextObxElement("Pathologist: " + panelSetOrder.Signature, document, "F");
            if (panelSetOrder.FinalDate.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrder.FinalDate.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }

            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);
            this.AddNextObxElement("", document, "F");


            this.AddNextObxElement("Result Data", document, "F");
            this.AddNextObxElement("Number of invasive tumor cells counted: " + panelSetOrder.CellsCounted.ToString(), document, "F");
            this.AddNextObxElement("Number of observers: " + panelSetOrder.NumberOfObservers, document, "F");
            if (panelSetOrder.AverageHer2NeuSignal.HasValue == true)
            {
                this.AddNextObxElement("HER2 average copy number per nucleus: " + panelSetOrder.AverageHer2NeuSignal.Value.ToString(), document, "F");
            }
            else
            {
                this.AddNextObxElement("HER2 average copy number per nucleus: Unable to calculate", document, "F");
            }
            this.AddNextObxElement("Chr17 average copy number per nucleus: " + panelSetOrder.AverageChr17Signal, document, "F");
            if (panelSetOrder.Her2Chr17Ratio.HasValue == true)
            {
                this.AddNextObxElement("Ratio of average HER2 / Chr17 signals: " + panelSetOrder.Her2Chr17Ratio.Value.ToString(), document, "F");
            }
            else
            {
                this.AddNextObxElement("Ratio of average HER2 / Chr17 signals: Unable to calculate", document, "F");
            }
            this.AddNextObxElement("", document, "F");

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(panelSetOrder.OrderedOnId);
            string blockDescription = string.Empty;
            if (aliquotOrder != null)
            {
                blockDescription = " - Block " + aliquotOrder.Label;
            }
            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);

            this.AddNextObxElement("Specimen Information", document, "F");
            this.AddNextObxElement("Specimen site and type: " + specimenOrder.Description + blockDescription, document, "F");
            this.AddNextObxElement("Specimen fixation type: " + specimenOrder.LabFixation, document, "F");
            this.AddNextObxElement("Time to fixation: " + specimenOrder.TimeToFixationHourString, document, "F");
            this.AddNextObxElement("Duration of fixation: " + specimenOrder.FixationDurationString, document, "F");
            this.AddNextObxElement("Sample adequacy: " + her2AmplificationByISHTestOrder.SampleAdequacy, document, "F");
            this.AddNextObxElement("Collection Date / Time: " + collectionDateTimeString, document, "F");
            this.AddNextObxElement("", document, "F");

            if (string.IsNullOrEmpty(specimenOrder.FixationComment) == false)
            {
                this.AddNextObxElement("Fixation Comment:", document, "F");
                this.HandleLongString(specimenOrder.FixationComment, document, "F");
                this.AddNextObxElement("", document, "F");
            }

            if (panelSetOrder.InterpretiveComment != null)
            {
                this.AddNextObxElement("Interpretation:", document, "F");
                this.HandleLongString(panelSetOrder.InterpretiveComment, document, "F");
                this.AddNextObxElement("", document, "F");
            }

            this.AddNextObxElement("Reference Ranges:", document, "F");
            this.AddNextObxElement("Positive", document, "F");
            this.AddNextObxElement("HER2/ Chr17 Ratio by ISH ≥2.0", document, "F");
            this.AddNextObxElement("Average HER2 Copy Number Per Cell by ISH ≥4.0", document, "F");
            this.AddNextObxElement("HER2 Result by IHC N/A", document, "F");
            this.AddNextObxElement("or", document, "F");
            this.AddNextObxElement("HER2/ Chr17 Ratio by ISH ≥2.0", document, "F");
            this.AddNextObxElement("Average HER2 Copy Number Per Cell by ISH <4.0", document, "F");
            this.AddNextObxElement("HER2 Result by IHC 3+", document, "F");
            this.AddNextObxElement("or", document, "F");
            this.AddNextObxElement("HER2/ Chr17 Ratio by ISH <2.0", document, "F");
            this.AddNextObxElement("Average HER2 Copy Number Per Cell by ISH ≥4.0 but <6.0", document, "F");
            this.AddNextObxElement("HER2 Result by IHC 3+", document, "F");
            this.AddNextObxElement("or", document, "F");
            this.AddNextObxElement("HER2/ Chr17 Ratio by ISH <2.0", document, "F");
            this.AddNextObxElement("Average HER2 Copy Number Per Cell by ISH ≥6.0", document, "F");
            this.AddNextObxElement("HER2 Result by IHC 2+ or 3+", document, "F");

            this.AddNextObxElement("Negative", document, "F");
            this.AddNextObxElement("HER2/ Chr17 Ratio by ISH <2.0", document, "F");
            this.AddNextObxElement("Average HER2 Copy Number Per Cell by ISH <4.0", document, "F");
            this.AddNextObxElement("HER2 Result by IHC N/A", document, "F");
            this.AddNextObxElement("or", document, "F");
            this.AddNextObxElement("HER2/ Chr17 Ratio by ISH ≥2.0", document, "F");
            this.AddNextObxElement("Average HER2 Copy Number Per Cell by ISH <4.0", document, "F");
            this.AddNextObxElement(" HER2 Result by IHC 0, 1+, or 2+", document, "F");
            this.AddNextObxElement("or", document, "F");
            this.AddNextObxElement("HER2/ Chr17 Ratio by ISH <2.0", document, "F");
            this.AddNextObxElement("Average HER2 Copy Number Per Cell by ISH ≥4.0 but <6.0", document, "F");
            this.AddNextObxElement("HER2 Result by IHC 0, 1+, or 2+", document, "F");
            this.AddNextObxElement("or", document, "F");
            this.AddNextObxElement("HER2/ Chr17 Ratio by ISH <2.0", document, "F");
            this.AddNextObxElement("Average HER2 Copy Number Per Cell by ISH ≥6.0", document, "F");
            this.AddNextObxElement("HER2 Result by IHC 0 or 1+", document, "F");
            this.AddNextObxElement("", document, "F");


            this.AddNextObxElement("References: ", document, "F");
            this.HandleLongString(panelSetOrder.ReportReference, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement(her2AmplificationByISHTestOrder.ASRComment, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
