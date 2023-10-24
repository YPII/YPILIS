using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.BoneMarrowSummary
{
    public class BoneMarrowSummaryEPICOBXView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
    {
        public BoneMarrowSummaryEPICOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
            : base(accessionOrder, reportNo, obxCount)
        {

        }

        public override void ToXml(XElement document)
        {
            PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            //Add the first element as narrative for Nikki to see.
            Business.HL7View.EPIC.EPICBeakerNarrativeOBXView.AddElement(document);

            this.AddNextNTEElement("Bone Marrow Summary", document);
            this.AddNextNTEElement("", document);

            this.AddNextNTEElement("SURGICAL PATHOLOGY DIAGNOSIS: ", document);

            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            this.AddNextNTEElement("Reference Report No: " + surgicalTestOrder.ReportNo, document);

            foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
            {
                this.AddNextNTEElement("Specimen: " + surgicalSpecimen.DiagnosisIdFormatted + "  " + surgicalSpecimen.SpecimenOrder.Description, document);
                this.AddNextNTEElement("Diagnosis: " + surgicalSpecimen.Diagnosis, document);
            }

            if (string.IsNullOrEmpty(surgicalTestOrder.Comment) == false)
            {
                this.AddNextNTEElement("Comment: " + surgicalTestOrder.Comment, document);
            }

            this.AddNextNTEElement("", document);
            this.AddAmendmentsNTE(document, panelSetOrder, this.m_AccessionOrder);

            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("TESTING SUMMARY:", document);

            List<Business.Test.PanelSetOrder> testingSummaryList = this.m_AccessionOrder.PanelSetOrderCollection.GetBoneMarrowAccessionSummaryList(panelSetOrder.ReportNo, true);
            int surgicalPanelSetId = new Test.Surgical.SurgicalTest().PanelSetId;

            for (int idx = testingSummaryList.Count - 1;  idx > -1; idx--)
            {
                Business.Test.PanelSetOrder pso = testingSummaryList[idx];
                if (pso.PanelSetId != surgicalPanelSetId && pso.IncludeOnSummaryReport == true)
                {
                    this.AddNextNTEElement("Reference Report No: " + pso.ReportNo, document);
                    this.AddNextNTEElement("Test Name: " + pso.PanelSetName, document);
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
                    this.AddNextNTEElement(result, document);
                    this.AddNextNTEElement("", document);
                }
            }

            this.AddNextNTEElement("", document);
            this.AddNextNTEElement("Pathologist: " + panelSetOrder.Signature, document);

            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextNTEElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document);
            }
        }
    }
}
