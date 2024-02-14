using System;
using System.Xml.Linq;
using YellowstonePathology.Business.Helper;

namespace YellowstonePathology.Business.Test.TCellNKProfile
{
    class TCellNKProfileEPICOBXView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        public TCellNKProfileEPICOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            TCellNKProfileTestOrder panelSetOrder = (TCellNKProfileTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, "T-Cell/NK Profile");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("WBC: " + panelSetOrder.WBC.Value.ToString(), document, "F");
            this.AddNextObxElement("Lympocyte Percentage: " + panelSetOrder.LymphocytePercentage.ToString().StringAsPercent(), document, "F");
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("T - cells % of Lymphocytes: " + panelSetOrder.CD3TPercent.ToString().StringAsPercent(), document, "F");
            this.AddNextObxElement("T - cells Absolute Count: " + panelSetOrder.CD3TCount + "uL", document, "F");
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("CD4 + T - helper % of Lymphocytes: " + panelSetOrder.CD4TPercent.ToString().StringAsPercent(), document, "F");
            this.AddNextObxElement("CD4 + T - helper Absolute Count: " + panelSetOrder.CD4TCount + "uL", document, "F");
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("CD8 + T - suppressor % of Lymphocytes: " + panelSetOrder.CD8TPercent.ToString().StringAsPercent(), document, "F");
            this.AddNextObxElement("CD8 + T - suppressor Absolute Count: " + panelSetOrder.CD8TCount + "uL", document, "F");
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("NK cells % of Lymphocytes: " + panelSetOrder.CD16CD56NKPercent.ToString().StringAsPercent(), document, "F");
            this.AddNextObxElement("NK cells Absolute Count: " + panelSetOrder.CD16CD56NKCount + "uL", document, "F");
            this.AddNextObxElement("CD4/CD8 Ratio: " + panelSetOrder.CD4CD8Ratio, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Clinical History:", document, "F");
            this.HandleLongString(this.m_AccessionOrder.ClinicalHistory, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Method: " + panelSetOrder.Method, document, "F");

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Specimen: " + specimenOrder.Description, document, "F");
            this.AddNextObxElement("", document, "F");

            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Specimen Adequacy: " + specimenOrder.SpecimenAdequacy, document, "F");
            this.AddNextObxElement("", document, "F");

            this.HandleLongString(panelSetOrder.ASRComment, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
