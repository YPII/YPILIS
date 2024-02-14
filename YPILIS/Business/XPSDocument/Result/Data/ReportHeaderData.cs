using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace YellowstonePathology.Document.Result.Data
{
	/// <summary>block of report header data
	/// </summary>
	public class ReportHeaderData : XElement
	{

		#region Public constants
		/// <summary>Master Accession No XML element name
		/// </summary>
		public static readonly string MasterAccessionNo = "MasterAccessionNo";
		/// <summary>Report No XML element name
		/// </summary>
		public static readonly string ReportNo = "ReportNo";
		/// <summary>Patient Birthdate XML element name
		/// </summary>
		public static readonly string PatientBirthdate = "PBirthdate";
		/// <summary>Provider XML element name
		/// </summary>
		public static readonly string Provider = "PhysicianName";
		/// <summary>Client Ref Number XML element name
		/// </summary>
		public static readonly string ClientRefNumber = "ClientRefNumber";
		#endregion Public constants

		#region Public properties
		/// <summary>if true, then report has one or more amendment
		/// </summary>
		public bool HasAmendments { get; set; } 
		#endregion Public properties

		#region Constructors
		/// <summary>constructor with report number and XML data block
		/// </summary>
		/// <param name="reportNo">report number</param>
		/// <param name="inDocument">XML element with input data</param>
		public ReportHeaderData(string reportNo, XElement inDocument)
			: base("ReportHeader")
        {
			//this.AddFirstDescendantElement(inDocument, ReportNo);
			this.AddChildElement(ReportNo, reportNo);
			this.AddChildElement(inDocument, "PLastName");
			this.AddChildElement(inDocument, "PFirstName");
			this.AddChildElement(inDocument, "PMiddleInitial");
			this.AddChildElement(inDocument, "PSex");
			this.AddChildElement(inDocument, PatientBirthdate);
			this.AddChildElement(inDocument, "PSSN");
			this.AddChildElement(inDocument, "ClientName");
			this.AddChildElement(inDocument, Provider);
			this.AddChildElement(inDocument, MasterAccessionNo);
			this.AddFirstDescendantElement(inDocument, "FinalTime");
			this.AddChildElement(inDocument, "AccessionTime");
			this.AddChildElement(inDocument, "CollectionTime");
			this.AddChildElement(ClientRefNumber, GetClientRefNumber(inDocument));
		}
		#endregion Constructors

		/// <summary>property return report's final time string
		/// </summary>
		public string FinalTimeString
		{
			get { return this.GetDateTime("FinalTime", "MM/dd/yyyy"); }
		}

		#region Public methods
		/// <summary>method return patient code in format "(DOB [BirthDate], [AccessionAge], [Sex])"
		/// </summary>
		/// <returns>patient code </returns>
		public string GetPatientCode()
		{
			return string.Format("(DOB {0}, {1}, {2})", this.GetDateTime(PatientBirthdate, "MM/dd/yyyy"), GetAccessionAge(), this.GetStringValue("PSex"));
		}
		/// <summary>method return full patient name in format "[Last Name], [First Name] [Middle Initial]"
		/// </summary>
		/// <returns>full patient name</returns>
		public string GetPatientDisplayName()
		{
			string patientLName = this.GetStringValue("PLastName");
			string patientFName = this.GetStringValue("PFirstName");
			string patientMiddleInitial = this.GetStringValue("PMiddleInitial");

			StringBuilder result = new StringBuilder();
			if (!string.IsNullOrEmpty(patientMiddleInitial)) result.Append(patientMiddleInitial);
			if (!string.IsNullOrEmpty(patientFName))
			{
				if (result.Length > 0) result.Insert(0, " ");
				result.Insert(0, patientFName);
			}
			if (!string.IsNullOrEmpty(patientLName))
			{
				if (result.Length > 0) result.Insert(0, ", ");
				result.Insert(0, patientLName);
			}
			return result.ToString();
		}
		#endregion Public methods

		#region Private methods
		/// <summary>method return patient age on accession date
		/// </summary>
		/// <returns>patient age on accession date</returns>
		private string GetAccessionAge()
		{
			DateTime? birthdate = this.GetNullableDateValue(PatientBirthdate);
			DateTime? accessionTime = this.GetNullableDateValue("AccessionTime");
			string ageString = string.Empty;
			if (birthdate.HasValue)
			{
				TimeSpan timeSpan = new TimeSpan(accessionTime.Value.Ticks - birthdate.Value.Ticks);
				long days = timeSpan.Days;
				if (days < 14)
				{
					ageString = string.Format("{0}DO", days);
				}
				else if (days < 56)
				{
					long weeks = days / 7;
					ageString = string.Format("{0}WO", weeks);
				}
				else
				{
					DateTime dt = birthdate.Value.AddMonths(24);
					DateTime newdt = accessionTime.Value;
					if (dt.CompareTo(accessionTime.Value) > 0)
					{
						int mos = 0;
						newdt = newdt.AddMonths(-1);
						while (newdt.CompareTo(birthdate.Value) >= 0)
						{
							mos++;
							newdt = newdt.AddMonths(-1);
						}
						ageString = string.Format("{0}MO", mos);
					}
					else
					{
						int years = 0;
						newdt = newdt.AddYears(-1);
						while (newdt.CompareTo(birthdate.Value) >= 0)
						{
							years++;
							newdt = newdt.AddYears(-1);
						}
						ageString = string.Format("{0}YO", years);
					}
				}
			}
			return ageString;
		}
		/// <summary>method return Client Ref number
		/// </summary>
		/// <param name="flowAccessionDocument">XML document with report data</param>
		private static string GetClientRefNumber(XElement flowAccessionDocument)
		{
			string pcan = flowAccessionDocument.GetStringValue("PCAN");
			string svhMedicalRecord = flowAccessionDocument.GetStringValue("SvhMedicalRecord");
			string svhAccount = flowAccessionDocument.GetStringValue("SvhAccount");

			if (!string.IsNullOrEmpty(pcan))
				return pcan;
			else if (!string.IsNullOrEmpty(svhMedicalRecord) && !string.IsNullOrEmpty(svhAccount))
				return string.Format("{0}/{1}", svhAccount, svhMedicalRecord);
			else if (!string.IsNullOrEmpty(svhMedicalRecord))
				return svhMedicalRecord;
			else if (!string.IsNullOrEmpty(svhAccount))
				return svhAccount;
			else
				return string.Empty;
		}
		#endregion Private methods
	}
}
