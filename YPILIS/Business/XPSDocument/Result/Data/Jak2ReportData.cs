using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace YellowstonePathology.Document.Result.Data
{
	/// <summary>JAK2 Mutation V617F report data
	/// </summary>
	public class Jak2ReportData : YpReportDataBase
	{

		#region Private constants
		/// <summary>test name in result section
		/// </summary>
		private const string m_TestName = "JAK2 Analysis:";
		/// <summary>"Method" section's body text
		/// </summary>
		private const string m_MethodData =
			"Highly purified DNA was extracted from the specimen using an automated method. The extracted DNA was amplified using real-time PCR with 2 hydrolysis probes. " +
			"One probe targeted the mutated sequence and one targeted the wild-type sequence of the target in exon 12 of the JAK2 gene. The real-time PCR curves were " +
			"analyzed to determine the presence of the mutated JAK2 gene.";
		#endregion Private constants

		#region Constructors
		/// <summary>constructor with report number and XML data block
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="inDocument">XML element with input data</param>
		public Jak2ReportData(string reportNo, XElement xmlDataDocument)
			: base(reportNo, xmlDataDocument, "JAK2Report")
        {
		}
		#endregion Constructors

		#region Protected methods
		/// <summary>method add custom report data
		/// </summary>
		/// <param name="xmlDataDocument">XML document with report data</param>
		protected override void AddCustomData(string reportNo, XElement xmlDataDocument)
		{
			AddTestName(m_TestName);
			AddTestResult(reportNo, xmlDataDocument);
			AddResultComment(reportNo, xmlDataDocument);
			AddPathologistSignature(reportNo, xmlDataDocument);
			AddSpecimen(xmlDataDocument);
			AddInterpretation(reportNo, xmlDataDocument);
			AddMethod(m_MethodData);
			AddReferences(reportNo, xmlDataDocument);
		}
		#endregion Protected methods

	}
}
