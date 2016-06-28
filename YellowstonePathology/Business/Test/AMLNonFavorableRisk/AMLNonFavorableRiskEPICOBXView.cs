using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.AMLNonFavorableRisk
{
    public class AMLNonFavorableRiskEPICOBXView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        public AMLNonFavorableRiskEPICOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
        }

        public override void ToXml(XElement document)
        {
            AMLNonFavorableRiskTestOrder panelSetOrder = (AMLNonFavorableRiskTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, "AML Non-Favorable Risk");

            this.AddNextObxElement("", document, "F");
            string result = "Result: " + panelSetOrder.Result;
            this.AddNextObxElement(result, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Pathologist: " + panelSetOrder.ReferenceLabSignature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrder.ReferenceLabFinalDate.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
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
            this.AddNextObxElement("Probe Set Details:", document, "F");
            this.HandleLongString(panelSetOrder.ProbeSetDetail, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Nuclei Scored:", document, "F");
            this.HandleLongString(panelSetOrder.NucleiScored, document, "F");
            this.AddNextObxElement("", document, "F");

            this.HandleLongString(panelSetOrder.ReportDisclaimer, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
