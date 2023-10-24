using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ReticulatedPlateletAnalysisV2
{
    public class ReticulatedPlateletAnalysisV2EPICNTEView : Business.HL7View.EPIC.EPICBeakerNTEView
    {
        private Business.Test.AccessionOrder m_AccessionOrder;
        private string m_ReportNo;

        public ReticulatedPlateletAnalysisV2EPICNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int nteCount)             
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;
            this.m_NTECount = nteCount;
        }

        public override void ToXml(XElement document)
        {
            ReticulatedPlateletAnalysisV2TestOrder panelSetOrder = (ReticulatedPlateletAnalysisV2TestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddCompanyHeader(document);
            this.AddNextNTEElement("Reticulated Platelet Analysis", document);

            this.AddNextNTEElement("Result: " + panelSetOrder.Result, document);
            this.AddNextNTEElement("Reference: " + panelSetOrder.ResultReference, document);
            this.AddNextNTEElement(string.Empty, document);

            //this.AddAmendments(document);

            this.AddNextNTEElement("Antibodies Used: CD41, Thiozole Orange", document);
            this.AddNextNTEElement(string.Empty, document);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextNTEElement("Specimen Description: " + specimenOrder.Description, document);
            this.AddNextNTEElement("", document);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextNTEElement("Collection Date/Time: " + collectionDateTimeString, document);
            this.AddNextNTEElement(string.Empty, document);

            this.AddNextNTEElement("Method: Quantitative Flow Cytometry", document);
            this.AddNextNTEElement(string.Empty, document);

            this.AddNextNTEElement(panelSetOrder.ASRComment, document);
            this.AddNextNTEElement(string.Empty, document);

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextNTEElement(locationPerformed, document);
            this.AddNextNTEElement(string.Empty, document);
        }
    }
}
