using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.WomensHealthProfile
{
	public class WomensHealthProfileWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public WomensHealthProfileWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{			
			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\WomensHealthProfile.2.xml";
			this.OpenTemplate();

			this.SetCurrentPapResults();
			this.SetCurrentMolecularResults();
			this.SetPriorResults();            

            WomensHealthProfileTestOrder womensHealthProfileTestOrder = (WomensHealthProfileTestOrder)this.m_PanelSetOrder;
			WomensHealthProfileResult womensHealthProfileResult = new WomensHealthProfileResult(this.m_AccessionOrder);

            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(womensHealthProfileTestOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(womensHealthProfileTestOrder.OrderedOnId);
			this.SetXmlNodeData("specimen_source", specimenOrder.SpecimenSource);
			string collectionDateTimeString = specimenOrder.GetCollectionDateTimeString();
			this.SetXmlNodeData("collection_date_time", collectionDateTimeString);
			this.SetXmlNodeData("specimen_description", specimenOrder.Description);

            if (this.m_PanelSetOrder.FinalTime.HasValue == true)
            {
				string finalDateTime = YellowstonePathology.Business.Document.CaseReportV2.ReportDateTimeFormat(this.m_PanelSetOrder.FinalTime.Value);
                this.SetXmlNodeData("final_date", finalDateTime);
            }
            else
            {
                this.SetXmlNodeData("final_date", string.Empty);
            }

			string clinicalHistory = this.m_AccessionOrder.ClinicalHistory;
			this.SetXMLNodeParagraphData("clinical_history", clinicalHistory);
			this.SetXMLNodeParagraphData("report_method", womensHealthProfileResult.Method);
			this.SetXMLNodeParagraphData("report_references", womensHealthProfileResult.References);            

			this.SetDemographicsV2();
			this.SetReportDistribution();
			this.SetCaseHistory();

			this.SaveReport();
		}

		private void SetCurrentPapResults()
		{
			YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(15);
			string sceeningImpression = panelSetOrderCytology.ScreeningImpression;
			if (string.IsNullOrEmpty(sceeningImpression) == false)
			{
				this.SetXmlNodeData("screening_impression", sceeningImpression);
			}
			
			string specimenAdequacy = panelSetOrderCytology.SpecimenAdequacy;
			this.SetXmlNodeData("specimen_adequacy", specimenAdequacy);

			string otherConditions = panelSetOrderCytology.OtherConditions;
			if (string.IsNullOrEmpty(otherConditions) == false)
			{
				this.SetXmlNodeData("other_conditions", otherConditions);
			}
			else
			{
				this.DeleteRow("Other Conditions");
				this.DeleteRow("other_conditions");
			}

			string resultComment = panelSetOrderCytology.ReportComment;
			if (string.IsNullOrEmpty(resultComment) == false)
			{
				this.SetXMLNodeParagraphData("report_comment", resultComment);
			}
			else
			{
				this.DeleteRow("Comment");
				this.DeleteRow("report_comment");
			}

			YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology screeningPanelOrder = null;
			YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology reviewPanelOrder = null;

			foreach (YellowstonePathology.Business.Interface.IPanelOrder panelOrder in panelSetOrderCytology.PanelOrders)
			{
				Type objectType = panelOrder.GetType();
				if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
				{
					YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology cytologyPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)panelOrder;
					if (cytologyPanelOrder.PanelId == 38)
					{
						if (cytologyPanelOrder.ScreeningType == "Primary Screening")
						{
							screeningPanelOrder = cytologyPanelOrder;
						}
						else if (cytologyPanelOrder.ScreeningType == "Pathologist Review")
						{
							reviewPanelOrder = cytologyPanelOrder;
						}
						else if (cytologyPanelOrder.ScreeningType == "Cytotech Review")
						{
							if (reviewPanelOrder == null || reviewPanelOrder.ScreeningType != "Pathologist Review")
							{
								reviewPanelOrder = cytologyPanelOrder;
							}
						}
					}
				}
			}

			YellowstonePathology.Business.User.SystemUser systemUser = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(screeningPanelOrder.ScreenedById);
			string screenedBy = string.Empty;
			if (string.IsNullOrEmpty(systemUser.Signature) == false)
			{
				screenedBy = systemUser.Signature;
			}
			this.SetXmlNodeData("screened_by", screenedBy);

			string cytoTechFinal = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(screeningPanelOrder.AcceptedDate);
			this.SetXmlNodeData("cytotech_final", cytoTechFinal);

			if (reviewPanelOrder != null)
			{
				string reviewedBy = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(reviewPanelOrder.ScreenedById).Signature;
				string reviewedByFinal = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(reviewPanelOrder.AcceptedDate);

				if (reviewedBy.IndexOf("MD") >= 0)
				{
					this.SetXmlNodeData("Reviewed By:", "Interpreted By:");
				}
				this.SetXmlNodeData("reviewed_by", reviewedBy);
				this.SetXmlNodeData("case_final", reviewedByFinal);
			}
			else
			{
				this.SetXmlNodeData("Reviewed By:", "");
				this.SetXmlNodeData("reviewed_by", "");
				this.SetXmlNodeData("case_final", "");
			}
		}

		private void SetCurrentMolecularResults()
		{
			XmlNode mainTableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='molecular_test']", this.m_NameSpaceManager);
			XmlNode rowTestNode = mainTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='molecular_test']", this.m_NameSpaceManager);
			XmlNode rowResultNode = mainTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='test_result']", this.m_NameSpaceManager);
			XmlNode rowBlankRow = mainTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='blank_row']", this.m_NameSpaceManager);
			XmlNode insertAfterRow = rowBlankRow;

			string headerText = "High Risk HPV Testing";
			YellowstonePathology.Business.Test.HPV.HPVTest panelSetHPV = new YellowstonePathology.Business.Test.HPV.HPVTest();
			if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHPV.PanelSetId) == true)
			{
				YellowstonePathology.Business.Test.HPV.HPVTestOrder hpvTestOrder = (YellowstonePathology.Business.Test.HPV.HPVTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetHPV.PanelSetId);
				XmlNode rowTestNodeClone = rowTestNode.Clone();
				rowTestNodeClone.SelectSingleNode("descendant::w:r[w:t='molecular_test']/w:t", this.m_NameSpaceManager).InnerText = headerText;
				mainTableNode.InsertAfter(rowTestNodeClone, insertAfterRow);
				insertAfterRow = rowTestNodeClone;

				XmlNode rowResultNodeClone = rowResultNode.Clone();
				rowResultNodeClone.SelectSingleNode("descendant::w:r[w:t='test_name']/w:t", this.m_NameSpaceManager).InnerText = "High Risk HPV";
				rowResultNodeClone.SelectSingleNode("descendant::w:r[w:t='test_result']/w:t", this.m_NameSpaceManager).InnerText = hpvTestOrder.Result;
                rowResultNodeClone.SelectSingleNode("descendant::w:r[w:t='test_reference']/w:t", this.m_NameSpaceManager).InnerText = "Negative";
				string testFinaldate = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(hpvTestOrder.FinalDate);
				rowResultNodeClone.SelectSingleNode("descendant::w:r[w:t='test_final_date']/w:t", this.m_NameSpaceManager).InnerText = testFinaldate;
				mainTableNode.InsertAfter(rowResultNodeClone, insertAfterRow);
				insertAfterRow = rowResultNodeClone;

				XmlNode rowBlankRowClone = rowBlankRow.Clone();
				rowBlankRowClone.SelectSingleNode("descendant::w:r[w:t='blank_row']/w:t", this.m_NameSpaceManager).InnerText = string.Empty;
				mainTableNode.InsertAfter(rowBlankRowClone, insertAfterRow);
				insertAfterRow = rowBlankRowClone;
			}

			headerText = "HPV Genotyping";
			YellowstonePathology.Business.Test.HPV1618.HPV1618Test panelSetHPV1618 = new YellowstonePathology.Business.Test.HPV1618.HPV1618Test();
			if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHPV1618.PanelSetId) == true)
			{
				YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618 panelSetOrderHPV1618 = (YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetHPV1618.PanelSetId);
				XmlNode rowTestNodeClone = rowTestNode.Clone();
				rowTestNodeClone.SelectSingleNode("descendant::w:r[w:t='molecular_test']/w:t", this.m_NameSpaceManager).InnerText = headerText;
				mainTableNode.InsertAfter(rowTestNodeClone, insertAfterRow);
				insertAfterRow = rowTestNodeClone;

				XmlNode rowResultNode16Clone = rowResultNode.Clone();
				rowResultNode16Clone.SelectSingleNode("descendant::w:r[w:t='test_name']/w:t", this.m_NameSpaceManager).InnerText = "HPV type 16";
				rowResultNode16Clone.SelectSingleNode("descendant::w:r[w:t='test_result']/w:t", this.m_NameSpaceManager).InnerText = panelSetOrderHPV1618.HPV16Result;
                rowResultNode16Clone.SelectSingleNode("descendant::w:r[w:t='test_reference']/w:t", this.m_NameSpaceManager).InnerText = "Negative";
				string testFinaldate = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(panelSetOrderHPV1618.FinalDate);
				rowResultNode16Clone.SelectSingleNode("descendant::w:r[w:t='test_final_date']/w:t", this.m_NameSpaceManager).InnerText = testFinaldate;
				mainTableNode.InsertAfter(rowResultNode16Clone, insertAfterRow);
				insertAfterRow = rowResultNode16Clone;

				XmlNode rowResultNode18Clone = rowResultNode.Clone();
				rowResultNode18Clone.SelectSingleNode("descendant::w:r[w:t='test_name']/w:t", this.m_NameSpaceManager).InnerText = "HPV type 18";
				rowResultNode18Clone.SelectSingleNode("descendant::w:r[w:t='test_result']/w:t", this.m_NameSpaceManager).InnerText = panelSetOrderHPV1618.HPV18Result;
                rowResultNode18Clone.SelectSingleNode("descendant::w:r[w:t='test_reference']/w:t", this.m_NameSpaceManager).InnerText = "Negative";
				rowResultNode18Clone.SelectSingleNode("descendant::w:r[w:t='test_final_date']/w:t", this.m_NameSpaceManager).InnerText = string.Empty;
				mainTableNode.InsertAfter(rowResultNode18Clone, insertAfterRow);
				insertAfterRow = rowResultNode18Clone;

				XmlNode rowBlankRowClone = rowBlankRow.Clone();
				rowBlankRowClone.SelectSingleNode("descendant::w:r[w:t='blank_row']/w:t", this.m_NameSpaceManager).InnerText = string.Empty;
				mainTableNode.InsertAfter(rowBlankRowClone, insertAfterRow);
				insertAfterRow = rowBlankRowClone;
			}

			headerText = "Chlamydia Gonorrhea Screening";
            YellowstonePathology.Business.Test.NGCT.NGCTTest panelSetNGCT = new YellowstonePathology.Business.Test.NGCT.NGCTTest();
			if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetNGCT.PanelSetId) == true)
			{
                YellowstonePathology.Business.Test.NGCT.NGCTTestOrder panelSetOrderNGCT = (YellowstonePathology.Business.Test.NGCT.NGCTTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetNGCT.PanelSetId);
				this.SetXmlNodeData("chlamydia_gonorrhea_screening", string.Empty);
				this.SetXmlNodeData("ct_result", panelSetOrderNGCT.ChlamydiaTrachomatisResult);
				this.SetXmlNodeData("ng_result", panelSetOrderNGCT.NeisseriaGonorrhoeaeResult);
				string hpvFinaldate = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(panelSetOrderNGCT.FinalDate);
				this.SetXmlNodeData("ngct_final_date", hpvFinaldate);


				XmlNode rowTestNodeClone = rowTestNode.Clone();
				rowTestNodeClone.SelectSingleNode("descendant::w:r[w:t='molecular_test']/w:t", this.m_NameSpaceManager).InnerText = headerText;
				mainTableNode.InsertAfter(rowTestNodeClone, insertAfterRow);
				insertAfterRow = rowTestNodeClone;

				XmlNode rowResultNodeCtClone = rowResultNode.Clone();
				rowResultNodeCtClone.SelectSingleNode("descendant::w:r[w:t='test_name']/w:t", this.m_NameSpaceManager).InnerText = "Chlamydia trachomatis";
				rowResultNodeCtClone.SelectSingleNode("descendant::w:r[w:t='test_result']/w:t", this.m_NameSpaceManager).InnerText = panelSetOrderNGCT.ChlamydiaTrachomatisResult;
                rowResultNodeCtClone.SelectSingleNode("descendant::w:r[w:t='test_reference']/w:t", this.m_NameSpaceManager).InnerText = "Negative";
				string testFinaldate = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(panelSetOrderNGCT.FinalDate);
				rowResultNodeCtClone.SelectSingleNode("descendant::w:r[w:t='test_final_date']/w:t", this.m_NameSpaceManager).InnerText = testFinaldate;
				mainTableNode.InsertAfter(rowResultNodeCtClone, insertAfterRow);
				insertAfterRow = rowResultNodeCtClone;

				XmlNode rowResultNodeNgClone = rowResultNode.Clone();
				rowResultNodeNgClone.SelectSingleNode("descendant::w:r[w:t='test_name']/w:t", this.m_NameSpaceManager).InnerText = "Neisseria gonorrhoeae";
				rowResultNodeNgClone.SelectSingleNode("descendant::w:r[w:t='test_result']/w:t", this.m_NameSpaceManager).InnerText = panelSetOrderNGCT.NeisseriaGonorrhoeaeResult;
                rowResultNodeNgClone.SelectSingleNode("descendant::w:r[w:t='test_reference']/w:t", this.m_NameSpaceManager).InnerText = "Negative";
				rowResultNodeNgClone.SelectSingleNode("descendant::w:r[w:t='test_final_date']/w:t", this.m_NameSpaceManager).InnerText = string.Empty;
				mainTableNode.InsertAfter(rowResultNodeNgClone, insertAfterRow);
				insertAfterRow = rowResultNodeNgClone;

				XmlNode rowBlankRowClone = rowBlankRow.Clone();
				rowBlankRowClone.SelectSingleNode("descendant::w:r[w:t='blank_row']/w:t", this.m_NameSpaceManager).InnerText = string.Empty;
				mainTableNode.InsertAfter(rowBlankRowClone, insertAfterRow);
				insertAfterRow = rowBlankRowClone;
			}

			headerText = "Trichomonas Screening";
			YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest panelSetTrichomonas = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest();
			if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetTrichomonas.PanelSetId) == true)
			{
				YellowstonePathology.Business.Test.Trichomonas.TrichomonasTestOrder reportOrderTrichomonas = (YellowstonePathology.Business.Test.Trichomonas.TrichomonasTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetTrichomonas.PanelSetId);
				XmlNode rowTestNodeClone = rowTestNode.Clone();
				rowTestNodeClone.SelectSingleNode("descendant::w:r[w:t='molecular_test']/w:t", this.m_NameSpaceManager).InnerText = headerText;
				mainTableNode.InsertAfter(rowTestNodeClone, insertAfterRow);
				insertAfterRow = rowTestNodeClone;

				XmlNode rowResultNodeClone = rowResultNode.Clone();
				rowResultNodeClone.SelectSingleNode("descendant::w:r[w:t='test_name']/w:t", this.m_NameSpaceManager).InnerText = "Trichomonas vaginalis";
				rowResultNodeClone.SelectSingleNode("descendant::w:r[w:t='test_result']/w:t", this.m_NameSpaceManager).InnerText = reportOrderTrichomonas.Result;
                rowResultNodeClone.SelectSingleNode("descendant::w:r[w:t='test_reference']/w:t", this.m_NameSpaceManager).InnerText = "Negative";
				string testFinaldate = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(reportOrderTrichomonas.FinalDate);
				rowResultNodeClone.SelectSingleNode("descendant::w:r[w:t='test_final_date']/w:t", this.m_NameSpaceManager).InnerText = testFinaldate;
				mainTableNode.InsertAfter(rowResultNodeClone, insertAfterRow);
				insertAfterRow = rowResultNodeClone;
			}

			if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHPV.PanelSetId) == false &&
				this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetHPV1618.PanelSetId) == false &&
				this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetNGCT.PanelSetId) == false &&
				this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetTrichomonas.PanelSetId) == false)
			{
				rowResultNode.SelectSingleNode("descendant::w:r[w:t='test_name']/w:t", this.m_NameSpaceManager).InnerText = "No tests performed";
                rowResultNode.SelectSingleNode("descendant::w:r[w:t='test_reference']/w:t", this.m_NameSpaceManager).InnerText = string.Empty;
				rowResultNode.SelectSingleNode("descendant::w:r[w:t='test_result']/w:t", this.m_NameSpaceManager).InnerText = string.Empty;
				rowResultNode.SelectSingleNode("descendant::w:r[w:t='test_final_date']/w:t", this.m_NameSpaceManager).InnerText = string.Empty;
			}
			else
			{
				this.DeleteRow("test_result");
			}

			this.DeleteRow("molecular_test");
			this.DeleteRow("blank_row");
		}

		private void SetPriorResults()
		{
			YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest panelSetThinPrepPap = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest();
			YellowstonePathology.Business.Test.HPV.HPVTest panelSetHPV = new YellowstonePathology.Business.Test.HPV.HPVTest();
			YellowstonePathology.Business.Test.HPV1618.HPV1618Test panelSetHPV1618 = new YellowstonePathology.Business.Test.HPV1618.HPV1618Test();
            YellowstonePathology.Business.Test.NGCT.NGCTTest panelSetNGCT = new YellowstonePathology.Business.Test.NGCT.NGCTTest();
			YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest panelSetTrichomonas = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest();

			DateTime cutoffDate = this.m_AccessionOrder.AccessionDate.Value.AddYears(-5);

			XmlNode mainTableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='prior_report_no']", this.m_NameSpaceManager);
			XmlNode rowHistoryNode = mainTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='prior_report_no']", this.m_NameSpaceManager);
			XmlNode insertAfterRow = rowHistoryNode;

			YellowstonePathology.Business.Domain.PatientHistory patientHistory = new YellowstonePathology.Business.Domain.PatientHistory();
			patientHistory = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPatientHistory(this.m_AccessionOrder.PatientId);
            YellowstonePathology.Business.Domain.PatientHistory priorPapRelatedHistory = patientHistory.GetPriorPapRelatedHistory(this.m_AccessionOrder.MasterAccessionNo, cutoffDate);

            foreach (YellowstonePathology.Business.Domain.PatientHistoryResult patientHistoryResult in priorPapRelatedHistory)
			{
				YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.GetAccessionOrderByMasterAccessionNo(patientHistoryResult.MasterAccessionNo);
				foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in accessionOrder.PanelSetOrderCollection)
				{
					string reportNo = null;
					DateTime? finalDate = null;
					string result = null;

					if (panelSetOrder.PanelSetId == panelSetThinPrepPap.PanelSetId ||
						panelSetOrder.PanelSetId == panelSetHPV.PanelSetId ||
						panelSetOrder.PanelSetId == panelSetHPV1618.PanelSetId ||
						panelSetOrder.PanelSetId == panelSetNGCT.PanelSetId ||
						panelSetOrder.PanelSetId == panelSetTrichomonas.PanelSetId)
					{
						reportNo = panelSetOrder.ReportNo;
						finalDate = panelSetOrder.FinalDate;
						result = panelSetOrder.GetResultWithTestName();
						XmlNode rowHistoryNodeClone = rowHistoryNode.Clone();
						string testFinaldate = YellowstonePathology.Business.Helper.DateTimeExtensions.DateStringFromNullable(finalDate);

						rowHistoryNodeClone.SelectSingleNode("descendant::w:r[w:t='prior_final_date']/w:t", this.m_NameSpaceManager).InnerText = testFinaldate;
						rowHistoryNodeClone.SelectSingleNode("descendant::w:r[w:t='prior_report_no']/w:t", this.m_NameSpaceManager).InnerText = reportNo;
						rowHistoryNodeClone.SelectSingleNode("descendant::w:r[w:t='prior_result']/w:t", this.m_NameSpaceManager).InnerText = result;
						mainTableNode.InsertAfter(rowHistoryNodeClone, insertAfterRow);
						insertAfterRow = rowHistoryNodeClone;
					}
				}
			}

			if (patientHistory.Count > 0)
			{
				this.DeleteRow("prior_report_no");
			}
			else
			{
				rowHistoryNode.SelectSingleNode("descendant::w:r[w:t='prior_final_date']/w:t", this.m_NameSpaceManager).InnerText = "No prior tests performed";
				rowHistoryNode.SelectSingleNode("descendant::w:r[w:t='prior_report_no']/w:t", this.m_NameSpaceManager).InnerText = string.Empty;
				rowHistoryNode.SelectSingleNode("descendant::w:r[w:t='prior_result']/w:t", this.m_NameSpaceManager).InnerText = string.Empty;
			}
		}

		public override void Publish()
		{
			base.Publish();
		}
	}
}
