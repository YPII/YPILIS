using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Globalization;

namespace YellowstonePathology.Document.Result.Data
{
	/// <summary>base class for YPI report data
	/// </summary>
	public abstract class YpReportDataBase : XElement
	{

		#region Protected constants
		/// <summary>numeric format (used for normal read/write string representation of numerics in non-US cultures)
		/// </summary>
		protected readonly static NumberFormatInfo m_Nfi = new CultureInfo("en-US").NumberFormat;
		/// <summary>Report Distribution сollection XML element names array
		/// </summary>
		protected static readonly string[] ReportDistributionCollectionNames = new string[] { "ReportDistributionCollection", "ReportDistributionItemCollection" };
		/// <summary>Report Distribution сollection item XML element names array
		/// </summary>
		protected static readonly string[] ReportDistributionNames = new string[] { "ReportDistribution", "ReportDistributionItemV2" };

		/// <summary>name of XML element with results collection
		/// </summary>
		protected static readonly string ResultsCollectionName = "Results";
		/// <summary>name of XML element with single result
		/// </summary>
		protected static readonly string ResultName = "Result";

        /// <summary>name of XML element with test number
        /// </summary>
        protected static readonly string TestNumber = "TestNumber";
        /// <summary>name of XML element with test name
		/// </summary>
		protected static readonly string TestName = "TestName";
		/// <summary>name of XML element with test result
		/// </summary>
		protected static readonly string TestResult = "TestResult";
		/// <summary>name of XML element with result comment
		/// </summary>
		protected static readonly string ResultComment = "ResultComment";
		/// <summary>name of XML element with test reference
		/// </summary>
		protected static readonly string TestReference = "TestReference";
		/// <summary>name of XML element with test information
		/// </summary>
		protected static readonly string TestInformation = "TestInformation";

		/// <summary>name of XML element with pathologist signature
		/// </summary>
		protected static readonly string PathologistSignature = "PathologistSignature";

		/// <summary>Amendments сollection XML element name
		/// </summary>
		protected static readonly string AmendmentCollectionName = "AmendmentItemCollection";
		/// <summary>Amendments сollection's item XML element name
		/// </summary>
		protected static readonly string AmendmentItemName = "AmendmentItem";

		/// <summary>name of XML element with specimen description
		/// </summary>
		protected static readonly string Specimen = "Specimen";
		/// <summary>name of XML element with report indication
		/// </summary>
		protected static readonly string ReportIndication = "ReportIndication";
		/// <summary>name of XML element with interpretation
		/// </summary>
		protected static readonly string Interpretation = "Interpretation";
		/// <summary>name of XML element with method
		/// </summary>
		protected static readonly string Method = "Method";
		/// <summary>name of XML element with references
		/// </summary>
		protected static readonly string References = "References";
		/// <summary>Other Yellowstone Pathology Institute Reports XML element name
		/// </summary>
		protected static readonly string OtherReports = "OtherReports";

		#endregion Private constants

		#region Public constants
		/// <summary>name of XML element with revised diagnosis flag
		/// </summary>
		public static readonly string RevisedDiagnosis = "RevisedDiagnosis";
		//RevisedDiagnosis
		/// <summary>name of XML element with amendment type
		/// </summary>
		public static readonly string AmendmentType = "AmendmentType";
		/// <summary>name of XML element with amendment time
		/// </summary>
		public static readonly string AmendmentTime = "AmendmentTime";
		/// <summary>name of XML element with amendment text
		/// </summary>
		public static readonly string AmendmentText = "Amendment";
		/// <summary>name of XML element with amendment text
		/// </summary>
		public static readonly string AmendmentPathologistSignature = "PathologistSignature";

		/// <summary>Disclaimer XML element name
		/// </summary>
		public static readonly string[] DisclaimerText = new string[]
		{
			"Tests utilizing Analytic Specific Reagents (ASR's) were developed and performance characteristics determined by " +
			"Yellowstone Pathology Institute, Inc. They have not been cleared or approved by the U.S. Food and Drug Administration. " +
			"The FDA has determined that such clearance or approval is not necessary. ASR's may be used for clinical purposes and " +
			"should not be regarded as investigational or for research. This laboratory is certified under the Clinical Laboratory " +
			"Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.",
			
			"This test was performed using a US FDA approved DNA probe kit, modified to report results according to ASCO/CAP " +
			"guidelines, and the modified procedure was validated by Yellowstone Pathology Institute (YPI).  YPI assumes the " +
			"responsibility for test performance.",

			"This test was developed and its performance characteristics determined by Yellowstone Pathology Institute, Inc.  " +
			"It has not been cleared or approved by the U.S. Food and Drug Administration. The FDA has determined that such clearance " +
			"or approval is not necessary.  This test is used for clinical purposes.  It should not be regarded as investigational " +
			"or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) " +
			"as qualified to perform high complexity clinical laboratory testing."
		};
		#endregion Public constants

		#region Constructors
		/// <summary>constructor with report number and XML data block
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="inDocument">XML element with input data</param>
		public YpReportDataBase(string reportNo, XElement xmlDataDocument, string rootName)
			: base(rootName)
        {
			Add(PageHeader = new ReportHeaderData(reportNo, xmlDataDocument));
			AddAmendmentsData(xmlDataDocument);
			AddCustomData(reportNo, xmlDataDocument);
			AddOtherReportsData(xmlDataDocument);
			AddReportDistributionsData(xmlDataDocument);
		}
		#endregion Constructors

		#region Public properties
		/// <summary>page header XML data block
		/// </summary>
		public ReportHeaderData PageHeader { get; private set; }
		/// <summary>property return text for "PathologistSignature" label
		/// </summary>
		public string PathologistSignatureText
		{
			get { return this.Element(PathologistSignature).Value; }
		}
		/// <summary>property return rows count for table in results table
		/// </summary>
		public int ResultRowsCount
		{
			get { return Element(ResultsCollectionName).Elements(ResultName).Count(); }
		}
		/// <summary>property return text for "TestName" label
		/// </summary>
		public string TestNameText
		{
			get { return this.GetStringValue(TestName); }
		}
		/// <summary>property return text for "TestResult" label
		/// </summary>
		public string TestResultText
		{
			get { return this.GetStringValue(TestResult); }
		}
		/// <summary>property return text for "ResultComment" label
		/// </summary>
		public string ResultCommentText
		{
			get { return this.GetStringValue(ResultComment); }
		}
		/// <summary>property return xml element with amendments
		/// </summary>
		public IEnumerable<XElement> Amendments
		{
			get { return Element(YpReportDataBase.AmendmentCollectionName).Elements(YpReportDataBase.AmendmentItemName); }
		}
		/// <summary>property return body text for "Specimen" section
		/// </summary>
		public string SpecimenText
		{
			get { return this.GetStringValue(Specimen); }
		}
		/// <summary>property return body text for "Interpretation" section
		/// </summary>
		public string InterpretationText
		{
			get { return this.GetStringValue(Interpretation); }
		}
		/// <summary>property return body text for "Method" section
		/// </summary>
		public string MethodText
		{
			get { return this.GetStringValue(YpReportDataBase.Method); }
		}
		/// <summary>property return body text for "References" section
		/// </summary>
		public string ReferencesText
		{
			get { return this.GetStringValue(YpReportDataBase.References); }
		}
		/// <summary>property return body text for "ReportIndication" section
		/// </summary>
		public string ReportIndicationText
		{
			get { { return this.GetStringValue(YpReportDataBase.ReportIndication); } }
		}
		/// <summary>property return body text of "OtherReports" section
		/// </summary>
		public string OtherReportsText
		{
			get { return this.GetStringValue(YpReportDataBase.OtherReports); }
		}
		/// <summary>property return list of "Report Distribution" xml elements
		/// </summary>
		public IEnumerable<XElement> ReportDistributionList
		{
			get { return Element("ReportDistributionCollection").Elements("ReportDistribution"); }
		}
		#endregion Public properties

		#region Public methods
		/// <summary>method return cell value of results table
		/// </summary>
		/// <param name="rowIndex">row index</param>
		/// <param name="columnIndex">column index</param>
		/// <returns>cell value of results table</returns>
		public string GetResultCellValue(int rowIndex, int columnIndex)
		{
			string[] ResultCellValueNames = new string[] { TestName, TestResult, TestReference };
			IEnumerable<XElement> results = Element(ResultsCollectionName).Elements(ResultName);
			XElement result = results.ElementAt(rowIndex);
			return result.GetStringValue(ResultCellValueNames[columnIndex]);
		}
		#endregion Public methods

		#region Protected methods
		/// <summary>method write report's custom data blocks
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="xmlDataDocument">XML document with report data</param>
		protected abstract void AddCustomData(string reportNo, XElement xmlDataDocument);
		/// <summary>method write report's pathologist signature data
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="xmlDataDocument">XML document with report data</param>
		protected void AddPathologistSignature(string reportNo, XElement xmlDataDocument)
		{
			this.AddChildElement(xmlDataDocument, "PanelSetOrderItemCollection", "PanelSetOrderItem", "Signature", PathologistSignature, new string[] { "ReportNo" }, new string[] { reportNo });
		}
		/// <summary>method write report's references data
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="xmlDataDocument">XML document with report data</param>
		protected void AddReferences(string reportNo, XElement xmlDataDocument)
		{
			this.AddChildElement(xmlDataDocument, "PanelSetOrderCommentItemCollection", "PanelSetOrderCommentItem", "Comment", References, new string[] { "ReportNo", "CommentName" }, new string[] { reportNo, "Report Reference" });
		}
		/// <summary>method write report's references data
		/// </summary>
		/// <param name="references">temp constant data</param>
		protected void AddReferences(string references)
		{
			this.AddChildElement(References, references);
		}
		/// <summary>method write report's specimen data
		/// </summary>
		/// <param name="xmlDataDocument">XML document with report data</param>
		protected void AddSpecimen(XElement xmlDataDocument)
		{
			this.AddChildElement(xmlDataDocument, "SpecimenOrderCollection", "SpecimenOrder", "Description", Specimen);
		}
		/// <summary>method write report's interpretation data
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="xmlDataDocument">XML document with report data</param>
		/// <param name="nodeName">optional node name, default is "Interpretation"</param>
		protected void AddInterpretation(string reportNo, XElement xmlDataDocument, string nodeName = "Interpretation")
		{
			this.AddChildElement(xmlDataDocument, "PanelSetOrderCommentItemCollection", "PanelSetOrderCommentItem", "Comment", Interpretation, new string[] { "ReportNo", "CommentName" }, new string[] { reportNo, nodeName });	//"Interpretation"
		}
		/// <summary>method write report's interpretation data
		/// </summary>
		/// <param name="interpretation">temp constant data</param>
		protected void AddInterpretation(string interpretation)
		{
			this.AddChildElement(Interpretation, interpretation);
		}
		/// <summary>method write report's report indication data
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="xmlDataDocument">XML document with report data</param>
		protected void AddReportIndication(string reportNo, XElement xmlDataDocument)
		{
			this.AddChildElement(xmlDataDocument, "PanelSetOrderCommentItemCollection", "PanelSetOrderCommentItem", "Comment", ReportIndication, new string[] { "ReportNo", "CommentName" }, new string[] { reportNo, "Report Indication" });
		}
		/// <summary>method write report's method data
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="xmlDataDocument">XML document with report data</param>
		protected void AddMethod(string reportNo, XElement xmlDataDocument)
		{
			this.AddChildElement(xmlDataDocument, "PanelSetOrderCommentItemCollection", "PanelSetOrderCommentItem", "Comment", Method, new string[] { "ReportNo", "CommentName" }, new string[] { reportNo, "Report Method" });
		}
		/// <summary>method write report's method data
		/// </summary>
		/// <param name="methodData">temp constant data</param>
		protected void AddMethod(string methodData)
		{
			this.AddChildElement(Method, methodData);
		}
		/// <summary>method write report's result comment data
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="xmlDataDocument">XML document with report data</param>
		/// <param name="nodeName">optional node name, default is "Result Comment"</param>
		protected void AddResultComment(string reportNo, XElement xmlDataDocument, string nodeName = "Result Comment")
		{
			this.AddChildElement(xmlDataDocument, "PanelSetOrderCommentItemCollection", "PanelSetOrderCommentItem", "Comment", ResultComment, new string[] { "ReportNo", "CommentName" }, new string[] { reportNo, nodeName });
		}
		/// <summary>method write report's test result data
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="xmlDataDocument">XML document with report data</param>
		protected void AddTestResult(string reportNo, XElement xmlDataDocument)
		{
		}
		/// <summary>method write report's test name data
		/// </summary>
		/// <param name="interpretation">temp constant data</param>
		protected void AddTestName(string testName)
		{
			this.AddChildElement(TestName, testName);
		}
		/// <summary>method write report's test information data
		/// </summary>
		/// <param name="testInfo">temp constant data</param>
		protected void AddTestInformation(string testInfo)
		{
			this.AddChildElement(TestInformation, testInfo);
		}
		#endregion Protected methods

		#region Private methods
		/// <summary>method add "Amendments" data block
		/// </summary>
		/// <param name="xmlDataDocument">XML document with report data</param>
		private void AddAmendmentsData(XElement xmlDataDocument)
		{
			XElement destAmendments = this.AddChildElement(AmendmentCollectionName);
			IEnumerable<XElement> srcAmendments = xmlDataDocument.Descendants(AmendmentItemName);
			PageHeader.HasAmendments = (srcAmendments.Count() > 0);
			for (int i = 0; i < srcAmendments.Count(); i++)
			{
				XElement srcAmendment = srcAmendments.ElementAt(i);
				XElement destAmendment = destAmendments.AddChildElement(AmendmentItemName);
				destAmendment.AddChildElement(RevisedDiagnosis, (srcAmendment.Element(RevisedDiagnosis) == null ? "false" : (srcAmendment.Element(RevisedDiagnosis).Value == "1" ? "true" : "false")));
				destAmendment.AddChildElement(AmendmentType, srcAmendment.Element(AmendmentType).Value);
				destAmendment.AddChildElement(AmendmentTime, srcAmendment.Element(AmendmentTime).Value);
				destAmendment.AddChildElement(AmendmentText, srcAmendment.Element(AmendmentText).Value);
				destAmendment.AddChildElement(AmendmentPathologistSignature, srcAmendment.Element(AmendmentPathologistSignature).Value);
			}
		}
		/// <summary>method add "Other Yellowstone Pathology Reports" data block
		/// </summary>
		/// <param name="xmlDataDocument">XML document with report data</param>
		private void AddOtherReportsData(XElement xmlDataDocument)
		{
			StringBuilder reports = new StringBuilder();
			XElement caseHistory = xmlDataDocument.Element("CaseHistory");
			if (caseHistory != null)
			{
				IEnumerable<XElement> reportLst = caseHistory.Elements("Report");
				for (int i = 0; i < reportLst.Count(); i++)
				{
					if (i > 0) reports.Append(", ");
					reports.Append(reportLst.ElementAt(i).Attribute("ReportNo").Value);
				}
			}
			if (reports.Length == 0) reports.Append("None");
			this.AddChildElement(OtherReports, reports.ToString());
		}
		/// <summary>method add "Report Distributions" data block
		/// </summary>
		/// <param name="xmlDataDocument">XML document with report data</param>
		private void AddReportDistributionsData(XElement xmlDataDocument)
		{
			int i, j, k;
			StringBuilder text;
			XElement dest, source = null;

			string[] colValues = new string[] { "ClientName", "PhysicianName" };

			for (k = 0; k < ReportDistributionCollectionNames.Length; k++)
			{
				source = xmlDataDocument.Element(ReportDistributionCollectionNames[k]);
				if (source != null) break;
			}
			dest = this.AddChildElement(ReportDistributionCollectionNames[0]);
			if (source != null)
			{
				IEnumerable<XElement> reportDistributions = source.Elements(ReportDistributionNames[k]);
				for (i = 0; i < reportDistributions.Count(); i++)
				{
					XElement reportDistribution = reportDistributions.ElementAt(i);
					text = new StringBuilder();
					for (j = 0; j < colValues.Length; j++)
					{
						if (j > 0) text.Append(" - ");
						text.Append(reportDistribution.Element(colValues[j]).Value);
					}
					dest.AddChildElement(ReportDistributionNames[0], text.ToString());
				}
			}
		}
		#endregion Private methods

	}
}
