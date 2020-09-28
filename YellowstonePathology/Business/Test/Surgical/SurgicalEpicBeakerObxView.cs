using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.Surgical
{
	public class SurgicalEPICBeakerObxView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
	{
		public SurgicalEPICBeakerObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}		

		public override void ToXml(XElement document)
		{
			SurgicalTestOrder panelSetOrderSurgical = (SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(panelSetOrderSurgical.ReportNo);
            string observationResultStatus = "F";
            if (amendmentCollection.HasFinalAmendments() == true) observationResultStatus = "C";

            this.AddNextObxElementBeaker("Report No", this.m_ReportNo, document, observationResultStatus);                        
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
            this.AddNextObxElementBeaker("Final Diagnosis", finalDiagnosis.ToString(), document, observationResultStatus);                

            if (string.IsNullOrEmpty(panelSetOrderSurgical.Comment) == false)
			{                
                this.AddNextObxElementBeaker("Comment", panelSetOrderSurgical.Comment, document, observationResultStatus);
            }

			if (string.IsNullOrEmpty(panelSetOrderSurgical.CancerSummary) == false)
			{			
                this.AddNextObxElementBeaker("Cancer Summary", panelSetOrderSurgical.CancerSummary, document, observationResultStatus);
                if (string.IsNullOrEmpty(panelSetOrderSurgical.AJCCStage) == false)
				{                
                    this.AddNextObxElementBeaker("TNM Stage", panelSetOrderSurgical.AJCCStage, document, observationResultStatus);
                }
			}
                        
            this.AddNextObxElementBeaker("Pathologist Signature", panelSetOrderSurgical.Signature, document, observationResultStatus);

            if (panelSetOrderSurgical.FinalTime.HasValue == true)
			{                
                this.AddNextObxElementBeaker("Final Date", panelSetOrderSurgical.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, observationResultStatus);
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
                this.AddNextObxElementBeaker("Amendments", amendments.ToString(), document, observationResultStatus);                
            }   
            
            this.AddNextObxElementBeaker("Microscopic Description", panelSetOrderSurgical.MicroscopicX, document, observationResultStatus);
            
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
                this.AddNextObxElementBeaker("Intraoperative Consultation", intraoperativeConsultation.ToString(), document, observationResultStatus);
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
                this.AddNextObxElementBeaker("Ancillary Studies", ancillaryStudies.ToString(), document, observationResultStatus);                
            }
                        
            if (string.IsNullOrEmpty(this.m_AccessionOrder.ClinicalHistory) == false)
            {                
                this.AddNextObxElementBeaker("Clinical Information", this.m_AccessionOrder.ClinicalHistory, document, observationResultStatus);
            }            
            
            this.AddNextObxElementBeaker("Gross Description", panelSetOrderSurgical.GrossX, document, observationResultStatus);            
            this.AddNextObxElementBeaker("Additional Testing", this.m_AccessionOrder.PanelSetOrderCollection.GetAdditionalTestingString(panelSetOrderSurgical.ReportNo), document, observationResultStatus);
            
            string immunoComment = panelSetOrderSurgical.GetImmunoComment();
			if (immunoComment.Length > 0)
			{				
                this.AddNextObxElementBeaker("Immuno Comment", immunoComment, document, observationResultStatus);
            }

            YellowstonePathology.Business.Test.Model.TestOrderCollection testOrders = panelSetOrderSurgical.GetTestOrders();
            if (this.ERPRExistsInCollection(testOrders) == true)
            {
                YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeResult result = new ErPrSemiQuantitative.ErPrSemiQuantitativeResult();                
                this.AddNextObxElementBeaker("References", result.ReportReferences, document, observationResultStatus);
            }
            
			string locationPerformed = panelSetOrderSurgical.GetLocationPerformedComment();			
            this.AddNextObxElementBeaker("Location Performed", locationPerformed, document, observationResultStatus);
        }        

        private void InformRevisedDiagnosis(XElement document, YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection)
        {
            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
            {
                if (amendment.Final == true && (amendment.RevisedDiagnosis == true || amendment.ShowPreviousDiagnosis == true))
                {
                    this.AddNextObxElementBeaker("Revised Diagnosis", "Report reflects revised diagnosis.", document, "C");                    
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
