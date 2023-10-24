using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.XPSDocument.Result.Data
{
    public class PlacentalPathologyQuestionnaireDataV2
    {
        private string m_DateFormat = "yyyy-MM-ddTHH:mm:ss.FFF";

        private string m_PFirstName;
        private string m_PLastName;

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

        public PlacentalPathologyQuestionnaireDataV2(ClientOrder.Model.ClientOrder clientOrder)
        {
            SetClientOrderValues(clientOrder);
            SetPlacentaDetailValues(clientOrder);
        }

        private void SetClientOrderValues(ClientOrder.Model.ClientOrder clientOrder)
        {
            this.m_PFirstName = clientOrder.PFirstName;
            this.m_PLastName = clientOrder.PLastName;
        }

        private void SetPlacentaDetailValues(ClientOrder.Model.ClientOrder clientOrder)
        {
            ClientOrder.Model.PlacentaClientOrderDetail placentaClientOrderDetail = this.GetPlacentaClientOrderDetail(clientOrder);
            if(placentaClientOrderDetail != null)
            {
                this.m_DateSubmitted = placentaClientOrderDetail.DateSubmitted.HasValue ? placentaClientOrderDetail.DateSubmitted.Value.ToString(this.m_DateFormat) : string.Empty;
                this.m_Birthdate = string.IsNullOrEmpty(placentaClientOrderDetail.Birthdate) == false ? placentaClientOrderDetail.Birthdate : string.Empty;
                this.m_BirthTime = string.IsNullOrEmpty(placentaClientOrderDetail.BirthTime) == false ? placentaClientOrderDetail.BirthTime : string.Empty;
                this.m_PlacentaDeliveryTime = string.IsNullOrEmpty(placentaClientOrderDetail.PlacentaDeliveryTime) == false ? placentaClientOrderDetail.PlacentaDeliveryTime : string.Empty;
                this.m_GestationalAge = string.IsNullOrEmpty(placentaClientOrderDetail.GestationalAge) == false ? placentaClientOrderDetail.GestationalAge : string.Empty;
                this.m_Gravida = string.IsNullOrEmpty(placentaClientOrderDetail.Gravida) == false ? placentaClientOrderDetail.Gravida : string.Empty;
                this.m_Para = string.IsNullOrEmpty(placentaClientOrderDetail.Para) == false ? placentaClientOrderDetail.Para : string.Empty;
                this.m_Apgar1Min = string.IsNullOrEmpty(placentaClientOrderDetail.Apgar1Min) == false ? placentaClientOrderDetail.Apgar1Min : string.Empty;
                this.m_Apgar5Min = string.IsNullOrEmpty(placentaClientOrderDetail.Apgar5Min) == false ? placentaClientOrderDetail.Apgar5Min : string.Empty;
                this.m_Apgar10Min = string.IsNullOrEmpty(placentaClientOrderDetail.Apgar10Min) == false ? placentaClientOrderDetail.Apgar10Min : string.Empty;
                this.m_InfantWeight = string.IsNullOrEmpty(placentaClientOrderDetail.InfantWeight) == false ? placentaClientOrderDetail.InfantWeight : string.Empty;
                this.m_SpecificQuestions = string.IsNullOrEmpty(placentaClientOrderDetail.SpecificQuestions) == false ? placentaClientOrderDetail.SpecificQuestions : string.Empty;
                this.m_OtherExam = string.IsNullOrEmpty(placentaClientOrderDetail.OtherExam) == false ? placentaClientOrderDetail.OtherExam : string.Empty;
                this.m_Other1 = string.IsNullOrEmpty(placentaClientOrderDetail.Other1) == false ? placentaClientOrderDetail.Other1 : string.Empty;
                this.m_Other2 = string.IsNullOrEmpty(placentaClientOrderDetail.Other2) == false ? placentaClientOrderDetail.Other2 : string.Empty;
                this.m_Other3 = string.IsNullOrEmpty(placentaClientOrderDetail.Other3) == false ? placentaClientOrderDetail.Other3 : string.Empty;
                this.m_SubmittedBy = string.IsNullOrEmpty(placentaClientOrderDetail.SubmittedBy) == false ? placentaClientOrderDetail.SubmittedBy : string.Empty;

                this.m_Abortion = placentaClientOrderDetail.Abortion;
                this.m_GrossExam = placentaClientOrderDetail.GrossExam;
                this.m_CompleteExam = placentaClientOrderDetail.CompleteExam;
                this.m_Cytogenetics = placentaClientOrderDetail.Cytogenetics;
                this.m_DiabetesMellitus = placentaClientOrderDetail.DiabetesMellitus;
                this.m_PregnancyInducedHypertension = placentaClientOrderDetail.PregnancyInducedHypertension;
                this.m_UnexplainedFever = placentaClientOrderDetail.UnexplainedFever;
                this.m_PrematureRuptureOfMembranes = placentaClientOrderDetail.PrematureRuptureOfMembranes;
                this.m_PoorOrLimitedPrenatalCare = placentaClientOrderDetail.PoorOrLimitedPrenatalCare;
                this.m_Polyhydramnios = placentaClientOrderDetail.Polyhydramnios;
                this.m_Oligohydramnios = placentaClientOrderDetail.Oligohydramnios;
                this.m_PretermDeliveryLessThan36Weeks = placentaClientOrderDetail.PretermDeliveryLessThan36Weeks;
                this.m_PostTermDeliveryMoreThan42Weeks = placentaClientOrderDetail.PostTermDeliveryMoreThan42Weeks;
                this.m_Infection = placentaClientOrderDetail.Infection;
                this.m_PostpartumHemorrhage = placentaClientOrderDetail.PostpartumHemorrhage;
                this.m_MaternalHistoryOfReproductiveFailure = placentaClientOrderDetail.MaternalHistoryOfReproductiveFailure;
                this.m_SeverePreeclampsia = placentaClientOrderDetail.SeverePreeclampsia;
                this.m_SuspectedDrugUse = placentaClientOrderDetail.SuspectedDrugUse;
                this.m_SuspectedInfection = placentaClientOrderDetail.SuspectedInfection;
                this.m_Stillborn = placentaClientOrderDetail.Stillborn;
                this.m_ErythroblastosisFetalis = placentaClientOrderDetail.ErythroblastosisFetalis;
                this.m_NeonatalDeath = placentaClientOrderDetail.NeonatalDeath;
                this.m_TransferToNICU = placentaClientOrderDetail.TransferToNICU;
                this.m_ViscidOrThickMeconium = placentaClientOrderDetail.ViscidOrThickMeconium;
                this.m_MultipleGestation = placentaClientOrderDetail.MultipleGestation;
                this.m_OminousFHRTracing = placentaClientOrderDetail.OminousFHRTracing;
                this.m_Prematurity = placentaClientOrderDetail.Prematurity;
                this.m_IUGR = placentaClientOrderDetail.IUGR;
                this.m_ApgarLessThan7at5Min = placentaClientOrderDetail.ApgarLessThan7at5Min;
                this.m_CordpHLessThan7dot10 = placentaClientOrderDetail.CordpHLessThan7dot10;
                this.m_CongenitalAnomalies = placentaClientOrderDetail.CongenitalAnomalies;
                this.m_NeonatalSeizures = placentaClientOrderDetail.NeonatalSeizures;
                this.m_Infarcts = placentaClientOrderDetail.Infarcts;
                this.m_AbnormalCalcifications = placentaClientOrderDetail.AbnormalCalcifications;
                this.m_Abruption = placentaClientOrderDetail.Abruption;
                this.m_PlacentaPrevia = placentaClientOrderDetail.PlacentaPrevia;
                this.m_VasaPrevia = placentaClientOrderDetail.VasaPrevia;
                this.m_AbnormalCordAppearance = placentaClientOrderDetail.AbnormalCordAppearance;
                this.m_Mass = placentaClientOrderDetail.Mass;
            }            
        }

        private ClientOrder.Model.PlacentaClientOrderDetail GetPlacentaClientOrderDetail(ClientOrder.Model.ClientOrder clientOrder)
        {
            ClientOrder.Model.PlacentaClientOrderDetail result = null;
            foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail in clientOrder.ClientOrderDetailCollection)
            {
                if (clientOrderDetail.OrderTypeCode == "PLCNT")
                {
                    result = (ClientOrder.Model.PlacentaClientOrderDetail)clientOrderDetail;
                    break;
                }
            }
            return result;
        }

        public string PFirstName
        {
            get { return this.m_PFirstName; }
        }

        public string PLastName
        {
            get { return this.m_PLastName; }
        }

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
