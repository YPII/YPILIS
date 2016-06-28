using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile
{
	public class ComprehensiveColonCancerProfileWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public ComprehensiveColonCancerProfileWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\ComprehensiveColonCancerProfile.2.xml";
            this.OpenTemplate();
            this.SetDemographicsV2();
            this.SetReportDistribution();

            ComprehensiveColonCancerProfile comprehensiveColonCancerProfile = (ComprehensiveColonCancerProfile)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetOrder.ReportNo);
            ComprehensiveColonCancerProfileResult comprehensiveColonCancerProfileResult = new ComprehensiveColonCancerProfileResult(this.m_AccessionOrder, comprehensiveColonCancerProfile);

            base.ReplaceText("report_interpretation", comprehensiveColonCancerProfile.Interpretation);
            base.ReplaceText("ajcc_stage", comprehensiveColonCancerProfileResult.PanelSetOrderSurgical.AJCCStage);

            this.SetSpecimen(comprehensiveColonCancerProfileResult);
            this.SetIHC(comprehensiveColonCancerProfileResult);
            this.SetMolecularResults(comprehensiveColonCancerProfileResult);

            base.ReplaceText("pathologist_signature", comprehensiveColonCancerProfile.Signature);

            this.SaveReport();
        }

        private void SetSpecimen(ComprehensiveColonCancerProfileResult comprehensiveColonCancerProfileResult)
        {
            XmlNode diagnosisTableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='specimen_description']", this.m_NameSpaceManager);
            XmlNode rowSpecimenNode = diagnosisTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='specimen_description']", this.m_NameSpaceManager);
            XmlNode rowDiagnosisNode = diagnosisTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='specimen_diagnosis']", this.m_NameSpaceManager);
            XmlNode insertAfterRow = rowSpecimenNode;

            foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in comprehensiveColonCancerProfileResult.SurgicalSpecimenCollection)
            {
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(surgicalSpecimen.SpecimenOrderId);
                StringBuilder specimenDescription = new StringBuilder();
                specimenDescription.Append(specimenOrder.Description + ": ");
                foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    specimenDescription.Append(aliquotOrder.Label + ", ");
                }
                specimenDescription.Remove(specimenDescription.Length - 2, 2);
                XmlNode rowSpecimenNodeClone = rowSpecimenNode.Clone();
                rowSpecimenNodeClone.SelectSingleNode("descendant::w:r[w:t='specimen_description']/w:t", this.m_NameSpaceManager).InnerText = specimenDescription.ToString();
                rowSpecimenNodeClone.SelectSingleNode("descendant::w:r[w:t='surgical_reportno']/w:t", this.m_NameSpaceManager).InnerText = comprehensiveColonCancerProfileResult.PanelSetOrderSurgical.ReportNo;
                diagnosisTableNode.InsertAfter(rowSpecimenNodeClone, insertAfterRow);

                XmlNode rowDiagnosisNodeClone = rowDiagnosisNode.Clone();
                this.SetXMLNodeParagraphDataNode(rowDiagnosisNodeClone, "specimen_diagnosis", surgicalSpecimen.Diagnosis);
                diagnosisTableNode.InsertAfter(rowDiagnosisNodeClone, rowSpecimenNodeClone);

                insertAfterRow = rowDiagnosisNodeClone;
            }
            diagnosisTableNode.RemoveChild(rowSpecimenNode);
            diagnosisTableNode.RemoveChild(rowDiagnosisNode);
        }

        private void SetIHC(ComprehensiveColonCancerProfileResult comprehensiveColonCancerProfileResult)
        {
            XmlNode tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='mlh1_result']", this.m_NameSpaceManager);
            XmlNode rowmlh1Node = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='mlh1_result']", this.m_NameSpaceManager);
            XmlNode rowmsh2Node = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='msh2_result']", this.m_NameSpaceManager);
            XmlNode rowmsh6Node = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='msh6_result']", this.m_NameSpaceManager);
            XmlNode rowpms2Node = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='pms2_result']", this.m_NameSpaceManager);
            XmlNode insertAfterRow = rowmlh1Node;
            foreach (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC testOrder in comprehensiveColonCancerProfileResult.PanelSetOrderLynchSyndromeIHCCollection)
            {
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(testOrder.OrderedOnId);
                YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = specimenOrder.AliquotOrderCollection.GetByAliquotOrderId(testOrder.OrderedOnId);
                YellowstonePathology.Business.Test.LynchSyndrome.IHCResult ihcResult = YellowstonePathology.Business.Test.LynchSyndrome.IHCResult.CreateResultFromResultCode(testOrder.ResultCode);

                XmlNode rowmlh1NodeClone = rowmlh1Node.Clone();
                rowmlh1NodeClone.SelectSingleNode("descendant::w:r[w:t='mlh1_result']/w:t", this.m_NameSpaceManager).InnerText = ihcResult.MLH1Result.Description;
                rowmlh1NodeClone.SelectSingleNode("descendant::w:r[w:t='ihc_reportno']/w:t", this.m_NameSpaceManager).InnerText = testOrder.ReportNo;
                rowmlh1NodeClone.SelectSingleNode("descendant::w:r[w:t='ihc_block_label']/w:t", this.m_NameSpaceManager).InnerText = aliquotOrder.Label;
                tableNode.InsertAfter(rowmlh1NodeClone, insertAfterRow);

                XmlNode rowmsh2NodeClone = rowmsh2Node.Clone();
                rowmsh2NodeClone.SelectSingleNode("descendant::w:r[w:t='msh2_result']/w:t", this.m_NameSpaceManager).InnerText = ihcResult.MSH2Result.Description;
                tableNode.InsertAfter(rowmsh2NodeClone, rowmlh1NodeClone);

                XmlNode rowmsh6NodeClone = rowmsh6Node.Clone();
                rowmsh6NodeClone.SelectSingleNode("descendant::w:r[w:t='msh6_result']/w:t", this.m_NameSpaceManager).InnerText = ihcResult.MSH6Result.Description;
                tableNode.InsertAfter(rowmsh6NodeClone, rowmsh2NodeClone);

                XmlNode rowpms2NodeClone = rowpms2Node.Clone();
                rowpms2NodeClone.SelectSingleNode("descendant::w:r[w:t='pms2_result']/w:t", this.m_NameSpaceManager).InnerText = ihcResult.PMS2Result.Description;
                tableNode.InsertAfter(rowpms2NodeClone, rowmsh6NodeClone);

                insertAfterRow = rowpms2NodeClone;
            }
            if (comprehensiveColonCancerProfileResult.PanelSetOrderLynchSyndromeIHCCollection.Count > 0)
            {
                this.DeleteRow("mlh1_result");
                this.DeleteRow("msh2_result");
                this.DeleteRow("msh6_result");
                this.DeleteRow("pms2_result");
            }
            else
            {
                base.ReplaceText("ihc_reportno", "Not Included");
                base.ReplaceText("mlh1_result", "Not Included");
                base.ReplaceText("msh2_result", "Not Included");
                base.ReplaceText("msh6_result", "Not Included");
                base.ReplaceText("pms2_result", "Not Included");
            }
        }

        private void SetMolecularResults(ComprehensiveColonCancerProfileResult comprehensiveColonCancerProfileResult)
        {
            XmlNode tableNode = null;
            XmlNode rowResultNode = null;
            XmlNode insertAfterRow = null;

            foreach (YellowstonePathology.Business.Test.PanelSetOrder testOrder in comprehensiveColonCancerProfileResult.MolecularTestOrderCollection)
            {
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimen = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(testOrder.OrderedOnId);
                YellowstonePathology.Business.Test.AliquotOrder aliquot = specimen.AliquotOrderCollection.GetByAliquotOrderId(testOrder.OrderedOnId);
                if (testOrder is LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis)
                {
                    tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='mlh1promoter_result']", this.m_NameSpaceManager);
                    rowResultNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='mlh1promoter_result']", this.m_NameSpaceManager);
                    insertAfterRow = rowResultNode;
                    this.SetTestResults("mlh1promoter", ((LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis)testOrder).Result, testOrder.ReportNo, aliquot.Label, tableNode, rowResultNode, insertAfterRow);
                }
                else if (testOrder is KRASStandard.KRASStandardTestOrder)
                {
                    tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='kras_result']", this.m_NameSpaceManager);
                    rowResultNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='kras_result']", this.m_NameSpaceManager);
                    insertAfterRow = rowResultNode;
                    this.SetTestResults("kras", ((KRASStandard.KRASStandardTestOrder)testOrder).Result, testOrder.ReportNo, aliquot.Label, tableNode, rowResultNode, insertAfterRow);
                }
                else if (testOrder is KRASExon23Mutation.KRASExon23MutationTestOrder)
                {
                    tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='kras_result']", this.m_NameSpaceManager);
                    rowResultNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='kras_result']", this.m_NameSpaceManager);
                    insertAfterRow = rowResultNode;
                    this.SetTestResults("kras", ((KRASExon23Mutation.KRASExon23MutationTestOrder)testOrder).Result, testOrder.ReportNo, aliquot.Label, tableNode, rowResultNode, insertAfterRow);
                }
                else if (testOrder is KRASExon4Mutation.KRASExon4MutationTestOrder)
                {
                    tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='kras_result']", this.m_NameSpaceManager);
                    rowResultNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='kras_result']", this.m_NameSpaceManager);
                    insertAfterRow = rowResultNode;
                    this.SetTestResults("kras", ((KRASExon4Mutation.KRASExon4MutationTestOrder)testOrder).Result, testOrder.ReportNo, aliquot.Label, tableNode, rowResultNode, insertAfterRow);
                }
                else if (testOrder is BRAFV600EK.BRAFV600EKTestOrder)
                {
                    tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='braf_result']", this.m_NameSpaceManager);
                    rowResultNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='braf_result']", this.m_NameSpaceManager);
                    insertAfterRow = rowResultNode;
                    this.SetTestResults("braf", ((BRAFV600EK.BRAFV600EKTestOrder)testOrder).Result, testOrder.ReportNo, aliquot.Label, tableNode, rowResultNode, insertAfterRow);
                }
                else if (testOrder is NRASMutationAnalysis.NRASMutationAnalysisTestOrder)
                {
                    tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='nras_result']", this.m_NameSpaceManager);
                    rowResultNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='nras_result']", this.m_NameSpaceManager);
                    insertAfterRow = rowResultNode;
                    this.SetTestResults("nras", ((NRASMutationAnalysis.NRASMutationAnalysisTestOrder)testOrder).Result, testOrder.ReportNo, aliquot.Label, tableNode, rowResultNode, insertAfterRow);
                }
                else if (testOrder is RASRAFPanel.RASRAFPanelTestOrder)
                {
                    tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='kras_result']", this.m_NameSpaceManager);
                    rowResultNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='kras_result']", this.m_NameSpaceManager);
                    insertAfterRow = rowResultNode;
                    this.SetTestResults("kras", ((RASRAFPanel.RASRAFPanelTestOrder)testOrder).KRASResult, testOrder.ReportNo, aliquot.Label, tableNode, rowResultNode, insertAfterRow);

                    tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='braf_result']", this.m_NameSpaceManager);
                    rowResultNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='braf_result']", this.m_NameSpaceManager);
                    insertAfterRow = rowResultNode;
                    this.SetTestResults("braf", ((RASRAFPanel.RASRAFPanelTestOrder)testOrder).BRAFResult, testOrder.ReportNo, aliquot.Label, tableNode, rowResultNode, insertAfterRow);

                    tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='nras_result']", this.m_NameSpaceManager);
                    rowResultNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='nras_result']", this.m_NameSpaceManager);
                    insertAfterRow = rowResultNode;
                    this.SetTestResults("nras", ((RASRAFPanel.RASRAFPanelTestOrder)testOrder).NRASResult, testOrder.ReportNo, aliquot.Label, tableNode, rowResultNode, insertAfterRow);

                    tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='hras_result']", this.m_NameSpaceManager);
                    rowResultNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='hras_result']", this.m_NameSpaceManager);
                    insertAfterRow = rowResultNode;
                    this.SetTestResults("hras", ((RASRAFPanel.RASRAFPanelTestOrder)testOrder).HRASResult, testOrder.ReportNo, aliquot.Label, tableNode, rowResultNode, insertAfterRow);
                }
            }

            this.DeleteRow("mlh1promoter_result");
            this.DeleteRow("braf_result");
            this.DeleteRow("kras_result");
            this.DeleteRow("nras_result");
            this.DeleteRow("hras_result");

            if(comprehensiveColonCancerProfileResult.MolecularTestOrderCollection.Count > 0)
            {
                this.DeleteRow("None Performed");
            }
		}

        private void SetTestResults(string test, string result, string reportNo, string block, XmlNode tableNode, XmlNode rowNode, XmlNode insertAfterRow)
        {
            XmlNode rowNodeClone = rowNode.Clone();
            rowNodeClone.SelectSingleNode("descendant::w:r[w:t='" + test + "_result']/w:t", this.m_NameSpaceManager).InnerText = result;
            rowNodeClone.SelectSingleNode("descendant::w:r[w:t='" + test + "_reportno']/w:t", this.m_NameSpaceManager).InnerText = reportNo;
            rowNodeClone.SelectSingleNode("descendant::w:r[w:t='" + test + "_label']/w:t", this.m_NameSpaceManager).InnerText = block;
            tableNode.InsertAfter(rowNodeClone, insertAfterRow);

            //insertAfterRow = rowNodeClone;
        }

        public override void Publish()
		{
			base.Publish();
		}
	}
}
