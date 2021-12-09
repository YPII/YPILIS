using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using YellowstonePathology.Business.Helper;

namespace YellowstonePathology.Business.Test.BCellEnumeration
{
    public class BCellEnumerationWPHOBXView : YellowstonePathology.Business.HL7View.WPH.WPHOBXView
    {
        public BCellEnumerationWPHOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
        }

        public override void ToXml(XElement document)
        {
            BCellEnumerationTestOrder panelSetOrder = (BCellEnumerationTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, "B-Cell Enumeration");

            this.AddNextObxElement("", document, "F");
            StringBuilder result = new StringBuilder("WBC: " + panelSetOrder.WBC.ToString() + "/uL (from client)");
            this.AddNextObxElement(result.ToString(), document, "F");
            result = new StringBuilder("Lymphocyte Percentage: " + panelSetOrder.LymphocytePercentage.ToString().StringAsPercent());
            this.AddNextObxElement(result.ToString(), document, "F");
            result = new StringBuilder("CD19 B-Cell Positive Percent: " + panelSetOrder.CD19BCellPositivePercent.ToString().StringAsPercent());
            this.AddNextObxElement(result.ToString(), document, "F");
            result = new StringBuilder("CD20 B-Cell Positive Percent: " + panelSetOrder.CD20BCellPositivePercent.ToString().StringAsPercent());
            this.AddNextObxElement(result.ToString(), document, "F");
            result = new StringBuilder("CD19 Absolute Count: " + panelSetOrder.CD19AbsoluteCount.ToString() + "/uL");
            this.AddNextObxElement(result.ToString(), document, "F");
            result = new StringBuilder("CD20 Absolute Count: " + panelSetOrder.CD20AbsoluteCount.ToString() + "/uL");
            this.AddNextObxElement(result.ToString(), document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextObxElement("Specimen Description: " + specimenOrder.Description, document, "F");
            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Method:", document, "F");
            this.HandleLongString(panelSetOrder.Method, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("References:", document, "F");
            this.HandleLongString(panelSetOrder.ReportReferences, document, "F");
            this.AddNextObxElement("", document, "F");

            this.HandleLongString(panelSetOrder.ASRComment, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.HandleLongString(panelSetOrder.GetLocationPerformedComment(), document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
