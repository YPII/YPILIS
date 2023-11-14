using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
    public class UniversalServiceCollectionRiverstone : ObservableCollection<UniversalService>
    {
        public UniversalServiceCollectionRiverstone()
        {
            this.Add(new UniversalService("SRGCL", "Surgical Pathology", UniversalServiceApplicationNameEnum.ECLINICALWORKS, 13));
            this.Add(new UniversalService("THINPREP", "Thin Prep Pap", UniversalServiceApplicationNameEnum.ECLINICALWORKS, 15));
            this.Add(new UniversalService("THINPREP", "Thin Prep Pap", UniversalServiceApplicationNameEnum.ECLINICALWORKS, 116));
            this.Add(new UniversalService("NGCT", "Chlamydia Gonorrhea Screening", UniversalServiceApplicationNameEnum.ECLINICALWORKS, 3));
            this.Add(new UniversalService("HRHPVTEST", "High Risk HPV", UniversalServiceApplicationNameEnum.ECLINICALWORKS, 14));
            this.Add(new UniversalService("HPVG1618", "HPV Genotypes 16 and 18", UniversalServiceApplicationNameEnum.ECLINICALWORKS, 62));
            this.Add(new UniversalService("TRICH", "Trichomonas Vaginalis", UniversalServiceApplicationNameEnum.ECLINICALWORKS, 61));
            this.Add(new UniversalService("SARSCOV2", "SARS-CoV-2", UniversalServiceApplicationNameEnum.ECLINICALWORKS, 400));
            this.Add(new UniversalService("APCONSULT", "Anatomic Pathology Consultation", UniversalServiceApplicationNameEnum.ECLINICALWORKS, 110));
        }        

        public UniversalService GetByPanelSetId(int panelSetId)
        {
            UniversalService result = null;

            
            foreach(UniversalService us in this)
            {
                if(us.PanelSetId == panelSetId)
                {
                    result = us;
                    break;
                }
            }
            return result;
        }
    }
}
