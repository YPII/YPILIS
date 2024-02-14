﻿using System;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.MultipleMyelomaMGUSByFish
{
    class MultipleMyelomaMGUSByFishWPHOBXView : YellowstonePathology.Business.HL7View.WPH.WPHOBXView
    {
        public MultipleMyelomaMGUSByFishWPHOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
        }

        public override void ToXml(XElement document)
        {
            MultipleMyelomaMGUSByFishTestOrder panelSetOrder = (MultipleMyelomaMGUSByFishTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, panelSetOrder.PanelSetName);

            this.AddNextObxElement("", document, "F");
            string result = "Result: " + panelSetOrder.Result;
            this.AddNextObxElement(result, document, "F");

            if (string.IsNullOrEmpty(panelSetOrder.ReportComment) == false)
            {
                this.AddNextObxElement("", document, "F");
                this.AddNextObxElement("Report Comment: " + panelSetOrder.ReportComment, document, "F");
            }

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Pathologist: " + panelSetOrder.Signature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }

            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Specimen Information:", document, "F");
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.HandleLongString("Specimen Identification: " + specimenOrder.Description, document, "F");
            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Interpretation:", document, "F");
            this.HandleLongString(panelSetOrder.Interpretation, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Probe Set Details:", document, "F");
            this.HandleLongString(panelSetOrder.ProbeSetDetail, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Nuclei Scored:", document, "F");
            this.HandleLongString(panelSetOrder.NucleiScored, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("References:", document, "F");
            this.HandleLongString(panelSetOrder.ReportReferences, document, "F");

            this.AddNextObxElement("", document, "F");
            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.HandleLongString(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}