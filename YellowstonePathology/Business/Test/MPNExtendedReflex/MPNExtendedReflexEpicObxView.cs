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
			MPNExtendedReflexResult mpnExtendedReflexResult = new MPNExtendedReflexResult(this.m_AccessionOrder);

			this.AddHeader(document, mpnExtendedReflexResult.PanelSetOrderMPNExtendedReflex, mpnExtendedReflexResult.PanelSetOrderMPNExtendedReflex.PanelSetName);
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("JAK2 V617F Mutation Analysis: " + mpnExtendedReflexResult.JAK2V617FResult, document, "F");
			this.AddNextObxElement("Calreticulin Mutation Analysis: " + mpnExtendedReflexResult.CALRResult, document, "F");
			this.AddNextObxElement("MPL Mutation Analysis: " + mpnExtendedReflexResult.MPLResult, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");

			this.AddNextObxElement("Pathologist: " + mpnExtendedReflexResult.PanelSetOrderMPNExtendedReflex.Signature, document, "F");
			if (mpnExtendedReflexResult.PanelSetOrderMPNExtendedReflex.FinalTime.HasValue == true)
			{
				this.AddNextObxElement("E-signed " + mpnExtendedReflexResult.PanelSetOrderMPNExtendedReflex.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
			}
			this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Specimen Description:", document, "F");
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(mpnExtendedReflexResult.PanelSetOrderMPNExtendedReflex.OrderedOn, mpnExtendedReflexResult.PanelSetOrderMPNExtendedReflex.OrderedOnId);
			this.HandleLongString(specimenOrder.Description, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");

			this.AddNextObxElement("Comment: ", document, "F");
			this.HandleLongString(mpnExtendedReflexResult.PanelSetOrderMPNExtendedReflex.Comment, document, "F");
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("Interpretation: ", document, "F");
			this.HandleLongString(mpnExtendedReflexResult.PanelSetOrderMPNExtendedReflex.Interpretation, document, "F");
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("Method: ", document, "F");
			this.HandleLongString(mpnExtendedReflexResult.PanelSetOrderMPNExtendedReflex.Method, document, "F");
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("References: ", document, "F");
			this.HandleLongString(mpnExtendedReflexResult.PanelSetOrderMPNExtendedReflex.References, document, "F");
			this.AddNextObxElement("", document, "F");			

            string locationPerformed = mpnExtendedReflexResult.PanelSetOrderMPNExtendedReflex.GetLocationPerformedComment();
			this.HandleLongString(locationPerformed, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");
		}
	}
}
