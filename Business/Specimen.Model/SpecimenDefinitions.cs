using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model.SpecimenDefinition
{
    public class NullSpecimen : Specimen
    {
        public NullSpecimen()
        {
            this.m_SpecimenId = null;
            this.m_SpecimenName = null;
            this.m_Description = null;
        }
    }    

    public class ThinPrepFluid : Specimen
    {
        public ThinPrepFluid()
        {
            this.m_SpecimenId = "SPCMNTHNPRPFLD";
            this.m_SpecimenName = "Thin Prep Fluid";
            this.m_Description = "Thin Prep Fluid";
            this.m_LabFixation = "PreservCyt";
            this.m_ClientFixation = "PreservCyt";
            this.m_RequiresGrossExamination = false;
        }        
    }

    public class GIBiopsy : Specimen
    {
        public GIBiopsy()
        {
            this.m_SpecimenId = "GISPCMN";
            this.m_SpecimenName = "GI Biopsy";
            this.m_Description = null;
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = "Formalin";
            this.m_RequiresGrossExamination = true;
        }
    }
 
    public class SkinShavePunchMiscBiopsy : Specimen
    {
        public SkinShavePunchMiscBiopsy()
        {
            this.m_SpecimenId = "SKSHPHMSSPCMN";
            this.m_SpecimenName = "Skin Shave Punch and Misc Biopsy";
            this.m_Description = null;
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = "Formalin";
            this.m_RequiresGrossExamination = true;
        }
    }
    
    public class GallbladderExcision : Specimen
    {
        public GallbladderExcision()
        {
            this.m_SpecimenId = "GBSPCMN";
            this.m_SpecimenName = "Gallbladder Excision";
            this.m_Description = "Gallbladder Excision";
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = null;
            this.m_RequiresGrossExamination = true;
        }
    }
    public class SkinExcisionBiopsy : Specimen
    {
        public SkinExcisionBiopsy()
        {
            this.m_SpecimenId = "SKEXSPCMN";
            this.m_SpecimenName = "Skin Excision Biopsy";
            this.m_Description = null;
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = "Formalin";
            this.m_RequiresGrossExamination = true;
        }
    }

    public class ProstateTUR : Specimen
    {
        public ProstateTUR()
        {
            this.m_SpecimenId = "SPCMNPRSTTTUR";
            this.m_SpecimenName = "Prostate TUR";
            this.m_Description = "Prostate, transurethral resection";
            this.m_CPTCode = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88309();            
        }
    }

    public class ProstateRadicalResection : Specimen
    {
        public ProstateRadicalResection()
        {
            this.m_SpecimenId = "SPCMNPRSTTRDCLRSCTN";
            this.m_SpecimenName = "Prostate Radical Resection";
            this.m_Description = "Prostate, radical resection";
            this.m_CPTCode = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88309();
        }
    }

    public class ProstateExceptRadicalResection : Specimen
    {
        public ProstateExceptRadicalResection()
        {
            this.m_SpecimenId = "SPCMNPRSTTRDCLRSCTN";
            this.m_SpecimenName = "Prostate Except Radical Resection";
            this.m_Description = "Prostate, except radical resection";
            this.m_CPTCode = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88307();
        }
    }

    public class ProstateNeedleBiopsy : Specimen
    {
        public ProstateNeedleBiopsy()
        {
            this.m_SpecimenId = "SPCMNPRSTTNDLBPSY";
            this.m_SpecimenName = "Prostate Needle Biopsy";
            this.m_Description = "Prostate, [right/left/base], needle biopsy";
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = "B+ Fixative";
            this.m_CPTCode = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88305();
        }
    }

    public class AppendixExcision : Specimen
    {
        public AppendixExcision()
        {
            this.m_SpecimenId = "APPNDXXCSN";
            this.m_SpecimenName = "Appendix Excision";
            this.m_Description = "Appendix, excision";
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = null;
            this.m_CPTCode = new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT88304();
        }
    }

    public class TonsilExcision : Specimen
    {
        public TonsilExcision()
        {
            this.m_SpecimenId = "TNSLSPCM";
            this.m_SpecimenName = "Tonsil Excision";
            this.m_Description = null;
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = null;
            this.m_RequiresGrossExamination = true;
        }
    }
    public class POC : Specimen
    {
        public POC()
        {
            this.m_SpecimenId = "POCSPCMN";
            this.m_SpecimenName = "POC";
            this.m_Description = "Uteran Contents";
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = null;
            this.m_RequiresGrossExamination = true;
        }
    }

    public class BreastReduction : Specimen
    {
        public BreastReduction()
        {
            this.m_SpecimenId = "BRSTRDSPCMN";
            this.m_SpecimenName = "Breast Reduction";
            this.m_Description = null;
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = null;
            this.m_RequiresGrossExamination = true;
        }
    }

    public class ECC : Specimen
    {
        public ECC()
        {
            this.m_SpecimenId = "ECCSPCMN";
            this.m_SpecimenName = "ECC";
            this.m_Description = "Endocervical Curettage";
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = null;
            this.m_RequiresGrossExamination = true;
        }
    }

    public class EMB : Specimen
    {
        public EMB()
        {
            this.m_SpecimenId = "EMBSPCMN";
            this.m_SpecimenName = "EMB";
            this.m_Description = "Endometrium Biopsy";
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = null;
            this.m_RequiresGrossExamination = true;
        }
    }

    public class CervicalBiopsy : Specimen
    {
        public CervicalBiopsy()
        {
            this.m_SpecimenId = "CVCLBSPYSPCMN";
            this.m_SpecimenName = "Cervical Biopsy";
            this.m_Description = null;
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = null;
            this.m_RequiresGrossExamination = true;
        }
    }

        public class LEEPExcision : Specimen
    {
        public LEEPExcision()
        {
            this.m_SpecimenId = "LPSPCMN";
            this.m_SpecimenName = "LEEP Excision";
            this.m_Description = null;
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = null;
            this.m_RequiresGrossExamination = true;
        }
    }

        public class Placenta : Specimen
        {
            public Placenta()
            {
                this.m_SpecimenId = "PLCNTSPCMN";
                this.m_SpecimenName = "Placenta";
                this.m_Description = "Placenta";
                this.m_LabFixation = "Formalin";
                this.m_ClientFixation = null;
                this.m_RequiresGrossExamination = true;
            }
        }

        public class Uterus : Specimen
        {
            public Uterus()
            {
                this.m_SpecimenId = "UTRSSPCMN";
                this.m_SpecimenName = "Uterus Excision";
                this.m_Description = null;
                this.m_LabFixation = "Formalin";
                this.m_ClientFixation = null;
                this.m_RequiresGrossExamination = true;
            }
        }

        public class NeedleCoreBiopsy : Specimen
        {
            public NeedleCoreBiopsy()
            {
                this.m_SpecimenId = "NCBSPCMN";
                this.m_SpecimenName = "Needle Core Biopsy";
                this.m_Description = null;
                this.m_LabFixation = "Formalin";
                this.m_ClientFixation = null;
                this.m_RequiresGrossExamination = true;
            }
        }
}
