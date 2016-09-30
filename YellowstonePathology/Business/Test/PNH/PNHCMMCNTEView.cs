using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.PNH
{
    public class PNHCMMCNTEView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
    {
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

        public PNHCMMCNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;
        }

        public override void ToXml(XElement document)
        {
            PNHTestOrder testOrder = (PNHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            PNHNegativeResult pnhNegativeResult = new PNHNegativeResult();
            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);
            this.AddNextNteElement("PNH, Highly Sensitive(FLAER)", document);
            this.AddNextNteElement("Master Accession #: " + testOrder.MasterAccessionNo, document);
            this.AddNextNteElement("Report #: " + testOrder.ReportNo, document);
            this.AddBlankNteElement(document);

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
            this.AddNextNteElement("Result: Negative (No evidence of paroxysmal nocturnal hemoglobinuria)", document);
            this.AddBlankNteElement(document);
            this.AddNextNteElement("Comment:", document);
            this.AddNextNteElement("Flow cytometric analysis does not identify any evidence of a PNH clone, based on analysis of several different GPI-linked antibodies on 3 separate cell populations (red blood cells, monocytes and granulocytes).  These findings do not support the diagnosis of PNH.", document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Pathologist: " + testOrder.Signature, document);
            if (testOrder.FinalTime.HasValue == true)
            {
                this.AddNextNteElement("E-signed " + testOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document);
            }

            this.AddBlankNteElement(document);
            this.AddAmendments(document, testOrder);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testOrder.OrderedOn, testOrder.OrderedOnId);
            this.AddNextNteElement("Specimen Description: " + specimenOrder.Description, document);
            this.AddBlankNteElement(document);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextNteElement("Collection Date/Time: " + collectionDateTimeString, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Method: ", document);
            this.HandleLongString(testOrder.Method, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Result Data: ", document);
            this.AddNextNteElement("RBC: No evidence of PNH Clone or GPI - linked antibodies", document);
            this.AddNextNteElement("WBC - Granulocytes:	No evidence of PNH Clone or GPI - linked antibodies", document);
            this.AddNextNteElement("WBC - Monocytes:	No evidence of PNH Clone or GPI - linked antibodies", document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("References: ", document);
            this.HandleLongString(testOrder.ReportReferences, document);
            this.AddBlankNteElement(document);

            this.HandleLongString(testOrder.ASRComment, document);
            this.AddBlankNteElement(document);

            string locationPerformed = testOrder.GetLocationPerformedComment();
            this.HandleLongString(locationPerformed, document);
            this.AddBlankNteElement(document);
        }

        private void PositiveToXml(XElement document, PNHTestOrder testOrder)
        {
            this.AddNextNteElement("Result: " + testOrder.Result, document);
            this.AddBlankNteElement(document);

            if (string.IsNullOrEmpty(testOrder.Comment) == false)
            {
                this.AddNextNteElement("Comment: ", document);
                this.AddNextNteElement(testOrder.Comment, document);
                this.AddBlankNteElement(document);
            }

            this.AddNextNteElement("Pathologist: " + testOrder.Signature, document);
            if (testOrder.FinalTime.HasValue == true)
            {
                this.AddNextNteElement("E-signed " + testOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document);
            }

            this.AddBlankNteElement(document);
            this.AddAmendments(document, testOrder);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testOrder.OrderedOn, testOrder.OrderedOnId);
            this.AddNextNteElement("Specimen Description: " + specimenOrder.Description, document);
            this.AddBlankNteElement(document);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextNteElement("Collection Date/Time: " + collectionDateTimeString, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Method: ", document);
            this.HandleLongString(testOrder.Method, document);
            this.AddBlankNteElement(document);

            PNHResult pnhResult = new PNHResult();
            pnhResult.SetTotals(testOrder);
            this.AddNextNteElement("Result Data:)", document);
            this.AddNextNteElement("RBC(Total)", document);
            this.AddNextNteElement("PNH Clone (Type II + Type III):", document);
            this.AddNextNteElement("Type III (complete CD59 deficiency) = " + pnhResult.RedBloodCellsTypeIIIResult.ToString("F") + "%", document);
            this.AddNextNteElement("Type II (partial CD59 deficiency) = " + pnhResult.RedBloodCellsTypeIIResult.ToString("F") + "%", document);
            this.AddNextNteElement("Result: " + pnhResult.RedBloodTotal.ToString("F") + "%", document);
            this.AddNextNteElement("", document);
            this.AddNextNteElement("WBC - Granulocytes(Total)", document);
            this.AddNextNteElement("PNH Clone (Type II + Type III):", document);
            this.AddNextNteElement("TypeIII (complete FLAER/CD24 deficiency) = " + pnhResult.GranulocytesTypeIIIResult.ToString("F") + "%", document);
            if (pnhResult.GranulocytesTypeIIResult > 0.0m && pnhResult.GranulocytesTypeIIIResult > 0.0m)
            {
                this.AddNextNteElement("TypeII (partial FLAER/CD24 deficiency) = " + pnhResult.GranulocytesTypeIIResult.ToString("F") + "%", document);
            }

            this.AddNextNteElement("Result: " + pnhResult.GranulocytesTotal.ToString("F") + "%", document);
            this.AddNextNteElement("", document);
            this.AddNextNteElement("WBC-Monocytes (Total)", document);
            this.AddNextNteElement("TypeIII (complete FLAER/CD14 deficiency) = " + pnhResult.MonocytesTypeIIIResult.ToString("F") + "%", document);
            if (pnhResult.MonocytesTypeIIResult > 0.0m)
            {
                this.AddNextNteElement("TypeII (partial FLAER/CD14 deficiency) = " + pnhResult.MonocytesTypeIIResult.ToString("F") + "%", document);
            }

            this.AddNextNteElement("Result: " + pnhResult.MonocytesTotal.ToString("F") + "%", document);
            this.AddBlankNteElement(document);

            if (testOrder.ResultCode == PNHPersistentPositiveResult.PNHPersistentPositiveResultResultCode || testOrder.ResultCode == PNHNegativeWithPreviousPositiveResult.PNHNegativeWithPreviousPositiveResultResultCode)
            {
                string dateString = string.Empty;
                if (testOrder.FinalDate.HasValue)
                {
                    dateString = testOrder.FinalDate.Value.ToShortDateString();
                }

                this.AddNextNteElement("Current Result: " + dateString, document);
                this.AddNextNteElement("RBC: " + pnhResult.RedBloodTotal.ToString("F") + "%", document);
                this.AddNextNteElement("WBC-Granulocytes: " + pnhResult.GranulocytesTotal.ToString("F") + "%", document);
                this.AddNextNteElement("WBC-Monocytes: " + pnhResult.MonocytesTotal.ToString("F") + "%", document);
                this.AddBlankNteElement(document);

                this.SetPreviousResults(document, testOrder);
            }

            this.AddNextNteElement("References: ", document);
            this.HandleLongString(testOrder.ReportReferences, document);

            this.AddBlankNteElement(document);
            this.HandleLongString(testOrder.ASRComment, document);
            this.AddBlankNteElement(document);

            string locationPerformed = testOrder.GetLocationPerformedComment();
            this.HandleLongString(locationPerformed, document);
            this.AddBlankNteElement(document);
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
                this.AddNextNteElement("Previous Result: " + pnhTestOrders[idx].FinalDate.Value.ToShortDateString(), document);
                this.AddNextNteElement("RBC: " + pnhResult.RedBloodTotal.ToString("F") + "%", document);
                this.AddNextNteElement("WBC-Granulocytes: " + pnhResult.GranulocytesTotal.ToString("F") + "%", document);
                this.AddNextNteElement("WBC-Monocytes: " + pnhResult.MonocytesTotal.ToString("F") + "%", document);
                this.AddBlankNteElement(document);
            }
        }
    }
}
