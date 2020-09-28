using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HPV
{
    public class HPVEPICBeakerObxView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
    {

        public HPVEPICBeakerObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            string observationResultStatus = "F";

            HPVTestOrder panelSetOrder = (HPVTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.m_ReportNo);

            this.AddNextObxElementBeaker("Report No", this.m_ReportNo, document, observationResultStatus);            
            this.AddNextObxElementBeaker("HPV Result", panelSetOrder.Result, document, observationResultStatus, "Negative");

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
                this.AddNextObxElementBeaker("Amendments", amendments.ToString(), document, observationResultStatus);
            }

            this.AddNextObxElementBeaker("Specimen", "ThinPrep fluid", document, observationResultStatus);

            this.AddNextObxElementBeaker("Test Information", panelSetOrder.TestInformation, document, observationResultStatus);

            this.AddNextObxElementBeaker("References", panelSetOrder.ReportReferences, document, observationResultStatus);

            this.AddNextObxElementBeaker("ASR", panelSetOrder.ASRComment, document, observationResultStatus);

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElementBeaker("Location Performed", locationPerformed, document, observationResultStatus);            
        }
    }
}
