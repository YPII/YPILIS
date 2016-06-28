using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.MicrosatelliteInstabilityAnalysis
{
	public class MicrosatelliteInstabilityAnalysisEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
	{
		public MicrosatelliteInstabilityAnalysisEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

		public override void ToXml(XElement document)
		{
			MicrosatelliteInstabilityAnalysisTestOrder panelSetOrder = (MicrosatelliteInstabilityAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

			this.AddHeader(document, panelSetOrder, "Microsatellite Instability Analysis");
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("Result: " + panelSetOrder.Result, document, "F");
			this.AddNextObxElement("  Instability Level: " + panelSetOrder.InstabilityLevel, document, "F");
			this.AddNextObxElement("  BAT25  : " + panelSetOrder.BAT25Instability, document, "F");
			this.AddNextObxElement("  BAT26  : " + panelSetOrder.BAT26Instability, document, "F");
			this.AddNextObxElement("  D5S346 : " + panelSetOrder.D5S346Instability, document, "F");
			this.AddNextObxElement("  D17S250: " + panelSetOrder.D17S250Instability, document, "F");
			this.AddNextObxElement("  D2S123 : " + panelSetOrder.D2S123Instability, document, "F");
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

			this.AddNextObxElement("Method: ", document, "F");
			this.AddNextObxElement(panelSetOrder.Method, document, "F");
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("References: ", document, "F");
			this.AddNextObxElement(panelSetOrder.References, document, "F");
			this.AddNextObxElement("", document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
			this.AddNextObxElement(locationPerformed, document, "F");
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement(panelSetOrder.TestDevelopment, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");
		}
	}
}
