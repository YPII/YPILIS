using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace YellowstonePathology.Document.Result.Data
{
	/// <summary>KRAS Mutation Analysis report data class
	/// </summary>
	public class KrasReportData : YpReportDataBase
	{

		#region Private constants
		/// <summary>Test name text
		/// </summary>
		private const string m_TestName = "KRAS Mutation Analysis:";
		/// <summary>"References" section's body text
		/// </summary>
		private const string m_ReferencesData = 
			"1.   Amado RG, Wolf M, Peeters M, et al. Wild-type KRAS is required for panitumumab efficacy in patients with " +
			"metastatic colorectal cancer. J Clin Oncol. 2008; 26(10): 1626-1634.\n" +
			"2.   Bokemeyer C, Bondarenko I, Makhson A, et al. Fluorouracil, leucovorin, and oxiplatin with and without " +
			"cituximab in the first-line treatment of metastatic colorectal cancer.    J Clin Oncol. 2009; 27(5): 663-671.\n" +
			"3.   Van Cutsem E, Kohne CH, Hitre E, et al. Cituximab and chemotherapy as initial treatment for metastatic " +
			"colorectal cancer. N Engl J Med. 2009; 360(14): 1408-1417.\n" +
			"4.   Allegra CJ, Jessup JM, Somerfield MR, et al. American Society of Clinical Oncology provisional clinical " +
			"opinion: testing for KRAS gene mutations in patients with metastatic colorectal carcinoma to predict response " +
			"to anti-epidermal growth factor receptor monoclonal antibody therapy. J Clin Oncol. 2009; 27(12): 2091-2096.";
		#endregion Private constants

		#region Constructors
		/// <summary>constructor with report number and XML data block
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="inDocument">XML element with input data</param>
		public KrasReportData(string reportNo, XElement xmlDataDocument)
			: base(reportNo, xmlDataDocument, "KrasReport")
        {
		}
		#endregion Constructors

		#region Public properties
		/// <summary>property return text for "TestName" label
		/// </summary>
		public new string TestNameText
		{
			get { return this.GetStringValue(TestName); }
		}
		/// <summary>property return text for "TestResult" label
		/// </summary>
		public new string TestResultText
		{
			get { return this.GetStringValue(TestResult); }
		}
		#endregion Public properties

		#region Protected methods
		/// <summary>method add custom report data
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="xmlDataDocument">XML document with report data</param>
		protected override void AddCustomData(string reportNo, XElement xmlDataDocument)
		{
			AddTestName(m_TestName);
			AddTestResult(reportNo, xmlDataDocument);
			AddResultComment(reportNo, xmlDataDocument, "KRAS Result Detail");
			AddPathologistSignature(reportNo, xmlDataDocument);
			AddSpecimen(xmlDataDocument);
			AddReportIndication(reportNo, xmlDataDocument);
			AddInterpretation(reportNo, xmlDataDocument);
			AddMethod(reportNo, xmlDataDocument);
			AddReferences(m_ReferencesData);
		}
		#endregion Protected methods
	}
}
