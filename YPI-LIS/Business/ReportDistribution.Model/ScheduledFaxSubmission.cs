using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class ScheduledFaxSubmission 
    {
        public static DistributionResult Submit(string faxNumber, string subject, string fileName, DateTime timeToSchedule)
        {
            DistributionResult result = new DistributionResult();

            FAXCOMEXLib.FaxServer faxServer = new FAXCOMEXLib.FaxServer();
            faxServer.Connect("ypiifax");

            FAXCOMEXLib.FaxDocument faxDoc = new FAXCOMEXLib.FaxDocument();
            faxDoc.Body = fileName;

            faxNumber = "1" + faxNumber;

            faxDoc.Recipients.Add(faxNumber, subject);
            faxDoc.DocumentName = subject;
            faxDoc.Sender.Company = "YPII";
            faxDoc.Subject = subject;
            faxDoc.ScheduleType = FAXCOMEXLib.FAX_SCHEDULE_TYPE_ENUM.fstSPECIFIC_TIME;
            faxDoc.ScheduleTime = timeToSchedule;

            faxDoc.Priority = FAXCOMEXLib.FAX_PRIORITY_TYPE_ENUM.fptLOW;
            faxDoc.ConnectedSubmit(faxServer);
            faxServer.Disconnect();

            result.IsComplete = true;
            return result;
        }
    }
}
