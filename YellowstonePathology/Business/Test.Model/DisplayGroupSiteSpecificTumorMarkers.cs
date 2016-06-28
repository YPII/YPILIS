using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
    public class DisplayGroupSiteSpecificTumorMarkers : DisplayGroupIHC
    {
        public DisplayGroupSiteSpecificTumorMarkers()
        {
            this.m_GroupName = "Site Specific Tumor Markers";            

            this.m_List.Add(new NapsinA());
            this.m_List.Add(new Thyroglobulin());
            this.m_List.Add(new TTF1());
            this.m_List.Add(new CA199());
            this.m_List.Add(new CA125());
            this.m_List.Add(new Calretinin());
            this.m_List.Add(new CEA());
            this.m_List.Add(new PlacentalAlkalinePhosphatase());
            this.m_List.Add(new RCC());
            this.m_List.Add(new HepatocyteSpecificAntigen());
            this.m_List.Add(new PAX8());
            this.m_List.Add(new Glypican3());
            this.m_List.Add(new GATA3());
        }
    }
}
