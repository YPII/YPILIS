using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace YellowstonePathology.Document.Result.Data
{
	/// <summary>High Risk HPV Testing report data class
	/// </summary>
	public class HpvTwiReportData : YpReportDataBase
	{

		#region Private constants
		/// <summary>"Test Reference" column's text
		/// </summary>
		private const string m_TestReferenceData = "Negative";
		/// <summary>"Test Information" section's body text
		/// </summary>
		private const string m_TestInformationData =
			"Testing for high-risk HPV was performed using the Invader® technology from Hologic after automated DNA extraction from the ThinPrep sample for cytology case # P12-3929. " +
			"The Invader® chemistry is a proprietary signal amplification method capable of detecting low levels of target DNA. Using analyte specific reagents, the assay is capable of " +
			"detecting genotypes 16, 18, 31, 33, 35, 39, 45, 51, 52, 56, 58, 59, 66 and 68. The assay also evaluates specimen adequacy by measuring the amount of normal human DNA present " +
			"in the sample.\n\n" +
			"HPV types 16 & 18 are frequently associated with high risk for development of high grade cervical dysplasia and cervical carcinoma. HPV types 31/33/35/39/45/51/52/56/58/59/68 " +
			"have also been classified as high-risk for the development of high grade cervical dysplasia and cervical carcinoma. HPV type 66 has been classified as probable high-risk.\n\n" +
			"A negative test result does not necessarily imply the absence of HPV infection as this assay targets only the most common high-risk genotypes and insufficient cervical sampling " +
			"can affect results. These results should be correlated with Pap smear and clinical exam results.";
		#endregion Private constants

		#region Constructors
		/// <summary>constructor with report number and XML data block
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="inDocument">XML element with input data</param>
		public HpvTwiReportData(string reportNo, XElement xmlDataDocument)
			: base(reportNo, xmlDataDocument, "HpvTwiReport")
        {
		}
		#endregion Constructors

		#region Public properties
		/// <summary>property return body text for "TestInformation" section
		/// </summary>
		public string TestInformationText
		{
			get { return this.GetStringValue(HpvTwiReportData.TestInformation); }
		}
		#endregion Public properties

		#region Protected methods
		/// <summary>method add custom report data
		/// </summary>
		/// <param name="xmlDataDocument">XML document with report data</param>
		protected override void AddCustomData(string reportNo, XElement xmlDataDocument)
		{
			AddSpecimen(xmlDataDocument);
			AddTestInformation(m_TestInformationData);
		}
		#endregion Protected methods
	}
}
