using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Billing.Model
{
    public class SVHNoCDMMailMessage
    {
        public SVHNoCDMMailMessage()
        {

        }

        public static void SendMessage(string cptCode)
        {
            StringBuilder messageBody = new StringBuilder();
            messageBody.Append("We are unable to send a charge for CPT Code: " + cptCode + " because we do not have a CDM for it.");

            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("support@ypii.com", "sid.harder@ypii.com", System.Windows.Forms.SystemInformation.UserName, messageBody.ToString());
            message.To.Add("edie.gonitzke@sclhealth.org");
            message.To.Add("michaela.letasky-brekke@sclhealth.org");
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.112 ");

            Uri uri = new Uri("http://tempuri.org/");
            System.Net.ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
            System.Net.NetworkCredential credential = credentials.GetCredential(uri, "Basic");

            client.Credentials = credential;
            client.Send(message);            
        }
    }
}
