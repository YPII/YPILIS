using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.Panther
{
    public class PantherAssay
    {
        private string m_LongName;
        private string m_ShortName;
        private int m_YPITestId;
        private List<string> m_AnalyteList;

        public PantherAssay(string longName, string shortName, int ypiTestId)
        {
            this.m_AnalyteList = new List<string>();

            this.m_LongName = longName;
            this.m_ShortName = shortName;
            this.m_YPITestId = ypiTestId;
        }

        public List<string> AnalyteList
        {
            get { return this.m_AnalyteList; }
        }

        public string LongName
        {
            get { return this.m_LongName; }
            set { this.m_LongName = value; }
        }

        public string ShortName
        {
            get {return this.m_ShortName;}
            set { this.m_ShortName = value; }
        }

        public int YPITestId
        {
            get { return this.YPITestId; }
            set { this.m_YPITestId = value; }
        }
    }
}
