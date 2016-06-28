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
			MPNStandardReflexResult mpnStandardReflexResult = new MPNStandardReflexResult(this.m_AccessionOrder);

			this.AddHeader(document, mpnStandardReflexResult.PanelSetOrderMPNStandardReflex, mpnStandardReflexResult.PanelSetOrderMPNStandardReflex.PanelSetName);
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("JAK2 V617F Analysis: " + mpnStandardReflexResult.JAK2V617FResult, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");

			this.AddNextObxElement("JAK2 Exon 12-14 Analysis: " + mpnStandardReflexResult.JAK2Exon1214Result, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");

			this.AddNextObxElement("Pathologist: " + mpnStandardReflexResult.PanelSetOrderMPNStandardReflex.Signature, document, "F");
			if (mpnStandardReflexResult.PanelSetOrderMPNStandardReflex.FinalDate.HasValue == true)
			{
				this.AddNextObxElement("E-signed " + mpnStandardReflexResult.PanelSetOrderMPNStandardReflex.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
			}
			this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Specimen Description:", document, "F");
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(mpnStandardReflexResult.PanelSetOrderMPNStandardReflex.OrderedOn, mpnStandardReflexResult.PanelSetOrderMPNStandardReflex.OrderedOnId);
			this.AddNextObxElement(specimenOrder.Description, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");

			if (string.IsNullOrEmpty(mpnStandardReflexResult.PanelSetOrderMPNStandardReflex.Comment) == false)
			{
				this.AddNextObxElement("Comment: ", document, "F");
				this.HandleLongString(mpnStandardReflexResult.PanelSetOrderMPNStandardReflex.Comment, document, "F");
				this.AddNextObxElement("", document, "F");
			}

			this.AddNextObxElement("Interpretation: ", document, "F");
			this.HandleLongString(mpnStandardReflexResult.PanelSetOrderMPNStandardReflex.Interpretation, document, "F");
			this.AddNextObxElement("", document, "F");
			
			this.HandleLongString("Method: " + mpnStandardReflexResult.PanelSetOrderMPNStandardReflex.Method, document, "F");
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("References: ", document, "F");
			this.HandleLongString(mpnStandardReflexResult.PanelSetOrderMPNStandardReflex.References, document, "F");
			this.AddNextObxElement("", document, "F");

            string locationPerformed = mpnStandardReflexResult.PanelSetOrderMPNStandardReflex.GetLocationPerformedComment();
			this.HandleLongString(locationPerformed, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");
		}
	}
}
