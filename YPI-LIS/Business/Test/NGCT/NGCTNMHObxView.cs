using System;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.NGCT
{
    public class NGCTNMHObxView : Business.HL7View.NMH.NMHOBXView
    {
        public NGCTNMHObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {            
            NGCTTestOrder testOrder = (NGCTTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.m_ReportNo);

            this.AddNextObxElementV2("RN", "Report No", this.m_ReportNo, document, "F");
            this.AddNextObxElementV2("CT", "Chlamydia trachomatis result", testOrder.ChlamydiaTrachomatisResult, document, "F");
            this.AddNextObxElementV2("NG", "Neisseria gonorrhoeae result", testOrder.NeisseriaGonorrhoeaeResult, document, "F");

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
                this.AddNextObxElementV2("AMEND", "Amendments", amendments.ToString(), document, "F");
            }

            this.AddNextObxElementV2("SPECDES", "Specimen", "Thin Prep Fluid", document, "F");
            this.AddNextObxElementV2("METHOD", "Method", testOrder.Method, document, "F");
            this.AddNextObxElementV2("REF", "References", testOrder.ReportReferences, document, "F");
            this.AddNextObxElementV2("TESTINF", "Test Information", testOrder.TestInformation, document, "F");

            string locationPerformed = testOrder.GetLocationPerformedComment();
            this.AddNextObxElementV2("LOC", "Location Performed", locationPerformed, document, "F");            
        }        
    }
}
