using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Builder
{
    public class PanelSetOrderBuilderFactory
    {
        public static PanelSetOrderBuilder GetBuilder(int panelSetId)
        {
            PanelSetOrderBuilder panelSetOrderBuilder = null;
            switch (panelSetId)
            {				
				case 20: //Leukemia/Lymphoma Phenotyping
				case 21: //Thrombocytopenia Profile
				case 22: //Platelet Associated Antibodies
				case 23: //Reticulated Platelet Analysis
				case 24: //Stem Cell Enumeration
				case 28: //Fetal Hemoglobin
				case 29: //DNA Content and S-Phase Analysis
				case 56: //T-Cell Immunodeficiency Profile
				case 57: //Comprehensive Immunodeficiency Profile  (B-Cell and T-Cell)
				case 58: //Absolute CD4 Count
				case 59: //HLA - B27
					panelSetOrderBuilder = new PanelSetOrderBuilderLeukemiaLymphoma();
					break;
				case 60: //Egfr
                    panelSetOrderBuilder = new PanelSetOrderBuilderEgfr();
                    break;
                case 78:
                    panelSetOrderBuilder = new PanelSetOrderBuilderSummaryReport();
                    break;                
                default:
                    panelSetOrderBuilder = new PanelSetOrderBuilder();
                    break;
            }
            return panelSetOrderBuilder;                
        }
    }
}
