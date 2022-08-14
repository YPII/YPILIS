using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
    public class UniversalServiceCollectionNMH : ObservableCollection<UniversalService>
    {
        public UniversalServiceCollectionNMH()
        {
            this.Add(new UniversalService("NG", "Cytology Non-Gyn Path Specimen", UniversalServiceApplicationNameEnum.NMH, 13));
            this.Add(new UniversalService("PAP", "Pap Smear", UniversalServiceApplicationNameEnum.NMH, 15));
            //this.Add(new UniversalService("THINPREP", "Thin Prep Pap", UniversalServiceApplicationNameEnum.ECLINICALWORKS, 15));
            //this.Add(new UniversalService("THINPREP", "Thin Prep Pap", UniversalServiceApplicationNameEnum.ECLINICALWORKS, 116));
            //this.Add(new UniversalService("NGCT", "Chlamydia Gonorrhea Screening", UniversalServiceApplicationNameEnum.ECLINICALWORKS, 3));
            //this.Add(new UniversalService("HRHPVTEST", "High Risk HPV", UniversalServiceApplicationNameEnum.ECLINICALWORKS, 14));
            //this.Add(new UniversalService("HPVG1618", "HPV Genotypes 16 and 18", UniversalServiceApplicationNameEnum.ECLINICALWORKS, 62));            
        }

        public UniversalService GetByUniversalServiceId(string universalServiceId)
        {
            UniversalService result = null;
            foreach (UniversalService us in this)
            {
                if (us.UniversalServiceId == universalServiceId)
                {
                    result = us;
                    break;
                }
            }
            return result;
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
