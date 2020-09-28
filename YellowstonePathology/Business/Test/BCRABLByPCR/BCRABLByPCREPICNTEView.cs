using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.BCRABLByPCR
{
	public class BCRABLByPCREPICNTEView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
	{
		public BCRABLByPCREPICNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
		}

		public override void ToXml(XElement document)
		{
			//Add the first element as narrative for Nikki to see.
			Business.HL7View.EPIC.EPICBeakerNarrativeOBXView.AddElement(document);

			BCRABLByPCRTestOrder panelSetOrder = (BCRABLByPCRTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			this.AddCompanyHeaderNTE(document);
			this.AddNextNTEElement(panelSetOrder.PanelSetName, document);
			this.AddNextNTEElement("Report No: " + panelSetOrder.ReportNo, document);

			this.AddNextNTEElement("", document);
			string result = "Result: " + panelSetOrder.Result;
			this.AddNextNTEElement(result, document);
            if (string.IsNullOrEmpty(panelSetOrder.DetectedLogReduction) == false)
            {
                result = "Detected Log Reduction: " + panelSetOrder.DetectedLogReduction;
                this.AddNextNTEElement(result, document);
            }

            result = "Fusion Transcript Type: " + panelSetOrder.FusionTranscriptType;
			this.AddNextNTEElement(result, document);

            result = "% BCR - ABL1 / ABL1(IS): " + panelSetOrder.PercentBCRABL;
            this.AddNextNTEElement(result, document);

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
            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
			this.AddNextNTEElement(locationPerformed, document);
			this.AddNextNTEElement(string.Empty, document);
		}
	}
}
