using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Windows.Networking.NetworkOperators;
using YellowstonePathology.Business.Test.Surgical;

namespace YellowstonePathology.Business.Test.LLP
{
	public class LLPNMHObxView : YellowstonePathology.Business.HL7View.NMH.NMHOBXView
	{
		public LLPNMHObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}		

		public override void ToXml(XElement document)
		{
            //RN - Report No
            //PATHSIG - Pathologist Signature
            //COM - Comment
            //AMEND - Amendments
            //CI - Clinical Information
            //REF - References
            //LOC - Location Performed
            //ADDTEST - Additional Testing
            //SPECDES - Specimen Description
            //METHOD - Method
            //COLLECT - Collection Date / Time

            //IMP - Impression
            //ICMT - Interpretive Comment
            //MRKS - Marker Detail
            //BLST - Blast Marker Percent
            //CLLDIST - Cell Distribution
            //SPECADQ - Specimen Adequacy
            //SPECVBLTY - Specimen Viablity Percent

            Business.Test.LLP.PanelSetOrderLeukemiaLymphoma panelSetOrder = (Business.Test.LLP.PanelSetOrderLeukemiaLymphoma)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.m_ReportNo);
            string observationResultStatus = "F";
            if (amendmentCollection.HasFinalAmendments() == true) observationResultStatus = "C";

            this.AddNextObxElementV2("RN", "Report No", this.m_ReportNo, document, observationResultStatus);
            this.AddNextObxElementV2("IMP", "Impression", panelSetOrder.Impression, document, observationResultStatus);
            this.AddNextObxElementV2("ICMT", "Interpretive Comment", panelSetOrder.InterpretiveComment, document, observationResultStatus);
            this.AddNextObxElementV2("CI", "Clinical Information", this.m_AccessionOrder.ClinicalHistory, document, observationResultStatus);
            this.AddNextObxElementV2("REF", "References", panelSetOrder.ReportReferences, document, observationResultStatus);
            this.AddNextObxElementV2("PATHSIG", "Pathologist Signature", panelSetOrder.Signature, document, observationResultStatus);
            this.AddNextObxElementV2("CLLPOP", "Cell Population Of Interest", panelSetOrder.FlowMarkerCollection.CellPopulationsOfInterest[0].Description, document, "F");

            StringBuilder markerDetail = new StringBuilder();
            foreach(Business.Flow.FlowMarkerItem marker in panelSetOrder.FlowMarkerCollection)
            {
                markerDetail.AppendLine($"Name: {marker.Name}, Interpretation: {marker.Interpretation}, Intensity: {marker.Intensity}");
            }
            this.AddNextObxElementV2("MRKS", "Marker Detail", markerDetail.ToString(), document, observationResultStatus);

            StringBuilder cellDistribution = new StringBuilder();
            cellDistribution.AppendLine($"Lymphocytes: {panelSetOrder.LymphocyteCountPercent}");
            cellDistribution.AppendLine($"Monocytes: {panelSetOrder.MonocyteCountPercent}");
            cellDistribution.AppendLine($"Myeloid: {panelSetOrder.MyeloidCellPercent}");
            cellDistribution.AppendLine($"Dim CD45/Mod SS: {panelSetOrder.DimCD45ModSSCountPercent}");
            this.AddNextObxElementV2("CLLDIST", "Cell Distribution", cellDistribution.ToString(), document, observationResultStatus);

            StringBuilder blastMarkers = new StringBuilder();
            blastMarkers.AppendLine($"CD34: {panelSetOrder.EGateCD34Percent}");
            blastMarkers.AppendLine($"CD117: {panelSetOrder.EGateCD117Percent}");
            this.AddNextObxElementV2("BLST", "Blast Marker Percent", blastMarkers.ToString(), document, observationResultStatus);

            Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOnId);
            this.AddNextObxElementV2("SPECDES", "Specimen Description", specimenOrder.Description, document, observationResultStatus);
            this.AddNextObxElementV2("SPECADQ", "Specimen Adequacy", specimenOrder.SpecimenAdequacy, document, observationResultStatus);
            this.AddNextObxElementV2("SPECVBLTY", "Specimen Viablity Percent", panelSetOrder.SpecimenViabilityPercent, document, observationResultStatus);

            string collectionDateTimeString = specimenOrder.GetCollectionDateTimeString();
            this.AddNextObxElementV2("COLLECT", "Collection Date/Time", collectionDateTimeString, document, observationResultStatus);

            this.AddNextObxElementV2("LOC", "Location Performed", panelSetOrder.GetLocationPerformedComment(), document, observationResultStatus);




            /*
            SurgicalTestOrder panelSetOrderSurgical = (SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(panelSetOrderSurgical.ReportNo);
            string observationResultStatus = "F";
            if (amendmentCollection.HasFinalAmendments() == true) observationResultStatus = "C";

            this.AddNextObxElementV2("RN", "Report No", this.m_ReportNo, document, observationResultStatus);                        
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
            this.AddNextObxElementV2("FD", "Final Diagnosis", finalDiagnosis.ToString(), document, observationResultStatus);                

            if (string.IsNullOrEmpty(panelSetOrderSurgical.Comment) == false)
			{                
                this.AddNextObxElementV2("COM", "Comment", panelSetOrderSurgical.Comment, document, observationResultStatus);
            }

			if (string.IsNullOrEmpty(panelSetOrderSurgical.CancerSummary) == false)
			{			
                this.AddNextObxElementV2("CANCSUM", "Cancer Summary", panelSetOrderSurgical.CancerSummary, document, observationResultStatus);
                if (string.IsNullOrEmpty(panelSetOrderSurgical.AJCCStage) == false)
				{                
                    this.AddNextObxElementV2("TNM", "TNM Stage", panelSetOrderSurgical.AJCCStage, document, observationResultStatus);
                }
			}
                        
            this.AddNextObxElementV2("PATHSIG", "Pathologist Signature", panelSetOrderSurgical.Signature, document, observationResultStatus);

            if (panelSetOrderSurgical.FinalTime.HasValue == true)
			{                
                this.AddNextObxElementV2("FINDATE", "Final Date", panelSetOrderSurgical.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, observationResultStatus);
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
                                    string finalDateP = Business.BaseData.GetShortDateString(panelSetOrderSurgical.FinalDate);
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

                                    YellowstonePathology.Business.User.SystemUser pathologistUser = Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(surgicalAudit.PathologistId);
                                    amendments.AppendLine("Signature: " + pathologistUser.Signature);
                                    amendments.AppendLine();                                 
                                }
                            }
                        }
                    }
                }

                amendments.AppendLine();
                this.AddNextObxElementV2("AMEND", "Amendments", amendments.ToString(), document, observationResultStatus);      
                              
            }   
            
            this.AddNextObxElementV2("MD", "Microscopic Description", panelSetOrderSurgical.MicroscopicX, document, observationResultStatus);
            
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
                this.AddNextObxElementV2("INTRAOP", "Intraoperative Consultation", intraoperativeConsultation.ToString(), document, observationResultStatus);
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
                                if(string.IsNullOrEmpty(stainResultItem.ReportComment) == false) ancillaryStudies.AppendLine("Comment: " + stainResultItem.ReportComment);
                            }
                            ancillaryStudies.AppendLine();
						}						
					}
				}
                this.AddNextObxElementV2("ANCILSTUD", "Ancillary Studies", ancillaryStudies.ToString(), document, observationResultStatus);                
            }
                        
            if (string.IsNullOrEmpty(this.m_AccessionOrder.ClinicalHistory) == false)
            {                
                this.AddNextObxElementV2("CI", "Clinical Information", this.m_AccessionOrder.ClinicalHistory, document, observationResultStatus);
            }            
            
            this.AddNextObxElementV2("GD", "Gross Description", panelSetOrderSurgical.GrossX, document, observationResultStatus);            
            this.AddNextObxElementV2("ADDTEST", "Additional Testing", this.m_AccessionOrder.PanelSetOrderCollection.GetAdditionalTestingString(panelSetOrderSurgical.ReportNo), document, observationResultStatus);
            
            string immunoComment = panelSetOrderSurgical.GetImmunoComment();
			if (immunoComment.Length > 0)
			{				
                this.AddNextObxElementV2("IMMCOM", "Immuno Comment", immunoComment, document, observationResultStatus);
            }

            YellowstonePathology.Business.Test.Model.TestOrderCollection testOrders = panelSetOrderSurgical.GetTestOrders();
            if (this.ERPRExistsInCollection(testOrders) == true)
            {
                YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeResult result = new ErPrSemiQuantitative.ErPrSemiQuantitativeResult();                
                this.AddNextObxElementV2("REF", "References", result.ReportReferences, document, observationResultStatus);
            }
            
			string locationPerformed = panelSetOrderSurgical.GetLocationPerformedComment();			
            this.AddNextObxElementV2("LOC", "Location Performed", locationPerformed, document, observationResultStatus);
             */
        }

        private void InformRevisedDiagnosis(XElement document, YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection)
        {
            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
            {
                if (amendment.Final == true && (amendment.RevisedDiagnosis == true || amendment.ShowPreviousDiagnosis == true))
                {
                    this.AddNextObxElementV2("REVD", "Revised Diagnosis", "Report reflects revised diagnosis.", document, "C");                    
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
