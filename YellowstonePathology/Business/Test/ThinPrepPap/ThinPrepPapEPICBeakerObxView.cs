using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ThinPrepPap
{
    public class ThinPrepPapEPICBeakerObxView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
    {
        public ThinPrepPapEPICBeakerObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.m_ReportNo);

            this.AddNextObxElementBeaker("Report No", this.m_ReportNo, document, "F");

            this.AddNextObxElementBeaker("Epithelial Cell Description", panelSetOrderCytology.ScreeningImpression.ToUpper(), document, "F");

            string otherConditions = panelSetOrderCytology.OtherConditions;
            if (string.IsNullOrEmpty(otherConditions) == true)
            {
                otherConditions = "None.";
            }
            this.AddNextObxElementBeaker("Other Conditions", otherConditions, document, "F");

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrderCytology.OrderedOn, panelSetOrderCytology.OrderedOnId);
            this.AddNextObxElementBeaker("Specimen Description", specimenOrder.Description, document, "F");

            this.AddNextObxElementBeaker("Specimen Adequacy", panelSetOrderCytology.SpecimenAdequacy, document, "F");

            if (string.IsNullOrEmpty(panelSetOrderCytology.ReportComment) == false)
            {
                this.AddNextObxElementBeaker("Comment", panelSetOrderCytology.ReportComment, document, "F");
            }

            this.AddNextObxElementBeaker("Finaled By", panelSetOrderCytology.Signature, document, "F");
            if (panelSetOrderCytology.FinalTime.HasValue == true)
            {
                this.AddNextObxElementBeaker("E-signed", panelSetOrderCytology.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
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

            this.AddNextObxElementBeaker("Screening Method", panelSetOrderCytology.Method, document, "F");

            this.AddNextObxElementBeaker("References", panelSetOrderCytology.ReportReferences, document, "F");

            string disclaimer = "This Pap test is only a screening test. A negative result does not definitively rule out the presence of disease. Women should, therefore, in consultation with their physician, have this test performed at mutually agreed intervals.";
            this.AddNextObxElementBeaker("ASR", disclaimer, document, "F");

            string locationPerformed = panelSetOrderCytology.GetLocationPerformedComment();
            this.AddNextObxElementBeaker("Location Performed", locationPerformed, document, "F");
        }
    }
}
