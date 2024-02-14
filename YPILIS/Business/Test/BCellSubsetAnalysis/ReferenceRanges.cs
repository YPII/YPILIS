using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.BCellSubsetAnalysis
{
    public class ReferenceRanges
    {
        private List<ReferenceRangeValue> m_ReferenceRanges;
        public ReferenceRanges()
        {
            this.m_ReferenceRanges = new List<ReferenceRangeValue>();
            this.m_ReferenceRanges.Add(new ReferenceRangeValue(0, "Mature B-Cells (21+ 38+)", 49.76, 100));
            this.m_ReferenceRanges.Add(new ReferenceRangeValue(1, "Mature B-Cells (21+ 38-)", 1.03, 30.99));
            this.m_ReferenceRanges.Add(new ReferenceRangeValue(2, "Memory B-Cells (27+)", 1.2, 31.48));
            this.m_ReferenceRanges.Add(new ReferenceRangeValue(3, "Non-Switched Memory B-Cells (IgD+ 27+)", 0, 12.62));
            this.m_ReferenceRanges.Add(new ReferenceRangeValue(4, "Marginal Zone B-Cells (IgD+ IgM+ 27+)", 0, 13.33));
            this.m_ReferenceRanges.Add(new ReferenceRangeValue(5, " Class Switched Memory B-Cells (IgD+ IgM 27+)", 0, 15.73));
            this.m_ReferenceRanges.Add(new ReferenceRangeValue(6, "Natice B-Cells (IgD+ 38++)", 42.00, 95.91));
            this.m_ReferenceRanges.Add(new ReferenceRangeValue(7, "Traditional B-Cells (IgM+ 38++)", 0, 4.35));
            this.m_ReferenceRanges.Add(new ReferenceRangeValue(8, "Plasmablasts (38++ IgM-)", 0, 1.81));            
        }        

        public ReferenceRangeValue Get(int id)
        {
            ReferenceRangeValue result = null;
            foreach(ReferenceRangeValue item in this.m_ReferenceRanges)
            {
                if(item.m_CellTypeId == id)
                {
                    result = item;
                    break;
                }
            }
            return result;                
        }
    }
}
