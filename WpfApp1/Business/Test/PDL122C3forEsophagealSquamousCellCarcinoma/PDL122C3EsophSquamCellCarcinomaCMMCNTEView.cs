using System;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.PDL122C3forEsophagealSquamousCellCarcinoma
{
    public class PDL122C3EsophSquamCellCarcinomaCMMCNTEView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
    {
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

        public PDL122C3EsophSquamCellCarcinomaCMMCNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;
        }

        public override void ToXml(XElement document)
        {
            PDL122C3forEsophagealSquamousCellCarcinomaTestOrder panelSetOrder = (PDL122C3forEsophagealSquamousCellCarcinomaTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("PD-L1 (22C3) for Esophageal Squamous Cell Carcinoma Analysis", document);

            this.AddBlankNteElement(document);
            this.AddNextNteElement("Stain Percent: " + panelSetOrder.StainPercent, document);

            this.AddBlankNteElement(document);
            this.AddNextNteElement("Reference Ranges", document);
            this.AddNextNteElement("High Expression: >/= 50 % TPS", document);
            this.AddNextNteElement("Expressed: 1 – 49 % TPS", document);
            this.AddNextNteElement("No Expression: < 1 % TPS", document);

            if (string.IsNullOrEmpty(panelSetOrder.Comment) == false)
            {
                this.AddBlankNteElement(document);
                this.AddNextNteElement("Comment: ", document);
                this.HandleLongString(panelSetOrder.Comment, document);
            }

            this.AddBlankNteElement(document);
            this.AddNextNteElement("Pathologist: " + panelSetOrder.ReferenceLabSignature, document);
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextNteElement("E-signed " + panelSetOrder.FinalDate.Value.ToString("MM/dd/yyyy HH:mm"), document);
            }

            this.AddBlankNteElement(document);
            this.AddAmendments(document, panelSetOrder, this.m_AccessionOrder);

            this.AddNextNteElement("Specimen Information:", document);
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextNteElement("Specimen Identification: " + specimenOrder.Description, document);
            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextNteElement("Collection Date/Time: " + collectionDateTimeString, document);

            this.AddBlankNteElement(document);
            this.AddNextNteElement("Interpretation:", document);
            this.HandleLongString(panelSetOrder.Interpretation, document);

            this.AddBlankNteElement(document);
            this.AddNextNteElement("Method:", document);
            this.HandleLongString(panelSetOrder.Method, document);

            this.AddBlankNteElement(document);
            this.AddNextNteElement("References:", document);
            this.HandleLongString(panelSetOrder.ReportReferences, document);

            this.AddBlankNteElement(document);
            string locationComment = panelSetOrder.GetLocationPerformedComment();
            this.HandleLongString(locationComment, document);
            this.AddNextNteElement(string.Empty, document);
        }
    }
}
