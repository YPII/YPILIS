﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.Surgical
{
	public class SurgicalEPICBeakerObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
	{
		public SurgicalEPICBeakerObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}		

		public override void ToXml(XElement document)
		{
			SurgicalTestOrder panelSetOrderSurgical = (SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddNextObxElement(string.Empty, document, "F");            
            this.AddNextObxElement("Report No: " + this.m_ReportNo, document, "F");
            this.AddNextObxElement("", document, "F");

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(panelSetOrderSurgical.ReportNo);
            this.InformRevisedDiagnosis(document, amendmentCollection);

			foreach (SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
			{
				//this.AddNextObxElement("Specimen: " + surgicalSpecimen.SpecimenOrder.SpecimenNumber.ToString(), document, "F");
				this.AddNextObxElementBeaker("SPECIMEN^" + surgicalSpecimen.DiagnosisId + "^DESCRIPTION", surgicalSpecimen.SpecimenOrder.Description, document, "F");
                YellowstonePathology.Business.Helper.DateTimeJoiner collectionDateTimeJoiner = new YellowstonePathology.Business.Helper.DateTimeJoiner(surgicalSpecimen.SpecimenOrder.CollectionDate.Value, surgicalSpecimen.SpecimenOrder.CollectionTime);
                //this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeJoiner.DisplayString, document, "F");
                this.AddNextObxElementBeaker("SPECIMEN^" + surgicalSpecimen.DiagnosisId + "^COLLECTIONDATE", collectionDateTimeJoiner.DisplayString, document, "F");
                                
                YellowstonePathology.Business.Test.Model.TestOrderCollection specimenTestOrders = surgicalSpecimen.SpecimenOrder.GetTestOrders(panelSetOrderSurgical.GetTestOrders());
                if (this.ERPRExistsInCollection(specimenTestOrders) == true)
                {
                    //this.AddNextObxElement("Fixation type: " + surgicalSpecimen.SpecimenOrder.LabFixation, document, "F");
                    this.AddNextObxElementBeaker("SPECIMEN^" + surgicalSpecimen.DiagnosisId + "^FIXATIONTYPE", surgicalSpecimen.SpecimenOrder.LabFixation, document, "F");

                    //this.AddNextObxElement("Time to fixation: " + surgicalSpecimen.SpecimenOrder.TimeToFixationHourString, document, "F");
                    this.AddNextObxElementBeaker("SPECIMEN^" + surgicalSpecimen.DiagnosisId + "^TIMETOFIXATION", surgicalSpecimen.SpecimenOrder.TimeToFixationHourString, document, "F");

                    //this.AddNextObxElement("Duration of Fixation: " + surgicalSpecimen.SpecimenOrder.FixationDurationString, document, "F");
                    this.AddNextObxElementBeaker("SPECIMEN^" + surgicalSpecimen.DiagnosisId + "^FIXATIONDURATION", surgicalSpecimen.SpecimenOrder.FixationDurationString, document, "F");
                }                
                
                //this.AddNextObxElement("Diagnosis: ", document, "F");
                this.AddNextObxElementBeaker("SPECIMEN^" + surgicalSpecimen.DiagnosisId + "^DIAGNOSIS", surgicalSpecimen.Diagnosis, document, "F");                
			}

			if (string.IsNullOrEmpty(panelSetOrderSurgical.Comment) == false)
			{
                //this.HandleLongString(panelSetOrderSurgical.Comment, document, "F");				
                this.AddNextObxElementBeaker("COMMENT", panelSetOrderSurgical.Comment, document, "F");
            }

			if (string.IsNullOrEmpty(panelSetOrderSurgical.CancerSummary) == false)
			{
				
				//this.HandleLongString(panelSetOrderSurgical.CancerSummary, document, "F");
                this.AddNextObxElementBeaker("CANCERSUMMARY", panelSetOrderSurgical.CancerSummary, document, "F");

                if (string.IsNullOrEmpty(panelSetOrderSurgical.AJCCStage) == false)
				{
                    //this.HandleLongString("Pathologic TNM Stage: " + panelSetOrderSurgical.AJCCStage, document, "F");
                    this.AddNextObxElementBeaker("TNMSTAGE", panelSetOrderSurgical.AJCCStage, document, "F");
                }
			}


            //this.AddNextObxElement("Pathologist: " + panelSetOrderSurgical.Signature, document, "F");
            this.AddNextObxElementBeaker("SIGNATURE", panelSetOrderSurgical.Signature, document, "F");

            if (panelSetOrderSurgical.FinalTime.HasValue == true)
			{
                //this.AddNextObxElement("E-signed " + panelSetOrderSurgical.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
                this.AddNextObxElementBeaker("FINALDATE", panelSetOrderSurgical.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }

            int amendmentCount = 1;
            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
            {
                if (amendment.Final == true)
                {                    
                    //this.AddNextObxElement(amendment.AmendmentType + ": " + amendment.AmendmentDate.Value.ToString("MM/dd/yyyy"), document, "C");
                    //this.HandleLongString(amendment.Text, document, "C");
                    this.AddNextObxElementBeaker("AMENDMENT^" + amendmentCount + "^TYPE", amendment.AmendmentType, document, "F");
                    this.AddNextObxElementBeaker("AMENDMENT^" + amendmentCount + "^TEXT", amendment.Text, document, "F");

                    if (amendment.RequirePathologistSignature == true)
                    {
                        //this.AddNextObxElement("Signature: " + amendment.PathologistSignature, document, "C");
                        this.AddNextObxElementBeaker("AMENDMENT^" + amendmentCount + "^SIGNATURE", amendment.PathologistSignature, document, "F");

                        //this.AddNextObxElement("E-signed " + amendment.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "C");
                        this.AddNextObxElementBeaker("AMENDMENT^" + amendmentCount + "^FINALDATE", amendment.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
                    }                    

                    if (amendment.RevisedDiagnosis == true || amendment.ShowPreviousDiagnosis == true)
                    {
                        string amendmentId = amendment.AmendmentId;
                        foreach (YellowstonePathology.Business.Test.Surgical.SurgicalAudit surgicalAudit in panelSetOrderSurgical.SurgicalAuditCollection)
                        {
                            if (surgicalAudit.AmendmentId == amendmentId)
                            {
                                string finalDateP = YellowstonePathology.Business.BaseData.GetShortDateString(panelSetOrderSurgical.FinalDate);
                                finalDateP += " " + YellowstonePathology.Business.BaseData.GetMillitaryTimeString(panelSetOrderSurgical.FinalTime);

                                //string previousDiagnosisHeader = "Previous diagnosis on " + finalDateP;
                                //this.AddNextObxElement(previousDiagnosisHeader, document, "C");                                

                                foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenAudit specimenAudit in surgicalAudit.SurgicalSpecimenAuditCollection)
                                {
                                    if (specimenAudit.AmendmentId == amendmentId)
                                    {
                                        string diagnosisIDP = specimenAudit.DiagnosisId + ".";
                                        string specimenTypeP = specimenAudit.SpecimenOrder.Description + ":";
                                        //this.AddNextObxElement(diagnosisIDP + specimenTypeP, document, "C");
                                        //this.HandleLongString(specimenAudit.Diagnosis, document, "C");
                                        this.AddNextObxElementBeaker("AMENDMENT^" + amendmentCount + "^PREVIOUSDIAGNOSIS^TEXT", diagnosisIDP + specimenTypeP + specimenAudit.Diagnosis, document, "C");
                                    }
                                }

                                YellowstonePathology.Business.User.SystemUser pathologistUser = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(surgicalAudit.PathologistId);
                                //this.AddNextObxElement(pathologistUser.Signature, document, "C");
                                this.AddNextObxElementBeaker("AMENDMENT^" + amendmentCount + "^PREVIOUSDIAGNOSIS^SIGNATURE", pathologistUser.Signature, document, "C");
                            }
                        }
                    }
                    amendmentCount += 1;
                }
            }

            //this.HandleLongString(panelSetOrderSurgical.MicroscopicX, document, "F");
            this.AddNextObxElementBeaker("MICROSCOPICX", panelSetOrderSurgical.MicroscopicX, document, "F");
            
            if(panelSetOrderSurgical.SurgicalSpecimenCollection.HasIC() == true)
            {
                foreach (SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
                {
                    if (surgicalSpecimen.IntraoperativeConsultationResultCollection.Count != 0)
                    {
                        int icCount = 1;
                        foreach (IntraoperativeConsultationResult icItem in surgicalSpecimen.IntraoperativeConsultationResultCollection)
                        {
                            //this.AddNextObxElement(surgicalSpecimen.DiagnosisId + ". " + surgicalSpecimen.SpecimenOrder.Description, document, "F");
                            //this.HandleLongString(icItem.Result, document, "F");                            
                            this.AddNextObxElementBeaker("INTRAOPERATIVECONSULTATION^" + icCount, surgicalSpecimen.SpecimenOrder.Description + ": " + icItem.Result, document, "F");
                            icCount += 1;
                        }
                    }
                }                
            }
            
            if (panelSetOrderSurgical.TypingStainCollection.Count > 0)
			{
				//this.AddNextObxElement("Ancillary Studies:", document, "F");
				//string ancillaryComment = panelSetOrderSurgical.GetAncillaryStudyComment();
                //this.HandleLongString(ancillaryComment, document, "F");
                this.AddNextObxElementBeaker("ANCILLARYSTUDIES^COMMENT", panelSetOrderSurgical.GetAncillaryStudyComment(), document, "F");

                foreach (SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
				{
					if (surgicalSpecimen.StainResultItemCollection.Count > 0)
					{
						//this.HandleLongString(surgicalSpecimen.SpecimenOrder.SpecimenNumber.ToString() + ". " + surgicalSpecimen.SpecimenOrder.Description, document, "F");
                        this.AddNextObxElementBeaker("ANCILLARYSTUDIES^SPECIMEN^" + surgicalSpecimen.SpecimenOrder.SpecimenNumber.ToString(), surgicalSpecimen.SpecimenOrder.SpecimenNumber.ToString() + ". " + surgicalSpecimen.SpecimenOrder.Description, document, "F");

                        int stainCount = 1;
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

                                //this.HandleLongString("Test: " + stainResultItem.ProcedureName + "  Result: " + stainResult, document, "F");
                                this.AddNextObxElementBeaker("ANCILLARYSTUDIES^SPECIMEN^" + surgicalSpecimen.DiagnosisId + "^TEST^" + stainCount + "^RESULT", "Test: " + stainResultItem.ProcedureName + "  Result: " + stainResult, document, "F");

                            }
                            stainCount += 1;
						}						
					}
				}
			}
            
            //this.AddNextObxElement("Clinical Information: ", document, "F");
            if (string.IsNullOrEmpty(this.m_AccessionOrder.ClinicalHistory) == false)
            {
                //this.HandleLongString(this.m_AccessionOrder.ClinicalHistory, document, "F");
                this.AddNextObxElementBeaker("CLINICALINFO", this.m_AccessionOrder.ClinicalHistory, document, "F");
            }
            //else
            //{
            //this.AddNextObxElement("none", document, "F");
            //}
            //this.AddNextObxElement("", document, "F");


            //this.AddNextObxElement("Gross Description: ", document, "F");
            //this.HandleLongString(panelSetOrderSurgical.GrossX, document, "F");
            this.AddNextObxElementBeaker("GROSSX", panelSetOrderSurgical.GrossX, document, "F");

            //this.AddNextObxElement("Additional Testing: ", document, "F");
            //this.HandleLongString(this.m_AccessionOrder.PanelSetOrderCollection.GetAdditionalTestingString(panelSetOrderSurgical.ReportNo), document, "F");
            this.AddNextObxElementBeaker("ADDITIONALTESTING", this.m_AccessionOrder.PanelSetOrderCollection.GetAdditionalTestingString(panelSetOrderSurgical.ReportNo), document, "F");
            
            string immunoComment = panelSetOrderSurgical.GetImmunoComment();
			if (immunoComment.Length > 0)
			{
				//this.HandleLongString(immunoComment, document, "F");				
                this.AddNextObxElementBeaker("IMMUNOCOMMENT", immunoComment, document, "F");
            }

            YellowstonePathology.Business.Test.Model.TestOrderCollection testOrders = panelSetOrderSurgical.GetTestOrders();
            if (this.ERPRExistsInCollection(testOrders) == true)
            {
                YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeResult result = new ErPrSemiQuantitative.ErPrSemiQuantitativeResult();
                //this.AddNextObxElement("ER/PR References:", document, "F");
                //this.HandleLongString(result.ReportReferences, document, "F");                
                this.AddNextObxElementBeaker("ERPRREFERENCES", result.ReportReferences, document, "F");
            }
            
			string locationPerformed = panelSetOrderSurgical.GetLocationPerformedComment();
			//this.AddNextObxElement(locationPerformed, document, "F");			
            this.AddNextObxElementBeaker("LOCATIONPERFORMED", locationPerformed, document, "F");
        }

        public override void AddAmendments(XElement document)
        {
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
                                string finalDateP = YellowstonePathology.Business.BaseData.GetShortDateString(panelSetOrder.FinalDate);
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

                                YellowstonePathology.Business.User.SystemUser pathologistUser = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(surgicalAudit.PathologistId);
                                this.AddNextObxElement(pathologistUser.Signature, document, "C");
                                this.AddNextObxElement("", document, "F");
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
                    this.AddNextObxElement("Showing Revised Diagnosis", document, "F");
                    this.AddNextObxElement("", document, "F");
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