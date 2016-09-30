using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Document.Result.Data
{
    public class AccessionOrderDataSheetData : XElement
    {
        public AccessionOrderDataSheetData( XElement accessionOrderDocument, XElement specimenOrderDocument, XElement clientOrderDocument, XElement caseNotesDocument)
            : base("AccessionOrderDataSheet")
        {						
			string patientDisplayName = accessionOrderDocument.Element("PLastName").Value + ", " + accessionOrderDocument.Element("PFirstName").Value;
			string patientBirthdate = accessionOrderDocument.Element("PBirthdate").Value;
			DateTime aDate = DateTime.Parse(accessionOrderDocument.Element("AccessionDate").Value);
			DateTime date;
			DateTime? bDate = null;
			if (DateTime.TryParse(patientBirthdate, out date) == true)
			{
				bDate = date;
			}

            if (bDate.HasValue == true)
            {
                string pAge = this.GetAccessionAge(bDate, aDate);
                patientDisplayName += "(" + bDate.Value.ToShortDateString() + ", " + pAge + ", " + accessionOrderDocument.Element("PSex").Value + ")";
            }

            XElement patientNameElement = new XElement("PatientDisplayName", patientDisplayName);
            this.Add(patientNameElement);

			XElement ssnElement = new XElement("SSN", accessionOrderDocument.Element("PSSN").Value);
			this.Add(ssnElement);

            string clientName = accessionOrderDocument.Element("ClientName").Value;
            XElement clientNameElement = new XElement("ClientName", clientName);
            this.Add(clientNameElement);

            string physicianName = accessionOrderDocument.Element("PhysicianName").Value;
            XElement physicianNameElement = new XElement("PhysicianName", physicianName);
            this.Add(physicianNameElement);

            string masterAccessionNo = accessionOrderDocument.Element("MasterAccessionNo").Value;
            XElement masterAccessionNoElement = new XElement("MasterAccessionNo", masterAccessionNo);
            this.Add(masterAccessionNoElement);

			DateTime accessionTime = DateTime.Parse(accessionOrderDocument.Element("AccessionTime").Value);			
			XElement accessionTimeElement = new XElement("AccessionTime", accessionTime.ToString("MM/dd/yyyy HH:mm"));
			this.Add(accessionTimeElement);

			XElement medRecordElement = new XElement("SvhMedicalRecord", accessionOrderDocument.Element("SvhMedicalRecord").Value);
			this.Add(medRecordElement);

			XElement accountNoElement = new XElement("SvhAccount", accessionOrderDocument.Element("SvhAccount").Value);
			this.Add(accountNoElement);

			string clinicalHistory = string.Empty;
			string preOpDiagnosis = string.Empty;
            string coSpecialInstructions = string.Empty;                               

			foreach (XElement clientElement in clientOrderDocument.Elements("ClientOrder"))
			{
				string clientClinicalHistory = clientElement.Element("ClinicalHistory").Value;

                string clientPreOpDiagnosis = string.Empty;
                if (clientElement.Element("PreOpDiagnosis") != null) clientPreOpDiagnosis = clientElement.Element("PreOpDiagnosis").Value;

				if (string.IsNullOrEmpty(clinicalHistory) == true)
				{
					if (string.IsNullOrEmpty(clientClinicalHistory) == false)
					{
						clinicalHistory = clientClinicalHistory;
					}
				}

				if (string.IsNullOrEmpty(preOpDiagnosis) == true)
				{
					if (string.IsNullOrEmpty(clientPreOpDiagnosis) == false)
					{
						preOpDiagnosis = clientPreOpDiagnosis;
					}
				}

                if (clientElement.Element("SpecialInstructions") != null) coSpecialInstructions += clientElement.Element("SpecialInstructions").Value;                
			}

			XElement clinicalHistoryElement = new XElement("ClinicalHistory", clinicalHistory);
			this.Add(clinicalHistoryElement);

			XElement preOpDiagnosisElement = new XElement("PreOpDiagnosis", preOpDiagnosis);
			this.Add(preOpDiagnosisElement);

            XElement coSpecialInstructionsElement = new XElement("SpecialInstructions", coSpecialInstructions);
            this.Add(coSpecialInstructionsElement);            

			foreach (XElement clientOrderElement in clientOrderDocument.Elements("ClientOrder"))
			{
				XElement clientReportElement = new XElement("ClientOrder");

				string clientOrderName = clientOrderElement.Element("ClientName").Value;
				XElement clientOrderNameElement = new XElement("ClientName", clientOrderName);
				clientReportElement.Add(clientOrderNameElement);

				string orderedBy = clientOrderElement.Element("OrderedBy").Value;
				XElement orderedByElement = new XElement("OrderedBy", orderedBy);
				clientReportElement.Add(orderedByElement);

				string clientOrderDate = clientOrderElement.Element("OrderDate").Value;
				DateTime coDate;
				if (DateTime.TryParse(clientOrderDate, out coDate) == true)
				{
					clientOrderDate = coDate.ToShortDateString();
				}
				XElement clientOrderDateElement = new XElement("OrderDate", clientOrderDate);
				clientReportElement.Add(clientOrderDateElement);

				string submitted = clientOrderElement.Element("Submitted").Value;
				XElement submittedElement = new XElement("Submitted", submitted);
				clientReportElement.Add(submittedElement);

				string accessioned = clientOrderElement.Element("Accessioned").Value;
				XElement accessionedElement = new XElement("Accessioned", accessioned);
				clientReportElement.Add(accessionedElement);

				string initiatingSystem = clientOrderElement.Element("SystemInitiatingOrder").Value;
				XElement initiatingSystemElement = new XElement("SystemInitiatingOrder", initiatingSystem);
				clientReportElement.Add(initiatingSystemElement);

				this.Add(clientReportElement);

			}

            foreach (XElement element in specimenOrderDocument.Elements("SpecimenOrder"))
            {
                XElement specimenOrderElement = new XElement("SpecimenOrder");
				XElement specimenAccessionElement = new XElement("AccessionOrder");
				XElement specimenClientElement = new XElement("ClientOrder");

				string specimenNumber = element.Element("SpecimenNumber").Value;
				string specimenDescription = element.Element("Description").Value;
				XElement specimenDescriptionElement = new XElement("Description", specimenNumber + ". " + specimenDescription);
				specimenAccessionElement.Add(specimenDescriptionElement);                
                
                string collectionDate = element.Element("CollectionDate").Value;
                string collectionTime = element.Element("CollectionTime").Value;
				YellowstonePathology.Business.Helper.DateTimeJoiner dateTimeJoiner = new Business.Helper.DateTimeJoiner(collectionDate, collectionTime);
                XElement specimenCollectionTimeElement = new XElement("CollectionTime", dateTimeJoiner.DisplayString);
                specimenAccessionElement.Add(specimenCollectionTimeElement);

				string specimenReceivedIn = element.Element("ClientFixation").Value;
				XElement specimenReceivedInElement = new XElement("ReceivedIn", specimenReceivedIn);
				specimenAccessionElement.Add(specimenReceivedInElement);

				string specimenProcessedIn = element.Element("LabFixation").Value;
				XElement specimenProcessedInElement = new XElement("ProcessedIn", specimenProcessedIn);
				specimenAccessionElement.Add(specimenProcessedInElement);				

				string specimenAccessionTime = element.Element("AccessionTime").Value;
				DateTime saDate;
				if (DateTime.TryParse(specimenAccessionTime, out saDate) == true)
				{
					specimenAccessionTime = saDate.ToShortDateString() + " " + saDate.ToShortTimeString();
				}
				XElement specimenAccessionTimeElement = new XElement("AccessionTime", specimenAccessionTime);
				specimenAccessionElement.Add(specimenAccessionTimeElement);

				string specimenVerified = element.Element("Verified").Value;
				XElement specimenVerifiedElement = new XElement("Verified", specimenVerified);
				specimenAccessionElement.Add(specimenVerifiedElement);

				string specimenVerifiedById = element.Element("VerifiedById").Value;
				XElement specimenVerifiedByIdElement = new XElement("VerifiedById", specimenVerifiedById);
				specimenAccessionElement.Add(specimenVerifiedByIdElement);

				string specimenVerifiedDate = element.Element("VerifiedDate").Value;
				DateTime svDate;
				if (DateTime.TryParse(specimenVerifiedDate, out svDate) == true)
				{
					specimenVerifiedDate = svDate.ToShortDateString() + " " + svDate.ToShortTimeString();
				}
				XElement specimenVerifiedDateElement = new XElement("VerifiedDate", specimenVerifiedDate);
				specimenAccessionElement.Add(specimenVerifiedDateElement);

				foreach (XElement clientOrderElement in clientOrderDocument.Elements("ClientOrder"))
				{
					foreach (XElement clientSpecimenElement in clientOrderElement.Elements("ClientOrderDetail"))
					{
						string containerId = clientSpecimenElement.Element("ContainerId").Value;
						if (containerId == element.Element("ContainerId").Value)
						{
							string clientSpecimenNumber = clientSpecimenElement.Element("SpecimenNumber").Value;
							string clientDescription = clientSpecimenElement.Element("Description").Value;
							XElement clientDescriptionElement = new XElement("Description", clientSpecimenNumber + ". " + clientDescription);
							specimenClientElement.Add(clientDescriptionElement);

							string accessionedAsDescription = clientSpecimenElement.Element("DescriptionToAccession").Value;
							XElement accessionedAsDescriptionElement = new XElement("AccessionedAsDescription", accessionedAsDescription);
							specimenClientElement.Add(accessionedAsDescriptionElement);

							XElement accessionedAsNumberedDescriptionElement = new XElement("AccessionedAsNumberedDescription", clientSpecimenNumber + ". " + accessionedAsDescription);
							specimenClientElement.Add(accessionedAsNumberedDescriptionElement);

							string specialInstructions = clientSpecimenElement.Element("SpecialInstructions").Value;
							XElement specialInstructionsElement = new XElement("SpecialInstructions", specialInstructions);
							specimenClientElement.Add(specialInstructionsElement);

							string orderType = clientSpecimenElement.Element("OrderType").Value;
							XElement orderTypeElement = new XElement("SpecialInstructions", specialInstructions);
							specimenClientElement.Add(orderTypeElement);

							string callbackNumber = clientSpecimenElement.Element("CallbackNumber").Value;
							XElement callbackNumberElement = new XElement("CallbackNumber", callbackNumber);
							specimenClientElement.Add(callbackNumberElement);

							string clientCollectionDate = clientSpecimenElement.Element("CollectionDate").Value;
							DateTime ccDate;
							if(DateTime.TryParse(clientCollectionDate, out ccDate) == true)
							{
								clientCollectionDate = ccDate.ToShortDateString() + " " + ccDate.ToShortTimeString();
							}
							XElement clientCollectionDateElement = new XElement("CollectionDate", clientCollectionDate);
							specimenClientElement.Add(clientCollectionDateElement);

							string clientSpecimenNumberMatchStatus = clientSpecimenElement.Element("SpecimenNumberMatchStatus").Value;
							XElement clientSpecimenNumberMatchStatusElement = new XElement("SpecimenNumberMatchStatus", clientSpecimenNumberMatchStatus);
							specimenClientElement.Add(clientSpecimenNumberMatchStatusElement);

							string clientSpecimenDescriptionMatchStatus = clientSpecimenElement.Element("SpecimenDescriptionMatchStatus").Value;
							XElement specimenDescriptionMatchStatusElement = new XElement("SpecimenDescriptionMatchStatus", clientSpecimenDescriptionMatchStatus);
							specimenClientElement.Add(specimenDescriptionMatchStatusElement);

							string clientOrderedBy = clientSpecimenElement.Element("OrderedBy").Value;
							XElement orderedByElement = new XElement("OrderedBy", clientOrderedBy);
							specimenClientElement.Add(orderedByElement);

							string clientOrderTime = clientSpecimenElement.Element("OrderTime").Value;
							DateTime coDate;
							if (DateTime.TryParse(clientOrderTime, out coDate) == true)
							{
								clientOrderTime = coDate.ToShortDateString() + " " + coDate.ToShortTimeString();
							}
							XElement clientOrderTimeElement = new XElement("OrderTime", clientOrderTime);
							specimenClientElement.Add(clientOrderTimeElement);

							string clientAccessioned = clientSpecimenElement.Element("Accessioned").Value;
							XElement accessionedElement = new XElement("Accessioned", clientAccessioned);
							specimenClientElement.Add(accessionedElement);

							break;
						}
					}
				}

				specimenOrderElement.Add(specimenAccessionElement);
				if (string.IsNullOrEmpty(specimenClientElement.Value) == true)
				{
					this.SetEmptyClientElement(specimenClientElement);
				}
				specimenOrderElement.Add(specimenClientElement);
                this.Add(specimenOrderElement);
            }

			foreach (XElement orderCommentElement in caseNotesDocument.Elements("OrderComment"))
			{
				XElement caseNoteElement = new XElement("CaseNote");

				string loggedBy = orderCommentElement.Element("LoggedBy").Value;
				XElement loggedByElement = new XElement("LoggedBy", loggedBy);
				caseNoteElement.Add(loggedByElement);

				string description = orderCommentElement.Element("Description").Value;
				XElement descriptionElement = new XElement("Description", description);
				caseNoteElement.Add(descriptionElement);

				string comment = orderCommentElement.Element("Comment").Value;
				XElement commentElement = new XElement("Comment", comment);
				caseNoteElement.Add(commentElement);

				string logDate = orderCommentElement.Element("LogDate").Value;
				DateTime lDate;
				if (DateTime.TryParse(logDate, out lDate) == true)
				{
					logDate = lDate.ToShortDateString() + " " + lDate.ToShortTimeString();
				}
				XElement logDateElement = new XElement("LogDate", logDate);
				caseNoteElement.Add(logDateElement);

				this.Add(caseNoteElement);
			}
		}

		private void SetEmptyClientElement(XElement specimenClientElement)
		{
			XElement clientDescriptionElement = new XElement("Description", "None");
			specimenClientElement.Add(clientDescriptionElement);

			XElement accessionedAsDescriptionElement = new XElement("AccessionedAsDescription");
			specimenClientElement.Add(accessionedAsDescriptionElement);

			XElement accessionedAsNumberedDescriptionElement = new XElement("AccessionedAsNumberedDescription");
			specimenClientElement.Add(accessionedAsNumberedDescriptionElement);

			XElement specialInstructionsElement = new XElement("SpecialInstructions");
			specimenClientElement.Add(specialInstructionsElement);

			XElement orderTypeElement = new XElement("SpecialInstructions");
			specimenClientElement.Add(orderTypeElement);

			XElement callbackNumberElement = new XElement("CallbackNumber");
			specimenClientElement.Add(callbackNumberElement);

			XElement clientCollectionDateElement = new XElement("CollectionDate");
			specimenClientElement.Add(clientCollectionDateElement);

			XElement clientSpecimenNumberMatchStatusElement = new XElement("SpecimenNumberMatchStatus");
			specimenClientElement.Add(clientSpecimenNumberMatchStatusElement);

			XElement specimenDescriptionMatchStatusElement = new XElement("SpecimenDescriptionMatchStatus");
			specimenClientElement.Add(specimenDescriptionMatchStatusElement);

			XElement orderedByElement = new XElement("OrderedBy");
			specimenClientElement.Add(orderedByElement);

			XElement clientOrderTimeElement = new XElement("OrderTime");
			specimenClientElement.Add(clientOrderTimeElement);

			XElement accessionedElement = new XElement("Accessioned");
			specimenClientElement.Add(accessionedElement);
		}

		public string GetAccessionAge(Nullable<DateTime> birthDate, DateTime accessionDate)
		{
			string ageString = string.Empty;
			if (birthDate.HasValue)
			{
				System.TimeSpan timeSpan = new TimeSpan(accessionDate.Ticks - birthDate.Value.Ticks);
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
					DateTime newdt = accessionDate;
					if (dt.CompareTo(accessionDate) > 0)
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
