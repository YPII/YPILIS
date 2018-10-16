using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.MPNStandardReflex
{
	public class MPNStandardReflexEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
		public MPNStandardReflexEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

		public override void ToXml(XElement document)
		{
            PanelSetOrderMPNStandardReflex panelSetOrder = (PanelSetOrderMPNStandardReflex)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddHeader(document, panelSetOrder, panelSetOrder.PanelSetName);
			this.AddNextObxElement("", document, "F");

            if (string.IsNullOrEmpty(panelSetOrder.JAK2V617FResult) == false)
            {
                this.AddNextObxElement("JAK2 V617F Analysis: " + panelSetOrder.JAK2V617FResult, document, "F");
                this.AddNextObxElement(string.Empty, document, "F");
            }

            if (string.IsNullOrEmpty(panelSetOrder.JAK2Exon1214Result) == false)
            {
                this.AddNextObxElement("JAK2 Exon 12-14 Analysis: " + panelSetOrder.JAK2Exon1214Result, document, "F");
                this.AddNextObxElement(string.Empty, document, "F");
            }

			this.AddNextObxElement("Pathologist: " + panelSetOrder.Signature, document, "F");
			if (panelSetOrder.FinalDate.HasValue == true)
			{
				this.AddNextObxElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
			}
			this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Specimen Description:", document, "F");
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
			this.AddNextObxElement(specimenOrder.Description, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");

			if (string.IsNullOrEmpty(panelSetOrder.Comment) == false)
			{
				this.AddNextObxElement("Comment: ", document, "F");
				this.HandleLongString(panelSetOrder.Comment, document, "F");
				this.AddNextObxElement("", document, "F");
			}

			this.AddNextObxElement("Interpretation: ", document, "F");
			this.HandleLongString(panelSetOrder.Interpretation, document, "F");
			this.AddNextObxElement("", document, "F");
			
			this.HandleLongString("Method: " + panelSetOrder.Method, document, "F");
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("References: ", document, "F");
			this.HandleLongString(panelSetOrder.ReportReferences, document, "F");
			this.AddNextObxElement("", document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
			this.HandleLongString(locationPerformed, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");
		}
	}
}
