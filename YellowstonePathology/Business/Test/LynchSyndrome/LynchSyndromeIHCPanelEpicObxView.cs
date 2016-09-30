using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LynchSyndromeIHCPanelEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
		public LynchSyndromeIHCPanelEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
            PanelSetOrderLynchSyndromeIHC panelSetOrder = (PanelSetOrderLynchSyndromeIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, "Lynch Syndrome IHC Panel");

            this.AddNextObxElement("", document, "F");            
            this.AddNextObxElement("MLH1 Result: " + panelSetOrder.MLH1Result, document, "F");
            this.AddNextObxElement("MSH2 Result: " + panelSetOrder.MSH2Result, document, "F");
            this.AddNextObxElement("MSH6 Result: " + panelSetOrder.MSH6Result, document, "F");
            this.AddNextObxElement("PMS2 Result: " + panelSetOrder.PMS2Result, document, "F");

            this.AddNextObxElement("", document, "F");                       

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Pathologist: " + panelSetOrder.Signature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }                                   

            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Specimen Description:", document, "F");
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextObxElement(specimenOrder.Description, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
