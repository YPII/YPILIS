using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.PNH
{
	public class PNHEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
		public PNHEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
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
            this.AddHeader(document, testOrder, "PNH, Highly Sensitive(FLAER)");
            this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("Result: Negative (No evidence of paroxysmal nocturnal hemoglobinuria)", document, "F");
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Comment:", document, "F");
            this.AddNextObxElement("Flow cytometric analysis does not identify any evidence of a PNH clone, based on analysis of several different GPI-linked antibodies on 3 separate cell populations (red blood cells, monocytes and granulocytes).  These findings do not support the diagnosis of PNH.", document, "F");
            this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("Pathologist: " + testOrder.Signature, document, "F");
			if (testOrder.FinalTime.HasValue == true)
            {
				this.AddNextObxElement("E-signed " + testOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testOrder.OrderedOn, testOrder.OrderedOnId);
            this.AddNextObxElement("Specimen Description: " + specimenOrder.Description, document, "F");
            this.AddNextObxElement("", document, "F");

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Method: ", document, "F");
            this.HandleLongString(testOrder.Method, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Result Data: ", document, "F");
            this.AddNextObxElement("RBC: No evidence of PNH Clone or GPI - linked antibodies", document, "F");
            this.AddNextObxElement("WBC - Granulocytes:	No evidence of PNH Clone or GPI - linked antibodies", document, "F");
            this.AddNextObxElement("WBC - Monocytes:	No evidence of PNH Clone or GPI - linked antibodies", document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("References: ", document, "F");
            this.HandleLongString(testOrder.ReportReferences, document, "F");
            this.AddNextObxElement("", document, "F");
            
            this.HandleLongString(testOrder.ASRComment, document, "F");
            this.AddNextObxElement("", document, "F");

            string locationPerformed = testOrder.GetLocationPerformedComment();
            this.HandleLongString(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }

        private void PositiveToXml(XElement document, PNHTestOrder testOrder)
        {
            this.AddHeader(document, testOrder, "PNH, Highly Sensitive(FLAER)");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Result: " + testOrder.Result, document, "F");
            this.AddNextObxElement("", document, "F");

            if (string.IsNullOrEmpty(testOrder.Comment) == false)
            {
                this.AddNextObxElement("Comment: ", document, "F");
                this.AddNextObxElement(testOrder.Comment, document, "F");
                this.AddNextObxElement("", document, "F");
            }

            this.AddNextObxElement("Pathologist: " + testOrder.Signature, document, "F");
            if (testOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + testOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testOrder.OrderedOn, testOrder.OrderedOnId);
            this.AddNextObxElement("Specimen Description: " + specimenOrder.Description, document, "F");
            this.AddNextObxElement("", document, "F");

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Method: ", document, "F");
            this.HandleLongString(testOrder.Method, document, "F");
            this.AddNextObxElement("", document, "F");

            PNHResult pnhResult = new PNHResult();
            pnhResult.SetTotals(testOrder);
            this.AddNextObxElement("Result Data:)", document, "F");
            this.AddNextObxElement("RBC(Total)", document, "F");
            this.AddNextObxElement("PNH Clone (Type II + Type III):", document, "F");
            this.AddNextObxElement("Type III (complete CD59 deficiency) = " + pnhResult.RedBloodCellsTypeIIIResult.ToString("F") + "%", document, "F");
            this.AddNextObxElement("Type II (partial CD59 deficiency) = " + pnhResult.RedBloodCellsTypeIIResult.ToString("F") + "%", document, "F");
            this.AddNextObxElement("Result: " + pnhResult.RedBloodTotal.ToString("F") + "%", document, "F");
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("WBC - Granulocytes(Total)", document, "F");
            this.AddNextObxElement("PNH Clone (Type II + Type III):", document, "F");
            this.AddNextObxElement("TypeIII (complete FLAER/CD24 deficiency) = " + pnhResult.GranulocytesTypeIIIResult.ToString("F") + "%", document, "F");
            if (pnhResult.GranulocytesTypeIIResult > 0.0m && pnhResult.GranulocytesTypeIIIResult > 0.0m)
            {
                this.AddNextObxElement("TypeII (partial FLAER/CD24 deficiency) = " + pnhResult.GranulocytesTypeIIResult.ToString("F") + "%", document, "F");
            }
            this.AddNextObxElement("Result: " + pnhResult.GranulocytesTotal.ToString("F") + "%", document, "F");
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("WBC-Monocytes (Total)", document, "F");
            this.AddNextObxElement("TypeIII (complete FLAER/CD14 deficiency) = " + pnhResult.MonocytesTypeIIIResult.ToString("F") + "%", document, "F");
            if (pnhResult.MonocytesTypeIIResult > 0.0m)
            {
                this.AddNextObxElement("TypeII (partial FLAER/CD14 deficiency) = " + pnhResult.MonocytesTypeIIResult.ToString("F") + "%", document, "F");
            }
            this.AddNextObxElement("Result: " + pnhResult.MonocytesTotal.ToString("F") + "%", document, "F");
            this.AddNextObxElement("", document, "F");

            if (testOrder.ResultCode == PNHPersistentPositiveResult.PNHPersistentPositiveResultResultCode || testOrder.ResultCode == PNHNegativeWithPreviousPositiveResult.PNHNegativeWithPreviousPositiveResultResultCode)
            {
                string dateString = string.Empty;
                if (testOrder.FinalDate.HasValue)
                {
                    dateString = testOrder.FinalDate.Value.ToShortDateString();
                }
                this.AddNextObxElement("Current Result: " + dateString, document, "F");
                this.AddNextObxElement("RBC: " + pnhResult.RedBloodTotal.ToString("F") + "%", document, "F");
                this.AddNextObxElement("WBC-Granulocytes: " + pnhResult.GranulocytesTotal.ToString("F") + "%", document, "F");
                this.AddNextObxElement("WBC-Monocytes: " + pnhResult.MonocytesTotal.ToString("F") + "%", document, "F");
                this.AddNextObxElement("", document, "F");

                this.SetPreviousResults(document, testOrder);
            }


            this.AddNextObxElement("References: ", document, "F");
            this.HandleLongString(testOrder.ReportReferences, document, "F");

            this.AddNextObxElement("", document, "F");
            this.HandleLongString(testOrder.ASRComment, document, "F");
            this.AddNextObxElement("", document, "F");

            string locationPerformed = testOrder.GetLocationPerformedComment();
            this.HandleLongString(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
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
                this.AddNextObxElement("Previous Result: " + pnhTestOrders[idx].FinalDate.Value.ToShortDateString(), document, "F");
                this.AddNextObxElement("RBC: " + pnhResult.RedBloodTotal.ToString("F") + "%", document, "F");
                this.AddNextObxElement("WBC-Granulocytes: " + pnhResult.GranulocytesTotal.ToString("F") + "%", document, "F");
                this.AddNextObxElement("WBC-Monocytes: " + pnhResult.MonocytesTotal.ToString("F") + "%", document, "F");
                this.AddNextObxElement("", document, "F");
            }
        }
    }
}
