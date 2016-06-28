using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business
{
    public class ReportNoLetter
    {
        protected string m_Letter;
        protected bool m_AllowMultipleInSameAccession;

        public ReportNoLetter()
        {

        }

        public string Letter
        {
            get { return this.m_Letter; }
        }

        public bool AllowMultipleInSameAccession
        {
            get { return this.m_AllowMultipleInSameAccession; }
        }
    }
}
