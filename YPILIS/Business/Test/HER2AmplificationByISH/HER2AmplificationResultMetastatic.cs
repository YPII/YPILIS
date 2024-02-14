using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Specimen.Model;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
    public class HER2AmplificationResultMetastatic : HER2AmplificationResultBreast
    {

        public HER2AmplificationResultMetastatic(PanelSetOrderCollection panelSetOrderCollection, HER2AnalysisSummary.HER2AnalysisSummaryTestOrder panelSetOrder) : base(panelSetOrderCollection, panelSetOrder)
        {

        }

        public override bool IsAMatch()
        {
            bool result = false;
            if (this.m_Indicator.ToString().StartsWith("Breast Metastatic"))
            {                
                result = true;                
            }
            return result;
        }        

        public override void SetSummaryResults(SpecimenOrder specimenOrder)
        {
            Business.Test.HER2AnalysisSummary.HER2AnalysisSummaryTestOrder her2Summary = (Business.Test.HER2AnalysisSummary.HER2AnalysisSummaryTestOrder)this.m_PanelSetOrderCollection.GetFirstByPanelSetId(313);
            Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder her2ISH = (Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder)this.m_PanelSetOrderCollection.GetFirstByPanelSetId(46);
            Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC her2IHC = (Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC)this.m_PanelSetOrderCollection.GetFirstByPanelSetId(171);
            Business.Test.Her2AmplificationByIHC.MetastaticStatusCollection metastaticStatusCollection = new Business.Test.Her2AmplificationByIHC.MetastaticStatusCollection();
            Business.Test.Her2AmplificationByIHC.MetastaticStatusResult metastaticStatusResult = metastaticStatusCollection.GetResult(her2ISH.Result, her2IHC.Result, her2IHC.Score);
            her2Summary.InterpretiveComment = her2IHC.Interpretation;
            
            her2Summary.Result = metastaticStatusResult.Her2Status;
        }        
    }
}
