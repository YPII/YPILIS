using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HPV1618
{
    public class HPV1618EPICBeakerObxView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
    {
		public HPV1618EPICBeakerObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            PanelSetOrderHPV1618 panelSetOrder = (PanelSetOrderHPV1618)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.m_ReportNo);

            this.AddNextObxElementBeaker("Report No", this.m_ReportNo, document, "F");
            this.AddNextObxElementBeaker("HPV-16 Result", panelSetOrder.HPV16Result, document, "F", "Negative");            
            this.AddNextObxElementBeaker("HPV-18/45 Result", panelSetOrder.HPV18Result, document, "F", "Negative");            

            if (string.IsNullOrEmpty(panelSetOrder.Comment) == false)
            {
                this.AddNextObxElementBeaker("Comment", "Comment" + panelSetOrder.Comment, document, "F");
            }

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

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(panelSetOrder.OrderedOnId);
            this.AddNextObxElementBeaker("Specimen", specimenOrder.GetDescription(), document, "F");

            this.AddNextObxElementBeaker("Method", panelSetOrder.Method, document, "F");

            this.AddNextObxElementBeaker("References", panelSetOrder.ReportReferences, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElementBeaker("Location Performed", locationPerformed, document, "F");
        }
    }
}
