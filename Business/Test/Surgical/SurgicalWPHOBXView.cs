using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.Surgical
{
    public class SurgicalWPHOBXView : YellowstonePathology.Business.HL7View.WPH.WPHOBXView
    {
        public SurgicalWPHOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            SurgicalTestOrder panelSetOrderSurgical = (SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddHeader(document, panelSetOrderSurgical, "Surgical Pathology Report");
            this.AddNextObxElement("", document, "F");

            foreach (SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
            {
                this.AddNextObxElement("Specimen: " + surgicalSpecimen.SpecimenOrder.SpecimenNumber.ToString(), document, "F");
                this.HandleLongString(surgicalSpecimen.SpecimenOrder.Description, document, "F");
                this.AddNextObxElement("", document, "F");

                this.AddNextObxElement("Diagnosis: ", document, "F");
                this.HandleLongString(surgicalSpecimen.Diagnosis, document, "F");
                this.AddNextObxElement("", document, "F");
            }

            if (string.IsNullOrEmpty(panelSetOrderSurgical.Comment) == false)
            {
                this.AddNextObxElement("Comment: ", document, "F");
                this.HandleLongString(panelSetOrderSurgical.Comment, document, "F");
                this.AddNextObxElement("", document, "F");
            }

            if (string.IsNullOrEmpty(panelSetOrderSurgical.CancerSummary) == false)
            {
                this.AddNextObxElement("Cancer Summary: ", document, "F");
                this.HandleLongString(panelSetOrderSurgical.CancerSummary, document, "F");
                this.AddNextObxElement("", document, "F");

                if (string.IsNullOrEmpty(panelSetOrderSurgical.AJCCStage) == false)
                {
                    this.HandleLongString("Pathologic TNM Stage: " + panelSetOrderSurgical.AJCCStage, document, "F");
                    this.AddNextObxElement(string.Empty, document, "F");
                }
            }

            this.AddNextObxElement("Pathologist: " + panelSetOrderSurgical.Signature, document, "F");
            if (panelSetOrderSurgical.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrderSurgical.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement("", document, "F");

            this.AddAmendments(document);

            this.AddNextObxElement("Microscopic Description: ", document, "F");
            this.HandleLongString(panelSetOrderSurgical.MicroscopicX, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            if (panelSetOrderSurgical.TypingStainCollection.Count > 0)
            {
                this.AddNextObxElement("Ancillary Studies:", document, "F");
                string ancillaryComment = panelSetOrderSurgical.GetAncillaryStudyComment();
                this.HandleLongString(ancillaryComment, document, "F");

                foreach (SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
                {
                    if (surgicalSpecimen.StainResultItemCollection.Count > 0)
                    {
                        this.HandleLongString(surgicalSpecimen.SpecimenOrder.SpecimenNumber.ToString() + ". " + surgicalSpecimen.SpecimenOrder.Description, document, "F");

                        foreach (YellowstonePathology.Business.SpecialStain.StainResultItem stainResultItem in surgicalSpecimen.StainResultItemCollection)
                        {
                            if (stainResultItem.Reportable)
                            {
                                string stainResult = stainResultItem.Result;
                                if (string.IsNullOrEmpty(stainResult) == true)
                                {
                                    stainResult = "Pending";
                                }
                                else if (stainResult.ToUpper() == "SEE COMMENT")
                                {
                                    stainResult = stainResultItem.ReportComment;
                                }
                                else
                                {
                                    string specialStainReportComment = stainResultItem.ReportComment;

                                    if (!string.IsNullOrEmpty(specialStainReportComment))
                                    {
                                        stainResult += " - " + specialStainReportComment;
                                    }
                                }

                                this.HandleLongString("Test: " + stainResultItem.ProcedureName + "  Result: " + stainResult, document, "F");
                            }
                        }
                        this.AddNextObxElement(string.Empty, document, "F");
                    }
                }
            }

            this.AddNextObxElement("Gross Description: ", document, "F");
            this.HandleLongString(panelSetOrderSurgical.GrossX, document, "F");
            this.AddNextObxElement("", document, "F");


            string immunoComment = panelSetOrderSurgical.GetImmunoComment();
            if (immunoComment.Length > 0)
            {
                this.HandleLongString(immunoComment, document, "F");
                this.AddNextObxElement(string.Empty, document, "F");
            }

            string locationPerformed = panelSetOrderSurgical.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}