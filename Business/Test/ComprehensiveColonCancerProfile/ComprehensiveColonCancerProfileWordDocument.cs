﻿using System;
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
			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\ComprehensiveColonCancerProfile.1.xml";
			this.OpenTemplate();
			this.SetDemographicsV2();
			this.SetReportDistribution();

			ComprehensiveColonCancerProfile comprehensiveColonCancerProfile = (ComprehensiveColonCancerProfile)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetOrder.ReportNo);
			ComprehensiveColonCancerProfileResult comprehensiveColonCancerProfileResult = new ComprehensiveColonCancerProfileResult(this.m_AccessionOrder, comprehensiveColonCancerProfile);

			base.ReplaceText("report_interpretation", comprehensiveColonCancerProfile.Interpretation);
			base.ReplaceText("ajcc_stage", comprehensiveColonCancerProfileResult.PanelSetOrderSurgical.AJCCStage);

            YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest lynchSyndromeEvaluationTest = new LynchSyndrome.LynchSyndromeEvaluationTest();
            XmlNode diagnosisTableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='specimen_description']", this.m_NameSpaceManager);
            XmlNode rowSpecimenNode = diagnosisTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='specimen_description']", this.m_NameSpaceManager);
            XmlNode rowDiagnosisNode = diagnosisTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='specimen_diagnosis']", this.m_NameSpaceManager);
            XmlNode insertAfterRow = rowSpecimenNode;
            int count = 0;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder pso in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                if(pso.PanelSetId == lynchSyndromeEvaluationTest.PanelSetId)
                {
                    count++;
                    YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(pso.OrderedOnId);
                    YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen = comprehensiveColonCancerProfileResult.PanelSetOrderSurgical.SurgicalSpecimenCollection.GetBySpecimenOrderId(specimenOrder.SpecimenOrderId);
                    YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(pso.OrderedOnId);
                    string specimenDescription = specimenOrder.Description;
                    if (aliquotOrder != null) specimenDescription += ": " + aliquotOrder.Label;

                    XmlNode rowSpecimenNodeClone = rowSpecimenNode.Clone();
                    rowSpecimenNodeClone.SelectSingleNode("descendant::w:r[w:t='specimen_description']/w:t", this.m_NameSpaceManager).InnerText = specimenDescription;
                    if (count == 1)
                    {
                        rowSpecimenNodeClone.SelectSingleNode("descendant::w:r[w:t='surgical_reportno']/w:t", this.m_NameSpaceManager).InnerText = comprehensiveColonCancerProfileResult.PanelSetOrderSurgical.ReportNo;
                    }
                    else
                    {
                        rowSpecimenNodeClone.SelectSingleNode("descendant::w:r[w:t='surgical_reportno']/w:t", this.m_NameSpaceManager).InnerText = string.Empty;
                    }
                    diagnosisTableNode.InsertAfter(rowSpecimenNodeClone, insertAfterRow);

                    XmlNode rowDiagnosisNodeClone = rowDiagnosisNode.Clone();
                    string diagnosis = surgicalSpecimen.Diagnosis;

                    this.SetXMLNodeParagraphDataNode(rowDiagnosisNodeClone, "specimen_diagnosis", diagnosis);
                    diagnosisTableNode.InsertAfter(rowDiagnosisNodeClone, rowSpecimenNodeClone);

                    insertAfterRow = rowDiagnosisNodeClone;
                }
            }
            diagnosisTableNode.RemoveChild(rowSpecimenNode);
            diagnosisTableNode.RemoveChild(rowDiagnosisNode);

            if (comprehensiveColonCancerProfileResult.IHCResult != null)
            {
                base.ReplaceText("mlh1_result", comprehensiveColonCancerProfileResult.IHCResult.MLH1Result.Description);
                base.ReplaceText("msh2_result", comprehensiveColonCancerProfileResult.IHCResult.MSH2Result.Description);
                base.ReplaceText("msh6_result", comprehensiveColonCancerProfileResult.IHCResult.MSH6Result.Description);
                base.ReplaceText("pms2_result", comprehensiveColonCancerProfileResult.IHCResult.PMS2Result.Description);
            }
            else
            {
                base.ReplaceText("mlh1_result", "Not Included");
                base.ReplaceText("msh2_result", "Not Included");
                base.ReplaceText("msh6_result", "Not Included");
                base.ReplaceText("pms2_result", "Not Included");
            }
            if (comprehensiveColonCancerProfileResult.PanelSetOrderLynchSyndromeIHC != null)
            {
                base.ReplaceText("ihc_reportno", comprehensiveColonCancerProfileResult.PanelSetOrderLynchSyndromeIHC.ReportNo);
            }
            else
            {
                base.ReplaceText("ihc_reportno", "Not Included");
            }

            DateTime testChangeDate = DateTime.Parse("11/23/2015");
            if(comprehensiveColonCancerProfile.OrderDate.Value > testChangeDate)
            {
                RenderRasRafResults(comprehensiveColonCancerProfile, comprehensiveColonCancerProfileResult);
            }
            else
            {
                RenderSeparateTestResults(comprehensiveColonCancerProfile, comprehensiveColonCancerProfileResult);
            }
                        
            if (comprehensiveColonCancerProfileResult.PanelSetOrderMLH1MethylationAnalysis != null)
			{
				base.ReplaceText("mlh1promoter_result", comprehensiveColonCancerProfileResult.PanelSetOrderMLH1MethylationAnalysis.Result);
				base.ReplaceText("mlh1promoter_reportno", comprehensiveColonCancerProfileResult.PanelSetOrderMLH1MethylationAnalysis.ReportNo);
			}
			else
			{
				this.DeleteRow("mlh1promoter_result");
			}

            base.ReplaceText("pathologist_signature", comprehensiveColonCancerProfile.Signature);

			this.SaveReport();
		}

        private void RenderRasRafResults(ComprehensiveColonCancerProfile comprehensiveColonCancerProfile,
            ComprehensiveColonCancerProfileResult comprehensiveColonCancerProfileResult)
        {
            if (comprehensiveColonCancerProfileResult.RASRAFIsOrdered == true)
            {
                base.ReplaceText("braf_result", comprehensiveColonCancerProfileResult.RASRAFTestOrder.BRAFResult);
                base.ReplaceText("braf_reportno", comprehensiveColonCancerProfile.ReportNo);

                base.ReplaceText("kras_result", comprehensiveColonCancerProfileResult.RASRAFTestOrder.KRASResult);
                base.ReplaceText("kras_reportno", comprehensiveColonCancerProfile.ReportNo);

                base.ReplaceText("nras_result", comprehensiveColonCancerProfileResult.RASRAFTestOrder.NRASResult);
                base.ReplaceText("nras_reportno", comprehensiveColonCancerProfile.ReportNo);

                base.ReplaceText("hras_result", comprehensiveColonCancerProfileResult.RASRAFTestOrder.HRASResult);
                base.ReplaceText("hras_reportno", comprehensiveColonCancerProfile.ReportNo);
                this.DeleteRow("None Performed");
            }
            else
            {
                this.DeleteRow("kras_result");
                this.DeleteRow("braf_result");
                this.DeleteRow("nras_result");
                this.DeleteRow("hras_result");
            }
        }

        private void RenderSeparateTestResults(ComprehensiveColonCancerProfile comprehensiveColonCancerProfile, 
            ComprehensiveColonCancerProfileResult comprehensiveColonCancerProfileResult)
        {
            bool deleteKrasResult = true;
            bool hasMolecularTest = false;
            if (comprehensiveColonCancerProfileResult.KRASStandardIsOrderd == true)
            {
                base.ReplaceText("kras_result", comprehensiveColonCancerProfileResult.KRASStandardTestOrder.Result);
                base.ReplaceText("kras_reportno", comprehensiveColonCancerProfileResult.KRASStandardTestOrder.ReportNo);
                deleteKrasResult = false;
                hasMolecularTest = true;
            }

            if (comprehensiveColonCancerProfileResult.BRAFV600EKIsOrdered == true)
            {
                base.ReplaceText("braf_result", comprehensiveColonCancerProfileResult.BRAFV600EKTestOrder.Result);
                base.ReplaceText("braf_reportno", comprehensiveColonCancerProfileResult.BRAFV600EKTestOrder.ReportNo);
                hasMolecularTest = true;
            }
            else
            {
                this.DeleteRow("braf_result");
            }

            if (comprehensiveColonCancerProfileResult.KRASExon23MutationIsOrdered == true)
            {
                base.ReplaceText("kras_result", comprehensiveColonCancerProfileResult.KRASExon23MutationTestOrder.Result);
                base.ReplaceText("kras_reportno", comprehensiveColonCancerProfileResult.KRASExon23MutationTestOrder.ReportNo);
                deleteKrasResult = false;
                hasMolecularTest = true;
            }

            if (comprehensiveColonCancerProfileResult.KRASExon4MutationIsOrdered == true)
            {
                base.ReplaceText("kras_result", comprehensiveColonCancerProfileResult.KRASExon4MutationTestOrder.Result);
                base.ReplaceText("kras_reportno", comprehensiveColonCancerProfileResult.KRASExon4MutationTestOrder.ReportNo);
                deleteKrasResult = false;
                hasMolecularTest = true;
            }

            if (comprehensiveColonCancerProfileResult.NRASMutationAnalysisIsOrdered == true)
            {
                base.ReplaceText("nras_result", comprehensiveColonCancerProfileResult.NRASMutationAnalysisTestOrder.Result);
                base.ReplaceText("nras_reportno", comprehensiveColonCancerProfileResult.NRASMutationAnalysisTestOrder.ReportNo);
                hasMolecularTest = true;
            }
            else
            {
                this.DeleteRow("nras_result");
            }

            if(deleteKrasResult == true)
            {
                this.DeleteRow("kras_result");
            }

            if(hasMolecularTest == true)
            {
                this.DeleteRow("None Performed");
            }
            this.DeleteRow("hras_result");

        }

        public override void Publish()
		{
			base.Publish();
		}
	}
}
