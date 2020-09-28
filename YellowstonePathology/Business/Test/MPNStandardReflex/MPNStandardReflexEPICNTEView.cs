using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.MPNStandardReflex
{
	public class MPNStandardReflexEPICNTEView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
    {
		public MPNStandardReflexEPICNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

		public override void ToXml(XElement document)
		{
			//Add the first element as narrative for Nikki to see.
			Business.HL7View.EPIC.EPICBeakerNarrativeOBXView.AddElement(document);

			PanelSetOrderMPNStandardReflex panelSetOrder = (PanelSetOrderMPNStandardReflex)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

			this.AddCompanyHeaderNTE(document);
			this.AddNextNTEElement(panelSetOrder.PanelSetName, document);
			this.AddNextNTEElement("Report No: " + panelSetOrder.ReportNo, document);
			this.AddNextNTEElement("", document);


			if (string.IsNullOrEmpty(panelSetOrder.JAK2V617FResult) == false)
            {
                this.AddNextNTEElement("JAK2 V617F Analysis: " + panelSetOrder.JAK2V617FResult, document);
            }

            if (string.IsNullOrEmpty(panelSetOrder.JAK2Exon1214Result) == false)
            {
                this.AddNextNTEElement("JAK2 Exon 12-14 Analysis: " + panelSetOrder.JAK2Exon1214Result, document);
            }

            if (string.IsNullOrEmpty(panelSetOrder.MPLResult) == false)
            {
                this.AddNextNTEElement("MPL Mutation Analysis: " + panelSetOrder.MPLResult, document);
            }

            this.AddNextNTEElement(string.Empty, document);
            this.AddNextNTEElement("Pathologist: " + panelSetOrder.Signature, document);
			if (panelSetOrder.FinalDate.HasValue == true)
			{
				this.AddNextNTEElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document);
			}
			this.AddNextNTEElement("", document);
            this.AddAmendments(document);

            this.AddNextNTEElement("Specimen Description:", document);
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
			this.AddNextNTEElement(specimenOrder.Description, document);
			this.AddNextNTEElement(string.Empty, document);

			if (string.IsNullOrEmpty(panelSetOrder.Comment) == false)
			{
				this.AddNextNTEElement("Comment: ", document);
				this.AddNextNTEElement(panelSetOrder.Comment, document);
				this.AddNextNTEElement("", document);
			}

			this.AddNextNTEElement("Interpretation: ", document);
			this.AddNextNTEElement(panelSetOrder.Interpretation, document);
			this.AddNextNTEElement("", document);
			
			this.AddNextNTEElement("Method: " + panelSetOrder.Method, document);
			this.AddNextNTEElement("", document);

			this.AddNextNTEElement("References: ", document);
			this.AddNextNTEElement(panelSetOrder.ReportReferences, document);
			this.AddNextNTEElement("", document);

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
			this.AddNextNTEElement(locationPerformed, document);
			this.AddNextNTEElement(string.Empty, document);
		}
	}
}
