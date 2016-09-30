using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.RASRAFPanel
{
    public class RASRAFPanelEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        public RASRAFPanelEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
        }

        public override void ToXml(XElement document)
        {
            RASRAFPanelTestOrder panelSetOrder = (RASRAFPanelTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, "RAS/RAF Panel");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("BRAF Result: " + panelSetOrder.BRAFResult, document, "F");
            if(panelSetOrder.BRAFResult.ToUpper() == "DETECTED")
            {
	            this.AddNextObxElement("BRAF Mutation Name: " + panelSetOrder.BRAFMutationName, document, "F");
	            this.AddNextObxElement("BRAF Alternate Nucleotide Mutation Name: " + panelSetOrder.BRAFAlternateNucleotideMutationName, document, "F");
	            this.AddNextObxElement("BRAF Consequence: " + panelSetOrder.BRAFConsequence, document, "F");
	            this.AddNextObxElement("BRAF Predicted Effect On Protein: " + panelSetOrder.BRAFPredictedEffectOnProtein, document, "F");
            }
            
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("KRAS Result: " + panelSetOrder.KRASResult, document, "F");
            if(panelSetOrder.KRASResult.ToUpper() == "DETECTED")
            {
	            this.AddNextObxElement("KRAS Mutation Name: " + panelSetOrder.KRASMutationName, document, "F");
	            this.AddNextObxElement("KRAS Alternate Nucleotide Mutation Name: " + panelSetOrder.KRASAlternateNucleotideMutationName, document, "F");
	            this.AddNextObxElement("KRAS Consequence: " + panelSetOrder.KRASConsequence, document, "F");
	            this.AddNextObxElement("KRAS Predicted Effect On Protein: " + panelSetOrder.KRASPredictedEffectOnProtein, document, "F");
            }
            
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("NRAS Result: " + panelSetOrder.NRASResult, document, "F");
            if(panelSetOrder.NRASResult.ToUpper() == "DETECTED")
            {
	            this.AddNextObxElement("NRAS Mutation Name: " + panelSetOrder.NRASMutationName, document, "F");
	            this.AddNextObxElement("NRAS Alternate Nucleotide Mutation Name: " + panelSetOrder.NRASAlternateNucleotideMutationName, document, "F");
	            this.AddNextObxElement("NRAS Consequence: " + panelSetOrder.NRASConsequence, document, "F");
	            this.AddNextObxElement("NRAS Predicted Effect On Protein: " + panelSetOrder.NRASPredictedEffectOnProtein, document, "F");
            }
            
            this.AddNextObxElement("HRAS Result: " + panelSetOrder.HRASResult, document, "F");
            if(panelSetOrder.KRASResult.ToUpper() == "DETECTED")
            {
	            this.AddNextObxElement("HRAS Mutation Name: " + panelSetOrder.HRASMutationName, document, "F");
	            this.AddNextObxElement("HRAS Alternate Nucleotide Mutation Name: " + panelSetOrder.HRASAlternateNucleotideMutationName, document, "F");
	            this.AddNextObxElement("HRAS Consequence: " + panelSetOrder.HRASConsequence, document, "F");
	            this.AddNextObxElement("HRAS Predicted Effect On Protein: " + panelSetOrder.HRASPredictedEffectOnProtein, document, "F");
            }

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Comment: " + panelSetOrder.Comment, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Pathologist: " + panelSetOrder.ReferenceLabSignature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
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
            this.AddNextObxElement("Interpretation:", document, "F");
            this.HandleLongString(panelSetOrder.Interpretation, document, "F");
            
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Method:", document, "F");            
            this.HandleLongString(panelSetOrder.Method, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("References:", document, "F");
            this.HandleLongString(panelSetOrder.ReportReferences, document, "F");

            this.AddNextObxElement("", document, "F");
            this.HandleLongString(panelSetOrder.ReportDisclaimer, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
