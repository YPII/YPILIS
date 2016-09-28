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

    public class AutopsySpecimen : Specimen
    {
        public AutopsySpecimen()
        {
            this.m_SpecimenId = "SPCMNAUTOPSY";
            this.m_SpecimenName = "Autopsy Specimen";
            this.m_Description = null;
            this.m_LabFixation = "Fomalin";
            this.m_ClientFixation = "Formalin";
            this.m_RequiresGrossExamination = false;
        }
    }

    public class Urine : Specimen
    {
        public Urine()
        {
            this.m_SpecimenId = "SPCMNURINE";
            this.m_SpecimenName = "Urine";
            this.m_Description = "Urine";
            this.m_LabFixation = null;
            this.m_ClientFixation = null;
            this.m_RequiresGrossExamination = false;
        }
    }

    public class Peripheral : Specimen
    {
        public Peripheral()
        {
            this.m_SpecimenId = "SPCPBS";
            this.m_SpecimenName = "Peripheral Blood Smear";
            this.m_Description = "Peripheral Blood Smear";
            this.m_LabFixation = null;
            this.m_ClientFixation = null;
            this.m_RequiresGrossExamination = false;
        }
    }

    public class GenericSpecimenGrossRequiredWithBlocks : Specimen
    {
        public GenericSpecimenGrossRequiredWithBlocks()
        {
            this.m_SpecimenId = "SPCMGENERICGRWBLCKS";
            this.m_SpecimenName = "Generic Specimen, Gross Required With Blocks";
            this.m_Description = null;
            this.m_LabFixation = "Fomalin";
            this.m_ClientFixation = "Formalin";
            this.m_RequiresGrossExamination = true;
        }
    }

    public class GenericSpecimenGrossOnly : Specimen
    {
        public GenericSpecimenGrossOnly()
        {
            this.m_SpecimenId = "SPCMGENERICGO";
            this.m_SpecimenName = "Generic Specimen, Gross Only";
            this.m_Description = null;
            this.m_LabFixation = "Fomalin";
            this.m_ClientFixation = "Formalin";
            this.m_RequiresGrossExamination = false;
        }
    }

    public class Fluid : Specimen
    {
        public Fluid()
        {
            this.m_SpecimenId = "SPCMFLUID";
            this.m_SpecimenName = "Fluid";
            this.m_Description = "Fluid";
            this.m_LabFixation = null;
            this.m_ClientFixation = null;
            this.m_RequiresGrossExamination = true;
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

    public class Biopsy : Specimen
    {
        public Biopsy()
        {
            this.m_SpecimenId = "BXSPCMN";
            this.m_SpecimenName = "Biopsy";
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

    public class SkinShavewithCurettingsBiopsy : Specimen
    {
        public SkinShavewithCurettingsBiopsy()
        {
            this.m_SpecimenId = "SKSHCSPCMN";
            this.m_SpecimenName = "Skin Shave Biopsy with Curettings";
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
            this.m_Description = "Gallbladder, excision";
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = null;
            this.m_RequiresGrossExamination = true;
        }
    }
    
    public class SkinExcisionOrientedBiopsy : Specimen
    {
        public SkinExcisionOrientedBiopsy()
        {
            this.m_SpecimenId = "SKEXOSPCMN";
            this.m_SpecimenName = "Skin Excision Biopsy, Oriented";
            this.m_Description = "Skin, [site], excisional biopsy";
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = "Formalin";
            this.m_RequiresGrossExamination = true;
        }
    }
    public class SkinExcisionUnorientedBiopsy : Specimen
    {
        public SkinExcisionUnorientedBiopsy()
        {
            this.m_SpecimenId = "SKEXUOSPCMN";
            this.m_SpecimenName = "Skin Excision Biopsy, Unoriented";
            this.m_Description = "Skin, [site], excisional biopsy";
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = "Formalin";
            this.m_RequiresGrossExamination = true;
        }
    }
    
        public class SkinExcisionOrientedwithCurettingsBiopsy : Specimen
    {
        public SkinExcisionOrientedwithCurettingsBiopsy()
        {
            this.m_SpecimenId = "SKEXOCSPCMN";
            this.m_SpecimenName = "Skin Excision Biopsy with Curettings, Oriented";
            this.m_Description = "Skin, [site], excisional biopsy";
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = "Formalin";
            this.m_RequiresGrossExamination = true;
        }
    }
    public class SkinExcisionUnorientedwithCurettingsBiopsy : Specimen
    {
        public SkinExcisionUnorientedwithCurettingsBiopsy()
        {
            this.m_SpecimenId = "SKEXUOCSPCMN";
            this.m_SpecimenName = "Skin Excision Biopsy with Curettings, Unoriented";
            this.m_Description = "Skin, [site], excisional biopsy";
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
            this.m_SpecimenId = "SPCMNPRSTTXRDCLRSCTN";
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

    public class AdenoidExcision : Specimen
    {
        public AdenoidExcision()
        {
            this.m_SpecimenId = "ADSPCM";
            this.m_SpecimenName = "Adenoid Excision";
            this.m_Description = "Adenoid, excision";
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = null;
            this.m_RequiresGrossExamination = true;
        }
    }

    public class TonsilExcision : Specimen
    {
        public TonsilExcision()
        {
            this.m_SpecimenId = "TNSLSPCM";
            this.m_SpecimenName = "Tonsil Excision";
            this.m_Description = "Right and left tonsils, excision";
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = null;
            this.m_RequiresGrossExamination = true;
        }
    }

    public class TonsilAdenoidExcision : Specimen
    {
        public TonsilAdenoidExcision()
        {
            this.m_SpecimenId = "TNSLADSPCM";
            this.m_SpecimenName = "Tonsil and Adenoids Excision";
            this.m_Description = "Right and left tonsils with adenoids, excision";
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
            this.m_Description = "Uterine Contents";
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
            this.m_Description = "[right/left] breast, reduction mammoplasty";
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
            this.m_Description = "Endocervix, curettage";
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
            this.m_Description = "Endometrium, biopsy";
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
            this.m_Description = "Cervix, [site], biopsy";
            this.m_LabFixation = "Formalin";
            this.m_ClientFixation = null;
            this.m_RequiresGrossExamination = true;
        }
    }


        public class SinglePlacenta : Specimen
        {
            public SinglePlacenta()
            {
                this.m_SpecimenId = "SNGLPLCNTSPCMN";
                this.m_SpecimenName = "Placenta";
                this.m_Description = "Placenta";
                this.m_LabFixation = "Formalin";
                this.m_ClientFixation = null;
                this.m_RequiresGrossExamination = true;
            }
        }
        
        public class Twin1Placenta : Specimen
        {
            public Twin1Placenta()
            {
                this.m_SpecimenId = "TWNPLCNTWMSPCMN";
                this.m_SpecimenName = "Twin Placenta with Dividing Membrane";
                this.m_Description = "Twin Placenta";
                this.m_LabFixation = "Formalin";
                this.m_ClientFixation = null;
                this.m_RequiresGrossExamination = true;
            }
        }
        
        public class Twin2Placenta : Specimen
        {
            public Twin2Placenta()
            {
                this.m_SpecimenId = "TWNPLCNTWOMSPCMN";
                this.m_SpecimenName = "Twin Placenta without Dividing Membrane";
                this.m_Description = "Twin Placenta";
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
                this.m_SpecimenName = "Uterus Resection";
                this.m_Description = "Uterus and cervix, resection";
                this.m_LabFixation = "Formalin";
                this.m_ClientFixation = null;
                this.m_RequiresGrossExamination = true;
            }
        }

        public class UterusAdnexa : Specimen
        {
            public UterusAdnexa()
            {
                this.m_SpecimenId = "UTRADNSPCMN";
                this.m_SpecimenName = "Uterus with Adnexa Resection";
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

        public class LEEP : Specimen
        {
            public LEEP()
            {
                this.m_SpecimenId = "LPSPCMN";
                this.m_SpecimenName = "LEEP";
                this.m_Description = "Cervix, LEEP";
                this.m_LabFixation = "Formalin";
                this.m_ClientFixation = null;
                this.m_RequiresGrossExamination = true;
            }
        }
        
        public class CervicalCone : Specimen
        {
           public CervicalCone()
            {
                this.m_SpecimenId = "CXCNSPCMN";
                this.m_SpecimenName = "Cervical Cone";
                this.m_Description = "Cervix, cone, excision";
                this.m_LabFixation = "Formalin";
                this.m_ClientFixation = null;
                this.m_RequiresGrossExamination = true;
            }
        }

        public class LEEPPieces : Specimen
        {
            public LEEPPieces()
            {
                this.m_SpecimenId = "LPPCSPCMN";
                this.m_SpecimenName = "LEEP Pieces";
                this.m_Description = "Cervix, LEEP excision";
                this.m_LabFixation = "Formalin";
                this.m_ClientFixation = null;
                this.m_RequiresGrossExamination = true;
            }
        }

        public class AorticValve : Specimen
        {
            public AorticValve()
            {
                this.m_SpecimenId = "AVSPCMN";
                this.m_SpecimenName = "Aortic Valve";
                this.m_Description = "Aortic valve";
                this.m_LabFixation = "Formalin";
                this.m_ClientFixation = null;
                this.m_RequiresGrossExamination = true;
            }
        }

        public class MitralValve : Specimen
        {
            public MitralValve()
            {
                this.m_SpecimenId = "MVSPCMN";
                this.m_SpecimenName = "Mitral Valve";
                this.m_Description = "Mitral valve";
                this.m_LabFixation = "Formalin";
                this.m_ClientFixation = null;
                this.m_RequiresGrossExamination = true;
            }
        }
        
        public class KneeTissue : Specimen
        {
            public KneeTissue()
            {
                this.m_SpecimenId = "KTSPCMN";
                this.m_SpecimenName = "Knee Tissue";
                this.m_Description = null;
                this.m_LabFixation = "Formalin";
                this.m_ClientFixation = null;
                this.m_RequiresGrossExamination = true;
            }
        }

        public class FemoralHead : Specimen
        {
            public FemoralHead()
            {
                this.m_SpecimenId = "FHSPCMN";
                this.m_SpecimenName = "Femoral Head";
                this.m_Description = null;
                this.m_LabFixation = "Formalin";
                this.m_ClientFixation = null;
                this.m_RequiresGrossExamination = true;
            }
        }

        public class FallopianTube : Specimen
        {
            public FallopianTube()
            {
                this.m_SpecimenId = "FTSPCMN";
                this.m_SpecimenName = "Fallopian Tube";
                this.m_Description = "[Right/Left] fallopian tube, segmental resection";
                this.m_LabFixation = "Formalin";
                this.m_ClientFixation = null;
                this.m_RequiresGrossExamination = true;
            }
        }

        public class SinusContent : Specimen
        {
            public SinusContent()
            {
                this.m_SpecimenId = "SCSPCMN";
                this.m_SpecimenName = "Sinus Content";
                this.m_Description = "[Right/Left] sinus content";
                this.m_LabFixation = "Formalin";
                this.m_ClientFixation = null;
                this.m_RequiresGrossExamination = true;
            }
        }
    public class Consult : Specimen
    {
        public Consult()
        {
            this.m_SpecimenId = "CNSLT";
            this.m_SpecimenName = "Consult";
            this.m_Description = null;
            this.m_LabFixation = null;
            this.m_ClientFixation = null;
            this.m_RequiresGrossExamination = false;
        }
    }

    public class InitialReading : Specimen
    {
        public InitialReading()
        {
            this.m_SpecimenId = "NTLRDNG";
            this.m_SpecimenName = "Initial Reading";
            this.m_Description = null;
            this.m_LabFixation = null;
            this.m_ClientFixation = null;
            this.m_RequiresGrossExamination = false;
        }
    }
}
