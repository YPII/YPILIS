using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class ReportDistributionLogEntryCollection : ObservableCollection<ReportDistributionLogEntry>
    {        
        public ReportDistributionLogEntryCollection()
        {
         
        }

        public void AddEntry(string level, string source, string distributionType, string reportNo, string masterAccessionNo, string physicianName, string clientName, string message)
        {
            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            ReportDistributionLogEntry reportDistributionLogEntry = new ReportDistributionLogEntry(objectId);
            reportDistributionLogEntry.Date = DateTime.Now;
            reportDistributionLogEntry.Level = level;
            reportDistributionLogEntry.DistributionType = distributionType;
            reportDistributionLogEntry.Source = source;
            reportDistributionLogEntry.ReportNo = reportNo;
            reportDistributionLogEntry.MasterAccessionNo = masterAccessionNo;
            reportDistributionLogEntry.PhysicianName = physicianName;
            reportDistributionLogEntry.ClientName = clientName;
            reportDistributionLogEntry.Message = message;
            this.InsertItem(0, reportDistributionLogEntry);
        }           
    }
}
