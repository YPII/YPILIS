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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "bit")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
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

		[DataMember]
		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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
