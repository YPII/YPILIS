using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.Trichomonas
{
    public class TrichomonasEPICBeakerObxView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
    {
        public TrichomonasEPICBeakerObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            TrichomonasTestOrder panelSetOrder = (TrichomonasTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.m_ReportNo);

            this.AddNextObxElementBeaker("Report No", this.m_ReportNo, document, "F");
            this.AddNextObxElementBeaker("Trich Result", panelSetOrder.Result, document, "F", "Negative");            

            if (amendmentCollection.Count != 0)
            {
                StringBuilder amendments = new StringBuilder();
                foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
                {
                    if (amendment.Final == true)
                    {
                        amendments.AppendLine(amendment.AmendmentType + ": " + amendment.AmendmentDate.Value.ToString("MM/dd/yyyy"));
                        amendments.AppendLine(amendment.Text);
                        if (amendment.RequirePathologistSignature == true)
                        {
                            amendments.AppendLine("Signature: " + amendment.PathologistSignature);
                            amendments.AppendLine("E-signed " + amendment.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"));
                        }
                    }
                }
                amendments.AppendLine();
                this.AddNextObxElementBeaker("Amendments", amendments.ToString(), document, "F");
            }

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextObxElementBeaker("Specimen Description", specimenOrder.Description, document, "F");

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElementBeaker("Collection Date/Time", collectionDateTimeString, document, "F");

            this.AddNextObxElementBeaker("Method", panelSetOrder.Method, document, "F");

            this.AddNextObxElementBeaker("References", 
                "Jordan JA, Lowery D, Trucco M. Taqman-Based Detection of Trichomonas vaginalis DNA from female genitals specimens. " +
                "J Clin Micro 2001; 3819-22", document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElementBeaker("Location Performed", locationPerformed, document, "F");
        }
    }
}
