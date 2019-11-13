using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplayGroupBreast : DisplayGroupIHC
    {
        public DisplayGroupBreast()
        {
            this.m_GroupName = "Breast";
            
            this.m_List.Add((ImmunoHistochemistryTest)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("98")); // EstrogenReceptor());
            this.m_List.Add((ImmunoHistochemistryTest)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("144")); // ProgesteroneReceptor());
            this.m_List.Add((ImmunoHistochemistryTest)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("267")); // HER2DISH());
            this.m_List.Add((ImmunoHistochemistryTest)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("154")); // SmoothMuscleMyosin());
            this.m_List.Add((ImmunoHistochemistryTest)YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("118")); // Mammaglobin());            
        }
    }
}
