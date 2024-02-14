using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Reports
{
    public class DisposalReportData
    {
        public static string NoCasesFound = "No Cases Found";

        List<string> m_AddToHold;
        List<string> m_DisposeOfFromHold;
        string m_DisposeOf;
        DateTime m_DisposalDate;

        public DisposalReportData()
        {
            m_AddToHold = new List<string>();
            m_DisposeOfFromHold = new List<string>();
        }

        public string DisposeOf
        {
            get { return this.m_DisposeOf; }
            set { this.m_DisposeOf = value; }
        }

        public DateTime DisposalDate
        {
            get { return this.m_DisposalDate; }
            set { this.m_DisposalDate = value; }
        }

        public List<string> AddToHold
        {
            get { return this.m_AddToHold; }
        }

        public List<string> DisposeOfFromHold
        {
            get { return this.m_DisposeOfFromHold; }
        }
    }
}
