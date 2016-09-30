using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace YellowstonePathology.Document.Result.Data
{
	/// <summary>HPV-16 Testing report data class
	/// </summary>
	public class Hpv16ReportData : YpReportDataBase
	{

		#region Private constants
		/// <summary>"Method" section's body text
		/// </summary>
		private const string m_MethodData = "Paraffin embedded tumor tissue was subjected to lysis.  DNA was then extracted with an automated method.  Real-time PCR amplification was performed using primers and a hydrolysis probe specific for the E7 gene of HPV-16.  The beta-actin gene was used as an internal control.  The real-time PCR curves were analyzed to determine the presence of HPV-16 DNA in the specimen.";
		/// <summary>"References" section's body text
		/// </summary>
		private const string m_ReferencesData = 
			"1) Mendenhall WM, Logan HL.  Human Papillomavirus and Head and Neck Cancer.  Am J Clin Oncol. 2009 Jul 31.\n" +
			"2) Termine N, Panzarella V, Falaschini S et al. HPV in oral squamous cell carcinoma vs head and neck squamous cell carcinoma biopsies: a meta-analysis (1988-2007). Ann Oncol 2008 October;19(10):1681-90.\n" +
			"3) Sugiyama M, et al.  Human papillomavirus-16 in oral squamous cell carcinoma: Clinical correlates and 5-year survival.  Br J Oral Maxillofac Surg. 2007 Mar;45(2):116-22\n" +
			"4) Fakhry C, Gillison ML.  Clinical implications of human papillomavirus in head and neck cancers.  J Clin Oncol. 2006 Jun 10;24 (17):2606-11.";
		#endregion Private constants

		#region Constructors
		/// <summary>constructor with report number and XML data block
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="inDocument">XML element with input data</param>
		public Hpv16ReportData(string reportNo, XElement xmlDataDocument)
			: base(reportNo, xmlDataDocument, "HVP16Report")
        {
		}
		#endregion Constructors

		#region Public properties
		#endregion Public properties

		#region Protected methods
		/// <summary>method add custom report data
		/// </summary>
		/// <param name="xmlDataDocument">XML document with report data</param>
		protected override void AddCustomData(string reportNo, XElement xmlDataDocument)
		{
			AddPathologistSignature(reportNo, xmlDataDocument);
			AddSpecimen(xmlDataDocument);
			AddInterpretation(reportNo, xmlDataDocument, "Report Interpretation");
			AddMethod(m_MethodData);
			AddReferences(m_ReferencesData);
		}
		#endregion Protected methods
	}
}
