using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HPV
{
    public class HPVNMHOBXView : Business.HL7View.NMH.NMHOBXView
    {
        public HPVNMHOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        //REPNO
        //HPV
        //AMENDM
        //SPEC
        //TESTINFO
        //REFER
        //ASRW
        //LOCAT

        //REPN
        //HPVRES
        //AMEND
        //SPECIMEN
        //TESTINF
        //REF
        //ASR
        //LOC


        public override void ToXml(XElement document)
        {
            HPVTestOrder panelSetOrder = (HPVTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.m_ReportNo);

            this.AddNextOBXElement("REPN", "Report No", this.m_ReportNo, document, "F");
            this.AddNextOBXElement("HPVRES", "HPV Result", panelSetOrder.Result, document, "F");

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
                this.AddNextOBXElement("AMEND", "Amendments", amendments.ToString(), document, "F");
            }            

            this.AddNextOBXElement("SPECIMEN", "Specimen", "ThinPrep fluid", document, "F");

            this.AddNextOBXElement("TESTINF", "Test Information", panelSetOrder.TestInformation, document, "F");

            this.AddNextOBXElement("REF", "References", panelSetOrder.ReportReferences, document, "F");

            this.AddNextOBXElement("ASR", "ASR", panelSetOrder.ASRComment, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextOBXElement("LOC", "Location Performed", locationPerformed, document, "F");            
        }
    }
}
