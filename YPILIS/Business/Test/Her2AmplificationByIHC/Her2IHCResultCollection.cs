using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.Her2AmplificationByIHC
{
    public class Her2IHCResultCollection : ObservableCollection<Her2IHCResult>
    {
        public Her2IHCResultCollection()
        {

        }

        public Her2IHCResultCollection FilterByIndication(string indication)
        {
            if (indication == "Breast") indication = "Breast Primary";
            Her2IHCResultCollection result = new Her2IHCResultCollection();            
            IEnumerable<Business.Test.Her2AmplificationByIHC.Her2IHCResult> filteredResult = this.Where(r => r.Indication == indication);
            foreach(Business.Test.Her2AmplificationByIHC.Her2IHCResult ihc in filteredResult)
            {
                result.Add(ihc);
            }
            return result;
        }

        public static Her2IHCResultCollection GetALL()
        {
            Her2IHCResultCollection result = new Her2IHCResultCollection();
            result.Add(new Her2IHCResult("Breast Primary", "Positive", "3+", "Intense, complete, circumferential staining in >10% of tumor cells", PanelSetOrderHer2AmplificationByIHC.BreastPrimaryMethod));
            result.Add(new Her2IHCResult("Breast Primary", "Equivocal", "2+", "Weak to moderate complete membrane staining in >10% of tumor cells", PanelSetOrderHer2AmplificationByIHC.BreastPrimaryMethod));
            result.Add(new Her2IHCResult("Breast Primary", "Negative", "1+", "Incomplete membrane staining that is faint/barely perceptible in >10% of tumor cells", PanelSetOrderHer2AmplificationByIHC.BreastPrimaryMethod));
            result.Add(new Her2IHCResult("Breast Primary", "Negative", "0", "No staining; or incomplete membrane staining that is faint/barely perceptible in <10% of tumor cells", PanelSetOrderHer2AmplificationByIHC.BreastPrimaryMethod));

            result.Add(new Her2IHCResult("Breast Metastatic", "Positive", "3+", "Intense, complete, circumferential staining in >10% of tumor cell", PanelSetOrderHer2AmplificationByIHC.BreastMetastaticMethod));
            result.Add(new Her2IHCResult("Breast Metastatic", "Low Positive", "2+", "Weak to moderate complete membrane staining in >10% of tumor cells", PanelSetOrderHer2AmplificationByIHC.BreastMetastaticMethod));
            result.Add(new Her2IHCResult("Breast Metastatic", "Low Positive", "1+", "Incomplete membrane staining that is faint/barely perceptible in >10% of tumor cells", PanelSetOrderHer2AmplificationByIHC.BreastMetastaticMethod));
            result.Add(new Her2IHCResult("Breast Metastatic", "Negative", "0", "No staining; or incomplete membrane staining that is faint/barely perceptible in <10% of tumor cells", PanelSetOrderHer2AmplificationByIHC.BreastMetastaticMethod));
            return result;
        }
    }
}
