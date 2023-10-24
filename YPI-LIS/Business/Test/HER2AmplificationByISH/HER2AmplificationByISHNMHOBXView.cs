using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
	public class HER2AmplificationByISHNMHOBXView : YellowstonePathology.Business.HL7View.NMH.NMHOBXViewOld
    {
		public HER2AmplificationByISHNMHOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
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
            else if(this.m_AccessionOrder.AccessionDate >= DateTime.Parse("1/1/2014") == true)
            {
                if (panelSetOrder.Indicator == "Breast")
                {
                    this.ASCOCAP2018(document, panelSetOrder);
                }
                else if (panelSetOrder.Indicator == "Gastric")
                {
                    this.GastricToXml(document, panelSetOrder);
                }            
            }
        }

        //REPN Report No
        //HER2 HER2 Result
        //RATIO Ratio
        //HER2COPY Average HER2 Copy Number
        //COM Comment
        //PATH Pathologist
        //ESIGN E-signed
        //AMEND Amendments
        //INVCELLS Number of invasive tumor cells counted
        //OBSERV Number of observers
        //CHR17 Chr17 average copy number
        //HER2/CHR17 Ratio of average HER2/Chr17 signals
        //SPEC SITE Specimen site and type
        //FIX Specimen fixation type
        //FIXTIME Time to fixation
        //FIXDUR Duration of fixation
        //SPECADEQ Sample adequacy
        //COLLECT Collection Date/Time
        //INTERP Interpretation
        //RANGE Reference Range
        //FIXCOM Fixation Comment
        //METHOD Method
        //REF References
        //TESTDEV Test Development
        //LOC Location Performed

        public void BreastToXml(XElement document, HER2AmplificationByISHTestOrder panelSetOrder)
        {
            string referenceRange = "Based on 2013 CAP/ASCO guidelines, a case is considered POSITIVE when the HER2 to Chr17 ratio is >=2.0 with any average " +
                "HER2 copy number or when the HER2 to Chr17 ratio is <2.0 with an average HER2 copy number >=6.0 signals/nucleus, " +
                "EQUIVOCAL when the HER2 to Chr17 ratio is <2.0 with an average HER2 copy number >=4.0 and <6.0 signals/cell, and " +
                "NEGATIVE when the HER2 to Chr17 ratio is <2.0 with an average HER2 copy number < 4.0 signals/cell.";

            this.AddNextOBXElement("REPN", "Report No", panelSetOrder.ReportNo, document, "F");
            this.AddNextOBXElement("HER2", "HER2", panelSetOrder.Result, document, "F");
            this.AddNextOBXElement("RATIO", "Ratio", panelSetOrder.AverageHer2Chr17Signal, document, "F");
            this.AddNextOBXElement("HER2COPY", "Average HER2 Copy Number", panelSetOrder.AverageHer2NeuSignal.Value.ToString(), document, "F");

            if (string.IsNullOrEmpty(panelSetOrder.ResultComment) != true)
            {
				this.AddNextOBXElement("COM", "Comment", panelSetOrder.ResultComment, document, "F");
            }

            this.AddNextOBXElement("PATH", "Pathologist", panelSetOrder.Signature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextOBXElement("ESIGN", "E-signed", panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }

            //this.AddAmendments(document);

            this.AddNextOBXElement("INVCELLS", "Number of invasive tumor cells counted", panelSetOrder.CellsCounted.ToString(), document, "F");
            this.AddNextOBXElement("OBSERV", "Number of observers", panelSetOrder.NumberOfObservers.ToString(), document, "F");
            if (panelSetOrder.AverageHer2NeuSignal.HasValue == true)
            {
                this.AddNextOBXElement("HER2", "HER2 average copy number", panelSetOrder.AverageHer2NeuSignal.Value.ToString(), document, "F");
            }
            this.AddNextOBXElement("CHR17", "Chr17 average copy number", panelSetOrder.AverageChr17Signal, document, "F");
            this.AddNextOBXElement("HER2/CHR17", "Ratio of average HER2/Chr17 signals", panelSetOrder.AverageHer2Chr17Signal, document, "F");

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = (YellowstonePathology.Business.Test.AliquotOrder)this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(panelSetOrder.OrderedOnId);
            string blockDescription = string.Empty;
            if (aliquotOrder != null)
            {
                blockDescription = " - Block " + aliquotOrder.Label;
            }

            this.AddNextOBXElement("SPEC SITE", "Specimen site and type", specimenOrder.Description + blockDescription, document, "F");
            this.AddNextOBXElement("FIX", "Specimen fixation type", specimenOrder.LabFixation, document, "F");
            this.AddNextOBXElement("FIXTIME", "Time to fixation", specimenOrder.TimeToFixationHourString, document, "F");
            this.AddNextOBXElement("FIXDUR", "Duration of fixation", specimenOrder.FixationDurationString, document, "F");
            this.AddNextOBXElement("SPECADEQ", "Sample adequacy", panelSetOrder.SampleAdequacy, document, "F");

            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextOBXElement("COLLECT", "Collection Date/Time", collectionDateTimeString, document, "F");
            
            this.AddNextOBXElement("INTERP", "Interpretation", panelSetOrder.InterpretiveComment, document, "F");
            this.AddNextOBXElement("RANGE", "Reference Range", referenceRange, document, "F");

            if (string.IsNullOrEmpty(specimenOrder.FixationComment) == false)
            {
                this.AddNextOBXElement("FIXCOM", "Fixation Comment", specimenOrder.FixationComment, document, "F");
            }

            this.AddNextOBXElement("METHOD", "Method", panelSetOrder.Method, document, "F");
            this.AddNextOBXElement("", "References", panelSetOrder.ReportReference, document, "F");

			this.AddNextOBXElement("REF", "Test Development", panelSetOrder.ASRComment, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextOBXElement("LOC", "Location Performed", locationPerformed, document, "F");
        }

		public void GastricToXml(XElement document, HER2AmplificationByISHTestOrder panelSetOrder)
        {
            throw new Exception("Not implemented.");
            /*
            this.AddHeader(document, panelSetOrder, "HER2 Gene Amplification");
            this.AddNextOBXElement("", "", document, "F");

            this.AddNextOBXElement("", "HER2: " + panelSetOrder.Result, document, "F");
            this.AddNextOBXElement("", "Ratio: " + panelSetOrder.AverageHer2Chr17Signal, document, "F");
            this.AddNextOBXElement("", "Reference Range: Negative < 2, Positive >= 2", document, "F");

            if (string.IsNullOrEmpty(panelSetOrder.ResultComment) != true)
            {
				this.HandleLongString("Comment: " + panelSetOrder.ResultComment, document, "F");
            }
            this.AddNextOBXElement("", string.Empty, document, "F");

            this.AddNextOBXElement("", "Pathologist: " + panelSetOrder.Signature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextOBXElement("", "E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextOBXElement("", string.Empty, document, "F");
            this.AddAmendments(document);

            this.AddNextOBXElement("", "Number of invasive tumor cells counted: " + panelSetOrder.CellsCounted.ToString(), document, "F");
            this.AddNextOBXElement("", "Number of observers: " + panelSetOrder.NumberOfObservers.ToString(), document, "F");
            if (panelSetOrder.AverageHer2NeuSignal.HasValue == true)
            {
                this.AddNextOBXElement("", "HER2 average copy number per nucleus: " + panelSetOrder.AverageHer2NeuSignal.Value.ToString(), document, "F");
            }
            this.AddNextOBXElement("", "Chr17 average copy number per nucleus: " + panelSetOrder.AverageChr17Signal, document, "F");
            this.AddNextOBXElement("", "Ratio of average HER2/Chr17 signals: " + panelSetOrder.AverageHer2Chr17Signal, document, "F");
            this.AddNextOBXElement("", string.Empty, document, "F");

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = (YellowstonePathology.Business.Test.AliquotOrder)this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(panelSetOrder.OrderedOnId);
            string blockDescription = string.Empty;
            if (aliquotOrder != null)
            {
                blockDescription = " - Block " + aliquotOrder.Label;
            }

            this.AddNextOBXElement("", "Specimen site and type: " + specimenOrder.Description + blockDescription, document, "F");
            this.AddNextOBXElement("", "Specimen fixation type: " + specimenOrder.LabFixation, document, "F");
            this.AddNextOBXElement("", "Time to fixation: " + specimenOrder.TimeToFixationHourString, document, "F");
            this.AddNextOBXElement("", "Duration of fixation: " + specimenOrder.FixationDurationString, document, "F");
            this.AddNextOBXElement("", "Sample adequacy: " + panelSetOrder.SampleAdequacy, document, "F");

            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextOBXElement("", "Collection Date/Time: " + collectionDateTimeString, document, "F");
            this.AddNextOBXElement("", string.Empty, document, "F");

            this.AddNextOBXElement("", "Interpretation: ", document, "F");
            this.HandleLongString(panelSetOrder.InterpretiveComment, document, "F");
            this.AddNextOBXElement("", string.Empty, document, "F");

            if (string.IsNullOrEmpty(specimenOrder.FixationComment) == false)
            {
                this.HandleLongString("Fixation Comment:" + specimenOrder.FixationComment, document, "F");
                this.AddNextOBXElement("", string.Empty, document, "F");
            }

            this.AddNextOBXElement("", "Method: ", document, "F");
			this.HandleLongString(panelSetOrder.Method, document, "F");
            this.AddNextOBXElement("", string.Empty, document, "F");

            this.AddNextOBXElement("", "References: ", document, "F");
			this.HandleLongString(panelSetOrder.ReportReference, document, "F");
            this.AddNextOBXElement("", string.Empty, document, "F");

            this.HandleLongString(panelSetOrder.ASRComment, document, "F");
            this.AddNextOBXElement("", string.Empty, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.HandleLongString(locationPerformed, document, "F");
            this.AddNextOBXElement("", string.Empty, document, "F");
            */
        }

        public void ASCOPre2014ToXml(XElement document, HER2AmplificationByISHTestOrder panelSetOrder)
        {
            throw new Exception("Not implemented.");
            /*
            this.AddHeader(document, panelSetOrder, "HER2 Gene Amplification");
            this.AddNextOBXElement("", "", document, "F");

            this.AddNextOBXElement("", "HER2: " + panelSetOrder.Result, document, "F");
            this.AddNextOBXElement("", "Ratio: " + panelSetOrder.AverageHer2Chr17Signal, document, "F");
            this.AddNextOBXElement("", "Reference Range: Negative < 2, Positive >= 2", document, "F");

            if (string.IsNullOrEmpty(panelSetOrder.ResultComment) != true)
            {
                this.HandleLongString("Comment: " + panelSetOrder.ResultComment, document, "F");
            }
            this.AddNextOBXElement("", string.Empty, document, "F");

            this.AddNextOBXElement("", "Pathologist: " + panelSetOrder.Signature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextOBXElement("", "E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextOBXElement("", string.Empty, document, "F");
            this.AddAmendments(document);

            this.AddNextOBXElement("", "Number of invasive tumor cells counted: " + panelSetOrder.CellsCounted.ToString(), document, "F");
            this.AddNextOBXElement("", "Number of observers: " + panelSetOrder.NumberOfObservers.ToString(), document, "F");
            if (panelSetOrder.AverageHer2NeuSignal.HasValue == true)
            {
                this.AddNextOBXElement("", "HER2 average copy number per nucleus: " + panelSetOrder.AverageHer2NeuSignal.Value.ToString(), document, "F");
            }
            this.AddNextOBXElement("", "Chr17 average copy number per nucleus: " + panelSetOrder.AverageChr17Signal, document, "F");
            this.AddNextOBXElement("", "Ratio of average HER2/Chr17 signals: " + panelSetOrder.AverageHer2Chr17Signal, document, "F");
            this.AddNextOBXElement("", string.Empty, document, "F");

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = (YellowstonePathology.Business.Test.AliquotOrder)this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(panelSetOrder.OrderedOnId);
            string blockDescription = string.Empty;
            if (aliquotOrder != null)
            {
                blockDescription = " - Block " + aliquotOrder.Label;
            }

            this.AddNextOBXElement("", "Specimen site and type: " + specimenOrder.Description + blockDescription, document, "F");
            this.AddNextOBXElement("", "Specimen fixation type: " + specimenOrder.LabFixation, document, "F");
            this.AddNextOBXElement("", "Time to fixation: " + specimenOrder.TimeToFixationHourString, document, "F");
            this.AddNextOBXElement("", "Duration of fixation: " + specimenOrder.FixationDurationString, document, "F");
            this.AddNextOBXElement("", "Sample adequacy: " + panelSetOrder.SampleAdequacy, document, "F");

            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextOBXElement("", "Collection Date/Time: " + collectionDateTimeString, document, "F");
            this.AddNextOBXElement("", string.Empty, document, "F");

            this.AddNextOBXElement("", "Interpretation: ", document, "F");
            this.HandleLongString(panelSetOrder.InterpretiveComment, document, "F");
            this.AddNextOBXElement("", string.Empty, document, "F");

            if (string.IsNullOrEmpty(specimenOrder.FixationComment) == false)
            {
                this.HandleLongString("Fixation Comment:" + specimenOrder.FixationComment, document, "F");
                this.AddNextOBXElement("", string.Empty, document, "F");
            }

            this.AddNextOBXElement("", "Method: ", document, "F");
            this.HandleLongString(panelSetOrder.Method, document, "F");
            this.AddNextOBXElement("", string.Empty, document, "F");

            this.AddNextOBXElement("", "References: ", document, "F");
            this.HandleLongString(panelSetOrder.ReportReference, document, "F");
            this.AddNextOBXElement("", string.Empty, document, "F");

            this.AddNextOBXElement("", panelSetOrder.ASRComment, document, "F");
            this.AddNextOBXElement("", string.Empty, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.HandleLongString(locationPerformed, document, "F");
            this.AddNextOBXElement("", string.Empty, document, "F");
            */
        }

        private void ASCOCAP2018(XElement document, HER2AmplificationByISHTestOrder panelSetOrder)
        {
            throw new Exception("Not implemented.");
            /*
            string referenceRange = "Based on 2018 CAP/ASCO guidelines, a case is considered POSITIVE when the HER2 to Chr17 ratio is >= 2.0 and the average " +
                "HER2 copy number >= 4.0; when the HER2 to Chr17 ratio is >= 2.0 and the average HER2 copy number < 4.0 and a HER2 By IHC score = +3; when the HER2 " +
                "to Chr17 ratio is < 2.0 and the average copy number >= 4.0 but < 6.0 and a HER2 By IHC score = +3; when the HER2 to Chr17 ratio is < 2.0 and " +
                "the average HER2 copy number >= 6.0 and a HER2 By IHC score = +2 or +3." +
                "NEGATIVE when the HER2 to Chr17 ratio is < 2.0 with an average HER2 copy number < 4.0.";

            this.AddNextOBXElement("", "HER2 Gene Amplification", panelSetOrder.PanelSetName, document, "F");

            this.AddNextOBXElement("", "HER2", panelSetOrder.Result, document, "F");
            this.AddNextOBXElement("", "HER2 to Chr17 Ratio", panelSetOrder.AverageHer2Chr17Signal, document, "F");
            this.AddNextOBXElement("", "Average HER2 Copy Number", panelSetOrder.AverageHer2NeuSignal.Value.ToString(), document, "F");

            if (string.IsNullOrEmpty(panelSetOrder.ResultComment) != true)
            {
                this.AddNextOBXElement("", "Comment",panelSetOrder.ResultComment, document, "F");
            }

            this.AddNextOBXElement("", "Pathologist", panelSetOrder.Signature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextOBXElement("", "E-signed", panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddAmendments(document);

            this.AddNextOBXElement("", "Number of invasive tumor cells counted", panelSetOrder.CellsCounted.ToString(), document, "F");
            this.AddNextOBXElement("", "Number of observers", panelSetOrder.NumberOfObservers.ToString(), document, "F");
            if (panelSetOrder.AverageHer2NeuSignal.HasValue == true)
            {
                this.AddNextOBXElement("", "HER2 average copy number", panelSetOrder.AverageHer2NeuSignal.Value.ToString(), document, "F");
            }
            this.AddNextOBXElement("", "Chr17 average copy number", panelSetOrder.AverageChr17Signal, document, "F");
            this.AddNextOBXElement("", "Ratio of average HER2/Chr17 signals", panelSetOrder.AverageHer2Chr17Signal, document, "F");

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = (YellowstonePathology.Business.Test.AliquotOrder)this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(panelSetOrder.OrderedOnId);
            string blockDescription = string.Empty;
            if (aliquotOrder != null)
            {
                blockDescription = " - Block " + aliquotOrder.Label;
            }

            this.AddNextOBXElement("", "Specimen site and type", specimenOrder.Description + blockDescription, document, "F");
            this.AddNextOBXElement("", "Specimen fixation type", specimenOrder.LabFixation, document, "F");
            this.AddNextOBXElement("", "Time to fixation", specimenOrder.TimeToFixationHourString, document, "F");
            this.AddNextOBXElement("", "Duration of fixation", specimenOrder.FixationDurationString, document, "F");
            this.AddNextOBXElement("", "Sample adequacy", panelSetOrder.SampleAdequacy, document, "F");

            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextOBXElement("", "Collection Date/Time", collectionDateTimeString, document, "F");

            this.AddNextOBXElement("", "Interpretation", panelSetOrder.InterpretiveComment, document, "F");
            this.AddNextOBXElement("", "Reference Range", referenceRange, document, "F");

            if (string.IsNullOrEmpty(specimenOrder.FixationComment) == false)
            {
                this.AddNextOBXElement("", "Fixation Comment", specimenOrder.FixationComment, document, "F");
            }

            this.AddNextOBXElement("", "Method", panelSetOrder.Method, document, "F");
            this.AddNextOBXElement("", "References", panelSetOrder.ReportReference, document, "F");
            this.AddNextOBXElement("", "Test Development", panelSetOrder.ASRComment, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextOBXElement("", "Location Performed", locationPerformed, document, "F");
            */
        }
    }
}
