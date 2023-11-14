using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.BladderCancerFISHUrovysion
{
	public class BladderCancerFISHUrovysionEPICNTEView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
	{
		public BladderCancerFISHUrovysionEPICNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
		}

		public override void ToXml(XElement document)
		{
			//Add the first element as narrative for Nikki to see.
			Business.HL7View.EPIC.EPICBeakerNarrativeOBXView.AddElement(document);

			BladderCancerFISHUrovysionTestOrder testOrder = (BladderCancerFISHUrovysionTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			this.AddCompanyHeaderNTE(document);
			this.AddNextNTEElement(testOrder.PanelSetName, document);

			this.AddNextNTEElement("", document);
			string result = testOrder.Result;
			if (string.IsNullOrEmpty(testOrder.ResultDescription) == false) result = testOrder.ResultDescription;
			this.AddNextNTEElement("Result: " + result, document);

			this.AddNextNTEElement("", document);
			this.AddNextNTEElement("Pathologist: " + testOrder.Signature, document);
			if (testOrder.FinalTime.HasValue == true)
			{
				this.AddNextNTEElement("E-signed " + testOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document);
			}

			this.AddNextNTEElement("", document);
            this.AddAmendments(document);

            this.AddNextNTEElement("Specimen Information:", document);
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testOrder.OrderedOn, testOrder.OrderedOnId);
			this.AddNextNTEElement("Specimen Identification: " + specimenOrder.Description, document);
			string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.AddNextNTEElement("Collection Date/Time: " + collectionDateTimeString, document);

			this.AddNextNTEElement("", document);
			this.AddNextNTEElement("Interpretation:", document);
			this.AddNextNTEElement(testOrder.Interpretation, document);

			this.AddNextNTEElement("", document);
			this.AddNextNTEElement("Probe Set Details:", document);
			this.AddNextNTEElement(testOrder.ProbeSetDetail, document);

			this.AddNextNTEElement("", document);
			this.AddNextNTEElement("Nuclei Scored:", document);
			this.AddNextNTEElement(testOrder.NucleiScored, document);

			this.AddNextNTEElement("", document);
			this.AddNextNTEElement("References:", document);
			this.AddNextNTEElement(testOrder.ReportReferences, document);

			this.AddNextNTEElement("", document);
			string locationPerformed = testOrder.GetLocationPerformedComment();
			this.AddNextNTEElement(locationPerformed, document);
			this.AddNextNTEElement(string.Empty, document);
		}
	}
}
