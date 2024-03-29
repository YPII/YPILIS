﻿using System;
using System.Collections.Generic;
using System.Xml;

namespace YellowstonePathology.Business.Reports.Surgical
{
	public class SurgicalMasterLog : Report
	{
		string ReportBaseFileName = @"\\fileserver\Documents\Reports\Templates\ReportBase.xml";
		public XmlDocument ReportBaseXml;
		protected XmlNamespaceManager NameSpaceManagerBase;

		public SurgicalMasterLog()
		{

		}


        public void CreateNorthernMtTechOnlyReport(DateTime reportDate)
        {
            this.m_ReportSaveFileName = @"\\fileserver\documents\Reports\Surgical\MasterLog\YEAR\MONTH\NorthernMtTechOnlyMasterLog.FILEDATE.v1.xml";
            Business.Surgical.SurgicalMasterLogList surgicalMasterLogList = new Business.Surgical.SurgicalMasterLogList();
            surgicalMasterLogList.FillNorthernMtTechnicalOnlyByDateAndLocation(reportDate);
            string reportTitle = "Northern Montana Technical Only Master Log - " + reportDate.ToLongDateString();
            this.BuildReport(reportDate, surgicalMasterLogList, reportTitle);
        }

		public void CreateDrReckReport(DateTime reportDate)
		{
			this.m_ReportSaveFileName = @"\\fileserver\documents\Reports\Surgical\MasterLog\YEAR\MONTH\DrReckMasterLog.FILEDATE.v1.xml";
			Business.Surgical.SurgicalMasterLogList surgicalMasterLogList = new Business.Surgical.SurgicalMasterLogList();
			surgicalMasterLogList.FillDrReck(reportDate);
			string reportTitle = "Dr Reck Master Log - " + reportDate.ToLongDateString();
			this.BuildReport(reportDate, surgicalMasterLogList, reportTitle);
		}

		public void CreateReport(DateTime reportDate)
        {
            this.m_ReportSaveFileName = @"\\fileserver\documents\Reports\Surgical\MasterLog\YEAR\MONTH\SurgicalMasterLog.FILEDATE.v1.xml";
            Business.Surgical.SurgicalMasterLogList surgicalMasterLogList = new Business.Surgical.SurgicalMasterLogList();
            surgicalMasterLogList.FillByReportDateAndLocation(reportDate);
            string reportTitle = "Surgical Master Log - " + reportDate.ToLongDateString();
            this.BuildReport(reportDate, surgicalMasterLogList, reportTitle);
        }

        private void BuildReport(DateTime reportDate, Business.Surgical.SurgicalMasterLogList surgicalMasterLogList, string reportTitle)
        {
			this.m_ReportTemplate = @"\\fileserver\documents\Reports\Templates\SurgicalMasterLog.6.xml";

			this.m_ReportXml = new XmlDocument();
			this.m_ReportXml.Load(this.m_ReportTemplate);

			this.m_NameSpaceManager = new XmlNamespaceManager(this.m_ReportXml.NameTable);
			this.m_NameSpaceManager.AddNamespace("w", "http://schemas.microsoft.com/office/word/2003/wordml");
			this.m_NameSpaceManager.AddNamespace("wx", "http://schemas.microsoft.com/office/word/2003/auxHint");			

			this.ReportBaseXml = new XmlDocument();
			this.ReportBaseXml.Load(ReportBaseFileName);

			this.NameSpaceManagerBase = new XmlNamespaceManager(ReportBaseXml.NameTable);
			this.NameSpaceManagerBase.AddNamespace("w", "http://schemas.microsoft.com/office/word/2003/wordml");
			this.NameSpaceManagerBase.AddNamespace("wx", "http://schemas.microsoft.com/office/word/2003/auxHint");
			
			this.m_ReportXml.SelectSingleNode("//w:r[w:t='report_title_first_page']/w:t", this.NameSpaceManagerBase).InnerText = reportTitle;			

			XmlNode nodeTable = this.FindXmlTableInDetail("accession_no");
			XmlNode nodeTemplateFacility = this.FindXmlTableRowInDetail("accessioning_location", nodeTable);
			XmlNode nodeTemplateR1 = this.FindXmlTableRowInDetail("accession_no", nodeTable);
			XmlNode nodeTemplateR2 = this.FindXmlTableRowInDetail("specimen", nodeTable);
			XmlNode nodeTemplateR3 = this.FindXmlTableRowInDetail("blank_row", nodeTable);            

            List<string> accessioningLocations = new List<string>();
            accessioningLocations.Add("Billings");
            accessioningLocations.Add("Cody");
			accessioningLocations.Add("Havre");

			foreach (string location in accessioningLocations)
			{
                XmlNode nodeNewLocation = nodeTemplateFacility.Clone();
				this.ReplaceTextInRowNode(nodeNewLocation, "accessioning_location", "Accessioning Location: " + location);
				nodeTable.AppendChild(nodeNewLocation);

				foreach (Business.Surgical.SurgicalMasterLogItem surgicalMasterLogItem in surgicalMasterLogList)
				{
                    YellowstonePathology.Business.Facility.Model.Facility accessioningFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(surgicalMasterLogItem.AccessioningFacilityId);
                    if (accessioningFacility.AccessioningLocation == location)
					{
						string reportNo = surgicalMasterLogItem.ReportNo;
						string patientName = surgicalMasterLogItem.PFirstName + " " + surgicalMasterLogItem.PLastName;
						if (surgicalMasterLogItem.PBirthdate.HasValue)
						{
							patientName += " - " + surgicalMasterLogItem.PBirthdate.Value.ToShortDateString();
						}
						string physicianClient = surgicalMasterLogItem.PhysicianName + " - " + surgicalMasterLogItem.ClientName;
						string aliquotCount = surgicalMasterLogItem.AliquotCount.ToString();

						XmlNode nodeNewR1 = nodeTemplateR1.Clone();
						this.ReplaceTextInRowNode(nodeNewR1, "accession_no", reportNo);
                        this.ReplaceTextInRowNode(nodeNewR1, "client_accession", surgicalMasterLogItem.ClientAccessionNo);
                        this.ReplaceTextInRowNode(nodeNewR1, "patient_name", patientName);
						this.ReplaceTextInRowNode(nodeNewR1, "physician_client", physicianClient);
						this.ReplaceTextInRowNode(nodeNewR1, "alq_cnt", aliquotCount);

						nodeTable.AppendChild(nodeNewR1);
						foreach (Business.Surgical.MasterLogItem masterLogItem in surgicalMasterLogItem.MasterLogList)
						{
							XmlNode nodeNewR2 = nodeTemplateR2.Clone();
							string specimen = masterLogItem.DiagnosisId.ToString() + ". " + masterLogItem.Description;
							this.ReplaceTextInRowNode(nodeNewR2, "specimen", specimen);
							nodeTable.AppendChild(nodeNewR2);
						}

						XmlNode nodeNewR3 = nodeTemplateR3.Clone();
						this.ReplaceTextInRowNode(nodeNewR3, "blank_row", "");
						nodeTable.AppendChild(nodeNewR3);
					}
				}
			}

			nodeTable.RemoveChild(nodeTemplateFacility);
			nodeTable.RemoveChild(nodeTemplateR1);
			nodeTable.RemoveChild(nodeTemplateR2);
			nodeTable.RemoveChild(nodeTemplateR3);
			SetReportBody(nodeTable);
			SaveReport(reportDate.ToString());
		}

		public void SetReportBody(XmlNode nodeNew)
		{
			XmlNode nodeOld = ReportBaseXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='report_body']", this.m_NameSpaceManager);
			XmlNode nodeOldParent = nodeOld.ParentNode;

			XmlNode nodeImported = ReportBaseXml.ImportNode(nodeNew, true);

			nodeOldParent.AppendChild(nodeImported);
			nodeOldParent.RemoveChild(nodeOld);
		}
	}
}
