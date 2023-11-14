using System;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.NGCT
{
    public class NGCTEPICBeakerNTEView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
    {
        public NGCTEPICBeakerNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            Business.HL7View.EPIC.EPICBeakerNarrativeOBXView.AddElement(document);

            NGCTTestOrder testOrder = (NGCTTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.m_ReportNo);

            this.AddCompanyHeaderNTE(document);

            this.AddNextNTEElement("Report No: " + this.m_ReportNo, document);
            this.AddNextNTEElement("Chlamydia trachomatis result: " + testOrder.ChlamydiaTrachomatisResult, document);
            this.AddNextNTEElement("Neisseria gonorrhoeae result: " +testOrder.NeisseriaGonorrhoeaeResult, document);

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
                this.AddNextNTEElement("Amendments" + amendments.ToString(), document);
            }

            this.AddNextNTEElement("Specimen: Thin Prep Fluid", document);
            this.AddNextNTEElement("Method: " + testOrder.Method, document);
            this.AddNextNTEElement("References: " + testOrder.ReportReferences, document);
            this.AddNextNTEElement("Test Information: " + testOrder.TestInformation, document);

            string locationPerformed = testOrder.GetLocationPerformedComment();
            this.AddNextNTEElement("Location Performed: " +locationPerformed, document);            
        }        
    }
}
