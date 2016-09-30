using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model.CptCodeDefinition
{
    public class AutopsyBlock : CptCode
    {
        public AutopsyBlock()
        {
            this.m_Code = "AUTOPSYBLOCK";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
            this.m_Modifier = "TC";
        }
    }

    public class CPT81288 : CptCode
    {
        public CPT81288()
        {
            this.m_Code = "81288";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT81246 : CptCode
    {
        public CPT81246()
        {
            this.m_Code = "81246";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT88120 : CptCode
    {        
        public CPT88120()
        {
            this.m_Code = "88120";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760803367";
            this.m_SVHCDMDescription = "CYTP URNE 3-5 PROBES EA SPEC";
        }
    }

    public class CPT88369 : CptCode
    {
        public CPT88369()
        {
            this.m_Code = "88369";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760806281";
            this.m_SVHCDMDescription = "M/PHMTRC ANALYS ISH QUANT/SEMIQ MANUAL EACH ADDTL";
        }
    }

    public class CPT83540 : CptCode
    {
        public CPT83540()
        {
            this.m_Code = "83540";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
        }
    }

    public class CPT88373 : CptCode
    {
        public CPT88373()
        {
            this.m_Code = "88373";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760806282";
            this.m_SVHCDMDescription = "M/PHMTRC ANALYS ISH QUANT/SEMIQ COMPUTER-ASSIST ";
        }
    }

    public class CPT88374 : CptCode
    {
        public CPT88374()
        {
            this.m_Code = "88374";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760806283";
            this.m_SVHCDMDescription = "M/PHMTRC ANALYS ISH QUANT/SEMIQ COMPUTR-ASSIST EA";
        }
    }

    public class CPT88341 : CptCode
    {
        public CPT88341()
        {
            this.m_Code = "88341";
            this.m_FeeSchedule = FeeScheduleEnum.Physician;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760806277";
            this.m_SVHCDMDescription = "IMMUNOHISTO AB SLIDE; PER SPEC; SINGLE STAIN";
        }
    }

    public class CPT88346 : CptCode
    {
        public CPT88346()
        {            
            this.m_Code = "88346";
            this.m_FeeSchedule = FeeScheduleEnum.Physician;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
        }
    }

    public class CPT88347 : CptCode
    {
        public CPT88347()
        {
            this.m_Code = "88347";
            this.m_FeeSchedule = FeeScheduleEnum.Physician;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
        }
    }

    public class CPT88348 : CptCode
    {
        public CPT88348()
        {
            this.m_Code = "88348";
            this.m_FeeSchedule = FeeScheduleEnum.Physician;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
        }
    }

    public class CPT87624 : CptCode
    {
        public CPT87624()
        {
            this.m_Code = "87624";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT87661 : CptCode
    {
        public CPT87661()
        {
            this.m_Code = "87661";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT87625 : CptCode
    {
        public CPT87625()
        {
            this.m_Code = "87625";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT88361 : CptCode
    {
        public CPT88361()
        {
            this.m_Code = "88361";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT88327 : CptCode
    {
        public CPT88327()
        {
            this.m_Code = "88327";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT84179 : CptCode
    {
        public CPT84179()
        {
            this.m_Code = "84179";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT81321 : CptCode
    {
        public CPT81321()
        {
            this.m_Code = "81321";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT88285 : CptCode
    {
        public CPT88285()
        {
            this.m_Code = "88285";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT81287 : CptCode
    {
        public CPT81287()
        {
            this.m_Code = "81287";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT81407 : CptCode
    {
        public CPT81407()
        {
            this.m_Code = "81407";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
            this.m_SVHCDMCode = "760806446";
            this.m_SVHCDMDescription = "MOLECULAR PATHOLOGY PROC LEVEL 8";
        }
    }

    public class CPT81406 : CptCode
    {
        public CPT81406()
        {
            this.m_Code = "81406";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT81401 : CptCode
    {
        public CPT81401()
        {
            this.m_Code = "81401";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
            this.m_SVHCDMCode = "760805571";
            this.m_SVHCDMDescription = "MOLECULAR PATHOLOGY PROC LEVEL 2";
        }
    }

    public class CPT81301 : CptCode
    {
        public CPT81301()
        {
            this.m_Code = "81301";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT88261 : CptCode
    {
        public CPT88261()
        {
            this.m_Code = "88261";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT81263 : CptCode
    {
        public CPT81263()
        {
            this.m_Code = "81263";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
            this.m_SVHCDMCode = "760805684";
            this.m_SVHCDMDescription = "HIGH VARI REGIONAL MUTATION";
        }
    }

    public class CPT81445 : CptCode
    {
        public CPT81445()
        {
            this.m_Code = "81445";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT81342 : CptCode
    {
        public CPT81342()
        {
            this.m_Code = "81342";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
            this.m_SVHCDMCode = "760805723";
            this.m_SVHCDMDescription = "TRG GENE REARRANGEMENT ANAL";
        }
    }

    public class CPT88264 : CptCode    
    {
        public CPT88264()
        {
            this.m_Code = "88264";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
            this.m_SVHCDMCode = "760804017";
            this.m_SVHCDMDescription = "CHROMOSOME ANALYSIS 20 25";
        }
    }

    public class CPT81207 : CptCode
    {
        public CPT81207()
        {
            this.m_Code = "81207";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
        }
    }

    public class CPT81206 : CptCode
    {
        public CPT81206()
        {
            this.m_Code = "81206";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
        }
    }

    public class CPT81404 : CptCode
    {
        public CPT81404()
        {
            this.m_Code = "81404";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
            this.m_SVHCDMCode = "760805647";
            this.m_SVHCDMDescription = "MOLECULAR PATHOLOGY PROC LEVEL 5";
        }
    }

    public class CPT81310 : CptCode
    {
        public CPT81310()
        {
            this.m_Code = "81310";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
        }
    }

    public class CPT81402 : CptCode
    {
        public CPT81402()
        {
            this.m_Code = "81402";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
            this.m_SVHCDMCode = "760805645";
            this.m_SVHCDMDescription = "MOLECULAR PATHOLOGY PROC LEVEL 3";
        }
    }

    public class CPT81403 : CptCode 
    {
        public CPT81403()
        {
            this.m_Code = "81403";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
            this.m_SVHCDMCode = "760805646";
            this.m_SVHCDMDescription = "MOLECULAR PATHOLOGY PROC LEVEL 4";
        }
    }

    public class CPT81245 : CptCode
    {
        public CPT81245()
        {
            this.m_Code = "81245";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760805629";
            this.m_SVHCDMDescription = "FLT3 GENE";
        }
    }

    public class CPT81450 : CptCode
    {
        public CPT81450()
        {
            this.m_Code = "81450";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
        }
    }

    public class CPT81455 : CptCode
    {
        public CPT81455()
        {
            this.m_Code = "81455";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
        }
    }

    public class CPT81315 : CptCode
    {
        public CPT81315()
        {
            this.m_Code = "81315";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
        }
    }

    public class CPT81479 : CptCode
    {
        public CPT81479()
        {
            this.m_Code = "81479";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT83890 : CptCode
    {
        public CPT83890()
        {
            this.m_Code = "83890";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT83891 : CptCode
    {
        public CPT83891()
        {
            this.m_Code = "83891";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }


    public class CPT83892 : CptCode
    {
        public CPT83892()
        {
            this.m_Code = "83892";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT83896 : CptCode
    {
        public CPT83896()
        {
            this.m_Code = "83896";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT83898 : CptCode
    {
        public CPT83898()
        {
            this.m_Code = "83898";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
            this.m_IsBillable = true;
        }
    }

    public class CPT83900 : CptCode
    {
        public CPT83900()
        {
            this.m_Code = "83900";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT83901 : CptCode
    {
        public CPT83901()
        {
            this.m_Code = "83901";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT83902 : CptCode
    {
        public CPT83902()
        {
            this.m_Code = "83902";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT83903 : CptCode
    {
        public CPT83903()
        {
            this.m_Code = "83903";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.PQRS;
        }
    }

    public class CPT83907 : CptCode
    {
        public CPT83907()
        {
            this.m_Code = "83907";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT83909 : CptCode
    {
        public CPT83909()
        {
            this.m_Code = "83901";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT83914 : CptCode
    {
        public CPT83914()
        {
            this.m_Code = "83914";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }


    public class CPT85055 : CptCode
	{
		public CPT85055()
		{
			this.m_Code = "85055";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
		}
	}

	public class CPT85060 : CptCode
	{
		public CPT85060()
		{
			this.m_Code = "85060";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = false;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.ProfessionalOnly;
		}
	}

    public class CPT86360 : CptCode
    {
        public CPT86360()
        {
            this.m_Code = "86360";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT86361 : CptCode
    {
        public CPT86361()
        {
            this.m_Code = "86361";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT87169 : CptCode
    {
        public CPT87169()
        {
            this.m_Code = "87169";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT88164 : CptCode
    {
        public CPT88164()
        {
            this.m_Code = "88164";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT89060 : CptCode
    {
        public CPT89060()
        {
            this.m_Code = "89060";
            this.m_FeeSchedule = FeeScheduleEnum.Physician;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = false;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.ProfessionalOnly;
        }
    }

	public class CPT85097 : CptCode
	{
		public CPT85097()
		{
			this.m_Code = "85097";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = false;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.ProfessionalOnly;
		}
	}

	public class CPT86023 : CptCode
	{
		public CPT86023()
		{
			this.m_Code = "86023";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
		}
	}	

	public class CPT86367 : CptCode
	{
		public CPT86367()
		{
			this.m_Code = "86367";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
		}
	}
	
	public class CPT87491 : CptCode
	{
		public CPT87491()
		{
			this.m_Code = "87491";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
		}
	}

	public class CPT87591 : CptCode
	{
		public CPT87591()
		{
			this.m_Code = "87591";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
		}
	}

	public class CPT87621 : CptCode
	{
		public CPT87621()
		{
			this.m_Code = "87621";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
		}
	}

	public class CPT88104 : CptCode
	{
		public CPT88104()
		{
			this.m_Code = "88104";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760805885";
            this.m_SVHCDMDescription = "SVHR-CYTOPATH FLUIDS NONGYN SMEARS";
		}
	}

	public class CPT88108 : CptCode
	{
		public CPT88108()
		{
			this.m_Code = "88108";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
		}
	}

	public class CPT88112 : CptCode
	{
		public CPT88112()
		{
			this.m_Code = "88112";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760402247";
            this.m_SVHCDMDescription = "CYTOPATH CELL ENHANCE TECH";
		}
	}

	public class CPT88141 : CptCode
	{
		public CPT88141()
		{
			this.m_Code = "88141";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = false;
            this.m_IsBillable = true;
            this.m_GCode = "G0124";
            this.m_CodeType = CPTCodeTypeEnum.ProfessionalOnly;
		}
	}

	public class CPT88142 : CptCode
	{
		public CPT88142()
		{
			this.m_Code = "88142";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_GCode = "G0123";
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
		}
	}

	public class CPT88155 : CptCode
	{
		public CPT88155()
		{
			this.m_Code = "88155";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
		}
	}

	public class CPT88160 : CptCode
	{
		public CPT88160()
		{
			this.m_Code = "88160";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
		}
	}

	public class CPT88161 : CptCode
	{
		public CPT88161()
		{
			this.m_Code = "88161";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760402250";
            this.m_SVHCDMDescription = "TOUCH PREP";
		}
	}	

	public class CPT88173 : CptCode
	{
		public CPT88173()
		{
			this.m_Code = "88173";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760402253";
            this.m_SVHCDMDescription = "EVAL OF FNA; INTERP & REPORT";
        }
    }

	public class CPT88175 : CptCode
	{
		public CPT88175()
		{
			this.m_Code = "88175";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_GCode = "G0145";
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
            this.m_SVHCDMCode = "760403862";
            this.m_SVHCDMDescription = "THIN PREP AUTO DIAGNOSTIC";
        }
    }

    public class CPT88177 : CptCode
    {
        public CPT88177()
        {
            this.m_Code = "88177";
            this.m_FeeSchedule = FeeScheduleEnum.Physician;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
        }
    }

	public class CPT88182 : CptCode
	{
		public CPT88182()
		{
			this.m_Code = "88182";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
		}
	}

	public class CPT88184 : CptCode
	{
		public CPT88184()
		{
			this.m_Code = "88184";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
		}
	}

	public class CPT88185 : CptCode
	{
		public CPT88185()
		{
			this.m_Code = "88185";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
		}
	}

	public class CPT88187 : CptCode
	{
		public CPT88187()
		{
			this.m_Code = "88187";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = false;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.ProfessionalOnly;
		}
	}

	public class CPT88188 : CptCode
	{
		public CPT88188()
		{
			this.m_Code = "88188";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = false;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.ProfessionalOnly;
		}
	}

	public class CPT88189 : CptCode
	{
		public CPT88189()
		{
			this.m_Code = "88189";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = false;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.ProfessionalOnly;
		}
	}

    public class CPT88230 : CptCode
    {
        public CPT88230()
        {
            this.m_Code = "88230";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT88237 : CptCode
	{
		public CPT88237()
		{
			this.m_Code = "88237";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
			this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
            this.m_SVHCDMCode = "760802264";
            this.m_SVHCDMDescription = "TISSUE CULTURE BLOOD CELSS";
		}
	}

    public class CPT88239 : CptCode
    {
        public CPT88239()
        {
            this.m_Code = "88239";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
            this.m_SVHCDMCode = "760802727";
            this.m_SVHCDMDescription = "TISSUE CULTRE SOLID TUMOR";
        }
    }

    public class CPT88262 : CptCode
	{
		public CPT88262()
		{
			this.m_Code = "88262";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
			this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
            this.m_SVHCDMCode = "760803848";
            this.m_SVHCDMDescription = "CHROMOSOME ANALYSIS; 15-20 CELLS";
        }
    }

    public class CPT88233 : CptCode
    {
        public CPT88233()
        {
            this.m_Code = "88233";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
            this.m_SVHCDMCode = "760802263";
            this.m_SVHCDMDescription = "CULTURE TISSUE SKIN";
        }
    }

	public class CPT88280 : CptCode
	{
		public CPT88280()
		{
			this.m_Code = "88280";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
			this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
		}
	}

	public class CPT88291 : CptCode
	{
		public CPT88291()
		{
			this.m_Code = "88291";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = false;
			this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.ProfessionalOnly;
		}
	}

	public class CPT88300 : CptCode
	{
		public CPT88300()
		{
			this.m_Code = "88300";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760402279";
            this.m_SVHCDMDescription = "SURG PATH LEVEL-I GROSS EXAM ONLY";
		}
	}

	public class CPT88302 : CptCode
	{
		public CPT88302()
		{
			this.m_Code = "88302";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760402280";
            this.m_SVHCDMDescription = "LEVEL II SURG PATH GROSS/MICRO";
        }
    }

	public class CPT88304 : CptCode
	{
		public CPT88304()
		{
			this.m_Code = "88304";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760402281";
            this.m_SVHCDMDescription = "SURG PATH LEVEL-III GROSS & MICRO";
        }
    }

	public class CPT88305 : CptCode
	{
		public CPT88305()
		{
			this.m_Code = "88305";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760402282";
            this.m_SVHCDMDescription = "SURG PATH LEVEL-IV GROSS & MICRO";
        }
    }

	public class CPT88307 : CptCode
	{
		public CPT88307()
		{
			this.m_Code = "88307";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760402283";
            this.m_SVHCDMDescription = "SURG PATH LEVEL-V GROSS & MICRO";
        }
    }

	public class CPT88309 : CptCode
	{
		public CPT88309()
		{
			this.m_Code = "88309";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760402284";
            this.m_SVHCDMDescription = "SURG PATH LEVEL-VI GROSS & MICRO";
        }
    }

	public class CPT88311 : CptCode
	{
		public CPT88311()
		{
			this.m_Code = "88311";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760402285";
            this.m_SVHCDMDescription = " DECALCIFICATION PROCEDURE";
        }
    }

	public class CPT88312 : CptCode
	{
		public CPT88312()
		{
			this.m_Code = "88312";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760402286";
            this.m_SVHCDMDescription = "SPECIAL STAINS; GROUP 1, EACH";
        }
    }

	public class CPT88313 : CptCode
	{
		public CPT88313()
		{
			this.m_Code = "88313";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760402287";
            this.m_SVHCDMDescription = "SPECIAL STAINS; GROUP 2, EACH";
        }
    }

    public class CPT88314 : CptCode
    {
        public CPT88314()
        {
            this.m_Code = "88314";
            this.m_FeeSchedule = FeeScheduleEnum.Physician;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
        }
    }

    public class CPT88319 : CptCode
    {
        public CPT88319()
        {
            this.m_Code = "88319";
            this.m_FeeSchedule = FeeScheduleEnum.Physician;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
        }
    }

    public class CPT88321 : CptCode
	{
		public CPT88321()
		{
			this.m_Code = "88321";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = false;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.ProfessionalOnly;
		}
	}

	public class CPT88323 : CptCode
	{
		public CPT88323()
		{
			this.m_Code = "88323";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
		}
	}

	public class CPT88325 : CptCode
	{
		public CPT88325()
		{
			this.m_Code = "88325";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = false;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.ProfessionalOnly;
		}
	}

	public class CPT88329 : CptCode
	{
		public CPT88329()
		{
			this.m_Code = "88329";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = false;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.ProfessionalOnly;
		}
	}

	public class CPT88331 : CptCode
	{
		public CPT88331()
		{
			this.m_Code = "88331";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760402292";
            this.m_SVHCDMDescription = "TISSUE 1ST FROZEN SECTION EXAM";
        }
    }

	public class CPT88332 : CptCode
	{
		public CPT88332()
		{
			this.m_Code = "88332";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760402293";
            this.m_SVHCDMDescription = "TISSUE ADDL FROZEN SECT EXAM";
        }
    }

	public class CPT88333 : CptCode
	{
		public CPT88333()
		{
			this.m_Code = "88333";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
		}
	}

	public class CPT88334 : CptCode
	{
		public CPT88334()
		{
			this.m_Code = "88334";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
		}
	}

	public class CPT88342 : CptCode
	{
		public CPT88342()
		{
			this.m_Code = "88342";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760402296";
            this.m_SVHCDMDescription = " IMMUNO; PER SPEC INIT SGLE AB";
        }
    }

    public class CPT88364 : CptCode
    {
        public CPT88364()
        {
            this.m_Code = "88364";
            this.m_FeeSchedule = FeeScheduleEnum.Physician;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
        }
    }

    public class CPT88343 : CptCode
	{
		public CPT88343()
		{
			this.m_Code = "88343";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
			this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760805981";
            this.m_SVHCDMDescription = "IMMUNOHISTO/CYTO CHEM, EACH ADDITIONAL AB";
        }
    }

	public class CPT88360 : CptCode
	{
		public CPT88360()
		{
			this.m_Code = "88360";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760402732";
            this.m_SVHCDMDescription = "TUMOR IMM MAN PER SPEC/EA AB";
        }
    }

    public class CPT88365 : CptCode
    {
        public CPT88365()
        {
            this.m_Code = "88365";
            this.m_FeeSchedule = FeeScheduleEnum.Physician;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760802301";
            this.m_SVHCDMDescription = "TISSUE ISH SNGL PROBE PER SPEC";
        }
    }

    public class CPT88367 : CptCode
    {
        public CPT88367()
        {
            this.m_Code = "88367";
            this.m_FeeSchedule = FeeScheduleEnum.Physician;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_HasMedicareQuantityLimit = true;
            this.m_MedicareQuantityLimit = 1;
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760802268";
            this.m_SVHCDMDescription = "CHROMOSOME ANALYSIS TISSUE";
        }
    }

	public class CPT88368 : CptCode
	{
		public CPT88368()
		{
			this.m_Code = "88368";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;            
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_HasMedicareQuantityLimit = true;
            this.m_MedicareQuantityLimit = 1;
            this.m_CodeType = CPTCodeTypeEnum.Global;
		}
	}

    public class CPT88377 : CptCode
    {
        public CPT88377()
        {
            this.m_Code = "88377";
            this.m_FeeSchedule = FeeScheduleEnum.Physician;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_HasMedicareQuantityLimit = false;            
            this.m_CodeType = CPTCodeTypeEnum.Global;
            this.m_SVHCDMCode = "760806284";
            this.m_SVHCDMDescription = "M/PHMTRC ANALYS ISH QUANT/SEMIQ; MANUAL EA MULTIPLEX PROBE";
        }
    }

	public class CPT99000 : CptCode
	{
		public CPT99000()
		{
			this.m_Code = "99000";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
		}
	}		

	public class CPT86356 : CptCode
	{
		public CPT86356()
		{
			this.m_Code = "86356";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
		}
	}

	public class CPT88363 : CptCode
	{
		public CPT88363()
		{
			this.m_Code = "88363";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = false;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.ProfessionalOnly;
		}
	}	

	public class CPT81270 : CptCode
	{
		public CPT81270()
		{
			this.m_Code = "81270";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
            this.m_SVHCDMCode = "760805576";
            this.m_SVHCDMDescription = "JAK2 GENE";
        }
    }

	public class CPT81220 : CptCode
	{
		public CPT81220()
		{
			this.m_Code = "81220";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
		}
	}

	public class CPT81275 : CptCode
	{
		public CPT81275()
		{
			this.m_Code = "81275";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
            this.m_SVHCDMCode = "760805634";
            this.m_SVHCDMDescription = "KRAS GENE";
        }
    }

	public class CPT81210 : CptCode
	{
		public CPT81210()
		{
			this.m_Code = "81210";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
            this.m_SVHCDMCode = "760805628";
            this.m_SVHCDMDescription = "BRAF GENE";
        }
    }

	public class CPT81240 : CptCode
	{
		public CPT81240()
		{
			this.m_Code = "81240";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
		}
	}

	public class CPT81241 : CptCode
	{
		public CPT81241()
		{
			this.m_Code = "81241";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
		}
	}	

	public class CPT81261 : CptCode
	{
		public CPT81261()
		{
			this.m_Code = "81261";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
		}
	}

    public class CPT81264 : CptCode
    {
        public CPT81264()
        {
            this.m_Code = "81264";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

    public class CPT8127026 : CptCode
	{
		public CPT8127026()
		{
			this.m_Code = "81270";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_Modifier = "26";
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
		}
	}

	public class CPT8122026 : CptCode
	{
		public CPT8122026()
		{
			this.m_Code = "81220";
            this.m_Modifier = "26";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
		}
	}

	public class CPT8127526 : CptCode
	{
		public CPT8127526()
		{
			this.m_Code = "81275";
            this.m_Modifier = "26";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
		}
	}

	public class CPT8121026 : CptCode
	{
		public CPT8121026()
		{
			this.m_Code = "81210";
            this.m_Modifier = "26";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
		}
	}

	public class CPT8124026 : CptCode
	{
		public CPT8124026()
		{
			this.m_Code = "81240";
            this.m_Modifier = "26";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
		}
	}

	public class CPT8124126 : CptCode
	{
		public CPT8124126()
		{
			this.m_Code = "81241";
            this.m_Modifier = "26";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
		}
	}

	public class CPT8129126 : CptCode
	{
		public CPT8129126()
		{
			this.m_Code = "8129126";
            this.m_Modifier = "26";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
		}
	}

	public class CPT8126126 : CptCode
	{
		public CPT8126126()
		{
			this.m_Code = "81261";
            this.m_Modifier = "26";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
		}
	}			

	public class CPT88172 : CptCode
	{
		public CPT88172()
		{
			this.m_Code = "88172";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
		}
	}		

	public class CPT88360TC : CptCode
	{
		public CPT88360TC()
		{
			this.m_Code = "88360";
            this.m_Modifier = "TC";
			this.m_FeeSchedule = FeeScheduleEnum.Physician;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
		}
	}

	public class CPT88313TC : CptCode
	{
		public CPT88313TC()
		{
			this.m_Code = "88313";
            this.m_Modifier = "TC";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
		}
	}

	public class CPT88342TC : CptCode
	{
		public CPT88342TC()
		{
			this.m_Code = "88342";
            this.m_Modifier = "TC";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
		}
	}

	public class CPT88343TC : CptCode
	{
		public CPT88343TC()
		{
			this.m_Code = "88343";
			this.m_Modifier = "TC";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
			this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
		}
	}

	public class CPT88312TC : CptCode
	{
		public CPT88312TC()
		{
			this.m_Code = "88312";
            this.m_Modifier = "TC";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
		}
	}

    public class CPT81235 : CptCode
    {
        public CPT81235()
        {
            this.m_Code = "81235";            
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
        }
    }

    public class CPT8123526 : CptCode
    {
        public CPT8123526()
        {
            this.m_Code = "81235";
            this.m_Modifier = "26";
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = true;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.Global;
        }
    }

    public class CPT87798 : CptCode
    {
        public CPT87798()
        {
            this.m_Code = "87798";            
            this.m_FeeSchedule = FeeScheduleEnum.Clinical;
            this.m_HasProfessionalComponent = false;
            this.m_HasTechnicalComponent = true;
            this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.TechnicalOnly;
        }
    }

	public class CPT81291 : CptCode
	{
		public CPT81291()
		{
			this.m_Code = "81291";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = true;
			this.m_HasTechnicalComponent = false;
			this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.ProfessionalOnly;
		}
	}
	
	public class CPT81170 : CptCode
	{
		public CPT81170()
		{
			this.m_Code = "81170";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
			this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.ProfessionalOnly;
		}
	}
		
	public class CPT81219 : CptCode
	{
		public CPT81219()
		{
			this.m_Code = "81219";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
			this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.ProfessionalOnly;
		}
	}
		
	public class CPT81218 : CptCode
	{
		public CPT81218()
		{
			this.m_Code = "81218";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
			this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.ProfessionalOnly;
		}
	}
			
	public class CPT81276 : CptCode
	{
		public CPT81276()
		{
			this.m_Code = "81276";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
			this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.ProfessionalOnly;
		}
	}
			
	public class CPT81311 : CptCode
	{
		public CPT81311()
		{
			this.m_Code = "81311";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
			this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.ProfessionalOnly;
		}
	}
	
	public class CPT81314 : CptCode
	{
		public CPT81314()
		{
			this.m_Code = "81314";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
			this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.ProfessionalOnly;
		}
	}	
		
	public class CPT81272 : CptCode
	{
		public CPT81272()
		{
			this.m_Code = "81272";
			this.m_FeeSchedule = FeeScheduleEnum.Clinical;
			this.m_HasProfessionalComponent = false;
			this.m_HasTechnicalComponent = true;
			this.m_IsBillable = true;
            this.m_CodeType = CPTCodeTypeEnum.ProfessionalOnly;
		}
	}	
}
