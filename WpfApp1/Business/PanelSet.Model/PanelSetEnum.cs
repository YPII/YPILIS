using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public enum PanelSetEnum
	{
        None = 0,
        JAK2 = 1,
        CysticFibrosis = 2,
        NGCT = 3,
        KRAS = 4,
        HighRiskHPV = 10,
        FluorescentInSituHybridization = 11,
        HER2AmplificationByFISH = 12,
        SurgicalPathology = 13,
        HPV  = 14,
        Cytology = 15,
        Flow = 16,
        Extraction = 17,
		BRAF = 18,
		PNH = 19,
		LeukemiaLymphomaPhenotyping = 20,
		ThrombocytopeniaProfile = 21,
		PlateletAssociatedAntibodies = 22,
		ReticulatedPlateletAnalysis = 23,
		StemCellEnumeration = 24,
		HPV16 = 25,
		MiscellaneousSendOut = 26,
		KRASSTA = 27,
		FetalHemoglobin = 28,
		DNAContentandSPhaseAnalysis = 29,
		KRASwithBRAFreflex = 30,
        TechnicalOnly = 31,
        FactorVLeiden = 32,
        Prothrombin = 33,
        Mthfr = 34,
		Autopsy = 35,
		BCellClonality = 36
	}
}
