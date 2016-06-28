using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
	public class HER2AmplificationByISHEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
		public HER2AmplificationByISHEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
			HER2AmplificationByISHTestOrder panelSetOrder = (HER2AmplificationByISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
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

		public void BreastToXml(XElement document, HER2AmplificationByISHTestOrder panelSetOrder)
        {
            string referenceRange = "Based on 2013 CAP/ASCO guidelines, a case is considered POSITIVE when the HER2 to Chr17 ratio is >=2.0 with any average " +
                "HER2 copy number or when the HER2 to Chr17 ratio is <2.0 with an average HER2 copy number >=6.0 signals/nucleus, " +
                "EQUIVOCAL when the HER2 to Chr17 ratio is <2.0 with an average HER2 copy number >=4.0 and <6.0 signals/cell, and " +
                "NEGATIVE when the HER2 to Chr17 ratio is <2.0 with an average HER2 copy number < 4.0 signals/cell.";

            this.AddHeader(document, panelSetOrder, "HER2 Gene Amplification");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("HER2: " + panelSetOrder.Result, document, "F");
            this.AddNextObxElement("Ratio: " + panelSetOrder.AverageHer2Chr17Signal, document, "F");
            this.AddNextObxElement("Average HER2 Copy Number = " + panelSetOrder.AverageHer2NeuSignal.Value.ToString(), document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            if (string.IsNullOrEmpty(panelSetOrder.ResultComment) != true)
            {
				this.HandleLongString("Comment: " + panelSetOrder.ResultComment, document, "F");
                this.AddNextObxElement(string.Empty, document, "F");
            }

            this.AddNextObxElement("Pathologist: " + panelSetOrder.Signature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement(string.Empty, document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Number of invasive tumor cells counted: " + panelSetOrder.CellsCounted.ToString(), document, "F");
            this.AddNextObxElement("Number of observers: " + panelSetOrder.NumberOfObservers.ToString(), document, "F");
            if (panelSetOrder.AverageHer2NeuSignal.HasValue == true)
            {
                this.AddNextObxElement("HER2 average copy number: " + panelSetOrder.AverageHer2NeuSignal.Value.ToString(), document, "F");
            }
            this.AddNextObxElement("Chr17 average copy number: " + panelSetOrder.AverageChr17Signal, document, "F");
            this.AddNextObxElement("Ratio of average HER2/Chr17 signals: " + panelSetOrder.AverageHer2Chr17Signal, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = (YellowstonePathology.Business.Test.AliquotOrder)this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(panelSetOrder.OrderedOnId);
            string blockDescription = string.Empty;
            if (aliquotOrder != null)
            {
                blockDescription = " - Block " + aliquotOrder.Label;
            }

            this.AddNextObxElement("Specimen site and type: " + specimenOrder.Description + blockDescription, document, "F");
            this.AddNextObxElement("Specimen fixation type: " + specimenOrder.LabFixation, document, "F");
            this.AddNextObxElement("Time to fixation: " + specimenOrder.TimeToFixationHourString, document, "F");
            this.AddNextObxElement("Duration of fixation: " + specimenOrder.FixationDurationString, document, "F");
            this.AddNextObxElement("Sample adequacy: " + panelSetOrder.SampleAdequacy, document, "F");

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
            
            this.AddNextObxElement("Interpretation: ", document, "F");
            this.HandleLongString(panelSetOrder.InterpretiveComment, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Reference Range: " + referenceRange, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            if (string.IsNullOrEmpty(specimenOrder.FixationComment) == false)
            {
                this.HandleLongString("Fixation Comment:" + specimenOrder.FixationComment, document, "F");
                this.AddNextObxElement(string.Empty, document, "F");
            }

            this.AddNextObxElement("Method: ", document, "F");
			this.HandleLongString(panelSetOrder.Method, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");            

            this.AddNextObxElement("References: ", document, "F");
			this.HandleLongString(panelSetOrder.ReportReference, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

			this.HandleLongString(panelSetOrder.ASRComment, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }

		public void GastricToXml(XElement document, HER2AmplificationByISHTestOrder panelSetOrder)
        {
            this.AddHeader(document, panelSetOrder, "HER2 Gene Amplification");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("HER2: " + panelSetOrder.Result, document, "F");
            this.AddNextObxElement("Ratio: " + panelSetOrder.AverageHer2Chr17Signal, document, "F");
            this.AddNextObxElement("Reference Range: Negative < 2, Positive >= 2", document, "F");

            if (string.IsNullOrEmpty(panelSetOrder.ResultComment) != true)
            {
				this.HandleLongString("Comment: " + panelSetOrder.ResultComment, document, "F");
            }
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Pathologist: " + panelSetOrder.Signature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement(string.Empty, document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Number of invasive tumor cells counted: " + panelSetOrder.CellsCounted.ToString(), document, "F");
            this.AddNextObxElement("Number of observers: " + panelSetOrder.NumberOfObservers.ToString(), document, "F");
            if (panelSetOrder.AverageHer2NeuSignal.HasValue == true)
            {
                this.AddNextObxElement("HER2 average copy number per nucleus: " + panelSetOrder.AverageHer2NeuSignal.Value.ToString(), document, "F");
            }
            this.AddNextObxElement("Chr17 average copy number per nucleus: " + panelSetOrder.AverageChr17Signal, document, "F");
            this.AddNextObxElement("Ratio of average HER2/Chr17 signals: " + panelSetOrder.AverageHer2Chr17Signal, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = (YellowstonePathology.Business.Test.AliquotOrder)this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(panelSetOrder.OrderedOnId);
            string blockDescription = string.Empty;
            if (aliquotOrder != null)
            {
                blockDescription = " - Block " + aliquotOrder.Label;
            }

            this.AddNextObxElement("Specimen site and type: " + specimenOrder.Description + blockDescription, document, "F");
            this.AddNextObxElement("Specimen fixation type: " + specimenOrder.LabFixation, document, "F");
            this.AddNextObxElement("Time to fixation: " + specimenOrder.TimeToFixationHourString, document, "F");
            this.AddNextObxElement("Duration of fixation: " + specimenOrder.FixationDurationString, document, "F");
            this.AddNextObxElement("Sample adequacy: " + panelSetOrder.SampleAdequacy, document, "F");

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Interpretation: ", document, "F");
            this.HandleLongString(panelSetOrder.InterpretiveComment, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            if (string.IsNullOrEmpty(specimenOrder.FixationComment) == false)
            {
                this.HandleLongString("Fixation Comment:" + specimenOrder.FixationComment, document, "F");
                this.AddNextObxElement(string.Empty, document, "F");
            }

            this.AddNextObxElement("Method: ", document, "F");
			this.HandleLongString(panelSetOrder.Method, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("References: ", document, "F");
			this.HandleLongString(panelSetOrder.ReportReference, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement(panelSetOrder.ASRComment, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }

        public void ASCOPre2014ToXml(XElement document, HER2AmplificationByISHTestOrder panelSetOrder)
        {
            this.AddHeader(document, panelSetOrder, "HER2 Gene Amplification");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("HER2: " + panelSetOrder.Result, document, "F");
            this.AddNextObxElement("Ratio: " + panelSetOrder.AverageHer2Chr17Signal, document, "F");
            this.AddNextObxElement("Reference Range: Negative < 2, Positive >= 2", document, "F");

            if (string.IsNullOrEmpty(panelSetOrder.ResultComment) != true)
            {
                this.HandleLongString("Comment: " + panelSetOrder.ResultComment, document, "F");
            }
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Pathologist: " + panelSetOrder.Signature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement(string.Empty, document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Number of invasive tumor cells counted: " + panelSetOrder.CellsCounted.ToString(), document, "F");
            this.AddNextObxElement("Number of observers: " + panelSetOrder.NumberOfObservers.ToString(), document, "F");
            if (panelSetOrder.AverageHer2NeuSignal.HasValue == true)
            {
                this.AddNextObxElement("HER2 average copy number per nucleus: " + panelSetOrder.AverageHer2NeuSignal.Value.ToString(), document, "F");
            }
            this.AddNextObxElement("Chr17 average copy number per nucleus: " + panelSetOrder.AverageChr17Signal, document, "F");
            this.AddNextObxElement("Ratio of average HER2/Chr17 signals: " + panelSetOrder.AverageHer2Chr17Signal, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = (YellowstonePathology.Business.Test.AliquotOrder)this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(panelSetOrder.OrderedOnId);
            string blockDescription = string.Empty;
            if (aliquotOrder != null)
            {
                blockDescription = " - Block " + aliquotOrder.Label;
            }

            this.AddNextObxElement("Specimen site and type: " + specimenOrder.Description + blockDescription, document, "F");
            this.AddNextObxElement("Specimen fixation type: " + specimenOrder.LabFixation, document, "F");
            this.AddNextObxElement("Time to fixation: " + specimenOrder.TimeToFixationHourString, document, "F");
            this.AddNextObxElement("Duration of fixation: " + specimenOrder.FixationDurationString, document, "F");
            this.AddNextObxElement("Sample adequacy: " + panelSetOrder.SampleAdequacy, document, "F");

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Interpretation: ", document, "F");
            this.HandleLongString(panelSetOrder.InterpretiveComment, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            if (string.IsNullOrEmpty(specimenOrder.FixationComment) == false)
            {
                this.HandleLongString("Fixation Comment:" + specimenOrder.FixationComment, document, "F");
                this.AddNextObxElement(string.Empty, document, "F");
            }

            this.AddNextObxElement("Method: ", document, "F");
            this.HandleLongString(panelSetOrder.Method, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("References: ", document, "F");
            this.HandleLongString(panelSetOrder.ReportReference, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement(panelSetOrder.ASRComment, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
