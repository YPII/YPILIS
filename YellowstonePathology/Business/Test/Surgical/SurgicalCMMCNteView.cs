using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.Surgical
{
	public class SurgicalCMMCNteView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
	{
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

		public SurgicalCMMCNteView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;            
		}		

		public override void ToXml(XElement document)
		{
			SurgicalTestOrder panelSetOrderSurgical = (SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

			this.AddCompanyHeader(document);
			this.AddBlankNteElement(document);
			this.AddNextNteElement("Surgical Pathology Report: ", document);
			this.AddNextNteElement("Master Accession #: " + panelSetOrderSurgical.MasterAccessionNo, document);
			this.AddNextNteElement("Report #: " + panelSetOrderSurgical.ReportNo, document);

			this.AddBlankNteElement(document);

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(panelSetOrderSurgical.ReportNo);
            this.InformRevisedDiagnosis(document, amendmentCollection);

            foreach (SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
			{
				this.AddNextNteElement("Specimen: " + surgicalSpecimen.SpecimenOrder.SpecimenNumber.ToString(), document);
				this.HandleLongString(surgicalSpecimen.SpecimenOrder.Description, document);
                YellowstonePathology.Business.Helper.DateTimeJoiner collectionDateTimeJoiner = new YellowstonePathology.Business.Helper.DateTimeJoiner(surgicalSpecimen.SpecimenOrder.CollectionDate.Value, surgicalSpecimen.SpecimenOrder.CollectionTime);
                this.AddNextNteElement("Collection Date/Time: " + collectionDateTimeJoiner.DisplayString, document);

                YellowstonePathology.Business.Test.Model.TestOrderCollection specimenTestOrders = surgicalSpecimen.SpecimenOrder.GetTestOrders(panelSetOrderSurgical.GetTestOrders());
                if (this.ERPRExistsInCollection(specimenTestOrders) == true)
                {
                    this.AddNextNteElement("Fixation type: " + surgicalSpecimen.SpecimenOrder.LabFixation, document);
                    this.AddNextNteElement("Time to fixation: " + surgicalSpecimen.SpecimenOrder.TimeToFixationHourString, document);
                    this.AddNextNteElement("Duration of Fixation: " + surgicalSpecimen.SpecimenOrder.FixationDurationString, document);
                }

                this.AddBlankNteElement(document);

				this.AddNextNteElement("Diagnosis: ", document);
				this.HandleLongString(surgicalSpecimen.Diagnosis, document);

				this.AddBlankNteElement(document);
			}

			if (string.IsNullOrEmpty(panelSetOrderSurgical.Comment) == false)
			{
				this.AddNextNteElement("Comment: ", document);
				this.HandleLongString(panelSetOrderSurgical.Comment, document);
				this.AddBlankNteElement(document);
			}

			if (string.IsNullOrEmpty(panelSetOrderSurgical.CancerSummary) == false)
			{
				this.AddNextNteElement("Cancer Summary: ", document);
				this.HandleLongString(panelSetOrderSurgical.CancerSummary, document);
				this.AddBlankNteElement(document);

				if (string.IsNullOrEmpty(panelSetOrderSurgical.AJCCStage) == false)
				{
					this.HandleLongString("Pathologic TNM Stage: " + panelSetOrderSurgical.AJCCStage, document);
					this.AddBlankNteElement(document);
				}
			}

			this.AddNextNteElement("Pathologist: " + panelSetOrderSurgical.Signature, document);
            if (panelSetOrderSurgical.FinalTime.HasValue == true)
            {
                this.AddNextNteElement("*** E-signed: " + panelSetOrderSurgical.FinalTime.Value.ToString("MM/dd/yyyy HH:mm") + " ***", document);
            }

            this.AddBlankNteElement(document);

            this.AddAmendments(document);

            this.AddNextNteElement("Microscopic Description: ", document);
			this.HandleLongString(panelSetOrderSurgical.MicroscopicX, document);

			this.AddBlankNteElement(document);

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
                this.AddBlankNteElement(document);
            }

            if (panelSetOrderSurgical.TypingStainCollection.Count > 0)
			{
				this.AddNextNteElement("Ancillary Studies:", document);
				string ancillaryComment = panelSetOrderSurgical.GetAncillaryStudyComment();
				this.HandleLongString(ancillaryComment, document);

				foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
				{
					if (surgicalSpecimen.StainResultItemCollection.Count > 0)
					{
						this.HandleLongString(surgicalSpecimen.SpecimenOrder.SpecimenNumber.ToString() + ". " + surgicalSpecimen.SpecimenOrder.Description, document);

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

								this.HandleLongString("Test: " + stainResultItem.ProcedureName + "  Result: " + stainResult, document);
							}
						}
					}
				}
				this.AddBlankNteElement(document);
			}

            this.AddNextNteElement("Clinical Information: ", document);
            if (string.IsNullOrEmpty(this.m_AccessionOrder.ClinicalHistory) == false)
            {
                this.HandleLongString(this.m_AccessionOrder.ClinicalHistory, document);
            }
            else
            {
                this.AddNextNteElement("none", document);

            }
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Gross Description: ", document);
			this.HandleLongString(panelSetOrderSurgical.GrossX, document);
			this.AddBlankNteElement(document);

            this.AddNextNteElement("Additional Testing: ", document);
            this.HandleLongString(this.m_AccessionOrder.PanelSetOrderCollection.GetAdditionalTestingString(panelSetOrderSurgical.ReportNo), document);
            this.AddBlankNteElement(document);

            string immunoComment = panelSetOrderSurgical.GetImmunoComment();
            if (immunoComment.Length > 0)
            {
                this.HandleLongString(immunoComment, document);
                this.AddBlankNteElement(document);
            }

            YellowstonePathology.Business.Test.Model.TestOrderCollection testOrders = panelSetOrderSurgical.GetTestOrders();
            if (this.ERPRExistsInCollection(testOrders) == true)
            {
                YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeResult result = new ErPrSemiQuantitative.ErPrSemiQuantitativeResult();
                this.AddNextNteElement("ER/PR References:", document);
                this.HandleLongString(result.ReportReferences, document);
                this.AddBlankNteElement(document);
            }

            string locationPerformed = panelSetOrderSurgical.GetLocationPerformedComment();
            this.AddNextNteElement(locationPerformed, document);
            this.AddBlankNteElement(document);
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
                    this.HandleLongString(amendment.Text, document);
                    if (amendment.RequirePathologistSignature == true)
                    {
                        this.AddNextNteElement("Signature: " + amendment.PathologistSignature, document);
                        this.AddNextNteElement("E-signed " + amendment.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document);
                    }
                    this.AddBlankNteElement(document);

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

                                        this.HandleLongString(specimenAudit.Diagnosis, document);
                                    }
                                }

                                YellowstonePathology.Business.User.SystemUser pathologistUser = Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(surgicalAudit.PathologistId);
                                this.AddNextNteElement(pathologistUser.Signature, document);
                                this.AddBlankNteElement(document);
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
                    this.AddBlankNteElement(document);
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
