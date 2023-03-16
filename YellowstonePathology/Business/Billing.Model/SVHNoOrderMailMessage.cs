using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Billing.Model
{
    public class SVHNoOrderMailMessage
    {
        public SVHNoOrderMailMessage()
        {

        }

        public static void SendMessage(string firstName, string lastName, string dob)
        {
            string messageBody = $"A specimen was received but an order could not be found for the following patient: {lastName}, {firstName}: {dob}";
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("support@ypii.com", "sid.harder@ypii.com", System.Windows.Forms.SystemInformation.UserName, messageBody);            
            //message.To.Add("amanda.wiederien@sclhealth.org");
            //message.To.Add("edie.gonitzke@sclhealth.org");
            //message.To.Add("michaela.letasky-brekke@sclhealth.org");
            //message.To.Add("svhlab-rd@sclhealth.org");            
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.110 ");
            
            Uri uri = new Uri("http://tempuri.org/");
            System.Net.ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
            System.Net.NetworkCredential credential = credentials.GetCredential(uri, "Basic");

            client.Credentials = credential;
            client.Send(message);            
        }
    }
}
