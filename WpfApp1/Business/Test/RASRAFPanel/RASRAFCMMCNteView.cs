using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.RASRAFPanel
{
	public class RASRAFCMMCNTEView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
    {
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

        public RASRAFCMMCNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;            
		}		

		public override void ToXml(XElement document)
		{
            RASRAFPanelTestOrder panelSetOrder = (RASRAFPanelTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement(panelSetOrder.PanelSetName, document);            
            this.AddNextNteElement("Master Accession #: " + panelSetOrder.MasterAccessionNo, document);
            this.AddNextNteElement("Report #: " + panelSetOrder.ReportNo, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("", document);
            this.AddNextNteElement("BRAF Result: " + panelSetOrder.BRAFResult, document);
            if (panelSetOrder.BRAFResult.ToUpper() == "DETECTED")
            {
                this.AddNextNteElement("BRAF Mutation Name: " + panelSetOrder.BRAFMutationName, document);
                this.AddNextNteElement("BRAF Alternate Nucleotide Mutation Name: " + panelSetOrder.BRAFAlternateNucleotideMutationName, document);
                this.AddNextNteElement("BRAF Consequence: " + panelSetOrder.BRAFConsequence, document);
                this.AddNextNteElement("BRAF Predicted Effect On Protein: " + panelSetOrder.BRAFPredictedEffectOnProtein, document);
            }

            this.AddNextNteElement("", document);
            this.AddNextNteElement("KRAS Result: " + panelSetOrder.KRASResult, document);
            if (panelSetOrder.KRASResult.ToUpper() == "DETECTED")
            {
                this.AddNextNteElement("KRAS Mutation Name: " + panelSetOrder.KRASMutationName, document);
                this.AddNextNteElement("KRAS Alternate Nucleotide Mutation Name: " + panelSetOrder.KRASAlternateNucleotideMutationName, document);
                this.AddNextNteElement("KRAS Consequence: " + panelSetOrder.KRASConsequence, document);
                this.AddNextNteElement("KRAS Predicted Effect On Protein: " + panelSetOrder.KRASPredictedEffectOnProtein, document);
            }

            this.AddNextNteElement("", document);
            this.AddNextNteElement("NRAS Result: " + panelSetOrder.NRASResult, document);
            if (panelSetOrder.NRASResult.ToUpper() == "DETECTED")
            {
                this.AddNextNteElement("NRAS Mutation Name: " + panelSetOrder.NRASMutationName, document);
                this.AddNextNteElement("NRAS Alternate Nucleotide Mutation Name: " + panelSetOrder.NRASAlternateNucleotideMutationName, document);
                this.AddNextNteElement("NRAS Consequence: " + panelSetOrder.NRASConsequence, document);
                this.AddNextNteElement("NRAS Predicted Effect On Protein: " + panelSetOrder.NRASPredictedEffectOnProtein, document);
            }

            this.AddNextNteElement("HRAS Result: " + panelSetOrder.HRASResult, document);
            if (panelSetOrder.KRASResult.ToUpper() == "DETECTED")
            {
                this.AddNextNteElement("HRAS Mutation Name: " + panelSetOrder.HRASMutationName, document);
                this.AddNextNteElement("HRAS Alternate Nucleotide Mutation Name: " + panelSetOrder.HRASAlternateNucleotideMutationName, document);
                this.AddNextNteElement("HRAS Consequence: " + panelSetOrder.HRASConsequence, document);
                this.AddNextNteElement("HRAS Predicted Effect On Protein: " + panelSetOrder.HRASPredictedEffectOnProtein, document);
            }

            this.AddNextNteElement("", document);
            this.AddNextNteElement("Comment: " + panelSetOrder.Comment, document);

            this.AddNextNteElement("", document);
            this.AddNextNteElement("Pathologist: " + panelSetOrder.ReferenceLabSignature, document);
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextNteElement("E-signed " + panelSetOrder.ReferenceLabFinalDate.Value.ToString("MM/dd/yyyy HH:mm"), document);
            }

            this.AddNextNteElement("", document);
            //this.AddAmendments(document);

            this.AddNextNteElement("Specimen Information:", document);
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextNteElement("Specimen Identification: " + specimenOrder.Description, document);
            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextNteElement("Collection Date/Time: " + collectionDateTimeString, document);

            this.AddNextNteElement("", document);
            this.AddNextNteElement("Interpretation:", document);
            this.HandleLongString(panelSetOrder.Interpretation, document);

            this.AddNextNteElement("", document);
            this.AddNextNteElement("Method:", document);
            this.HandleLongString(panelSetOrder.Method, document);

            this.AddNextNteElement("", document);
            this.AddNextNteElement("References:", document);
            this.HandleLongString(panelSetOrder.ReportReferences, document);

            this.AddNextNteElement("", document);
            this.HandleLongString(panelSetOrder.ReportDisclaimer, document);
            this.AddNextNteElement(string.Empty, document);
        }        
	}
}
