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
            this.m_List.Add(new AcidFast());
            this.m_List.Add(new AlcianBlue());
            this.m_List.Add(new CongoRed());
            this.m_List.Add(new CopperRhodanine());
            this.m_List.Add(new Elastic());
            this.m_List.Add(new Fites());
            this.m_List.Add(new Giemsa());
            this.m_List.Add(new GMS());
            this.m_List.Add(new HuckerTwordt());
            this.m_List.Add(new Iron());
            this.m_List.Add(new MelanA());
            this.m_List.Add(new Mucin());
            this.m_List.Add(new OilRedO());
            this.m_List.Add(new PAS());
            this.m_List.Add(new PASAlcianBlue());
            this.m_List.Add(new PASforFungus());
            this.m_List.Add(new PASWithDiastase());
            this.m_List.Add(new Reticulin());
            this.m_List.Add(new SteinerandSteiner());
            this.m_List.Add(new Trichrome());
            this.m_List.Add(new AlphaNaphthylAcetateEsterase());            
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
