using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace YellowstonePathology.Document.Result.Data
{
	/// <summary>Chlamydia Gonorrhea Screening report data class
	/// </summary>
	public class NgctReportData : YpReportDataBase
	{

		#region Private constants
		/// <summary>"Method" section's body text
		/// </summary>
		private const string m_MethodData = "DNA was extracted from the patient's specimen using an automated method. Real time PCR amplification was performed for organism detection and identification.";
		#endregion Private constants

		#region Constructors
		/// <summary>constructor with report number and XML data block
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="inDocument">XML element with input data</param>
		public NgctReportData(string reportNo, XElement xmlDataDocument)
			: base(reportNo, xmlDataDocument, "NgctReport")
        {
		}
		#endregion Constructors

		#region Protected methods
		/// <summary>method add custom report data
		/// </summary>
		/// <param name="xmlDataDocument">XML document with report data</param>
		protected override void AddCustomData(string reportNo, XElement xmlDataDocument)
		{
			AddResultComment(reportNo, xmlDataDocument);
			AddSpecimen(xmlDataDocument);
			AddMethod(m_MethodData);
		}
		#endregion Protected methods
	}
}
