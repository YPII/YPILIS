using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.ASCCPRule
{
    public class Woman
    {
        private string m_Name;
        private bool m_RuleIsMatch;
        private Business.Cytology.Model.OrderType m_OrderType;
        private int m_Age;        
        private Business.Cytology.Model.ScreeningImpression m_ScreeningImpression;
        private Business.Cytology.Model.SpecimenAdequacy m_SpecimenAdequacy;
        private bool m_ECTZAbsent;
        private bool m_Reactive;
        private bool m_PerformHPV;
        private string m_HPVResult;
        private bool m_ReflexToHPVGenotypes;
        private string m_HPV16Result;
        private string m_HPV18Result;

        private string m_ManagementRecommendation;
        private string m_Reminder;
        private string m_RuleName;

        public Woman()
        {

        }

        public void FromMask(string mask)
        {

        }
        
        public void FromAccessionOrder(Business.Test.AccessionOrder accessionOrder)
        {
            Business.Cytology.Model.OrderTypeCollection orderTypeCollection = new Cytology.Model.OrderTypeCollection();
            Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womansHealthProfileTestOrder = (Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)accessionOrder.PanelSetOrderCollection.GetWomensHealthProfile();
            Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology = (Business.Test.ThinPrepPap.PanelSetOrderCytology)accessionOrder.PanelSetOrderCollection.GetThinPrepPap();
            Business.Cytology.Model.ScreeningImpressionCollection screeningImpressions = Business.Gateway.AccessionOrderGateway.GetScreeningImpressions();
            Business.Cytology.Model.SpecimenAdequacyCollection specimenAdequacies = Business.Gateway.AccessionOrderGateway.GetSpecimenAdequacy();

            this.m_Name = accessionOrder.PatientDisplayName;
            this.m_OrderType = orderTypeCollection.Get(womansHealthProfileTestOrder);
            this.m_Age = YellowstonePathology.Business.Helper.PatientHelper.GetAge(accessionOrder.PBirthdate.Value);
            if(string.IsNullOrEmpty(panelSetOrderCytology.ResultCode) == false) this.m_ScreeningImpression = screeningImpressions.Get(panelSetOrderCytology.ResultCode);

            if (string.IsNullOrEmpty(panelSetOrderCytology.ResultCode) == false)
            {
                this.m_SpecimenAdequacy = specimenAdequacies.GetFromPAPResultCode(panelSetOrderCytology.ResultCode);
                this.m_Reactive = Business.Cytology.Model.CytologyResultCode.IsResultCodeReactive(panelSetOrderCytology.ResultCode);
                this.m_ECTZAbsent = Business.Cytology.Model.CytologyResultCode.IsResultCodeTZoneAbsent(panelSetOrderCytology.ResultCode);
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(14) == true)
            {
                Business.Test.HPV.HPVTestOrder hpvTestOrder = (Business.Test.HPV.HPVTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(14);
                this.m_HPVResult = hpvTestOrder.Result;
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(62) == true)
            {
                Business.Test.HPV1618.PanelSetOrderHPV1618 hpv1618TestOrder = (Business.Test.HPV1618.PanelSetOrderHPV1618)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(62);
                this.m_HPV16Result = hpv1618TestOrder.HPV16Result;
                this.m_HPV18Result = hpv1618TestOrder.HPV18Result;
            }

            if (this.OrderType != null && this.OrderType.OrderCode == "11")
            {
                this.m_PerformHPV = true;
                if(accessionOrder.PanelSetOrderCollection.Exists(14) == false)
                {
                    this.m_Reminder = "An HPV is required and has not been ordered.";
                }
            }
        }
                
        public bool Cotesting
        {
            get
            {
                if(this.m_OrderType.OrderCode == "10")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }            
        }          

        public bool RuleIsMatch
        {
            get { return this.m_RuleIsMatch; }
            set { this.m_RuleIsMatch = value; }
        }

        public Business.Cytology.Model.OrderType OrderType
        {
            get { return this.m_OrderType; }
            set { this.m_OrderType = value; }
        }

        public string Name
        {
            get { return this.m_Name; }
            set { this.m_Name = value; }
        }

        public int Age
        {
            get { return this.m_Age; }
            set { this.m_Age = value; }
        }

        public bool Reactive
        {
            get { return this.m_Reactive; }
            set { this.m_Reactive = value; }
        }

        public bool ECTZAbsent
        {
            get { return this.m_ECTZAbsent; }
            set { this.m_ECTZAbsent = value; }
        }

        public bool PerformHPV
        {
            get { return this.m_PerformHPV; }
            set { this.m_PerformHPV = value; }
        }

        public string HPVResult
        {
            get { return this.m_HPVResult; }
            set { this.m_HPVResult = value; }
        }

        public bool ReflexToHPVGenotypes
        {
            get { return this.m_ReflexToHPVGenotypes; }
            set { this.m_ReflexToHPVGenotypes = value; }
        }

        public string HPV16Result
        {
            get { return this.m_HPV16Result; }
            set { this.m_HPV16Result = value; }
        }

        public string HPV18Result
        {
            get { return this.m_HPV18Result; }
            set { this.m_HPV18Result = value; }
        }

        public Business.Cytology.Model.ScreeningImpression ScreeningImpression
        {
            get { return this.m_ScreeningImpression; }
            set { this.m_ScreeningImpression = value; }
        }

        public Business.Cytology.Model.SpecimenAdequacy SpecimenAdequacy
        {
            get { return this.m_SpecimenAdequacy; }
            set { this.m_SpecimenAdequacy = value; }
        }

        public string ManagementRecommendation
        {
            get { return this.m_ManagementRecommendation; }
            set { this.m_ManagementRecommendation = value; }
        }

        public string Reminder
        {
            get { return this.m_Reminder; }
            set { this.m_Reminder = value; }
        }

        public string RuleName
        {
            get { return this.m_RuleName; }
            set { this.m_RuleName = value; }
        }

        public bool IsGenotypesPositive()
        {
            bool result = false;
            if(this.m_HPV16Result == "Positive" || this.m_HPV18Result == "Positive")
            {
                result = true;
            }
            return result;
        }

    }
}
