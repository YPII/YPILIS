﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HematopathologySummary
{
    public class HematopathologySummaryWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public HematopathologySummaryWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            HematopathologySummaryTestOrder panelSetOrderHematopathologySummary = (HematopathologySummaryTestOrder)this.m_PanelSetOrder;
            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\HematopathologySummary.1.xml";
            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();            

            //YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
            //amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

            string reportResult = panelSetOrderHematopathologySummary.Result;
            if (string.IsNullOrEmpty(reportResult))
            {
                reportResult = string.Empty;
            }
                        
            this.ReplaceText("summary_interpretation", panelSetOrderHematopathologySummary.Interpretation);

            this.ReplaceText("report_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
            this.ReplaceText("pathologist_signature", this.m_PanelSetOrder.Signature);

            string surgicalResult = string.Empty;
            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            foreach(YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
            {
                surgicalResult += surgicalSpecimen.Diagnosis + Environment.NewLine;
            }

            this.ReplaceText("surgical_result", surgicalResult);
            this.ReplaceText("surgical_report_no", surgicalTestOrder.ReportNo);

            XmlNode testTableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='test_name']", this.m_NameSpaceManager);            
            XmlNode rowTestNode = testTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='test_name']", this.m_NameSpaceManager);

            List<Business.Test.PanelSetOrder> testingSummaryList = this.GetTestingSummaryList(this.m_AccessionOrder.PanelSetOrderCollection);
            foreach (Business.Test.PanelSetOrder pso in testingSummaryList)
            {
                XmlNode rowTestNodeClone = rowTestNode.Clone();
                rowTestNodeClone.SelectSingleNode("descendant::w:r[w:t='test_name']/w:t", this.m_NameSpaceManager).InnerText = pso.PanelSetName;
                rowTestNodeClone.SelectSingleNode("descendant::w:r[w:t='test_report_no']/w:t", this.m_NameSpaceManager).InnerText = pso.ReportNo;

                this.SetXMLNodeParagraphDataNode(rowTestNodeClone, "test_result", pso.ToResultString(this.m_AccessionOrder));

                testTableNode.InsertAfter(rowTestNodeClone, rowTestNode);                    
            }

            testTableNode.RemoveChild(rowTestNode);
            this.ReplaceText("disclosure_statement", string.Empty);

            this.SaveReport();
        }

        private List<Business.Test.PanelSetOrder> GetTestingSummaryList(Business.Test.PanelSetOrderCollection panelSetOrderCollection)
        {
            List<Business.Test.PanelSetOrder> result = new List<PanelSetOrder>();
            YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSets = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();

            Business.Test.PanelSetOrderCollection flow = new PanelSetOrderCollection();
            Business.Test.PanelSetOrderCollection cyto = new PanelSetOrderCollection();
            Business.Test.PanelSetOrderCollection fish = new PanelSetOrderCollection();
            Business.Test.PanelSetOrderCollection molecular = new PanelSetOrderCollection();
            Business.Test.PanelSetOrderCollection other = new PanelSetOrderCollection();

            List<int> exclusionList = new List<int>();
            exclusionList.Add(13);
            exclusionList.Add(197);
            exclusionList.Add(262);
            exclusionList.Add(268);

            foreach (Business.Test.PanelSetOrder pso in panelSetOrderCollection)
            {
                if(exclusionList.IndexOf(pso.PanelSetId) == -1)
                {
                    YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSets.GetPanelSet(pso.PanelSetId);
                    if (panelSet.CaseType == YellowstonePathology.Business.CaseType.FlowCytometry) flow.Insert(0, pso);
                    else if (panelSet.CaseType == YellowstonePathology.Business.CaseType.Cytogenetics) cyto.Insert(0, pso);
                    else if (panelSet.CaseType == YellowstonePathology.Business.CaseType.FISH) fish.Insert(0, pso);
                    else if (panelSet.CaseType == YellowstonePathology.Business.CaseType.Molecular) molecular.Insert(0, pso);
                    else other.Insert(0, pso);
                }
            }

            result.AddRange(other);
            result.AddRange(molecular);
            result.AddRange(fish);
            result.AddRange(cyto);
            result.AddRange(flow);
            return result;
        }

        public override void Publish()
        {
            base.Publish();
        }
    }
}
