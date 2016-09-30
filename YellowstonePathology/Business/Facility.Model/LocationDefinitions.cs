using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Facility.Model.LocationDefinitions
{
    public class NullLocation : Location
    {
        public NullLocation()
        {
        }
    }

    public class NANODROPPC : Location
    {
        public NANODROPPC()
        {
            this.LocationId = "NANODROPPC";
            this.m_Description = "Nano Drop PC";            
        }
    }

    public class YolandaHuttonOffice : Location
    {
        public YolandaHuttonOffice()
        {
            this.LocationId = "YLNDHTTNOFFC";
            this.m_Description = "Yolanda Hutton";
        }
    }

    public class Compile : Location
    {
        public Compile()
        {
            this.LocationId = "COMPILE";
            this.m_Description = "Application Computer";
        }
    }

    public class WilliamCoplandOffice : Location
    {
        public WilliamCoplandOffice()
        {
            this.LocationId = "WLLMCPLNDOFFC";
            this.m_Description = "William Copland";
        }
    }

    public class TiffanyGoodsonOffice : Location
    {
        public TiffanyGoodsonOffice()
        {
            this.LocationId = "TFFNYGDSNOFFC";
            this.m_Description = "Tiffany Goodson";
        }
    }

    public class TheDarkRoom : Location
    {
        public TheDarkRoom()
        {
            this.m_LocationId = "THEDARKRM";
            this.m_Description = "The Dark Room";
        }
    }

    public class SvhPathologistOffice : Location
    {
        public SvhPathologistOffice()
        {
            this.m_LocationId = "SVHPTHLGSTOFFC";
            this.m_Description = "SVH Pathologist's Office";
        }
    }

    public class SidHarderOffice : Location
    {
        public SidHarderOffice()
        {
            this.LocationId = "SDHRDROFFC";
            this.m_Description = "Sid Harder";
        }
    }

    public class RobertaBeckersOffice : Location
    {
        public RobertaBeckersOffice()
        {
            this.LocationId = "RBRTBCKRSOFFC";
            this.m_Description = "Roberta Beckers";
        }
    }

    public class PamCleggOffice : Location
    {
        public PamCleggOffice()
        {
            this.m_LocationId = "DRPMCLGOFFC";
            this.m_Description = "Dr. Clegg";
        }
    }

    public class ChelsyOrtloffOffice : Location
    {
        public ChelsyOrtloffOffice()
        {
            this.LocationId = "CHOLCMPTR";
            this.m_Description = "Chelsy Ortloff";
        }
    }

    public class MikeBoydOffice : Location
    {
        public MikeBoydOffice()
        {
            this.LocationId = "MKBYDOFFC";
            this.m_Description = "Mike Boyd's Office";
        }
    }

    public class MelissaMelbyOffice : Location
    {
        public MelissaMelbyOffice()
        {
            this.LocationId = "MLSSMLBYOFFC";
            this.m_Description = "Melissa Melby";
        }
    }

    public class KevinBengeOffice : Location
    {
        public KevinBengeOffice()
        {
            this.LocationId = "KVNBNGOFFC";
            this.m_Description = "Kevin Benge";
        }
    }

    public class JulieBlaschakOffice : Location
    {
        public JulieBlaschakOffice()
        {
            this.LocationId = "JLBLSCHKOFFC";
            this.m_Description = "Julie Blaschak";
        }
    }

    public class JoiGarzaOffice : Location
    {
        public JoiGarzaOffice()
        {
            this.LocationId = "JGRZOFFC";
            this.m_Description = "Joi Garza";
        }
    }

    public class EricRamseyOffice : Location
    {
        public EricRamseyOffice()
        {
            this.LocationId = "ERCRMSYOFFC";
            this.m_Description = "Eric Ramsey";
        }
    }

    public class DrSchultzOffice : Location
    {
        public DrSchultzOffice()
        {
            this.m_LocationId = "DRSCHLTZOFFC";
            this.m_Description = "Dr. Schultz";
        }
    }

    public class DrNeroOffice : Location
    {
        public DrNeroOffice()
        {
            this.m_LocationId = "DRNEROOFFC";
            this.m_Description = "Dr. Nero";
        }
    }

    public class DrEmerickOffice : Location
    {
        public DrEmerickOffice()
        {
            this.m_LocationId = "DREMRCKOFFC";
            this.m_Description = "Dr. Emerick";
        }
    }

    public class DrDurdenOffice : Location
    {
        public DrDurdenOffice()
        {
            this.m_LocationId = "DRDRDNOFFC";
            this.m_Description = "Dr. Durden";
        }

    }
    public class DrBrownOffice : Location
    {
        public DrBrownOffice()
        {
            this.m_LocationId = "DRBRWNOFFC";
            this.m_Description = "Dr. Brown";
        }
    }

    public class DonaCranstonOffice : Location
    {
        public DonaCranstonOffice()
        {
            this.LocationId = "DNNCRNSTNOFFC";
            this.m_Description = "Dona Cranston";
        }
    }

    public class CodyGrossStation : Location
    {
        public CodyGrossStation()
        {
            this.m_LocationId = "CDYGRSS";
            this.m_Description = "Cody Gross Station";            
        }
    }

    public class CodyAccessionStation : Location
    {
        public CodyAccessionStation()
        {
            this.m_LocationId = "CDYACCN";
            this.m_Description = "Cody Accessioning Station";
        }
    }

    public class BlgsMolecularBStation : Location
    {
        public BlgsMolecularBStation()
        {
            this.LocationId = "BLGSMLCLRBSTN";
            this.m_Description = "Billings Molecular B Station";
        }
    }

    public class BlgsMolecularAStation : Location
    {
        public BlgsMolecularAStation()
        {
            this.LocationId = "BLGSMLCLRASTN";
            this.m_Description = "Billings Molecular A Station";
        }
    }

    public class BlgsIT3Station : Location
    {
        public BlgsIT3Station()
        {
            this.m_LocationId = "BLGSIT3STN";
            this.m_Description = "Billings IT 3 Station";
        }
    }

    public class BlgsICStation : Location
    {
        public BlgsICStation()
        {
            this.m_LocationId = "BLGSICSTN";
            this.m_Description = "Billings Interoperative Consultation Station";
        }
    }

    public class BlgsHistologyIHCStation : Location
    {
        public BlgsHistologyIHCStation()
        {
            this.m_LocationId = "BLGSHSTIHCSTN";
            this.m_Description = "Billings Histology IHC station";
        }
    }

    public class BlgsHistologyBStation : Location
    {
        public BlgsHistologyBStation()
        {
            this.LocationId = "BLGSHSTLGYBSTN";
            this.m_Description = "Billings Histology B Station";
        }
    }

    public class BlgsHistologyAStation : Location
    {
        public BlgsHistologyAStation()
        {
            this.m_LocationId = "BLGSHSTASTN";
            this.m_Description = "Billings Histology A Station";
        }
    }

    public class BlgsGrossTechStation : Location
    {
        public BlgsGrossTechStation()
        {
            this.m_LocationId = "BLGSGRSTCHSTN";
            this.m_Description = "Billings Gross Technition Station";
        }
    }

    public class BlgsGrossPathStation : Location
    {
        public BlgsGrossPathStation()
        {
            this.m_LocationId = "BLGSGRSPTHSTN";
            this.m_Description = "Billings Gross Pathologist Station";
        }
    }

    public class BlgsGrossHobbitStation : Location
    {
        public BlgsGrossHobbitStation()
        {
            this.m_LocationId = "BLGSGRSSHBBTSTN";
            this.m_Description = "Billings Gross Hobbit Station";
        }
    }

    public class BlgsFlowCStation : Location
    {
        public BlgsFlowCStation()
        {
            this.LocationId = "BLGSFLWCSTN";
            this.m_Description = "Billings Flow C Station";
        }
    }

    public class BlgsFlowBStation : Location
    {
        public BlgsFlowBStation()
        {
            this.LocationId = "BLGSFLWBSTN";
            this.m_Description = "Billings Flow B Station";
        }
    }

    public class BlgsCytologyLoginStation : Location
    {
        public BlgsCytologyLoginStation()
        {
            this.m_LocationId = "BLGSCYTLGYLGNSTN";
            this.m_Description = "Billings Cytology Login Station";
        }
    }

    public class BlgsCuttingStationTenille : Location
    {
        public BlgsCuttingStationTenille()
        {
            this.LocationId = "BLGSCTTTNGSTNTNLL";
            this.m_Description = "Billings Cutting Station Tenille";
        }
    }

    public class BlgsCaseCompilationStation : Location
    {
        public BlgsCaseCompilationStation()
        {
            this.m_LocationId = "BLGSCSCMPLTNSTN";
            this.m_Description = "Billings Case Compilation Station";
        }
    }

    public class BlgsCuttingStationCaptain : Location
    {
        public BlgsCuttingStationCaptain()
        {
            this.LocationId = "BLGSCTTNGSTNCPTN";
            this.m_Description = "Billings Cutting Station Captain";
        }
    }

    public class BlgsFlowAStation : Location
    {
        public BlgsFlowAStation()
        {
            this.LocationId = "BLGSFLWASTN";
            this.m_Description = "Billings Flow A Station";
        }
    }

	public class BlgsCytologySlideStation : Location
	{
		public BlgsCytologySlideStation()
		{
			this.m_LocationId = "BLGSCYTLGYSLDSTN";
			this.m_Description = "Cytology Slide Printing Station";
		}
	}
}
