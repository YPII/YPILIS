using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class PrintCaseReport
    {
        long m_ReportDistributionLogId;
        string m_ReportNo = string.Empty;
        string m_FileName = string.Empty;

        public PrintCaseReport(long reportDistributionlogId)
        {
            this.m_ReportDistributionLogId = reportDistributionlogId;            
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_ReportNo);
			this.m_FileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser);
        }

        public PrintCaseReport(string reportNo)
        {
            this.m_ReportNo = reportNo;
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_ReportNo);
			this.m_FileName = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser);
        }

        public void Print()
        {
            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo(this.m_FileName);
            info.Verb = "Print";
            p.StartInfo = info;
            p.Start();            
        }
    }
}
