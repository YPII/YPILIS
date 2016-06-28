using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Web;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	[DataContract]
	[PersistentClass("tblPlacentaClientOrderDetail", "tblClientOrderDetail", "YPIDATA")]
	public partial class PlacentaClientOrderDetail : ClientOrderDetail
	{
        public PlacentaClientOrderDetail()
        {

        }

		public PlacentaClientOrderDetail(YellowstonePathology.Business.Persistence.PersistenceModeEnum persistenceMode) 
            : base(persistenceMode)
		{

		}

		public PlacentaClientOrderDetail(YellowstonePathology.Business.Persistence.PersistenceModeEnum persistenceMode, string objectId)
			: base(persistenceMode, objectId)
		{

		}

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
		private Nullable<DateTime> m_DateSubmitted;
		private string m_SubmittedBy;

		[PersistentProperty()]
		[DataMember]
		public string Birthdate
		{
			get { return this.m_Birthdate; }
			set
			{
				if (this.m_Birthdate != value)
				{
					this.m_Birthdate = value;
					this.NotifyPropertyChanged("Birthdate");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public string BirthTime
		{
			get { return this.m_BirthTime; }
			set
			{
				if (this.m_BirthTime != value)
				{
					this.m_BirthTime = value;
					this.NotifyPropertyChanged("BirthTime");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public string PlacentaDeliveryTime
		{
			get { return this.m_PlacentaDeliveryTime; }
			set
			{
				if (this.m_PlacentaDeliveryTime != value)
				{
					this.m_PlacentaDeliveryTime = value;
					this.NotifyPropertyChanged("PlacentaDeliveryTime");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public string GestationalAge
		{
			get { return this.m_GestationalAge; }
			set
			{
				if (this.m_GestationalAge != value)
				{
					this.m_GestationalAge = value;
					this.NotifyPropertyChanged("GestationalAge");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public string Gravida
		{
			get { return this.m_Gravida; }
			set
			{
				if (this.m_Gravida != value)
				{
					this.m_Gravida = value;
					this.NotifyPropertyChanged("Gravida");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public string Para
		{
			get { return this.m_Para; }
			set
			{
				if (this.m_Para != value)
				{
					this.m_Para = value;
					this.NotifyPropertyChanged("Para");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool Abortion
		{
			get { return this.m_Abortion; }
			set
			{
				if (this.m_Abortion != value)
				{
					this.m_Abortion = value;
					this.NotifyPropertyChanged("Abortion");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public string Apgar1Min
		{
			get { return this.m_Apgar1Min; }
			set
			{
				if (this.m_Apgar1Min != value)
				{
					this.m_Apgar1Min = value;
					this.NotifyPropertyChanged("Apgar1Min");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public string Apgar5Min
		{
			get { return this.m_Apgar5Min; }
			set
			{
				if (this.m_Apgar5Min != value)
				{
					this.m_Apgar5Min = value;
					this.NotifyPropertyChanged("Apgar5Min");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public string Apgar10Min
		{
			get { return this.m_Apgar10Min; }
			set
			{
				if (this.m_Apgar10Min != value)
				{
					this.m_Apgar10Min = value;
					this.NotifyPropertyChanged("Apgar10Min");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public string InfantWeight
		{
			get { return this.m_InfantWeight; }
			set
			{
				if (this.m_InfantWeight != value)
				{
					this.m_InfantWeight = value;
					this.NotifyPropertyChanged("InfantWeight");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public string SpecificQuestions
		{
			get { return this.m_SpecificQuestions; }
			set
			{
				if (this.m_SpecificQuestions != value)
				{
					this.m_SpecificQuestions = value;
					this.NotifyPropertyChanged("SpecificQuestions");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool GrossExam
		{
			get { return this.m_GrossExam; }
			set
			{
				if (this.m_GrossExam != value)
				{
					this.m_GrossExam = value;
					this.NotifyPropertyChanged("GrossExam");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool CompleteExam
		{
			get { return this.m_CompleteExam; }
			set
			{
				if (this.m_CompleteExam != value)
				{
					this.m_CompleteExam = value;
					this.NotifyPropertyChanged("CompleteExam");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool Cytogenetics
		{
			get { return this.m_Cytogenetics; }
			set
			{
				if (this.m_Cytogenetics != value)
				{
					this.m_Cytogenetics = value;
					this.NotifyPropertyChanged("Cytogenetics");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public string OtherExam
		{
			get { return this.m_OtherExam; }
			set
			{
				if (this.m_OtherExam != value)
				{
					this.m_OtherExam = value;
					this.NotifyPropertyChanged("OtherExam");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool DiabetesMellitus
		{
			get { return this.m_DiabetesMellitus; }
			set
			{
				if (this.m_DiabetesMellitus != value)
				{
					this.m_DiabetesMellitus = value;
					this.NotifyPropertyChanged("DiabetesMellitus");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool PregnancyInducedHypertension
		{
			get { return this.m_PregnancyInducedHypertension; }
			set
			{
				if (this.m_PregnancyInducedHypertension != value)
				{
					this.m_PregnancyInducedHypertension = value;
					this.NotifyPropertyChanged("PregnancyInducedHypertension");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool UnexplainedFever
		{
			get { return this.m_UnexplainedFever; }
			set
			{
				if (this.m_UnexplainedFever != value)
				{
					this.m_UnexplainedFever = value;
					this.NotifyPropertyChanged("UnexplainedFever");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool PrematureRuptureOfMembranes
		{
			get { return this.m_PrematureRuptureOfMembranes; }
			set
			{
				if (this.m_PrematureRuptureOfMembranes != value)
				{
					this.m_PrematureRuptureOfMembranes = value;
					this.NotifyPropertyChanged("PrematureRuptureOfMembranes");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool PoorOrLimitedPrenatalCare
		{
			get { return this.m_PoorOrLimitedPrenatalCare; }
			set
			{
				if (this.m_PoorOrLimitedPrenatalCare != value)
				{
					this.m_PoorOrLimitedPrenatalCare = value;
					this.NotifyPropertyChanged("PoorOrLimitedPrenatalCare");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool Polyhydramnios
		{
			get { return this.m_Polyhydramnios; }
			set
			{
				if (this.m_Polyhydramnios != value)
				{
					this.m_Polyhydramnios = value;
					this.NotifyPropertyChanged("Polyhydramnios");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool Oligohydramnios
		{
			get { return this.m_Oligohydramnios; }
			set
			{
				if (this.m_Oligohydramnios != value)
				{
					this.m_Oligohydramnios = value;
					this.NotifyPropertyChanged("Oligohydramnios");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool PretermDeliveryLessThan36Weeks
		{
			get { return this.m_PretermDeliveryLessThan36Weeks; }
			set
			{
				if (this.m_PretermDeliveryLessThan36Weeks != value)
				{
					this.m_PretermDeliveryLessThan36Weeks = value;
					this.NotifyPropertyChanged("PretermDeliveryLessThan36Weeks");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool PostTermDeliveryMoreThan42Weeks
		{
			get { return this.m_PostTermDeliveryMoreThan42Weeks; }
			set
			{
				if (this.m_PostTermDeliveryMoreThan42Weeks != value)
				{
					this.m_PostTermDeliveryMoreThan42Weeks = value;
					this.NotifyPropertyChanged("PostTermDeliveryMoreThan42Weeks");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool Infection
		{
			get { return this.m_Infection; }
			set
			{
				if (this.m_Infection != value)
				{
					this.m_Infection = value;
					this.NotifyPropertyChanged("Infection");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool PostpartumHemorrhage
		{
			get { return this.m_PostpartumHemorrhage; }
			set
			{
				if (this.m_PostpartumHemorrhage != value)
				{
					this.m_PostpartumHemorrhage = value;
					this.NotifyPropertyChanged("PostpartumHemorrhage");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool MaternalHistoryOfReproductiveFailure
		{
			get { return this.m_MaternalHistoryOfReproductiveFailure; }
			set
			{
				if (this.m_MaternalHistoryOfReproductiveFailure != value)
				{
					this.m_MaternalHistoryOfReproductiveFailure = value;
					this.NotifyPropertyChanged("MaternalHistoryOfReproductiveFailure");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool SeverePreeclampsia
		{
			get { return this.m_SeverePreeclampsia; }
			set
			{
				if (this.m_SeverePreeclampsia != value)
				{
					this.m_SeverePreeclampsia = value;
					this.NotifyPropertyChanged("SeverePreeclampsia");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool SuspectedDrugUse
		{
			get { return this.m_SuspectedDrugUse; }
			set
			{
				if (this.m_SuspectedDrugUse != value)
				{
					this.m_SuspectedDrugUse = value;
					this.NotifyPropertyChanged("SuspectedDrugUse");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public string Other1
		{
			get { return this.m_Other1; }
			set
			{
				if (this.m_Other1 != value)
				{
					this.m_Other1 = value;
					this.NotifyPropertyChanged("Other1");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool SuspectedInfection
		{
			get { return this.m_SuspectedInfection; }
			set
			{
				if (this.m_SuspectedInfection != value)
				{
					this.m_SuspectedInfection = value;
					this.NotifyPropertyChanged("SuspectedInfection");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool Stillborn
		{
			get { return this.m_Stillborn; }
			set
			{
				if (this.m_Stillborn != value)
				{
					this.m_Stillborn = value;
					this.NotifyPropertyChanged("Stillborn");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool ErythroblastosisFetalis
		{
			get { return this.m_ErythroblastosisFetalis; }
			set
			{
				if (this.m_ErythroblastosisFetalis != value)
				{
					this.m_ErythroblastosisFetalis = value;
					this.NotifyPropertyChanged("ErythroblastosisFetalis");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool NeonatalDeath
		{
			get { return this.m_NeonatalDeath; }
			set
			{
				if (this.m_NeonatalDeath != value)
				{
					this.m_NeonatalDeath = value;
					this.NotifyPropertyChanged("NeonatalDeath");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool TransferToNICU
		{
			get { return this.m_TransferToNICU; }
			set
			{
				if (this.m_TransferToNICU != value)
				{
					this.m_TransferToNICU = value;
					this.NotifyPropertyChanged("TransferToNICU");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool ViscidOrThickMeconium
		{
			get { return this.m_ViscidOrThickMeconium; }
			set
			{
				if (this.m_ViscidOrThickMeconium != value)
				{
					this.m_ViscidOrThickMeconium = value;
					this.NotifyPropertyChanged("ViscidOrThickMeconium");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool MultipleGestation
		{
			get { return this.m_MultipleGestation; }
			set
			{
				if (this.m_MultipleGestation != value)
				{
					this.m_MultipleGestation = value;
					this.NotifyPropertyChanged("MultipleGestation");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool OminousFHRTracing
		{
			get { return this.m_OminousFHRTracing; }
			set
			{
				if (this.m_OminousFHRTracing != value)
				{
					this.m_OminousFHRTracing = value;
					this.NotifyPropertyChanged("OminousFHRTracing");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool Prematurity
		{
			get { return this.m_Prematurity; }
			set
			{
				if (this.m_Prematurity != value)
				{
					this.m_Prematurity = value;
					this.NotifyPropertyChanged("Prematurity");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool IUGR
		{
			get { return this.m_IUGR; }
			set
			{
				if (this.m_IUGR != value)
				{
					this.m_IUGR = value;
					this.NotifyPropertyChanged("IUGR");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool ApgarLessThan7at5Min
		{
			get { return this.m_ApgarLessThan7at5Min; }
			set
			{
				if (this.m_ApgarLessThan7at5Min != value)
				{
					this.m_ApgarLessThan7at5Min = value;
					this.NotifyPropertyChanged("ApgarLessThan7at5Min");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool CordpHLessThan7dot10
		{
			get { return this.m_CordpHLessThan7dot10; }
			set
			{
				if (this.m_CordpHLessThan7dot10 != value)
				{
					this.m_CordpHLessThan7dot10 = value;
					this.NotifyPropertyChanged("CordpHLessThan7dot10");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool CongenitalAnomalies
		{
			get { return this.m_CongenitalAnomalies; }
			set
			{
				if (this.m_CongenitalAnomalies != value)
				{
					this.m_CongenitalAnomalies = value;
					this.NotifyPropertyChanged("CongenitalAnomalies");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool NeonatalSeizures
		{
			get { return this.m_NeonatalSeizures; }
			set
			{
				if (this.m_NeonatalSeizures != value)
				{
					this.m_NeonatalSeizures = value;
					this.NotifyPropertyChanged("NeonatalSeizures");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public string Other2
		{
			get { return this.m_Other2; }
			set
			{
				if (this.m_Other2 != value)
				{
					this.m_Other2 = value;
					this.NotifyPropertyChanged("Other2");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool Infarcts
		{
			get { return this.m_Infarcts; }
			set
			{
				if (this.m_Infarcts != value)
				{
					this.m_Infarcts = value;
					this.NotifyPropertyChanged("Infarcts");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool AbnormalCalcifications
		{
			get { return this.m_AbnormalCalcifications; }
			set
			{
				if (this.m_AbnormalCalcifications != value)
				{
					this.m_AbnormalCalcifications = value;
					this.NotifyPropertyChanged("AbnormalCalcifications");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool Abruption
		{
			get { return this.m_Abruption; }
			set
			{
				if (this.m_Abruption != value)
				{
					this.m_Abruption = value;
					this.NotifyPropertyChanged("Abruption");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool PlacentaPrevia
		{
			get { return this.m_PlacentaPrevia; }
			set
			{
				if (this.m_PlacentaPrevia != value)
				{
					this.m_PlacentaPrevia = value;
					this.NotifyPropertyChanged("PlacentaPrevia");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool VasaPrevia
		{
			get { return this.m_VasaPrevia; }
			set
			{
				if (this.m_VasaPrevia != value)
				{
					this.m_VasaPrevia = value;
					this.NotifyPropertyChanged("VasaPrevia");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool AbnormalCordAppearance
		{
			get { return this.m_AbnormalCordAppearance; }
			set
			{
				if (this.m_AbnormalCordAppearance != value)
				{
					this.m_AbnormalCordAppearance = value;
					this.NotifyPropertyChanged("AbnormalCordAppearance");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public bool Mass
		{
			get { return this.m_Mass; }
			set
			{
				if (this.m_Mass != value)
				{
					this.m_Mass = value;
					this.NotifyPropertyChanged("Mass");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public string Other3
		{
			get { return this.m_Other3; }
			set
			{
				if (this.m_Other3 != value)
				{
					this.m_Other3 = value;
					this.NotifyPropertyChanged("Other3");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public Nullable<DateTime> DateSubmitted
		{
			get { return this.m_DateSubmitted; }
			set
			{
				if (this.m_DateSubmitted != value)
				{
					this.m_DateSubmitted = value;
					this.NotifyPropertyChanged("DateSubmitted");
				}
			}
		}

		[PersistentProperty()]
		[DataMember]
		public string SubmittedBy
		{
			get { return this.m_SubmittedBy; }
			set
			{
				if (this.m_SubmittedBy != value)
				{
					this.m_SubmittedBy = value;
					this.NotifyPropertyChanged("SubmittedBy");
				}
			}
		}
	}
}
