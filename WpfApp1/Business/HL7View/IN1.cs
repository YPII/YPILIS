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
    public class IN1
    {
        private int m_SetId;
        private DateTime m_DateReceived;
        private string m_InsurancePlanId;
        private string m_InsuranceCompanyId;
        private string m_InsuranceName;
        private string m_InsuranceAddressLine1;
        private string m_InsuranceAddressLine2;
        private string m_InsuranceCity;
        private string m_InsuranceState;
        private string m_InsuranceZip;
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

            string[] insurancePlanIdSplit = split[2].Split('^');
            this.m_InsurancePlanId = insurancePlanIdSplit[0];

            this.m_InsuranceCompanyId = split[3];
            this.m_InsuranceName = split[4];
            this.m_InsurancePhoneNumber = this.GetPhoneNumber(split[7]);
            this.m_GroupNumber = split[8];
            this.m_GroupName = split[9];
            this.m_InusuredsGroupEmployerName = split[11];
                        
            if(split.Length >= 17)
            {
                string[] subNameOfInsuredFields = split[16].Split('^');
                this.m_NameOfInsured = subNameOfInsuredFields[0];
                if (subNameOfInsuredFields.Length >= 2)
                {
                    this.m_NameOfInsured = this.m_NameOfInsured + ", " + subNameOfInsuredFields[1];
                }
            }            

            if (string.IsNullOrEmpty(split[5]) == false)
            {
                string[] subAddressSubfields = split[5].Split('^');
                this.m_InsuranceAddressLine1 = subAddressSubfields[0];
                if(subAddressSubfields.Length >= 5)
                {
                    this.m_InsuranceAddressLine2 = subAddressSubfields[1];
                    this.m_InsuranceCity = subAddressSubfields[2];
                    this.m_InsuranceState = subAddressSubfields[3];
                    this.m_InsuranceZip = subAddressSubfields[4];
                }                
            }

            if (split.Length >= 36) this.m_PolicyNumber = split[36];            
        }

        private string GetPhoneNumber(string hl7Value)
        {
            string result = null;
            string[] caretSplit = hl7Value.Split('^');
            if(caretSplit.Length != 0)
            {
                result = caretSplit[0];
            }
            return result;
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

        public string InsuranceAddressLine1
        {
            get { return this.m_InsuranceAddressLine1; }
            set { this.m_InsuranceAddressLine1 = value; }
        }

        public string InsuranceAddressLine2
        {
            get { return this.m_InsuranceAddressLine2; }
            set { this.m_InsuranceAddressLine2 = value; }
        }

        public string InsuranceCity
        {
            get { return this.m_InsuranceCity; }
            set { this.m_InsuranceCity = value; }
        }

        public string InsuranceState
        {
            get { return this.m_InsuranceState; }
            set { this.m_InsuranceState = value; }
        }

        public string InsuranceZip
        {
            get { return this.m_InsuranceZip; }
            set { this.m_InsuranceZip = value; }
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

        public JObject ToJson()
        {
            return new JObject(
                new JProperty("groupName", Business.Helper.JSONHelper.HandleValue(this.m_GroupName)),
                new JProperty("groupNumber", Business.Helper.JSONHelper.HandleValue(this.GroupNumber)),
                new JProperty("insuranceAddressLine1", Business.Helper.JSONHelper.HandleValue(this.m_InsuranceAddressLine1)),
                new JProperty("insuranceAddressLine2", Business.Helper.JSONHelper.HandleValue(this.m_InsuranceAddressLine2)),
                new JProperty("insuranceAddressCity", Business.Helper.JSONHelper.HandleValue(this.m_InsuranceCity)),
                new JProperty("insuranceAddressState", Business.Helper.JSONHelper.HandleValue(this.m_InsuranceState)),
                new JProperty("insuranceAddressZip", Business.Helper.JSONHelper.HandleValue(this.m_InsuranceZip)),
                new JProperty("insuranceCompanyId", Business.Helper.JSONHelper.HandleValue(this.m_InsuranceCompanyId)),
                new JProperty("insuranceName", Business.Helper.JSONHelper.HandleValue(this.m_InsuranceName)),
                new JProperty("insurancePhoneNumber", Business.Helper.JSONHelper.HandleValue(this.m_InsurancePhoneNumber)),
                new JProperty("insurancePlanId", Business.Helper.JSONHelper.HandleValue(this.m_InsurancePlanId)),
                new JProperty("insuranceInsuredsGroupEmployerName", Business.Helper.JSONHelper.HandleValue(this.m_InusuredsGroupEmployerName)),
                new JProperty("nameOfInsured", Business.Helper.JSONHelper.HandleValue(this.m_NameOfInsured)),
                new JProperty("policyNumber", Business.Helper.JSONHelper.HandleValue(this.m_PolicyNumber))                
            );
        }
        
        public string DisplayString
        {
            get
            {
                StringBuilder result = new StringBuilder();
                result.AppendLine("Priority: " + this.InsurancePriority);
                result.AppendLine("Date Received: " + this.m_DateReceived.ToShortDateString());
                result.AppendLine(this.m_InsuranceName);
                result.AppendLine(this.m_InsuranceAddressLine1 + " " + this.m_InsuranceCity + ", " + this.m_InsuranceState + " " + this.m_InsuranceZip);
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
