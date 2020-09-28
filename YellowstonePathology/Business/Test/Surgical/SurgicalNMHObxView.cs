using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.Surgical
{
	public class SurgicalNMHOBXView : Business.HL7View.NMH.NMHOBXView
	{
		public SurgicalNMHOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        //OBX|1|TX|S&ADDTEST^Additional Testing^FREE|
        //OBX|2|TX|S&AMEND^Amendments^FREE|
        //OBX|3|TX|S&ANCILSTUD^Ancillary Studies^FREE|
        //OBX|4|TX|S&CANCSUM^Cancer Summary^FREE|
        //OBX|5|TX|S&CLINFO^Clinical Information^FREE|
        //OBX|6|TX|S&COM^Comment^FREE|
        //OBX|7|TX|S&FD^Final Diagnosis^FREE|
        //OBX|8|TX|S&FINDATE^Final Date^FREE|
        //OBX|9|TX|S&GD^Gross Description^FREE|
        //OBX|10|TX|S&IMMCOM^Immuno Comment^FREE|
        //OBX|11|TX|S&INTRAOP^Intraoperative Consultation^FREE|
        //OBX|12|TX|S&LOC^Location Performed^FREE|
        //OBX|13|TX|S&MD^Microscopic Description^FREE|
        //OBX|14|TX|S&PATHSIG^Pathologist Signature^FREE|
        //OBX|15|TX|S&REF^References^FREE|
        //OBX|16|TX|S&REPN^Report Number^FREE|
        //OBX|17|TX|S&TNM^TNM Stage^FREE|

		public override void ToXml(XElement document)
		{
			SurgicalTestOrder panelSetOrderSurgical = (SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(panelSetOrderSurgical.ReportNo);
            string observationResultStatus = "F";
            if (amendmentCollection.HasFinalAmendments() == true) observationResultStatus = "C";

            this.AddNextOBXElement("REPN", "Report Number", this.m_ReportNo, document, observationResultStatus);                        
            this.InformRevisedDiagnosis(document, amendmentCollection);

            StringBuilder finalDiagnosis = new StringBuilder();

			foreach (SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
			{
                Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(surgicalSpecimen.SpecimenOrderId);
                finalDiagnosis.AppendLine("Specimen: " + surgicalSpecimen.SpecimenOrder.SpecimenNumber.ToString() + ". " + specimenOrder.Description);                
                YellowstonePathology.Business.Helper.DateTimeJoiner collectionDateTimeJoiner = new YellowstonePathology.Business.Helper.DateTimeJoiner(surgicalSpecimen.SpecimenOrder.CollectionDate.Value, surgicalSpecimen.SpecimenOrder.CollectionTime);                
                finalDiagnosis.AppendLine("Collection Date: " + collectionDateTimeJoiner.DisplayString);                
                                
                YellowstonePathology.Business.Test.Model.TestOrderCollection specimenTestOrders = surgicalSpecimen.SpecimenOrder.GetTestOrders(panelSetOrderSurgical.GetTestOrders());
                if (this.ERPRExistsInCollection(specimenTestOrders) == true)
                {
                    finalDiagnosis.AppendLine("Fixation type: " + surgicalSpecimen.SpecimenOrder.LabFixation);                    
                    finalDiagnosis.AppendLine("Time to fixation: " + surgicalSpecimen.SpecimenOrder.TimeToFixationHourString);                    
                    finalDiagnosis.AppendLine("Duration of Fixation: " + surgicalSpecimen.SpecimenOrder.FixationDurationString);                    
                }

                finalDiagnosis.AppendLine("Diagnosis: " + surgicalSpecimen.Diagnosis);                
                finalDiagnosis.AppendLine();
			}
            this.AddNextOBXElement("FD", "Final Diagnosis", finalDiagnosis.ToString(), document, observationResultStatus);                

            if (string.IsNullOrEmpty(panelSetOrderSurgical.Comment) == false)
			{                
                this.AddNextOBXElement("COM", "Comment", panelSetOrderSurgical.Comment, document, observationResultStatus);
            }

			if (string.IsNullOrEmpty(panelSetOrderSurgical.CancerSummary) == false)
			{			
                this.AddNextOBXElement("CANCSUM", "Cancer Summary", panelSetOrderSurgical.CancerSummary, document, observationResultStatus);
                if (string.IsNullOrEmpty(panelSetOrderSurgical.AJCCStage) == false)
				{                
                    this.AddNextOBXElement("TNM", "TNM Stage", panelSetOrderSurgical.AJCCStage, document, observationResultStatus);
                }
			}
                        
            this.AddNextOBXElement("PATHSIG", "Pathologist Signature", panelSetOrderSurgical.Signature, document, observationResultStatus);

            if (panelSetOrderSurgical.FinalTime.HasValue == true)
			{                
                this.AddNextOBXElement("FINDATE", "Final Date", panelSetOrderSurgical.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, observationResultStatus);
            }

            if(amendmentCollection.Count != 0)
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

                        if (amendment.RevisedDiagnosis == true || amendment.ShowPreviousDiagnosis == true)
                        {
                            amendments.AppendLine();
                            string amendmentId = amendment.AmendmentId;
                            foreach (YellowstonePathology.Business.Test.Surgical.SurgicalAudit surgicalAudit in panelSetOrderSurgical.SurgicalAuditCollection)
                            {
                                if (surgicalAudit.AmendmentId == amendmentId)
                                {
                                    string finalDateP = YellowstonePathology.Business.BaseData.GetShortDateString(panelSetOrderSurgical.FinalDate);
                                    finalDateP += " " + YellowstonePathology.Business.BaseData.GetMillitaryTimeString(panelSetOrderSurgical.FinalTime);
                                    amendments.AppendLine("Previous diagnosis on " + finalDateP);                                    

                                    foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenAudit specimenAudit in surgicalAudit.SurgicalSpecimenAuditCollection)
                                    {
                                        if (specimenAudit.AmendmentId == amendmentId)
                                        {
                                            string diagnosisIDP = specimenAudit.DiagnosisId + ". ";
                                            string specimenTypeP = specimenAudit.SpecimenOrder.Description + ":";                                            
                                            amendments.AppendLine(diagnosisIDP + specimenTypeP);
                                            amendments.AppendLine(specimenAudit.Diagnosis);
                                            amendments.AppendLine();
                                        }
                                    }

                                    YellowstonePathology.Business.User.SystemUser pathologistUser = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(surgicalAudit.PathologistId);
                                    amendments.AppendLine("Signature: " + pathologistUser.Signature);
                                    amendments.AppendLine();                                 
                                }
                            }
                        }
                    }
                }

                amendments.AppendLine();
                this.AddNextOBXElement("AMEND", "Amendments", amendments.ToString(), document, observationResultStatus);                
            }   
            
            this.AddNextOBXElement("MD", "Microscopic Description", panelSetOrderSurgical.MicroscopicX, document, observationResultStatus);
            
            if(panelSetOrderSurgical.SurgicalSpecimenCollection.HasIC() == true)
            {
                StringBuilder intraoperativeConsultation = new StringBuilder();
                foreach (SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
                {
                    if (surgicalSpecimen.IntraoperativeConsultationResultCollection.Count != 0)
                    {                        
                        foreach (IntraoperativeConsultationResult icItem in surgicalSpecimen.IntraoperativeConsultationResultCollection)
                        {
                            intraoperativeConsultation.AppendLine(surgicalSpecimen.DiagnosisId + ". " + surgicalSpecimen.SpecimenOrder.Description);
                            intraoperativeConsultation.AppendLine(icItem.Result);                                                 
                        }
                    }
                }
                this.AddNextOBXElement("INTRAOP", "Intraoperative Consultation", intraoperativeConsultation.ToString(), document, observationResultStatus);
            }            
            
            if (panelSetOrderSurgical.TypingStainCollection.Count > 0)
			{
                StringBuilder ancillaryStudies = new StringBuilder();				                
                ancillaryStudies.AppendLine(panelSetOrderSurgical.GetAncillaryStudyComment());

                foreach (SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
				{
					if (surgicalSpecimen.StainResultItemCollection.Count > 0)
					{                        
                        ancillaryStudies.AppendLine(surgicalSpecimen.SpecimenOrder.SpecimenNumber.ToString() + ". " + surgicalSpecimen.SpecimenOrder.Description);
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
                        
                                ancillaryStudies.AppendLine("Test: " + stainResultItem.ProcedureName);
                                ancillaryStudies.AppendLine("Result: " + stainResultItem.Result);

                            }
                            ancillaryStudies.AppendLine();
						}						
					}
				}
                this.AddNextOBXElement("ANCILSTUD", "Ancillary Studies", ancillaryStudies.ToString(), document, observationResultStatus);                
            }
                        
            if (string.IsNullOrEmpty(this.m_AccessionOrder.ClinicalHistory) == false)
            {                
                this.AddNextOBXElement("CLINFO", "Clinical Information", this.m_AccessionOrder.ClinicalHistory, document, observationResultStatus);
            }            
            
            this.AddNextOBXElement("GD", "Gross Description", panelSetOrderSurgical.GrossX, document, observationResultStatus);            
            this.AddNextOBXElement("ADDTEST", "Additional Testing", this.m_AccessionOrder.PanelSetOrderCollection.GetAdditionalTestingString(panelSetOrderSurgical.ReportNo), document, observationResultStatus);
            
            string immunoComment = panelSetOrderSurgical.GetImmunoComment();
			if (immunoComment.Length > 0)
			{				
                this.AddNextOBXElement("IMMCOM", "Immuno Comment", immunoComment, document, observationResultStatus);
            }

            YellowstonePathology.Business.Test.Model.TestOrderCollection testOrders = panelSetOrderSurgical.GetTestOrders();
            if (this.ERPRExistsInCollection(testOrders) == true)
            {
                YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeResult result = new ErPrSemiQuantitative.ErPrSemiQuantitativeResult();                
                this.AddNextOBXElement("REF", "References", result.ReportReferences, document, observationResultStatus);
            }
            
			string locationPerformed = panelSetOrderSurgical.GetLocationPerformedComment();			
            this.AddNextOBXElement("LOC", "Location Performed", locationPerformed, document, observationResultStatus);
        }        

        private void InformRevisedDiagnosis(XElement document, YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection)
        {
            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
            {
                if (amendment.Final == true && (amendment.RevisedDiagnosis == true || amendment.ShowPreviousDiagnosis == true))
                {
                    throw new Exception("This is not implemented.");
                    //this.AddNextOBXElement("Revised Diagnosis", "Report reflects revised diagnosis.", document, "C");                    
                    //break;
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
