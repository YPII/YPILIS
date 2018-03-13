﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.Surgical
{
    public class SurgicalECWOBXView : YellowstonePathology.Business.HL7View.ECW.ECWOBXView
    {
        public SurgicalECWOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
            : base(accessionOrder, reportNo, obxCount)
        {

        }

        public override void ToXml(XElement document)
        {
            SurgicalTestOrder panelSetOrderSurgical = (SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddHeader(document, panelSetOrderSurgical, "Surgical Pathology Report");
            this.AddNextObxElement("", document, "F");

            this.InformRevisedDiagnosis(document, panelSetOrderSurgical.AmendmentCollection);

            foreach (SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
            {
                this.HandleLongString(surgicalSpecimen.SpecimenOrder.SpecimenNumber.ToString() + ".) " + surgicalSpecimen.SpecimenOrder.Description, document, "F");

                if (this.SpecimenHasERPR(surgicalSpecimen.SpecimenOrder, panelSetOrderSurgical) == true)
                {
                    this.AddNextObxElement("Fixation type: " + surgicalSpecimen.SpecimenOrder.LabFixation, document, "F");
                    this.AddNextObxElement("Time to fixation: " + surgicalSpecimen.SpecimenOrder.TimeToFixationHourString, document, "F");
                    this.AddNextObxElement("Duration of Fixation: " + surgicalSpecimen.SpecimenOrder.FixationDurationString, document, "F");
                }
                this.AddNextObxElement("", document, "F");

                this.AddNextObxElement("Diagnosis: ", document, "F");
                this.HandleLongString(surgicalSpecimen.Diagnosis, document, "F");
                this.AddNextObxElement("", document, "C");
            }

            this.AddNextObxElement("", document, "F");

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

            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in panelSetOrderSurgical.AmendmentCollection)
            {
                if (amendment.Final == true)
                {
                    this.AddNextObxElement(amendment.AmendmentType + ": " + amendment.AmendmentDate.Value.ToString("MM/dd/yyyy"), document, "C");
                    this.HandleLongString(amendment.Text, document, "C");
                    if (amendment.RequirePathologistSignature == true)
                    {
                        string signatureTitle = "E-Signed " + amendment.FinalDate.Value.ToShortDateString() + " " + amendment.FinalTime.Value.ToShortTimeString();
                        this.AddNextObxElement(signatureTitle, document, "C");
                    }
                    this.AddNextObxElement("", document, "C");

                    if (amendment.RevisedDiagnosis == true || amendment.ShowPreviousDiagnosis == true)
                    {
                        string amendmentId = amendment.AmendmentId;
                        foreach (YellowstonePathology.Business.Test.Surgical.SurgicalAudit surgicalAudit in panelSetOrderSurgical.SurgicalAuditCollection)
                        {
                            if (surgicalAudit.AmendmentId == amendmentId)
                            {
                                string finalDateP = YellowstonePathology.Business.BaseData.GetShortDateString(panelSetOrderSurgical.FinalDate);
                                finalDateP += " " + YellowstonePathology.Business.BaseData.GetMillitaryTimeString(panelSetOrderSurgical.FinalTime);

                                string previousDiagnosisHeader = "Previous diagnosis on " + finalDateP;
                                this.AddNextObxElement(previousDiagnosisHeader, document, "C");

                                foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenAudit specimenAudit in surgicalAudit.SurgicalSpecimenAuditCollection)
                                {
                                    if (specimenAudit.AmendmentId == amendmentId)
                                    {
                                        string diagnosisIDP = specimenAudit.DiagnosisId + ".";
                                        string specimenTypeP = specimenAudit.SpecimenOrder.Description + ":";
                                        this.AddNextObxElement(diagnosisIDP + specimenTypeP, document, "C");

                                        this.HandleLongString(specimenAudit.Diagnosis, document, "C");
                                    }
                                }

                                YellowstonePathology.Business.User.SystemUser pathologistUser = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(surgicalAudit.PathologistId);
                                this.AddNextObxElement(pathologistUser.Signature, document, "C");
                                this.AddNextObxElement("", document, "F");
                            }
                        }
                    }
                }
            }

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

            this.AddNextObxElement("Clinical Info: ", document, "F");
            this.HandleLongString(this.m_AccessionOrder.ClinicalHistory, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Additional Testing: ", document, "F");
            this.HandleLongString(this.m_AccessionOrder.PanelSetOrderCollection.GetAdditionalTestingString(panelSetOrderSurgical.ReportNo), document, "F");
            this.AddNextObxElement("", document, "F");

            string immunoComment = panelSetOrderSurgical.GetImmunoComment();
            if (string.IsNullOrEmpty(immunoComment) == false)
            {
                this.HandleLongString(immunoComment, document, "F");
                this.AddNextObxElement(string.Empty, document, "F");
            }

            string locationPerformed = panelSetOrderSurgical.GetLocationPerformedComment();
            this.HandleLongString(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }

        private void InformRevisedDiagnosis(XElement document, YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection)
        {
            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
            {
                if (amendment.Final == true && (amendment.RevisedDiagnosis == true || amendment.ShowPreviousDiagnosis == true))
                {
                    this.AddNextObxElement("Showing Revised Diagnosis", document, "F");
                    this.AddNextObxElement("", document, "F");
                    break;
                }
            }
        }

        private bool SpecimenHasERPR(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, SurgicalTestOrder panelSetOrder)
        {
            bool result = false;

            YellowstonePathology.Business.Test.Model.TestOrderCollection testOrders = specimenOrder.GetTestOrders(panelSetOrder.GetTestOrders());
            if (testOrders.ExistsByTestId("99") == true)
            {
                result = true;
            }
            return result;
        }
    }
}
