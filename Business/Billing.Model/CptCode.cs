using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CptCode
    {
        protected string m_Code;        
        protected string m_Description;
        protected FeeScheduleEnum m_FeeSchedule;
        protected bool m_HasTechnicalComponent;
        protected bool m_HasProfessionalComponent;
        protected string m_Modifier;
        protected bool m_IsBillable;
        protected string m_GCode;
        protected bool m_HasMedicareQuantityLimit;
        protected int m_MedicareQuantityLimit;
        protected CPTCodeTypeEnum m_CodeType;
          
        public CptCode()
        {
            this.m_HasMedicareQuantityLimit = false;            
        }

        [PersistentProperty()]
        public string Code
        {
            get { return this.m_Code; }
            set { this.m_Code = value; }
        }

        [PersistentProperty()]
        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }

        [PersistentProperty()]
        public string Modifier
        {
            get { return this.m_Modifier; }
            set { this.m_Modifier = value; }
        }

        [PersistentProperty()]
        public FeeScheduleEnum FeeSchedule
        {
            get { return this.m_FeeSchedule; }
            set { this.m_FeeSchedule = value; }
        }

        [PersistentProperty()]
        public bool HasTechnicalComponent
        {
            get { return this.m_HasTechnicalComponent; }
            set { this.m_HasTechnicalComponent = value; }
        }

        [PersistentProperty()]
        public bool HasProfessionalComponent
        {
            get { return this.m_HasProfessionalComponent; }
            set { this.m_HasProfessionalComponent = value; }
        }

        [PersistentProperty()]
        public CPTCodeTypeEnum CodeType
        {
            get { return this.m_CodeType; }
            set { this.m_CodeType = value; }
        }

        [PersistentProperty()]
        public bool IsBillable
        {
            get { return this.m_IsBillable; }
            set { this.m_IsBillable = value; }
        }

        [PersistentProperty()]
        public string GCode
        {
            get { return this.m_GCode; }
            set { this.m_GCode = value; }
        }

        [PersistentProperty()]
        public bool HasMedicareQuantityLimit
        {
            get { return this.m_HasMedicareQuantityLimit; }
            set { this.m_HasTechnicalComponent = value; }
        }

        [PersistentProperty()]
        public int MedicareQuantityLimit
        {
            get { return this.m_MedicareQuantityLimit; }
            set { this.m_MedicareQuantityLimit = value; }
        }

        public bool HasBillableProfessionalComponent()
        {
            bool result = true;
            if (this.Modifier == "26") result = true;
            if (string.IsNullOrEmpty(this.Modifier) == true)
            {
                if (this.m_HasProfessionalComponent == true)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool HasBillableTechnicalComponent()
        {
            bool result = false;
            if (this.Modifier == "TC") result = true;
            if (string.IsNullOrEmpty(this.Modifier) == true)
            {
                if (this.m_HasTechnicalComponent == true)
                {
                    result = true;
                }
            }
            return result;
        }

        public virtual string GetModifier(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent)
        {
            string result = null;
            switch (billingComponent)
            {
                case BillingComponentEnum.Professional:
                    if (this.m_HasTechnicalComponent == true && this.m_HasProfessionalComponent == true)
                    {
                        result = YellowstonePathology.Business.Billing.Model.CPTCodeModifier.TwentySix;
                    }
                    else if (this.m_HasTechnicalComponent == false && this.m_HasProfessionalComponent == true)
                    {
                        result = null;
                    }
                    else if (this.m_HasTechnicalComponent == true && this.m_HasProfessionalComponent == false)
                    {
                        throw new Exception("This code does not have a professional component.");
                    }
                    break;
                case BillingComponentEnum.Technical:
                    if (this.m_HasTechnicalComponent == true && this.m_HasProfessionalComponent == true)
                    {
                        result = YellowstonePathology.Business.Billing.Model.CPTCodeModifier.TechnicalComponent;
                    }
                    else if (this.m_HasTechnicalComponent == true && this.m_HasProfessionalComponent == false)
                    {
                        result = null;
                    }
                    else if (this.m_HasTechnicalComponent == false && this.m_HasProfessionalComponent == true)
                    {
                        throw new Exception("This code does not have a technical component.");
                    }
                    break;
                case BillingComponentEnum.Global:
                    result = null;
                    break;
            }
            return result;
        }

        public static CptCode Clone(CptCode cptCodeIn)
        {
            return (CptCode)cptCodeIn.MemberwiseClone();
        }
    }
}
