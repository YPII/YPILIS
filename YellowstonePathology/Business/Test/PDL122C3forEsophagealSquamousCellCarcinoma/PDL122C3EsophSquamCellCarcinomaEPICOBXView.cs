using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.PDL122C3forEsophagealSquamousCellCarcinoma
{
    public class PDL122C3EsophSquamCellCarcinomaEPICOBXView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        public PDL122C3EsophSquamCellCarcinomaEPICOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
            : base(accessionOrder, reportNo, obxCount)
        {
        }

        public override void ToXml(XElement document)
        {
            PDL122C3forEsophagealSquamousCellCarcinomaTestOrder panelSetOrder = (PDL122C3forEsophagealSquamousCellCarcinomaTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, "PD-L1 (22C3) for Esophageal Squamous Cell Carcinoma");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Stain Percent: " + panelSetOrder.StainPercent, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Reference Ranges", document, "F");
            this.AddNextObxElement("High Expression: >/= 50 % TPS", document, "F");
            this.AddNextObxElement("Expressed: 1 – 49 % TPS", document, "F");
            this.AddNextObxElement("No Expression: < 1 % TPS", document, "F");

            if (string.IsNullOrEmpty(panelSetOrder.Comment) == false)
            {
                this.AddNextObxElement("", document, "F");
                this.AddNextObxElement("Comment: ", document, "F");
                this.HandleLongString(panelSetOrder.Comment, document, "F");
            }

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Pathologist: " + panelSetOrder.ReferenceLabSignature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrder.FinalDate.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }

            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Specimen Information:", document, "F");
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextObxElement("Specimen Identification: " + specimenOrder.Description, document, "F");
            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
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
            string locationComment = panelSetOrder.GetLocationPerformedComment();
            this.HandleLongString(locationComment, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
