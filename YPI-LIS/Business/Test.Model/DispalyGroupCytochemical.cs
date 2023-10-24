using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplayGroupCytochemical
    {
        protected string m_GroupName;
        protected List<Test> m_List;

        public DisplayGroupCytochemical()
        {
            this.m_GroupName = "Cytochemical";

            this.m_List = new List<Test>();
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("50")); // AcidFast());
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("51")); // AlcianBlue());
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("84")); // CongoRed());
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("213")); // CopperRhodanine());
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("96")); // Elastic());
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("269")); // Fites());
            this.m_List.Add(new Giemsa());
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("106")); // GMS());
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("112")); // HuckerTwort());
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("115")); // Iron());
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("119")); // MelanA());
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("123")); // Mucin());
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("131")); // OilRedO());
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("137")); // PAS());
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("138")); // PASAlcianBlue());
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("139")); // PASforFungus());
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("140")); // PASWithDiastase());
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("151")); // Reticulin());
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("155")); // SteinerandSteiner());
            this.m_List.Add(YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("160")); // Trichrome());            
        }

        public string GroupName
        {
            get { return this.m_GroupName; }
        }

        public List<Test> List
        {
            get { return this.m_List; }
        }
    }
}
