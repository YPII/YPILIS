﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HematopathologySummary
{
    public class HematopathologySummaryEPICOBXView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        public HematopathologySummaryEPICOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
            : base(accessionOrder, reportNo, obxCount)
        {

        }

        public override void ToXml(XElement document)
        {
            HematopathologySummaryTestOrder panelSetOrder = (HematopathologySummaryTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, "Hematopathology Summary");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Result: " + panelSetOrder.Result, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Pathologist: " + panelSetOrder.Signature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Interpretation: ", document, "F");
            this.HandleLongString(panelSetOrder.Interpretation, document, "F");
            this.AddNextObxElement("", document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
