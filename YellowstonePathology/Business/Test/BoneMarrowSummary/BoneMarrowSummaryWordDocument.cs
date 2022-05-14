﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.BoneMarrowSummary
{
    public class BoneMarrowSummaryWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public BoneMarrowSummaryWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\BoneMarrowSummary.4.xml";
            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();

            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            amendmentSection.SetAmendment(amendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            this.ReplaceText("report_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
            this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);

            string surgicalResult = string.Empty;
            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            string reportNo = surgicalTestOrder.ReportNo;

            XmlNode surgicalTableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='surgical_description']", this.m_NameSpaceManager);
            XmlNode descriptionRowNode = surgicalTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='surgical_description']", this.m_NameSpaceManager);
            XmlNode diagnosisRowNode = surgicalTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='surgical_diagnosis']", this.m_NameSpaceManager);
            foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
            {
                string description = surgicalSpecimen.DiagnosisIdFormatted + "  " +surgicalSpecimen.SpecimenOrder.Description;

                if (surgicalSpecimen.DiagnosisId > 1) reportNo = string.Empty;

                XmlNode descriptionRowClone = descriptionRowNode.Clone();
                descriptionRowClone.SelectSingleNode("descendant::w:r[w:t='surgical_description']/w:t", this.m_NameSpaceManager).InnerText = description;
                descriptionRowClone.SelectSingleNode("descendant::w:r[w:t='surgical_report_no']/w:t", this.m_NameSpaceManager).InnerText = reportNo;

                XmlNode diagnosisRowClone = diagnosisRowNode.Clone();
                this.SetXMLNodeParagraphDataNode(diagnosisRowClone, "surgical_diagnosis", surgicalSpecimen.Diagnosis);

                surgicalTableNode.InsertBefore(descriptionRowClone, descriptionRowNode);
                surgicalTableNode.InsertBefore(diagnosisRowClone, descriptionRowNode);
            }

            surgicalTableNode.RemoveChild(descriptionRowNode);
            surgicalTableNode.RemoveChild(diagnosisRowNode);

            if (string.IsNullOrEmpty(surgicalTestOrder.Comment) == false)
            {
                this.ReplaceText("surgical_comment", surgicalTestOrder.Comment);
            }
            else
            {
                this.DeleteRow("surgical_comment");
            }

            bool hasFinalAmendment = false;
            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
            {
                if (amendment.Final == true)
                {
                    hasFinalAmendment = true;
                    break;
                }
            }
            if (hasFinalAmendment == false)
            {
                this.DeleteRow("has_amendment_row");
                this.DeleteRow("has_no_amendment_row_1");
                this.ReplaceText("has_no_amendment_row_2", string.Empty);
            }
            else if (hasFinalAmendment == true)
            {
                this.DeleteRow("has_no_amendment_row_1");
                this.DeleteRow("has_no_amendment_row_2");
                this.ReplaceText("has_amendment_row", string.Empty);
            }

            XmlNode testTableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='test_name']", this.m_NameSpaceManager);            
            XmlNode rowTestNode = testTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='test_name']", this.m_NameSpaceManager);

            List<Business.Test.PanelSetOrder> testingSummaryList = this.m_AccessionOrder.PanelSetOrderCollection.GetBoneMarrowAccessionSummaryList(this.m_PanelSetOrder.ReportNo, true);            

            int surgicalPanelSetId = new Test.Surgical.SurgicalTest().PanelSetId;
            foreach (Business.Test.PanelSetOrder pso in testingSummaryList)
            {
                if (pso.PanelSetId != surgicalPanelSetId && pso.IncludeOnSummaryReport == true)
                {
                    string result = pso.ToResultString(this.m_AccessionOrder);
                    if (result == "The result string for this test has not been implemented.")
                    {
                        if (string.IsNullOrEmpty(pso.SummaryComment) == false)
                        {
                            result = pso.SummaryComment;
                        }
                        else
                        {
                            result = "Result reported separately.";
                        }
                    }

                    XmlNode rowTestNodeClone = rowTestNode.Clone();
                    rowTestNodeClone.SelectSingleNode("descendant::w:r[w:t='test_name']/w:t", this.m_NameSpaceManager).InnerText = pso.PanelSetName;
                    rowTestNodeClone.SelectSingleNode("descendant::w:r[w:t='test_report_no']/w:t", this.m_NameSpaceManager).InnerText = pso.ReportNo;

                    this.SetXMLNodeParagraphDataNode(rowTestNodeClone, "test_result", result);

                    testTableNode.InsertAfter(rowTestNodeClone, rowTestNode);
                }
            }

            testTableNode.RemoveChild(rowTestNode);
            this.ReplaceText("disclosure_statement", string.Empty);

            this.SaveReport();
        }        

        public override void Publish()
        {
            base.Publish();
        }
    }
}
