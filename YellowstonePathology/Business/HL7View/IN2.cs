using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this.m_InsuredMaritalStatus = split[43];
            this.m_InsuredPhoneNumber = split[63];
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
