using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.BRAFV600EK
{
    public class BRAFV600EKWPHOBXView : YellowstonePathology.Business.HL7View.WPH.WPHOBXView
    {
        public BRAFV600EKWPHOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            BRAFV600EKTestOrder panelSetOrder = (BRAFV600EKTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddHeader(document, panelSetOrder, "BRAF V600E Mutation Analysis");
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

            this.AddNextObxElement("Specimen Description:", document, "F");
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextObxElement(specimenOrder.Description, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            string method = panelSetOrder.Method;
            this.HandleLongString("Method: " + method, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("References: ", document, "F");
            this.HandleLongString(panelSetOrder.ReportReferences, document, "F");
            this.AddNextObxElement("", document, "F");

            string asr = panelSetOrder.ReportDisclaimer;
            this.HandleLongString(asr, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
