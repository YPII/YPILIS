using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
    public class PanelSetMolecularTest : PanelSet
    {
        protected string m_GCode;
        protected bool m_HasSplitCPTCode;

        public PanelSetMolecularTest()
        {
            this.m_GCode = "G0452";
        }

        public string GCode
        {
            get { return this.m_GCode; }
        }

        public bool HasSplitCPTCode
        {
            get { return this.m_HasSplitCPTCode; }
        }
    }
}
