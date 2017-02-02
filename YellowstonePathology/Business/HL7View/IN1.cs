using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.HL7View
{
    public class IN1
    {
        private int m_SetId;
        private DateTime m_DateReceived;
        private string m_InsurancePlanId;
        private string m_InsuranceCompanyId;
        private string m_InsuranceName;
        private string m_InsuranceAddress;
        private string m_InsurancePhoneNumber;
        private string m_GroupNumber;
        private string m_GroupName;
        private string m_InusuredsGroupEmployerName;
        private string m_NameOfInsured;
        private string m_PolicyNumber;
        

        public IN1()
        {

        }

        public void FromHl7(string in1Segment, DateTime dateReceived)
        {
            this.m_DateReceived = dateReceived;

            string[] split = in1Segment.Split('|');
            this.m_SetId = Convert.ToInt32(split[1]);
            this.m_InsurancePlanId = split[2];
            this.m_InsuranceCompanyId = split[3];
            this.m_InsuranceName = split[4];
            this.m_InsurancePhoneNumber = split[7];
            this.m_GroupNumber = split[8];
            this.m_GroupName = split[9];
            this.m_InusuredsGroupEmployerName = split[11];
                        
            string [] subNameOfInsuredFields = split[16].Split('^');
            this.m_NameOfInsured = subNameOfInsuredFields[0];
            if(subNameOfInsuredFields.Length == 2)
            {
                this.m_NameOfInsured = this.m_NameOfInsured + ", " + subNameOfInsuredFields[1];
            }
               
            if(string.IsNullOrEmpty(split[5]) == false)
            {
                string[] subAddressSubfields = split[5].Split('^');
                this.m_InsuranceAddress = subAddressSubfields[0] + ", " + subAddressSubfields[2] + ", " + subAddressSubfields[3] + ", " + subAddressSubfields[4];
            }

            if(split.Length >= 36) this.m_PolicyNumber = split[36];            
        }

        public int SetId
        {
            get { return this.m_SetId; }
            set { this.m_SetId = value; }
        }

        public string InsurancePlanId
        {
            get { return this.m_InsurancePlanId; }
            set {  this.m_InsurancePlanId = value; }
        }

        public string InsuranceCompanyId
        {
            get { return this.m_InsuranceCompanyId; }
            set { this.m_InsuranceCompanyId = value; }
        }

        public string InsuranceName
        {
            get { return this.m_InsuranceName; }
            set { this.m_InsuranceName = value; }
        }

        public string InsuranceAddress
        {
            get { return this.m_InsuranceAddress; }
            set { this.m_InsuranceAddress = value; }
        }

        public string PolicyNumber
        {
            get { return this.m_PolicyNumber; }
            set { this.m_PolicyNumber = value; }
        }

        public string InsurancePhoneNumber
        {
            get { return this.m_InsurancePhoneNumber; }
            set { this.m_InsurancePhoneNumber = value; }
        }

        public string GroupNumber
        {
            get { return this.m_GroupNumber; }
            set { this.m_GroupNumber = value; }
        }

        public string GroupName
        {
            get { return this.m_GroupName; }
            set { this.m_GroupName = value; }
        }

        public string InusuredsGroupEmployerName
        {
            get { return this.m_InusuredsGroupEmployerName; }
            set { this.m_InusuredsGroupEmployerName = value; }
        }

        public string NameOfInsured
        {
            get { return this.m_NameOfInsured; }
            set { this.m_NameOfInsured = value; }
        }

        public string InsurancePriority
        {
            get
            {
                string result = null;
                switch (this.m_SetId)
                {
                    case 1:
                        result = "Primary";
                        break;
                    case 2:
                        result = "Secondary";
                        break;
                    default:
                        result = "#" + this.m_SetId.ToString();
                        break;

                }
                return result;
            }
        }
        
        public string DisplayString
        {
            get
            {
                StringBuilder result = new StringBuilder();
                result.AppendLine("Priority: " + this.InsurancePriority);
                result.AppendLine("Date Received: " + this.m_DateReceived.ToShortDateString());
                result.AppendLine(this.m_InsuranceName);
                result.AppendLine(this.m_InsuranceAddress);
                result.AppendLine("Phone Number: " + this.m_InsurancePhoneNumber);
                result.AppendLine("Plan Id: " + this.m_InsurancePlanId);
                result.AppendLine("Policy Number: " + this.m_PolicyNumber);                
                result.AppendLine("Group: " + this.m_GroupName + " - " + this.m_GroupNumber);
                result.AppendLine("Employer: " + this.m_InusuredsGroupEmployerName);
                result.AppendLine("Name Of Insured: " + this.m_NameOfInsured);                
                return result.ToString();
            }            
        }        
    }
}
