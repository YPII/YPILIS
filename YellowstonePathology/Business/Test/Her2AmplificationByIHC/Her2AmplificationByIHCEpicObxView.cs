using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.Her2AmplificationByIHC
{
	public class Her2AmplificationByIHCEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
	{
		public Her2AmplificationByIHCEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
		}

		public override void ToXml(XElement document)
		{
			PanelSetOrderHer2AmplificationByIHC panelSetOrder = (PanelSetOrderHer2AmplificationByIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            //If the ish is equivocal and the ihc is not equivocal show the summary
            if (this.m_AccessionOrder.PanelSetOrderCollection.DoesPanelSetExist(46) == true)
            {
                HER2AmplificationByISH.HER2AmplificationByISHTestOrder ish = (HER2AmplificationByISH.HER2AmplificationByISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(46);
                if (string.IsNullOrEmpty(ish.Result) == false && ish.Result.ToUpper().Contains("EQUIVOCAL") == true)
                {
                    if (panelSetOrder.Result.ToUpper().Contains("EQUIVOCAL") == false)
                    {
                        this.ToSummaryXml(document);
                        return;
                    }
                }
            }

            this.AddHeader(document, panelSetOrder, "HER2 Amplification by IHC");

			this.AddNextObxElement("", document, "F");
			string result = "Result: " + panelSetOrder.Result;
			this.AddNextObxElement(result, document, "F");
			result = "  Score: " + panelSetOrder.Score;
			this.AddNextObxElement(result, document, "F");
			result = "Percentage of Cells with Uniform Intense Complete Membrane Staining: " + panelSetOrder.IntenseCompleteMembraneStainingPercent;
			this.AddNextObxElement(result, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Pathologist: " + panelSetOrder.ReferenceLabSignature, document, "F");
			if (panelSetOrder.ReferenceLabFinalDate.HasValue == true)
			{
				this.AddNextObxElement("E-signed " + panelSetOrder.ReferenceLabFinalDate.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
			}

			this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Specimen Information:", document, "F");
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
			this.AddNextObxElement("Specimen Identification: " + specimenOrder.Description, document, "F");
			string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Breast Testing Fixative:", document, "F");
			this.HandleLongString(panelSetOrder.BreastTestingFixative, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Interpretation:", document, "F");
			this.HandleLongString(panelSetOrder.Interpretation, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Method:", document, "F");
			this.HandleLongString(panelSetOrder.Method, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("References:", document, "F");
			this.HandleLongString(panelSetOrder.Reference, document, "F");

			this.AddNextObxElement("", document, "F");
			this.HandleLongString(panelSetOrder.ReportDisclaimer, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");
		}

        public void ToSummaryXml(XElement document)
        {
            HER2AmplificationByISH.HER2AmplificationByISHTestOrder ish = (HER2AmplificationByISH.HER2AmplificationByISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(46);
            PanelSetOrderHer2AmplificationByIHC ihc = (PanelSetOrderHer2AmplificationByIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, ihc, "HER2 Analysis Summary");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Result: ", document, "F");
            this.AddNextObxElement("HER2 Status: " + ihc.Result, document, "F");
            this.AddNextObxElement("HER2 by ISH: " + ish.Result, document, "F");

            if (ish.Her2Chr17Ratio.HasValue == true)
            {
                this.AddNextObxElement("HER2/Chr17 Ratio = " + ish.AverageHer2Chr17Signal, document, "F");
            }

            if (ish.AverageHer2NeuSignal.HasValue == true)
            {
                this.AddNextObxElement("Average HER2 Copy Number = " + ish.AverageHer2NeuSignal.Value.ToString(), document, "F");
            }

            this.AddNextObxElement("HER2 by IHC:" + ihc.Score + " (" + ihc.Result + ")", document, "F");

            if (string.IsNullOrEmpty(ish.ResultComment) == false)
            {
                this.HandleLongString("Comment: " + ish.ResultComment, document, "F");
            }
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Pathologist: " + ihc.Signature, document, "F");
            if (ihc.FinalDate.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + BaseData.GetShortDateString(ihc.FinalDate), document, "F");
            }
            this.AddNextObxElement("", document, "F");

            this.AddAmendments(document);
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Result Data", document, "F");
            this.AddNextObxElement("Number of invasive tumor cells counted: " + ish.CellsCounted.ToString(), document, "F");
            this.AddNextObxElement("Number of observers: " + ish.NumberOfObservers.ToString(), document, "F");

            string avgher = "Unable to calculate";
            if (ish.AverageHer2NeuSignal.HasValue == true)
            {
                avgher = ish.AverageHer2NeuSignal.Value.ToString();
            }
            this.AddNextObxElement("HER2 average copy number per nucleus: " + avgher, document, "F");

            this.AddNextObxElement("Chr17 average copy number per nucleus: " + ish.AverageChr17Signal, document, "F");

            string testRatio = "Unable to calculate";
            if (ish.Her2Chr17Ratio.HasValue == true)
            {
                testRatio = ish.Her2Chr17Ratio.Value.ToString();
            }
            this.AddNextObxElement("Ratio of average HER2/ Chr17 signals: " + testRatio, document, "F");
            this.AddNextObxElement("", document, "F");

            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(ihc.OrderedOnId);
            string blockDescription = string.Empty;
            if (aliquotOrder != null)
            {
                blockDescription = " - Block " + aliquotOrder.Label;
            }
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(ihc.OrderedOn, ihc.OrderedOnId);
            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);

            this.AddNextObxElement("Specimen Information", document, "F");
            this.AddNextObxElement("Specimen site and type: " + specimenOrder.Description + blockDescription, document, "F");
            this.AddNextObxElement("Specimen fixation type: " + specimenOrder.LabFixation, document, "F");
            this.AddNextObxElement("Time to fixation: " + specimenOrder.TimeToFixationHourString, document, "F");
            this.AddNextObxElement("Duration of fixation: " + specimenOrder.FixationDurationString, document, "F");
            this.AddNextObxElement("Sample adequacy: " + ish.SampleAdequacy, document, "F");
            this.AddNextObxElement("Collection Date/ Time: " + collectionDateTimeString, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Reference Ranes", document, "F");
            this.AddNextObxElement("Positive:", document, "F");
            this.AddNextObxElement("HER2/Chr17 Ratio by ISH ≥ 2.0 , Average HER2 Copy Number Per Cell by ISH ≥ 4.0 , HER2 Result by IHC NA", document, "F");
            this.AddNextObxElement("HER2/Chr17 Ratio by ISH ≥ 2.0 , Average HER2 Copy Number Per Cell by ISH < 4.0 , HER2 Result by IHC 3+", document, "F");
            this.AddNextObxElement("HER2/Chr17 Ratio by ISH < 2.0 , Average HER2 Copy Number Per Cell by ISH ≥ 4.0 but < 6.0 , HER2 Result by IHC 3+", document, "F");
            this.AddNextObxElement("HER2/Chr17 Ratio by ISH < 2.0 , Average HER2 Copy Number Per Cell by ISH ≥ 6.0 , HER2 Result by IHC 2+ or 3+", document, "F");
            this.AddNextObxElement("Negative:", document, "F");
            this.AddNextObxElement("HER2/Chr17 Ratio by ISH < 2.0 , Average HER2 Copy Number Per Cell by ISH < 4.0 , HER2 Result by IHC NA", document, "F");
            this.AddNextObxElement("HER2/Chr17 Ratio by ISH ≥ 2.0 , Average HER2 Copy Number Per Cell by ISH < 4.0 , HER2 Result by IHC 0, 1+, or 2+", document, "F");
            this.AddNextObxElement("HER2/Chr17 Ratio by ISH < 2.0 , Average HER2 Copy Number Per Cell by ISH ≥ 4.0 but < 6.0 , HER2 Result by IHC 0, 1+, or 2+", document, "F");
            this.AddNextObxElement("HER2/Chr17 Ratio by ISH < 2.0 , Average HER2 Copy Number Per Cell by ISH ≥ 6.0 , HER2 Result by IHC 0 or 1+", document, "F");
            this.AddNextObxElement("", document, "F");

            if (string.IsNullOrEmpty(specimenOrder.FixationComment) == false)
            {
                this.HandleLongString(specimenOrder.FixationComment, document, "F");
                this.AddNextObxElement("", document, "F");
            }

            this.AddNextObxElement("Method:", document, "F");
            this.HandleLongString(ish.Method + Environment.NewLine + Environment.NewLine + ihc.Method, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("References:", document, "F");
            this.HandleLongString(ish.ReportReference + Environment.NewLine + Environment.NewLine + ihc.ReportReferences, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("This test was performed using a US FDA approved DNA probe kit, modified to report results according to ASCO/CAP guidelines, and the modified procedure was validated by Yellowstone Pathology Institute (YPI).  YPI assumes the responsibility for test performance.", document, "F");
            this.AddNextObxElement("", document, "F");

            this.HandleLongString(ihc.GetLocationPerformedComment(), document, "F");
            this.AddNextObxElement("", document, "F");
        }
    }
}
