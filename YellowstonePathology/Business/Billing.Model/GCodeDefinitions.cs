using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model.GCodeDefinitions
{
    public class CPTG0416 : CptCode
    {
        public CPTG0416()
        {
            this.m_Code = "G0416";
            this.m_FeeSchedule = FeeScheduleEnum.Physician;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
        }
    }

    public class CPTG0123 : CptCode
    {
        public CPTG0123()
        {
            this.m_Code = "G0123";
            this.m_FeeSchedule = FeeScheduleEnum.Physician;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
        }
    }

    public class CPTG0124 : CptCode
    {
        public CPTG0124()
        {
            this.m_Code = "G0124";
            this.m_FeeSchedule = FeeScheduleEnum.Physician;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = false;
            this.m_IsBillable = true;
        }
    }

    public class CPTG0145 : CptCode
    {
        public CPTG0145()
        {
            this.m_Code = "G0145";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_SVHCDMCode = "760804033";
            this.m_SVHCDMDescription = "THIN PREP AUTO SCREEN";
        }
    }

    public class CPTG0461 : CptCode
    {
        public CPTG0461()
        {
            this.m_Code = "G0461";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
        }
    }

    public class CPTG0461TC : CptCode
    {
        public CPTG0461TC()
        {
            this.m_Code = "G0461";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_Modifier = "TC";
        }
    }

    public class CPTG0462 : CptCode
    {
        public CPTG0462()
        {
            this.m_Code = "G0462";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
        }
    }

    public class CPTG0462TC : CptCode
    {
        public CPTG0462TC()
        {
            this.m_Code = "G0462";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_Modifier = "TC";
        }
    }    
}
