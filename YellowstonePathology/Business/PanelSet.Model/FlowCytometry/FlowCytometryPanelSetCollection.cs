using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model.FlowCytometry
{
    public class FlowCytometryPanelSetCollection : ObservableCollection<PanelSet>
    {
        public FlowCytometryPanelSetCollection()
        {
			this.Add(new YellowstonePathology.Business.Test.LLP.LeukemiaLymphomaTest());
			this.Add(new YellowstonePathology.Business.Test.ThrombocytopeniaProfile.ThrombocytopeniaProfileTest());
			this.Add(new YellowstonePathology.Business.Test.PlateletAssociatedAntibodies.PlateletAssociatedAntibodiesTest());
			this.Add(new YellowstonePathology.Business.Test.ReticulatedPlateletAnalysis.ReticulatedPlateletAnalysisTest());
			this.Add(new YellowstonePathology.Business.Test.StemCellEnumeration.StemCellEnumerationTest());
        }
    }
}
