using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.RASRAFPanel
{
    public class RASRAFPanelEPICNTEView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
    {
        public RASRAFPanelEPICNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
        }

        public override void ToXml(XElement document)
        {
            RASRAFPanelTestOrder panelSetOrder = (RASRAFPanelTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            //Add the first element as narrative for Nikki to see.
            Business.HL7View.EPIC.EPICBeakerNarrativeOBXView.AddElement(document);

            this.AddCompanyHeaderNTE(document);
            this.AddNextNTEElement(panelSetOrder.PanelSetName, document);

            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("BRAF Result: " + panelSetOrder.BRAFResult, document);
            if(panelSetOrder.BRAFResult.ToUpper() == "DETECTED")
            {
	            this.AddNextNTEElement("BRAF Mutation Name: " + panelSetOrder.BRAFMutationName, document);
	            this.AddNextNTEElement("BRAF Alternate Nucleotide Mutation Name: " + panelSetOrder.BRAFAlternateNucleotideMutationName, document);
	            this.AddNextNTEElement("BRAF Consequence: " + panelSetOrder.BRAFConsequence, document);
	            this.AddNextNTEElement("BRAF Predicted Effect On Protein: " + panelSetOrder.BRAFPredictedEffectOnProtein, document);
            }
            
            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("KRAS Result: " + panelSetOrder.KRASResult, document);
            if(panelSetOrder.KRASResult.ToUpper() == "DETECTED")
            {
	            this.AddNextNTEElement("KRAS Mutation Name: " + panelSetOrder.KRASMutationName, document);
	            this.AddNextNTEElement("KRAS Alternate Nucleotide Mutation Name: " + panelSetOrder.KRASAlternateNucleotideMutationName, document);
	            this.AddNextNTEElement("KRAS Consequence: " + panelSetOrder.KRASConsequence, document);
	            this.AddNextNTEElement("KRAS Predicted Effect On Protein: " + panelSetOrder.KRASPredictedEffectOnProtein, document);
            }
            
            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("NRAS Result: " + panelSetOrder.NRASResult, document);
            if(panelSetOrder.NRASResult.ToUpper() == "DETECTED")
            {
	            this.AddNextNTEElement("NRAS Mutation Name: " + panelSetOrder.NRASMutationName, document);
	            this.AddNextNTEElement("NRAS Alternate Nucleotide Mutation Name: " + panelSetOrder.NRASAlternateNucleotideMutationName, document);
	            this.AddNextNTEElement("NRAS Consequence: " + panelSetOrder.NRASConsequence, document);
	            this.AddNextNTEElement("NRAS Predicted Effect On Protein: " + panelSetOrder.NRASPredictedEffectOnProtein, document);
            }
            
            this.AddNextNTEElement("HRAS Result: " + panelSetOrder.HRASResult, document);
            if(panelSetOrder.KRASResult.ToUpper() == "DETECTED")
            {
	            this.AddNextNTEElement("HRAS Mutation Name: " + panelSetOrder.HRASMutationName, document);
	            this.AddNextNTEElement("HRAS Alternate Nucleotide Mutation Name: " + panelSetOrder.HRASAlternateNucleotideMutationName, document);
	            this.AddNextNTEElement("HRAS Consequence: " + panelSetOrder.HRASConsequence, document);
	            this.AddNextNTEElement("HRAS Predicted Effect On Protein: " + panelSetOrder.HRASPredictedEffectOnProtein, document);
            }

            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("Comment: " + panelSetOrder.Comment, document);

            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("Pathologist: " + panelSetOrder.ReferenceLabSignature, document);
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextNTEElement("E-signed " + panelSetOrder.ReferenceLabFinalDate.Value.ToString("MM/dd/yyyy HH:mm"), document);
            }

            this.AddNextNTEElement("", document);
            this.AddAmendments(document);

            this.AddNextNTEElement("Specimen Information:", document);
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextNTEElement("Specimen Identification: " + specimenOrder.Description, document);
            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextNTEElement("Collection Date/Time: " + collectionDateTimeString, document);

            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("Interpretation:", document);
            this.AddNextNTEElement(panelSetOrder.Interpretation, document);
            
            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("Method:", document);            
            this.AddNextNTEElement(panelSetOrder.Method, document);

            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("References:", document);
            this.AddNextNTEElement(panelSetOrder.ReportReferences, document);

            this.AddNextNTEElement("", document);
            this.AddNextNTEElement(panelSetOrder.ReportDisclaimer, document);
            this.AddNextNTEElement(string.Empty, document);
        }
    }
}
