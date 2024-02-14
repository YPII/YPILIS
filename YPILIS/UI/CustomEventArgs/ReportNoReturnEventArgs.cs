using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class ReportNoReturnEventArgs : System.EventArgs
    {
        private string m_ReportNo;

        public ReportNoReturnEventArgs(string reportNo)
        {
            this.m_ReportNo = reportNo;
        }

        public string ReportNo
        {
            get { return this.m_ReportNo; }
        }
    }
}
