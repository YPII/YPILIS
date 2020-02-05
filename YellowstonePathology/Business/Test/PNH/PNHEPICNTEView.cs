using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.PNH
{
	public class PNHEPICNTEView : Business.HL7View.EPIC.EPICBeakerNTEView
    {
        private Business.Test.AccessionOrder m_AccessionOrder;
        private string m_ReportNo;

        public PNHEPICNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;
		}

        public override void ToXml(XElement document)
        {
            PNHTestOrder testOrder = (PNHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            PNHNegativeResult pnhNegativeResult = new PNHNegativeResult();
            if (testOrder.ResultCode == pnhNegativeResult.ResultCode)
            {
                this.NegativeToXml(document, testOrder);
            }
            else
            {
                this.PositiveToXml(document, testOrder);
            }
        }

        private void NegativeToXml(XElement document, PNHTestOrder testOrder)
        {
            this.AddCompanyHeader(document);
            this.AddNextNTEElement("PNH, Highly Sensitive(FLAER)", document);

            this.AddNextNTEElement("Result: Negative (No evidence of paroxysmal nocturnal hemoglobinuria)", document);
            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("Comment:", document);
            this.AddNextNTEElement("Flow cytometric analysis does not identify any evidence of a PNH clone, based on analysis of several different GPI-linked antibodies on 3 separate cell populations (red blood cells, monocytes and granulocytes).  These findings do not support the diagnosis of PNH.", document);
            this.AddNextNTEElement("", document);

			this.AddNextNTEElement("Pathologist: " + testOrder.Signature, document);
			if (testOrder.FinalTime.HasValue == true)
            {
				this.AddNextNTEElement("E-signed " + testOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document);
            }
            this.AddNextNTEElement("", document);
            //this.AddAmendments(document);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testOrder.OrderedOn, testOrder.OrderedOnId);
            this.AddNextNTEElement("Specimen Description: " + specimenOrder.Description, document);
            this.AddNextNTEElement("", document);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextNTEElement("Collection Date/Time: " + collectionDateTimeString, document);
            this.AddNextNTEElement(string.Empty, document);

            this.AddNextNTEElement("Method: ", document);
            this.AddNextNTEElement(testOrder.Method, document);
            this.AddNextNTEElement("", document);

            this.AddNextNTEElement("Result Data: ", document);
            this.AddNextNTEElement("RBC: No evidence of PNH Clone or GPI - linked antibodies", document);
            this.AddNextNTEElement("WBC - Granulocytes:	No evidence of PNH Clone or GPI - linked antibodies", document);
            this.AddNextNTEElement("WBC - Monocytes:	No evidence of PNH Clone or GPI - linked antibodies", document);
            this.AddNextNTEElement("", document);

            this.AddNextNTEElement("References: ", document);
            this.AddNextNTEElement(testOrder.ReportReferences, document);
            this.AddNextNTEElement("", document);
            
            this.AddNextNTEElement(testOrder.ASRComment, document);
            this.AddNextNTEElement("", document);

            string locationPerformed = testOrder.GetLocationPerformedComment();
            this.AddNextNTEElement(locationPerformed, document);
            this.AddNextNTEElement(string.Empty, document);
        }

        private void PositiveToXml(XElement document, PNHTestOrder testOrder)
        {
            this.AddCompanyHeader(document);
            this.AddNextNTEElement("PNH, Highly Sensitive(FLAER)", document);

            this.AddNextNTEElement("Result: " + testOrder.Result, document);
            this.AddNextNTEElement("", document);

            if (string.IsNullOrEmpty(testOrder.Comment) == false)
            {
                this.AddNextNTEElement("Comment: ", document);
                this.AddNextNTEElement(testOrder.Comment, document);
                this.AddNextNTEElement("", document);
            }

            this.AddNextNTEElement("Pathologist: " + testOrder.Signature, document);
            if (testOrder.FinalTime.HasValue == true)
            {
                this.AddNextNTEElement("E-signed " + testOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document);
            }
            this.AddNextNTEElement("", document);
            //this.AddAmendments(document);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testOrder.OrderedOn, testOrder.OrderedOnId);
            this.AddNextNTEElement("Specimen Description: " + specimenOrder.Description, document);
            this.AddNextNTEElement("", document);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextNTEElement("Collection Date/Time: " + collectionDateTimeString, document);
            this.AddNextNTEElement(string.Empty, document);

            this.AddNextNTEElement("Method: ", document);
            this.AddNextNTEElement(testOrder.Method, document);
            this.AddNextNTEElement("", document);

            PNHResult pnhResult = new PNHResult();
            pnhResult.SetTotals(testOrder);
            this.AddNextNTEElement("Result Data:)", document);
            this.AddNextNTEElement("RBC(Total)", document);
            this.AddNextNTEElement("PNH Clone (Type II + Type III):", document);
            this.AddNextNTEElement("Type III (complete CD59 deficiency) = " + pnhResult.RedBloodCellsTypeIIIResult.ToString("F") + "%", document);
            this.AddNextNTEElement("Type II (partial CD59 deficiency) = " + pnhResult.RedBloodCellsTypeIIResult.ToString("F") + "%", document);
            this.AddNextNTEElement("Result: " + pnhResult.RedBloodTotal.ToString("F") + "%", document);
            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("WBC - Granulocytes(Total)", document);
            this.AddNextNTEElement("PNH Clone (Type II + Type III):", document);
            this.AddNextNTEElement("TypeIII (complete FLAER/CD24 deficiency) = " + pnhResult.GranulocytesTypeIIIResult.ToString("F") + "%", document);
            if (pnhResult.GranulocytesTypeIIResult > 0.0m && pnhResult.GranulocytesTypeIIIResult > 0.0m)
            {
                this.AddNextNTEElement("TypeII (partial FLAER/CD24 deficiency) = " + pnhResult.GranulocytesTypeIIResult.ToString("F") + "%", document);
            }
            this.AddNextNTEElement("Result: " + pnhResult.GranulocytesTotal.ToString("F") + "%", document);
            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("WBC-Monocytes (Total)", document);
            this.AddNextNTEElement("TypeIII (complete FLAER/CD14 deficiency) = " + pnhResult.MonocytesTypeIIIResult.ToString("F") + "%", document);
            if (pnhResult.MonocytesTypeIIResult > 0.0m)
            {
                this.AddNextNTEElement("TypeII (partial FLAER/CD14 deficiency) = " + pnhResult.MonocytesTypeIIResult.ToString("F") + "%", document);
            }
            this.AddNextNTEElement("Result: " + pnhResult.MonocytesTotal.ToString("F") + "%", document);
            this.AddNextNTEElement("", document);

            if (testOrder.ResultCode == PNHPersistentPositiveResult.PNHPersistentPositiveResultResultCode || testOrder.ResultCode == PNHNegativeWithPreviousPositiveResult.PNHNegativeWithPreviousPositiveResultResultCode)
            {
                string dateString = string.Empty;
                if (testOrder.FinalDate.HasValue)
                {
                    dateString = testOrder.FinalDate.Value.ToShortDateString();
                }
                this.AddNextNTEElement("Current Result: " + dateString, document);
                this.AddNextNTEElement("RBC: " + pnhResult.RedBloodTotal.ToString("F") + "%", document);
                this.AddNextNTEElement("WBC-Granulocytes: " + pnhResult.GranulocytesTotal.ToString("F") + "%", document);
                this.AddNextNTEElement("WBC-Monocytes: " + pnhResult.MonocytesTotal.ToString("F") + "%", document);
                this.AddNextNTEElement("", document);

                this.SetPreviousResults(document, testOrder);
            }


            this.AddNextNTEElement("References: ", document);
            this.AddNextNTEElement(testOrder.ReportReferences, document);

            this.AddNextNTEElement("", document);
            this.AddNextNTEElement(testOrder.ASRComment, document);
            this.AddNextNTEElement("", document);

            string locationPerformed = testOrder.GetLocationPerformedComment();
            this.AddNextNTEElement(locationPerformed, document);
            this.AddNextNTEElement(string.Empty, document);
        }

        private void SetPreviousResults(XElement document, PNHTestOrder testOrder)
        {
            PNHResult pnhResult = new PNHResult();
            List<YellowstonePathology.Business.Test.AccessionOrder> accessionOrders = pnhResult.GetPreviousAccessions(this.m_AccessionOrder.PatientId);
            List<PNHTestOrder> pnhTestOrders = pnhResult.GetPreviousPanelSetOrders(accessionOrders, testOrder.MasterAccessionNo, testOrder.OrderDate.Value);

            for (int idx = 0; idx < pnhTestOrders.Count; idx++)
            {
                if (idx > 2) break;

                pnhResult.SetTotals(pnhTestOrders[idx]);
                this.AddNextNTEElement("Previous Result: " + pnhTestOrders[idx].FinalDate.Value.ToShortDateString(), document);
                this.AddNextNTEElement("RBC: " + pnhResult.RedBloodTotal.ToString("F") + "%", document);
                this.AddNextNTEElement("WBC-Granulocytes: " + pnhResult.GranulocytesTotal.ToString("F") + "%", document);
                this.AddNextNTEElement("WBC-Monocytes: " + pnhResult.MonocytesTotal.ToString("F") + "%", document);
                this.AddNextNTEElement("", document);
            }
        }
    }
}
