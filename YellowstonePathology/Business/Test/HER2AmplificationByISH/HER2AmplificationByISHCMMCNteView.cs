using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
	public class HER2AmplificationByISHCMMCNteView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
	{
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

		public HER2AmplificationByISHCMMCNteView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;            
		}

        public override void ToXml(XElement document)
        {
            HER2AmplificationByISHTestOrder panelSetOrder = (HER2AmplificationByISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("HER2 Gene Amplification", document);
            this.AddNextNteElement("Master Accession #: " + panelSetOrder.MasterAccessionNo, document);
            this.AddNextNteElement("Report #: " + panelSetOrder.ReportNo, document);
            this.AddBlankNteElement(document);

            if (this.m_AccessionOrder.AccessionDate < DateTime.Parse("1/1/2014") == true)
            {
                this.ASCOPre2014ToXml(document, panelSetOrder);
            }
            else if (panelSetOrder.Indicator == "Breast")
            {
                this.BreastToXml(document, panelSetOrder);
            }
            else if (panelSetOrder.Indicator == "Gastric")
            {
                this.GastricToXml(document, panelSetOrder);
            }
        }

        private void BreastToXml(XElement document, HER2AmplificationByISHTestOrder panelSetOrder)
        {
            string referenceRange = "Based on 2013 CAP/ASCO guidelines, a case is considered POSITIVE when the HER2 to Chr17 ratio is >=2.0 with any average " +
                "HER2 copy number or when the HER2 to Chr17 ratio is <2.0 with an average HER2 copy number >=6.0 signals/nucleus, " +
                "EQUIVOCAL when the HER2 to Chr17 ratio is <2.0 with an average HER2 copy number >=4.0 and <6.0 signals/cell, and " +
                "NEGATIVE when the HER2 to Chr17 ratio is <2.0 with an average HER2 copy number < 4.0 signals/cell.";

            this.AddNextNteElement("HER2: " + panelSetOrder.Result, document);
            this.AddNextNteElement("Ratio: " + panelSetOrder.AverageHer2Chr17Signal, document);
            this.AddNextNteElement("Average HER2 Copy Number = " + panelSetOrder.AverageHer2NeuSignal.Value.ToString(), document);
            this.AddBlankNteElement(document);

            if (panelSetOrder.ResultComment != string.Empty)
            {
                this.AddNextNteElement("Comment: " + panelSetOrder.ResultComment, document);
                this.AddBlankNteElement(document);
            }

            this.AddNextNteElement("Pathologist: " + panelSetOrder.Signature, document);
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextNteElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document);
            }
            this.AddBlankNteElement(document);
            this.AddAmendments(document, panelSetOrder);

            this.AddNextNteElement("Number of invasive tumor cells counted: " + panelSetOrder.CellsCounted.ToString(), document);
            this.AddNextNteElement("Number of observers: " + panelSetOrder.NumberOfObservers.ToString(), document);
            if (panelSetOrder.AverageHer2NeuSignal.HasValue == true)
            {
                this.AddNextNteElement("HER2 average copy number per nucleus: " + panelSetOrder.AverageHer2NeuSignal, document);
            }
            this.AddNextNteElement("Chr17 average copy number per nucleus: " + panelSetOrder.AverageChr17Signal, document);
            this.AddNextNteElement("Ratio of average HER2/Chr17 signals: " + panelSetOrder.AverageHer2Chr17Signal, document);
            this.AddBlankNteElement(document);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = (YellowstonePathology.Business.Test.AliquotOrder)this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(panelSetOrder.OrderedOnId);
            string blockDescription = string.Empty;
            if (aliquotOrder != null)
            {
                blockDescription = " - Block " + aliquotOrder.Label;
            }

            this.AddNextNteElement("Specimen site and type: " + specimenOrder.Description + blockDescription, document);
            this.AddNextNteElement("Specimen fixation type: " + specimenOrder.LabFixation, document);
            this.AddNextNteElement("Time to fixation: " + specimenOrder.TimeToFixationHourString, document);
            this.AddNextNteElement("Duration of fixation: " + specimenOrder.FixationDurationString, document);
            this.AddNextNteElement("Sample adequacy: " + panelSetOrder.SampleAdequacy, document);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextNteElement("Collection Date/Time: " + collectionDateTimeString, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Interpretation: ", document);
            this.HandleLongString(panelSetOrder.InterpretiveComment, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Reference Range: " + referenceRange, document);
            this.AddBlankNteElement(document);

            if (string.IsNullOrEmpty(specimenOrder.FixationComment) == false)
            {
                this.HandleLongString("Fixation Comment:" + specimenOrder.FixationComment, document);
                this.AddBlankNteElement(document);
            }

            this.AddNextNteElement("Method: ", document);
            string method = panelSetOrder.Method;
            if (string.IsNullOrEmpty(method) == true)
            {
                method = System.Security.SecurityElement.Escape("This test was performed using a molecular method, In Situ Hybridization (ISH) with the US FDA approved Inform HER2 DNA probe kit, modified to report results according to ASCO/CAP guidelines. The test was performed on paraffin embedded tissue in compliance with ASCO/CAP guidelines.  Probes used include a locus specific probe for HER2 and an internal hybridization control probe for the centromeric region of chromosome 17 (Chr17).");
            }
            this.AddNextNteElement(method, document);
            this.AddBlankNteElement(document);            

            this.AddNextNteElement("References: ", document);
            this.AddNextNteElement(panelSetOrder.ReportReference, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement(panelSetOrder.ASRComment, document);
            this.AddBlankNteElement(document);

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextNteElement(locationPerformed, document);
            this.AddBlankNteElement(document);
        }

        private void GastricToXml(XElement document, HER2AmplificationByISHTestOrder panelSetOrder)
        {
            this.AddNextNteElement("HER2: " + panelSetOrder.Result, document);
            this.AddNextNteElement("Ratio: " + panelSetOrder.AverageHer2Chr17Signal, document);
            this.AddNextNteElement("Reference Range: Negative < 2, Positive >= 2", document);

            if (panelSetOrder.ResultComment != string.Empty)
            {
                this.AddNextNteElement("Comment: " + panelSetOrder.ResultComment, document);
                this.AddBlankNteElement(document);
            }

            this.AddNextNteElement("Pathologist: " + panelSetOrder.Signature, document);
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextNteElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document);
            }
            this.AddBlankNteElement(document);
            this.AddAmendments(document, panelSetOrder);

            this.AddNextNteElement("Number of invasive tumor cells counted: " + panelSetOrder.CellsCounted.ToString(), document);
            this.AddNextNteElement("Number of observers: " + panelSetOrder.NumberOfObservers.ToString(), document);
            this.AddNextNteElement("HER2 average copy number per nucleus: " + panelSetOrder.AverageHer2NeuSignal.Value.ToString(), document);
            this.AddNextNteElement("Chr17 average copy number per nucleus: " + panelSetOrder.AverageChr17Signal, document);
            this.AddNextNteElement("Ratio of average HER2/Chr17 signals: " + panelSetOrder.Her2Chr17Ratio, document);
            this.AddBlankNteElement(document);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = (YellowstonePathology.Business.Test.AliquotOrder)this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(panelSetOrder.OrderedOnId);
            string blockDescription = string.Empty;
            if (aliquotOrder != null)
            {
                blockDescription = " - Block " + aliquotOrder.Label;
            }

            this.AddNextNteElement("Specimen site and type: " + specimenOrder.Description + blockDescription, document);
            this.AddNextNteElement("Specimen fixation type: " + specimenOrder.LabFixation, document);
            this.AddNextNteElement("Time to fixation: " + specimenOrder.TimeToFixationHourString, document);
            this.AddNextNteElement("Duration of fixation: " + specimenOrder.FixationDurationString, document);
            this.AddNextNteElement("Sample adequacy: " + panelSetOrder.SampleAdequacy, document);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextNteElement("Collection Date/Time: " + collectionDateTimeString, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Interpretation: ", document);
            this.HandleLongString(panelSetOrder.InterpretiveComment, document);
            this.AddBlankNteElement(document);

            if (string.IsNullOrEmpty(specimenOrder.FixationComment) == false)
            {
                this.HandleLongString("Fixation Comment:" + specimenOrder.FixationComment, document);
                this.AddBlankNteElement(document);
            }

            this.AddNextNteElement("Method: ", document);
            string method = panelSetOrder.Method;
            if (string.IsNullOrEmpty(method) == true)
            {
                method = System.Security.SecurityElement.Escape("This test was performed using a molecular method, In Situ Hybridization (ISH) with the US FDA approved Inform HER2 DNA probe kit, modified to report results according to ASCO/CAP guidelines. The test was performed on paraffin embedded tissue in compliance with ASCO/CAP guidelines.  Probes used include a locus specific probe for HER2 and an internal hybridization control probe for the centromeric region of chromosome 17 (Chr17).");
            }
            this.AddNextNteElement(method, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("References: ", document);
            this.AddNextNteElement(panelSetOrder.ReportReference, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement(panelSetOrder.ASRComment, document);
            this.AddBlankNteElement(document);

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextNteElement(locationPerformed, document);
            this.AddBlankNteElement(document);
        }

        private void ASCOPre2014ToXml(XElement document, HER2AmplificationByISHTestOrder panelSetOrder)
        {
            this.AddNextNteElement("HER2: " + panelSetOrder.Result, document);
            this.AddNextNteElement("Ratio: " + panelSetOrder.AverageHer2Chr17Signal, document);
            this.AddNextNteElement("Reference Range: Negative < 2, Positive >= 2", document);

            if (panelSetOrder.ResultComment != string.Empty)
            {
                this.AddNextNteElement("Comment: " + panelSetOrder.ResultComment, document);
                this.AddBlankNteElement(document);
            }

            this.AddNextNteElement("Pathologist: " + panelSetOrder.Signature, document);
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextNteElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document);
            }
            this.AddBlankNteElement(document);
            this.AddAmendments(document, panelSetOrder);

            this.AddNextNteElement("Number of invasive tumor cells counted: " + panelSetOrder.CellsCounted.ToString(), document);
            this.AddNextNteElement("Number of observers: " + panelSetOrder.NumberOfObservers.ToString(), document);
            this.AddNextNteElement("HER2 average copy number per nucleus: " + panelSetOrder.AverageHer2NeuSignal.Value.ToString(), document);
            this.AddNextNteElement("Chr17 average copy number per nucleus: " + panelSetOrder.AverageChr17Signal, document);
            this.AddNextNteElement("Ratio of average HER2/Chr17 signals: " + panelSetOrder.Her2Chr17Ratio, document);
            this.AddBlankNteElement(document);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = (YellowstonePathology.Business.Test.AliquotOrder)this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(panelSetOrder.OrderedOnId);
            string blockDescription = string.Empty;
            if (aliquotOrder != null)
            {
                blockDescription = " - Block " + aliquotOrder.Label;
            }

            this.AddNextNteElement("Specimen site and type: " + specimenOrder.Description + blockDescription, document);
            this.AddNextNteElement("Specimen fixation type: " + specimenOrder.LabFixation, document);
            this.AddNextNteElement("Time to fixation: " + specimenOrder.TimeToFixationHourString, document);
            this.AddNextNteElement("Duration of fixation: " + specimenOrder.FixationDurationString, document);
            this.AddNextNteElement("Sample adequacy: " + panelSetOrder.SampleAdequacy, document);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextNteElement("Collection Date/Time: " + collectionDateTimeString, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Interpretation: ", document);
            this.HandleLongString(panelSetOrder.InterpretiveComment, document);
            this.AddBlankNteElement(document);

            if (string.IsNullOrEmpty(specimenOrder.FixationComment) == false)
            {
                this.HandleLongString("Fixation Comment:" + specimenOrder.FixationComment, document);
                this.AddBlankNteElement(document);
            }

            this.AddNextNteElement("Method: ", document);
            string method = panelSetOrder.Method;
            if (string.IsNullOrEmpty(method) == true)
            {
                method = System.Security.SecurityElement.Escape("This test was performed using a molecular method, In Situ Hybridization (ISH) with the US FDA approved Inform HER2 DNA probe kit, modified to report results according to ASCO/CAP guidelines. The test was performed on paraffin embedded tissue in compliance with ASCO/CAP guidelines.  Probes used include a locus specific probe for HER2 and an internal hybridization control probe for the centromeric region of chromosome 17 (Chr17).");
            }
            this.AddNextNteElement(method, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("References: ", document);
            this.AddNextNteElement(panelSetOrder.ReportReference, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement(panelSetOrder.ASRComment, document);
            this.AddBlankNteElement(document);

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextNteElement(locationPerformed, document);
            this.AddBlankNteElement(document);
        }
    }
}
