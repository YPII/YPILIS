using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.PanelSet.Model
{
    public class FISHTest : Business.PanelSet.Model.PanelSet
    {
        protected int m_ProbeSetCount;

        public FISHTest()
        {

        }

        public int ProbeSetCount
        {
            get { return this.m_ProbeSetCount; }
            set { this.m_ProbeSetCount = value; }
        }
    }
}
