using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.Surgical
{
    public class SurgicalEMAOBXView : YellowstonePathology.Business.HL7View.EMA.EMAOBXView
    {
        public SurgicalEMAOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
            : base(accessionOrder, reportNo, obxCount)
        {

        }

        public override void ToXml(XElement document)
        {
            

            /*
            this.AddHeader(document, panelSetOrderSurgical, "Surgical Pathology Report");
            this.AddNextObxElement("", document, "F");

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(panelSetOrderSurgical.ReportNo);
            this.InformRevisedDiagnosis(document, amendmentCollection);

            foreach (SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
            {
                this.HandleLongString(surgicalSpecimen.SpecimenOrder.SpecimenNumber.ToString() + ".) " + surgicalSpecimen.SpecimenOrder.Description, document, "F");
                YellowstonePathology.Business.Helper.DateTimeJoiner collectionDateTimeJoiner = new YellowstonePathology.Business.Helper.DateTimeJoiner(surgicalSpecimen.SpecimenOrder.CollectionDate.Value, surgicalSpecimen.SpecimenOrder.CollectionTime);
                this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeJoiner.DisplayString, document, "F");

                YellowstonePathology.Business.Test.Model.TestOrderCollection specimenTestOrders = surgicalSpecimen.SpecimenOrder.GetTestOrders(panelSetOrderSurgical.GetTestOrders());
                if (this.ERPRExistsInCollection(specimenTestOrders) == true)
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

            this.AddAmendments(document);

            this.AddNextObxElement("Microscopic Description: ", document, "F");
            this.HandleLongString(panelSetOrderSurgical.MicroscopicX, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            if (panelSetOrderSurgical.SurgicalSpecimenCollection.HasIC() == true)
            {
                foreach (SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
                {
                    if (surgicalSpecimen.IntraoperativeConsultationResultCollection.Count != 0)
                    {
                        foreach (IntraoperativeConsultationResult icItem in surgicalSpecimen.IntraoperativeConsultationResultCollection)
                        {
                            this.AddNextObxElement(surgicalSpecimen.DiagnosisId + ". " + surgicalSpecimen.SpecimenOrder.Description, document, "F");
                            this.AddNextObxElement(icItem.Result, document, "F");
                        }
                    }
                }
                this.AddNextObxElement(string.Empty, document, "F");
            }

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

            this.AddNextObxElement("Clinical Information: ", document, "F");
            if (string.IsNullOrEmpty(this.m_AccessionOrder.ClinicalHistory) == false)
            {
                this.HandleLongString(this.m_AccessionOrder.ClinicalHistory, document, "F");
            }
            else
            {
                this.AddNextObxElement("none", document, "F");

            }
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Gross Description: ", document, "F");
            this.HandleLongString(panelSetOrderSurgical.GrossX, document, "F");
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

            YellowstonePathology.Business.Test.Model.TestOrderCollection testOrders = panelSetOrderSurgical.GetTestOrders();
            if (this.ERPRExistsInCollection(testOrders) == true)
            {
                YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeResult result = new ErPrSemiQuantitative.ErPrSemiQuantitativeResult();
                this.AddNextObxElement("ER/PR References:", document, "F");
                this.HandleLongString(result.ReportReferences, document, "F");
                this.AddNextObxElement("", document, "F");
            }

            string locationPerformed = panelSetOrderSurgical.GetLocationPerformedComment();
            this.HandleLongString(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
            */
            }

        public void AddAmendments(XElement document)
        {
            /*
            SurgicalTestOrder panelSetOrder = (SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(panelSetOrder.ReportNo);
            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
            {
                if (amendment.Final == true)
                {
                    this.AddNextObxElement(amendment.AmendmentType + ": " + amendment.AmendmentDate.Value.ToString("MM/dd/yyyy"), document, "C");
                    this.HandleLongString(amendment.Text, document, "C");
                    if (amendment.RequirePathologistSignature == true)
                    {
                        this.AddNextObxElement("Signature: " + amendment.PathologistSignature, document, "C");
                        this.AddNextObxElement("E-signed " + amendment.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "C");
                    }
                    this.AddNextObxElement("", document, "C");

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

                                YellowstonePathology.Business.User.SystemUser pathologistUser = Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(surgicalAudit.PathologistId);
                                this.AddNextObxElement(pathologistUser.Signature, document, "C");
                                this.AddNextObxElement("", document, "F");
                            }
                        }
                    }
                }
            }
            */
        }

        private void InformRevisedDiagnosis(XElement document, YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection)
        {
            /*
            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
            {
                if (amendment.Final == true && (amendment.RevisedDiagnosis == true || amendment.ShowPreviousDiagnosis == true))
                {
                    this.AddNextObxElement("Showing Revised Diagnosis", document, "F");
                    this.AddNextObxElement("", document, "F");
                    break;
                }
            }
            */
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
