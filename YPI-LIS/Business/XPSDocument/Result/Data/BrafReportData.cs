using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace YellowstonePathology.Document.Result.Data
{
	/// <summary>BRAF V600E Mutation Analysis report data class
	/// </summary>
	public class BrafReportData : YpReportDataBase
	{

		#region Private constants
		/// <summary>Test name text
		/// </summary>
		private const string m_TestName = "BRAF V600E Mutation Analysis:";
		/// <summary>"Method" section's body text
		/// </summary>
		private const string m_MethodData =
			"Following lysis of paraffin embedded tissue; highly purified DNA was extracted from the specimen using an automated method.  " +
			"PCR amplification using fluorescently labeled primers was then performed.  The products of the PCR reaction were then separated by " +
			"high resolution capillary electrophoresis to look for the presence of the 107 nucleotide fragment indicative of a BRAF V600E mutation.";
		#endregion Private constants

		#region Constructors
		/// <summary>constructor with report number and XML data block
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="inDocument">XML element with input data</param>
		public BrafReportData(string reportNo, XElement xmlDataDocument)
			: base(reportNo, xmlDataDocument, "BrafReport")
        {
		}
		#endregion Constructors

		#region Public properties
		/// <summary>property return text for "TestName" label
		/// </summary>
		public new string TestNameText
		{
			get { return this.GetStringValue(TestName, ResultName); }
		}
		/// <summary>property return text for "TestResult" label
		/// </summary>
		public new string TestResultText
		{
			get { return this.GetStringValue(TestResult, ResultName); }
		}
		#endregion Public properties

		#region Protected methods
		/// <summary>method add custom report data
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="xmlDataDocument">XML document with report data</param>
		protected override void AddCustomData(string reportNo, XElement xmlDataDocument)
		{
			AddResultData(reportNo, xmlDataDocument);
			AddResultComment(reportNo, xmlDataDocument);
			AddPathologistSignature(reportNo, xmlDataDocument);
			AddSpecimen(xmlDataDocument);
			AddReportIndication(reportNo, xmlDataDocument);
			AddInterpretation(reportNo, xmlDataDocument);
			AddMethod(m_MethodData);
			AddReferences(reportNo, xmlDataDocument);
		}
		#endregion Protected methods

		#region Private methods
		/// <summary>method add result data colection
		/// </summary>
		/// <param name="xmlDataDocument">XML document with report data</param>
		private void AddResultData(string reportNo, XElement xmlDataDocument)
		{
			IEnumerable<XElement> resultsList = xmlDataDocument.Descendants("PanelOrder").Where(e => e.Element("ReportNo").Value == reportNo);
			XElement result = this.AddChildElement(ResultName);
			if (resultsList.Count() > 0)
			{
				XElement resultSource = resultsList.FirstOrDefault().Element("TestOrderCollection").Elements("TestOrder").FirstOrDefault();
				result.AddChildElement(TestName, m_TestName);
				result.AddChildElement(TestResult, (resultSource != null ? resultSource.GetStringValue("Result") : string.Empty));
			}
			else
			{
				result.AddChildElement(TestName);
				result.AddChildElement(TestResult);
			}
		}
		#endregion Private methods

	}
}
