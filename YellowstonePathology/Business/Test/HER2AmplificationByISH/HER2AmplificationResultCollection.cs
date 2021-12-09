using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
    public class HER2AmplificationResultCollection : ObservableCollection<HER2AmplificationResult>
    {
        public HER2AmplificationResultCollection(PanelSetOrderCollection panelSetOrderCollection, HER2AmplificationByISHTestOrder panelSetOrder)
        {
            this.Add(new HER2AmplificationResultGroup1Breast(panelSetOrderCollection, panelSetOrder));
            this.Add(new HER2AmplificationResultGroup2Breast(panelSetOrderCollection, panelSetOrder));
            this.Add(new HER2AmplificationResultGroup3Breast(panelSetOrderCollection, panelSetOrder));
            this.Add(new HER2AmplificationResultGroup4Breast(panelSetOrderCollection, panelSetOrder));
            this.Add(new HER2AmplificationResultGroup5Breast(panelSetOrderCollection, panelSetOrder));            
            this.Add(new HER2AmplificationResultGastric(panelSetOrderCollection, panelSetOrder));
            this.Add(new HER2AmplificationResultEndometrial(panelSetOrderCollection, panelSetOrder));
        }

        public HER2AmplificationResultCollection(PanelSetOrderCollection panelSetOrderCollection, HER2AnalysisSummary.HER2AnalysisSummaryTestOrder panelSetOrder)
        {
            this.Add(new HER2AmplificationResultGroup2Breast(panelSetOrderCollection, panelSetOrder));
            this.Add(new HER2AmplificationResultGroup3Breast(panelSetOrderCollection, panelSetOrder));
            this.Add(new HER2AmplificationResultGroup4Breast(panelSetOrderCollection, panelSetOrder));            
        }
        
        public HER2AmplificationResult FindMatch()
        {
            HER2AmplificationResult result = null;

            foreach (HER2AmplificationResult her2AmplificationResult in this)
            {
                if (her2AmplificationResult.IsAMatch() == true)
                {
                    result = her2AmplificationResult;
                    break;
                }
            }
            return result;
        }
    }
}
