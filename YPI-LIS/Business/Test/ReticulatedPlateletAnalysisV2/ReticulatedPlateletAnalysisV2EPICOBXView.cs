using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ReticulatedPlateletAnalysisV2
{
    public class ReticulatedPlateletAnalysisV2EPICOBXView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        public ReticulatedPlateletAnalysisV2EPICOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            ReticulatedPlateletAnalysisV2TestOrder panelSetOrder = (ReticulatedPlateletAnalysisV2TestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddHeader(document, panelSetOrder, "Reticulated Platelet Analysis");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Result: " + panelSetOrder.Result, document, "F");
            this.AddNextObxElement("Reference: " + panelSetOrder.ResultReference, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddAmendments(document);

            this.AddNextObxElement("Antibodies Used: CD41, Thiozole Orange", document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextObxElement("Specimen Description: " + specimenOrder.Description, document, "F");
            this.AddNextObxElement("", document, "F");

            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Method: Quantitative Flow Cytometry", document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.HandleLongString(panelSetOrder.ASRComment, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
