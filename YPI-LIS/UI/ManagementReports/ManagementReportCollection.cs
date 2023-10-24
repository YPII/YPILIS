using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.UI.ManagementReports
{
    public class ManagementReportCollection : ObservableCollection<ManagementReport>
    {
        public ManagementReportCollection()
        {
            ManagementReport report1 = new ManagementReport();
            report1.ReportName = "Department Of Health Daily COVID Cases";
            report1.ReportId = "DPMTOFHLTHDLYCOVID";            
            this.Add(report1);
        }
    }
}
