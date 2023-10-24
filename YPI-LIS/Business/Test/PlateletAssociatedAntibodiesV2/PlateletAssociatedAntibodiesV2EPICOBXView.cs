using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.PlateletAssociatedAntibodiesV2
{
    public class PlateletAssociatedAntibodiesV2EPICOBXView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        public PlateletAssociatedAntibodiesV2EPICOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
            : base(accessionOrder, reportNo, obxCount)
        { }

        public override void ToXml(XElement document)
        {
            PlateletAssociatedAntibodiesV2TestOrder testOrder = (PlateletAssociatedAntibodiesV2TestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddHeader(document, testOrder, "Platelet Associated Antibodies, Direct");

            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Test", document, "F");
            this.AddNextObxElement("Anti Platelet Antibody - IgG: " + testOrder.IgGResult, document, "F");
            this.AddNextObxElement("Reference" + testOrder.IgGReference, document, "F");
            this.AddNextObxElement("Anti Platelet Antibody - IgM: " + testOrder.IgMResult, document, "F");
            this.AddNextObxElement("Reference" + testOrder.IgMReference, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Specimen Information:", document, "F");
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testOrder.OrderedOn, testOrder.OrderedOnId);
            this.AddNextObxElement("Specimen Description: " + specimenOrder.Description, document, "F");
            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Interpretation:", document, "F");
            this.HandleLongString(testOrder.Interpretation, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Method:", document, "F");
            this.HandleLongString(testOrder.Method, document, "F");
            this.AddNextObxElement("", document, "F");

            this.HandleLongString(testOrder.ASRComment, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            string locationPerformed = testOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
