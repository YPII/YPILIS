using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Document.Result.Data
{
	public class ClientOrderDataSheetData : XElement
	{
		public ClientOrderDataSheetData(XElement clientOrderElement)
			: base("ClientOrderData")
		{
			string patientDisplayName = clientOrderElement.Element("PLastName").Value + ", " + clientOrderElement.Element("PFirstName").Value;
			string patientBirthdate = clientOrderElement.Element("PBirthdate").Value;
			DateTime aDate = DateTime.Parse(clientOrderElement.Element("OrderDate").Value);
			DateTime date;
			DateTime? bDate = null;
			if (DateTime.TryParse(patientBirthdate, out date) == true)
			{
				bDate = date;
			}

			if (bDate.HasValue == true)
			{
				string pAge = this.GetOrderAge(bDate, aDate);
				patientDisplayName += "(" + bDate.Value.ToShortDateString() + ", " + pAge + ", " + clientOrderElement.Element("PSex").Value + ")";
			}

			XElement patientNameElement = new XElement("PatientDisplayName", patientDisplayName);
			this.Add(patientNameElement);

			XElement ssnElement = new XElement("SSN", clientOrderElement.Element("PSSN").Value);
			this.Add(ssnElement);

			string clientClinicalHistory = clientOrderElement.Element("ClinicalHistory").Value;
			XElement clinicalHistoryElement = new XElement("ClinicalHistory", clientClinicalHistory);
			this.Add(clinicalHistoryElement);

            string clientPreOpDiagnosis = string.Empty;
            if (clientOrderElement.Element("PreOpDiagnosis") != null) clientPreOpDiagnosis = clientOrderElement.Element("PreOpDiagnosis").Value;
            
			XElement preOpDiagnosisElement = new XElement("PreOpDiagnosis", clientPreOpDiagnosis);
			this.Add(preOpDiagnosisElement);

			string clientOrderName = clientOrderElement.Element("ClientName").Value;
			XElement clientOrderNameElement = new XElement("ClientName", clientOrderName);
			this.Add(clientOrderNameElement);

			string physicianName = clientOrderElement.Element("ProviderName").Value;
			XElement physicianNameElement = new XElement("PhysicianName", physicianName);
			this.Add(physicianNameElement);

			string orderedBy = clientOrderElement.Element("OrderedBy").Value;
			XElement orderedByElement = new XElement("OrderedBy", orderedBy);
			this.Add(orderedByElement);

			string clientOrderDate = clientOrderElement.Element("OrderDate").Value;
			DateTime coDate;
			if (DateTime.TryParse(clientOrderDate, out coDate) == true)
			{
				clientOrderDate = coDate.ToShortDateString();
			}
			XElement clientOrderDateElement = new XElement("OrderDate", clientOrderDate);
			this.Add(clientOrderDateElement);

			string submitted = clientOrderElement.Element("Submitted").Value;
			XElement submittedElement = new XElement("Submitted", submitted);
			this.Add(submittedElement);

			string accessioned = clientOrderElement.Element("Accessioned").Value;
			XElement accessionedElement = new XElement("Accessioned", accessioned);
			this.Add(accessionedElement);

			XElement medRecordElement = new XElement("SvhMedicalRecord", clientOrderElement.Element("SvhMedicalRecord").Value);
			this.Add(medRecordElement);

			XElement accountNoElement = new XElement("SvhAccount", clientOrderElement.Element("SvhAccountNo").Value);
			this.Add(accountNoElement);

			string initiatingSystem = clientOrderElement.Element("SystemInitiatingOrder").Value;
			XElement initiatingSystemElement = new XElement("SystemInitiatingOrder", initiatingSystem);
			this.Add(initiatingSystemElement);

			string cospecialInstructions = clientOrderElement.Element("SpecialInstructions").Value;
			XElement cospecialInstructionsElement = new XElement("SpecialInstructions", cospecialInstructions);
			this.Add(cospecialInstructionsElement);

			foreach (XElement clientDetailElement in clientOrderElement.Elements("ClientOrderDetail"))
			{
				XElement specimenElement = new XElement("ClientOrderDetail");
				string containerId = clientDetailElement.Element("ContainerId").Value;
				XElement containerIdElement = new XElement("ContainerId", containerId);
				specimenElement.Add(containerIdElement);

				string clientSpecimenNumber = clientDetailElement.Element("SpecimenNumber").Value;
				string clientDescription = clientDetailElement.Element("Description").Value;
				XElement clientDescriptionElement = new XElement("Description", clientSpecimenNumber + ". " + clientDescription);
				specimenElement.Add(clientDescriptionElement);

				string accessionedAsDescription = clientDetailElement.Element("DescriptionToAccession").Value;
				XElement accessionedAsDescriptionElement = new XElement("AccessionedAsDescription", accessionedAsDescription);
				specimenElement.Add(accessionedAsDescriptionElement);

				XElement accessionedAsNumberedDescriptionElement = new XElement("AccessionedAsNumberedDescription", clientSpecimenNumber + ". " + accessionedAsDescription);
				specimenElement.Add(accessionedAsNumberedDescriptionElement);

				string specialInstructions = clientDetailElement.Element("SpecialInstructions").Value;
				XElement specialInstructionsElement = new XElement("SpecialInstructions", specialInstructions);
				specimenElement.Add(specialInstructionsElement);

				string orderType = clientDetailElement.Element("OrderType").Value;
				XElement orderTypeElement = new XElement("OrderType", orderType);
				specimenElement.Add(orderTypeElement);

				string callbackNumber = clientDetailElement.Element("CallbackNumber").Value;
				XElement callbackNumberElement = new XElement("CallbackNumber", callbackNumber);
				specimenElement.Add(callbackNumberElement);

				string clientCollectionDate = clientDetailElement.Element("CollectionDate").Value;
				DateTime ccDate;
				if(DateTime.TryParse(clientCollectionDate, out ccDate) == true)
				{
					clientCollectionDate = ccDate.ToShortDateString() + " " + ccDate.ToShortTimeString();
				}
				XElement clientCollectionDateElement = new XElement("CollectionDate", clientCollectionDate);
				specimenElement.Add(clientCollectionDateElement);

				string clientSpecimenNumberMatchStatus = clientDetailElement.Element("SpecimenNumberMatchStatus").Value;
				XElement clientSpecimenNumberMatchStatusElement = new XElement("SpecimenNumberMatchStatus", clientSpecimenNumberMatchStatus);
				specimenElement.Add(clientSpecimenNumberMatchStatusElement);

				string clientSpecimenDescriptionMatchStatus = clientDetailElement.Element("SpecimenDescriptionMatchStatus").Value;
				XElement specimenDescriptionMatchStatusElement = new XElement("SpecimenDescriptionMatchStatus", clientSpecimenDescriptionMatchStatus);
				specimenElement.Add(specimenDescriptionMatchStatusElement);

				string clientOrderedBy = clientDetailElement.Element("OrderedBy").Value;
				XElement clientOrderedByElement = new XElement("OrderedBy", clientOrderedBy);
				specimenElement.Add(clientOrderedByElement);

				string clientOrderTime = clientDetailElement.Element("OrderTime").Value;
				DateTime clientODate;
				if (DateTime.TryParse(clientOrderTime, out clientODate) == true)
				{
					clientOrderTime = clientODate.ToShortDateString() + " " + clientODate.ToShortTimeString();
				}
				XElement clientOrderTimeElement = new XElement("OrderTime", clientOrderTime);
				specimenElement.Add(clientOrderTimeElement);

				string clientAccessioned = clientDetailElement.Element("Accessioned").Value;
				XElement clientAccessionedElement = new XElement("Accessioned", clientAccessioned);
				specimenElement.Add(clientAccessionedElement);

				this.Add(specimenElement);
			}
		}

		public string GetOrderAge(Nullable<DateTime> birthDate, DateTime orderDate)
		{
			string ageString = string.Empty;
			if (birthDate.HasValue)
			{
				System.TimeSpan timeSpan = new TimeSpan(orderDate.Ticks - birthDate.Value.Ticks);
				long days = timeSpan.Days;
				if (days < 14)
				{
					ageString = days.ToString() + "DO";
				}
				else if (days < 56)
				{
					long weeks = days / 7;
					ageString = weeks.ToString() + "WO";
				}
				else
				{
					DateTime dt = birthDate.Value.AddMonths(24);
					DateTime newdt = orderDate;
					if (dt.CompareTo(orderDate) > 0)
					{
						int mos = 0;
						newdt = newdt.AddMonths(-1);
						while (newdt.CompareTo(birthDate.Value) >= 0)
						{
							mos++;
							newdt = newdt.AddMonths(-1);
						}
						ageString = mos.ToString() + "MO";
					}
					else
					{
						int years = 0;
						newdt = newdt.AddYears(-1);
						while (newdt.CompareTo(birthDate.Value) >= 0)
						{
							years++;
							newdt = newdt.AddYears(-1);
						}
						ageString = years.ToString() + "YO";
					}
				}
			}
			return ageString;
		}
	}
}
