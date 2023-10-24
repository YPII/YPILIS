using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplaySoftTissueMesenchymal : DisplayGroupIHC
    {
        public DisplaySoftTissueMesenchymal()
        {
            this.m_GroupName = "Soft Tissue Mesenchymal";

            this.m_List.Add((Business.Test.Model.ImmunoHistochemistryTest)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("164")); // Vimentin());            
            this.m_List.Add((Business.Test.Model.ImmunoHistochemistryTest)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("153")); // SmoothMuscleActin());
            this.m_List.Add((Business.Test.Model.ImmunoHistochemistryTest)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("92")); // Desmin());            
            this.m_List.Add((Business.Test.Model.ImmunoHistochemistryTest)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("101")); // FactorXIIIa());
            this.m_List.Add((Business.Test.Model.ImmunoHistochemistryTest)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("62")); // CD117());
            this.m_List.Add((Business.Test.Model.ImmunoHistochemistryTest)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("298")); // DOG1());
            this.m_List.Add((Business.Test.Model.ImmunoHistochemistryTest)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("299")); // BetaCatenin());            
        }
    }
}
