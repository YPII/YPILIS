﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.FLT3
{
    public class FLT3WPHOBXView : YellowstonePathology.Business.HL7View.WPH.WPHOBXView
    {
        public FLT3WPHOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
        }

        public override void ToXml(XElement document)
        {
            PanelSetOrderFLT3 panelSetOrder = (PanelSetOrderFLT3)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, panelSetOrder.PanelSetName);

            this.AddNextObxElement("", document, "F");
            string result = "Result: " + panelSetOrder.Result;
            this.AddNextObxElement(result, document, "F");
            result = "  ITD Mutation " + panelSetOrder.ITDMutation;
            this.AddNextObxElement(result, document, "F");
            result = "  ITD Percentage " + panelSetOrder.ITDPercentage;
            this.AddNextObxElement(result, document, "F");
            result = "  TKD Mutation " + panelSetOrder.TKDMutation;
            this.AddNextObxElement(result, document, "F");

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
            this.AddNextObxElement("Specimen Identification: " + specimenOrder.Description, document, "F");
            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Interpretation:", document, "F");
            this.HandleLongString(panelSetOrder.Interpretation, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Method:", document, "F");
            this.HandleLongString(panelSetOrder.Method, document, "F");

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
