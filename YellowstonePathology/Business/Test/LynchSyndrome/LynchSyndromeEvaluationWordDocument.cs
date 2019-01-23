using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LynchSyndromeEvaluationWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public LynchSyndromeEvaluationWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest lynchSyndromeEvaluationTest = new LynchSyndromeEvaluationTest();

            int molecularTestCount = 0;            
            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\LynchSyndromeEvaluation.2.xml";
            this.OpenTemplate();
            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetAmendments(this.m_PanelSetOrder.AmendmentCollection);
            
            YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation)this.m_PanelSetOrder;
            base.ReplaceText("report_result", panelSetOrderLynchSyndromeEvaluation.Result);
            base.ReplaceText("report_interpretation", panelSetOrderLynchSyndromeEvaluation.Interpretation);            
            
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrderLynchSyndromeEvaluation.OrderedOn, panelSetOrderLynchSyndromeEvaluation.OrderedOnId);
            this.ReplaceText("specimen_description", specimenOrder.Description);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

            YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(102, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);
            if (panelSetOrderLynchSyndromeIHC != null)
            {                
                base.ReplaceText("mlh1_result", panelSetOrderLynchSyndromeIHC.MLH1Result);
                base.ReplaceText("msh2_result", panelSetOrderLynchSyndromeIHC.MSH2Result);
                base.ReplaceText("msh6_result", panelSetOrderLynchSyndromeIHC.MSH6Result);
                base.ReplaceText("pms2_result", panelSetOrderLynchSyndromeIHC.PMS2Result);
            }

            if (panelSetOrderLynchSyndromeEvaluation.ReflexToBRAFMeth == true)
            {
                molecularTestCount += 1;
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTest brafMutationAnlaysis = new BRAFMutationAnalysis.BRAFMutationAnalysisTest();
                YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest brafV600EKTest = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest();
                YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTest rasRAFPanelTest = new YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTest();

                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(brafV600EKTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true)
                {
                    YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder panelSetOrderBraf = (YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafV600EKTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false);
                    base.ReplaceText("braf_result", panelSetOrderBraf.Result);
                    base.ReplaceText("molecular_analysis_header", "Molecular Analysis");
                }
                else if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(brafMutationAnlaysis.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true)
                {
                    YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder panelSetOrderBraf = (YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafMutationAnlaysis.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false);
                    base.ReplaceText("braf_result", panelSetOrderBraf.Result);
                    base.ReplaceText("molecular_analysis_header", "Molecular Analysis");
                }
                else if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(rasRAFPanelTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false) == true)
                {
                    YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder panelSetOrderRASRAF = (YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(rasRAFPanelTest.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, false);
                    base.ReplaceText("braf_result", panelSetOrderRASRAF.BRAFResult);
                    base.ReplaceText("molecular_analysis_header", "Molecular Analysis");
                }
            }
            else
            {
                this.DeleteRow("braf_result");
            }

			YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest panelSetMLH1 = new YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetMLH1.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true) == true)
            {
                molecularTestCount += 1;
				YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis panelSetOrderMLH1MethylationAnalysis = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetMLH1.PanelSetId, panelSetOrderLynchSyndromeEvaluation.OrderedOnId, true);
                base.ReplaceText("mlh1methylation_result", panelSetOrderMLH1MethylationAnalysis.Result);
                base.ReplaceText("molecular_analysis_header", "Molecular Analysis");
            }
            else
            {
                this.DeleteRow("mlh1methylation_result");                
            }

            if (molecularTestCount == 0)
            {
                this.DeleteRow("molecular_analysis_header");
            }

            base.ReplaceText("report_references", panelSetOrderLynchSyndromeEvaluation.ReportReferences);            
			base.ReplaceText("report_method", panelSetOrderLynchSyndromeEvaluation.Method);
            base.ReplaceText("pathologist_signature", panelSetOrderLynchSyndromeEvaluation.Signature);

            
            base.ReplaceText("summary_location_performed", this.m_AccessionOrder.PanelSetOrderCollection.GetLocationPerformedSummary(lynchSyndromeEvaluationTest.PanelSetIDList));

            this.SaveReport();
        }

        public void SetAmendments(YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendments)
        {
            XmlNode tableNode = m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='amendment_text']", this.m_NameSpaceManager);
            XmlNode AddendumDateTimeNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='amendment_date_time']", this.m_NameSpaceManager);
            XmlNode AddendumTextNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='amendment_text']", this.m_NameSpaceManager);
            XmlNode AddendumSignatureNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='amendment_signature']", this.m_NameSpaceManager);
            XmlNode ElectronicSignatureNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='signature_title']", this.m_NameSpaceManager);

            XmlNode nodeToInsertAfter = AddendumDateTimeNode;

            string amendmentTitle = string.Empty;
            if (amendments.Count != 0)
            {
                amendmentTitle = "Amendment";
            }
            m_ReportXml.SelectSingleNode("descendant::w:r[w:t='amendment_title']/w:t", this.m_NameSpaceManager).InnerText = amendmentTitle;

            foreach (YellowstonePathology.Business.Amendment.Model.Amendment dr in amendments)
            {

                if (dr.Final == true)
                {
                    string addendumText = dr.Text;
                    string addendumDateTime = dr.FinalDate.Value.ToShortDateString() + " - " + dr.FinalTime.Value.ToString("HH:mm");
                    string signature = dr.PathologistSignature;

                    XmlNode nodeLine1 = AddendumDateTimeNode.Clone();
                    XmlNode nodeLine2 = AddendumTextNode.Clone();
                    XmlNode nodeLine3 = AddendumSignatureNode.Clone();
                    XmlNode nodeLine4 = ElectronicSignatureNode.Clone();

                    nodeLine1.SelectSingleNode("descendant::w:r[w:t='amendment_date_time']/w:t", this.m_NameSpaceManager).InnerText = addendumDateTime;
                    this.SetXMLNodeParagraphDataNew(nodeLine2, "amendment_text", addendumText, this.m_NameSpaceManager);
                    nodeLine3.SelectSingleNode("descendant::w:r[w:t='amendment_signature']/w:t", this.m_NameSpaceManager).InnerText = signature;
                    nodeLine4.SelectSingleNode("descendant::w:r[w:t='signature_title']/w:t", this.m_NameSpaceManager).InnerText = "*** Electronic Signature ***";

                    tableNode.InsertAfter(nodeLine1, nodeToInsertAfter);
                    tableNode.InsertAfter(nodeLine2, nodeLine1);
                    tableNode.InsertAfter(nodeLine3, nodeLine2);
                    tableNode.InsertAfter(nodeLine4, nodeLine3);

                    nodeToInsertAfter = nodeLine4;
                }
            }

            tableNode.RemoveChild(AddendumDateTimeNode);
            tableNode.RemoveChild(ElectronicSignatureNode);
            tableNode.RemoveChild(AddendumSignatureNode);
            tableNode.RemoveChild(AddendumTextNode);
        }

        public void SetXMLNodeParagraphDataNew(XmlNode inputNode, string field, string data, XmlNamespaceManager nameSpaceManager)
        {
            XmlNode parentNode = inputNode.SelectSingleNode("descendant::w:tc[w:p/w:r/w:t='" + field + "']", nameSpaceManager);
            XmlNode childNode = parentNode.SelectSingleNode("descendant::w:p[w:r/w:t='" + field + "']", nameSpaceManager);

            string paragraphs = data;
            string[] lineSplit = paragraphs.Split('\n');

            for (int i = 0; i < lineSplit.Length; i++)
            {
                XmlNode childNodeClone = childNode.Clone();
                XmlNode node = childNodeClone.SelectSingleNode("descendant::w:r[w:t='" + field + "']/w:t", nameSpaceManager);
                node.InnerText = lineSplit[i];
                parentNode.AppendChild(childNodeClone);
            }
            parentNode.RemoveChild(childNode);
        }

        public override void Publish()
        {
            base.Publish();
        }        
    }
}
