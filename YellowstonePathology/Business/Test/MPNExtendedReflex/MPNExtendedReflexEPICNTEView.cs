using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.MPNExtendedReflex
{
	public class MPNExtendedReflexEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
    {
		public MPNExtendedReflexEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

		public override void ToXml(XElement document)
		{
			//Add the first element as narrative for Nikki to see.
			Business.HL7View.EPIC.EPICBeakerNarrativeOBXView.AddElement(document);

			PanelSetOrderMPNExtendedReflex panelSetOrderMPNExtendedReflex = (PanelSetOrderMPNExtendedReflex)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

			this.AddCompanyHeaderNTE(document);
			this.AddNextNTEElement(panelSetOrderMPNExtendedReflex.PanelSetName, document);
			this.AddNextNTEElement("Report No: " + panelSetOrderMPNExtendedReflex.ReportNo, document);

			if (string.IsNullOrEmpty(panelSetOrderMPNExtendedReflex.JAK2V617FResult) == false)
            {
                this.AddNextNTEElement("JAK2 V617F Mutation Analysis: " + panelSetOrderMPNExtendedReflex.JAK2V617FResult, document);
            }
            if (string.IsNullOrEmpty(panelSetOrderMPNExtendedReflex.JAK2Mutation) == false)
            {
                this.AddNextNTEElement("Mutation(s): " + panelSetOrderMPNExtendedReflex.JAK2Mutation, document);
            }
            if (string.IsNullOrEmpty(panelSetOrderMPNExtendedReflex.JAK2Exon1214Result) == false)
            {
                this.AddNextNTEElement("JAK2 Exon 12-14 Mutation Analysis: " + panelSetOrderMPNExtendedReflex.JAK2Exon1214Result, document);
            }
            if (string.IsNullOrEmpty(panelSetOrderMPNExtendedReflex.CalreticulinMutationAnalysisResult) == false)
            {
                this.AddNextNTEElement("Calreticulin Mutation Analysis: " + panelSetOrderMPNExtendedReflex.CalreticulinMutationAnalysisResult, document);
            }
            if (string.IsNullOrEmpty(panelSetOrderMPNExtendedReflex.MPLResult) == false)
            {
                this.AddNextNTEElement("MPL Mutation Analysis: " + panelSetOrderMPNExtendedReflex.MPLResult, document);
            }
            this.AddNextNTEElement(string.Empty, document);

			this.AddNextNTEElement("Pathologist: " + panelSetOrderMPNExtendedReflex.Signature, document);
			if (panelSetOrderMPNExtendedReflex.FinalTime.HasValue == true)
			{
				this.AddNextNTEElement("E-signed " + panelSetOrderMPNExtendedReflex.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document);
			}
			this.AddNextNTEElement("", document);
            this.AddAmendments(document);

            this.AddNextNTEElement("Specimen Description:", document);
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrderMPNExtendedReflex.OrderedOn, panelSetOrderMPNExtendedReflex.OrderedOnId);
			this.AddNextNTEElement(specimenOrder.Description, document);
			this.AddNextNTEElement(string.Empty, document);

			this.AddNextNTEElement("Comment: ", document);
			this.AddNextNTEElement(panelSetOrderMPNExtendedReflex.Comment, document);
			this.AddNextNTEElement("", document);

			this.AddNextNTEElement("Interpretation: ", document);
			this.AddNextNTEElement(panelSetOrderMPNExtendedReflex.Interpretation, document);
			this.AddNextNTEElement("", document);

			this.AddNextNTEElement("Method: ", document);
			this.AddNextNTEElement(panelSetOrderMPNExtendedReflex.Method, document);
			this.AddNextNTEElement("", document);

			this.AddNextNTEElement("References: ", document);
			this.AddNextNTEElement(panelSetOrderMPNExtendedReflex.ReportReferences, document);
			this.AddNextNTEElement("", document);			

            string locationPerformed = panelSetOrderMPNExtendedReflex.GetLocationPerformedComment();
			this.AddNextNTEElement(locationPerformed, document);
			this.AddNextNTEElement(string.Empty, document);
		}
	}
}
