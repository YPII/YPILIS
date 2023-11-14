using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.Surgical
{
    public class SurgicalRiverstoneOBXView : Business.HL7View.Riverstone.RiverstoneOBXView
    {
        public SurgicalRiverstoneOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
            : base(accessionOrder, reportNo, obxCount)
        {

        }

        public override void ToXml(XElement document, string resultStatus)
        {
            SurgicalTestOrder panelSetOrderSurgical = (SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddNextObxWithAttributeElement("SRGCLRESULT^Surgical Pathology", "See narrative", document, resultStatus);

            this.AddNextNteElement("Surgical Pathology Report", document);            

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(panelSetOrderSurgical.ReportNo);
            this.InformRevisedDiagnosis(document, amendmentCollection);

            foreach (SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
            {
                this.HandleLongStringAddNTE($"{surgicalSpecimen.SpecimenOrder.SpecimenNumber.ToString()}.) {surgicalSpecimen.SpecimenOrder.Description}", document);
                YellowstonePathology.Business.Helper.DateTimeJoiner collectionDateTimeJoiner = new YellowstonePathology.Business.Helper.DateTimeJoiner(surgicalSpecimen.SpecimenOrder.CollectionDate.Value, surgicalSpecimen.SpecimenOrder.CollectionTime);
                this.AddNextNteElement($"Collection Date/Time: {collectionDateTimeJoiner.DisplayString}", document);

                YellowstonePathology.Business.Test.Model.TestOrderCollection specimenTestOrders = surgicalSpecimen.SpecimenOrder.GetTestOrders(panelSetOrderSurgical.GetTestOrders());
                if (this.ERPRExistsInCollection(specimenTestOrders) == true)
                {
                    this.AddNextNteElement($"Fixation type: {surgicalSpecimen.SpecimenOrder.LabFixation}", document);
                    this.AddNextNteElement($"Time to fixation: {surgicalSpecimen.SpecimenOrder.TimeToFixationHourString}", document);
                    this.AddNextNteElement($"Duration of Fixation: {surgicalSpecimen.SpecimenOrder.FixationDurationString}", document);
                }
                this.AddNextNteElement("", document);

                this.AddNextNteElement("Diagnosis: ", document);
                this.HandleLongStringAddNTE(surgicalSpecimen.Diagnosis, document);
                this.AddNextNteElement("", document);
            }

            this.AddNextNteElement("", document);

            if (string.IsNullOrEmpty(panelSetOrderSurgical.Comment) == false)
            {
                this.AddNextNteElement("Comment: ", document);
                this.HandleLongStringAddNTE(panelSetOrderSurgical.Comment, document);
                this.AddNextNteElement("", document);
            }

            if (string.IsNullOrEmpty(panelSetOrderSurgical.CancerSummary) == false)
            {
                this.AddNextNteElement("Cancer Summary: ", document);
                this.HandleLongStringAddNTE(panelSetOrderSurgical.CancerSummary, document);
                this.AddNextNteElement("", document);

                if (string.IsNullOrEmpty(panelSetOrderSurgical.AJCCStage) == false)
                {
                    this.HandleLongStringAddNTE("Pathologic TNM Stage: " + panelSetOrderSurgical.AJCCStage, document);
                    this.AddNextNteElement(string.Empty, document);
                }
            }

            this.AddNextNteElement("Pathologist: " + panelSetOrderSurgical.Signature, document);
            if (panelSetOrderSurgical.FinalTime.HasValue == true)
            {
                this.AddNextNteElement("E-signed " + panelSetOrderSurgical.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document);
            }
            this.AddNextNteElement("", document);

            this.AddAmendments(document);

            this.AddNextNteElement("Microscopic Description: ", document);
            this.HandleLongStringAddNTE(panelSetOrderSurgical.MicroscopicX, document);
            this.AddNextNteElement(string.Empty, document);

            if (panelSetOrderSurgical.SurgicalSpecimenCollection.HasIC() == true)
            {
                foreach (SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
                {
                    if (surgicalSpecimen.IntraoperativeConsultationResultCollection.Count != 0)
                    {
                        foreach (IntraoperativeConsultationResult icItem in surgicalSpecimen.IntraoperativeConsultationResultCollection)
                        {
                            this.AddNextNteElement(surgicalSpecimen.DiagnosisId + ". " + surgicalSpecimen.SpecimenOrder.Description, document);
                            this.AddNextNteElement(icItem.Result, document);
                        }
                    }
                }
                this.AddNextNteElement(string.Empty, document);
            }

            if (panelSetOrderSurgical.TypingStainCollection.Count > 0)
            {
                this.AddNextNteElement("Ancillary Studies:", document);
                string ancillaryComment = panelSetOrderSurgical.GetAncillaryStudyComment();
                this.HandleLongStringAddNTE(ancillaryComment, document);

                foreach (SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
                {
                    if (surgicalSpecimen.StainResultItemCollection.Count > 0)
                    {
                        this.HandleLongStringAddNTE(surgicalSpecimen.SpecimenOrder.SpecimenNumber.ToString() + ". " + surgicalSpecimen.SpecimenOrder.Description, document);

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

                                this.HandleLongStringAddNTE("Test: " + stainResultItem.ProcedureName + "  Result: " + stainResult, document);
                            }
                        }
                        this.AddNextNteElement(string.Empty, document);
                    }
                }
            }

            this.AddNextNteElement("Clinical Information: ", document);
            if (string.IsNullOrEmpty(this.m_AccessionOrder.ClinicalHistory) == false)
            {
                this.HandleLongStringAddNTE(this.m_AccessionOrder.ClinicalHistory, document);
            }
            else
            {
                this.AddNextNteElement("none", document);

            }
            this.AddNextNteElement("", document);

            this.AddNextNteElement("Gross Description: ", document);
            this.HandleLongStringAddNTE(panelSetOrderSurgical.GrossX, document);
            this.AddNextNteElement("", document);

            this.AddNextNteElement("Additional Testing: ", document);
            this.HandleLongStringAddNTE(this.m_AccessionOrder.PanelSetOrderCollection.GetAdditionalTestingString(panelSetOrderSurgical.ReportNo), document);
            this.AddNextNteElement("", document);

            string immunoComment = panelSetOrderSurgical.GetImmunoComment();
            if (string.IsNullOrEmpty(immunoComment) == false)
            {
                this.HandleLongStringAddNTE(immunoComment, document);
                this.AddNextNteElement(string.Empty, document);
            }

            YellowstonePathology.Business.Test.Model.TestOrderCollection testOrders = panelSetOrderSurgical.GetTestOrders();
            if (this.ERPRExistsInCollection(testOrders) == true)
            {
                YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeResult result = new ErPrSemiQuantitative.ErPrSemiQuantitativeResult();
                this.AddNextNteElement("ER/PR References:", document);
                this.HandleLongStringAddNTE(result.ReportReferences, document);
                this.AddNextNteElement("", document);
            }

            string locationPerformed = panelSetOrderSurgical.GetLocationPerformedComment();
            this.HandleLongStringAddNTE(locationPerformed, document);
            this.AddNextNteElement(string.Empty, document);
        }

        public void AddAmendments(XElement document)
        {
            SurgicalTestOrder panelSetOrder = (SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(panelSetOrder.ReportNo);
            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
            {
                if (amendment.Final == true)
                {
                    this.AddNextNteElement(amendment.AmendmentType + ": " + amendment.AmendmentDate.Value.ToString("MM/dd/yyyy"), document);
                    this.HandleLongStringAddNTE(amendment.Text, document);
                    if (amendment.RequirePathologistSignature == true)
                    {
                        this.AddNextNteElement("Signature: " + amendment.PathologistSignature, document);
                        this.AddNextNteElement("E-signed " + amendment.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document);
                    }
                    this.AddNextNteElement("", document);

                    if (amendment.RevisedDiagnosis == true || amendment.ShowPreviousDiagnosis == true)
                    {
                        string amendmentId = amendment.AmendmentId;
                        foreach (YellowstonePathology.Business.Test.Surgical.SurgicalAudit surgicalAudit in panelSetOrder.SurgicalAuditCollection)
                        {
                            if (surgicalAudit.AmendmentId == amendmentId)
                            {
                                string finalDateP = Business.BaseData.GetShortDateString(panelSetOrder.FinalDate);
                                finalDateP += " " + YellowstonePathology.Business.BaseData.GetMillitaryTimeString(panelSetOrder.FinalTime);

                                string previousDiagnosisHeader = "Previous diagnosis on " + finalDateP;
                                this.AddNextNteElement(previousDiagnosisHeader, document);

                                foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenAudit specimenAudit in surgicalAudit.SurgicalSpecimenAuditCollection)
                                {
                                    if (specimenAudit.AmendmentId == amendmentId)
                                    {
                                        string diagnosisIDP = specimenAudit.DiagnosisId + ".";
                                        string specimenTypeP = specimenAudit.SpecimenOrder.Description + ":";
                                        this.AddNextNteElement(diagnosisIDP + specimenTypeP, document);

                                        this.HandleLongStringAddNTE(specimenAudit.Diagnosis, document);
                                    }
                                }

                                YellowstonePathology.Business.User.SystemUser pathologistUser = Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(surgicalAudit.PathologistId);
                                this.AddNextNteElement(pathologistUser.Signature, document);
                                this.AddNextNteElement("", document);
                            }
                        }
                    }
                }
            }
        }

        private void InformRevisedDiagnosis(XElement document, YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection)
        {
            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
            {
                if (amendment.Final == true && (amendment.RevisedDiagnosis == true || amendment.ShowPreviousDiagnosis == true))
                {
                    this.AddNextNteElement("Showing Revised Diagnosis", document);
                    this.AddNextNteElement("", document);
                    break;
                }
            }
        }

        private bool ERPRExistsInCollection(YellowstonePathology.Business.Test.Model.TestOrderCollection testOrders)
        {
            bool result = false;
            if (testOrders.ExistsByTestId("98") == true ||
                testOrders.ExistsByTestId("99") == true ||
                testOrders.ExistsByTestId("144") == true ||
                testOrders.ExistsByTestId("145") == true ||
                testOrders.ExistsByTestId("278") == true)
            {
                result = true;
            }
            return result;
        }
    }
}
