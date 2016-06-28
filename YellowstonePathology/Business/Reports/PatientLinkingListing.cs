using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Reports
{
	public class PatientLinkingListing : Report
	{
		string ReportBaseFileName = @"\\CFileServer\Documents\Reports\Templates\ReportBase.xml";
		public XmlDocument ReportBaseXml;
		protected XmlNamespaceManager NameSpaceManagerBase;

		public PatientLinkingListing()
		{
		}

		public void CreateReport(ObservableCollection<YellowstonePathology.Business.Patient.Model.PatientLinkingListItem> patientLinkingList)
		{
			this.m_ReportTemplate = @"\\CFileServer\documents\Reports\Templates\PatientLinkingListing.xml";

			this.m_ReportXml = new XmlDocument();
			this.m_ReportXml.Load(this.m_ReportTemplate);

			this.m_NameSpaceManager = new XmlNamespaceManager(this.m_ReportXml.NameTable);
			this.m_NameSpaceManager.AddNamespace("w", "http://schemas.microsoft.com/office/word/2003/wordml");
			this.m_NameSpaceManager.AddNamespace("wx", "http://schemas.microsoft.com/office/word/2003/auxHint");
			this.m_ReportSaveFileName = @"\\CFileServer\documents\Reports\Lab\PatientLinkingListing.xml";

			this.ReportBaseXml = new XmlDocument();
			this.ReportBaseXml.Load(ReportBaseFileName);

			this.NameSpaceManagerBase = new XmlNamespaceManager(ReportBaseXml.NameTable);
			this.NameSpaceManagerBase.AddNamespace("w", "http://schemas.microsoft.com/office/word/2003/wordml");
			this.NameSpaceManagerBase.AddNamespace("wx", "http://schemas.microsoft.com/office/word/2003/auxHint");

			string reportTitle = "Patient Linking Listing";
			XmlNode nodeHeaderTable = this.FindXmlTableInDetail("report_header");
			XmlNode nodeReportHeader = this.FindXmlTableRowInDetail("report_header", nodeHeaderTable);
			this.ReplaceTextInRowNode(nodeReportHeader, "report_header", reportTitle);

			XmlNode nodeTable = this.FindXmlTableInDetail("m_a");
			XmlNode nodeTemplateR1 = this.FindXmlTableRowInDetail("m_a", nodeTable);

			foreach (YellowstonePathology.Business.Patient.Model.PatientLinkingListItem patientLinkingListItem in patientLinkingList)
			{
				string masterAccessionNo = patientLinkingListItem.MasterAccessionNo.ToString();
				string patientId = patientLinkingListItem.PatientId;
				string firstName = patientLinkingListItem.PFirstName;
				string lastName = patientLinkingListItem.PLastName;
				string initial = patientLinkingListItem.PMiddleInitial;
				string ssn = patientLinkingListItem.PSSN;
				string birthdate = string.Empty;
				if(patientLinkingListItem.PBirthdate.HasValue) // != null)
				{
					birthdate = patientLinkingListItem.PBirthdate.Value.ToShortDateString();
				}

				XmlNode nodeNewR1 = nodeTemplateR1.Clone();
				this.ReplaceTextInRowNode(nodeNewR1, "patient_id", patientId);
				this.ReplaceTextInRowNode(nodeNewR1, "m_a", masterAccessionNo);
				this.ReplaceTextInRowNode(nodeNewR1, "last_name", lastName);
				this.ReplaceTextInRowNode(nodeNewR1, "first_name", firstName);
				this.ReplaceTextInRowNode(nodeNewR1, "m_i", initial);
				this.ReplaceTextInRowNode(nodeNewR1, "ssn", ssn);
				this.ReplaceTextInRowNode(nodeNewR1, "d_o_b", birthdate);

				nodeTable.AppendChild(nodeNewR1);
			}

			nodeTable.RemoveChild(nodeTemplateR1);
			this.SetReportBody(nodeTable);
			this.m_ReportXml.Save(m_ReportSaveFileName);
			this.PrintReport();
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
