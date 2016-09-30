using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace YellowstonePathology.Business.Test.Surgical
{
	public class SurgicalWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public SurgicalWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{						
			SurgicalTestOrder panelSetOrderSurgical = (SurgicalTestOrder)this.m_PanelSetOrder;
			this.m_TemplateName = @"\\Cfileserver\Documents\ReportTemplates\XmlTemplates\Surgical.8.xml";

			base.OpenTemplate();

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = panelSetOrderSurgical.AmendmentCollection;

			panelSetOrderSurgical.GrossX = YellowstonePathology.Business.Common.SpellChecker.FixString(panelSetOrderSurgical.GrossX);
			panelSetOrderSurgical.MicroscopicX = YellowstonePathology.Business.Common.SpellChecker.FixString(panelSetOrderSurgical.MicroscopicX);
			panelSetOrderSurgical.CancerSummary = YellowstonePathology.Business.Common.SpellChecker.FixString(panelSetOrderSurgical.CancerSummary);
			this.m_AccessionOrder.ClinicalHistory = YellowstonePathology.Business.Common.SpellChecker.FixString(this.m_AccessionOrder.ClinicalHistory);
			panelSetOrderSurgical.Comment = YellowstonePathology.Business.Common.SpellChecker.FixString(panelSetOrderSurgical.Comment);

			foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
			{
				surgicalSpecimen.Diagnosis = YellowstonePathology.Business.Common.SpellChecker.FixString(surgicalSpecimen.Diagnosis);
			}

            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
			{
				amendment.Text = YellowstonePathology.Business.Common.SpellChecker.FixString(amendment.Text);
			}

			this.SetDemographicsV2();

			this.SetReportDistribution();
			this.SetCaseHistory();

			string amendmentTitle = string.Empty;
			if (amendmentCollection.Count > 0)
			{
				for (int i = amendmentCollection.Count - 1; i >= 0; i--)
				{
					if (amendmentCollection[i].Final == true)
					{
						amendmentTitle = amendmentCollection[i].AmendmentType;
						if (amendmentTitle == "Correction") amendmentTitle = "Corrected";
						if (amendmentCollection[i].RevisedDiagnosis == true)
						{
							this.SetXmlNodeData("Amendment", "Revised");
						}
						else
						{
							this.SetXmlNodeData("Amendment", amendmentTitle);
						}
						break;
					}
				}
			}
			this.SetXmlNodeData("Amendment", amendmentTitle);

			XmlNode mainTableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='microscopic_description']", this.m_NameSpaceManager);
			XmlNode diagnosisTableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='s_id']", this.m_NameSpaceManager);
			XmlNode rowSpecimenNode = diagnosisTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='s_id']", this.m_NameSpaceManager);
			XmlNode rowDiagnosisNode = diagnosisTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='diagnosis']", this.m_NameSpaceManager);
			XmlNode rowSpecimenBlankRow = diagnosisTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='specimen_blank_row']", this.m_NameSpaceManager);
			XmlNode insertAfterRow = rowSpecimenNode;

			foreach (SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
			{
				XmlNode rowSpecimenNodeClone = rowSpecimenNode.Clone();
				string diagnosisID = surgicalSpecimen.DiagnosisId + ".";
				string specimenType = surgicalSpecimen.SpecimenOrder.Description + ":";
				rowSpecimenNodeClone.SelectSingleNode("descendant::w:r[w:t='s_id']/w:t", this.m_NameSpaceManager).InnerText = diagnosisID;
				rowSpecimenNodeClone.SelectSingleNode("descendant::w:r[w:t='specimen_type']/w:t", this.m_NameSpaceManager).InnerText = specimenType;
				diagnosisTableNode.InsertAfter(rowSpecimenNodeClone, insertAfterRow);

				XmlNode rowDiagnosisNodeClone = rowDiagnosisNode.Clone();
				string diagnosis = surgicalSpecimen.Diagnosis;

				this.SetXMLNodeParagraphDataNode(rowDiagnosisNodeClone, "diagnosis", diagnosis);
				diagnosisTableNode.InsertAfter(rowDiagnosisNodeClone, rowSpecimenNodeClone);

				insertAfterRow = rowDiagnosisNodeClone;
			}

			if (string.IsNullOrEmpty(panelSetOrderSurgical.Comment) == true | panelSetOrderSurgical.Comment.ToUpper() == "NONE")
			{
				XmlNode rowComment = diagnosisTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='report_comment']", this.m_NameSpaceManager);
				diagnosisTableNode.RemoveChild(rowComment);
			}
			else
			{
				this.SetXMLNodeParagraphData("report_comment", panelSetOrderSurgical.Comment);
			}

			YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
			amendmentSection.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

			diagnosisTableNode.RemoveChild(rowSpecimenNode);
			diagnosisTableNode.RemoveChild(rowDiagnosisNode);
			diagnosisTableNode.RemoveChild(rowSpecimenBlankRow);

			XmlNode tableNodeP = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='sidp']", this.m_NameSpaceManager);
			XmlNode rowPreviousDiagnosis = mainTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='previous_diagnosis_header']", this.m_NameSpaceManager);
			XmlNode rowSpecimenNodeP = mainTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='sidp']", this.m_NameSpaceManager);
			XmlNode rowDiagnosisNodeP = mainTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='diagnosis_previous']", this.m_NameSpaceManager);
			XmlNode rowCommentP = mainTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='report_comment_previous']", this.m_NameSpaceManager);
			XmlNode rowPreviousSignatureP = mainTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='previous_signature']", this.m_NameSpaceManager);
			XmlNode rowBlankLine1P = mainTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='previous_diagnosis_blank_line1']", this.m_NameSpaceManager);
			XmlNode rowBlankLine2P = mainTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='previous_diagnosis_blank_line2']", this.m_NameSpaceManager);

			if (amendmentCollection.Count != 0)
			{
				if (amendmentCollection.HasFinalRevisedDiagnosis() == true)
				{
                    YellowstonePathology.Business.Amendment.Model.Amendment revisedDiagnosisAmendment = amendmentCollection.GetMostRecentFinalRevisedDiagnosis();
					XmlNode rowRevisedDiagnosisHeader = mainTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='Diagnosis:']", this.m_NameSpaceManager);
					string finalDateHead = YellowstonePathology.Business.BaseData.GetShortDateString(revisedDiagnosisAmendment.FinalDate);
					finalDateHead += " " + YellowstonePathology.Business.BaseData.GetMillitaryTimeString(revisedDiagnosisAmendment.FinalTime);
					rowRevisedDiagnosisHeader.SelectSingleNode("descendant::w:r[w:t='Diagnosis:']/w:t", this.m_NameSpaceManager).InnerText = "Revised Diagnosis: " + finalDateHead;
				}

				XmlNode insertAfterRowP = rowPreviousDiagnosis;

				for (int counter = 0; counter < amendmentCollection.Count; counter++)
				{
                    YellowstonePathology.Business.Amendment.Model.Amendment amendment = amendmentCollection[counter];
					if (amendment.RevisedDiagnosis == true || amendment.ShowPreviousDiagnosis == true)
					{
						string amendmentId = amendment.AmendmentId;
						foreach (YellowstonePathology.Business.Test.Surgical.SurgicalAudit surgicalAudit in panelSetOrderSurgical.SurgicalAuditCollection)
						{
							if (surgicalAudit.AmendmentId == amendmentId)
							{
								string finalDateP = string.Empty;

								finalDateP = YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate);
								finalDateP += " " + YellowstonePathology.Business.BaseData.GetMillitaryTimeString(panelSetOrderSurgical.FinalTime);

								XmlNode rowSpecimenBlankRowP = mainTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='specimen_blank_row_previous']", this.m_NameSpaceManager);
								XmlNode rowPreviousDiagnosisHeaderClone = rowPreviousDiagnosis.Clone();
								string previousDiagnosisHeader = "Previous diagnosis on " + finalDateP;
								rowPreviousDiagnosisHeaderClone.SelectSingleNode("descendant::w:r[w:t='previous_diagnosis_header']/w:t", this.m_NameSpaceManager).InnerText = previousDiagnosisHeader;
								tableNodeP.InsertAfter(rowPreviousDiagnosisHeaderClone, insertAfterRowP);
								insertAfterRowP = rowPreviousDiagnosisHeaderClone;

								foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenAudit specimenAudit in surgicalAudit.SurgicalSpecimenAuditCollection)
								{
									if (specimenAudit.AmendmentId == amendmentId)
									{
										XmlNode rowSpecimenNodeCloneP = rowSpecimenNodeP.Clone();
										string diagnosisIDP = specimenAudit.DiagnosisId + ".";
										string specimenTypeP = specimenAudit.SpecimenOrder.Description + ":";

										rowSpecimenNodeCloneP.SelectSingleNode("descendant::w:r[w:t='sidp']/w:t", this.m_NameSpaceManager).InnerText = diagnosisIDP;
										rowSpecimenNodeCloneP.SelectSingleNode("descendant::w:r[w:t='specimen_type_previous']/w:t", this.m_NameSpaceManager).InnerText = specimenTypeP;
										tableNodeP.InsertAfter(rowSpecimenNodeCloneP, insertAfterRowP);

										XmlNode rowDiagnosisNodeCloneP = rowDiagnosisNodeP.Clone();
										string diagnosisP = specimenAudit.Diagnosis;
										this.SetXMLNodeParagraphDataNode(rowDiagnosisNodeCloneP, "diagnosis_previous", diagnosisP);
										mainTableNode.InsertAfter(rowDiagnosisNodeCloneP, rowSpecimenNodeCloneP);
										insertAfterRowP = rowDiagnosisNodeCloneP;
									}
								}

								XmlNode rowCommentPClone = rowCommentP.Clone();
								string reportCommentP = surgicalAudit.Comment;
								rowCommentPClone.SelectSingleNode("descendant::w:r[w:t='report_comment_previous']/w:t", this.m_NameSpaceManager).InnerText = reportCommentP;
								tableNodeP.InsertAfter(rowCommentPClone, insertAfterRowP);

								insertAfterRowP = rowCommentPClone;
								XmlNode rowPreviousSignaturePClone = rowPreviousSignatureP.Clone();
								YellowstonePathology.Business.User.SystemUser pathologistUser = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(surgicalAudit.PathologistId);

								string previousSignautreP = pathologistUser.Signature;
								rowPreviousSignaturePClone.SelectSingleNode("descendant::w:r[w:t='previous_signature']/w:t", this.m_NameSpaceManager).InnerText = previousSignautreP;
								tableNodeP.InsertAfter(rowPreviousSignaturePClone, insertAfterRowP);
								insertAfterRowP = rowPreviousSignaturePClone;
							}
						}
					}
				}
			}

			tableNodeP.RemoveChild(rowSpecimenNodeP);
			tableNodeP.RemoveChild(rowDiagnosisNodeP);
			tableNodeP.RemoveChild(rowPreviousDiagnosis);
			tableNodeP.RemoveChild(rowCommentP);
			tableNodeP.RemoveChild(rowPreviousSignatureP);
			tableNodeP.RemoveChild(rowBlankLine1P);
			tableNodeP.RemoveChild(rowBlankLine2P);


			if (this.m_PanelSetOrder.Final == true)
			{
				this.SetXmlNodeData("pathologist_signature", this.m_PanelSetOrder.Signature);
			}
			else
			{
				this.SetXmlNodeData("pathologist_signature", "THIS CASE IS NOT FINAL");
			}

			XmlNode rowCancerSummaryHeader = mainTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='Cancer Case Summary']", this.m_NameSpaceManager);
			XmlNode rowCancerSummary = mainTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='cancer_summary']", this.m_NameSpaceManager);
			XmlNode rowAJCCStage = mainTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='ajcc_stage']", this.m_NameSpaceManager);

			if (panelSetOrderSurgical.CancerSummary == string.Empty)
			{
				mainTableNode.RemoveChild(rowCancerSummaryHeader);
				mainTableNode.RemoveChild(rowCancerSummary);
			}
			else
			{
				this.SetXMLNodeParagraphData("cancer_summary", panelSetOrderSurgical.CancerSummary);
				if (panelSetOrderSurgical.AJCCStage != string.Empty)
				{
					this.SetXmlNodeData("ajcc_stage", "Pathologic TNM Stage: " + panelSetOrderSurgical.AJCCStage);
				}
				else
				{
					this.SetXmlNodeData("ajcc_stage", string.Empty);
				}
			}

			this.SetXmlNodeData("client_case", this.m_AccessionOrder.PCAN);
			this.SetXMLNodeParagraphData("microscopic_description", panelSetOrderSurgical.MicroscopicX);

			//Stains 
			XmlNode rowAncillaryCommentNode = mainTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='ancillary_studies_comment']", this.m_NameSpaceManager);
			XmlNode tableNodeSS = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='specimen_type']", this.m_NameSpaceManager);
			XmlNode rowSpecimenTypeNode = tableNodeSS.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='specimen_type']", this.m_NameSpaceManager);
			XmlNode rowSinglePlexNode = tableNodeSS.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='Immunohistochemistry(single)']", this.m_NameSpaceManager);
			XmlNode rowMultiPlexNode = tableNodeSS.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='Immunohistochemistry(dual)']", this.m_NameSpaceManager);
			XmlNode rowCytochemicalNode = tableNodeSS.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='Cytochemical']", this.m_NameSpaceManager);
			XmlNode rowTestHeaderNode = tableNodeSS.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='Result']", this.m_NameSpaceManager);
			XmlNode rowSpecialStainNode = tableNodeSS.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='stain_description']", this.m_NameSpaceManager);

			bool commentSet = false;
			string ancillaryComment = panelSetOrderSurgical.GetAncillaryStudyComment();

			if (this.m_PanelSetOrder.FinalDate.HasValue == false || this.m_PanelSetOrder.FinalDate >= DateTime.Parse("11/7/07"))
			{
				XmlNode insertAfterRowSS = rowAncillaryCommentNode;
				YellowstonePathology.Business.Test.Model.TestOrderCollection allTests = this.m_PanelSetOrder.GetTestOrders();
				foreach (SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
				{
					YellowstonePathology.Business.Test.Model.TestOrderCollection specimenTestCollection = surgicalSpecimen.SpecimenOrder.GetTestOrders(allTests);
					YellowstonePathology.Business.SpecialStain.StainResultItemCollection singleplexStainResultCollection = surgicalSpecimen.StainResultItemCollection.GetSingleplexStains(specimenTestCollection);
					YellowstonePathology.Business.SpecialStain.StainResultItemCollection multiplexStainResultCollection = surgicalSpecimen.StainResultItemCollection.GetMultiplexStains(specimenTestCollection);
					YellowstonePathology.Business.SpecialStain.StainResultItemCollection cytochemicalStainResultCollection = surgicalSpecimen.StainResultItemCollection.GetCytochemicalStains(specimenTestCollection);

					if ((singleplexStainResultCollection.Count > 0 || multiplexStainResultCollection.Count > 0 || cytochemicalStainResultCollection.Count > 0))
					{
						if (commentSet == false)
						{
							this.SetXMLNodeParagraphData("ancillary_studies_comment", ancillaryComment);
							commentSet = true;
						}

						XmlNode rowSpecimenTypeClone = rowSpecimenTypeNode.Clone();
						string specimenType = surgicalSpecimen.DiagnosisId + ". " + surgicalSpecimen.SpecimenOrder.Description;
						rowSpecimenTypeClone.SelectSingleNode("descendant::w:r[w:t='specimen_type']/w:t", this.m_NameSpaceManager).InnerText = specimenType;
						tableNodeSS.InsertAfter(rowSpecimenTypeClone, insertAfterRowSS);
						insertAfterRowSS = rowSpecimenTypeClone;

						if (singleplexStainResultCollection.Count > 0)
						{
							insertAfterRowSS = this.SetStains(tableNodeSS, rowSinglePlexNode, rowTestHeaderNode, insertAfterRowSS, rowSpecialStainNode, surgicalSpecimen, singleplexStainResultCollection);
						}

						if (multiplexStainResultCollection.Count > 0)
						{
							insertAfterRowSS = this.SetStains(tableNodeSS, rowMultiPlexNode, rowTestHeaderNode, insertAfterRowSS, rowSpecialStainNode, surgicalSpecimen, multiplexStainResultCollection);
						}

						if (cytochemicalStainResultCollection.Count > 0)
						{
							insertAfterRowSS = this.SetStains(tableNodeSS, rowCytochemicalNode, rowTestHeaderNode, insertAfterRowSS, rowSpecialStainNode, surgicalSpecimen, cytochemicalStainResultCollection);
						}
					}
				}
			}

			tableNodeSS.RemoveChild(rowSpecimenTypeNode);
			tableNodeSS.RemoveChild(rowTestHeaderNode);
			tableNodeSS.RemoveChild(rowSpecialStainNode);
			tableNodeSS.RemoveChild(rowSinglePlexNode);
			tableNodeSS.RemoveChild(rowMultiPlexNode);
			tableNodeSS.RemoveChild(rowCytochemicalNode);

			if (commentSet == false)
			{
				XmlNode rowAncillaryStudiesNode = mainTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='Ancillary Studies']", this.m_NameSpaceManager);
				mainTableNode.RemoveChild(rowAncillaryStudiesNode);
				mainTableNode.RemoveChild(rowAncillaryCommentNode);
			}

			this.SetXMLNodeParagraphData("clinical_information", this.m_AccessionOrder.ClinicalHistory);

			XmlNode rowICHeaderNode = mainTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='intraoperative_consultation_header']", this.m_NameSpaceManager);
			XmlNode rowICSpecimenNode = mainTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='intraoperative_consultation_specimen']", this.m_NameSpaceManager);
			XmlNode rowICTextNode = mainTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='intraoperative_consultation_text']", this.m_NameSpaceManager);

			XmlNode insertAfterRowIC = rowICHeaderNode;
			int icCount = 0;
			this.SetXMLNodeParagraphDataNode(rowICHeaderNode, "intraoperative_consultation_header", "Intraoperative Consultation");
			foreach (SurgicalSpecimen surgicalSpecimen in panelSetOrderSurgical.SurgicalSpecimenCollection)
			{
				foreach (IntraoperativeConsultationResult icItem in surgicalSpecimen.IntraoperativeConsultationResultCollection)
				{

					XmlNode rowICSpecimenNodeClone = rowICSpecimenNode.Clone();
					string specimenString = surgicalSpecimen.DiagnosisId + ". " + surgicalSpecimen.SpecimenOrder.Description + ":";
					this.SetXMLNodeParagraphDataNode(rowICSpecimenNodeClone, "intraoperative_consultation_specimen", specimenString);
					mainTableNode.InsertAfter(rowICSpecimenNodeClone, insertAfterRowIC);
					insertAfterRowIC = rowICSpecimenNodeClone;

					XmlNode rowICTextNodeClone = rowICTextNode.Clone();
					this.SetXMLNodeParagraphDataNode(rowICTextNodeClone, "intraoperative_consultation_text", icItem.Result);
					mainTableNode.InsertAfter(rowICTextNodeClone, insertAfterRowIC);
					insertAfterRowIC = rowICTextNodeClone;
					icCount += 1;
				}
			}

			if (icCount == 0)
			{
				mainTableNode.RemoveChild(rowICHeaderNode);
			}
			mainTableNode.RemoveChild(rowICSpecimenNode);
			mainTableNode.RemoveChild(rowICTextNode);

			XmlNode rowSpecimenDescriptionNode = mainTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='specimen_description']", this.m_NameSpaceManager);
			XmlNode rowInsertSpecimenDescriptionAfterNode = rowSpecimenDescriptionNode;

			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
			{
				XmlNode rowSpecimenDescriptionClone = rowSpecimenDescriptionNode.Clone();
				rowSpecimenDescriptionClone.SelectSingleNode("descendant::w:r[w:t='specimen_description']/w:t", this.m_NameSpaceManager).InnerText = specimenOrder.GetSpecimenDescriptionString();

				YellowstonePathology.Business.Helper.DateTimeJoiner collectionDateTimeJoiner = new YellowstonePathology.Business.Helper.DateTimeJoiner(specimenOrder.CollectionDate.Value, specimenOrder.CollectionTime);
				rowSpecimenDescriptionClone.SelectSingleNode("descendant::w:r[w:t='date_time_collected']/w:t", this.m_NameSpaceManager).InnerText = collectionDateTimeJoiner.DisplayString;
				mainTableNode.InsertAfter(rowSpecimenDescriptionClone, rowInsertSpecimenDescriptionAfterNode);
				rowInsertSpecimenDescriptionAfterNode = rowSpecimenDescriptionClone;
			}
			mainTableNode.RemoveChild(rowSpecimenDescriptionNode);

			this.SetXMLNodeParagraphData("gross_description", panelSetOrderSurgical.GrossX);
			this.SetXMLNodeParagraphData("client_name", this.m_AccessionOrder.ClientName);

			string finalDate = YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate) + " - " + YellowstonePathology.Business.BaseData.GetMillitaryTimeString(this.m_PanelSetOrder.FinalTime);
			this.SetXmlNodeData("final_date", finalDate);

			string immunoComment = panelSetOrderSurgical.GetImmunoComment();

			if (immunoComment.Length > 0)
			{
				this.SetXmlNodeData("immuno_comment", immunoComment);
			}
			else
			{
				this.DeleteRow("immuno_comment");
			}

			this.SaveReport();
		}

		private XmlNode SetStains(XmlNode tableNodeSS,
			XmlNode rowStainHeaderNode,
			XmlNode rowTestHeaderNode,
			XmlNode insertAfterRowSS,
			XmlNode rowSpecialStainNode,
			SurgicalSpecimen surgicalSpecimen,
			YellowstonePathology.Business.SpecialStain.StainResultItemCollection stainResultCollection)
		{
			XmlNode rowStainHeaderNodeClone = rowStainHeaderNode.Clone();
			tableNodeSS.InsertAfter(rowStainHeaderNodeClone, insertAfterRowSS);
			insertAfterRowSS = rowStainHeaderNodeClone;

			XmlNode rowTestHeaderNodeClone = rowTestHeaderNode.Clone();
			tableNodeSS.InsertAfter(rowTestHeaderNodeClone, insertAfterRowSS);
			insertAfterRowSS = rowTestHeaderNodeClone;

			foreach (YellowstonePathology.Business.SpecialStain.StainResultItem stainResultItem in stainResultCollection)
			{
				XmlNode rowSpecialStainClone = rowSpecialStainNode.Clone();
				string stainDescription = stainResultItem.ProcedureName;
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

				rowSpecialStainClone.SelectSingleNode("descendant::w:r[w:t='stain_description']/w:t", this.m_NameSpaceManager).InnerText = stainDescription;
				rowSpecialStainClone.SelectSingleNode("descendant::w:r[w:t='stain_result']/w:t", this.m_NameSpaceManager).InnerText = stainResult;

				string block = surgicalSpecimen.GetBlockFromTestOrderId(stainResultItem.TestOrderId);
				rowSpecialStainClone.SelectSingleNode("descendant::w:r[w:t='block_description']/w:t", this.m_NameSpaceManager).InnerText = block;

				tableNodeSS.InsertAfter(rowSpecialStainClone, insertAfterRowSS);
				insertAfterRowSS = rowSpecialStainClone;
			}

			return insertAfterRowSS;
		}
	}
}
