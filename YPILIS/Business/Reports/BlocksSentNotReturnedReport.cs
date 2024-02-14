using System;
using System.Collections.Generic;
using System.Xml;

namespace YellowstonePathology.Business.Reports
{
    public class BlocksSentNotReturnedReport : Report
    {
        string ReportBaseFileName = @"\\fileserver\Documents\Reports\Templates\ReportBase.xml";
        public XmlDocument ReportBaseXml;
        protected XmlNamespaceManager NameSpaceManagerBase;

        public BlocksSentNotReturnedReport()
        { }

        public void CreateReport(DateTime reportDate)
        {
            MaterialTracking.Model.BlockSentNotReturnedCollection blockSentNotReturnedCollection = Gateway.SlideAccessionGateway.GetBlocksSentNotReturned();

            this.m_ReportTemplate = @"\\fileserver\documents\Reports\Templates\BlocksSentNotReturnedReport.xml";

            this.m_ReportXml = new XmlDocument();
            this.m_ReportXml.Load(this.m_ReportTemplate);

            this.m_NameSpaceManager = new XmlNamespaceManager(this.m_ReportXml.NameTable);
            this.m_NameSpaceManager.AddNamespace("w", "http://schemas.microsoft.com/office/word/2003/wordml");
            this.m_NameSpaceManager.AddNamespace("wx", "http://schemas.microsoft.com/office/word/2003/auxHint");
            this.m_ReportSaveFileName = @"\\fileserver\documents\Reports\MissingBlocks\BlocksSentNotReturned.FILEDATE.v1.xml";

            this.ReportBaseXml = new XmlDocument();
            this.ReportBaseXml.Load(ReportBaseFileName);

            this.NameSpaceManagerBase = new XmlNamespaceManager(ReportBaseXml.NameTable);
            this.NameSpaceManagerBase.AddNamespace("w", "http://schemas.microsoft.com/office/word/2003/wordml");
            this.NameSpaceManagerBase.AddNamespace("wx", "http://schemas.microsoft.com/office/word/2003/auxHint");

            string reportTitle = "Blocks Sent Not Returned - " + reportDate.ToLongDateString();
            this.m_ReportXml.SelectSingleNode("//w:r[w:t='report_header']/w:t", this.NameSpaceManagerBase).InnerText = reportTitle;

            XmlNode nodeTable = this.FindXmlTableInDetail("facility_id");
            XmlNode nodeTemplate = this.FindXmlTableRowInDetail("facility_id", nodeTable);

            foreach (MaterialTracking.Model.BlockSentNotReturned blockSentNotReturned in blockSentNotReturnedCollection)
            {
                string facilityName = blockSentNotReturned.FacilityId;
                Facility.Model.Facility facility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(blockSentNotReturned.FacilityId);
                if (facility != null)
                {
                    facilityName = string.IsNullOrEmpty(facility.FacilityName) ? blockSentNotReturned.FacilityId : facility.FacilityName;
                }

                XmlNode nodeNew = nodeTemplate.Clone();
                this.ReplaceTextInRowNode(nodeNew, "facility_id", facilityName);
                this.ReplaceTextInRowNode(nodeNew, "aliquot_id", blockSentNotReturned.AliquotId);
                this.ReplaceTextInRowNode(nodeNew, "log_date", blockSentNotReturned.LogDate.ToString("MM/dd/yyyy"));
                nodeTable.AppendChild(nodeNew);
            }

            nodeTable.RemoveChild(nodeTemplate);
            SetReportBody(nodeTable);
            SaveReport(reportDate.ToString());
        }

        private void SetReportBody(XmlNode nodeNew)
        {
            XmlNode nodeOld = ReportBaseXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='report_body']", this.m_NameSpaceManager);
            XmlNode nodeOldParent = nodeOld.ParentNode;

            XmlNode nodeImported = ReportBaseXml.ImportNode(nodeNew, true);

            nodeOldParent.AppendChild(nodeImported);
            nodeOldParent.RemoveChild(nodeOld);
        }
    }
}
