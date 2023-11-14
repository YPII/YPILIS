using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RestSharp;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class FaxSubmission
    {
        public static DistributionResult Submit(string faxNumber, string subject, string fileName, string jobDescription)
        {
            DistributionResult result = new DistributionResult();

            if (System.IO.File.Exists(fileName) == false)
            {
                result.Message = "Not able to send fax because the file does not exist: " + fileName;
                result.IsComplete = false;
                return result;
            }

            var client = new RestClient("https://api2.westfax.com/REST/Fax_SendFax/json");            
            var request = new RestRequest();
            request.AddHeader("ContentType", "multipart/form-data");
            request.AddParameter("Username", "sid.harder@ypii.com");
            request.AddParameter("Password", "faxorama44");
            request.AddParameter("Cookies", "false");
            request.AddParameter("ProductId", "c6ae242b-4155-4934-bacf-276e9fbf9f53");
            request.AddParameter("JobName", jobDescription);
            request.AddParameter("Header", "Test Header");
            request.AddParameter("BillingCode", jobDescription);
            request.AddParameter("Numbers1", faxNumber);
            request.AddFile("Files0", fileName);            
            request.AddParameter("FaxQuality", "Fine");
            RestResponse response = client.ExecutePost(request);

            result.IsComplete = true;
            return result;
        }

        public static DistributionResult SubmitOld(string faxNumber, string subject, string fileName)
        {
            DistributionResult result = new DistributionResult();
            throw new Exception("this fax sucks.");

            /*
            if (System.IO.File.Exists(fileName) == false)
            {
                result.Message = "Not able to send fax because the file does not exist: " + fileName;
                result.IsComplete = false;
                return result;
            }            

            FAXCOMEXLib.FaxServer faxServer = new FAXCOMEXLib.FaxServer();
            faxServer.Connect("ypiifax");

            FAXCOMEXLib.FaxDocument faxDoc = new FAXCOMEXLib.FaxDocument();
            faxDoc.Body = fileName;

            Business.LocalPhonePrefix localPhonePrefix = new LocalPhonePrefix();
            faxNumber = localPhonePrefix.HandleLongDistance(faxNumber);            

            faxDoc.Recipients.Add(faxNumber, subject);
            faxDoc.DocumentName = subject;
            faxDoc.Sender.Company = "YPII";
            faxDoc.Subject = subject;            

            faxDoc.Priority = FAXCOMEXLib.FAX_PRIORITY_TYPE_ENUM.fptLOW;            
            faxDoc.ConnectedSubmit(faxServer);            
            faxServer.Disconnect();

            result.IsComplete = true;
            */

            //return result;
        }        
    }
}
