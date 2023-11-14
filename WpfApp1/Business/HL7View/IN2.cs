using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Business.HL7View
{
    public class IN2
    {
        private DateTime m_DateReceived;
        private string m_InsuredEmployerId;
        private string m_InsuredSSN;
        private string m_InsuredMaritalStatus;
        private string m_InsuredPhoneNumber;        
        

        public IN2()
        {

        }

        public void FromHl7(string in1Segment, DateTime dateReceived)
        {
            this.m_DateReceived = dateReceived;

            string[] split = in1Segment.Split('|');
            this.m_InsuredEmployerId = split[1];
            this.m_InsuredSSN = split[2];
            if(split.Length >= 44) this.m_InsuredMaritalStatus = split[43];
            if (split.Length >= 63) this.m_InsuredPhoneNumber = this.GetPhoneNumber(split[63]);
        }

        private string GetPhoneNumber(string hl7Value)
        {
            string result = null;
            string[] caretSplit = hl7Value.Split('^');
            if (caretSplit.Length != 0)
            {
                result = caretSplit[0];
            }
            return result;
        }

        public string InsuredEmployerId
        {
            get { return this.m_InsuredEmployerId; }
            set {  this.m_InsuredEmployerId = value; }
        }

        public string InsuredSSN
        {
            get { return this.m_InsuredSSN; }
            set { this.m_InsuredSSN = value; }
        }

        public string InsuredMaritalStatus
        {
            get { return this.m_InsuredMaritalStatus; }
            set { this.m_InsuredMaritalStatus = value; }
        }

        public string InsuredPhoneNumber
        {
            get { return this.m_InsuredPhoneNumber; }
            set { this.m_InsuredPhoneNumber = value; }
        }

        public DateTime DateReceived
        {
            get { return this.m_DateReceived; }
            set { this.m_DateReceived = value; }
        }

        public JObject ToJson()
        {
            return new JObject(
                new JProperty("insuredSSN", this.m_InsuredSSN),
                new JProperty("insuredEmployerId", this.m_InsuredEmployerId),
                new JProperty("insuredMaritalStatus", this.m_InsuredMaritalStatus),
                new JProperty("insuredPhoneNumber", this.m_InsuredPhoneNumber)                
            );
        }

        public string DisplayString
        {
            get
            {
                StringBuilder result = new StringBuilder();
                result.AppendLine("Date Received: " + this.m_DateReceived.ToShortDateString());
                result.AppendLine("Employer Id: " + this.m_InsuredEmployerId);
                result.AppendLine("SSN: " + this.m_InsuredSSN);
                result.AppendLine("Marital Status: " + this.m_InsuredMaritalStatus);
                result.AppendLine("Phone Number: " + this.m_InsuredPhoneNumber);                                
                return result.ToString();
            }            
        }        
    }
}
