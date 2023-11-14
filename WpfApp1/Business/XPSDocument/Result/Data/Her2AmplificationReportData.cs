using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace YellowstonePathology.Document.Result.Data
{
	/// <summary>HER2 Amplification report data class
	/// </summary>
	public class Her2AmplificationReportData : YpReportDataBase
	{

		#region Private constants

		#region Temp constants (for not found data)
		/// <summary>"Interpretation" section text
		/// </summary>
		private const string m_Interpretation =
			"Human epidermal growth factor receptor 2 gene (HER2) is amplified in approximately 20% of breast cancers.  HER2 positive breast tumors are associated with " +
			"a worse prognosis.  The HER2 status is also predictive of response to chemotherapeutic agents.  Dual ISH studies for HER2 were performed on the submitted " +
			"sample, in accordance with ASCO/CAP guidelines.\n" +
			"Ratios on parallel control slides were within normal limits. For this patient, the HER2:Chr17 ratio was 8.56.  A total of 20 nuclei were examined.  " +
			"Therefore HER2 status is positive (amplified).";
		/// <summary>Specimen fixation time string
		/// </summary>
		private const string m_SpecimenFixationTime = "Unknown";
		/// <summary>Specimen fixation duration string
		/// </summary>
		private const string m_SpecimenFixationDuration = "Unknown";
		/// <summary>Specimen sample adequacy string
		/// </summary>
		private const string m_SpecimenSampleAdequacy = "Limited by low number of tumor cells present";
		/// <summary>Invasive tumor cells count
		/// </summary>
		private const int m_InvasiveTumorСells = 20;
		/// <summary>Number of observers
		/// </summary>
		private const int m_ObserversNumber = 2;
		/// <summary>HER2 average copy number per nucleus
		/// </summary>
		private const double m_Her2CopyNumber = 10.7;
		/// <summary>Chr17 average copy number per nucleus
		/// </summary>
		private const double m_Chr17CopyNumber = 1.25;
		/// <summary>Average HER2/Chr17 signals ratio
		/// </summary>
		private const double m_Her2Chr17Ratio = m_Her2CopyNumber / m_Chr17CopyNumber;
		#endregion Temp constants (for not found data)

		/// <summary>name of XML element with specimen information
		/// </summary>
		private static readonly string SpecimenInformation = "SpecimenInformation";
		/// <summary>name of XML element with specimen site and type
		/// </summary>
		private static readonly string SpecimenType = "SpecimenType";
		/// <summary>name of XML element with specimen fixation type
		/// </summary>
		private static readonly string SpecimenFixation = "SpecimenFixation";
		/// <summary>name of XML element with specimen time to fixation
		/// </summary>
		private static readonly string SpecimenFixationTime = "TimeToFixation";
		/// <summary>name of XML element with specimen duration of fixation
		/// </summary>
		private static readonly string SpecimenFixationDuration = "DurationOfFixation";
		/// <summary>name of XML element with specimen sample adequacy
		/// </summary>
		private static readonly string SpecimenSampleAdequacy = "SampleAdequacy";
		/// <summary>name of XML element with result data collections
		/// </summary>
		private static readonly string ResultDataCollectionName = "ResultData";
		/// <summary>name of XML element with number of invasive tumor cells counted
		/// </summary>
		private static readonly string ResultDataInvasiveTumorСells = "InvasiveTumorСells";
		/// <summary>name of XML element with number of observers
		/// </summary>
		private static readonly string ObserversNumber = "ObserversNumber";
		/// <summary>name of XML element with HER2 average copy number per nucleus
		/// </summary>
		private static readonly string Her2CopyNumber = "Her2CopyNumber";
		/// <summary>name of XML element with Chr17 average copy number per nucleus
		/// </summary>
		private static readonly string Chr17CopyNumber = "Chr17CopyNumber";
		/// <summary>name of XML element with ratio of average HER2/Chr17 signals
		/// </summary>
		private static readonly string Her2Chr17Ratio = "Her2Chr17Ratio";

		/// <summary>value nodes names of result data collections
		/// </summary>
		private static readonly string[] resultValueNodeNames = new string[] { ResultDataInvasiveTumorСells, ObserversNumber, Her2CopyNumber, Chr17CopyNumber, Her2Chr17Ratio };
		/// <summary>value nodes names of specimen data collections
		/// </summary>
		string[] specimenValueNodeNames = new string[] { SpecimenType, SpecimenFixation, SpecimenFixationTime, SpecimenFixationDuration, SpecimenSampleAdequacy };

		/// <summary>"Method" section's body text
		/// </summary>
		private const string m_MethodData =
			"This test was performed using a molecular method, In Situ Hybridization (ISH) with the US FDA approved Inform HER2 DNA probe kit, modified " +
			"to report results according to ASCO/CAP guidelines. The test was performed on paraffin embedded tissue in compliance with ASCO/CAP guidelines.  " +
			"Probes used include a locus specific probe for HER2 and an internal hybridization control probe for the centromeric region of chromosome 17 (Chr17).";
		#endregion Private constants

		#region Constructors
		/// <summary>constructor with report number and XML data block
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="inDocument">XML element with input data</param>
		public Her2AmplificationReportData(string reportNo, XElement xmlDataDocument)
			: base(reportNo, xmlDataDocument, "Her2AmplificationReport")
        {
		}
		#endregion Constructors

		#region Public properties
		/// <summary>property return result string of HER2 Amplification test
		/// </summary>
		public new string TestResultText
		{
			get
			{
				string result;
				decimal ratio = this.GetDecimalValue(Her2Chr17Ratio, ResultDataCollectionName);
				if (ratio < (decimal)1.8)
					result = "NEGATIVE";
				else if (ratio >= (decimal)1.8 && ratio <= (decimal)2.2)
					result = "EQUIVOCAL";
				else
					result = "POSITIVE";
				return string.Format("{0} (Amplified)", result);
			}
		}
		/// <summary>property return Her2Chr17Ratio label text
		/// </summary>
		public string TestResultValue
		{
			get { return string.Format("Ratio = {0}", this.GetStringValue(Her2Chr17Ratio, ResultDataCollectionName)); }
		}
		#endregion Public properties

		#region Public methods
		/// <summary>method return row value of "Result Data" section table
		/// </summary>
		/// <param name="fieldIndex">row index</param>
		public string GetResultValue(int fieldIndex)
		{
			return this.GetStringValue(resultValueNodeNames[fieldIndex], ResultDataCollectionName);
		}
		/// <summary>method return row value of "Specimen Information" section table
		/// </summary>
		/// <param name="fieldIndex">row index</param>
		public string GetSpecimenValue(int fieldIndex)
		{
			return this.GetStringValue(specimenValueNodeNames[fieldIndex], SpecimenInformation);
		}
		#endregion Public methods

		#region Protected methods
		/// <summary>method add custom report data
		/// </summary>
		/// <param name="xmlDataDocument">XML document with report data</param>
		protected override void AddCustomData(string reportNo, XElement xmlDataDocument)
		{
			AddResultData(xmlDataDocument);
			AddSpecimenInformationData(xmlDataDocument);
			AddPathologistSignature(reportNo, xmlDataDocument);
			AddInterpretation(m_Interpretation);
			AddMethod(m_MethodData);
			AddReferences(reportNo, xmlDataDocument);
		}
		#endregion Protected methods

		#region Private methods
		/// <summary>method add result data colection
		/// </summary>
		/// <param name="xmlDataDocument">XML document with report data</param>
		private void AddResultData(XElement xmlDataDocument)
		{
			XElement destRoot = this.AddChildElement(ResultDataCollectionName);
			destRoot.AddChildElement(ResultDataInvasiveTumorСells, m_InvasiveTumorСells.ToString(m_Nfi));
			destRoot.AddChildElement(ObserversNumber, m_ObserversNumber.ToString(m_Nfi));
			destRoot.AddChildElement(Her2CopyNumber, m_Her2CopyNumber.ToString(m_Nfi));
			destRoot.AddChildElement(Chr17CopyNumber, m_Chr17CopyNumber.ToString(m_Nfi));
			destRoot.AddChildElement(Her2Chr17Ratio, m_Her2Chr17Ratio.ToString(m_Nfi));
		}
		/// <summary>method add specimen information data colection
		/// </summary>
		/// <param name="xmlDataDocument">XML document with report data</param>
		private void AddSpecimenInformationData(XElement xmlDataDocument)
		{
			XElement destRoot = this.AddChildElement(SpecimenInformation);
			destRoot.AddChildElement(xmlDataDocument, "SpecimenOrderCollection", "SpecimenOrder", "Description", SpecimenType);
			destRoot.AddChildElement(xmlDataDocument, "SpecimenOrderCollection", "SpecimenOrder", "ClientFixation", SpecimenFixation);
			destRoot.AddChildElement(SpecimenFixationTime, m_SpecimenFixationTime);
			destRoot.AddChildElement(SpecimenFixationDuration, m_SpecimenFixationDuration);
			destRoot.AddChildElement(SpecimenSampleAdequacy, m_SpecimenSampleAdequacy);
		}
		#endregion Private methods

	}
}
