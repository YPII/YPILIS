using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class FaxDistribution : Distribution
    {
        public const string DistributionType = "Fax";
        protected FAXCOMEXLib.FAX_PRIORITY_TYPE_ENUM m_Priority;

        public FaxDistribution()
            : base(DistributionType)
        {
            this.m_Priority = FAXCOMEXLib.FAX_PRIORITY_TYPE_ENUM.fptNORMAL;
        }		

        public override YellowstonePathology.Business.Rules.MethodResult Distribute(string reportNo, Business.Test.AccessionOrder accessionOrder)
        {            
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();

            /*
			YellowstonePathology.Domain.OrderIdParser orderIdParser = new YellowstonePathology.Domain.OrderIdParser(rdl.ReportNo);
			string fileName = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNameTif(orderIdParser);

            FAXCOMEXLib.FaxServer faxServer = new FAXCOMEXLib.FaxServer();
            faxServer.Connect("ypiiblfax");

            FAXCOMEXLib.FaxDocument faxDoc = new FAXCOMEXLib.FaxDocument();
            faxDoc.Body = fileName;

            
            string faxNumber = string.Empty;
            if (rdl.LongDistance == true)
            {
                faxNumber = "1" + rdl.FaxNumber;
            }
            else
            {
                faxNumber = rdl.FaxNumber;
            }

            faxDoc.Recipients.Add(faxNumber, rdl.ReportNo);
            faxDoc.DocumentName = rdl.ReportDistributionLogId;
            faxDoc.Sender.Company = "YPII";
            faxDoc.Subject = rdl.PhysicianName + " - " + rdl.ClientName;

            faxDoc.Priority = FAXCOMEXLib.FAX_PRIORITY_TYPE_ENUM.fptLOW;

            faxDoc.ConnectedSubmit(faxServer);
            faxServer.Disconnect();

            rdl.CaseDistributed = true;
            rdl.DateDistributed = DateTime.Now;

             */

            result.Success = true;
            return result;
        }
    }
}
