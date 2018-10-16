using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.MPNExtendedReflex
{
	public class MPNExtendedReflexEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
		public MPNExtendedReflexEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

		public override void ToXml(XElement document)
		{
            PanelSetOrderMPNExtendedReflex panelSetOrderMPNExtendedReflex = (PanelSetOrderMPNExtendedReflex)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddHeader(document, panelSetOrderMPNExtendedReflex, panelSetOrderMPNExtendedReflex.PanelSetName);
			this.AddNextObxElement("", document, "F");

            if (string.IsNullOrEmpty(panelSetOrderMPNExtendedReflex.JAK2V617FResult) == false)
            {
                this.AddNextObxElement("JAK2 V617F Mutation Analysis: " + panelSetOrderMPNExtendedReflex.JAK2V617FResult, document, "F");
            }
            if (string.IsNullOrEmpty(panelSetOrderMPNExtendedReflex.CalreticulinMutationAnalysisResult) == false)
            {
                this.AddNextObxElement("Calreticulin Mutation Analysis: " + panelSetOrderMPNExtendedReflex.CalreticulinMutationAnalysisResult, document, "F");
            }
            if (string.IsNullOrEmpty(panelSetOrderMPNExtendedReflex.MPLResult) == false)
            {
                this.AddNextObxElement("MPL Mutation Analysis: " + panelSetOrderMPNExtendedReflex.MPLResult, document, "F");
            }
            this.AddNextObxElement(string.Empty, document, "F");

			this.AddNextObxElement("Pathologist: " + panelSetOrderMPNExtendedReflex.Signature, document, "F");
			if (panelSetOrderMPNExtendedReflex.FinalTime.HasValue == true)
			{
				this.AddNextObxElement("E-signed " + panelSetOrderMPNExtendedReflex.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
			}
			this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Specimen Description:", document, "F");
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrderMPNExtendedReflex.OrderedOn, panelSetOrderMPNExtendedReflex.OrderedOnId);
			this.HandleLongString(specimenOrder.Description, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");

			this.AddNextObxElement("Comment: ", document, "F");
			this.HandleLongString(panelSetOrderMPNExtendedReflex.Comment, document, "F");
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("Interpretation: ", document, "F");
			this.HandleLongString(panelSetOrderMPNExtendedReflex.Interpretation, document, "F");
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("Method: ", document, "F");
			this.HandleLongString(panelSetOrderMPNExtendedReflex.Method, document, "F");
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("References: ", document, "F");
			this.HandleLongString(panelSetOrderMPNExtendedReflex.ReportReferences, document, "F");
			this.AddNextObxElement("", document, "F");			

            string locationPerformed = panelSetOrderMPNExtendedReflex.GetLocationPerformedComment();
			this.HandleLongString(locationPerformed, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");
		}
	}
}
