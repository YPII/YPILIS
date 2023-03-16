using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business
{
    public class SvhAuditItem
    {
        private string m_LastName;
        private string m_FirstName;
        private string m_SvhReportNo;
        private string m_Mrn;
        private string m_Dob;
        private string m_Dos;

        public SvhAuditItem(string lastName, string firstName, string svhReportNo, string mrn, string dob, string dos)
        {
            this.m_LastName = lastName;
            this.m_FirstName = firstName;
            this.m_SvhReportNo = svhReportNo;
            this.m_Mrn = mrn;
            this.m_Dob = dob;
            this.m_Dos = dos;
        }

        public string LastName
        {
            get { return m_LastName; }
        }

        public string FirstName
        {
            get { return m_FirstName; }
        }

        public string SvhReportNo
        {
            get { return m_SvhReportNo; }
        }

        public string Mrn
        {
            get { return m_Mrn; }
        }

        public string Dob
        {
            get { return m_Dob; }
        }

        public string Dos
        {
            get { return m_Dos; }
        }
    }
}
