using System;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.NGCT
{
    public class NGCTEPICBeakerObxView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
    {
        public NGCTEPICBeakerObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {            
            NGCTTestOrder testOrder = (NGCTTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.m_ReportNo);

            this.AddNextObxElementBeaker("Report No", this.m_ReportNo, document, "F");
            this.AddNextObxElementBeaker("Chlamydia trachomatis result", testOrder.ChlamydiaTrachomatisResult, document, "F", "Negative", false);
            this.AddNextObxElementBeaker("Neisseria gonorrhoeae result", testOrder.NeisseriaGonorrhoeaeResult, document, "F", "Negative", false);

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

            this.AddNextObxElementBeaker("Specimen", "Thin Prep Fluid", document, "F");
            this.AddNextObxElementBeaker("Method", testOrder.Method, document, "F");
            this.AddNextObxElementBeaker("References", testOrder.ReportReferences, document, "F");
            this.AddNextObxElementBeaker("Test Information", testOrder.TestInformation, document, "F");

            string locationPerformed = testOrder.GetLocationPerformedComment();
            this.AddNextObxElementBeaker("Location Performed", locationPerformed, document, "F");            
        }        
    }
}
