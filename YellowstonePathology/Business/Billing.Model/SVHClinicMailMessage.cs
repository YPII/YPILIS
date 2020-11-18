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
            message.To.Add("rebecca.ricci@sclhealth.org");
            message.To.Add("cheryl.stoltz@sclhealth.org");
            message.To.Add("amanda.wiederien@sclhealth.org");
            message.To.Add("edie.gonitzke@sclhealth.org");
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.112 ");

            Uri uri = new Uri("http://tempuri.org/");
            System.Net.ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
            System.Net.NetworkCredential credential = credentials.GetCredential(uri, "Basic");

            client.Credentials = credential;
            client.Send(message);
            return rowCount;
        }
    }
}
