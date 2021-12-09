using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ThinPrepPap
{
    public class ThinPrepPapNMHOBXView : Business.HL7View.NMH.NMHOBXView
    {
        public ThinPrepPapNMHOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        //REPN     -Report No
        //ECD      -Epithelial Cell Description
        //OTHER    -Other
        //SPECDES  -Specimen Description
        //SPECADEQ -Specimen Adequacy
        //COM      -Comment
        //FINALBY  -Finaled By
        //ESIGN    -E-sign
        //AMEND    -Amendments
        //SCREEN   -Screening Method
        //REF      -References
        //ASR      -ASR
        //LOC      -Location Performed

        public override void ToXml(XElement document)
        {                        
            /*
            YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.m_ReportNo);

            this.AddNextOBXElement("REPN", "Report No", this.m_ReportNo, document, "F");

            this.AddNextOBXElement("ECD", "Epithelial Cell Description", panelSetOrderCytology.ScreeningImpression.ToUpper(), document, "F");

            string otherConditions = panelSetOrderCytology.OtherConditions;
            if (string.IsNullOrEmpty(otherConditions) == true)
            {
                otherConditions = "None.";
            }
            this.AddNextOBXElement("Other", "Other", otherConditions, document, "F");

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrderCytology.OrderedOn, panelSetOrderCytology.OrderedOnId);
            this.AddNextOBXElement("SPECDES", "Specimen Description", specimenOrder.Description, document, "F");

            this.AddNextOBXElement("SPECADEQ", "Specimen Adequacy", panelSetOrderCytology.SpecimenAdequacy, document, "F");

            if (string.IsNullOrEmpty(panelSetOrderCytology.ReportComment) == false)
            {
                this.AddNextOBXElement("COM", "Comment", panelSetOrderCytology.ReportComment, document, "F");
            }

            this.AddNextOBXElement("FINALBY", "Finaled By", panelSetOrderCytology.Signature, document, "F");
            if (panelSetOrderCytology.FinalTime.HasValue == true)
            {
                this.AddNextOBXElement("ESIGN", "E-signed", panelSetOrderCytology.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
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
                this.AddNextOBXElement("AMEND", "Amendments", amendments.ToString(), document, "C");
            }

            this.AddNextOBXElement("SCREEN", "Screening Method", panelSetOrderCytology.Method, document, "F");

            this.AddNextOBXElement("REF", "References", panelSetOrderCytology.ReportReferences, document, "F");

            string disclaimer = "This Pap test is only a screening test. A negative result does not definitively rule out the presence of disease. Women should, therefore, in consultation with their physician, have this test performed at mutually agreed intervals.";
            this.AddNextOBXElement("ASR", "ASR", disclaimer, document, "F");

            string locationPerformed = panelSetOrderCytology.GetLocationPerformedComment();
            this.AddNextOBXElement("LOC", "Location Performed", locationPerformed, document, "F");
            */
        }
    }
}
