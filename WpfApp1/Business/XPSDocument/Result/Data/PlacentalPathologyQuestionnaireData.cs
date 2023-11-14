using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Document.Result.Data
{
	public class PlacentalPathologyQuestionnaireData
	{
		private string m_DateFormat = "yyyy-MM-ddTHH:mm:ss.FFF";    

		private XElement m_DataElement;
		//ClientOrder
		//private string m_ClientOrderId;
		//private bool m_Received;
		//private bool m_Submitted;
		//private Nullable<DateTime> m_OrderDate;
		//private Nullable<DateTime> m_OrderTime;
		//private string m_OrderedBy;
		private string m_PFirstName;
		private string m_PLastName;
		//private string m_PMiddleInitial;
		//private Nullable<DateTime> m_PBirthdate;
		//private string m_PSex;
		//private string m_PSSN;
		//private string m_SvhMedicalRecord;
		//private string m_SvhAccountNo;
		//private int m_ClientId;
		//private string m_ClientName;
		//private Nullable<int> m_ClientLocationId;
		//private string m_ProviderId;
		//private string m_ProviderName;
		//private string m_ClinicalHistory;
		//private string m_ReportCopyTo;
		//private string m_OrderType;
		//private Nullable<int> m_PanelSetId;
		//private bool m_Accessioned;
		//private bool m_Validated;
		//private Nullable<DateTime> m_CollectionDate;
		//private string m_ExternalOrderId;
		//private string m_IncomingHL7;
		//private string m_MasterAccessionNo;
		//private string m_OrderedByFirstName;
		//private string m_OrderedByLastName;
		//private string m_OrderedById;
		//private string m_ProviderFirstName;
		//private string m_ProviderLastName;
		//private bool m_Hold;
		//private string m_HoldMessage;
		//private string m_HoldBy;
		//private bool m_Acknowledged;
		//private int m_AcknowledgedById;
		//private Nullable<DateTime> m_AcknowledgedDate;
		//private string m_SystemInitiatingOrder;

		//PlacentaClientOrderDetail
		private string m_Birthdate;
		private string m_BirthTime;
		private string m_PlacentaDeliveryTime;
		private string m_GestationalAge;
		private string m_Gravida;
		private string m_Para;
		private bool m_Abortion;
		private string m_Apgar1Min;
		private string m_Apgar5Min;
		private string m_Apgar10Min;
		private string m_InfantWeight;
		private string m_SpecificQuestions;
		private bool m_GrossExam;
		private bool m_CompleteExam;
		private bool m_Cytogenetics;
		private string m_OtherExam;
		private bool m_DiabetesMellitus;
		private bool m_PregnancyInducedHypertension;
		private bool m_UnexplainedFever;
		private bool m_PrematureRuptureOfMembranes;
		private bool m_PoorOrLimitedPrenatalCare;
		private bool m_Polyhydramnios;
		private bool m_Oligohydramnios;
		private bool m_PretermDeliveryLessThan36Weeks;
		private bool m_PostTermDeliveryMoreThan42Weeks;
		private bool m_Infection;
		private bool m_PostpartumHemorrhage;
		private bool m_MaternalHistoryOfReproductiveFailure;
		private bool m_SeverePreeclampsia;
		private bool m_SuspectedDrugUse;
		private string m_Other1;
		private bool m_SuspectedInfection;
		private bool m_Stillborn;
		private bool m_ErythroblastosisFetalis;
		private bool m_NeonatalDeath;
		private bool m_TransferToNICU;
		private bool m_ViscidOrThickMeconium;
		private bool m_MultipleGestation;
		private bool m_OminousFHRTracing;
		private bool m_Prematurity;
		private bool m_IUGR;
		private bool m_ApgarLessThan7at5Min;
		private bool m_CordpHLessThan7dot10;
		private bool m_CongenitalAnomalies;
		private bool m_NeonatalSeizures;
		private string m_Other2;
		private bool m_Infarcts;
		private bool m_AbnormalCalcifications;
		private bool m_Abruption;
		private bool m_PlacentaPrevia;
		private bool m_VasaPrevia;
		private bool m_AbnormalCordAppearance;
		private bool m_Mass;
		private string m_Other3;
		private string m_DateSubmitted;
		private string m_SubmittedBy;

		public PlacentalPathologyQuestionnaireData(XElement dataElement)
		{
			this.m_DataElement = dataElement;
			SetClientOrderValues();
			SetPlacentaDetailValues();
		}

		private void SetClientOrderValues()
		{
			this.m_PFirstName = this.m_DataElement.Element("PFirstName").Value;
			this.m_PLastName = this.m_DataElement.Element("PLastName").Value;
		}

		private void SetPlacentaDetailValues()
		{
			XElement detailElement = this.m_DataElement.Element("ClientOrderDetail");

			if (detailElement.Element("DateSubmitted").Value == null)
			{
				this.m_DateSubmitted = string.Empty;
			}
			else
			{
				this.m_DateSubmitted = DateTime.Parse(detailElement.Element("DateSubmitted").Value).ToString(this.m_DateFormat);
			}

			this.m_Birthdate = detailElement.Element("Birthdate").Value;
			if (this.m_Birthdate == null) this.m_Birthdate = string.Empty;

			this.m_BirthTime = detailElement.Element("BirthTime").Value;
			if (this.m_BirthTime == null) this.m_BirthTime = string.Empty;

			this.m_PlacentaDeliveryTime = detailElement.Element("PlacentaDeliveryTime").Value;
			if (this.m_PlacentaDeliveryTime == null) this.m_PlacentaDeliveryTime = string.Empty;

			this.m_GestationalAge = detailElement.Element("GestationalAge").Value;
			if (this.m_GestationalAge == null) this.m_GestationalAge = string.Empty;

			this.m_Gravida = detailElement.Element("Gravida").Value;
			if (this.m_Gravida == null) this.m_Gravida = string.Empty;

			this.m_Para = detailElement.Element("Para").Value;
			if (this.m_Para == null) this.m_Para = string.Empty;

			this.m_Apgar1Min = detailElement.Element("Apgar1Min").Value;
			if (this.m_Apgar1Min == null) this.m_Apgar1Min = string.Empty;

			this.m_Apgar5Min = detailElement.Element("Apgar5Min").Value;
			if (this.m_Apgar5Min == null) this.m_Apgar5Min = string.Empty;

			this.m_Apgar10Min = detailElement.Element("Apgar10Min").Value;
			if (this.m_Apgar10Min == null) this.m_Apgar10Min = string.Empty;

			this.m_InfantWeight = detailElement.Element("InfantWeight").Value;
			if (this.m_InfantWeight == null) this.m_InfantWeight = string.Empty;

			this.m_SpecificQuestions = detailElement.Element("SpecificQuestions").Value;
			if (this.m_SpecificQuestions == null) this.m_SpecificQuestions = string.Empty;

			this.m_OtherExam = detailElement.Element("OtherExam").Value;
			if (this.m_OtherExam == null) this.m_OtherExam = string.Empty;

			this.m_Other1 = detailElement.Element("Other1").Value;
			if (this.m_Other1 == null) this.m_Other1 = string.Empty;
			
			this.m_Other2 = detailElement.Element("Other2").Value;
			if (this.m_Other2 == null) this.m_Other2 = string.Empty;

			this.m_Other3 = detailElement.Element("Other3").Value;
			if(this.m_Other3 == null) this.m_Other3 = string.Empty;

			this.m_SubmittedBy = detailElement.Element("SubmittedBy").Value;
			if(this.m_SubmittedBy == null) this.m_SubmittedBy = string.Empty;
	
			this.m_Abortion = Convert.ToBoolean(detailElement.Element("Abortion").Value);
			this.m_GrossExam = Convert.ToBoolean(detailElement.Element("GrossExam").Value);
			this.m_CompleteExam = Convert.ToBoolean(detailElement.Element("CompleteExam").Value);
			this.m_Cytogenetics = Convert.ToBoolean(detailElement.Element("Cytogenetics").Value);
			this.m_DiabetesMellitus = Convert.ToBoolean(detailElement.Element("DiabetesMellitus").Value);
			this.m_PregnancyInducedHypertension = Convert.ToBoolean(detailElement.Element("PregnancyInducedHypertension").Value);
			this.m_UnexplainedFever = Convert.ToBoolean(detailElement.Element("UnexplainedFever").Value);
			this.m_PrematureRuptureOfMembranes = Convert.ToBoolean(detailElement.Element("PrematureRuptureOfMembranes").Value);
			this.m_PoorOrLimitedPrenatalCare = Convert.ToBoolean(detailElement.Element("PoorOrLimitedPrenatalCare").Value);
			this.m_Polyhydramnios = Convert.ToBoolean(detailElement.Element("Polyhydramnios").Value);
			this.m_Oligohydramnios = Convert.ToBoolean(detailElement.Element("Oligohydramnios").Value);
			this.m_PretermDeliveryLessThan36Weeks = Convert.ToBoolean(detailElement.Element("PretermDeliveryLessThan36Weeks").Value);
			this.m_PostTermDeliveryMoreThan42Weeks = Convert.ToBoolean(detailElement.Element("PostTermDeliveryMoreThan42Weeks").Value);
			this.m_Infection = Convert.ToBoolean(detailElement.Element("Infection").Value);
			this.m_PostpartumHemorrhage = Convert.ToBoolean(detailElement.Element("PostpartumHemorrhage").Value);	
			this.m_MaternalHistoryOfReproductiveFailure = Convert.ToBoolean(detailElement.Element("MaternalHistoryOfReproductiveFailure").Value);
			this.m_SeverePreeclampsia = Convert.ToBoolean(detailElement.Element("SeverePreeclampsia").Value);
			this.m_SuspectedDrugUse = Convert.ToBoolean(detailElement.Element("SuspectedDrugUse").Value);
			this.m_SuspectedInfection = Convert.ToBoolean(detailElement.Element("SuspectedInfection").Value);
			this.m_Stillborn = Convert.ToBoolean(detailElement.Element("Stillborn").Value);
			this.m_ErythroblastosisFetalis = Convert.ToBoolean(detailElement.Element("ErythroblastosisFetalis").Value);
			this.m_NeonatalDeath = Convert.ToBoolean(detailElement.Element("NeonatalDeath").Value);
			this.m_TransferToNICU = Convert.ToBoolean(detailElement.Element("TransferToNICU").Value);
			this.m_ViscidOrThickMeconium = Convert.ToBoolean(detailElement.Element("ViscidOrThickMeconium").Value);
			this.m_MultipleGestation = Convert.ToBoolean(detailElement.Element("MultipleGestation").Value);
			this.m_OminousFHRTracing = Convert.ToBoolean(detailElement.Element("OminousFHRTracing").Value);
			this.m_Prematurity = Convert.ToBoolean(detailElement.Element("Prematurity").Value);
			this.m_IUGR = Convert.ToBoolean(detailElement.Element("IUGR").Value);
			this.m_ApgarLessThan7at5Min = Convert.ToBoolean(detailElement.Element("ApgarLessThan7at5Min").Value);
			this.m_CordpHLessThan7dot10 = Convert.ToBoolean(detailElement.Element("CordpHLessThan7dot10").Value);
			this.m_CongenitalAnomalies = Convert.ToBoolean(detailElement.Element("CongenitalAnomalies").Value);
			this.m_NeonatalSeizures = Convert.ToBoolean(detailElement.Element("NeonatalSeizures").Value);
			this.m_Infarcts = Convert.ToBoolean(detailElement.Element("Infarcts").Value);
			this.m_AbnormalCalcifications = Convert.ToBoolean(detailElement.Element("AbnormalCalcifications").Value);
			this.m_Abruption = Convert.ToBoolean(detailElement.Element("Abruption").Value);
			this.m_PlacentaPrevia = Convert.ToBoolean(detailElement.Element("PlacentaPrevia").Value);
			this.m_VasaPrevia = Convert.ToBoolean(detailElement.Element("VasaPrevia").Value);
			this.m_AbnormalCordAppearance = Convert.ToBoolean(detailElement.Element("AbnormalCordAppearance").Value);
			this.m_Mass = Convert.ToBoolean(detailElement.Element("Mass").Value);
		}

		/*public string ClientOrderId
		{
			get { return this.m_ClientOrderId; }
		}

		public bool Received
		{
			get { return this.m_Received; }
		}

		public bool Submitted
		{
			get { return this.m_Submitted; }
		}

		public Nullable<DateTime> OrderDate
		{
			get { return this.m_OrderDate; }
		}

		public Nullable<DateTime> OrderTime
		{
			get { return this.m_OrderTime; }
		}

		public string OrderedBy
		{
			get { return this.m_OrderedBy; }
		}*/

		public string PFirstName
		{
			get { return this.m_PFirstName; }
		}

		public string PLastName
		{
			get { return this.m_PLastName; }
		}

		/*public string PMiddleInitial
		{
			get { return this.m_PMiddleInitial; }
		}

		public Nullable<DateTime> PBirthdate
		{
			get { return this.m_PBirthdate; }
		}

		public string PSex
		{
			get { return this.m_PSex; }
		}

		public string PSSN
		{
			get { return this.m_PSSN; }
		}

		public string SvhMedicalRecord
		{
			get { return this.m_SvhMedicalRecord; }
		}

		public string SvhAccountNo
		{
			get { return this.m_SvhAccountNo; }
		}

		public int ClientId
		{
			get { return this.m_ClientId; }
		}

		public string ClientName
		{
			get { return this.m_ClientName; }
		}

		public Nullable<int> ClientLocationId
		{
			get { return this.m_ClientLocationId; }
		}

		public string ProviderId
		{
			get { return this.m_ProviderId; }
		}

		public string ProviderName
		{
			get { return this.m_ProviderName; }
		}

		public string ClinicalHistory
		{
			get { return this.m_ClinicalHistory; }
		}

		public string ReportCopyTo
		{
			get { return this.m_ReportCopyTo; }
		}

		public string OrderType
		{
			get { return this.m_OrderType; }
		}

		public Nullable<int> PanelSetId
		{
			get { return this.m_PanelSetId; }
		}

		public bool Accessioned
		{
			get { return this.m_Accessioned; }
		}

		public bool Validated
		{
			get { return this.m_Validated; }
		}

		public Nullable<DateTime> CollectionDate
		{
			get { return this.m_CollectionDate; }
		}

		public string ExternalOrderId
		{
			get { return this.m_ExternalOrderId; }
		}

		public string IncomingHL7
		{
			get { return this.m_IncomingHL7; }
		}

		public string MasterAccessionNo
		{
			get { return this.m_MasterAccessionNo; }
		}

		public string OrderedByFirstName
		{
			get { return this.m_OrderedByFirstName; }
		}

		public string OrderedByLastName
		{
			get { return this.m_OrderedByLastName; }
		}

		public string OrderedById
		{
			get { return this.m_OrderedById; }
		}

		public string ProviderFirstName
		{
			get { return this.m_ProviderFirstName; }
		}

		public string ProviderLastName
		{
			get { return this.m_ProviderLastName; }
		}

		public bool Hold
		{
			get { return this.m_Hold; }
		}

		public string HoldMessage
		{
			get { return this.m_HoldMessage; }
		}

		public string HoldBy
		{
			get { return this.m_HoldBy; }
		}

		public bool Acknowledged
		{
			get { return this.m_Acknowledged; }
		}

		public int AcknowledgedById
		{
			get { return this.m_AcknowledgedById; }
		}

		public Nullable<DateTime> AcknowledgedDate
		{
			get { return this.m_AcknowledgedDate; }
		}

		public string SystemInitiatingOrder
		{
			get { return this.m_SystemInitiatingOrder; }
		}*/

		public string Birthdate
		{
			get { return this.m_Birthdate; }
		}

		public string BirthTime
		{
			get { return this.m_BirthTime; }
		}

		public string PlacentaDeliveryTime
		{
			get { return this.m_PlacentaDeliveryTime; }
		}

		public string GestationalAge
		{
			get { return this.m_GestationalAge; }
		}

		public string Gravida
		{
			get { return this.m_Gravida; }
		}

		public string Para
		{
			get { return this.m_Para; }
		}

		public bool Abortion
		{
			get { return this.m_Abortion; }
		}

		public string Apgar1Min
		{
			get { return this.m_Apgar1Min; }
		}

		public string Apgar5Min
		{
			get { return this.m_Apgar5Min; }
		}

		public string Apgar10Min
		{
			get { return this.m_Apgar10Min; }
		}

		public string InfantWeight
		{
			get { return this.m_InfantWeight; }
		}

		public string SpecificQuestions
		{
			get { return this.m_SpecificQuestions; }
		}

		public bool GrossExam
		{
			get { return this.m_GrossExam; }
		}

		public bool CompleteExam
		{
			get { return this.m_CompleteExam; }
		}

		public bool Cytogenetics
		{
			get { return this.m_Cytogenetics; }
		}

		public string OtherExam
		{
			get { return this.m_OtherExam; }
		}

		public bool DiabetesMellitus
		{
			get { return this.m_DiabetesMellitus; }
		}

		public bool PregnancyInducedHypertension
		{
			get { return this.m_PregnancyInducedHypertension; }
		}

		public bool UnexplainedFever
		{
			get { return this.m_UnexplainedFever; }
		}

		public bool PrematureRuptureOfMembranes
		{
			get { return this.m_PrematureRuptureOfMembranes; }
		}

		public bool PoorOrLimitedPrenatalCare
		{
			get { return this.m_PoorOrLimitedPrenatalCare; }
		}

		public bool Polyhydramnios
		{
			get { return this.m_Polyhydramnios; }
		}

		public bool Oligohydramnios
		{
			get { return this.m_Oligohydramnios; }
		}

		public bool PretermDeliveryLessThan36Weeks
		{
			get { return this.m_PretermDeliveryLessThan36Weeks; }
		}

		public bool PostTermDeliveryMoreThan42Weeks
		{
			get { return this.m_PostTermDeliveryMoreThan42Weeks; }
		}

		public bool Infection
		{
			get { return this.m_Infection; }
		}

		public bool PostpartumHemorrhage
		{
			get { return this.m_PostpartumHemorrhage; }
		}

		public bool MaternalHistoryOfReproductiveFailure
		{
			get { return this.m_MaternalHistoryOfReproductiveFailure; }
		}

		public bool SeverePreeclampsia
		{
			get { return this.m_SeverePreeclampsia; }
		}

		public bool SuspectedDrugUse
		{
			get { return this.m_SuspectedDrugUse; }
		}

		public string Other1
		{
			get { return this.m_Other1; }
		}

		public bool SuspectedInfection
		{
			get { return this.m_SuspectedInfection; }
		}

		public bool Stillborn
		{
			get { return this.m_Stillborn; }
		}

		public bool ErythroblastosisFetalis
		{
			get { return this.m_ErythroblastosisFetalis; }
		}

		public bool NeonatalDeath
		{
			get { return this.m_NeonatalDeath; }
		}

		public bool TransferToNICU
		{
			get { return this.m_TransferToNICU; }
		}

		public bool ViscidOrThickMeconium
		{
			get { return this.m_ViscidOrThickMeconium; }
		}

		public bool MultipleGestation
		{
			get { return this.m_MultipleGestation; }
		}

		public bool OminousFHRTracing
		{
			get { return this.m_OminousFHRTracing; }
		}

		public bool Prematurity
		{
			get { return this.m_Prematurity; }
		}

		public bool IUGR
		{
			get { return this.m_IUGR; }
		}

		public bool ApgarLessThan7at5Min
		{
			get { return this.m_ApgarLessThan7at5Min; }
		}

		public bool CordpHLessThan7dot10
		{
			get { return this.m_CordpHLessThan7dot10; }
		}

		public bool CongenitalAnomalies
		{
			get { return this.m_CongenitalAnomalies; }
		}

		public bool NeonatalSeizures
		{
			get { return this.m_NeonatalSeizures; }
		}

		public string Other2
		{
			get { return this.m_Other2; }
		}

		public bool Infarcts
		{
			get { return this.m_Infarcts; }
		}

		public bool AbnormalCalcifications
		{
			get { return this.m_AbnormalCalcifications; }
		}

		public bool Abruption
		{
			get { return this.m_Abruption; }
		}

		public bool PlacentaPrevia
		{
			get { return this.m_PlacentaPrevia; }
		}

		public bool VasaPrevia
		{
			get { return this.m_VasaPrevia; }
		}

		public bool AbnormalCordAppearance
		{
			get { return this.m_AbnormalCordAppearance; }
		}

		public bool Mass
		{
			get { return this.m_Mass; }
		}

		public string Other3
		{
			get { return this.m_Other3; }
		}

		public string DateSubmitted
		{
			get { return this.m_DateSubmitted; }
		}

		public string SubmittedBy
		{
			get { return this.m_SubmittedBy; }
		}
	}
}
