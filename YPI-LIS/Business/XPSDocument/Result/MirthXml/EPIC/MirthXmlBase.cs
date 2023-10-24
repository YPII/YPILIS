using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Globalization;

using YellowstonePathology.Document.Result.Data;
using YellowstonePathology.Document.Result.Xps;

namespace YellowstonePathology.Document.Result.MirthXml.EPIC
{
	/// <summary>Mirth HL7 data base class
	/// </summary>
	public abstract class MirthXmlBase : XDocument
	{

		#region Private constants
		/// <summary>root XML element name
		/// </summary>
		private const string m_RootName = "HL7Message";
		/// <summary>report author name
		/// </summary>
		protected const string m_ReportAuthor = "Yellowstone Pathology";
		/// <summary>address line
		/// </summary>
		protected const string m_AddressLine = "2900 12th Ave. North, Ste. 295W";
		/// <summary>city, state, zip
		/// </summary>
		protected const string m_CityStateZip = "Billings, Mt 59101";

		#region Temp constants (for not found data)
		//ORC/OBR data
		private const string m_FillerOrderNumber = "S12-2967";
		private const string m_FillerOrderCode = "YPILIS";

		//ORC segment data
		private readonly DateTime m_TransactionDateTime = DateTime.Now;
		private const string m_OrderingProviderID = "1639355928";
		private const string m_OrderingProviderLastName = "Setzer";
		private const string m_OrderingProviderFirstName = "Shannon";
		private const string m_OrderingProviderAssigningAuthority = "NPI";
		private const string m_OrderingProviderIdentifierTypeCode = "NPI";

		//OBR segment data
		private const string m_UniversalServiceIdentifier = "YPI";
		private const string m_UniversalServiceIdentifierText = "Pathology Procedure/Specimen";
		private readonly DateTime m_RequestedDateTime = DateTime.Parse("2012-02-24 00:00");
		private readonly DateTime m_SpecimenReceivedDateTime = DateTime.Parse("2012-02-24 00:00");
		private readonly DateTime m_ResultsDateTime = DateTime.Parse("2012-05-07 15:34");
		private const string m_PrincipalResultInterpreterID = "1184820482";
		private const string m_PrincipalResultInterpreterLastName = "Emerick";
		private const string m_PrincipalResultInterpreterFirstName = "Kerrie";
		private const string m_PrincipalResultInterpreterAssigningAuthority = "NPI";
		private const string m_PrincipalResultInterpreterIdentifierTypeCode = "NPI";

		#endregion Temp constants (for not found data)
		#endregion Private constants

		#region Private data
		/// <summary>root XML element object
		/// </summary>
		private readonly XElement m_Root;
		/// <summary>page header XML data block
		/// </summary>
		private readonly ReportHeaderData m_PageHeaderData;
		/// <summary>numeric format (used for normal read/write string representation of numerics in non-US cultures)
		/// </summary>
		protected readonly static NumberFormatInfo m_Nfi = new CultureInfo("en-US").NumberFormat;
		/// <summary>current OBX segment serial number
		/// </summary>
		protected int _obxSegmentNo = 1;
		#endregion Private data

		#region Constructors
		/// <summary>constructor
		/// </summary>
		/// <param name="data">report XML data</param>
		public MirthXmlBase(string reportName, ReportHeaderData pageHeaderData)
		{
			m_PageHeaderData = pageHeaderData;
			m_Root = new XElement(m_RootName);
			AddMshSegment();
			AddPidSegment(pageHeaderData);
			AddOrcSegment();
			AddObrSegment();
			AddHeaderObxSegments(reportName);

			Add(m_Root);
		}
		#endregion Constructors

		#region Protected methods
		/// <summary>method add OBX segment data block to document's root element
		/// </summary>
		/// <param name="root">document's root element</param>
		/// <param name="text">OBX segment value</param>
		protected void AddObxSegment(string text = "")
		{
			XElement obx = new XElement("OBX");
			obx.AddChildElement("OBX.1").AddChildHl7Element("OBX.1.1", _obxSegmentNo.ToString());
			_obxSegmentNo++;
			obx.AddChildElement("OBX.2").AddChildHl7Element("OBX.2.1", "TX");
			obx.AddChildElement("OBX.3").AddChildHl7Element("OBX.3.1", "&GT");
			//obx.AddChildElement("OBX.4");
			obx.AddChildElement("OBX.5").AddChildHl7Element("OBX.5.1", text);
			obx.AddChildElement("OBX.11").AddChildHl7Element("OBX.11.1", "F");
			m_Root.Add(obx);
		}
		/// <summary>method add OBX segments collection for report section (empty segment + segment with section body)
		/// </summary>
		/// <param name="segmentNo">OBX segment serial number</param>
		/// <param name="body">section body text</param>
		protected void AddObxSegmentsForSection(string body)
		{
			AddObxSegment();
			AddObxSegments(body);
		}
		/// <summary>method add OBX segments collection for report headered section (empty segment + segment with section header + segment with section body)
		/// </summary>
		/// <param name="header">section header text</param>
		/// <param name="body">section body text</param>
		protected void AddObxSegmentsForHeaderSection(string header, string body)
		{
			AddObxSegment();
			AddObxSegment(header);
			AddObxSegments(body);
		}
		/// <summary>method add OBX segments collection for all amendment sections
		/// </summary>
		/// <param name="header">section header text</param>
		/// <param name="amendments">array of XML elements with amendments parameters</param>
		protected void AddObxSegmentsForAmendments(IEnumerable<XElement> amendments)
		{
			if (amendments != null)
			{
				for (int i = 0; i < amendments.Count(); i++)
				{
					AddObxSegmentsForAmendment(amendments.ElementAt(i));
				}
			}
		}
		/// <summary>method write report's amendment sections
		/// </summary>
		/// <param name="amendment">XML element with amendment parameters</param>
		private void AddObxSegmentsForAmendment(XElement amendment)
		{
			AddObxSegmentsForSection(string.Format("{0}: {1}", amendment.Element(YpReportDataBase.AmendmentType).Value, amendment.GetDateTime(YpReportDataBase.AmendmentTime, "MM/dd/yyyy - HH:mm")));
			//AddObxSegment(amendment.GetStringValue(YpReportDataBase.AmendmentText));
			AddObxSegments(amendment.GetStringValue(YpReportDataBase.AmendmentText));
			AddObxSegmentsForSection(amendment.GetStringValue(YpReportDataBase.AmendmentPathologistSignature));
		}
		/// <summary>method write standard trailer sectiohs ("OtherReports", "ReportDistribution", "Disclaimer") and initialize page numbers in next page's headers
		/// </summary>
		/// <param name="segmentNo">OBX segment start serial number</param>
		/// <param name="otherReports">string for "Other report" section</param>
		/// <param name="reportDistributions">XML elements list for "ReportDistribution" section</param>
		/// <param name="disclaimerIndex">index of disclaimer text in disclaimer text array</param>
		protected void AddObxSegmentsForStandardTrailerSections(string otherReports, IEnumerable<XElement> reportDistributions, int disclaimerIndex)
		{
			AddObxSegmentsForHeaderSection(YpReportBase.OtherReportsLabel, otherReports);
			AddReportDistributionObxSegments(reportDistributions);
			AddObxSegmentsForSection(YpReportDataBase.DisclaimerText[disclaimerIndex]);
		}
		/// <summary>method add OBX segments collection for report distribution section
		/// </summary>
		/// <param name="reportDistributions">XML elements list for "ReportDistribution" section</param>
		protected void AddReportDistributionObxSegments(IEnumerable<XElement> reportDistributions)
		{
			AddObxSegmentsForSection(LeukemiaLymphomaReport.ReportDistributionLabel);
			for (int i = 0; i < reportDistributions.Count(); i++)
			{
				AddObxSegment(reportDistributions.ElementAt(i).Value);
			}
		}
		#endregion Protected methods

		#region Private methods
		/// <summary>method add MSH segment data block to document's root element
		/// </summary>
		private void AddMshSegment()
		{
			const string type = "ORU";
			const string subType = "R01";

			string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
			string messageID = Guid.NewGuid().ToString().Replace("-", string.Empty);

			XElement msh = new XElement("MSH");
			msh.AddChildElement("MSH.1", @"|");
			msh.AddChildElement("MSH.2", @"^~\&");
			msh.AddChildElement("MSH.3").AddChildElement("MSH.3.1", "YPILIS");
			XElement msh4 = msh.AddChildElement("MSH.4");
			msh4.AddChildElement("MSH.4.1", "YPIBILLINGS");
			msh4.AddChildElement("MSH.4.2", "27D0946844");
			msh4.AddChildElement("MSH.4.3", "CLIA");
			msh.AddChildElement("MSH.7").AddChildElement("MSH.7.1", timeStamp);
			XElement msh9 = msh.AddChildElement("MSH.9");
			msh9.AddChildElement("MSH.9.1", type);
			msh9.AddChildElement("MSH.9.2", subType);
			msh.AddChildElement("MSH.10").AddChildElement("MSH.10.1", messageID);
			msh.AddChildElement("MSH.11").AddChildElement("MSH.11.1", "P");
			msh.AddChildElement("MSH.12").AddChildElement("MSH.12.1", "2.3");
			m_Root.Add(msh);
		}
		/// <summary>method add PID segment data block to document's root element
		/// </summary>
		private void AddPidSegment(ReportHeaderData headerData)
		{
			//ReportHeaderData headerData = m_Data.PageHeader;
			XElement pid = new XElement("PID");
			XElement pid5 = pid.AddChildElement("PID.5");
			pid5.AddChildHl7Element("PID.5.1", headerData.GetStringValue("PLastName"));
			pid5.AddChildHl7Element("PID.5.2", headerData.GetStringValue("PFirstName"));
			pid.AddChildElement("PID.7").AddChildHl7Element("PID.7.1", headerData.GetDateTime(ReportHeaderData.PatientBirthdate, "yyyyMMdd"));
			pid.AddChildElement("PID.8").AddChildHl7Element("PID.8.1", headerData.GetStringValue("PSex"));
			pid.AddChildElement("PID.19").AddChildHl7Element("PID.19.1", headerData.GetStringValue("PSSN"));
			m_Root.Add(pid);
		}
		/// <summary>method add ORC segment data block to document's root element
		/// </summary>
		private void AddOrcSegment()
		{
			XElement orc = new XElement("ORC");
			orc.AddChildElement("ORC.1").AddChildHl7Element("ORC.1.1", "RE");
			orc.AddChildElement("ORC.2").AddChildHl7Element("ORC.2.1", "58683");
			XElement orc3 = orc.AddChildElement("ORC.3");
			orc3.AddChildHl7Element("ORC.3.1", m_FillerOrderNumber);
			orc3.AddChildHl7Element("ORC.3.2", m_FillerOrderCode);
			orc.AddChildElement("ORC.5").AddChildHl7Element("ORC.5.1", "CM");
			orc.AddChildElement("ORC.9").AddChildElement("ORC.9.1", m_TransactionDateTime.ToString("yyyyMMddHHmm"));
			XElement orc12 = orc.AddChildElement("ORC.12");
			orc12.AddChildHl7Element("ORC.12.1", m_OrderingProviderID);
			orc12.AddChildHl7Element("ORC.12.2", m_OrderingProviderLastName);
			orc12.AddChildHl7Element("ORC.12.3", m_OrderingProviderFirstName);
			orc12.AddChildHl7Element("ORC.12.9", m_OrderingProviderAssigningAuthority);
			orc12.AddChildHl7Element("ORC.12.13", m_OrderingProviderIdentifierTypeCode);
			m_Root.Add(orc);
		}
		/// <summary>method add OBR segment data block to document's root element
		/// </summary>
		private void AddObrSegment()
		{
			XElement obr = new XElement("OBR");
			obr.AddChildElement("OBR.1").AddChildHl7Element("OBR.1.1", "1");
			XElement obr3 = obr.AddChildElement("OBR.3");
			obr3.AddChildHl7Element("OBR.3.1", m_FillerOrderNumber);
			obr3.AddChildHl7Element("OBR.3.2", m_FillerOrderCode);
			XElement obr4 = obr.AddChildElement("OBR.4");
			obr4.AddChildHl7Element("OBR.4.1", m_UniversalServiceIdentifier);
			obr4.AddChildHl7Element("OBR.4.2", m_UniversalServiceIdentifierText);
			obr.AddChildElement("OBR.7").AddChildHl7Element("OBR.7.1", m_RequestedDateTime.ToString("yyyyMMddHHmm"));
			obr.AddChildElement("OBR.14").AddChildHl7Element("OBR.14.1", m_SpecimenReceivedDateTime.ToString("yyyyMMddHHmm"));
			obr.AddChildElement("OBR.22").AddChildHl7Element("OBR.22.1", m_ResultsDateTime.ToString("yyyyMMddHHmm"));
			obr.AddChildElement("OBR.25").AddChildHl7Element("OBR.25.1", "F");
			XElement obr32 = obr.AddChildElement("OBR.32");
			obr32.AddChildHl7Element("OBR.32.1", m_PrincipalResultInterpreterID);
			obr32.AddChildHl7Element("OBR.32.2", m_PrincipalResultInterpreterLastName);
			obr32.AddChildHl7Element("OBR.32.3", m_PrincipalResultInterpreterFirstName);
			obr32.AddChildHl7Element("OBR.32.9", m_PrincipalResultInterpreterAssigningAuthority);
			obr32.AddChildHl7Element("OBR.32.13", m_PrincipalResultInterpreterIdentifierTypeCode);
			m_Root.Add(obr);
		}
		/// <summary>method add OBX segments collection for report header to document's root element
		/// </summary>
		/// <param name="reportName">name of report</param>
		private void AddHeaderObxSegments(string reportName)
		{
			AddObxSegment();
			AddObxSegment(m_ReportAuthor);
			AddObxSegment(m_AddressLine);
			AddObxSegment(m_CityStateZip);
			AddObxSegment(reportName);
			AddObxSegment();
			AddObxSegment(string.Format("Patient Name: {0}", m_PageHeaderData.GetPatientDisplayName()));
			AddObxSegment(string.Format("Master Accession: {0}", m_PageHeaderData.GetStringValue(ReportHeaderData.MasterAccessionNo)));
			AddObxSegment(string.Format("Report No: {0}", m_PageHeaderData.GetStringValue(ReportHeaderData.ReportNo)));
			AddObxSegment(string.Format("Client Ref #: {0}", m_PageHeaderData.GetStringValue(ReportHeaderData.ClientRefNumber)));
			AddObxSegment(string.Format("DOB: {0}", m_PageHeaderData.GetDateTime(ReportHeaderData.PatientBirthdate, "dd/MM/yyyy")));
			AddObxSegment(string.Format("Provider: {0}", m_PageHeaderData.GetStringValue(ReportHeaderData.Provider)));
		}
		/// <summary>method add OBX segments collection for multiline text
		/// </summary>
		/// <param name="text">multiline text</param>
		private void AddObxSegments(string text)
		{
			string[] lines = SplitTextBlockToLines(text);
			for (int i = 0; i < lines.GetLength(0); i++)
			{
				AddObxSegment(lines[i]);
			}
		}
		/// <summary>method split multiline text to array of single lines
		/// </summary>
		/// <param name="textBlock">multiline text</param>
		/// <returns>array of single lines</returns>
		private static string[] SplitTextBlockToLines(string textBlock)
		{
			textBlock = textBlock.Replace(Environment.NewLine, "\n");
			textBlock = textBlock.Replace("\r", "\n");
			return textBlock.Split('\n');
		}
		#endregion Private methods

	}
}
