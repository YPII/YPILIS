using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.PDL122C3forUrothelialCarcinoma
{
    public class PDL122C3forUrothelialCarcinomaEPICNTEView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
    {
        public PDL122C3forUrothelialCarcinomaEPICNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
        }

        public override void ToXml(XElement document)
        {
            Business.HL7View.EPIC.EPICBeakerNarrativeOBXView.AddElement(document);

            PDL122C3forUrothelialCarcinomaTestOrder panelSetOrder = (PDL122C3forUrothelialCarcinomaTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddCompanyHeaderNTE(document);
            this.AddNextNTEElement(panelSetOrder.PanelSetName, document);
            this.AddNextNTEElement("Report No: " + panelSetOrder.ReportNo, document);

            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("Stain Percent: " + panelSetOrder.StainPercent, document);

            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("Reference Ranges", document);
            this.AddNextNTEElement("High Expression: >/= 50 % TPS", document);
            this.AddNextNTEElement("Expressed: 1 – 49 % TPS", document);
            this.AddNextNTEElement("No Expression: < 1 % TPS", document);

            if (string.IsNullOrEmpty(panelSetOrder.Comment) == false)
            {
                this.AddNextNTEElement("", document);
                this.AddNextNTEElement("Comment: ", document);
                this.AddNextNTEElement(panelSetOrder.Comment, document);
            }

            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("Pathologist: " + panelSetOrder.ReferenceLabSignature, document);
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextNTEElement("E-signed " + panelSetOrder.FinalDate.Value.ToString("MM/dd/yyyy HH:mm"), document);
            }

            this.AddNextNTEElement("", document);
            this.AddAmendments(document);

            this.AddNextNTEElement("Specimen Information:", document);
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextNTEElement("Specimen Identification: " + specimenOrder.Description, document);
            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextNTEElement("Collection Date/Time: " + collectionDateTimeString, document);            

            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("Interpretation:", document);
            this.AddNextNTEElement(panelSetOrder.Interpretation, document);

            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("Method:", document);
            this.AddNextNTEElement(panelSetOrder.Method, document);

            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("References:", document);
            this.AddNextNTEElement(panelSetOrder.ReportReferences, document);

            this.AddNextNTEElement("", document);
            string locationComment = panelSetOrder.GetLocationPerformedComment();
            this.AddNextNTEElement(locationComment, document);
            this.AddNextNTEElement(string.Empty, document);
        }
    }
}
