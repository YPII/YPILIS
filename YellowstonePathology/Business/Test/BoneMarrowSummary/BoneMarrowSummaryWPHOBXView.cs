using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.BoneMarrowSummary
{
    public class BoneMarrowSummaryWPHOBXView : YellowstonePathology.Business.HL7View.WPH.WPHOBXView
    {
        public BoneMarrowSummaryWPHOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
            : base(accessionOrder, reportNo, obxCount)
        {

        }

        public override void ToXml(XElement document)
        {
            PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, "Bone Marrow Summary");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("SURGICAL PATHOLOGY DIAGNOSIS: ", document, "F");

            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            this.AddNextObxElement("Reference Report No: " + surgicalTestOrder.ReportNo, document, "F");

            foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
            {
                this.HandleLongString("Specimen: " + surgicalSpecimen.DiagnosisIdFormatted + "  " + surgicalSpecimen.SpecimenOrder.Description, document, "F");
                this.HandleLongString("Diagnosis: " + surgicalSpecimen.Diagnosis, document, "F");
            }

            if (string.IsNullOrEmpty(surgicalTestOrder.Comment) == false)
            {
                this.HandleLongString("Comment: " + surgicalTestOrder.Comment, document, "F");
            }

            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("TESTING SUMMARY:", document, "F");

            List<Business.Test.PanelSetOrder> testingSummaryList = this.m_AccessionOrder.PanelSetOrderCollection.GetBoneMarrowAccessionSummaryList(panelSetOrder.ReportNo, true);
            int surgicalPanelSetId = new Test.Surgical.SurgicalTest().PanelSetId;

            for (int idx = testingSummaryList.Count - 1; idx > -1; idx--)
            {
                Business.Test.PanelSetOrder pso = testingSummaryList[idx];
                if (pso.PanelSetId != surgicalPanelSetId && pso.IncludeOnSummaryReport == true)
                {
                    this.AddNextObxElement("Reference Report No: " + pso.ReportNo, document, "F");
                    this.AddNextObxElement("Test Name: " + pso.PanelSetName, document, "F");
                    string result = pso.ToResultString(this.m_AccessionOrder);
                    if (result == "The result string for this test has not been implemented.")
                    {
                        if (string.IsNullOrEmpty(pso.SummaryComment) == false)
                        {
                            result = pso.SummaryComment;
                        }
                        else
                        {
                            result = "Result reported separately.";
                        }
                    }
                    this.HandleLongString(result, document, "F");
                    this.AddNextObxElement("", document, "F");
                }
            }

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Pathologist: " + panelSetOrder.Signature, document, "F");

            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }

            this.HandleLongString(panelSetOrder.GetLocationPerformedComment(), document, "F");
        }
    }
}
