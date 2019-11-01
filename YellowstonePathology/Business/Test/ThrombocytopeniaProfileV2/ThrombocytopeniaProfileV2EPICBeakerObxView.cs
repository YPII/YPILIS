using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ThrombocytopeniaProfileV2
{
    public class ThrombocytopeniaProfileV2EPICBeakerObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        public ThrombocytopeniaProfileV2EPICBeakerObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            ThrombocytopeniaProfileV2TestOrder panelSetOrder = (ThrombocytopeniaProfileV2TestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.m_ReportNo);

            this.AddNextObxElementBeaker("Report No", this.m_ReportNo, document, "F");

            this.AddNextObxElementBeaker("Anti-Platelet Antibody - IgG", panelSetOrder.AntiPlateletAntibodyIgG, document, "F");
            this.AddNextObxElementBeaker("Reference", panelSetOrder.AntiPlateletAntibodyIgGReference, document, "F");

            this.AddNextObxElementBeaker("Anti-Platelet Antibody - IgM", panelSetOrder.AntiPlateletAntibodyIgM, document, "F");
            this.AddNextObxElementBeaker("Reference", panelSetOrder.AntiPlateletAntibodyIgMReference, document, "F");

            if (string.IsNullOrEmpty(panelSetOrder.ReticulatedPlateletAnalysis) == false)
            {
                this.AddNextObxElementBeaker("Reticulated Platelet Analysis", panelSetOrder.ReticulatedPlateletAnalysis, document, "F");
                this.AddNextObxElementBeaker("Reference Range", panelSetOrder.ReticulatedPlateletAnalysisReference, document, "F");
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

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextObxElementBeaker("Specimen", specimenOrder.Description, document, "F");

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElementBeaker("Collection Date/Time", collectionDateTimeString, document, "F");

            this.AddNextObxElementBeaker("Interpretation", panelSetOrder.Interpretation, document, "F");

            this.AddNextObxElementBeaker("Method", panelSetOrder.Method, document, "F");

            this.AddNextObxElementBeaker("ASR", panelSetOrder.ASRComment, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElementBeaker("Location Performed", locationPerformed, document, "F");
        }
    }
}
