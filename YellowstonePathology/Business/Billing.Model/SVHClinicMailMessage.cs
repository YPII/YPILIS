using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Billing.Model
{
    public class SVHClinicMailMessage
    {
        public SVHClinicMailMessage()
        {

        }

        public static int SendMessage()
        {
            StringBuilder messageBody = new StringBuilder();
            int rowCount = Business.Gateway.AccessionOrderGateway.GetSVHClinicMessageBody(messageBody);
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("support@ypii.com", "sid.harder@ypii.com", System.Windows.Forms.SystemInformation.UserName, messageBody.ToString());            
            message.To.Add("amanda.wiederien@sclhealth.org");
            message.To.Add("edie.gonitzke@sclhealth.org");
            message.To.Add("michaela.letasky-brekke@sclhealth.org");
            message.To.Add("svhlab-rd@sclhealth.org");            
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.112 ");
            
            Uri uri = new Uri("http://tempuri.org/");
            System.Net.ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
            System.Net.NetworkCredential credential = credentials.GetCredential(uri, "Basic");

            client.Credentials = credential;
            client.Send(message);
            return rowCount;
        }

        public static void SendReport()
        {
            string messageBody = "Attached is today's YPI billing report.";
            System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(@"c:\temp\billing_report_protected.xlsx");
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("support@ypii.com", "sid.harder@ypii.com", System.Windows.Forms.SystemInformation.UserName, messageBody);
            message.Attachments.Add(attachment);
            message.To.Add("eric.ramsey@ypii.com");
            //message.To.Add("amanda.wiederien@sclhealth.org");
            //message.To.Add("edie.gonitzke@sclhealth.org");
            //message.To.Add("michaela.letasky-brekke@sclhealth.org");
            //message.To.Add("svhlab-rd@sclhealth.org");
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.112 ");

            Uri uri = new Uri("http://tempuri.org/");
            System.Net.ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
            System.Net.NetworkCredential credential = credentials.GetCredential(uri, "Basic");

            client.Credentials = credential;
            client.Send(message);            
        }
    }
}
