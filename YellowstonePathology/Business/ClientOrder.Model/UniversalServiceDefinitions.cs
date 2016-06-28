using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions
{
    public class UniversalServiceCFYPI : UniversalService
    {
        public UniversalServiceCFYPI()
        {
            this.m_UniversalServiceId = "CFYPI";
            this.m_ServiceName = "CFYPI";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
    }

    public class UniversalServiceNone : UniversalService
    {
        public UniversalServiceNone()
        {
            this.m_UniversalServiceId = "NONE";
            this.m_ServiceName = "None";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.NONE;
        }
    }

    public class UniversalServicePathSummary : UniversalService
    {
        public UniversalServicePathSummary()
        {
            this.m_UniversalServiceId = "PTHSMMRY";
            this.m_ServiceName = "Pathology Summary";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
    }

    public class UniversalServiceMiscellaneous : UniversalService
    {
        public UniversalServiceMiscellaneous()
        {
            this.m_UniversalServiceId = "MISCYPI";
            this.m_ServiceName = "Miscellaneous Pathology";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
    }

	public class UniversalServiceJAK2 : UniversalService
	{
        public UniversalServiceJAK2()
        {
            this.m_UniversalServiceId = "JAK2MUT";
            this.m_ServiceName = "JAK2 Mutation V617F";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
	}

    public class UniversalServiceCTGC : UniversalService
    {
        public UniversalServiceCTGC()
        {
            this.m_UniversalServiceId = "CTGC";
            this.m_ServiceName = "Chlamydia Trachomatis/Neisseria Gonorrhoeae Screen by PCR";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
    }

    public class UniversalServiceHERAMP : UniversalService
    {
        public UniversalServiceHERAMP()
        {
            this.m_UniversalServiceId = "HERAMP";
            this.m_ServiceName = "HER2 Gene Amplification";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
    }

    public class UniversalServiceHRHPVTEST : UniversalService
	{
        public UniversalServiceHRHPVTEST()
        {
            this.m_UniversalServiceId = "HRHPVTEST";
            this.m_ServiceName = "High Risk HPV Testing";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
	}

    public class UniversalServiceBRAFMANAL : UniversalService
	{
        public UniversalServiceBRAFMANAL()
        {
            this.m_UniversalServiceId = "BRAFMANAL";
            this.m_ServiceName = "BRAF V600E Mutation Analysis";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
	}

    public class UniversalServicePNHHS : UniversalService
	{
        public UniversalServicePNHHS()
        {
            this.m_UniversalServiceId = "PNHHS";
            this.m_ServiceName = "PNH, Highly Sensitive";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
	}

    public class UniversalServiceHPV16 : UniversalService
	{
        public UniversalServiceHPV16()
        {
            this.m_UniversalServiceId = "HPV16";
            this.m_ServiceName = "HPV-16 Testing";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
	}

    public class UniversalServiceKRASBRAF : UniversalService
	{
        public UniversalServiceKRASBRAF()
        {
            this.m_UniversalServiceId = "KRASBRAF";
            this.m_ServiceName = "KRAS with BRAF Reflex";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
	}

    public class UniversalServiceFVLMUT : UniversalService
	{
        public UniversalServiceFVLMUT()
        {
            this.m_UniversalServiceId = "FVLMUT";
            this.m_ServiceName = "Factor V Leiden (R506Q) Mutation Analysis";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
	}

    public class UniversalServicePROMUT : UniversalService
	{
        public UniversalServicePROMUT()
        {
            this.m_UniversalServiceId = "PROMUT";
            this.m_ServiceName = "Prothrombin 20210A Mutation Analysis (Factor II)";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
	}

    public class UniversalServiceMTHFRM : UniversalService
	{
        public UniversalServiceMTHFRM()
        {
            this.m_UniversalServiceId = "MTHFRM";
            this.m_ServiceName = "MTHFR Mutation Analysis";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
	}

    public class UniversalServiceAUTOPSY : UniversalService
	{
        public UniversalServiceAUTOPSY()
        {
            this.m_UniversalServiceId = "AUTOPSY";
            this.m_ServiceName = "Autopsy";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
	}

    public class UniversalServiceBCELLCLON : UniversalService
	{
        public UniversalServiceBCELLCLON()
        {
            this.m_UniversalServiceId = "BCELLCLON";
            this.m_ServiceName = "B-Cell Clonality";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
	}

    public class UniversalServiceKRAS : UniversalService
	{
        public UniversalServiceKRAS()
        {
            this.m_UniversalServiceId = "KRAS";
            this.m_ServiceName = "KRAS";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
	}

    public class UniversalServiceTHINPREP : UniversalService
	{
        public UniversalServiceTHINPREP()
        {
            this.m_UniversalServiceId = "THINPREP";
            this.m_ServiceName = "ThinPrep Pap";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
	}

    public class UniversalServicePLATEAB : UniversalService
	{
        public UniversalServicePLATEAB()
        {
            this.m_UniversalServiceId = "PLATEAB";
            this.m_ServiceName = "Platelet Antibodies";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
	}

    public class UniversalServiceRETICPLATE : UniversalService
	{
        public UniversalServiceRETICPLATE()
        {
            this.m_UniversalServiceId = "RETICPLATE";
            this.m_ServiceName = "Reticulated Platelet";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
	}

    public class UniversalServiceTHROMPRO : UniversalService
	{
        public UniversalServiceTHROMPRO()
        {
            this.m_UniversalServiceId = "THROMPRO";
            this.m_ServiceName = "Thrombocytopenia Profile (Platelet Antibody & Reticulated Platelet)";
        }
	}

    public class UniversalServiceSTEMCE : UniversalService
	{
        public UniversalServiceSTEMCE()
        {
            this.m_UniversalServiceId = "STEMCE";
            this.m_ServiceName = "Stem Cell Enumeration";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
	}

    public class UniversalServiceEPPR : UniversalService
	{
        public UniversalServiceEPPR()
        {
            this.m_UniversalServiceId = "EPPR";
            this.m_ServiceName = "Estrogen Receptor/Progesterone Receptor (IHC)";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
	}

    public class UniversalServiceEGFR : UniversalService
	{
        public UniversalServiceEGFR()
        {
            this.m_UniversalServiceId = "EGFR";
            this.m_ServiceName = "EGFR";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
	}

    public class UniversalServiceMOLEGEN : UniversalService
	{
        public UniversalServiceMOLEGEN()
        {
            this.m_UniversalServiceId = "MOLEGEN";
            this.m_ServiceName = "Molecular Genetics";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
	}

    public class UniversalServiceYPI : UniversalService
	{
        public UniversalServiceYPI()
        {
            this.m_UniversalServiceId = "YPI";
            this.m_ServiceName = "Surgical Pathology";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
	}

    public class UniversalServiceSurgicalPathology : UniversalService
    {
        public UniversalServiceSurgicalPathology()
        {
            this.m_UniversalServiceId = "SRGCLPTH";
            this.m_ServiceName = "Surgical Pathology";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.ECLINICALWORKS;
        }
    }

    public class UniversalServiceTRCHMNAA : UniversalService
	{
        public UniversalServiceTRCHMNAA()
        {
            this.m_UniversalServiceId = "TRCHMNAA";
            this.m_ServiceName = "Trichomonas Vaginalis";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
	}

    public class UniversalServiceCYTO : UniversalService
	{
        public UniversalServiceCYTO()
        {
            this.m_UniversalServiceId = "CYTO";
            this.m_ServiceName = "CYTOLOGY NON GYNECOLOGIC";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
	}    

    public class UniversalServiceHPV1618GEN : UniversalService
	{
        public UniversalServiceHPV1618GEN()
        {
            this.m_UniversalServiceId = "HPV1618GEN";
            this.m_ServiceName = "HPV Genotypes 16 and 18";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
	}

    public class UniversalServiceFLOWYPI : UniversalService
	{
        public UniversalServiceFLOWYPI()
        {
            this.m_UniversalServiceId = "FLOWYPI";
            this.m_ServiceName = "Leukemia/Lymphoma Phenotyping";
            this.m_ApplicationName = UniversalServiceApplicationNameEnum.EPIC;
        }
	}
}
