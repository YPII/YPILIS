using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.KRASStandard
{
	public class KRASStandardEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
		public KRASStandardEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

		public override void ToXml(XElement document)
		{
			KRASStandardTestOrder panelSetOrder = (KRASStandardTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

			this.AddHeader(document, panelSetOrder, "KRAS Mutation Analysis");
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("Result: " + panelSetOrder.Result, document, "F");

			if (string.IsNullOrEmpty(panelSetOrder.MutationDetected) == false)
			{
				this.AddNextObxElement("  " + panelSetOrder.MutationDetected, document, "F");
			}

            if (string.IsNullOrEmpty(panelSetOrder.Comment) == false)
			{
				this.AddNextObxElement("Comment: " + panelSetOrder.Comment, document, "F");
				this.AddNextObxElement("", document, "F");
			}

            this.AddNextObxElement("Pathologist: " + panelSetOrder.Signature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

			this.AddNextObxElement("Indication: " + panelSetOrder.Indication, document, "F");
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("Interpretation: ", document, "F");
			this.HandleLongString(panelSetOrder.Interpretation, document, "F");
			this.AddNextObxElement("", document, "F");

			if (string.IsNullOrEmpty(panelSetOrder.TumorNucleiPercentage) == false)
			{
				this.AddNextObxElement("Tumor Nuclei Percent: ", document, "F");
				this.HandleLongString(panelSetOrder.TumorNucleiPercentage, document, "F");
				this.AddNextObxElement("", document, "F");
			}

			this.AddNextObxElement("Method: ", document, "F");
			this.AddNextObxElement(panelSetOrder.Method, document, "F");
			this.AddNextObxElement("", document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
			this.AddNextObxElement(locationPerformed, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");
		}      
    }
}
