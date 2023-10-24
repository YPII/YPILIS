using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.FLT3
{
	public class FLT3EPICNTEView : Business.HL7View.EPIC.EPICBeakerObxView
	{
		public FLT3EPICNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
		}

		public override void ToXml(XElement document)
		{
			PanelSetOrderFLT3 panelSetOrder = (PanelSetOrderFLT3)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(panelSetOrder.PanelSetId);

			//Add the first element as narrative for Nikki to see.
			Business.HL7View.EPIC.EPICBeakerNarrativeOBXView.AddElement(document);
			this.AddCompanyHeaderNTE(document);
			this.AddNextNTEElement(panelSet.PanelSetName, document);

            this.AddNextNTEElement("", document);
			string result = "Result: " + panelSetOrder.Result;
			this.AddNextNTEElement(result, document);
			result = "  ITD Mutation " + panelSetOrder.ITDMutation;
			this.AddNextNTEElement(result, document);
			result = "  ITD Percentage " + panelSetOrder.ITDPercentage;
			this.AddNextNTEElement(result, document);
			result = "  TKD Mutation " + panelSetOrder.TKDMutation;
			this.AddNextNTEElement(result, document);

			this.AddNextNTEElement("", document);
			this.AddNextNTEElement("Pathologist: " + panelSetOrder.Signature, document);
			if (panelSetOrder.FinalTime.HasValue == true)
			{
				this.AddNextNTEElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document);
			}

			this.AddNextNTEElement("", document);
            this.AddAmendmentsNTE(document, panelSetOrder, this.m_AccessionOrder);

            this.AddNextNTEElement("Specimen Information:", document);
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
			this.AddNextNTEElement("Specimen Identification: " + specimenOrder.Description, document);
			string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
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
            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
			this.AddNextNTEElement(locationPerformed, document);
			this.AddNextNTEElement(string.Empty, document);
		}
	}
}
