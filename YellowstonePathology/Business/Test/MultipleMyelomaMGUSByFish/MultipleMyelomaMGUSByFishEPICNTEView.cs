﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.MultipleMyelomaMGUSByFish
{
	public class MultipleMyelomaMGUSByFishEPICNTEView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
	{
		public MultipleMyelomaMGUSByFishEPICNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
		}

		public override void ToXml(XElement document)
		{
			//Add the first element as narrative for Nikki to see.
			Business.HL7View.EPIC.EPICBeakerNarrativeOBXView.AddElement(document);

			MultipleMyelomaMGUSByFishTestOrder panelSetOrder = (MultipleMyelomaMGUSByFishTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			this.AddCompanyHeaderNTE(document);
			this.AddNextNTEElement(panelSetOrder.PanelSetName, document);
			this.AddNextNTEElement("Report No: " + panelSetOrder.ReportNo, document);

			this.AddNextNTEElement("", document);
			string result = "Result: " + panelSetOrder.Result;
			this.AddNextNTEElement(result, document);

            if (string.IsNullOrEmpty(panelSetOrder.ReportComment) == false)
            {
                this.AddNextNTEElement("", document);                
                this.AddNextNTEElement("Report Comment: " + panelSetOrder.ReportComment, document);
            }

			this.AddNextNTEElement("", document);
			this.AddNextNTEElement("Pathologist: " + panelSetOrder.Signature, document);
			if (panelSetOrder.FinalTime.HasValue == true)
			{
				this.AddNextNTEElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document);
			}

			this.AddNextNTEElement("", document);
            this.AddAmendments(document);

            this.AddNextNTEElement("Specimen Information:", document);
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
			this.AddNextNTEElement("Specimen Identification: " + specimenOrder.Description, document);
			string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
			this.AddNextNTEElement("Collection Date/Time: " + collectionDateTimeString, document);

			this.AddNextNTEElement("", document);
			this.AddNextNTEElement("Interpretation:", document);
			this.AddNextNTEElement(panelSetOrder.Interpretation, document);

			this.AddNextNTEElement("", document);
			this.AddNextNTEElement("Probe Set Details:", document);
			this.AddNextNTEElement(panelSetOrder.ProbeSetDetail, document);

			this.AddNextNTEElement("", document);
			this.AddNextNTEElement("Nuclei Scored:", document);
			this.AddNextNTEElement(panelSetOrder.NucleiScored, document);

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