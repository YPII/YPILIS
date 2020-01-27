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
        private bool m_Reactive;
        private bool m_PerformHPV;
        private string m_HPVResult;
        private bool m_ReflexToHPVGenotypes;
        private string m_GenotypingResult;
        private string m_ManagementRecomendation;        

        public Woman()
        {
            this.m_HPVResult = "Unknown";
            this.m_GenotypingResult = "Unknown";
            this.m_ManagementRecomendation = "Unknown";
        }

        public Woman(string name, Business.Cytology.Model.OrderType orderType, int age, Business.Cytology.Model.SpecimenAdequacy specimenAdequacy, 
            Business.Cytology.Model.ScreeningImpression screeningImpression, bool reactive)
        {
            this.m_Name = name;
            this.m_OrderType = orderType;
            this.m_Age = age;
            this.m_ScreeningImpression = screeningImpression;
            this.m_SpecimenAdequacy = specimenAdequacy;
            this.m_Reactive = reactive;            
            this.m_ManagementRecomendation = "Unknown";            
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

        public string GenotypingResult
        {
            get { return this.m_GenotypingResult; }
            set { this.m_GenotypingResult = value; }
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

        public string ManagementRecomendation
        {
            get { return this.m_ManagementRecomendation; }
            set { this.m_ManagementRecomendation = value; }
        }
    }
}
