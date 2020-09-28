using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.PNH
{
	public class PNHEPICBeakerObxView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
    {
		public PNHEPICBeakerObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
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
            this.AddNextObxElementBeaker("Test Name", "PNH, Highly Sensitive(FLAER)", document, "F");
            this.AddNextObxElementBeaker("Report No", testOrder.ReportNo, document, "F");
            this.AddNextObxElementBeaker("Result", "Negative (No evidence of paroxysmal nocturnal hemoglobinuria)", document, "F");
            this.AddNextObxElementBeaker("Comment", "Flow cytometric analysis does not identify any evidence of a PNH clone, based on analysis of several different GPI-linked antibodies on 3 separate cell populations (red blood cells, monocytes and granulocytes).  These findings do not support the diagnosis of PNH.", document, "F");

			this.AddNextObxElementBeaker("Pathologist", testOrder.Signature, document, "F");
			if (testOrder.FinalTime.HasValue == true)
            {
				this.AddNextObxElementBeaker("E-signed", testOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }

            this.AddAmendments(document);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testOrder.OrderedOn, testOrder.OrderedOnId);
            this.AddNextObxElementBeaker("Specimen Description", specimenOrder.Description, document, "F");

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElementBeaker("Collection Date/Time", collectionDateTimeString, document, "F");
            
            this.AddNextObxElementBeaker("Method", testOrder.Method, document, "F");
            this.AddNextObxElementBeaker("RBC",  "No evidence of PNH Clone or GPI - linked antibodies", document, "F");
            this.AddNextObxElementBeaker("WBC - Granulocytes",	"No evidence of PNH Clone or GPI - linked antibodies", document, "F");
            this.AddNextObxElementBeaker("WBC - Monocytes",	"No evidence of PNH Clone or GPI - linked antibodies", document, "F");
            
            this.AddNextObxElementBeaker("References", testOrder.ReportReferences, document, "F");
            this.AddNextObxElementBeaker("Test Development", testOrder.ASRComment, document, "F");

            string locationPerformed = testOrder.GetLocationPerformedComment();
            this.AddNextObxElementBeaker("Location Performed", locationPerformed, document, "F");
        }

        private void PositiveToXml(XElement document, PNHTestOrder testOrder)
        {            
            this.AddNextObxElementBeaker("Test Name", "PNH, Highly Sensitive(FLAER)", document, "F");
            this.AddNextObxElementBeaker("Result", testOrder.Result, document, "F");

            if (string.IsNullOrEmpty(testOrder.Comment) == false)
            {
                this.AddNextObxElementBeaker("Comment", testOrder.Comment, document, "F");
            }

            this.AddNextObxElementBeaker("Pathologist", testOrder.Signature, document, "F");
            if (testOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElementBeaker("E-signed", testOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            
            this.AddAmendments(document);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testOrder.OrderedOn, testOrder.OrderedOnId);
            this.AddNextObxElementBeaker("Specimen Description", specimenOrder.Description, document, "F");            

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElementBeaker("Collection Date/Time", collectionDateTimeString, document, "F");
            this.AddNextObxElementBeaker("Method", testOrder.Method, document, "F");

            PNHResult pnhResult = new PNHResult();
            pnhResult.SetTotals(testOrder);
            this.AddNextObxElementBeaker("Type III (complete CD59 deficiency)", pnhResult.RedBloodCellsTypeIIIResult.ToString("F") + "%", document, "F");
            this.AddNextObxElementBeaker("Type II (partial CD59 deficiency)", pnhResult.RedBloodCellsTypeIIResult.ToString("F") + "%", document, "F");
            this.AddNextObxElementBeaker("Result", pnhResult.RedBloodTotal.ToString("F") + "%", document, "F");            
            this.AddNextObxElementBeaker("TypeIII (complete FLAER/CD24 deficiency)", pnhResult.GranulocytesTypeIIIResult.ToString("F") + "%", document, "F");
            if (pnhResult.GranulocytesTypeIIResult > 0.0m && pnhResult.GranulocytesTypeIIIResult > 0.0m)
            {
                this.AddNextObxElementBeaker("TypeII (partial FLAER/CD24 deficiency)", pnhResult.GranulocytesTypeIIResult.ToString("F") + "%", document, "F");
            }
            this.AddNextObxElementBeaker("Result", pnhResult.GranulocytesTotal.ToString("F") + "%", document, "F");
            this.AddNextObxElementBeaker("TypeIII (complete FLAER/CD14 deficiency)", pnhResult.MonocytesTypeIIIResult.ToString("F") + "%", document, "F");
            if (pnhResult.MonocytesTypeIIResult > 0.0m)
            {
                this.AddNextObxElementBeaker("TypeII (partial FLAER/CD14 deficiency)", pnhResult.MonocytesTypeIIResult.ToString("F") + "%", document, "F");
            }
            this.AddNextObxElementBeaker("Result", pnhResult.MonocytesTotal.ToString("F") + "%", document, "F");

            if (testOrder.ResultCode == PNHPersistentPositiveResult.PNHPersistentPositiveResultResultCode || testOrder.ResultCode == PNHNegativeWithPreviousPositiveResult.PNHNegativeWithPreviousPositiveResultResultCode)
            {
                string dateString = string.Empty;
                if (testOrder.FinalDate.HasValue)
                {
                    dateString = testOrder.FinalDate.Value.ToShortDateString();
                }
                this.AddNextObxElementBeaker("Current Result", dateString, document, "F");
                this.AddNextObxElementBeaker("RBC", pnhResult.RedBloodTotal.ToString("F") + "%", document, "F");
                this.AddNextObxElementBeaker("WBC-Granulocytes", pnhResult.GranulocytesTotal.ToString("F") + "%", document, "F");
                this.AddNextObxElementBeaker("WBC-Monocytes", pnhResult.MonocytesTotal.ToString("F") + "%", document, "F");                

                this.SetPreviousResults(document, testOrder);
            }
            
            this.AddNextObxElementBeaker("References", testOrder.ReportReferences, document, "F");
            this.AddNextObxElementBeaker("Test Development", testOrder.ASRComment, document, "F");

            string locationPerformed = testOrder.GetLocationPerformedComment();
            this.AddNextObxElementBeaker("Location Performed", locationPerformed, document, "F");
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
