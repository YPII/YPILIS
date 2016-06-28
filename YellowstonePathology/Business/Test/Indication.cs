using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test
{
    public class Indication
    {
        private string m_IndicationCode;
        private string m_Description;

        public Indication()
        {

        }

        public Indication(string indicationCode, string description)
        {
            this.m_IndicationCode = indicationCode;
            this.m_Description = description;
        }

        public string IndicationCode
        {
            get { return this.m_IndicationCode; }
        }

        public string Description
        {
            get { return this.m_Description; }
        }
    }
}
